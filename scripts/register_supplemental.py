"""Record exact project sources for the 0.4 adapters, including staged platform code."""
import argparse,difflib,json,shutil
from pathlib import Path
from inventory import KIT,write,sha

SLICES=[
 ('loadingprogress','LoadingProgress','../../Tools/Platform/WeChat/StartupLoadingView86.cs',['AdvanceProgress','RenderProgress','AnimateProgress'],'保留单调进度、99%完成门控、原追赶速度/帧上限/装饰曲线；分离宿主任务与网络'),
 ('startuploading','StartupLoadingPresentation','../../Tools/Platform/WeChat/StartupLoadingView86.cs',['Build','Update','Hop'],'原启动页图层与姿态；进度共用LoadingProgress。安全区由绑定宿主管理，示例按自身RectTransform适配'),
 ('pagenav','PageSlide','Assets/Scripts/UI/AppUI.cs',['ShowPage'],'仅提取0.27秒OutCubic页面轨道；等价归一化曲线替代DOTween宿主序列；导航、输入和路由归宿主'),
 ('stagger','GroupStagger','Assets/Scripts/UI/AppUI.Shop.cs',['ShopEnter'],'保持.98起始比例、.20秒OutCubic、.018逐项延迟；初始scale向量由绑定对象捕获'),
 ('rankmove','RankReorder','Assets/Scripts/UI/LeagueRankingView.cs',['Refresh'],'保持.48秒OutCubic行位移；主机传目标名次；原排行读取、头像工厂、声音与方向箭头不在此组件中'),
 ('guide','TutorialGesture','Assets/Scripts/UI/PlayableTutorialGuide.cs',['Update'],'提取手势/触点/目标框曲线；起终点由宿主输入，移除游戏瞄准、存档和教学完成业务'),
 ('hintfade','HintFade','Assets/Scripts/UI/BattleUIArt.cs',['BattleAimHint.Update'],'提取每秒2透明度变化；宿主传Visible，不监听游戏事件，所有提示组不挡射线'),
 ('homefeedback','HomeLayeredFeedback','Assets/Scripts/UI/HomeFeedbackView.cs',['Update','Pop'],'保留分层位移、比例、相位及衰减，绑定宿主视觉层；不读取连胜、登录或文案业务'),
 ('pendulum','PendantSwing','Assets/Scripts/UI/BattleScoreMeter.cs',['Advance','Impulse'],'提取纯视觉四子步弹簧；默认示例冲量120，保留30/3.6/36度/-.25；碰撞影响由宿主换算后输入'),
 ('breath','DecorativeBreath','Assets/Scripts/UI/LeagueMatchPresentation.cs',['Update'],'仅提取徽章1+.035sin(3t)；不包含匹配、排行、日期或确认保存'),
]

def main():
    ap=argparse.ArgumentParser();ap.add_argument('--source',type=Path,required=True);a=ap.parse_args();src=a.source.resolve()
    previous=json.loads((KIT/'references/supplemental-deliveries.json').read_text(encoding='utf-8'))
    records=[];diffs=json.loads((KIT/'references/extraction-diffs.json').read_text(encoding='utf-8'))
    for ident,cls,rel,methods,adaptation in SLICES:
        original=src/rel;snapshot=KIT/'references/source'/('Platform/StartupLoadingView86.cs.txt' if rel.startswith('../../') else rel+'.txt')
        if not original.exists():
            record=next(r.copy() for r in previous['entries'] if r['galleryEntry']==ident)
            record['sourceStatus']='frozen-0.4.0-reference';record['usageStatus']='historical-reference';records.append(record);continue
        snapshot.parent.mkdir(parents=True,exist_ok=True);shutil.copy2(original,snapshot)
        target=KIT/'assets/unity-kit/Runtime'/(cls+'.cs');diffs=[d for d in diffs if d['target']!=target.relative_to(KIT).as_posix()]
        owner={'pagenav':'AppUI','stagger':'AppUI','rankmove':'LeagueRankingView','guide':'PlayableTutorialGuide','hintfade':'BattleAimHint','homefeedback':'HomeFeedbackView','pendulum':'BattleScoreMeter','breath':'LeagueMatchPresentation'}.get(ident,'StartupLoadingView86')
        record={'galleryEntry':ident,'component':cls,'owner':owner,'source':rel,'sourceSha256':sha(original),'snapshot':snapshot.relative_to(KIT).as_posix(),'target':target.relative_to(KIT).as_posix(),'targetSha256':sha(target),'methods':methods,'adaptation':adaptation,'usageStatus':'platform-startup-source-route-reviewed' if rel.startswith('../../') else 'source-presentation-slice-reviewed'}
        records.append(record);diffs.append({**record,'diff':list(difflib.unified_diff(original.read_text(encoding='utf-8-sig').splitlines(),target.read_text(encoding='utf-8-sig').splitlines(),fromfile=rel,tofile=target.name,lineterm=''))})
    # Exact helpers are also auditable; they carry no host business code.
    for cls in ['OnboardingArt','TutorialArt']:
        rel='Assets/Scripts/UI/'+cls+'.cs';original=src/rel;target=KIT/'assets/unity-kit/Runtime'/(cls+'.cs')
        if not original.exists():continue
        diffs=[d for d in diffs if d['target']!=target.relative_to(KIT).as_posix()]
        diffs.append({'source':rel,'sourceSha256':sha(original),'target':target.relative_to(KIT).as_posix(),'targetSha256':sha(target),'diff':list(difflib.unified_diff(original.read_text(encoding='utf-8-sig').splitlines(),target.read_text(encoding='utf-8-sig').splitlines(),fromfile=rel,tofile=target.name,lineterm=''))})
    host=src/'../../Tools/Platform/WeChat/HomeContentStartup.cs';hostSnapshot=KIT/'references/source/Platform/HomeContentStartup.cs.txt';shutil.copy2(host,hostSnapshot)
    write(KIT/'references/supplemental-deliveries.json',{'entries':records,'platformHostEvidence':{'source':'../../Tools/Platform/WeChat/HomeContentStartup.cs','snapshot':hostSnapshot.relative_to(KIT).as_posix(),'sha256':sha(host),'route':'HomeContentStartup.Build → StartupLoadingView86.Build；Load的真实阶段SetProgress → 首页首帧准备完成后Complete；Failure/Resume保持进度'}})
    write(KIT/'references/extraction-diffs.json',diffs)
    data=json.loads((KIT/'references/source-map.json').read_text(encoding='utf-8'))
    for family in data['families']:
        family.pop('additionalDeliveries',None)
        matches=[r for r in records if r['source']==family['source'] and r['owner']==family['class'].split('.')[-1]]
        if matches:family['additionalDeliveries']=[{k:r[k] for k in ['galleryEntry','component','methods','adaptation']} for r in matches]
    write(KIT/'references/source-map.json',data)
    print('Registered',len(records),'project presentation slices, including platform startup')

if __name__=='__main__':main()
