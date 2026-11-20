# 补间覆盖复查 · 2026-11-20

核对主工程480个C#文件、258个候选分组；另扫描Tools/Platform/WeChat中的6个C#文件。主工程序列化资源：{'.prefab': 11, '.unity': 1}。

微信启动Loading原先位于主Assets扫描范围之外，本轮已明确补入。HomeContentStartup真实阶段输入进度，首页首帧就绪后才发送Complete；运行示例只模拟这些事件，不加载网络资源。

## 0.4.0新增10项

| 设计条目 | 对应组件 | 提取边界 |
|---|---|---|
| `loadingprogress` | `LoadingProgress` | 保留单调进度、99%完成门控、原追赶速度/帧上限/装饰曲线；分离宿主任务与网络 |
| `startuploading` | `StartupLoadingPresentation` | 原启动页图层与姿态；进度共用LoadingProgress。安全区由绑定宿主管理，示例按自身RectTransform适配 |
| `pagenav` | `PageSlide` | 仅提取0.27秒OutCubic页面轨道；等价归一化曲线替代DOTween宿主序列；导航、输入和路由归宿主 |
| `stagger` | `GroupStagger` | 保持.98起始比例、.20秒OutCubic、.018逐项延迟；初始scale向量由绑定对象捕获 |
| `rankmove` | `RankReorder` | 保持.48秒OutCubic行位移；主机传目标名次；原排行读取、头像工厂、声音与方向箭头不在此组件中 |
| `guide` | `TutorialGesture` | 提取手势/触点/目标框曲线；起终点由宿主输入，移除游戏瞄准、存档和教学完成业务 |
| `hintfade` | `HintFade` | 提取每秒2透明度变化；宿主传Visible，不监听游戏事件，所有提示组不挡射线 |
| `homefeedback` | `HomeLayeredFeedback` | 保留分层位移、比例、相位及衰减，绑定宿主视觉层；不读取连胜、登录或文案业务 |
| `pendulum` | `PendantSwing` | 提取纯视觉四子步弹簧；默认示例冲量120，保留30/3.6/36度/-.25；碰撞影响由宿主换算后输入 |
| `breath` | `DecorativeBreath` | 仅提取徽章1+.035sin(3t)；不包含匹配、排行、日期或确认保存 |

前一轮已补入分数变色放大、加分合并上浮、任务印章、步数挤压、评级星光、排行卡抖动与背景换景；累计36项可体验条目。参数显示与导出不包含AI Prompt。

参考仓库96项逐条取舍见[对照表](reference96-comparison.md)。未把不相关的翻牌、背包、表单和经验系统加入工具包。

## 待继续确认的源分组

仍有94个源分组需要整体核对，部分已有明确交付片段。它们包含大型宿主、业务/物理混合和可能不可达分支，不等于还有这么多个已确认在用动画遗漏。

| 源分组 | 匹配方法 | 已交付的补充片段 |
|---|---|---|
| `Assets/Scripts/Core/BoardFlowPlanner.cs` / GameSession | FlowRelativeCollision, FlowPositionAt | — |
| `Assets/Scripts/Core/BossLevel.cs` / GameSession | TickBoss | — |
| `Assets/Scripts/Core/ChainCollectionSession.cs` / GameSession | TickChainCollection | — |
| `Assets/Scripts/Core/CollectionSession.cs` / GameSession | FallCollectionTreats, TickCollection | — |
| `Assets/Scripts/Core/FlowJellySession.cs` / FlowJellyDeform | LateUpdate | — |
| `Assets/Scripts/Core/FusionPresentationSession.cs` / GameSession | TickFusionPresentation | — |
| `Assets/Scripts/Core/GameSession.cs` / GameSession | StartSession, PrepareSession, BuildSession, NormalizeAimDirection | — |
| `Assets/Scripts/Core/OpeningCampaign.cs` / GameSession | InitializeOpeningGridSteps | — |
| `Assets/Scripts/Core/OpeningTargets.cs` / GameSession | OpeningCollect | — |
| `Assets/Scripts/Core/OpeningThirdSession.cs` / GameSession | TickOpeningThird | — |
| `Assets/Scripts/Core/PixelDropSession.cs` / PixelDropBody | Tick, LateUpdate | — |
| `Assets/Scripts/Core/VictoryPlayback.cs` / GameSession | PresentationDelta | — |
| `Assets/Scripts/SoundService.cs` / SoundService | WarmEffects, Trigger, Speak, Crossfade, Update, SetBusVolume, SaveMix | — |
| `Assets/Scripts/UI/ActivityCarousel.cs` / ActivityCarousel | Settle | — |
| `Assets/Scripts/UI/AlphabetIntroductionView.Boosters.cs` / BoosterStoryActor | Update | — |
| `Assets/Scripts/UI/AlphabetIntroductionView.cs` / AlphabetIntroductionView | Begin, Enter, Advance, AdvanceForQA, Cancel, OnDestroy | — |
| `Assets/Scripts/UI/AlphabetIntroductionView.Motion.cs` / AlphabetIntroductionView | AdvanceMotion | — |
| `Assets/Scripts/UI/AlphabetTweenExtensions.cs` / AlphabetTweenExtensions | DOAnchorPos, DOAnchorPosY, DOFade, DOFade | — |
| `Assets/Scripts/UI/AppUI.Alphabet.cs` / AppUI | ShowAlphabetIntroductions | — |
| `Assets/Scripts/UI/AppUI.BoosterUnlocks.cs` / AppUI | ShowBoosterUnlocks | — |
| `Assets/Scripts/UI/AppUI.ChapterOpening70.cs` / AppUI | OpenChapterOnHome, ReturnHomeAfterChapterResult | — |
| `Assets/Scripts/UI/AppUI.ContentLoading.cs` / AppUI | PrefetchFollowingLevel, PrepareOutcomeRoutine, RunLevelLoad, PrepareMenuContentRoutine | — |
| `Assets/Scripts/UI/AppUI.cs` / AppUI | Update, ShowPage, StartIntroWhenVisible, DismissIntro, StartPreparedLevel, HideTutorialLater, OpenModalMotion, CloseModal, Pop, CountWallet, FlyCoins, FlyCoin, WalletPulse, Fireworks, Spark, Confetti, Toast, QAWaitForLevel | PageSlide |
| `Assets/Scripts/UI/AppUI.LeagueActivity.cs` / AppUI | TickLeagueActivity | — |
| `Assets/Scripts/UI/AppUI.LeagueFlow.cs` / AppUI | TickLeagueFlow | — |
| `Assets/Scripts/UI/AppUI.PlayerSettings71.cs` / AppUI | FitSettingsModal77 | — |
| `Assets/Scripts/UI/AppUI.SettingsC.cs` / AppUI | LayoutSettingsSheetC | — |
| `Assets/Scripts/UI/AppUI.Shop.cs` / AppUI | ShopEnter, OpenReferenceModal, DrawDressing, CosmeticSpark | GroupStagger |
| `Assets/Scripts/UI/AppUI.StreakReward.cs` / AppUI | ShowCampaignStreakReward | — |
| `Assets/Scripts/UI/AppUI.StreakReward.cs` / StreakRewardGlow | OnPopulateMesh | — |
| `Assets/Scripts/UI/AppUI.StreakReward.cs` / StreakRewardPresentation | Play | — |
| `Assets/Scripts/UI/AppUI.Tutorial.cs` / AppUI | ShowFirstBounceIntroduction | — |
| `Assets/Scripts/UI/BattleInterfaceTour.cs` / AppUI | ShowBattleInterfaceTour | — |
| `Assets/Scripts/UI/BattleInterfaceTour.cs` / BattleInterfaceTour | Begin, Place, Advance, Update | — |
| `Assets/Scripts/UI/BattleScoreMeter.cs` / BattleScoreMeter | OnBall, LateUpdate | PendantSwing |
| `Assets/Scripts/UI/BattleUIArt.cs` / BattleAimHint | Update | HintFade |
| `Assets/Scripts/UI/BossHUD.cs` / BossHUD | Update | — |
| `Assets/Scripts/UI/ChapterOpening70.cs` / ChapterOpening70 | Sample, Update | — |
| `Assets/Scripts/UI/CollectionGoalEntrance.cs` / AppUI | ShowCollectionGoalEntrance | — |
| `Assets/Scripts/UI/GoalCelebration.cs` / GoalCelebration | Update, Render | — |
| `Assets/Scripts/UI/HomeFeedbackView.cs` / HomeFeedbackView | Update, Pop | HomeLayeredFeedback |
| `Assets/Scripts/UI/InGameFeedbackView.cs` / InGameFeedbackView | LateUpdate, AnimateCheer | — |
| `Assets/Scripts/UI/JellyChapterOrb.cs` / JellyChapterOrb | Update | — |
| `Assets/Scripts/UI/JellyJourney80.cs` / JellyJourney80 | MotionClock, Sway, PullJelly, EndJellyDrag, LateUpdate | — |
| `Assets/Scripts/UI/JellyJourneyMotion80.cs` / JellyLevelMotion80 | Clock, Entrance, OnPointerDown, Release, Apply | — |
| `Assets/Scripts/UI/JellyJourneyMotion80.cs` / JellyRouteRibbon80 | Configure, RefreshMotion, OnPopulateMesh | — |
| `Assets/Scripts/UI/LeagueBannerStatus.cs` / LeagueBannerStatus | Update | — |
| `Assets/Scripts/UI/LeagueMatchPresentation.cs` / LeagueMatchPresentation | Build, Update | DecorativeBreath |
| `Assets/Scripts/UI/LeagueRankingView.cs` / LeagueRankingView | Update, UpdateClock, Refresh | RankReorder |
| `Assets/Scripts/UI/LessonCollectionFlight.cs` / LessonCollectionFlight | Update | — |
| `Assets/Scripts/UI/OpeningIntroduction.cs` / AppUI | ShowOpeningIntroduction | — |
| `Assets/Scripts/UI/OpeningIntroduction.cs` / OpeningRuleDemo | Render, Update | — |
| `Assets/Scripts/UI/PaintingCollectionPresentation.cs` / PaintingCollectionPresentation | Begin, Update, Render | — |
| `Assets/Scripts/UI/PlayableTutorialGuide.cs` / PlayableTutorialGuide | Update | TutorialGesture |
| `Assets/Scripts/UI/TutorialMotionBoard.cs` / TutorialMotionBoard | Configure, Render | — |
| `Assets/Scripts/UI/UIBounce.cs` / JellyButtonMesh | Refresh | — |
| `Assets/Scripts/UI/VictoryFinale.cs` / VictoryFinale | Begin, Update, Advance | — |
| `Assets/Scripts/UI/VictoryRoundPreparation.cs` / VictoryRoundPreparation | Update | — |
| `Assets/Scripts/World/AbilityArt.cs` / AbilityFrameFx | Update | — |
| `Assets/Scripts/World/CameraJuice.cs` / CameraJuice | Impact, LateUpdate | — |
| `Assets/Scripts/World/ChainCubeArt.cs` / ChainBondView | Update | — |
| `Assets/Scripts/World/ChainCubeArt.cs` / ChainCubeBurst | Update | — |
| `Assets/Scripts/World/ChainCubeArt.cs` / ChainCubeIdle | Update | — |
| `Assets/Scripts/World/ChainCubeArt.cs` / ChainCubeView | Update | — |
| `Assets/Scripts/World/ChainVisualArt.cs` / ChainBrickView | Tick, Update | — |
| `Assets/Scripts/World/ChainVisualArt.cs` / ChainGlint | Update | — |
| `Assets/Scripts/World/ChainVisualArt.cs` / ChainIdleSprite | Tick, Update | — |
| `Assets/Scripts/World/ChainVisualArt.cs` / ChainSpriteFx | Update | — |
| `Assets/Scripts/World/ChainVisualArt.cs` / ChainStoneView | Update | — |
| `Assets/Scripts/World/CombatFeedback79.cs` / CombatFeedback79 | EmphasizeSpecial, Initialize, Cue, RequestStop, Update, AdvanceParticles, LateUpdate, ReleaseClock, ReleaseSlow | — |
| `Assets/Scripts/World/ElectricCircuit.cs` / ElectricCircuit | Update, UpdateLines | — |
| `Assets/Scripts/World/FusionBallBudgetView.cs` / FusionBallBudgetView | Refresh | — |
| `Assets/Scripts/World/GameplayFlight.cs` / FlightTrail | Configure, BindYarn, LateUpdate | — |
| `Assets/Scripts/World/IngotSpecialFx.cs` / IngotSpecialFx | Play, Update | — |
| `Assets/Scripts/World/JellyBrickArt.cs` / JellyPopPool | Update | — |
| `Assets/Scripts/World/JellyWallFrame.cs` / JellyWallFrame | Envelope, Influence, PulseShape, LateUpdate | — |
| `Assets/Scripts/World/JuiceGardenView.cs` / JuiceGardenView | Impact, Tether, Callout, PawFlick, Update | — |
| `Assets/Scripts/World/MainlineArt.cs` / MainlineBrickView | Update | — |
| `Assets/Scripts/World/MainlineArt.cs` / MainlineCanvas | Update, Render | — |
| `Assets/Scripts/World/MainlineArt.cs` / MainlineFrameFx | Update | — |
| `Assets/Scripts/World/OpeningCookieBoxView.cs` / OpeningCookieBoxView | Tick | — |
| `Assets/Scripts/World/OpeningCopperArt.cs` / OpeningCopperEffect | Render | — |
| `Assets/Scripts/World/OpeningCopperArt.cs` / OpeningCopperView | Render | — |
| `Assets/Scripts/World/OpeningThirdArt.cs` / OpeningThirdDirector | Update | — |
| `Assets/Scripts/World/OpeningYarnArt.cs` / OpeningYarnEffect | Render | — |
| `Assets/Scripts/World/OpeningYarnArt.cs` / OpeningYarnView | Tick | — |
| `Assets/Scripts/World/PaintingBoard.cs` / PaintingBoard | Update, Render | — |
| `Assets/Scripts/World/RearCatLauncher.cs` / RearCatLauncher | Update | — |
| `Assets/Scripts/World/SevenSpecialArt.cs` / SevenSpecialFx | Update | — |
| `Assets/Scripts/World/SevenSpecialArt.cs` / SevenSpecialIdle | Update | — |
| `Assets/Scripts/World/SevenSpecialCinematics.cs` / SevenSuctionFx | Update, Advance | — |
| `Assets/Scripts/World/SevenSpecialCinematics.cs` / SevenWindup | Update, Advance | — |
| `Assets/Scripts/World/StreakBallArt.cs` / StreakImpactPool | Update | — |
| `Assets/Scripts/World/WorldArt.cs` / WorldFx | Emit, Update | — |

文件指纹与平台来源见[机器记录](coverage-audit.json)及[supplemental-deliveries.json](supplemental-deliveries.json)。
