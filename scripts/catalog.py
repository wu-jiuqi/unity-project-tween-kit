"""Render the catalogue from scanned evidence plus explicit route reviews.

An inventory candidate is never silently promoted into an active/delivered effect.
"""
import json,re,hashlib
from pathlib import Path
from inventory import KIT,write

# Reviewed chains in the current 0.4.0 working tree. Runtime gallery validation
# checks the extracted component, not proof of every original gameplay branch.
REVIEWS={
 'BattleHUDSummary':('文字与数值','独立组件','AppUI.BuildBattleHUD → BattleHUDSummary.Initialize；VictoryFinale.Advance / VictoryRoundPreparation.Update → ShowVictoryScore(impact=true)','结算星光到达时分数变金色并放大回落','ScoreImpact','scoreimpact'),
 'ScoreGainPresentation':('文字与数值','独立组件','AppUI.BuildScoreHUD（非Opening/Tutorial的ChainCollection分支） → ScoreGainPresentation.Initialize → StateChanged','短窗口合并加分，提示放大、上移与淡出','ScoreGain','scoregain'),
 'ChainTaskCard':('奖励与目标','独立组件','ChainCollectionHUD.Initialize → AddCard → ChainTaskCard.Initialize / Refresh','实际收集值改变触发目标到达挤压；完成显示印章','TaskArrival','taskarrival'),
 'ChainCollectionHUD':('文字与数值','独立组件','AppUI.BuildBattleHUD → ChainCollectionHUD.Initialize → StateChanged → Refresh → Update','剩余步数改变时挤压；此交付只提取步数视觉','MovesFeedback','moves'),
 'ChainRatingView':('奖励与目标','组合预制体','ChainCollectionHUD.Initialize → ChainRatingView.Initialize；Update观察ChainRatingSerial','连击评级变动后字图放大与星光爆发','CheerBurst','rating'),
 'LeagueJelly':('控件交互','独立组件','LeagueRankingView.Refresh / LeaguePodiumView.Build → LeagueJelly.Play','首屏错峰或名次/积分更新时卡片果冻抖动','LeagueJelly','leaguejelly'),
 'ChapterHomeView':('页面与布局','宿主适配配方','AppUI.BuildHome → BuildChapterHome → ChapterHomeView.Build → Transition(previousTheme)','主题改变时旧背景SmoothStep淡出；只提取Transition透明度段','ThemeDissolve','themedissolve'),
 'UIBounce':('控件交互','独立组件','Bootstrap.Boot → AppUI.Awake → ShowPage → CSettingsArt.Button / UICraft.Button','悬停、按下、拖拽、松开；命中根保持不动','UIBounce','press'),
 'SettingsSwitch77':('控件交互','组合预制体','AppUI.Settings → OpenSettingsSheetC → SettingsSwitch77.Create；AudioSettings 复用','声音与震动开关；点击读写宿主一次','SettingsSwitch77','switch'),
 'JellyLogoIntro':('文字与数值','组合预制体','AppUI.Awake → ShowIntro → StartIntroWhenVisible → JellyLogoIntro.Begin','启动画面可见后自动演出','JellyLogoIntro','logo'),
 'JellyGlyph':('文字与数值','组合预制体','JellyLogoIntro.Begin → 每个独立字层 JellyGlyph.Begin → Impact','错峰下落、手工帧挤压、齐落后衰减','JellyGlyph','logo'),
 'HomeWordmarkWiggle':('持续表现','独立组件','AppUI.BuildHome → BuildChapterHome → ChapterHomeView.Build → HomeWordmarkWiggle','主页字标定期震颤；HomeMotionAllowed 门控','HomeWordmarkWiggle','wordmark'),
 'LoadingPresentation':('文字与数值','组合预制体','AppUI.BeginLevel → LoadingPresentation.Build / Play → RunLevelLoad','进入关卡的加载页自动循环；内容准备另由宿主管理','LoadingPresentation','loading'),
 'UIFloat':('持续表现','独立组件','OpenSettingsSheetC 猫头 amplitude=3/speed=1.4；About 插画 amplitude=4/speed=1.3','设置、帮助、关于等插画轻浮动','UIFloat','float'),
 'EncouragementDrag':('控件交互','独立组件','ChapterHomeView.Build → HomeFeedbackView.Create → EncouragementDrag.Initialize','拖动主页欢迎语；释放后四子步弹簧归位','EncouragementDrag','drag'),
 'ActivityJellyMotion':('控件交互','宿主适配配方','ChapterHomeView.Build → ActivityCarousel.Create / Build → ActivityJellyMotion.Initialize','活动卡悬停、按下、松开与初次入场','ActivityJellyMotion','activity'),
 'CollectionFlightMotion':('奖励与目标','独立组件','GameSession.OpeningCollect → chainLoot → TickChainCollection → CollectionFlightMotion.Apply；ChainCollectionHUD 计算真实目标','主线收集物飞向目标卡；收集计数留在 GameSession','RewardFlight','reward'),
 'CollectionGoalEntrance':('奖励与目标','宿主适配配方','AppUI.StartPreparedLevel → ShowCollectionGoalEntrance → CollectionGoalEntrance.Begin(hud.GoalsRect)','本关目标先居中展示，再返回真实 HUD 容器','CollectionGoalEntrance','goal'),
 'StarChargeView':('奖励与目标','独立组件','BattleScoreMeter.Initialize → StarChargeView.Initialize；Place → Charge；Advance 驱动','分数星光到达、材质边缘填充和满星脉冲','StarChargeView','charge'),
 'VictoryCatAnimation':('配套编排','独立组件','AppUI.ShowResults → VictoryPresentation.Render → VictoryCatAnimation.FrameAt/Get','胜利猫16帧，逐帧时间原样保留','FrameSequence','victorycat'),
 'FailureCatAnimation':('配套编排','独立组件','AppUI.ShowResults → FailurePresentation.Render → FailureCatAnimation.FrameAt/Get','失败猫16帧，逐帧时间原样保留','FrameSequence','failurecat'),
 'VictoryPresentation':('奖励与目标','组合预制体','AppUI.ShowResults(win) → VictoryPresentation.Begin → Render / RenderHeaderStars','结算页面猫、光环、奖励卡、按钮与星级；结果已由宿主计算','VictoryPresentation','victory'),
 'FailurePresentation':('奖励与目标','组合预制体','AppUI.ShowResults(!win) → FailurePresentation.Begin → Render','失败页的背景帧、猫、逐字标题、动作区入场','FailurePresentation','failure'),
 'LeagueMedalPresentation':('奖励与目标','宿主适配配方','AppUI.TickLeagueFlow → LeagueMedalPresentation.Show(results, before, after, medals)','胜利结算奖励区的奖章和名次变化','LeagueMedalPresentation','medal'),
 'JellyMotion':('场景表现','独立组件','OpeningCopperView.ImpactStretch / OpeningThirdArt.ImpactStretch → JellyMotion.HitStretch；VictoryPresentation.Stars 复用 Float*','铜钱／招财猫方向受击；结算星星与炸弹待机浮动','JellyHit','hit'),
 'ResultAudioTimeline':('配套编排','宿主适配配方','VictoryPresentation.Begin / FailurePresentation.Begin → ResultAudioTimeline.Initialize','按原节点发声；Skip / OnDisable 取消瞬态音频','ResultAudioTimeline','victory'),
}
NOT_ANIMATIONS={
 'BalanceStatistics':'业务统计','PlayerHaptics71':'触觉节点（非视觉动画）','PlaytestMetrics76':'试玩统计',
 'MainlinePlanner':'业务规划计算','GameContent':'资源加载业务','ContentLoadOperation':'资源加载业务','StartupAttempt77':'启动／重试业务',
 'GameTypography':'字体与材质静态设置','UICraft':'静态造型绘制（UIFloat 单列）','WorldArt':'静态纹理绘制（WorldFX 单列）',
 'PixelPieceArt':'静态多边形造型','BoardDisplay':'坐标投影','GardenSceneArt':'纹理上传','BoardSurface':'棋盘静态网格',
}
LEGACY=['Pop','CountWallet','FlyCoins','FlyCoin','WalletPulse','Fireworks','Spark','Confetti']

def main():
    data=json.loads((KIT/'references/source-map.json').read_text(encoding='utf-8'));entries=[]
    for f in data['families']:
        cls=f['class'].split('.')[-1];review=REVIEWS.get(cls)
        if f['classification']=='runtime-candidate' and cls in NOT_ANIMATIONS:
            f['usageStatus']='non-animation';f['scopeReason']=NOT_ANIMATIONS[cls]
        elif f['classification']=='runtime-candidate' and review:
            cat,kind,route,trigger,component,demo=review
            f.update(usageStatus='source-route-reviewed',category=cat,reuseForm=kind,route=route,trigger=trigger,deliveredComponent=component,galleryEntry=demo,deliveryStatus='extracted-component')
        elif cls=='ChapterTransitionPreview':f.update(usageStatus='inactive-preview',scopeReason='显式预览类，不能当作正常场景入口')
        elif cls=='Round6Nav':f.update(usageStatus='legacy-route-disabled',scopeReason='当前 AppUI.BuildNav 只禁用 navSlot；保留旧实现供追溯')
        elif cls in ['BallBody','BrickBody','MovingObstacle','BossBody']:
            f['usageStatus']='mixed-physics-presentation-review';f['scopeReason']='须把视觉分量与 Rigidbody/碰撞/业务规则逐方法拆分'
        # AppUI is a large partial host. Only the reviewed subsets are marked.
        if f['source']=='Assets/Scripts/UI/AppUI.cs':
            for m in f['methods']:
                if m['name'] in LEGACY:m['usageReview']='legacy-no-current-entry-observed'
                elif m['name'] in ['ShowPage','OpenModalMotion','CloseModal','StartIntroWhenVisible','DismissIntro']:m['usageReview']='source-route-reviewed'
        if f['source']=='Assets/Scripts/UI/VictoryFinale.cs':
            f['subsetDelivery']={'component':'NumberRoll','methods':['Advance','Ease'],'scope':'单个分数到达后的0.2秒滚分片段；完整投弹／爆炸／业务编排尚未提取','galleryEntry':'number'}
        if f['source']=='Assets/Scripts/UI/AppUI.SettingsC.cs' and cls=='AppUI':
            f['subsetDelivery']={'component':'ModalMotion','methods':['LayoutSettingsSheetC'],'scope':'拟合后的入场scale／alpha，加 AppUI.CloseModal 关闭；安全区布局由宿主完成','galleryEntry':'modal'}
        # Recipe pages retain concrete evidence even when extraction has not been completed.
        path=KIT/'references/recipes'/str(f['id']+'.md');path.parent.mkdir(parents=True,exist_ok=True)
        lines=[f"# {f['class']} · {Path(f['source']).stem}",'',f"状态：`{f['usageStatus']}`；交付：`{f['deliveryStatus']}`。",'',f"源文件：`{f['source']}`",f"文件 SHA-256：`{f['fileSha256']}`",f"[原始源码快照](../source/{f['source']}.txt)",'']
        if review and f['usageStatus']=='source-route-reviewed':
            lines += [f'用在哪里：{review[2]}',f'触发操作：{review[3]}',f'导入组件：`ProjectTweenKit.{review[4]}`；展馆 ID：`{review[5]}`。',f'复用形式：{review[1]}。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。','']
        else:lines+=['此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。','']
        lines+=['## 源方法、参数与曲线','']
        for m in f['methods']:
            lines += [f"### {m['name']} · L{m['line']}",'',f"方法 SHA-256：`{m['sha256']}`",f"时间来源：{', '.join(m.get('timeSources',[])) or '本方法未直接取时；查调用者/字段'}",f"曲线证据：{', '.join(m.get('curveEvidence',[])) or '查原式或下级调用'}",f"解析符号：`{m.get('symbol')}`",'']
            refs=m.get('resolvedCallSites',[])
            if refs:
                lines+=['Roslyn 已解析的实际源码调用位置：','']+[f"- `{c['path']}:{c['callLine']}` · `{c['class']}.{c['method']}` → `{c['expression']}{c['arguments']}`" for c in refs]
            elif m.get('engineCallback'):lines+=['Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。']
            else:lines+=['未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。']
            lines+=['','```csharp',m['source'].strip(),'```','']
        lines+=['## 对象层级和素材入口','']
        seen=set()
        for m in f['methods']:
            for c in m.get('hierarchyCalls',[]):
                key=str(c['line'])+c['expression']
                if key not in seen:lines.append(f"- L{c['line']}：`{c['expression']}{c['arguments']}`");seen.add(key)
        lines+=['','素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。','','## 生命周期证据','']
        if f['lifecycleEvidence']:
            for m in f['lifecycleEvidence']:lines+=['```csharp',m['source'].strip(),'```','']
        else:lines+=['本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。','']
        lines+=['## 源字段与依赖','']
        for m in f['parametersEvidence']:lines+=['```csharp',m['source'].strip(),'```']
        lines+=['','业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。','']
        path.write_text('\n'.join(lines),encoding='utf-8')
        entries.append({'id':f['id'],'class':f['class'],'source':f['source'],'status':f['usageStatus'],'delivery':f['deliveryStatus'],'component':f.get('deliveredComponent'),'recipe':path.relative_to(KIT).as_posix()})
    # Preserve reviewed statuses in the same source map, rather than a contradictory second list.
    write(KIT/'references/source-map.json',data);write(KIT/'references/catalog-index.json',entries)
    grouped={}
    for e in entries:grouped.setdefault(e['status'],[]).append(e)
    lines=['# 项目动画来源目录','','扫描主工程 `BounceGarden/BounceGarden`；以 `DEVELOPMENT_BASELINE.json` 的0.4.0工作版为场景背景。数量由每次真实扫描产生。','','这是来源清单及交付进度表。`source-route-reviewed` 表示核对了源码入口链，不等于已在正常游戏中实测每个调用点。其余候选保持待核对；最终全量验收尚未通过。','',f"本次有 {len(data['families'])} 个源文件／类分组，含测试、编辑器与非动画候选；{len(data['nonAnimationFiles'])} 个未命中动画模式的源码文件另列。",'','[机器清单](source-map.json) · [源码指纹](source-fingerprints.json) · [完整资源扫描](resource-inventory.json) · [交付素材映射](resource-map.json) · [提取差异](extraction-diffs.json) · [验收状态](acceptance.json)','']
    for status,items in sorted(grouped.items()):
        lines+=['## '+status+' · '+str(len(items)),'','| 条目 | 源文件 | 交付 |','|---|---|---|']
        for e in items:lines.append(f"| [{e['class']}](recipes/{e['id']}.md) | `{e['source']}` | {e['component'] or e['delivery']} |")
        lines+=['']
    (KIT/'references/catalog.md').write_text('\n'.join(lines),encoding='utf-8')
    write(KIT/'references/review-summary.json',{'counts':{s:len(v) for s,v in grouped.items()},'legacyHostMethods':LEGACY,'unresolvedInvocationCount':sum(c.get('resolution')!='semantic' for f in data['families'] for m in f['methods'] for c in m['calls']),'scope':'Syntax + partial semantic resolution + explicitly reviewed source routes; no blanket reachability claim.'})
    print(json.dumps({s:len(v) for s,v in grouped.items()},ensure_ascii=False))
if __name__=='__main__':main()
