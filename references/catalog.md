# 项目动画来源目录

扫描主工程 `BounceGarden/BounceGarden`；以 `DEVELOPMENT_BASELINE.json` 的0.4.0工作版为场景背景。数量由每次真实扫描产生。

这是来源清单及交付进度表。`source-route-reviewed` 表示核对了源码入口链，不等于已在正常游戏中实测每个调用点。其余候选保持待核对；最终全量验收尚未通过。

本次有 258 个源文件／类分组，含测试、编辑器与非动画候选；256 个未命中动画模式的源码文件另列。

[机器清单](source-map.json) · [源码指纹](source-fingerprints.json) · [完整资源扫描](resource-inventory.json) · [交付素材映射](resource-map.json) · [提取差异](extraction-diffs.json) · [验收状态](acceptance.json)

## editor-tool · 1

| 条目 | 源文件 | 交付 |
|---|---|---|
| [EarlyContinuousV40Runner](recipes/assets-editor-earlycontinuousv40qa-earlycontinuousv40runner.md) | `Assets/Editor/EarlyContinuousV40QA.cs` | source-evidence-only |

## inactive-preview · 1

| 条目 | 源文件 | 交付 |
|---|---|---|
| [ChapterTransitionPreview](recipes/ui-journeyart-chaptertransitionpreview.md) | `Assets/Scripts/UI/JourneyArt.cs` | source-evidence-only |

## legacy-route-disabled · 1

| 条目 | 源文件 | 交付 |
|---|---|---|
| [Round6Nav](recipes/ui-round6nav-round6nav.md) | `Assets/Scripts/UI/Round6Nav.cs` | source-evidence-only |

## mixed-physics-presentation-review · 4

| 条目 | 源文件 | 交付 |
|---|---|---|
| [BossBody](recipes/world-bossbody-bossbody.md) | `Assets/Scripts/World/BossBody.cs` | source-evidence-only |
| [BallBody](recipes/world-physicsbodies-ballbody.md) | `Assets/Scripts/World/PhysicsBodies.cs` | source-evidence-only |
| [BrickBody](recipes/world-physicsbodies-brickbody.md) | `Assets/Scripts/World/PhysicsBodies.cs` | source-evidence-only |
| [MovingObstacle](recipes/world-physicsbodies-movingobstacle.md) | `Assets/Scripts/World/PhysicsBodies.cs` | source-evidence-only |

## non-animation · 15

| 条目 | 源文件 | 交付 |
|---|---|---|
| [BalanceStatistics](recipes/balancestatistics-balancestatistics.md) | `Assets/Scripts/BalanceStatistics.cs` | source-evidence-only |
| [BalanceStatistics](recipes/balancestatistics-strategy-balancestatistics.md) | `Assets/Scripts/BalanceStatistics.Strategy.cs` | source-evidence-only |
| [ContentLoadOperation](recipes/core-contentflow-contentloadoperation.md) | `Assets/Scripts/Core/ContentFlow.cs` | source-evidence-only |
| [GameContent](recipes/core-contentflow-gamecontent.md) | `Assets/Scripts/Core/ContentFlow.cs` | source-evidence-only |
| [MainlinePlanner](recipes/core-mainlineplanner-mainlineplanner.md) | `Assets/Scripts/Core/MainlinePlanner.cs` | source-evidence-only |
| [StartupAttempt77](recipes/core-startupattempt77-startupattempt77.md) | `Assets/Scripts/Core/StartupAttempt77.cs` | source-evidence-only |
| [PlayerHaptics71](recipes/playerhaptics71-playerhaptics71.md) | `Assets/Scripts/PlayerHaptics71.cs` | source-evidence-only |
| [PlaytestMetrics76](recipes/playtestmetrics76-playtestmetrics76.md) | `Assets/Scripts/PlaytestMetrics76.cs` | source-evidence-only |
| [GameTypography](recipes/ui-gametypography-gametypography.md) | `Assets/Scripts/UI/GameTypography.cs` | source-evidence-only |
| [UICraft](recipes/ui-uicraft-uicraft.md) | `Assets/Scripts/UI/UICraft.cs` | source-evidence-only |
| [BoardDisplay](recipes/world-boarddisplay-boarddisplay.md) | `Assets/Scripts/World/BoardDisplay.cs` | source-evidence-only |
| [BoardSurface](recipes/world-boardsurface-boardsurface.md) | `Assets/Scripts/World/BoardSurface.cs` | source-evidence-only |
| [GardenSceneArt](recipes/world-gardensceneart-gardensceneart.md) | `Assets/Scripts/World/GardenSceneArt.cs` | source-evidence-only |
| [PixelPieceArt](recipes/world-pixelpieceart-pixelpieceart.md) | `Assets/Scripts/World/PixelPieceArt.cs` | source-evidence-only |
| [WorldArt](recipes/world-worldart-worldart.md) | `Assets/Scripts/World/WorldArt.cs` | source-evidence-only |

## requires-route-review · 101

| 条目 | 源文件 | 交付 |
|---|---|---|
| [GameSession](recipes/core-boardflowplanner-gamesession.md) | `Assets/Scripts/Core/BoardFlowPlanner.cs` | source-evidence-only |
| [GameSession](recipes/core-bosslevel-gamesession.md) | `Assets/Scripts/Core/BossLevel.cs` | source-evidence-only |
| [GameSession](recipes/core-chaincollectionsession-gamesession.md) | `Assets/Scripts/Core/ChainCollectionSession.cs` | source-evidence-only |
| [GameSession](recipes/core-collectionsession-gamesession.md) | `Assets/Scripts/Core/CollectionSession.cs` | source-evidence-only |
| [FlowJellyDeform](recipes/core-flowjellysession-flowjellydeform.md) | `Assets/Scripts/Core/FlowJellySession.cs` | source-evidence-only |
| [GameSession](recipes/core-fusionpresentationsession-gamesession.md) | `Assets/Scripts/Core/FusionPresentationSession.cs` | source-evidence-only |
| [GameSession](recipes/core-gamesession-gamesession.md) | `Assets/Scripts/Core/GameSession.cs` | source-evidence-only |
| [GameSession](recipes/core-openingcampaign-gamesession.md) | `Assets/Scripts/Core/OpeningCampaign.cs` | source-evidence-only |
| [GameSession](recipes/core-openingtargets-gamesession.md) | `Assets/Scripts/Core/OpeningTargets.cs` | source-evidence-only |
| [GameSession](recipes/core-openingthirdsession-gamesession.md) | `Assets/Scripts/Core/OpeningThirdSession.cs` | source-evidence-only |
| [PixelDropBody](recipes/core-pixeldropsession-pixeldropbody.md) | `Assets/Scripts/Core/PixelDropSession.cs` | source-evidence-only |
| [GameSession](recipes/core-victoryplayback-gamesession.md) | `Assets/Scripts/Core/VictoryPlayback.cs` | source-evidence-only |
| [SoundService](recipes/soundservice-soundservice.md) | `Assets/Scripts/SoundService.cs` | source-evidence-only |
| [ActivityCarousel](recipes/ui-activitycarousel-activitycarousel.md) | `Assets/Scripts/UI/ActivityCarousel.cs` | source-evidence-only |
| [BoosterStoryActor](recipes/ui-alphabetintroductionview-boosters-boosterstoryactor.md) | `Assets/Scripts/UI/AlphabetIntroductionView.Boosters.cs` | source-evidence-only |
| [AlphabetIntroductionView](recipes/ui-alphabetintroductionview-alphabetintroductionview.md) | `Assets/Scripts/UI/AlphabetIntroductionView.cs` | source-evidence-only |
| [AlphabetIntroductionView](recipes/ui-alphabetintroductionview-motion-alphabetintroductionview.md) | `Assets/Scripts/UI/AlphabetIntroductionView.Motion.cs` | source-evidence-only |
| [AlphabetTweenExtensions](recipes/ui-alphabettweenextensions-alphabettweenextensions.md) | `Assets/Scripts/UI/AlphabetTweenExtensions.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-alphabet-appui.md) | `Assets/Scripts/UI/AppUI.Alphabet.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-boosterunlocks-appui.md) | `Assets/Scripts/UI/AppUI.BoosterUnlocks.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-chapteropening70-appui.md) | `Assets/Scripts/UI/AppUI.ChapterOpening70.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-contentloading-appui.md) | `Assets/Scripts/UI/AppUI.ContentLoading.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-appui.md) | `Assets/Scripts/UI/AppUI.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-leagueactivity-appui.md) | `Assets/Scripts/UI/AppUI.LeagueActivity.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-leagueflow-appui.md) | `Assets/Scripts/UI/AppUI.LeagueFlow.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-playersettings71-appui.md) | `Assets/Scripts/UI/AppUI.PlayerSettings71.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-settingsc-appui.md) | `Assets/Scripts/UI/AppUI.SettingsC.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-shop-appui.md) | `Assets/Scripts/UI/AppUI.Shop.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-streakreward-appui.md) | `Assets/Scripts/UI/AppUI.StreakReward.cs` | source-evidence-only |
| [StreakRewardGlow](recipes/ui-appui-streakreward-streakrewardglow.md) | `Assets/Scripts/UI/AppUI.StreakReward.cs` | source-evidence-only |
| [StreakRewardPresentation](recipes/ui-appui-streakreward-streakrewardpresentation.md) | `Assets/Scripts/UI/AppUI.StreakReward.cs` | source-evidence-only |
| [AppUI](recipes/ui-appui-tutorial-appui.md) | `Assets/Scripts/UI/AppUI.Tutorial.cs` | source-evidence-only |
| [BattleHUDSummary](recipes/ui-battlehudsummary-battlehudsummary.md) | `Assets/Scripts/UI/BattleHUDSummary.cs` | source-evidence-only |
| [AppUI](recipes/ui-battleinterfacetour-appui.md) | `Assets/Scripts/UI/BattleInterfaceTour.cs` | source-evidence-only |
| [BattleInterfaceTour](recipes/ui-battleinterfacetour-battleinterfacetour.md) | `Assets/Scripts/UI/BattleInterfaceTour.cs` | source-evidence-only |
| [BattleScoreMeter](recipes/ui-battlescoremeter-battlescoremeter.md) | `Assets/Scripts/UI/BattleScoreMeter.cs` | source-evidence-only |
| [BattleAimHint](recipes/ui-battleuiart-battleaimhint.md) | `Assets/Scripts/UI/BattleUIArt.cs` | source-evidence-only |
| [BossHUD](recipes/ui-bosshud-bosshud.md) | `Assets/Scripts/UI/BossHUD.cs` | source-evidence-only |
| [ChainCollectionHUD](recipes/ui-chaincollectionhud-chaincollectionhud.md) | `Assets/Scripts/UI/ChainCollectionHUD.cs` | source-evidence-only |
| [ChainTaskCard](recipes/ui-chaincollectionhud-chaintaskcard.md) | `Assets/Scripts/UI/ChainCollectionHUD.cs` | source-evidence-only |
| [ChainRatingView](recipes/ui-chainratingview-chainratingview.md) | `Assets/Scripts/UI/ChainRatingView.cs` | source-evidence-only |
| [ChapterOpening70](recipes/ui-chapteropening70-chapteropening70.md) | `Assets/Scripts/UI/ChapterOpening70.cs` | source-evidence-only |
| [AppUI](recipes/ui-collectiongoalentrance-appui.md) | `Assets/Scripts/UI/CollectionGoalEntrance.cs` | source-evidence-only |
| [GoalCelebration](recipes/ui-goalcelebration-goalcelebration.md) | `Assets/Scripts/UI/GoalCelebration.cs` | source-evidence-only |
| [HomeFeedbackView](recipes/ui-homefeedbackview-homefeedbackview.md) | `Assets/Scripts/UI/HomeFeedbackView.cs` | source-evidence-only |
| [InGameFeedbackView](recipes/ui-ingamefeedbackview-ingamefeedbackview.md) | `Assets/Scripts/UI/InGameFeedbackView.cs` | source-evidence-only |
| [JellyChapterOrb](recipes/ui-jellychapterorb-jellychapterorb.md) | `Assets/Scripts/UI/JellyChapterOrb.cs` | source-evidence-only |
| [JellyJourney80](recipes/ui-jellyjourney80-jellyjourney80.md) | `Assets/Scripts/UI/JellyJourney80.cs` | source-evidence-only |
| [JellyLevelMotion80](recipes/ui-jellyjourneymotion80-jellylevelmotion80.md) | `Assets/Scripts/UI/JellyJourneyMotion80.cs` | source-evidence-only |
| [JellyRouteRibbon80](recipes/ui-jellyjourneymotion80-jellyrouteribbon80.md) | `Assets/Scripts/UI/JellyJourneyMotion80.cs` | source-evidence-only |
| [LeagueBannerStatus](recipes/ui-leaguebannerstatus-leaguebannerstatus.md) | `Assets/Scripts/UI/LeagueBannerStatus.cs` | source-evidence-only |
| [LeagueMatchPresentation](recipes/ui-leaguematchpresentation-leaguematchpresentation.md) | `Assets/Scripts/UI/LeagueMatchPresentation.cs` | source-evidence-only |
| [LeagueJelly](recipes/ui-leaguerankingview-leaguejelly.md) | `Assets/Scripts/UI/LeagueRankingView.cs` | source-evidence-only |
| [LeagueRankingView](recipes/ui-leaguerankingview-leaguerankingview.md) | `Assets/Scripts/UI/LeagueRankingView.cs` | source-evidence-only |
| [LessonCollectionFlight](recipes/ui-lessoncollectionflight-lessoncollectionflight.md) | `Assets/Scripts/UI/LessonCollectionFlight.cs` | source-evidence-only |
| [AppUI](recipes/ui-openingintroduction-appui.md) | `Assets/Scripts/UI/OpeningIntroduction.cs` | source-evidence-only |
| [OpeningRuleDemo](recipes/ui-openingintroduction-openingruledemo.md) | `Assets/Scripts/UI/OpeningIntroduction.cs` | source-evidence-only |
| [PaintingCollectionPresentation](recipes/ui-paintingcollectionpresentation-paintingcollectionpresentation.md) | `Assets/Scripts/UI/PaintingCollectionPresentation.cs` | source-evidence-only |
| [PlayableTutorialGuide](recipes/ui-playabletutorialguide-playabletutorialguide.md) | `Assets/Scripts/UI/PlayableTutorialGuide.cs` | source-evidence-only |
| [ChapterHomeView](recipes/ui-referencechapterview-chapterhomeview.md) | `Assets/Scripts/UI/ReferenceChapterView.cs` | source-evidence-only |
| [ScoreGainPresentation](recipes/ui-scoregainpresentation-scoregainpresentation.md) | `Assets/Scripts/UI/ScoreGainPresentation.cs` | source-evidence-only |
| [TutorialMotionBoard](recipes/ui-tutorialmotionboard-tutorialmotionboard.md) | `Assets/Scripts/UI/TutorialMotionBoard.cs` | source-evidence-only |
| [JellyButtonMesh](recipes/ui-uibounce-jellybuttonmesh.md) | `Assets/Scripts/UI/UIBounce.cs` | source-evidence-only |
| [VictoryFinale](recipes/ui-victoryfinale-victoryfinale.md) | `Assets/Scripts/UI/VictoryFinale.cs` | source-evidence-only |
| [VictoryRoundPreparation](recipes/ui-victoryroundpreparation-victoryroundpreparation.md) | `Assets/Scripts/UI/VictoryRoundPreparation.cs` | source-evidence-only |
| [AbilityFrameFx](recipes/world-abilityart-abilityframefx.md) | `Assets/Scripts/World/AbilityArt.cs` | source-evidence-only |
| [CameraJuice](recipes/world-camerajuice-camerajuice.md) | `Assets/Scripts/World/CameraJuice.cs` | source-evidence-only |
| [ChainBondView](recipes/world-chaincubeart-chainbondview.md) | `Assets/Scripts/World/ChainCubeArt.cs` | source-evidence-only |
| [ChainCubeBurst](recipes/world-chaincubeart-chaincubeburst.md) | `Assets/Scripts/World/ChainCubeArt.cs` | source-evidence-only |
| [ChainCubeIdle](recipes/world-chaincubeart-chaincubeidle.md) | `Assets/Scripts/World/ChainCubeArt.cs` | source-evidence-only |
| [ChainCubeView](recipes/world-chaincubeart-chaincubeview.md) | `Assets/Scripts/World/ChainCubeArt.cs` | source-evidence-only |
| [ChainBrickView](recipes/world-chainvisualart-chainbrickview.md) | `Assets/Scripts/World/ChainVisualArt.cs` | source-evidence-only |
| [ChainGlint](recipes/world-chainvisualart-chainglint.md) | `Assets/Scripts/World/ChainVisualArt.cs` | source-evidence-only |
| [ChainIdleSprite](recipes/world-chainvisualart-chainidlesprite.md) | `Assets/Scripts/World/ChainVisualArt.cs` | source-evidence-only |
| [ChainSpriteFx](recipes/world-chainvisualart-chainspritefx.md) | `Assets/Scripts/World/ChainVisualArt.cs` | source-evidence-only |
| [ChainStoneView](recipes/world-chainvisualart-chainstoneview.md) | `Assets/Scripts/World/ChainVisualArt.cs` | source-evidence-only |
| [CombatFeedback79](recipes/world-combatfeedback79-combatfeedback79.md) | `Assets/Scripts/World/CombatFeedback79.cs` | source-evidence-only |
| [ElectricCircuit](recipes/world-electriccircuit-electriccircuit.md) | `Assets/Scripts/World/ElectricCircuit.cs` | source-evidence-only |
| [FusionBallBudgetView](recipes/world-fusionballbudgetview-fusionballbudgetview.md) | `Assets/Scripts/World/FusionBallBudgetView.cs` | source-evidence-only |
| [FlightTrail](recipes/world-gameplayflight-flighttrail.md) | `Assets/Scripts/World/GameplayFlight.cs` | source-evidence-only |
| [IngotSpecialFx](recipes/world-ingotspecialfx-ingotspecialfx.md) | `Assets/Scripts/World/IngotSpecialFx.cs` | source-evidence-only |
| [JellyPopPool](recipes/world-jellybrickart-jellypoppool.md) | `Assets/Scripts/World/JellyBrickArt.cs` | source-evidence-only |
| [JellyWallFrame](recipes/world-jellywallframe-jellywallframe.md) | `Assets/Scripts/World/JellyWallFrame.cs` | source-evidence-only |
| [JuiceGardenView](recipes/world-juicegardenview-juicegardenview.md) | `Assets/Scripts/World/JuiceGardenView.cs` | source-evidence-only |
| [MainlineBrickView](recipes/world-mainlineart-mainlinebrickview.md) | `Assets/Scripts/World/MainlineArt.cs` | source-evidence-only |
| [MainlineCanvas](recipes/world-mainlineart-mainlinecanvas.md) | `Assets/Scripts/World/MainlineArt.cs` | source-evidence-only |
| [MainlineFrameFx](recipes/world-mainlineart-mainlineframefx.md) | `Assets/Scripts/World/MainlineArt.cs` | source-evidence-only |
| [OpeningCookieBoxView](recipes/world-openingcookieboxview-openingcookieboxview.md) | `Assets/Scripts/World/OpeningCookieBoxView.cs` | source-evidence-only |
| [OpeningCopperEffect](recipes/world-openingcopperart-openingcoppereffect.md) | `Assets/Scripts/World/OpeningCopperArt.cs` | source-evidence-only |
| [OpeningCopperView](recipes/world-openingcopperart-openingcopperview.md) | `Assets/Scripts/World/OpeningCopperArt.cs` | source-evidence-only |
| [OpeningThirdDirector](recipes/world-openingthirdart-openingthirddirector.md) | `Assets/Scripts/World/OpeningThirdArt.cs` | source-evidence-only |
| [OpeningYarnEffect](recipes/world-openingyarnart-openingyarneffect.md) | `Assets/Scripts/World/OpeningYarnArt.cs` | source-evidence-only |
| [OpeningYarnView](recipes/world-openingyarnart-openingyarnview.md) | `Assets/Scripts/World/OpeningYarnArt.cs` | source-evidence-only |
| [PaintingBoard](recipes/world-paintingboard-paintingboard.md) | `Assets/Scripts/World/PaintingBoard.cs` | source-evidence-only |
| [RearCatLauncher](recipes/world-rearcatlauncher-rearcatlauncher.md) | `Assets/Scripts/World/RearCatLauncher.cs` | source-evidence-only |
| [SevenSpecialFx](recipes/world-sevenspecialart-sevenspecialfx.md) | `Assets/Scripts/World/SevenSpecialArt.cs` | source-evidence-only |
| [SevenSpecialIdle](recipes/world-sevenspecialart-sevenspecialidle.md) | `Assets/Scripts/World/SevenSpecialArt.cs` | source-evidence-only |
| [SevenSuctionFx](recipes/world-sevenspecialcinematics-sevensuctionfx.md) | `Assets/Scripts/World/SevenSpecialCinematics.cs` | source-evidence-only |
| [SevenWindup](recipes/world-sevenspecialcinematics-sevenwindup.md) | `Assets/Scripts/World/SevenSpecialCinematics.cs` | source-evidence-only |
| [StreakImpactPool](recipes/world-streakballart-streakimpactpool.md) | `Assets/Scripts/World/StreakBallArt.cs` | source-evidence-only |
| [WorldFx](recipes/world-worldart-worldfx.md) | `Assets/Scripts/World/WorldArt.cs` | source-evidence-only |

## source-route-reviewed · 20

| 条目 | 源文件 | 交付 |
|---|---|---|
| [ActivityJellyMotion](recipes/ui-activityjellymotion-activityjellymotion.md) | `Assets/Scripts/UI/ActivityJellyMotion.cs` | ActivityJellyMotion |
| [CollectionFlightMotion](recipes/ui-battlejellyart-collectionflightmotion.md) | `Assets/Scripts/UI/BattleJellyArt.cs` | RewardFlight |
| [CollectionGoalEntrance](recipes/ui-collectiongoalentrance-collectiongoalentrance.md) | `Assets/Scripts/UI/CollectionGoalEntrance.cs` | CollectionGoalEntrance |
| [EncouragementDrag](recipes/ui-encouragementdrag-encouragementdrag.md) | `Assets/Scripts/UI/EncouragementDrag.cs` | EncouragementDrag |
| [FailureCatAnimation](recipes/ui-failurecatanimation-failurecatanimation.md) | `Assets/Scripts/UI/FailureCatAnimation.cs` | FrameSequence |
| [FailurePresentation](recipes/ui-failurepresentation-failurepresentation.md) | `Assets/Scripts/UI/FailurePresentation.cs` | FailurePresentation |
| [HomeWordmarkWiggle](recipes/ui-homewordmarkwiggle-homewordmarkwiggle.md) | `Assets/Scripts/UI/HomeWordmarkWiggle.cs` | HomeWordmarkWiggle |
| [JellyGlyph](recipes/ui-jellylogointro-jellyglyph.md) | `Assets/Scripts/UI/JellyLogoIntro.cs` | JellyGlyph |
| [JellyLogoIntro](recipes/ui-jellylogointro-jellylogointro.md) | `Assets/Scripts/UI/JellyLogoIntro.cs` | JellyLogoIntro |
| [LeagueMedalPresentation](recipes/ui-leaguemotionart-leaguemedalpresentation.md) | `Assets/Scripts/UI/LeagueMotionArt.cs` | LeagueMedalPresentation |
| [LoadingPresentation](recipes/ui-loadingpresentation-loadingpresentation.md) | `Assets/Scripts/UI/LoadingPresentation.cs` | LoadingPresentation |
| [ResultAudioTimeline](recipes/ui-resultaudiotimeline-resultaudiotimeline.md) | `Assets/Scripts/UI/ResultAudioTimeline.cs` | ResultAudioTimeline |
| [SettingsSwitch77](recipes/ui-settingsswitch77-settingsswitch77.md) | `Assets/Scripts/UI/SettingsSwitch77.cs` | SettingsSwitch77 |
| [StarChargeView](recipes/ui-starchargeview-starchargeview.md) | `Assets/Scripts/UI/StarChargeView.cs` | StarChargeView |
| [UIBounce](recipes/ui-uibounce-uibounce.md) | `Assets/Scripts/UI/UIBounce.cs` | UIBounce |
| [UIFloat](recipes/ui-uicraft-uifloat.md) | `Assets/Scripts/UI/UICraft.cs` | UIFloat |
| [VictoryCatAnimation](recipes/ui-victorycatanimation-victorycatanimation.md) | `Assets/Scripts/UI/VictoryCatAnimation.cs` | FrameSequence |
| [VictoryPresentation](recipes/ui-victorypresentation-victorypresentation.md) | `Assets/Scripts/UI/VictoryPresentation.cs` | VictoryPresentation |
| [VictoryPresentation](recipes/ui-victorypresentation-stars-victorypresentation.md) | `Assets/Scripts/UI/VictoryPresentation.Stars.cs` | VictoryPresentation |
| [JellyMotion](recipes/world-jellymotion-jellymotion.md) | `Assets/Scripts/World/JellyMotion.cs` | JellyHit |

## test-fixture · 115

| 条目 | 源文件 | 交付 |
|---|---|---|
| [AlphabetPhysicsSurvey](recipes/alphabetphysicssurvey-alphabetphysicssurvey.md) | `Assets/Scripts/AlphabetPhysicsSurvey.cs` | source-evidence-only |
| [BalanceStatisticsQA](recipes/balancestatisticsqa-balancestatisticsqa.md) | `Assets/Scripts/BalanceStatisticsQA.cs` | source-evidence-only |
| [BannerJellyQA](recipes/bannerjellyqa-bannerjellyqa.md) | `Assets/Scripts/BannerJellyQA.cs` | source-evidence-only |
| [BattleJellyQA](recipes/battlejellyqa-battlejellyqa.md) | `Assets/Scripts/BattleJellyQA.cs` | source-evidence-only |
| [CampaignSettlement020QA](recipes/campaignsettlement020qa-campaignsettlement020qa.md) | `Assets/Scripts/CampaignSettlement020QA.cs` | source-evidence-only |
| [CatCosmeticsQA](recipes/catcosmeticsqa-catcosmeticsqa.md) | `Assets/Scripts/CatCosmeticsQA.cs` | source-evidence-only |
| [CatFlowQA](recipes/catflowqa-catflowqa.md) | `Assets/Scripts/CatFlowQA.cs` | source-evidence-only |
| [Celebration020QA](recipes/celebration020qa-celebration020qa.md) | `Assets/Scripts/Celebration020QA.cs` | source-evidence-only |
| [ChainCollectionQA](recipes/chaincollectionqa-chaincollectionqa.md) | `Assets/Scripts/ChainCollectionQA.cs` | source-evidence-only |
| [ChainDifficultyResearch](recipes/chaindifficultyresearch-chaindifficultyresearch.md) | `Assets/Scripts/ChainDifficultyResearch.cs` | source-evidence-only |
| [ChainGravityQA](recipes/chaingravityqa-chaingravityqa.md) | `Assets/Scripts/ChainGravityQA.cs` | source-evidence-only |
| [ChainGuardQA](recipes/chainguardqa-chainguardqa.md) | `Assets/Scripts/ChainGuardQA.cs` | source-evidence-only |
| [ChainPowerQA](recipes/chainpowerqa-chainpowerqa.md) | `Assets/Scripts/ChainPowerQA.cs` | source-evidence-only |
| [ChainPresentationQA](recipes/chainpresentationqa-chainpresentationqa.md) | `Assets/Scripts/ChainPresentationQA.cs` | source-evidence-only |
| [ChainRatingQA](recipes/chainratingqa-chainratingqa.md) | `Assets/Scripts/ChainRatingQA.cs` | source-evidence-only |
| [ChainRealtimeQA](recipes/chainrealtimeqa-chainrealtimeqa.md) | `Assets/Scripts/ChainRealtimeQA.cs` | source-evidence-only |
| [ChainRefillV39QA](recipes/chainrefillv39qa-chainrefillv39qa.md) | `Assets/Scripts/ChainRefillV39QA.cs` | source-evidence-only |
| [ChainVisualQA](recipes/chainvisualqa-chainvisualqa.md) | `Assets/Scripts/ChainVisualQA.cs` | source-evidence-only |
| [ChainVolleyGravityQA](recipes/chainvolleygravityqa-chainvolleygravityqa.md) | `Assets/Scripts/ChainVolleyGravityQA.cs` | source-evidence-only |
| [ChapterArtPreview](recipes/chapterartpreview-chapterartpreview.md) | `Assets/Scripts/ChapterArtPreview.cs` | source-evidence-only |
| [ChineseCheerArtQA](recipes/chinesecheerartqa-chinesecheerartqa.md) | `Assets/Scripts/ChineseCheerArtQA.cs` | source-evidence-only |
| [ChineseV60OutcomeQA](recipes/chinesev60outcomeqa-chinesev60outcomeqa.md) | `Assets/Scripts/ChineseV60OutcomeQA.cs` | source-evidence-only |
| [CollectionQA](recipes/collectionqa-collectionqa.md) | `Assets/Scripts/CollectionQA.cs` | source-evidence-only |
| [CollisionHapticsQA](recipes/collisionhapticsqa-collisionhapticsqa.md) | `Assets/Scripts/CollisionHapticsQA.cs` | source-evidence-only |
| [CombatFeedback79QA](recipes/combatfeedback79qa-combatfeedback79qa.md) | `Assets/Scripts/CombatFeedback79QA.cs` | source-evidence-only |
| [CookieColliderGravityQA](recipes/cookiecollidergravityqa-cookiecollidergravityqa.md) | `Assets/Scripts/CookieColliderGravityQA.cs` | source-evidence-only |
| [CopperNumberQA](recipes/coppernumberqa-coppernumberqa.md) | `Assets/Scripts/CopperNumberQA.cs` | source-evidence-only |
| [EconomyV67QA](recipes/economyv67qa-economyv67qa.md) | `Assets/Scripts/EconomyV67QA.cs` | source-evidence-only |
| [EncouragementDragQA](recipes/encouragementdragqa-encouragementdragqa.md) | `Assets/Scripts/EncouragementDragQA.cs` | source-evidence-only |
| [EncouragementQA](recipes/encouragementqa-encouragementqa.md) | `Assets/Scripts/EncouragementQA.cs` | source-evidence-only |
| [FailureQA](recipes/failureqa-failureqa.md) | `Assets/Scripts/FailureQA.cs` | source-evidence-only |
| [FlightQA](recipes/flightqa-flightqa.md) | `Assets/Scripts/FlightQA.cs` | source-evidence-only |
| [FlightV22QA](recipes/flightv22qa-flightv22qa.md) | `Assets/Scripts/FlightV22QA.cs` | source-evidence-only |
| [FontGoalLoopQA](recipes/fontgoalloopqa-fontgoalloopqa.md) | `Assets/Scripts/FontGoalLoopQA.cs` | source-evidence-only |
| [FourthGroupQA](recipes/fourthgroupqa-fourthgroupqa.md) | `Assets/Scripts/FourthGroupQA.cs` | source-evidence-only |
| [GardenBackgroundQA](recipes/gardenbackgroundqa-gardenbackgroundqa.md) | `Assets/Scripts/GardenBackgroundQA.cs` | source-evidence-only |
| [GroupRefill82QA](recipes/grouprefill82qa-grouprefill82qa.md) | `Assets/Scripts/GroupRefill82QA.cs` | source-evidence-only |
| [HomeBannerQA](recipes/homebannerqa-homebannerqa.md) | `Assets/Scripts/HomeBannerQA.cs` | source-evidence-only |
| [HomeFeedbackQA](recipes/homefeedbackqa-homefeedbackqa.md) | `Assets/Scripts/HomeFeedbackQA.cs` | source-evidence-only |
| [ImmediateSpecialIntro020QA](recipes/immediatespecialintro020qa-immediatespecialintro020qa.md) | `Assets/Scripts/ImmediateSpecialIntro020QA.cs` | source-evidence-only |
| [InGameFeedbackQA](recipes/ingamefeedbackqa-ingamefeedbackqa.md) | `Assets/Scripts/InGameFeedbackQA.cs` | source-evidence-only |
| [IngotFxQA](recipes/ingotfxqa-ingotfxqa.md) | `Assets/Scripts/IngotFxQA.cs` | source-evidence-only |
| [JellyBalanceR2QA](recipes/jellybalancer2qa-jellybalancer2qa.md) | `Assets/Scripts/JellyBalanceR2QA.cs` | source-evidence-only |
| [JellyBrickQA](recipes/jellybrickqa-jellybrickqa.md) | `Assets/Scripts/JellyBrickQA.cs` | source-evidence-only |
| [JellyJourney80Preview](recipes/jellyjourney80qa-jellyjourney80preview.md) | `Assets/Scripts/JellyJourney80QA.cs` | source-evidence-only |
| [JellyJourney80QA](recipes/jellyjourney80qa-jellyjourney80qa.md) | `Assets/Scripts/JellyJourney80QA.cs` | source-evidence-only |
| [JellyRibbonTexture80QA](recipes/jellyribbontexture80qa-jellyribbontexture80qa.md) | `Assets/Scripts/JellyRibbonTexture80QA.cs` | source-evidence-only |
| [JellyV40QA](recipes/jellyv40qa-jellyv40qa.md) | `Assets/Scripts/JellyV40QA.cs` | source-evidence-only |
| [JellyWallQA](recipes/jellywallqa-jellywallqa.md) | `Assets/Scripts/JellyWallQA.cs` | source-evidence-only |
| [LeagueCalendarPreview](recipes/leaguecalendarpreview-leaguecalendarpreview.md) | `Assets/Scripts/LeagueCalendarPreview.cs` | source-evidence-only |
| [LeagueCalendarQA](recipes/leaguecalendarqa-leaguecalendarqa.md) | `Assets/Scripts/LeagueCalendarQA.cs` | source-evidence-only |
| [LeagueFontPreview](recipes/leaguefontpreview-leaguefontpreview.md) | `Assets/Scripts/LeagueFontPreview.cs` | source-evidence-only |
| [LeagueQA](recipes/leagueqa-leagueqa.md) | `Assets/Scripts/LeagueQA.cs` | source-evidence-only |
| [LeagueV66QA](recipes/leaguev66qa-leaguev66qa.md) | `Assets/Scripts/LeagueV66QA.cs` | source-evidence-only |
| [LeagueV79QA](recipes/leaguev79qa-leaguev79qa.md) | `Assets/Scripts/LeagueV79QA.cs` | source-evidence-only |
| [FusionCoreQA](recipes/leveleditor-fusioncoreqa-fusioncoreqa.md) | `Assets/Scripts/LevelEditor/FusionCoreQA.cs` | source-evidence-only |
| [FusionDeviceQA](recipes/leveleditor-fusiondeviceqa-fusiondeviceqa.md) | `Assets/Scripts/LevelEditor/FusionDeviceQA.cs` | source-evidence-only |
| [FusionFlowQA](recipes/leveleditor-fusionflowqa-fusionflowqa.md) | `Assets/Scripts/LevelEditor/FusionFlowQA.cs` | source-evidence-only |
| [FusionQA](recipes/leveleditor-fusionqa-fusionqa.md) | `Assets/Scripts/LevelEditor/FusionQA.cs` | source-evidence-only |
| [FusionRearQA](recipes/leveleditor-fusionrearqa-fusionrearqa.md) | `Assets/Scripts/LevelEditor/FusionRearQA.cs` | source-evidence-only |
| [FusionSuperRangeQA](recipes/leveleditor-fusionsuperrangeqa-fusionsuperrangeqa.md) | `Assets/Scripts/LevelEditor/FusionSuperRangeQA.cs` | source-evidence-only |
| [BalanceFirePolicy](recipes/leveleditor-levelbalancepolicy-balancefirepolicy.md) | `Assets/Scripts/LevelEditor/LevelBalancePolicy.cs` | source-evidence-only |
| [LevelBalanceRunner](recipes/leveleditor-levelbalancerunner-levelbalancerunner.md) | `Assets/Scripts/LevelEditor/LevelBalanceRunner.cs` | source-evidence-only |
| [LevelEditorApp](recipes/leveleditor-leveleditorapp-leveleditorapp.md) | `Assets/Scripts/LevelEditor/LevelEditorApp.cs` | source-evidence-only |
| [LevelEditorPlaytest](recipes/leveleditor-leveleditorplaytest-leveleditorplaytest.md) | `Assets/Scripts/LevelEditor/LevelEditorPlaytest.cs` | source-evidence-only |
| [OpeningBalanceChecks](recipes/leveleditor-openingbalancechecks-openingbalancechecks.md) | `Assets/Scripts/LevelEditor/OpeningBalanceChecks.cs` | source-evidence-only |
| [LevelEntry88QA](recipes/levelentry88qa-levelentry88qa.md) | `Assets/Scripts/LevelEntry88QA.cs` | source-evidence-only |
| [LevelFiveSettlementQA](recipes/levelfivesettlementqa-levelfivesettlementqa.md) | `Assets/Scripts/LevelFiveSettlementQA.cs` | source-evidence-only |
| [LifecycleQA](recipes/lifecycleqa-lifecycleqa.md) | `Assets/Scripts/LifecycleQA.cs` | source-evidence-only |
| [LoadingQA](recipes/loadingqa-loadingqa.md) | `Assets/Scripts/LoadingQA.cs` | source-evidence-only |
| [Mobile73QA](recipes/mobile73qa-mobile73qa.md) | `Assets/Scripts/Mobile73QA.cs` | source-evidence-only |
| [MobileFrameStats](recipes/mobileframestats-mobileframestats.md) | `Assets/Scripts/MobileFrameStats.cs` | source-evidence-only |
| [MobileV54QA](recipes/mobilev54qa-mobilev54qa.md) | `Assets/Scripts/MobileV54QA.cs` | source-evidence-only |
| [MobileV55HandoffQA](recipes/mobilev55handoffqa-mobilev55handoffqa.md) | `Assets/Scripts/MobileV55HandoffQA.cs` | source-evidence-only |
| [MobileV55StressQA](recipes/mobilev55stressqa-mobilev55stressqa.md) | `Assets/Scripts/MobileV55StressQA.cs` | source-evidence-only |
| [MobileV56BrickQA](recipes/mobilev56brickqa-mobilev56brickqa.md) | `Assets/Scripts/MobileV56BrickQA.cs` | source-evidence-only |
| [MobileV57InputQA](recipes/mobilev57inputqa-mobilev57inputqa.md) | `Assets/Scripts/MobileV57InputQA.cs` | source-evidence-only |
| [MobileV58OutcomeQA](recipes/mobilev58outcomeqa-mobilev58outcomeqa.md) | `Assets/Scripts/MobileV58OutcomeQA.cs` | source-evidence-only |
| [OnboardingQA](recipes/onboardingqa-onboardingqa.md) | `Assets/Scripts/OnboardingQA.cs` | source-evidence-only |
| [Opening81QA](recipes/opening81qa-opening81qa.md) | `Assets/Scripts/Opening81QA.cs` | source-evidence-only |
| [OpeningEffectsQA](recipes/openingeffectsqa-openingeffectsqa.md) | `Assets/Scripts/OpeningEffectsQA.cs` | source-evidence-only |
| [OptimizationV76QA](recipes/optimizationv76qa-optimizationv76qa.md) | `Assets/Scripts/OptimizationV76QA.cs` | source-evidence-only |
| [PackageCleanupQA](recipes/packagecleanupqa-packagecleanupqa.md) | `Assets/Scripts/PackageCleanupQA.cs` | source-evidence-only |
| [PastelQA](recipes/pastelqa-pastelqa.md) | `Assets/Scripts/PastelQA.cs` | source-evidence-only |
| [AppUI](recipes/prefetchreuse020qa-appui.md) | `Assets/Scripts/PrefetchReuse020QA.cs` | source-evidence-only |
| [PrefetchReuse020QA](recipes/prefetchreuse020qa-prefetchreuse020qa.md) | `Assets/Scripts/PrefetchReuse020QA.cs` | source-evidence-only |
| [GameSession](recipes/refill80qa-gamesession.md) | `Assets/Scripts/Refill80QA.cs` | source-evidence-only |
| [Refill80QA](recipes/refill80qa-refill80qa.md) | `Assets/Scripts/Refill80QA.cs` | source-evidence-only |
| [RefillColumnsQA](recipes/refillcolumnsqa-refillcolumnsqa.md) | `Assets/Scripts/RefillColumnsQA.cs` | source-evidence-only |
| [RefillV63QA](recipes/refillv63qa-refillv63qa.md) | `Assets/Scripts/RefillV63QA.cs` | source-evidence-only |
| [ResultNoSplashF4QA](recipes/resultnosplashf4qa-resultnosplashf4qa.md) | `Assets/Scripts/ResultNoSplashF4QA.cs` | source-evidence-only |
| [Round4Audit](recipes/round4audit-round4audit.md) | `Assets/Scripts/Round4Audit.cs` | source-evidence-only |
| [Round5Demo](recipes/round5demo-round5demo.md) | `Assets/Scripts/Round5Demo.cs` | source-evidence-only |
| [Round6Demo](recipes/round6demo-round6demo.md) | `Assets/Scripts/Round6Demo.cs` | source-evidence-only |
| [SecondGroupIntegrationQA](recipes/secondgroupintegrationqa-secondgroupintegrationqa.md) | `Assets/Scripts/SecondGroupIntegrationQA.cs` | source-evidence-only |
| [SecondGroupPreview](recipes/secondgroupintegrationqa-secondgrouppreview.md) | `Assets/Scripts/SecondGroupIntegrationQA.cs` | source-evidence-only |
| [SettingsCPreview](recipes/settingscpreview-settingscpreview.md) | `Assets/Scripts/SettingsCPreview.cs` | source-evidence-only |
| [SettingsCQA](recipes/settingscqa-settingscqa.md) | `Assets/Scripts/SettingsCQA.cs` | source-evidence-only |
| [Settlement82QA](recipes/settlement82qa-settlement82qa.md) | `Assets/Scripts/Settlement82QA.cs` | source-evidence-only |
| [SevenSpecial80QA](recipes/sevenspecial80qa-sevenspecial80qa.md) | `Assets/Scripts/SevenSpecial80QA.cs` | source-evidence-only |
| [SfxV87QA](recipes/sfxv87qa-sfxv87qa.md) | `Assets/Scripts/SfxV87QA.cs` | source-evidence-only |
| [SpecialGenerationAudit](recipes/specialgenerationaudit-specialgenerationaudit.md) | `Assets/Scripts/SpecialGenerationAudit.cs` | source-evidence-only |
| [StreakRewardV74QA](recipes/streakrewardv74qa-streakrewardv74qa.md) | `Assets/Scripts/StreakRewardV74QA.cs` | source-evidence-only |
| [StreakV61QA](recipes/streakv61qa-streakv61qa.md) | `Assets/Scripts/StreakV61QA.cs` | source-evidence-only |
| [SystemsV64QA](recipes/systemsv64qa-systemsv64qa.md) | `Assets/Scripts/SystemsV64QA.cs` | source-evidence-only |
| [ThirdGroupQA](recipes/thirdgroupqa-thirdgroupqa.md) | `Assets/Scripts/ThirdGroupQA.cs` | source-evidence-only |
| [VictoryFinalePreview](recipes/victoryfinaleqa-victoryfinalepreview.md) | `Assets/Scripts/VictoryFinaleQA.cs` | source-evidence-only |
| [VictoryFinaleQA](recipes/victoryfinaleqa-victoryfinaleqa.md) | `Assets/Scripts/VictoryFinaleQA.cs` | source-evidence-only |
| [VictoryQA](recipes/victoryqa-victoryqa.md) | `Assets/Scripts/VictoryQA.cs` | source-evidence-only |
| [VictoryRace020QA](recipes/victoryrace020qa-victoryrace020qa.md) | `Assets/Scripts/VictoryRace020QA.cs` | source-evidence-only |
| [VictorySpeedR3QA](recipes/victoryspeedr3qa-victoryspeedr3qa.md) | `Assets/Scripts/VictorySpeedR3QA.cs` | source-evidence-only |
| [WeChatAdaptation87QA](recipes/wechatadaptation87qa-wechatadaptation87qa.md) | `Assets/Scripts/WeChatAdaptation87QA.cs` | source-evidence-only |
| [DemoRecorderBootstrap](recipes/windowsdemorecorder-demorecorderbootstrap.md) | `Assets/Scripts/WindowsDemoRecorder.cs` | source-evidence-only |
| [WindowsDemoRecorder](recipes/windowsdemorecorder-windowsdemorecorder.md) | `Assets/Scripts/WindowsDemoRecorder.cs` | source-evidence-only |
| [YarnQA](recipes/yarnqa-yarnqa.md) | `Assets/Scripts/YarnQA.cs` | source-evidence-only |
