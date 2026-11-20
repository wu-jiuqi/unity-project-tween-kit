"""Reproducible extraction. Exact source copies + recorded, small host adaptations.

Original GUIDs remain on original resources. New scripts use stable path-derived GUIDs.
"""
from pathlib import Path
import argparse, hashlib, json, shutil, re, difflib
from inventory import KIT, write, sha

def main():
    ap=argparse.ArgumentParser();ap.add_argument('--source',type=Path,required=True);ap.add_argument('--dotween-standard',type=Path,default=KIT/'assets/unity-kit/Plugins/DOTween');a=ap.parse_args();src=a.source.resolve()/'Assets'
    dest=KIT/'assets/unity-kit';dest.mkdir(parents=True,exist_ok=True);changes=[];mapping=[]
    def emit(relative,text):
        p=dest/relative;p.parent.mkdir(parents=True,exist_ok=True);p.write_text(text,encoding='utf-8')
    def copycode(relative,transform=None):
        p=src/'Scripts'/relative;old=p.read_text(encoding='utf-8-sig');new=old.replace('namespace BounceGarden','namespace ProjectTweenKit')
        new=new.replace('Time.unscaledDeltaTime','KitClock.Delta').replace('Time.deltaTime','KitClock.ScaledDelta').replace('Time.unscaledTime','KitClock.Now')
        new=new.replace('.SetUpdate(true)','.SetUpdate(KitClock.TweenUpdate, true)')
        if transform:new=transform(new)
        emit('Runtime/Extracted/'+p.name,new)
        changes.append({'source':'Assets/Scripts/'+relative,'sourceSha256':sha(p),'target':'assets/unity-kit/Runtime/Extracted/'+p.name,'targetSha256':sha(dest/'Runtime/Extracted'/p.name),'diff':list(difflib.unified_diff(old.splitlines(),new.splitlines(),fromfile=relative,tofile=p.name,lineterm=''))})
    def press(s):
        s=s.replace('Button button; AppUI app;', 'public float HoverDuration=.16f, ExitDuration=.32f, PressDuration=.10f, ReleaseDuration=.48f, IdleDuration=.7f;\n public float Magnitude=1; public System.Func<bool> CanAnimate;\n Button button;')
        s=s.replace('void Update(){Initialize();','void Update(){if(KitClock.Paused)return;Initialize();')
        s=s.replace('app=GetComponentInParent<AppUI>();','')
        s=s.replace('(app!=null&&!app.HomeMotionAllowed)','(CanAnimate!=null&&!CanAnimate())')
        s=s.replace('float press=Mathf.Max(0,-pose),hover=Mathf.Max(0,pose);','float press=Mathf.Max(0,-pose)*Magnitude,hover=Mathf.Max(0,pose)*Magnitude;')
        for old,new in [('Animate(1,.16f','Animate(1,HoverDuration'),('Animate(0,.32f','Animate(0,ExitDuration'),('Animate(-1,.10f','Animate(-1,PressDuration'),('Animate(hovered?1:0,.48f','Animate(hovered?1:0,ReleaseDuration'),('},1,.7f)','},1,IdleDuration)')]:s=s.replace(old,new)
        # The source waited until Start to register mesh effect. Keep explicit refresh available for tests/adapters.
        return s
    copycode('UI/UIBounce.cs',press)
    copycode('UI/AlphabetTweenExtensions.cs')
    copycode('UI/ActivityJellyMotion.cs',lambda s:s.replace('float dt=Mathf.Min(KitClock.Delta,.04f);','float dt=Mathf.Min(KitClock.Delta,.04f);'))
    copycode('UI/HomeWordmarkWiggle.cs',lambda s:s.replace('AppUI app;','public System.Func<bool> CanAnimate;').replace('app=GetComponentInParent<AppUI>();','').replace('(app!=null&&!app.HomeMotionAllowed)','(CanAnimate!=null&&!CanAnimate())'))
    copycode('World/JellyMotion.cs')
    def copper(s):
        s=s[:s.index('[DefaultExecutionOrder')]
        s='\n'.join(line for line in s.splitlines() if not line.startswith(' public static void Fit(') and not line.startswith(' public static SpriteRenderer Image('))+'\n}\n'
        return s
    copycode('World/OpeningCopperArt.cs',copper)
    copycode('UI/FailureCatAnimation.cs');copycode('UI/VictoryCatAnimation.cs')
    copycode('UI/ShopScrollDrag.cs')
    copycode('UI/EncouragementDrag.cs')
    copycode('UI/PastelLogo.cs');copycode('UI/GameTypography.cs')
    copycode('UI/CSettingsArt.cs')
    copycode('UI/SettingsSwitch77.cs',lambda s:s.replace('Func<bool> read;', 'public float SlideDuration=.16f;\n Func<bool> read;').replace('new Vector2(x,0),.16f','new Vector2(x,0),SlideDuration').replace('void OnDisable(){slide?.Kill(false);}','void OnDisable(){slide?.Kill(false);if(read!=null&&knob!=null)Refresh(false);}'))
    copycode('UI/StarChargeView.cs',lambda s:s.replace('void OnDestroy(){','void OnDisable(){if(comet!=null)comet.gameObject.SetActive(false);}\n void OnDestroy(){'))
    copycode('UI/VictoryArt.cs');copycode('UI/FailureArt.cs')
    copycode('UI/VictoryPresentation.cs');copycode('UI/VictoryPresentation.Stars.cs');copycode('UI/FailurePresentation.cs')
    copycode('UI/ResultAudioTimeline.cs');copycode('UI/LeagueMotionArt.cs')
    # Only remove the partial AppUI host and keep the complete presentation class verbatim.
    def goal(s):
        start=s.index('public partial class AppUI');end=s.index('// Move the real target cards')
        return s[:start]+s[end:]
    copycode('UI/CollectionGoalEntrance.cs',goal)
    def logo(s):
        s=s.replace('new WaitForSecondsRealtime(1f/30f)','new KitWait(1f/30f)')
        s=s.replace('public void Begin(){logo=', 'public void Begin(){StopAndRestore();logo=')
        s=s.replace('void OnDestroy(){reveal?.Kill();}', 'public void StopAndRestore(){reveal?.Kill(false);StopAllCoroutines();if(logo!=null){logo.gameObject.SetActive(false);Destroy(logo.gameObject);logo=null;}if(quote!=null){quote.gameObject.SetActive(false);Destroy(quote.gameObject);quote=null;}glyphs.Clear();}\n void OnDisable(){StopAndRestore();}\n void OnDestroy(){StopAndRestore();}')
        return s
    copycode('UI/JellyLogoIntro.cs',logo)
    def loading(s):
        s=s.replace('if(trackContentStatus&&GameContent.Status!=null&&label.text!=GameContent.Status())label.text=GameContent.Status();','if(trackContentStatus&&ContentStatus!=null)label.text=ContentStatus();').replace('public const float CatCycleDuration', 'public System.Func<string> ContentStatus;\n public const float CatCycleDuration')
        s=s.replace(' public void SetStatus(', ' public void StopAndRestore(){running=false;Elapsed=0;if(letters!=null&&poses!=null)Render();}\n void OnDisable(){StopAndRestore();}\n public void SetStatus(')
        return s
    copycode('UI/LoadingPresentation.cs',loading)
    # UICraft retains original layout/mesh/font constructors; remove game-only art factories.
    def craft(s):
        start=s.index(' public static Image Art(');end=s.index(' public static TMP_Text Label',start)
        s=s[:start]+''' public static Image Art(Transform p,string resource,float x,float y,float w,float h){var i=Box(p,resource,x,y,w,h,Color.white);i.sprite=PastelArt.Resolve(resource)??PastelArt.Get(resource)??Resources.Load<Sprite>("Art/"+resource);if(i.sprite==null)throw new InvalidOperationException("Missing example art "+resource);i.type=Image.Type.Simple;i.preserveAspect=true;return i;}
'''+s[end:]
        start=s.index(' public static Button Button(');end=s.index(' public static float OutBack',start)
        s=s[:start]+''' public static Button Button(Transform p,string value,float x,float y,float w,float h,Color color,Action action,int size=44)=>CSettingsArt.Button(p,"Button "+value,value,x,y,w,h,action,"button_peach_normal",size);
 public static RectTransform Character(Transform p,float x,float y,float scale,Color color,bool ears=true){var r=Root(p,"Garden friend",x,y,300,300);r.localScale=Vector3.one*scale;Art(r,"cat_idle",0,0,300,300);return r;}
'''+s[end:]
        # UIFloat is delivered separately, with an explicit restoration contract.
        s=s[:s.index('public class UIFloat')]+ '}\n'
        return s
    copycode('UI/UICraft.cs',craft)
    # Keep the original art catalog; it defines actual source cropping and border math.
    copycode('World/PastelArt.cs')
    # DOTween DLL is the same real project version, never replace it with a hand-written approximation.
    for p in (src/'Plugins/Demigiant/DOTween').glob('*'):
        if p.is_file() and p.suffix in ['.dll','.XML','.meta']:
            q=dest/'Plugins/DOTween'/p.name;q.parent.mkdir(parents=True,exist_ok=True);shutil.copy2(p,q)
            mapping.append({'source':p.relative_to(src.parent).as_posix(),'target':q.relative_to(KIT).as_posix(),'sha256':sha(q),'reason':'Exact project tween runtime dependency'})
    # The project modified ModuleUtils by inserting two defines. Redistribute the
    # supplier's unmodified files instead; the project DLL matches this 1.3.045 package byte-for-byte.
    for rel in ['readme.txt','Modules/DOTweenModuleUtils.cs','Modules/DOTweenModulePhysics.cs','Modules/DOTweenModulePhysics2D.cs']:
        p=a.dotween_standard/rel;q=dest/'Plugins/DOTween'/rel
        if not p.exists():raise FileNotFoundError('Unmodified DOTween 1.3.045 file required: '+str(p))
        q.parent.mkdir(parents=True,exist_ok=True)
        if p.resolve()!=q.resolve():shutil.copy2(p,q)
        mapping.append({'source':'third-party/DOTween-1.3.045/'+rel,'target':q.relative_to(KIT).as_posix(),'sha256':sha(q),'reason':'Unmodified supplier file; preserves required readme and attribution','license':'DOTween Artistic License','licenseUrl':'https://dotween.demigiant.com/license.php'})
    # Resource closure for delivered examples. Every directory is reached by the listed source classes.
    roots={
        'Art/SettingsC':'CSettingsArt.Get / SettingsSwitch77.Create / AppUI.OpenSettingsSheetC',
        'Art/ChineseV60/Logo':'PastelLogo.Create / JellyLogoIntro.Begin',
        'Art/Pastel':'PastelArt.Get / LoadingPresentation.Render',
        'Art/FailureCatV26':'FailureCatAnimation.Get / FailurePresentation.Render',
        'Art/VictoryCatV23':'VictoryCatAnimation.Get / VictoryPresentation.Render',
        'Art/FailureV25':'FailurePresentation.Render',
        'Art/VictoryV23':'VictoryPresentation.Render',
        'Art/LeagueCalendarV1':'LeagueMedalPresentation.Build',
        'Art/GoalCelebrationV1':'CollectionGoalEntrance.Begin glow',
    }
    # Resolve actual pastel root from its resource catalog rather than inventing a location.
    pastel=(src/'Scripts/World/PastelArt.cs').read_text(encoding='utf-8-sig')
    print('Pastel resources:',re.findall(r'Resources.Load[^\n]+',pastel)[-4:])
    roots.pop('Art/Pastel',None)
    for x in re.findall(r'Resources.Load<[^>]+>\("([^"]+)"',pastel):
        base=x.rsplit('/',1)[0];roots[base]='PastelArt.Get catalog and textures'
    selected=[]
    for rel,reason in roots.items():
        folder=src/'Resources'/rel
        if not folder.exists():raise FileNotFoundError(folder)
        if rel=='Art/Pastel':
            catalog=folder/'PastelArtData.json';selected.extend([(catalog,reason),(catalog.with_name(catalog.name+'.meta'),reason)])
            used={'cat_idle','cat_anticipate','cat_celebrate','cat_settle','cat_wave','icon_coin','resource_capsule','icon_medal','brick_mint','modal_panel','icon_video','icon_restart','icon_home'}
            info=json.loads(catalog.read_text(encoding='utf-8-sig'))
            resourcePaths={i['resource'] for i in info['sprites'] if i['name'] in used}
            for resource in resourcePaths:
                for p in (src/'Resources').glob(resource+'.*'):selected.append((p,'Delivered examples reference PastelArt key '+Path(resource).name))
        else:
            for p in folder.rglob('*'):
                if p.is_file():selected.append((p,reason))
    for rel,reason in [('Art/BannerJelly/panel','ActivityCarousel.Build panel'),('Art/BannerJelly/medal','ActivityCarousel.Build medal'),('Art/FourthGroup/states','OpeningCopperView.Render'),('Art/FourthGroup/layout','OpeningCopperArt.Clip')]:
        for p in (src/'Resources').glob(rel+'.*'):selected.append((p,reason))
    for name in ['vine-star-on','vine-star-off']:
        for p in (src/'Resources/Art/BattleJellyV5').glob(name+'.*'):selected.append((p,'StarChargeView.Initialize'))
    for name in ['MaokenZhuyuanTi','MiaoRoundedUI-Bold']:
        for p in (src/'Resources/Fonts').glob(name+'*'):selected.append((p,'GameTypography.Font/HeadingFont'))
    for p in (src/'Resources').glob('StarCharge*'):selected.append((p,'StarChargeView material shader'))
    # Resolve the delivered sound nodes through the actual project mixer override order.
    cues=['click','whoosh','ui_back','jelly_land','coin','score_star_full','rank_up','medal_arrive','result_win_stinger','result_lose_stinger','cat_jump_soft','cat_happy','cat_land_soft','result_win_burst','result_reward_reveal','cat_retry','cat_droop_soft','vo_win_zh','vo_retry_zh']
    paths={cue:'Audio/Jelly/'+cue for cue in cues}
    for manifest in ['Audio/Jelly/AI/manifest.json','Audio/DeliveryV68/manifest.json','Audio/DeliveryV87/manifest.json']:
        p=src/'Resources'/manifest
        if p.exists():
            for item in json.loads(p.read_text(encoding='utf-8-sig'))['clips']:
                if item.get('path'):paths[item['cue']]=item['path']
                elif manifest.startswith('Audio/Jelly/AI'):paths[item['cue']]='Audio/Jelly/AI/'+('bgm/' if item['cue'].startswith('bgm_') else 'sfx/')+item['cue']
    soundmap={}
    for cue in cues:
        candidates=[cue] if cue in paths and list((src/'Resources').glob(paths[cue]+'.*')) else [cue+'_a']
        for key in candidates:
            path=paths.get(key,paths.get(cue));matches=list((src/'Resources').glob(path+'.*')) if path else []
            clips=[p for p in matches if p.suffix.lower() in ['.wav','.ogg','.mp3']]
            if clips:
                soundmap[cue]=path
                for p in matches:selected.append((p,'SoundService cue '+cue))
    emit('Resources/ProjectTweenKit/sound-map.json',json.dumps({'clips':[{'cue':k,'path':v} for k,v in soundmap.items()]},ensure_ascii=False,indent=2))
    for p in (src/'StreamingAssets').rglob('*'):
        if p.is_file():selected.append((p,'Original project font/audio attribution evidence'))
    # Remove only stale materialized Pastel files from previous runs; never source files.
    keep={p.relative_to(src).as_posix() for p,_ in selected}
    expected=(dest/'Resources/Art/Pastel').resolve()
    if expected.exists():
        for p in expected.rglob('*'):
            if p.is_file() and p.relative_to(dest).as_posix() not in keep and not (p.suffix=='.meta' and p.with_suffix('').is_dir()):
                if not p.resolve().is_relative_to(expected):raise RuntimeError('Unsafe generated resource path')
                p.unlink()
    for p,reason in selected:
        q=dest/p.relative_to(src);q.parent.mkdir(parents=True,exist_ok=True);shutil.copy2(p,q)
        if p.suffix!='.meta':
            meta=p.with_name(p.name+'.meta');g=re.search(r'^guid: (\w+)',meta.read_text(encoding='utf-8-sig'),re.M) if meta.exists() else None
            mapping.append({'source':p.relative_to(src.parent).as_posix(),'target':q.relative_to(KIT).as_posix(),'sha256':sha(q),'sourceGuid':g[1] if g else None,'reason':reason,'usageVerification':'source-path closure; individual atlas cells retain original catalog'})
    write(KIT/'references/extraction-diffs.json',changes);write(KIT/'references/resource-map.json',mapping)
    print('Extracted',len(changes),'source files;',len(mapping),'mapped dependencies')

if __name__=='__main__':main()
