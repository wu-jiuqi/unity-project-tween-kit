# 项目动画来源目录

扫描主工程 `BounceGarden/BounceGarden`；以 `DEVELOPMENT_BASELINE.json` 的0.5.1工作版为场景背景。数量由每次真实扫描产生。

这是来源清单及交付进度表。`source-route-reviewed` 表示核对了源码入口链，不等于已在正常游戏中实测每个调用点。其余候选保持待核对；最终全量验收尚未通过。

本次有 113 个源文件／类分组，含测试、编辑器与非动画候选；139 个未命中动画模式的源码文件另列。

[机器清单](source-map.json) · [源码指纹](source-fingerprints.json) · [完整资源扫描](resource-inventory.json) · [交付素材映射](resource-map.json) · [提取差异](extraction-diffs.json) · [验收状态](acceptance.json)

## mixed-physics-presentation-review · 2

| 条目 | 源文件 | 交付 |
|---|---|---|
| [BallBody](recipes/world-physicsbodies-ballbody.md) | `Assets/Scripts/World/PhysicsBodies.cs` | source-evidence-only |
| [BrickBody](recipes/world-physicsbodies-brickbody.md) | `Assets/Scripts/World/PhysicsBodies.cs` | source-evidence-only |

## non-animation · 9

| 条目 | 源文件 | 交付 |
|---|---|---|
| [BalanceStatistics](recipes/balancestatistics-balancestatistics.md) | `Assets/Scripts/BalanceStatistics.cs` | source-evidence-only |
| [BalanceStatistics](recipes/balancestatistics-strategy-balancestatistics.md) | `Assets/Scripts/BalanceStatistics.Strategy.cs` | source-evidence-only |
| [ContentLoadOperation](recipes/core-contentflow-contentloadoperation.md) | `Assets/Scripts/Core/ContentFlow.cs` | source-evidence-only |
| [GameContent](recipes/core-contentflow-gamecontent.md) | `Assets/Scripts/Core/ContentFlow.cs` | source-evidence-only |
| [StartupAttempt77](recipes/core-startupattempt77-startupattempt77.md) | `Assets/Scripts/Core/StartupAttempt77.cs` | source-evidence-only |
| [PlayerHaptics71](recipes/playerhaptics71-playerhaptics71.md) | `Assets/Scripts/PlayerHaptics71.cs` | source-evidence-only |
| [PlaytestMetrics76](recipes/playtestmetrics76-playtestmetrics76.md) | `Assets/Scripts/PlaytestMetrics76.cs` | source-evidence-only |
| [BoardDisplay](recipes/world-boarddisplay-boarddisplay.md) | `Assets/Scripts/World/BoardDisplay.cs` | source-evidence-only |
| [WorldArt](recipes/world-worldart-worldart.md) | `Assets/Scripts/World/WorldArt.cs` | source-evidence-only |

## requires-route-review · 67

| 条目 | 源文件 | 交付 |
|---|---|---|
| [GameSession](recipes/core-boardflowplanner-gamesession.md) | `Assets/Scripts/Core/BoardFlowPlanner.cs` | source-evidence-only |
| [GameSession](recipes/core-chaincollectionsession-gamesession.md) | `Assets/Scripts/Core/ChainCollectionSession.cs` | source-evidence-only |
| [FlowJellyDeform](recipes/core-flowjellysession-flowjellydeform.md) | `Assets/Scripts/Core/FlowJellySession.cs` | source-evidence-only |
| [GameSession](recipes/core-gamesession-gamesession.md) | `Assets/Scripts/Core/GameSession.cs` | source-evidence-only |
| [GameSession](recipes/core-openingcampaign-gamesession.md) | `Assets/Scripts/Core/OpeningCampaign.cs` | source-evidence-only |
| [GameSession](recipes/core-openingtargets-gamesession.md) | `Assets/Scripts/Core/OpeningTargets.cs` | source-evidence-only |
| [GameSession](recipes/core-openingthirdsession-gamesession.md) | `Assets/Scripts/Core/OpeningThirdSession.cs` | source-evidence-only |
| [GameSession](recipes/core-victoryplayback-gamesession.md) | `Assets/Scripts/Core/VictoryPlayback.cs` | source-evidence-only |
| [SoundService](recipes/soundservice-soundservice.md) | `Assets/Scripts/SoundService.cs` | source-evidence-only |
| [AlphabetIntroductionView](recipes/ui-alphabetintroductionview-alphabetintroductionview.md) | `Assets/Scripts/UI/AlphabetIntroductionView.cs` | source-evidence-only |
| [AlphabetTweenExtensions](recipes/ui-alphabettweenextensions-alphabettweenextensions.md) | `Assets/Scripts/UI/AlphabetTweenExtensions.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-alphabet-appui.md) | `Assets/Scripts/UI/AppUI.Alphabet.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-contentloading-appui.md) | `Assets/Scripts/UI/AppUI.ContentLoading.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-appui.md) | `Assets/Scripts/UI/AppUI.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-league046-appui.md) | `Assets/Scripts/UI/AppUI.League046.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-leagueactivity-appui.md) | `Assets/Scripts/UI/AppUI.LeagueActivity.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-leagueflow-appui.md) | `Assets/Scripts/UI/AppUI.LeagueFlow.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-playersettings71-appui.md) | `Assets/Scripts/UI/AppUI.PlayerSettings71.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-settingsc-appui.md) | `Assets/Scripts/UI/AppUI.SettingsC.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-streakreward-appui.md) | `Assets/Scripts/UI/AppUI.StreakReward.cs` | source-evidence-only |
| [StreakRewardGlow](recipes/ui-appui-streakreward-streakrewardglow.md) | `Assets/Scripts/UI/AppUI.StreakReward.cs` | source-evidence-only |
| [StreakRewardPresentation](recipes/ui-appui-streakreward-streakrewardpresentation.md) | `Assets/Scripts/UI/AppUI.StreakReward.cs` | source-evidence-only |
| [BrandGardenHedge](recipes/ui-brandgardenhedge-brandgardenhedge.md) | `Assets/Scripts/UI/BrandGardenHedge.cs` | source-evidence-only |
| [BrandGardenScene](recipes/ui-brandgardenscene-brandgardenscene.md) | `Assets/Scripts/UI/BrandGardenScene.cs` | source-evidence-only |
| [BrandLoadingProgress](recipes/ui-brandloadingprogress-brandloadingprogress.md) | `Assets/Scripts/UI/BrandLoadingProgress.cs` | source-evidence-only |
| [BrandStartupHost](recipes/ui-brandstartuphost-brandstartuphost.md) | `Assets/Scripts/UI/BrandStartupHost.cs` | source-evidence-only |
| [AppUI](recipes/ui-collectiongoalentrance-appui.md) | `Assets/Scripts/UI/CollectionGoalEntrance.cs` | source-evidence-only |
| [DressingShopArt044](recipes/ui-dressingshopart044-dressingshopart044.md) | `Assets/Scripts/UI/DressingShopArt044.cs` | source-evidence-only |
| [AppUI](recipes/ui-errorpopup0411-appui.md) | `Assets/Scripts/UI/ErrorPopup0411.cs` | source-evidence-only |
| [HomeAFeedback](recipes/ui-homeafeedback-homeafeedback.md) | `Assets/Scripts/UI/HomeAFeedback.cs` | source-evidence-only |
| [HomeANavigation](recipes/ui-homeanavigation-homeanavigation.md) | `Assets/Scripts/UI/HomeANavigation.cs` | source-evidence-only |
| [HomeALogo](recipes/ui-homeaview-homealogo.md) | `Assets/Scripts/UI/HomeAView.cs` | source-evidence-only |
| [InGameFeedbackView](recipes/ui-ingamefeedbackview-ingamefeedbackview.md) | `Assets/Scripts/UI/InGameFeedbackView.cs` | source-evidence-only |
| [JellyChapterOrb](recipes/ui-jellychapterorb-jellychapterorb.md) | `Assets/Scripts/UI/JellyChapterOrb.cs` | source-evidence-only |
| [JellyJourney80](recipes/ui-jellyjourney80-jellyjourney80.md) | `Assets/Scripts/UI/JellyJourney80.cs` | source-evidence-only |
| [JellyLevelMotion80](recipes/ui-jellyjourneymotion80-jellylevelmotion80.md) | `Assets/Scripts/UI/JellyJourneyMotion80.cs` | source-evidence-only |
| [JellyRouteRibbon80](recipes/ui-jellyjourneymotion80-jellyrouteribbon80.md) | `Assets/Scripts/UI/JellyJourneyMotion80.cs` | source-evidence-only |
| [JellyLogoDecoration](recipes/ui-jellylogointro-jellylogodecoration.md) | `Assets/Scripts/UI/JellyLogoIntro.cs` | source-evidence-only |
| [JellyLogoMotion](recipes/ui-jellylogointro-jellylogomotion.md) | `Assets/Scripts/UI/JellyLogoIntro.cs` | source-evidence-only |
| [AppUI](recipes/ui-openingintroduction-appui.md) | `Assets/Scripts/UI/OpeningIntroduction.cs` | source-evidence-only |
| [OpeningStory049Art](recipes/ui-openingstory049-openingstory049art.md) | `Assets/Scripts/UI/OpeningStory049.cs` | source-evidence-only |
| [OpeningStoryStep049](recipes/ui-openingstory049-openingstorystep049.md) | `Assets/Scripts/UI/OpeningStory049.cs` | source-evidence-only |
| [JellyButtonMesh](recipes/ui-uibounce-jellybuttonmesh.md) | `Assets/Scripts/UI/UIBounce.cs` | source-evidence-only |
| [VictoryFinale](recipes/ui-victoryfinale-victoryfinale.md) | `Assets/Scripts/UI/VictoryFinale.cs` | source-evidence-only |
| [VictoryRoundPreparation](recipes/ui-victoryroundpreparation-victoryroundpreparation.md) | `Assets/Scripts/UI/VictoryRoundPreparation.cs` | source-evidence-only |
| [CameraJuice](recipes/world-camerajuice-camerajuice.md) | `Assets/Scripts/World/CameraJuice.cs` | source-evidence-only |
| [ChainGlint](recipes/world-chainvisualart-chainglint.md) | `Assets/Scripts/World/ChainVisualArt.cs` | source-evidence-only |
| [ChainSpriteFx](recipes/world-chainvisualart-chainspritefx.md) | `Assets/Scripts/World/ChainVisualArt.cs` | source-evidence-only |
| [CombatFeedback79](recipes/world-combatfeedback79-combatfeedback79.md) | `Assets/Scripts/World/CombatFeedback79.cs` | source-evidence-only |
| [FlightTrail](recipes/world-gameplayflight-flighttrail.md) | `Assets/Scripts/World/GameplayFlight.cs` | source-evidence-only |
| [IngotSpecialFx](recipes/world-ingotspecialfx-ingotspecialfx.md) | `Assets/Scripts/World/IngotSpecialFx.cs` | source-evidence-only |
| [JellyPopPool](recipes/world-jellybrickart-jellypoppool.md) | `Assets/Scripts/World/JellyBrickArt.cs` | source-evidence-only |
| [JellyWallFrame](recipes/world-jellywallframe-jellywallframe.md) | `Assets/Scripts/World/JellyWallFrame.cs` | source-evidence-only |
| [OpeningCookieBoxView](recipes/world-openingcookieboxview-openingcookieboxview.md) | `Assets/Scripts/World/OpeningCookieBoxView.cs` | source-evidence-only |
| [OpeningCopperEffect](recipes/world-openingcopperart-openingcoppereffect.md) | `Assets/Scripts/World/OpeningCopperArt.cs` | source-evidence-only |
| [OpeningCopperView](recipes/world-openingcopperart-openingcopperview.md) | `Assets/Scripts/World/OpeningCopperArt.cs` | source-evidence-only |
| [OpeningThirdDirector](recipes/world-openingthirdart-openingthirddirector.md) | `Assets/Scripts/World/OpeningThirdArt.cs` | source-evidence-only |
| [OpeningYarnEffect](recipes/world-openingyarnart-openingyarneffect.md) | `Assets/Scripts/World/OpeningYarnArt.cs` | source-evidence-only |
| [OpeningYarnView](recipes/world-openingyarnart-openingyarnview.md) | `Assets/Scripts/World/OpeningYarnArt.cs` | source-evidence-only |
| [PetalField050](recipes/world-petalfield050-petalfield050.md) | `Assets/Scripts/World/PetalField050.cs` | source-evidence-only |
| [RearCatLauncher](recipes/world-rearcatlauncher-rearcatlauncher.md) | `Assets/Scripts/World/RearCatLauncher.cs` | source-evidence-only |
| [SevenSpecialFx](recipes/world-sevenspecialart-sevenspecialfx.md) | `Assets/Scripts/World/SevenSpecialArt.cs` | source-evidence-only |
| [SevenSpecialIdle](recipes/world-sevenspecialart-sevenspecialidle.md) | `Assets/Scripts/World/SevenSpecialArt.cs` | source-evidence-only |
| [SevenSuctionFx](recipes/world-sevenspecialcinematics-sevensuctionfx.md) | `Assets/Scripts/World/SevenSpecialCinematics.cs` | source-evidence-only |
| [SevenWindup](recipes/world-sevenspecialcinematics-sevenwindup.md) | `Assets/Scripts/World/SevenSpecialCinematics.cs` | source-evidence-only |
| [StreakImpactPool](recipes/world-streakballart-streakimpactpool.md) | `Assets/Scripts/World/StreakBallArt.cs` | source-evidence-only |
| [WorldFx](recipes/world-worldart-worldfx.md) | `Assets/Scripts/World/WorldArt.cs` | source-evidence-only |

## source-route-reviewed · 33

| 条目 | 源文件 | 交付 |
|---|---|---|
| [BattleHUDSummary](recipes/ui-battlehudsummary-battlehudsummary.md) | `Assets/Scripts/UI/BattleHUDSummary.cs` | ScoreImpact |
| [CollectionFlightMotion](recipes/ui-battlejellyart-collectionflightmotion.md) | `Assets/Scripts/UI/BattleJellyArt.cs` | RewardFlight |
| [BattleScoreMeter](recipes/ui-battlescoremeter-battlescoremeter.md) | `Assets/Scripts/UI/BattleScoreMeter.cs` | PendantSwing |
| [BattleAimHint](recipes/ui-battleuiart-battleaimhint.md) | `Assets/Scripts/UI/BattleUIArt.cs` | HintFade |
| [ChainCollectionHUD](recipes/ui-chaincollectionhud-chaincollectionhud.md) | `Assets/Scripts/UI/ChainCollectionHUD.cs` | MovesFeedback |
| [ChainTaskCard](recipes/ui-chaincollectionhud-chaintaskcard.md) | `Assets/Scripts/UI/ChainCollectionHUD.cs` | TaskArrival |
| [ChainRatingView](recipes/ui-chainratingview-chainratingview.md) | `Assets/Scripts/UI/ChainRatingView.cs` | CheerBurst |
| [CollectionGoalEntrance](recipes/ui-collectiongoalentrance-collectiongoalentrance.md) | `Assets/Scripts/UI/CollectionGoalEntrance.cs` | CollectionGoalEntrance |
| [EncouragementDrag](recipes/ui-encouragementdrag-encouragementdrag.md) | `Assets/Scripts/UI/EncouragementDrag.cs` | EncouragementDrag |
| [ErrorPopup0411](recipes/ui-errorpopup0411-errorpopup0411.md) | `Assets/Scripts/UI/ErrorPopup0411.cs` | ErrorPopupMotion |
| [FailurePresentation](recipes/ui-failurepresentation-failurepresentation.md) | `Assets/Scripts/UI/FailurePresentation.cs` | FailurePresentation |
| [GoalCelebration](recipes/ui-goalcelebration-goalcelebration.md) | `Assets/Scripts/UI/GoalCelebration.cs` | GoalCelebration |
| [HomeFeedbackView](recipes/ui-homefeedbackview-homefeedbackview.md) | `Assets/Scripts/UI/HomeFeedbackView.cs` | HomeLayeredFeedback |
| [HomeWordmarkWiggle](recipes/ui-homewordmarkwiggle-homewordmarkwiggle.md) | `Assets/Scripts/UI/HomeWordmarkWiggle.cs` | HomeWordmarkWiggle |
| [JellyGlyph](recipes/ui-jellylogointro-jellyglyph.md) | `Assets/Scripts/UI/JellyLogoIntro.cs` | JellyGlyph |
| [JellyLogoIntro](recipes/ui-jellylogointro-jellylogointro.md) | `Assets/Scripts/UI/JellyLogoIntro.cs` | JellyLogoIntro |
| [LeagueMatchPresentation](recipes/ui-leaguematchpresentation-leaguematchpresentation.md) | `Assets/Scripts/UI/LeagueMatchPresentation.cs` | DecorativeBreath |
| [LeagueMedalPresentation](recipes/ui-leaguemotionart-leaguemedalpresentation.md) | `Assets/Scripts/UI/LeagueMotionArt.cs` | LeagueMedalPresentation |
| [LeagueJelly](recipes/ui-leaguerankingview-leaguejelly.md) | `Assets/Scripts/UI/LeagueRankingView.cs` | LeagueJelly |
| [LeagueRankingView](recipes/ui-leaguerankingview-leaguerankingview.md) | `Assets/Scripts/UI/LeagueRankingView.cs` | RankReorder |
| [LoadingBoxAnimation](recipes/ui-loadingboxanimation-loadingboxanimation.md) | `Assets/Scripts/UI/LoadingBoxAnimation.cs` | LoadingBoxAnimation |
| [LoadingPresentation](recipes/ui-loadingpresentation-loadingpresentation.md) | `Assets/Scripts/UI/LoadingPresentation.cs` | LoadingPresentation |
| [OpeningStory049](recipes/ui-openingstory049-openingstory049.md) | `Assets/Scripts/UI/OpeningStory049.cs` | OpeningStory049 |
| [ResultAudioTimeline](recipes/ui-resultaudiotimeline-resultaudiotimeline.md) | `Assets/Scripts/UI/ResultAudioTimeline.cs` | ResultAudioTimeline |
| [ScoreGainPresentation](recipes/ui-scoregainpresentation-scoregainpresentation.md) | `Assets/Scripts/UI/ScoreGainPresentation.cs` | ScoreGain |
| [SettingsSwitch77](recipes/ui-settingsswitch77-settingsswitch77.md) | `Assets/Scripts/UI/SettingsSwitch77.cs` | SettingsSwitch77 |
| [StarChargeView](recipes/ui-starchargeview-starchargeview.md) | `Assets/Scripts/UI/StarChargeView.cs` | StarChargeView |
| [ToastNotification0410](recipes/ui-toastnotification0410-toastnotification0410.md) | `Assets/Scripts/UI/ToastNotification0410.cs` | ToastMotion |
| [UIBounce](recipes/ui-uibounce-uibounce.md) | `Assets/Scripts/UI/UIBounce.cs` | UIBounce |
| [UIFloat](recipes/ui-uicraft-uifloat.md) | `Assets/Scripts/UI/UICraft.cs` | UIFloat |
| [VictoryPresentation](recipes/ui-victorypresentation-victorypresentation.md) | `Assets/Scripts/UI/VictoryPresentation.cs` | VictoryPresentation |
| [VictoryPresentation](recipes/ui-victorypresentation-stars-victorypresentation.md) | `Assets/Scripts/UI/VictoryPresentation.Stars.cs` | VictoryPresentation.Stars |
| [JellyMotion](recipes/world-jellymotion-jellymotion.md) | `Assets/Scripts/World/JellyMotion.cs` | JellyHit |

## test-fixture · 2

| 条目 | 源文件 | 交付 |
|---|---|---|
| [AlphabetPhysicsSurvey](recipes/alphabetphysicssurvey-alphabetphysicssurvey.md) | `Assets/Scripts/AlphabetPhysicsSurvey.cs` | source-evidence-only |
| [MobileFrameStats](recipes/mobileframestats-mobileframestats.md) | `Assets/Scripts/MobileFrameStats.cs` | source-evidence-only |
