# 补间遗漏复查 · 2026-11-20

重跑Roslyn扫描及宽模式检索，核对480个C#文件的当前字节，得到258个候选源分组（其中运行时候选142个）。同时检查Assets内场景、预制体和动画/控制器/Timeline文件：{'.prefab': 11, '.unity': 1}。

这些数量表示扫描覆盖。当前页面是否真正启用仍需按入口确认；测试、物理、旧逻辑和未启用实验分别保留，不直接计为遗漏动画。

## 这轮确认遗漏并补入的7项

| 设计 | 已核对调用链 | 本轮交付 |
|---|---|---|
| 结算星光到达时分数变金色并放大回落 | `AppUI.BuildBattleHUD → BattleHUDSummary.Initialize；VictoryFinale.Advance / VictoryRoundPreparation.Update → ShowVictoryScore(impact=true)` | `ScoreImpact` / `scoreimpact` |
| 剩余步数改变时挤压；此交付只提取步数视觉 | `AppUI.BuildBattleHUD → ChainCollectionHUD.Initialize → StateChanged → Refresh → Update` | `MovesFeedback` / `moves` |
| 实际收集值改变触发目标到达挤压；完成显示印章 | `ChainCollectionHUD.Initialize → AddCard → ChainTaskCard.Initialize / Refresh` | `TaskArrival` / `taskarrival` |
| 连击评级变动后字图放大与星光爆发 | `ChainCollectionHUD.Initialize → ChainRatingView.Initialize；Update观察ChainRatingSerial` | `CheerBurst` / `rating` |
| 首屏错峰或名次/积分更新时卡片果冻抖动 | `LeagueRankingView.Refresh / LeaguePodiumView.Build → LeagueJelly.Play` | `LeagueJelly` / `leaguejelly` |
| 主题改变时旧背景SmoothStep淡出；只提取Transition透明度段 | `AppUI.BuildHome → BuildChapterHome → ChapterHomeView.Build → Transition(previousTheme)` | `ThemeDissolve` / `themedissolve` |
| 短窗口合并加分，提示放大、上移与淡出 | `AppUI.BuildScoreHUD（非Opening/Tutorial的ChainCollection分支） → ScoreGainPresentation.Initialize → StateChanged` | `ScoreGain` / `scoregain` |

分数相关现在分为：单段数字滚动、到达时变金放大、加分合并上浮。这三项用途与触发时机不同，分别体验和复制Prompt。

## 仍需继续确认或拆分的范围

还存在开章光门、地图节点/路径/拖拽回弹、主页欢迎语分层入场、联赛匹配和排行位移、连胜奖励、教学引导、弹幕、目标庆祝等候选；场景侧另有藤蔓摆动、镜头、碎片、消除和特殊机制编排。已扫描并记录，尚未把它们统称为已交付组件。

下面列出全部94个待确认分组；它们可能含业务、物理或不可达分支。已交付的一部分也不能代表同文件其他动画全部交付。

| 源分组 | 本次匹配方法 |
|---|---|
| `Assets/Scripts/Core/BoardFlowPlanner.cs` / GameSession | FlowRelativeCollision, FlowPositionAt |
| `Assets/Scripts/Core/BossLevel.cs` / GameSession | TickBoss |
| `Assets/Scripts/Core/ChainCollectionSession.cs` / GameSession | TickChainCollection |
| `Assets/Scripts/Core/CollectionSession.cs` / GameSession | FallCollectionTreats, TickCollection |
| `Assets/Scripts/Core/FlowJellySession.cs` / FlowJellyDeform | LateUpdate |
| `Assets/Scripts/Core/FusionPresentationSession.cs` / GameSession | TickFusionPresentation |
| `Assets/Scripts/Core/GameSession.cs` / GameSession | StartSession, PrepareSession, BuildSession, NormalizeAimDirection |
| `Assets/Scripts/Core/OpeningCampaign.cs` / GameSession | InitializeOpeningGridSteps |
| `Assets/Scripts/Core/OpeningTargets.cs` / GameSession | OpeningCollect |
| `Assets/Scripts/Core/OpeningThirdSession.cs` / GameSession | TickOpeningThird |
| `Assets/Scripts/Core/PixelDropSession.cs` / PixelDropBody | Tick, LateUpdate |
| `Assets/Scripts/Core/VictoryPlayback.cs` / GameSession | PresentationDelta |
| `Assets/Scripts/SoundService.cs` / SoundService | WarmEffects, Trigger, Speak, Crossfade, Update, SetBusVolume, SaveMix |
| `Assets/Scripts/UI/ActivityCarousel.cs` / ActivityCarousel | Settle |
| `Assets/Scripts/UI/AlphabetIntroductionView.Boosters.cs` / BoosterStoryActor | Update |
| `Assets/Scripts/UI/AlphabetIntroductionView.cs` / AlphabetIntroductionView | Begin, Enter, Advance, AdvanceForQA, Cancel, OnDestroy |
| `Assets/Scripts/UI/AlphabetIntroductionView.Motion.cs` / AlphabetIntroductionView | AdvanceMotion |
| `Assets/Scripts/UI/AlphabetTweenExtensions.cs` / AlphabetTweenExtensions | DOAnchorPos, DOAnchorPosY, DOFade, DOFade |
| `Assets/Scripts/UI/AppUI.Alphabet.cs` / AppUI | ShowAlphabetIntroductions |
| `Assets/Scripts/UI/AppUI.BoosterUnlocks.cs` / AppUI | ShowBoosterUnlocks |
| `Assets/Scripts/UI/AppUI.ChapterOpening70.cs` / AppUI | OpenChapterOnHome, ReturnHomeAfterChapterResult |
| `Assets/Scripts/UI/AppUI.ContentLoading.cs` / AppUI | PrefetchFollowingLevel, PrepareOutcomeRoutine, RunLevelLoad, PrepareMenuContentRoutine |
| `Assets/Scripts/UI/AppUI.cs` / AppUI | Update, ShowPage, StartIntroWhenVisible, DismissIntro, StartPreparedLevel, HideTutorialLater, OpenModalMotion, CloseModal, Pop, CountWallet, FlyCoins, FlyCoin, WalletPulse, Fireworks, Spark, Confetti, Toast, QAWaitForLevel |
| `Assets/Scripts/UI/AppUI.LeagueActivity.cs` / AppUI | TickLeagueActivity |
| `Assets/Scripts/UI/AppUI.LeagueFlow.cs` / AppUI | TickLeagueFlow |
| `Assets/Scripts/UI/AppUI.PlayerSettings71.cs` / AppUI | FitSettingsModal77 |
| `Assets/Scripts/UI/AppUI.SettingsC.cs` / AppUI | LayoutSettingsSheetC |
| `Assets/Scripts/UI/AppUI.Shop.cs` / AppUI | ShopEnter, OpenReferenceModal, DrawDressing, CosmeticSpark |
| `Assets/Scripts/UI/AppUI.StreakReward.cs` / AppUI | ShowCampaignStreakReward |
| `Assets/Scripts/UI/AppUI.StreakReward.cs` / StreakRewardGlow | OnPopulateMesh |
| `Assets/Scripts/UI/AppUI.StreakReward.cs` / StreakRewardPresentation | Play |
| `Assets/Scripts/UI/AppUI.Tutorial.cs` / AppUI | ShowFirstBounceIntroduction |
| `Assets/Scripts/UI/BattleInterfaceTour.cs` / AppUI | ShowBattleInterfaceTour |
| `Assets/Scripts/UI/BattleInterfaceTour.cs` / BattleInterfaceTour | Begin, Place, Advance, Update |
| `Assets/Scripts/UI/BattleScoreMeter.cs` / BattleScoreMeter | OnBall, LateUpdate |
| `Assets/Scripts/UI/BattleUIArt.cs` / BattleAimHint | Update |
| `Assets/Scripts/UI/BossHUD.cs` / BossHUD | Update |
| `Assets/Scripts/UI/ChapterOpening70.cs` / ChapterOpening70 | Sample, Update |
| `Assets/Scripts/UI/CollectionGoalEntrance.cs` / AppUI | ShowCollectionGoalEntrance |
| `Assets/Scripts/UI/GoalCelebration.cs` / GoalCelebration | Update, Render |
| `Assets/Scripts/UI/HomeFeedbackView.cs` / HomeFeedbackView | Update, Pop |
| `Assets/Scripts/UI/InGameFeedbackView.cs` / InGameFeedbackView | LateUpdate, AnimateCheer |
| `Assets/Scripts/UI/JellyChapterOrb.cs` / JellyChapterOrb | Update |
| `Assets/Scripts/UI/JellyJourney80.cs` / JellyJourney80 | MotionClock, Sway, PullJelly, EndJellyDrag, LateUpdate |
| `Assets/Scripts/UI/JellyJourneyMotion80.cs` / JellyLevelMotion80 | Clock, Entrance, OnPointerDown, Release, Apply |
| `Assets/Scripts/UI/JellyJourneyMotion80.cs` / JellyRouteRibbon80 | Configure, RefreshMotion, OnPopulateMesh |
| `Assets/Scripts/UI/LeagueBannerStatus.cs` / LeagueBannerStatus | Update |
| `Assets/Scripts/UI/LeagueMatchPresentation.cs` / LeagueMatchPresentation | Build, Update |
| `Assets/Scripts/UI/LeagueRankingView.cs` / LeagueRankingView | Update, UpdateClock, Refresh |
| `Assets/Scripts/UI/LessonCollectionFlight.cs` / LessonCollectionFlight | Update |
| `Assets/Scripts/UI/OpeningIntroduction.cs` / AppUI | ShowOpeningIntroduction |
| `Assets/Scripts/UI/OpeningIntroduction.cs` / OpeningRuleDemo | Render, Update |
| `Assets/Scripts/UI/PaintingCollectionPresentation.cs` / PaintingCollectionPresentation | Begin, Update, Render |
| `Assets/Scripts/UI/PlayableTutorialGuide.cs` / PlayableTutorialGuide | Update |
| `Assets/Scripts/UI/TutorialMotionBoard.cs` / TutorialMotionBoard | Configure, Render |
| `Assets/Scripts/UI/UIBounce.cs` / JellyButtonMesh | Refresh |
| `Assets/Scripts/UI/VictoryFinale.cs` / VictoryFinale | Begin, Update, Advance |
| `Assets/Scripts/UI/VictoryRoundPreparation.cs` / VictoryRoundPreparation | Update |
| `Assets/Scripts/World/AbilityArt.cs` / AbilityFrameFx | Update |
| `Assets/Scripts/World/CameraJuice.cs` / CameraJuice | Impact, LateUpdate |
| `Assets/Scripts/World/ChainCubeArt.cs` / ChainBondView | Update |
| `Assets/Scripts/World/ChainCubeArt.cs` / ChainCubeBurst | Update |
| `Assets/Scripts/World/ChainCubeArt.cs` / ChainCubeIdle | Update |
| `Assets/Scripts/World/ChainCubeArt.cs` / ChainCubeView | Update |
| `Assets/Scripts/World/ChainVisualArt.cs` / ChainBrickView | Tick, Update |
| `Assets/Scripts/World/ChainVisualArt.cs` / ChainGlint | Update |
| `Assets/Scripts/World/ChainVisualArt.cs` / ChainIdleSprite | Tick, Update |
| `Assets/Scripts/World/ChainVisualArt.cs` / ChainSpriteFx | Update |
| `Assets/Scripts/World/ChainVisualArt.cs` / ChainStoneView | Update |
| `Assets/Scripts/World/CombatFeedback79.cs` / CombatFeedback79 | EmphasizeSpecial, Initialize, Cue, RequestStop, Update, AdvanceParticles, LateUpdate, ReleaseClock, ReleaseSlow |
| `Assets/Scripts/World/ElectricCircuit.cs` / ElectricCircuit | Update, UpdateLines |
| `Assets/Scripts/World/FusionBallBudgetView.cs` / FusionBallBudgetView | Refresh |
| `Assets/Scripts/World/GameplayFlight.cs` / FlightTrail | Configure, BindYarn, LateUpdate |
| `Assets/Scripts/World/IngotSpecialFx.cs` / IngotSpecialFx | Play, Update |
| `Assets/Scripts/World/JellyBrickArt.cs` / JellyPopPool | Update |
| `Assets/Scripts/World/JellyWallFrame.cs` / JellyWallFrame | Envelope, Influence, PulseShape, LateUpdate |
| `Assets/Scripts/World/JuiceGardenView.cs` / JuiceGardenView | Impact, Tether, Callout, PawFlick, Update |
| `Assets/Scripts/World/MainlineArt.cs` / MainlineBrickView | Update |
| `Assets/Scripts/World/MainlineArt.cs` / MainlineCanvas | Update, Render |
| `Assets/Scripts/World/MainlineArt.cs` / MainlineFrameFx | Update |
| `Assets/Scripts/World/OpeningCookieBoxView.cs` / OpeningCookieBoxView | Tick |
| `Assets/Scripts/World/OpeningCopperArt.cs` / OpeningCopperEffect | Render |
| `Assets/Scripts/World/OpeningCopperArt.cs` / OpeningCopperView | Render |
| `Assets/Scripts/World/OpeningThirdArt.cs` / OpeningThirdDirector | Update |
| `Assets/Scripts/World/OpeningYarnArt.cs` / OpeningYarnEffect | Render |
| `Assets/Scripts/World/OpeningYarnArt.cs` / OpeningYarnView | Tick |
| `Assets/Scripts/World/PaintingBoard.cs` / PaintingBoard | Update, Render |
| `Assets/Scripts/World/RearCatLauncher.cs` / RearCatLauncher | Update |
| `Assets/Scripts/World/SevenSpecialArt.cs` / SevenSpecialFx | Update |
| `Assets/Scripts/World/SevenSpecialArt.cs` / SevenSpecialIdle | Update |
| `Assets/Scripts/World/SevenSpecialCinematics.cs` / SevenSuctionFx | Update, Advance |
| `Assets/Scripts/World/SevenSpecialCinematics.cs` / SevenWindup | Update, Advance |
| `Assets/Scripts/World/StreakBallArt.cs` / StreakImpactPool | Update |
| `Assets/Scripts/World/WorldArt.cs` / WorldFx | Emit, Update |

维护证据：[逐文件扫描与剩余候选](coverage-audit.json)。展馆只展示可操作效果与迁移Prompt，不展示源码档案。
