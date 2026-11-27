# AppUI · AppUI

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.cs`
文件 SHA-256：`01d6a79a5fb77b7488867b897ebb97b392e85a23b80b4c0562b2f9f9418f3ebe`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `PageSlide` / `pagenav`：仅提取0.27秒OutCubic页面轨道；等价归一化曲线替代DOTween宿主序列；导航、输入和路由归宿主
- `PageTransition` / `pageflow`：手工抽取纯表现接口；保留时序，业务事件由宿主输入

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### ShowPage · L106

方法 SHA-256：`86f40860daa3589b730aff5a922f681b228ced925e1e0d47d9047ac3192016db`
时间来源：SetUpdate(true)
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.AppUI.ShowPage(string, bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.Chapters.cs:7` · `AppUI.BuildChapters` → `ShowPage("home")`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:49` · `AppUI.CancelLoadingToHome` → `ShowPage("home",false)`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:69` · `AppUI.PrepareMenuContentRoutine` → `ShowPage(page,animate)`
- `Assets/Scripts/UI/AppUI.cs:32` · `AppUI.Awake` → `ShowPage("home",false)`
- `Assets/Scripts/UI/AppUI.cs:92` · `AppUI.Update` → `ShowPage("home")`
- `Assets/Scripts/UI/AppUI.cs:115` · `AppUI.QAStaticNavigation` → `ShowPage(page,false)`
- `Assets/Scripts/UI/AppUI.cs:122` · `AppUI.TopBar` → `ShowPage("home")`
- `Assets/Scripts/UI/AppUI.cs:152` · `AppUI.OpenShopCategory` → `ShowPage("shop")`
- `Assets/Scripts/UI/AppUI.cs:154` · `AppUI.BuildRanking` → `ShowPage("home")`
- `Assets/Scripts/UI/AppUI.cs:230` · `AppUI.ReturnHome` → `ShowPage(returnToTree?"chapters":"home",false)`
- `Assets/Scripts/UI/AppUI.cs:250` · `AppUI.QANavigate` → `ShowPage(page,false)`
- `Assets/Scripts/UI/AppUI.DressingShop044.cs:42` · `AppUI.OpenLeagueCosmetics044` → `ShowPage("league-prizes")`
- `Assets/Scripts/UI/AppUI.DressingShop044.cs:44` · `AppUI.OpenDressing` → `ShowPage("dressing")`
- `Assets/Scripts/UI/AppUI.League046.cs:12` · `AppUI.QARebuildLeague046` → `ShowPage("ranking",false)`
- `Assets/Scripts/UI/AppUI.League046.cs:13` · `AppUI.NavigateLeague046` → `ShowPage("chapters")`
- `Assets/Scripts/UI/AppUI.League046.cs:13` · `AppUI.NavigateLeague046` → `ShowPage("home")`
- `Assets/Scripts/UI/AppUI.League046.cs:13` · `AppUI.NavigateLeague046` → `ShowPage("ranking")`
- `Assets/Scripts/UI/AppUI.League046.cs:18` · `AppUI.OpenLeagueDetail046` → `ShowPage("league-detail",false)`
- `Assets/Scripts/UI/AppUI.League046.cs:19` · `AppUI.BuildLeagueLocked046` → `ShowPage("home")`
- `Assets/Scripts/UI/AppUI.League046.cs:28` · `AppUI.DrawLeagueDetail046` → `ShowPage("ranking")`
- `Assets/Scripts/UI/AppUI.League046.cs:34` · `AppUI.DrawRecords046` → `ShowPage("ranking")`
- `Assets/Scripts/UI/AppUI.ShopDetails045.cs:12` · `AppUI.OpenDetail045` → `ShowPage("shop-detail",false)`
- `Assets/Scripts/UI/HomeANavigation.cs:36` · `AppUI.BuildApprovedNavigation` → `ShowPage("chapters")`
- `Assets/Scripts/UI/HomeANavigation.cs:36` · `AppUI.BuildApprovedNavigation` → `ShowPage("shop")`
- `Assets/Scripts/UI/HomeANavigation.cs:36` · `AppUI.BuildApprovedNavigation` → `ShowPage("home")`
- `Assets/Scripts/UI/HomeANavigation.cs:36` · `AppUI.BuildApprovedNavigation` → `ShowPage("ranking")`

```csharp
void ShowPage(string page,bool animate=true){CloseGoalCelebration();CloseCollectionGoalEntrance();if(!GameContent.PageReady(page)){PrepareMenuContent(page,animate);return;}CancelContentLoad();SoundService.CancelTransient();if(busy&&playing)return;string previousPage=CurrentPage;CloseAlphabetIntroduction();CancelAim();RemoveModal(false);viewGeneration++;if(resultSequence!=null){StopCoroutine(resultSequence);resultSequence=null;}skipResult=null;victory=null;results=null;Metrics?.Finish("abandoned");playing=false;boardLayout=false;Session.StopLevel();ApplySafeArea();Clear(hudRoot);Clear(overlayRoot);RecycleCoins();Clear(effectsRoot);KillNavigation();navigationFromPage=previousPage;CurrentPage=page;SoundService.Music("bgm_home");
  if(!pages.TryGetValue(page,out var next)||next==null){next=UICraft.Root(pageTrack,page);next.gameObject.AddComponent<RectMask2D>();next.gameObject.AddComponent<CanvasGroup>();pages[page]=next;switch(page){case "chapters":BuildChapters(next);break;case "shop":BuildShop(next);break;case "shop-detail":break;case "league-detail":break;case "dressing":BuildDressingPage044(next);break;case "league-prizes":BuildLeagueCosmetics044(next);break;case "ranking":BuildRanking(next,false);break;case "vip":BuildRanking(next,true);break;default:BuildHome(next);break;}}
  foreach(var pair in pages)pair.Value.anchoredPosition=new Vector2(PageOrder(pair.Key)*pageRoot.rect.width,0);currentPage=next;if(page=="league-detail")DrawLeagueDetail046(next);LayoutMenuPage(next,page);if(page=="home"){feedback.Visit();RefreshHomeFeedback(false);feedbackRefreshPending=true;}
  bottomFill.gameObject.SetActive(true);rankingBackdrop.gameObject.SetActive(page=="ranking"||page=="vip"||page=="shop");headerSlot.gameObject.SetActive(true);ShowApprovedNavigation(page);TopBar(headerSlot,page=="home");float target=-PageOrder(page)*pageRoot.rect.width;int version=routeVersion;busy=false;
  if(animate&&Mathf.Abs(pageTrack.anchoredPosition.x-target)>.1f){SoundService.Play("click");float slideTarget=pageTrack.anchoredPosition.x+Mathf.Sign(target-pageTrack.anchoredPosition.x)*pageRoot.rect.width;next.anchoredPosition=new Vector2(-slideTarget,0);navigation=DOTween.Sequence().SetUpdate(true).SetId(this);navigation.Append(DOTween.To(()=>pageTrack.anchoredPosition,v=>pageTrack.anchoredPosition=v,new Vector2(slideTarget,0),.27f).SetEase(Ease.OutCubic));navigation.OnComplete(()=>{if(version!=routeVersion)return;PlaceMenuPages();pageTrack.anchoredPosition=new Vector2(target,0);navigation=null;SetPageInput();});}else pageTrack.anchoredPosition=new Vector2(target,0);SetPageInput();RefreshWallet();if(page=="ranking"||page=="vip")next.GetComponentInChildren<LeagueRankingView>()?.Enter();if(page=="home"){SoundService.Play("cat_home_greeting");}if(page=="home"||page=="chapters")StartIdleContentPrefetch();Debug.Log("BOUNCE_PAGE_READY "+page);
 }
```

### StartIntroWhenVisible · L134

方法 SHA-256：`7f3284345f8b6b8c37eb93a21e00f8e6cffc2f52290b7ff7fa77f47da9272302`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.StartIntroWhenVisible(UnityEngine.RectTransform, int, bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:132` · `AppUI.ShowIntro` → `StartIntroWhenVisible(intro,generation,autoDismiss)`

```csharp
IEnumerator StartIntroWhenVisible(RectTransform intro,int generation,bool autoDismiss){
 while(!UnityEngine.Rendering.SplashScreen.isFinished){if(generation!=viewGeneration||intro==null)yield break;yield return null;}
 // Present the local courtyard before the first animation tick, including on a slow first frame.
 #if UNITY_EDITOR
  if(Application.isBatchMode)yield return null;else yield return new WaitForEndOfFrame();
  #else
  yield return new WaitForEndOfFrame();
  #endif
  yield return null;
 if(generation!=viewGeneration||intro==null)yield break;
 var presentation=intro.gameObject.AddComponent<JellyLogoIntro>();presentation.Begin();
 Debug.Log("INTRO_VISIBLE_START splashFinished="+UnityEngine.Rendering.SplashScreen.isFinished+" realtime="+Time.realtimeSinceStartup);
 if(autoDismiss)StartCoroutine(DismissIntro(intro,generation));
 }
```

### DismissIntro · L148

方法 SHA-256：`3e417b506da0d108f85845262c157fe2589e2b6a77e37bd402fc43267cfc21f6`
时间来源：Time.unscaledDeltaTime, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.DismissIntro(UnityEngine.RectTransform, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:146` · `AppUI.StartIntroWhenVisible` → `DismissIntro(intro,generation)`

```csharp
IEnumerator DismissIntro(RectTransform intro,int generation){yield return new WaitForSecondsRealtime(JellyLogoIntro.HoldDuration);if(generation!=viewGeneration||intro==null)yield break;
 var presentation=intro.GetComponent<JellyLogoIntro>();while(presentation!=null&&!presentation.CanDismiss){if(generation!=viewGeneration||intro==null)yield break;yield return null;}
 var group=intro.gameObject.AddComponent<CanvasGroup>();float t=0;while(t<JellyLogoIntro.FadeDuration){if(generation!=viewGeneration||intro==null)yield break;t+=Time.unscaledDeltaTime;group.alpha=1-Mathf.Clamp01(t/JellyLogoIntro.FadeDuration);yield return null;}if(intro!=null)Destroy(intro.gameObject);
 }
```

### StartPreparedLevel · L156

方法 SHA-256：`47e78df56fd9757d0e8a3d3209f8abac958e736faeec7b224bed9ed0fe8b6a74`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.StartPreparedLevel(UnityEngine.RectTransform, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.ContentLoading.cs:53` · `AppUI.RunLevelLoad` → `StartPreparedLevel(loading,generation)`

```csharp
IEnumerator StartPreparedLevel(RectTransform loading,int generation){
  // Present the overlay before synchronous level preparation, then start its visible clock.
  #if UNITY_EDITOR
  if(Application.isBatchMode)yield return null;else yield return new WaitForEndOfFrame();
  #else
  yield return new WaitForEndOfFrame();
  #endif
  yield return null;
  if(generation!=viewGeneration||loading==null)yield break;
  Session.StopLevel();yield return null;
  yield return GameContent.PrepareLevel(currentLevel);
  if(generation!=viewGeneration||loading==null)yield break;
  loading.GetComponent<LoadingPresentation>().SetProgress(.7f);
  yield return null;
  yield return Session.PrepareSession(currentLevel,false,streakTrial);
  if(generation!=viewGeneration||loading==null)yield break;
  SoundService.Music("bgm_gameplay");Session.BoosterProfile=Profile;Session.SetPaused(true);
  progressionRun=streakTrial<0&&currentLevel>=Profile.AlphabetUnlocked;feedback=savedFeedback;if(streakTrial<0)feedback.StartLevel();
  boardLayout=true;gameCamera=Camera.main;ApplySafeArea();
  var presentation=loading.GetComponent<LoadingPresentation>();presentation.Complete();
  while(!presentation.CanDismiss){
   if(generation!=viewGeneration||loading==null)yield break;
   yield return null;
  }
  if(generation!=viewGeneration||loading==null)yield break;
  if(currentPage!=null){Destroy(currentPage.gameObject);currentPage=null;}
  Destroy(loading.gameObject);busy=false;playing=true;CurrentPage="battle";
  BuildBattleHUD();RefreshHUD();if(contentLoad!=null)contentLoad.PreparationComplete=true;StartCoroutine(PrefetchFollowingLevel(currentLevel,generation));Session.SetPaused(true);yield return ShowCollectionGoalEntrance(generation);if(generation!=viewGeneration)yield break;if(streakTrial<0)yield return ShowAlphabetIntroductions(generation);if(generation!=viewGeneration)yield break;if(generation!=viewGeneration)yield break;if(generation!=viewGeneration)yield break;if(Session.ApplyCampaignStreakBonus())yield return ShowCampaignStreakReward(generation);if(generation!=viewGeneration)yield break;Session.SetPaused(false);Metrics?.BeginRound(currentLevel);ShowBoosterSelectionGuide();SoundService.Play("click"); }
```

### HideTutorialLater · L186

方法 SHA-256：`67d045ad35376576ea0166f9466a29f00d4ee33cbcf8eb3ee736284bc01bb8ba`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.HideTutorialLater(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:185` · `AppUI.ShowTutorial` → `HideTutorialLater(viewGeneration)`

```csharp
IEnumerator HideTutorialLater(int generation){yield return new WaitForSecondsRealtime(3.4f);if(generation==viewGeneration&&tutorialStep==2)ShowTutorial(0);}
```

### OpenModalMotion · L207

方法 SHA-256：`44406ca2dd200af83affbf444aa1d6b91e8d407f3660d45ad9df3f4cb8692c6f`
时间来源：SetUpdate(true)
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.AppUI.OpenModalMotion()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:206` · `AppUI.OpenModal` → `OpenModalMotion()`
- `Assets/Scripts/UI/AppUI.SettingsC.cs:24` · `AppUI.OpenSettingsSheetC` → `OpenModalMotion()`

```csharp
void OpenModalMotion(){var group=modal.gameObject.AddComponent<CanvasGroup>();group.alpha=0;modal.localScale=Vector3.one*.94f;modalMotion=DOTween.Sequence().SetUpdate(true).SetId(modal);modalMotion.Append(modal.DOScale(1,.21f).SetEase(Ease.OutCubic));modalMotion.Join(DOTween.To(()=>group.alpha,v=>group.alpha=v,1,.21f));SoundService.Play("whoosh");}
```

### CloseModal · L208

方法 SHA-256：`efab336b25623a836d0cdb373c2a9f0d4377689e75f50c884d3715776c74baeb`
时间来源：SetUpdate(true)
曲线证据：Ease.InCubic
解析符号：`BounceGarden.AppUI.CloseModal()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:92` · `AppUI.Update` → `CloseModal()`

```csharp
void CloseModal(){if(modal==null)return;SoundService.Play("ui_back");modalMotion?.Kill(false);var old=modal;var group=old.GetComponent<CanvasGroup>();group.blocksRaycasts=false;modalMotion=DOTween.Sequence().SetUpdate(true).SetId(old);modalMotion.Append(old.DOScale(old.localScale*.94f,.14f).SetEase(Ease.InCubic));modalMotion.Join(DOTween.To(()=>group.alpha,v=>group.alpha=v,0,.14f));modalMotion.OnComplete(()=>{if(modal!=old)return;modalMotion=null;RemoveModal(true);});}
```

### QAWaitForLevel · L252

方法 SHA-256：`cbe2feebdaa3f1f03ea2fd1b5bdafa82c426830d632f6bc3af28c81b2ce45b7a`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.QAWaitForLevel()`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public IEnumerator QAWaitForLevel(){float deadline=Time.realtimeSinceStartup+30;do{yield return null;}while(overlayRoot.Find("Loading")!=null&&Time.realtimeSinceStartup<deadline);if(CurrentPage!="battle")throw new InvalidOperationException("Level failed to enter battle after loading.");}
```

## 对象层级和素材入口

- L107：`UICraft.Root(pageTrack,page)`
- L252：`overlayRoot.Find("Loading")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){CloseGoalCelebration();CloseCollectionGoalEntrance();CancelContentLoad();leagueFlowContent?.Cancel();CloseAlphabetIntroduction();modalMotion?.Kill();KillNavigation();if(Session!=null){Session.StateChanged-=RefreshHUD;Session.Outcome-=OnOutcome;Session.Feedback-=OnFeedback;}Profile?.Save();}
```

```csharp
void OnApplicationFocus(bool focus){leagueFocused=focus;if(!focus)FlushLeagueActivity(true);LeagueSource?.Read(focus);if(!focus){if(!playing)SnapNavigation();CancelAim();if(playing&&Session.IsPlaying&&results==null){Session.SetPaused(true);Pause();}}}
```

```csharp
void OnApplicationPause(bool pause){leagueFocused=!pause;if(pause)FlushLeagueActivity(true);if(!pause)LeagueSource?.Read(true);if(pause){Metrics?.Save();SoundService.SaveMix();CancelAim();Profile?.Save();if(playing&&Session.IsPlaying&&results==null)Pause();}}
```

```csharp
void KillNavigation(){routeVersion++;navigation?.Kill(false);navigation=null;}
```

```csharp
void CancelAim(){if(aiming){Metrics?.Balance?.StrategyInput("cancel",aimDirection);}aiming=false;pointerId=-999;if(Session!=null)Session.SetAim(null);}
```

```csharp
void Pause(){if(victoryCelebration!=null||victoryFinale!=null||victoryPreparation!=null||StreakRewardActive||AlphabetIntroActive||results!=null)return;BuildPlayerSettings();}
```

## 源字段与依赖

```csharp
RectTransform canvasRoot,safeRoot,designRoot,pageRoot,hudRoot,overlayRoot,effectsRoot,rankingBackdrop;
```
```csharp
RectTransform currentPage,modal,results,tutorial,walletTarget;
```
```csharp
TMP_Text walletText,shotsText,ballsText,targetText;
```
```csharp
VictoryPresentation victory;
```
```csharp
UnityEngine.UI.Button recallButton;
```
```csharp
TMP_Text recallText;
```
```csharp
bool boardLayout;
```
```csharp
float menuHeight=2340;
```
```csharp
Image bottomFill;
```
```csharp
RectTransform coinPoolRoot;
```
```csharp
readonly Stack<RectTransform> coinPool=new Stack<RectTransform>();
```
```csharp
readonly Dictionary<RectTransform,int> coinLeases=new Dictionary<RectTransform,int>();
```
```csharp
RectTransform pageTrack,headerSlot,navSlot;
```
```csharp
Sequence navigation,modalMotion;
```
```csharp
readonly Dictionary<string,RectTransform> pages=new Dictionary<string,RectTransform>();
```
```csharp
Vector2Int? qaViewport;
```
```csharp
Canvas canvas;
```
```csharp
Camera gameCamera;
```
```csharp
Rect? qaSafe;
```
```csharp
Rect lastSafe;
```
```csharp
Vector2Int lastSize;
```
```csharp
int routeVersion,viewGeneration,pointerId=-999,tutorialStep;
```
```csharp
int earnedWinCoins;
```
```csharp
string earnedWinNote="";
```
```csharp
bool busy,playing,aiming,won,qaResult,hasAdBonus,baseRewardSaved;
```
```csharp
Vector2 pointerStart,aimDirection;
```
```csharp
string resultKey;
```
```csharp
int currentLevel;
```
```csharp
Coroutine resultSequence;
```
```csharp
Action skipResult;
```
```csharp
ToastNotification0410 toast;
```
```csharp
readonly List<RaycastResult> uiHits=new List<RaycastResult>();
```
```csharp
Color purple=new Color32(98,65,155,255);
```
```csharp
string navigationFromPage;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
