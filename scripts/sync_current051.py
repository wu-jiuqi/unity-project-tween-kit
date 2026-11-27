"""Refresh the reviewed 0.5.1 slices; never overwrite unrelated kit adapters.

Run inventory.py first. Run catalog.py after this script, then materialize/build.
Old optional assets keep an explicit frozen source record if no longer in the host.
"""
import argparse, difflib, json, shutil, re
from pathlib import Path
from inventory import KIT, sha, write
from prepare_project import stable_meta

def main():
    ap=argparse.ArgumentParser();ap.add_argument('--source',type=Path,required=True);src=ap.parse_args().source.resolve()
    root=KIT/'assets/unity-kit';runtime=root/'Runtime';records=[]
    diffs=json.loads((KIT/'references/extraction-diffs.json').read_text(encoding='utf-8'))
    def record(source,target,entry,adaptation):
        original=src/source;snapshot=KIT/'references/source'/(source+'.txt');snapshot.parent.mkdir(parents=True,exist_ok=True);shutil.copy2(original,snapshot)
        owner=Path(source).stem.split('.')[0]
        rec=dict(source=source,sourceSha256=sha(original),snapshot=snapshot.relative_to(KIT).as_posix(),target=target.relative_to(KIT).as_posix(),targetSha256=sha(target),galleryEntry=entry,component=target.stem,owner=owner,methods=['presentation slice; inspect recorded diff'],adaptation=adaptation)
        records.append(rec);diffs[:]=[d for d in diffs if d['target']!=rec['target']];diffs.append({**rec,'diff':list(difflib.unified_diff(original.read_text(encoding='utf-8-sig').splitlines(),target.read_text(encoding='utf-8').splitlines(),lineterm=''))})
    def copy(name,entry,transform=lambda s:s):
        rel='Assets/Scripts/UI/'+name+'.cs';old=(src/rel).read_text(encoding='utf-8-sig')
        code=old.replace('namespace BounceGarden','namespace ProjectTweenKit').replace('Time.unscaledDeltaTime','KitClock.Delta').replace('Time.deltaTime','KitClock.ScaledDelta').replace('Time.unscaledTime','KitClock.Now')
        p=runtime/(name+'.cs');p.write_text(transform(code),encoding='utf-8');record(rel,p,entry,'0.5.1表现切片；KitClock替换Unity时钟；宿主业务与屏幕布局适配见 current-051.md')
    copy('CommonButtonsA','errorpopup')
    copy('LevelLoadingArt','loadingbox');copy('LoadingBoxAnimation','loadingbox',lambda s:s.replace('public const float CatCycleDuration=4.2f;','public float CycleDuration=4.2f; public const float CatCycleDuration=4.2f;').replace('/CatCycleDuration','/Mathf.Max(.01f,CycleDuration)').replace(',CatCycleDuration)',',Mathf.Max(.01f,CycleDuration))')
         .replace(' void AnimatePart(', ' public bool AutoPlay;\n void Update(){if(AutoPlay)Render(Elapsed+KitClock.Delta);}\n public void StopAndRestore(){Render(0);}\n void OnDisable(){StopAndRestore();}\n void AnimatePart('))
    copy('ResultsC048Art','victorycat')
    copy('OpeningStory049','teaching',lambda s:s.replace('void Update(){Age+=KitClock.Delta;Render();}','public void Sample(float seconds){Age=Mathf.Max(0,seconds);Render();}\n public void SampleStep(int index,float phase){steps[index].Render(phase);}') .replace('static float Rest(', 'public static float Rest('))
    for name,cut in [('OpeningYarnArt','[DefaultExecutionOrder(200)]'),('OpeningThirdArt','[DefaultExecutionOrder(500)]'),('SevenSpecialArt',' public static Transform Tool('),('SecondGroupArt',None)]:
        rel='Assets/Scripts/World/'+name+'.cs';s=(src/rel).read_text(encoding='utf-8-sig').replace('namespace BounceGarden','namespace ProjectTweenKit')
        if cut:s=s[:s.index(cut)]+ ('}\n}\n' if name=='SevenSpecialArt' else '}\n')
        s=s.replace('CollectionLevels.Pitch','.76f')
        p=runtime/(name+'.cs');p.write_text(s,encoding='utf-8');record(rel,p,'teaching','仅静态图集读取、实际切片、轴心与显示尺寸；移除游戏对象控制类')
    for name in ['VictoryPresentation','VictoryPresentation.Stars','FailurePresentation']:
        # These replace earlier extracted classes rather than creating duplicate types.
        old=runtime/'Extracted'/(name+'.cs')
        def adapt(s,name=name):
            s=s.replace('LevelCatalog.Count','45').replace('CommonButtonsA.Button','CurrentKitArt.Button')
            if name=='VictoryPresentation':s=s.replace(' public bool Ready{',' public float Elapsed=>clock;\n public bool Ready{')
            return s
        copy(name,'failure' if name=='FailurePresentation' else 'victory',adapt)
        generated=runtime/(name+'.cs');old.write_text(generated.read_text(encoding='utf-8'),encoding='utf-8');generated.unlink();records[-1]['target']=old.relative_to(KIT).as_posix();diffs[-1]['target']=records[-1]['target']
    for name,win in [('VictoryCatAnimation',True),('FailureCatAnimation',False)]:
        p=runtime/'Extracted'/(name+'.cs');p.write_text('using UnityEngine;\nnamespace ProjectTweenKit { public static class '+name+' {\n public const int FrameCount=8; public const float Duration='+('1.92f' if win else '2.32f')+';\n public static int FrameAt(float age)=>ResultsC048Art.FrameAt('+str(win).lower()+',age);\n public static Sprite Get(int index)=>ResultsC048Art.Frame('+str(win).lower()+',index);\n}}\n',encoding='utf-8')
        record('Assets/Scripts/UI/ResultsC048Art.cs',p,'victorycat' if win else 'failurecat','保留类名与FrameAt/Get接口；切换当前8帧与非均匀时长')
    # Native UI mask uses current artwork, while keeping the kit Settle API.
    def star(s):
        s=s.replace('BattleGardenArt050.Get','CurrentKitArt.Star').replace('BattleGardenArt050.Image','CurrentKitArt.StarImage')
        s=s.replace(' public float Fill=>fill;', ' public bool Settled=>flight<0&&Mathf.Abs(fill-pending)<.002f;\n public float Fill=>fill;')
        s=s.replace(' void OnDestroy()', ' void OnDisable(){if(comet!=null)comet.gameObject.SetActive(false);}\n void OnDestroy()')
        return s
    copy('StarChargeView','nativecharge',star);p=runtime/'StarChargeView.cs';q=runtime/'Extracted/StarChargeView.cs';q.write_text(p.read_text(encoding='utf-8'),encoding='utf-8');p.unlink();records[-1]['target']=q.relative_to(KIT).as_posix();diffs[-1]['target']=records[-1]['target']
    # Goal choreography uses a caller-bound local canvas instead of GameSession/AppUI.
    def celebration(s):
        s=s[:s.index('public static class GoalCelebrationArt {')]+ '}\n'
        s=s.replace(' AppUI app;GameSession game;Action completed;int generation;float age;',' float age;public float Elapsed=>age;public bool Docked=>age>=Duration;public bool ReducedMotion;public float PlaybackSpeed=1;')
        start=s.index(' public void Begin(');end=s.index('  Stretch();',start)
        s=s[:start]+' public void Begin(){\n  if(content!=null){age=0;Render();return;}canvas=GetComponentInParent<Canvas>();\n'+s[end:]
        s=s.replace('public void Cancel(){completed=null;','public void Cancel(){').replace('void OnDisable(){completed=null;}void OnDestroy(){completed=null;}','void OnDisable(){age=0;}')
        s=s.replace('Canvas canvas;Vector2Int viewport;Rect safe;Vector3 parentScale;','').replace('bool suspended,focused=true,finished;int layoutRevision=-1;','bool suspended,focused=true;').replace('canvas=GetComponentInParent<Canvas>();','')
        s=s.replace('void OnDisable(){age=0;}', 'void OnDisable(){if(content!=null)StopAndRestore();}')
        a=s.index('  viewport=app.AlphabetViewport;');b=s.index('  LayoutFits=',a)
        s=s[:a]+'''  var r=((RectTransform)transform).rect;float aw=r.width>1?r.width:1080,ah=r.height>1?r.height:1920;
  unit=Mathf.Min(aw/1080,ah/1920);basis=Vector3.one*unit;center=Vector2.zero;destination=new Vector2(0,-ah*.32f);
'''+s[b:]
        a=s.index('  if(game==null');b=s.index(' void Render()',a)
        s=s[:a]+'''  if(suspended||!focused)return;age+=Mathf.Min(KitClock.Delta,.05f)*PlaybackSpeed;Layout();Render();
 }
 public void Sample(float seconds){age=Mathf.Max(0,seconds);Layout();Render();}
 public void StopAndRestore(){Sample(0);}
'''+s[b:]
        s=s.replace('FeedbackProfile.Active.enabledFeedback','!ReducedMotion').replace('  RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform,point,canvas.renderMode==RenderMode.ScreenSpaceOverlay?null:canvas.worldCamera,out var local);\n  fitRoot.anchoredPosition=local;','  fitRoot.anchoredPosition=point;')
        s=s.replace('  dim.color=', '  dim.raycastTarget=age<Duration;\n  dim.color=')
        s=s.replace('public sealed class GoalCelebration:', 'public sealed class GoalCelebration:')
        return s
    copy('GoalCelebration','celebration',celebration)
    for name,entry,source in [('ToastMotion','toast','ToastNotification0410'),('PageTransition','pageflow','AppUI'),('ThreeStepLoop','teaching','OpeningStory049'),('MovesFeedback','lowmoves','ChainCollectionHUD'),('ErrorPopupMotion','errorpopup','ErrorPopup0411'),('ResultSurface048','victory','ResultSurface048')]:record('Assets/Scripts/UI/'+source+'.cs',runtime/(name+'.cs'),entry,'手工抽取纯表现接口；保留时序，业务事件由宿主输入')
    # Only selected current resource closures are copied; byte/meta identity is preserved.
    mapping=json.loads((KIT/'references/resource-map.json').read_text(encoding='utf-8'));by={m['target']:m for m in mapping}
    resources=src/'Assets/Resources'
    selected=[]
    for folder in ['Art/CommonButtonsA','Art/LevelLoadingC','Art/Toasts0410','Art/ResultsC048','Art/GoalCelebration047','Art/YarnObstacleV3','Art/ThirdGroup','Art/FourthGroup','Art/SecondGroupProduction','Art/FiveBakedV80']:
        selected+=list((resources/folder).rglob('*'))
    selected += [resources/'Art/GoalCelebrationV1/halo.png',resources/'Art/BattleGarden050/star-empty-v22.png',resources/'Art/BattleGarden050/star-gold-v18.png',resources/'OpeningStory049UI.shader',resources/'Art/YarnV19/ball_yarn_apricot.png',resources/'Art/BattleUIV42/goals-tray.png']
    for p in selected:
        if not p.is_file() or p.suffix=='.meta':continue
        q=root/'Resources'/p.relative_to(resources);q.parent.mkdir(parents=True,exist_ok=True);shutil.copy2(p,q);meta=p.with_name(p.name+'.meta')
        if meta.exists():shutil.copy2(meta,q.with_name(q.name+'.meta'))
        target=q.relative_to(KIT).as_posix();guid=re.search(r'^guid: (\w+)',meta.read_text(encoding='utf-8-sig'),re.M) if meta.exists() else None
        by[target]=dict(source=p.relative_to(src).as_posix(),target=target,sha256=sha(q),sourceGuid=guid[1] if guid else None,reason='0.5.1 reviewed presentation closure',license='Project assets; permission unchanged; consult licenses.md',sourceVersion='0.5.1')
    # Existing resources are an explicit legacy preset, never falsely claimed as current bytes.
    for m in by.values():
        original=src/m['source']
        sourceMeta=original.with_name(original.name+'.meta');targetMeta=(KIT/m['target']).with_name(Path(m['target']).name+'.meta')
        changedMeta=sourceMeta.exists() and targetMeta.exists() and sha(sourceMeta)!=sha(targetMeta)
        if (m['source'].startswith('Assets/') or m.get('scope')=='project-platform') and (not original.exists() or sha(original)!=m['sha256'] or changedMeta):m['sourceStatus']='frozen-0.4.0-reference'
        elif original.exists():m.pop('sourceStatus',None)
        meta=(KIT/m['target']).with_name(Path(m['target']).name+'.meta')
        if meta.exists():m['packagedMetaSha256']=sha(meta)
    # Replaced extracted classes have the same maintained target, not a second diff.
    uniqueDiffs={d['target']:d for d in diffs}
    for d in uniqueDiffs.values():
        original=src/d['source'];target=KIT/d['target']
        if original.exists():
            d['sourceSha256']=sha(original);d['targetSha256']=sha(target)
            d['diff']=list(difflib.unified_diff(original.read_text(encoding='utf-8-sig').splitlines(),target.read_text(encoding='utf-8-sig').splitlines(),fromfile=d['source'],tofile=d['target'],lineterm=''))
        else:d['sourceStatus']='frozen-0.4.0-reference'
    write(KIT/'references/resource-map.json',list(by.values()));write(KIT/'references/extraction-diffs.json',list(uniqueDiffs.values()));write(KIT/'references/current-051-deliveries.json',dict(version='0.5.1',date='2026-11-27',entries=records))
    for p in root.rglob('*'):
        if p.suffix!='.meta':stable_meta(p,root)
    print('Synced',len(records),'reviewed source slices')

if __name__=='__main__':main()
