# AppUI · AppUI

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.cs`
文件 SHA-256：`d3a8c4c850781f1a9e0e4dd2cab54ac95920856ba9b6cb331efd181c8f0183f4`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `PageSlide` / `pagenav`：仅提取0.27秒OutCubic页面轨道；等价归一化曲线替代DOTween宿主序列；导航、输入和路由归宿主

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### Update · L89

方法 SHA-256：`c6d84d3495f3e352115a6169ce34005b3e32732a2e59e66bf581a5df36591d35`
时间来源：Time.Now, Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(lastFeedbackDate!=DateTime.Now.Date){lastFeedbackDate=DateTime.Now.Date;savedFeedback.Visit();feedbackRefreshPending=true;}if(lastSafe!=ViewSafe||lastSize!=(qaViewport??new Vector2Int(Screen.width,Screen.height)))ApplySafeArea();RefreshWeChatHomeHeader();if(toast!=null&&Time.unscaledTime>toastUntil){Destroy(toast.transform.parent.gameObject);toast=null;}TickLeagueActivity();TickLeagueFlow();if(LeagueFlowActive)return;HandleInput();if(Input.GetKeyDown(KeyCode.Escape)){if(modal!=null)CloseModal();else if(playing&&results==null)Pause();else if(!playing&&CurrentPage!="home")ShowPage("home");}}
```

### ShowPage · L101

方法 SHA-256：`233bab1f75d93511f2109de389e0cae4d31ec9f725e4663a3252d8d86a32e9cc`
时间来源：SetUpdate(true)
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.AppUI.ShowPage(string, bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.Alphabet.cs:31` · `AppUI.OpenAlphabetStagePage` → `ShowPage("chapters")`
- `Assets/Scripts/UI/AppUI.Chapters.cs:5` · `AppUI.BuildChapterHome` → `ShowPage("chapters")`
- `Assets/Scripts/UI/AppUI.Chapters.cs:5` · `AppUI.BuildChapterHome` → `ShowPage("ranking")`
- `Assets/Scripts/UI/AppUI.Chapters.cs:5` · `AppUI.BuildChapterHome` → `ShowPage("shop")`
- `Assets/Scripts/UI/AppUI.Chapters.cs:7` · `AppUI.BuildChapters` → `ShowPage("home")`
- `Assets/Scripts/UI/AppUI.Chapters.cs:9` · `AppUI.QAOpenChapters` → `ShowPage("chapters",false)`
- `Assets/Scripts/UI/AppUI.Chapters.cs:10` · `AppUI.RefreshChapterHome` → `ShowPage("home",false)`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:52` · `AppUI.CancelLoadingToHome` → `ShowPage("home",false)`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:76` · `AppUI.PrepareMenuContentRoutine` → `ShowPage(page,animate)`
- `Assets/Scripts/UI/AppUI.cs:31` · `AppUI.Awake` → `ShowPage("home",false)`
- `Assets/Scripts/UI/AppUI.cs:89` · `AppUI.Update` → `ShowPage("home")`
- `Assets/Scripts/UI/AppUI.cs:108` · `AppUI.QANavigateAnimated` → `ShowPage(page,true)`
- `Assets/Scripts/UI/AppUI.cs:109` · `AppUI.QAStaticNavigation` → `ShowPage(page,false)`
- `Assets/Scripts/UI/AppUI.cs:116` · `AppUI.TopBar` → `ShowPage("home")`
- `Assets/Scripts/UI/AppUI.cs:145` · `AppUI.OpenShopCategory` → `ShowPage("shop")`
- `Assets/Scripts/UI/AppUI.cs:147` · `AppUI.BuildRanking` → `ShowPage("home")`
- `Assets/Scripts/UI/AppUI.cs:206` · `AppUI.ContentEnd` → `ShowPage("vip")`
- `Assets/Scripts/UI/AppUI.cs:207` · `AppUI.LevelPicker` → `ShowPage("chapters")`
- `Assets/Scripts/UI/AppUI.cs:225` · `AppUI.ReturnHome` → `ShowPage(returnToTree?"chapters":"home",false)`
- `Assets/Scripts/UI/AppUI.cs:245` · `AppUI.QANavigate` → `ShowPage(page,false)`
- `Assets/Scripts/UI/AppUI.Shop.cs:29` · `AppUI.DrawReferenceHeader` → `ShowPage("home")`
- `Assets/Scripts/UI/HomeFeedbackTestTools.cs:25` · `AppUI.PreviewFeedback` → `ShowPage("home",false)`
- `Assets/Scripts/UI/HomeFeedbackTestTools.cs:27` · `AppUI.UseSavedFeedback` → `ShowPage("home",false)`
- `Assets/Scripts/UI/HomeFeedbackTestTools.cs:39` · `AppUI.ResetLocalTestData` → `ShowPage("home",false)`

```csharp
void ShowPage(string page,bool animate=true){CloseCollectionGoalEntrance();if(!GameContent.PageReady(page)){PrepareMenuContent(page,animate);return;}CancelContentLoad();SoundService.CancelTransient();if(busy&&playing)return;CloseTutorialGuide();CloseAlphabetIntroduction();ClearPaintingCollection();CancelAim();RemoveModal(false);viewGeneration++;if(resultSequence!=null){StopCoroutine(resultSequence);resultSequence=null;}skipResult=null;victory=null;results=null;Metrics?.Finish("abandoned");playing=false;boardLayout=false;Session.StopLevel();ApplySafeArea();Clear(hudRoot);Clear(overlayRoot);RecycleCoins();Clear(effectsRoot);KillNavigation();CurrentPage=page;SoundService.Music("bgm_home");
  if(!pages.TryGetValue(page,out var next)||next==null){next=UICraft.Root(pageTrack,page);next.gameObject.AddComponent<RectMask2D>();next.gameObject.AddComponent<CanvasGroup>();pages[page]=next;switch(page){case "chapters":BuildChapters(next);break;case "shop":BuildShop(next);break;case "ranking":BuildRanking(next,false);break;case "vip":BuildRanking(next,true);break;default:BuildHome(next);break;}}
  foreach(var pair in pages)pair.Value.anchoredPosition=new Vector2(PageOrder(pair.Key)*pageRoot.rect.width,0);currentPage=next;LayoutMenuPage(next,page);if(page=="home"){feedback.Visit();RefreshHomeFeedback(false);feedbackRefreshPending=true;}
  bottomFill.gameObject.SetActive(true);rankingBackdrop.gameObject.SetActive(page=="ranking"||page=="vip"||page=="shop");headerSlot.gameObject.SetActive(true);navSlot.gameObject.SetActive(false);TopBar(headerSlot,page=="home");float target=-PageOrder(page)*pageRoot.rect.width;int version=routeVersion;busy=false;
  if(animate&&Mathf.Abs(pageTrack.anchoredPosition.x-target)>.1f){SoundService.Play("click");navigation=DOTween.Sequence().SetUpdate(true).SetId(this);navigation.Append(DOTween.To(()=>pageTrack.anchoredPosition,v=>pageTrack.anchoredPosition=v,new Vector2(target,0),.27f).SetEase(Ease.OutCubic));navigation.OnComplete(()=>{if(version!=routeVersion)return;pageTrack.anchoredPosition=new Vector2(target,0);navigation=null;SetPageInput();});}else pageTrack.anchoredPosition=new Vector2(target,0);SetPageInput();RefreshWallet();if(page=="ranking"||page=="vip")next.GetComponentInChildren<LeagueRankingView>()?.Enter();if(page=="home"){SoundService.Play("cat_home_greeting");}if(page=="home"||page=="chapters")StartIdleContentPrefetch();
 }
```

### StartIntroWhenVisible · L128

方法 SHA-256：`4d18d821c3af6b78745000cfd72156de3f48b638defeffb281d4fc880e78f809`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.StartIntroWhenVisible(UnityEngine.RectTransform, int, bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:126` · `AppUI.ShowIntro` → `StartIntroWhenVisible(intro,generation,autoDismiss)`

```csharp
IEnumerator StartIntroWhenVisible(RectTransform intro,int generation,bool autoDismiss){
 while(!UnityEngine.Rendering.SplashScreen.isFinished){if(generation!=viewGeneration||intro==null)yield break;yield return null;}
 // Present the empty cream page before the first animation tick, even on a slow first frame.
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

### DismissIntro · L142

方法 SHA-256：`dfc8d9fad13fa1292e86f102ae20a93c060d678697863bf23bccecac09dc5daf`
时间来源：Time.unscaledDeltaTime, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.DismissIntro(UnityEngine.RectTransform, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:140` · `AppUI.StartIntroWhenVisible` → `DismissIntro(intro,generation)`

```csharp
IEnumerator DismissIntro(RectTransform intro,int generation){yield return new WaitForSecondsRealtime(JellyLogoIntro.HoldDuration);if(generation!=viewGeneration||intro==null)yield break;
 var group=intro.gameObject.AddComponent<CanvasGroup>();float t=0;while(t<JellyLogoIntro.FadeDuration){if(generation!=viewGeneration||intro==null)yield break;t+=Time.unscaledDeltaTime;group.alpha=1-Mathf.Clamp01(t/JellyLogoIntro.FadeDuration);yield return null;}if(intro!=null)Destroy(intro.gameObject);
 }
```

### StartPreparedLevel · L149

方法 SHA-256：`dc81b6df7d7f5f7045a68bbff749cb725e965d4b4619622a9849b09afec12a0f`
时间来源：Time.realtimeSinceStartupAsDouble
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.StartPreparedLevel(UnityEngine.RectTransform, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.ContentLoading.cs:58` · `AppUI.RunLevelLoad` → `StartPreparedLevel(loading,generation)`

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
  loading.GetComponent<LoadingPresentation>().SetStatus("正在布置棋盘…");
  yield return null;
  yield return Session.PrepareSession(currentLevel,tutorialRun,streakTrial);
  if(generation!=viewGeneration||loading==null)yield break;
  SoundService.Music(Session.IsBossTrial?"bgm_boss":"bgm_gameplay");Session.BoosterProfile=Profile;Session.SetPaused(true);
  progressionRun=streakTrial<0&&currentLevel>=Profile.AlphabetUnlocked;feedback=savedFeedback;if(streakTrial<0)feedback.StartLevel();
  boardLayout=true;gameCamera=Camera.main;ApplySafeArea();
  var presentation=loading.GetComponent<LoadingPresentation>();presentation.Play();
  double revealAt=Time.realtimeSinceStartupAsDouble+LoadingPresentation.MinimumDuration;
  while(Time.realtimeSinceStartupAsDouble<revealAt){
   if(generation!=viewGeneration||loading==null)yield break;
   yield return null;
  }
  if(generation!=viewGeneration||loading==null)yield break;
  if(currentPage!=null){Destroy(currentPage.gameObject);currentPage=null;}
  Destroy(loading.gameObject);busy=false;playing=true;CurrentPage="battle";
  BuildBattleHUD();RefreshHUD();if(contentLoad!=null)contentLoad.PreparationComplete=true;StartCoroutine(PrefetchFollowingLevel(currentLevel,generation));Session.SetPaused(true);yield return ShowCollectionGoalEntrance(generation);if(generation!=viewGeneration)yield break;if(tutorialRun)yield return ShowFirstBounceIntroduction(generation);else if(streakTrial<0)yield return ShowAlphabetIntroductions(generation);if(generation!=viewGeneration)yield break;if(streakTrial<0&&(tutorialRun||currentLevel==0))yield return ShowBattleInterfaceTour(generation);if(generation!=viewGeneration)yield break;if(!tutorialRun&&streakTrial<0&&!Session.IsOpening)yield return ShowBoosterUnlocks(generation);if(generation!=viewGeneration)yield break;if(Session.ApplyCampaignStreakBonus())yield return ShowCampaignStreakReward(generation);if(generation!=viewGeneration)yield break;Session.SetPaused(false);Metrics?.BeginRound(currentLevel);ShowBoosterSelectionGuide();StartTutorialGuide();SoundService.Play("click");if(tutorialRun)SoundService.Speak("vo_tutorial_launch_zh",70);
 }
```

### HideTutorialLater · L181

方法 SHA-256：`67d045ad35376576ea0166f9466a29f00d4ee33cbcf8eb3ee736284bc01bb8ba`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.HideTutorialLater(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:180` · `AppUI.ShowTutorial` → `HideTutorialLater(viewGeneration)`

```csharp
IEnumerator HideTutorialLater(int generation){yield return new WaitForSecondsRealtime(3.4f);if(generation==viewGeneration&&tutorialStep==2)ShowTutorial(0);}
```

### OpenModalMotion · L202

方法 SHA-256：`44406ca2dd200af83affbf444aa1d6b91e8d407f3660d45ad9df3f4cb8692c6f`
时间来源：SetUpdate(true)
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.AppUI.OpenModalMotion()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:201` · `AppUI.OpenModal` → `OpenModalMotion()`
- `Assets/Scripts/UI/AppUI.SettingsC.cs:24` · `AppUI.OpenSettingsSheetC` → `OpenModalMotion()`

```csharp
void OpenModalMotion(){var group=modal.gameObject.AddComponent<CanvasGroup>();group.alpha=0;modal.localScale=Vector3.one*.94f;modalMotion=DOTween.Sequence().SetUpdate(true).SetId(modal);modalMotion.Append(modal.DOScale(1,.21f).SetEase(Ease.OutCubic));modalMotion.Join(DOTween.To(()=>group.alpha,v=>group.alpha=v,1,.21f));SoundService.Play("whoosh");}
```

### CloseModal · L203

方法 SHA-256：`efab336b25623a836d0cdb373c2a9f0d4377689e75f50c884d3715776c74baeb`
时间来源：SetUpdate(true)
曲线证据：Ease.InCubic
解析符号：`BounceGarden.AppUI.CloseModal()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:89` · `AppUI.Update` → `CloseModal()`
- `Assets/Scripts/UI/AppUI.cs:206` · `AppUI.ContentEnd` → `CloseModal()`
- `Assets/Scripts/UI/AppUI.cs:210` · `AppUI.MockPurchase` → `CloseModal()`
- `Assets/Scripts/UI/AppUI.cs:210` · `AppUI.MockPurchase` → `CloseModal()`
- `Assets/Scripts/UI/AppUI.cs:210` · `AppUI.MockPurchase` → `CloseModal()`
- `Assets/Scripts/UI/AppUI.cs:210` · `AppUI.MockPurchase` → `CloseModal()`
- `Assets/Scripts/UI/AppUI.cs:224` · `AppUI.MockAd` → `CloseModal()`
- `Assets/Scripts/UI/AppUI.League.cs:24` · `AppUI.OpenLeagueRewardClaimed` → `CloseModal()`
- `Assets/Scripts/UI/AppUI.Shop.cs:58` · `AppUI.DrawDressing` → `CloseModal()`
- `Assets/Scripts/UI/AppUI.Tutorial.cs:23` · `AppUI.FinishFirstBounce` → `CloseModal()`
- `Assets/Scripts/UI/AppUI.Tutorial.cs:29` · `AppUI.FinishFirstBounce` → `CloseModal()`
- `Assets/Scripts/UI/AppUI.Tutorial.cs:30` · `AppUI.FinishFirstBounce` → `CloseModal()`

```csharp
void CloseModal(){if(modal==null)return;SoundService.Play("ui_back");modalMotion?.Kill(false);var old=modal;var group=old.GetComponent<CanvasGroup>();group.blocksRaycasts=false;modalMotion=DOTween.Sequence().SetUpdate(true).SetId(old);modalMotion.Append(old.DOScale(old.localScale*.94f,.14f).SetEase(Ease.InCubic));modalMotion.Join(DOTween.To(()=>group.alpha,v=>group.alpha=v,0,.14f));modalMotion.OnComplete(()=>{if(modal!=old)return;modalMotion=null;RemoveModal(true);});}
```

### Pop · L205

方法 SHA-256：`29ea64d1bef208d46bc5251f52221a09993792b6976baa374745714dabd8a28f`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.LerpUnclamped
解析符号：`BounceGarden.AppUI.Pop(UnityEngine.RectTransform, float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
IEnumerator Pop(RectTransform rt,float duration){float t=0;while(t<duration&&rt!=null){t+=Time.unscaledDeltaTime;rt.localScale=Vector3.one*Mathf.LerpUnclamped(.75f,1,UICraft.OutBack(Mathf.Clamp01(t/duration)));yield return null;}if(rt!=null)rt.localScale=Vector3.one;}
```

### CountWallet · L226

方法 SHA-256：`4f34f775519bf2e81f0588294eca89027a94dc701fa65f118162bb7105c9e5d5`
时间来源：Time.unscaledDeltaTime, WaitForSecondsRealtime
曲线证据：Mathf.Pow
解析符号：`BounceGarden.AppUI.CountWallet(int, int, int)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
IEnumerator CountWallet(int from,int to,int generation){float t=0;yield return new WaitForSecondsRealtime(.32f);while(t<.65f&&generation==viewGeneration){t+=Time.unscaledDeltaTime;if(walletText!=null)walletText.text=Mathf.RoundToInt(Mathf.Lerp(from,to,1-Mathf.Pow(1-Mathf.Clamp01(t/.65f),3))).ToString();yield return null;}if(generation==viewGeneration)RefreshWallet();}
```

### FlyCoins · L227

方法 SHA-256：`391b9474e97a4e80da3c574e1ba3b8f4a96e13eb1f9e2bee6defdeb9f8c973d5`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.FlyCoins()`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
IEnumerator FlyCoins(){yield return null;for(int i=0;i<8;i++){var coin=TakeCoin(viewGeneration);coin.anchoredPosition=new Vector2(UnityEngine.Random.Range(-220,220),UnityEngine.Random.Range(-80,120));StartCoroutine(FlyCoin(coin,i*.04f,viewGeneration));}yield return new WaitForSecondsRealtime(1.5f);RefreshWallet();}
```

### FlyCoin · L228

方法 SHA-256：`e902b695384a0175987d41416c93b7435012b55f559b3f5f88636de139c5ae20`
时间来源：Time.unscaledDeltaTime, WaitForSecondsRealtime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.AppUI.FlyCoin(UnityEngine.RectTransform, float, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:227` · `AppUI.FlyCoins` → `FlyCoin(coin,i*.04f,viewGeneration)`

```csharp
IEnumerator FlyCoin(RectTransform coin,float delay,int generation){yield return new WaitForSecondsRealtime(delay);if(coin==null||!coinLeases.TryGetValue(coin,out int lease)||lease!=generation)yield break;Vector2 start=coin.anchoredPosition,end=new Vector2(30,1010);float t=0;while(t<.48f&&coin!=null&&generation==viewGeneration){t+=Time.unscaledDeltaTime;float a=Mathf.Clamp01(t/.48f);Vector2 c=Vector2.Lerp(start,end,a*a);c.x+=Mathf.Sin(a*Mathf.PI)*(start.x<0?-130:130);coin.anchoredPosition=c;coin.localScale=Vector3.one*Mathf.Lerp(1,.56f,a);coin.localRotation=Quaternion.Euler(0,0,a*360);yield return null;}ReturnCoin(coin,generation);if(generation==viewGeneration&&walletTarget!=null){if(walletPulse!=null)StopCoroutine(walletPulse);walletPulse=StartCoroutine(WalletPulse(viewGeneration));if(delay>=.08f&&Mathf.RoundToInt(delay/.04f)%3==0)SoundService.Play("coin");}}
```

### WalletPulse · L232

方法 SHA-256：`e3f88e1c5080c327f32309d67f4c79c48077a327b36fa7533897d66216a52721`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Pow
解析符号：`BounceGarden.AppUI.WalletPulse(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:228` · `AppUI.FlyCoin` → `WalletPulse(viewGeneration)`

```csharp
IEnumerator WalletPulse(int generation){float t=0;while(t<.16f&&walletTarget!=null&&generation==viewGeneration){t+=Time.unscaledDeltaTime;float f=1-Mathf.Pow(1-Mathf.Clamp01(t/.16f),3);walletTarget.localScale=Vector3.one*Mathf.Lerp(1.14f,1,f);yield return null;}if(walletTarget!=null&&generation==viewGeneration)walletTarget.localScale=Vector3.one;walletPulse=null;}
```

### Fireworks · L233

方法 SHA-256：`1abdc9d3c8c77ccc6bc4d45203f0944a2f1e496454cfe4e23c4001a9c9a9af83`
时间来源：Time.unscaledTime, WaitForSecondsRealtime
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.AppUI.Fireworks(int)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
IEnumerator Fireworks(int generation){float until=Time.unscaledTime+5;while(Time.unscaledTime<until&&generation==viewGeneration){Vector2 center=new Vector2(UnityEngine.Random.Range(-360,360),UnityEngine.Random.Range(50,800));Color color=Color.Lerp(new Color(1,.63f,.10f),new Color(1,1,.70f),UnityEngine.Random.value);for(int i=0;i<20;i++){float a=i*Mathf.PI*2/20;var dot=UICraft.Box(effectsRoot,"Firework spark",center.x,center.y,5,25,color);dot.rectTransform.localRotation=Quaternion.Euler(0,0,a*Mathf.Rad2Deg-90);StartCoroutine(Spark(dot,center,new Vector2(Mathf.Cos(a),Mathf.Sin(a))*UnityEngine.Random.Range(170,320),.85f,generation));}yield return new WaitForSecondsRealtime(.40f);}if(generation==viewGeneration){for(int i=0;i<70;i++){var c=UICraft.Box(effectsRoot,"Confetti",UnityEngine.Random.Range(-500,500),1200+UnityEngine.Random.Range(0,500),13,28,Color.HSVToRGB(UnityEngine.Random.value,.65f,1));StartCoroutine(Confetti(c,generation));}}}
```

### Spark · L234

方法 SHA-256：`44f0ab27a6459c49f13826389cdc5bbfe0cf514e3dea5b7e58e63cd0cf5b071d`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.Spark(UnityEngine.UI.Image, UnityEngine.Vector2, UnityEngine.Vector2, float, int)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
IEnumerator Spark(Image image,Vector2 start,Vector2 velocity,float duration,int generation){float t=0;Color color=image.color;while(t<duration&&image!=null&&generation==viewGeneration){t+=Time.unscaledDeltaTime;image.rectTransform.anchoredPosition=start+velocity*t+Vector2.down*(120*t*t);color.a=1-t/duration;image.color=color;yield return null;}if(image!=null)Destroy(image.gameObject);}
```

### Confetti · L235

方法 SHA-256：`2115e49d73fc72ff07ed9858c497a88e6cf40a9bd83413b40a8bd1cc1d6cbf53`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.AppUI.Confetti(UnityEngine.UI.Image, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:233` · `AppUI.Fireworks` → `Confetti(c,generation)`

```csharp
IEnumerator Confetti(Image image,int generation){float t=0;var start=image.rectTransform.anchoredPosition;float speed=UnityEngine.Random.Range(330,510),phase=UnityEngine.Random.value*6;while(t<5.8f&&image!=null&&generation==viewGeneration){t+=Time.unscaledDeltaTime;image.rectTransform.anchoredPosition=start+new Vector2(Mathf.Sin(t*3+phase)*75,-speed*t);image.rectTransform.localRotation=Quaternion.Euler(0,0,t*170+phase);yield return null;}if(image!=null)Destroy(image.gameObject);}
```

### Toast · L237

方法 SHA-256：`2232ab18479c65d6b954ef4a117a0e22a91c933fc6e8704605922c78183bd60d`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.Toast(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.Alphabet.cs:35` · `AppUI.BuildAlphabetResultActions` → `Toast("进度保存失败，请重试。")`
- `Assets/Scripts/UI/AppUI.BalanceStatistics.cs:10` · `AppUI.DrawBalanceStatistics` → `Toast("统计暂不可用")`
- `Assets/Scripts/UI/AppUI.BalanceStatistics.cs:42` · `AppUI.DrawBalanceStatistics` → `Toast("导出失败："+stats.LastError)`
- `Assets/Scripts/UI/AppUI.BalanceStatistics.cs:63` · `AppUI.ShowBalanceExport` → `Toast("已复制导出路径")`
- `Assets/Scripts/UI/AppUI.BoosterUnlocks.cs:10` · `AppUI.ShowBoosterUnlocks` → `Toast("道具赠送保存失败，请稍后重试。")`
- `Assets/Scripts/UI/AppUI.BoosterUnlocks.cs:23` · `AppUI.OpenInGameBoosterOffer` → `Toast(Profile.Coins<price?"金币不足":Profile.BoosterStock(kind)>=99?"背包已满":"购买保存失败，请重试。")`
- `Assets/Scripts/UI/AppUI.BoosterUnlocks.cs:23` · `AppUI.OpenInGameBoosterOffer` → `Toast("已补充"+StreakTrials.Name(kind))`
- `Assets/Scripts/UI/AppUI.ChapterOpening70.cs:8` · `AppUI.OpenChapterOnHome` → `Toast("新章节保存失败，请重试。")`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:72` · `AppUI.PrepareMenuContentRoutine` → `Toast(page=="shop"?"正在准备小铺…":"正在准备关卡地图…")`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:76` · `AppUI.PrepareMenuContentRoutine` → `Toast(operation.TimedOut?"等待时间较长，请检查网络后重试。":"资源准备未完成，请重试。")`
- `Assets/Scripts/UI/AppUI.cs:92` · `AppUI.OnFeedback` → `Toast("道具保存失败，本次未发射；请重启后重试。")`
- `Assets/Scripts/UI/AppUI.cs:148` · `AppUI.BeginLevel` → `Toast("请先通关前面的关卡")`
- `Assets/Scripts/UI/AppUI.cs:210` · `AppUI.MockPurchase` → `Toast("仅供预览，尚未接入购买或广告服务。")`
- `Assets/Scripts/UI/AppUI.cs:210` · `AppUI.MockPurchase` → `Toast(status==MockOutcome.Completed?"演示购买：+"+coins+" 金币":"购买保存失败，未增加金币。")`
- `Assets/Scripts/UI/AppUI.cs:210` · `AppUI.MockPurchase` → `Toast("购买已取消，余额不变。")`
- `Assets/Scripts/UI/AppUI.cs:210` · `AppUI.MockPurchase` → `Toast("模拟失败，余额不变。")`
- `Assets/Scripts/UI/AppUI.cs:211` · `AppUI.OnOutcome` → `Toast("救助进度保存失败，请重启后重试。")`
- `Assets/Scripts/UI/AppUI.cs:211` · `AppUI.OnOutcome` → `Toast("已补充本关专用穿透球，下次发射可免费使用1次。")`
- `Assets/Scripts/UI/AppUI.cs:222` · `AppUI.ClaimBaseOnce` → `Toast("奖励保存失败，请重试。")`
- `Assets/Scripts/UI/AppUI.cs:224` · `AppUI.MockAd` → `Toast("界面预览不会发放金币。")`
- `Assets/Scripts/UI/AppUI.cs:224` · `AppUI.MockAd` → `Toast("基础奖励保存失败，暂时无法领取加成。")`
- `Assets/Scripts/UI/AppUI.cs:224` · `AppUI.MockAd` → `Toast("本轮加成已领取。")`
- `Assets/Scripts/UI/AppUI.cs:224` · `AppUI.MockAd` → `Toast(hasAdBonus?"演示奖励：20金币，仅发放一次。":"奖励保存失败，未增加金币。")`
- `Assets/Scripts/UI/AppUI.cs:224` · `AppUI.MockAd` → `Toast("广告已取消，未发放额外奖励。")`
- `Assets/Scripts/UI/AppUI.cs:224` · `AppUI.MockAd` → `Toast("模拟广告失败，未发放额外奖励。")`
- `Assets/Scripts/UI/AppUI.League.cs:8` · `AppUI.AddMedalForTesting` → `Toast(ok?"+"+Profile.LeagueSettings.testMedals+"枚奖章 · "+Profile.Badges+"枚累计":"奖章保存失败。")`
- `Assets/Scripts/UI/AppUI.League.cs:11` · `AppUI.OpenLeagueRewards` → `Toast("通关第10关后解锁花园联赛")`
- `Assets/Scripts/UI/AppUI.League.cs:17` · `AppUI.OpenLeagueRewards` → `Toast("奖励保存失败。")`
- `Assets/Scripts/UI/AppUI.Painting.cs:13` · `AppUI.BeginPaintingCollection` → `Toast("作品保存失败，请重试。")`
- `Assets/Scripts/UI/AppUI.Shop.cs:51` · `AppUI.DrawDressing` → `Toast("尚未解锁，去小铺看看吧。")`
- `Assets/Scripts/UI/AppUI.Shop.cs:58` · `AppUI.DrawDressing` → `Toast("装扮保存失败，请重试。")`
- `Assets/Scripts/UI/AppUI.Shop.cs:58` · `AppUI.DrawDressing` → `Toast("装扮已保存")`
- `Assets/Scripts/UI/AppUI.Shop.cs:60` · `AppUI.OpenCosmeticOffer` → `Toast(Profile.Coins<price?"金币不足":"购买保存失败，请重试。")`
- `Assets/Scripts/UI/AppUI.Systems.cs:34` · `AppUI.OpenBoosterOffer` → `Toast("第"+LocalProfile.BoosterUnlockLevel(kind)+"关解锁")`
- `Assets/Scripts/UI/AppUI.Systems.cs:39` · `AppUI.OpenBoosterOffer` → `Toast(Profile.Coins<price?"金币不足":Profile.BoosterStock(kind)>=99?"背包已满":"购买保存失败，请重试。")`
- `Assets/Scripts/UI/AppUI.Tutorial.cs:29` · `AppUI.FinishFirstBounce` → `Toast("进度保存失败，请重试。")`
- `Assets/Scripts/UI/FailureFlow.cs:20` · `AppUI.WatchContinueAd` → `Toast(outcome==MockOutcome.Cancelled?"广告已取消，未增加次数。":"广告暂不可用，请重试。")`
- `Assets/Scripts/UI/FailureFlow.cs:23` · `AppUI.WatchContinueAd` → `Toast("增加5次机会，继续加油！")`
- `Assets/Scripts/UI/HomeFeedbackTestTools.cs:27` · `AppUI.UseSavedFeedback` → `Toast("已恢复真实存档。")`
- `Assets/Scripts/UI/HomeFeedbackTestTools.cs:37` · `AppUI.ResetLocalTestData` → `Toast("进度重置失败。")`
- `Assets/Scripts/UI/HomeFeedbackTestTools.cs:38` · `AppUI.ResetLocalTestData` → `Toast("连胜记录重置失败。")`
- `Assets/Scripts/UI/HomeFeedbackTestTools.cs:39` · `AppUI.ResetLocalTestData` → `Toast(all?"存档已重置：第1天，0连胜。":"连胜已重置，关卡进度已保留。")`

```csharp
void Toast(string message){if(toast!=null)Destroy(toast.transform.parent.gameObject);var rt=UICraft.Root(effectsRoot,"Toast",0,-853,1000,130);UICraft.Box(rt,"Toast backing",0,0,1000,130,new Color(.04f,.14f,.23f,.93f));toast=UICraft.Label(rt,message,0,0,950,117,31);toastUntil=Time.unscaledTime+3.2f;}
```

### QAWaitForLevel · L247

方法 SHA-256：`cbe2feebdaa3f1f03ea2fd1b5bdafa82c426830d632f6bc3af28c81b2ce45b7a`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.QAWaitForLevel()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/BalanceStatisticsQA.cs:21` · `BalanceStatisticsQA.Battle` → `app.QAWaitForLevel()`
- `Assets/Scripts/CampaignSettlement020QA.cs:10` · `CampaignSettlement020QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/CatCosmeticsQA.cs:41` · `CatCosmeticsQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/Celebration020QA.cs:22` · `Celebration020QA.Level` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainCollectionQA.cs:29` · `ChainCollectionQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainCollectionQA.cs:36` · `ChainCollectionQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainGravityQA.cs:28` · `ChainGravityQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainGravityQA.cs:54` · `ChainGravityQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainGravityQA.cs:57` · `ChainGravityQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainGravityQA.cs:59` · `ChainGravityQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainGravityQA.cs:62` · `ChainGravityQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainGravityQA.cs:63` · `ChainGravityQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainGuardQA.cs:26` · `ChainGuardQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainPowerQA.cs:28` · `ChainPowerQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainPowerQA.cs:57` · `ChainPowerQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainPresentationQA.cs:19` · `ChainPresentationQA.Level` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainRatingQA.cs:27` · `ChainRatingQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainRealtimeQA.cs:27` · `ChainRealtimeQA.Level` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainRefillV39QA.cs:42` · `ChainRefillV39QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainVisualQA.cs:23` · `ChainVisualQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChainVolleyGravityQA.cs:21` · `ChainVolleyGravityQA.Level` → `app.QAWaitForLevel()`
- `Assets/Scripts/ChineseCheerArtQA.cs:15` · `ChineseCheerArtQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/CollectionQA.cs:27` · `CollectionQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/CombatFeedback79QA.cs:13` · `CombatFeedback79QA.Open` → `app.QAWaitForLevel()`
- `Assets/Scripts/EconomyV67QA.cs:13` · `EconomyV67QA.Open` → `app.QAWaitForLevel()`
- `Assets/Scripts/FailureQA.cs:16` · `FailureQA.Level` → `app.QAWaitForLevel()`
- `Assets/Scripts/FailureQA.cs:59` · `FailureQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/FontGoalLoopQA.cs:16` · `FontGoalLoopQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/FontGoalLoopQA.cs:38` · `FontGoalLoopQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/FourthGroupQA.cs:28` · `FourthGroupQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/FourthGroupQA.cs:48` · `FourthGroupQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/GardenBackgroundQA.cs:37` · `GardenBackgroundQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/GardenBackgroundQA.cs:60` · `GardenBackgroundQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/GardenBackgroundQA.cs:63` · `GardenBackgroundQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/GroupRefill82QA.cs:21` · `GroupRefill82QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/HomeBannerQA.cs:66` · `HomeBannerQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/HomeFeedbackQA.cs:53` · `HomeFeedbackQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/HomeFeedbackQA.cs:55` · `HomeFeedbackQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ImmediateSpecialIntro020QA.cs:7` · `ImmediateSpecialIntro020QA.Load` → `app.QAWaitForLevel()`
- `Assets/Scripts/InGameFeedbackQA.cs:19` · `InGameFeedbackQA.Level` → `app.QAWaitForLevel()`
- `Assets/Scripts/IngotFxQA.cs:16` · `IngotFxQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/JellyBalanceR2QA.cs:12` · `JellyBalanceR2QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/JellyJourney80QA.cs:13` · `JellyJourney80QA.Battle` → `app.QAWaitForLevel()`
- `Assets/Scripts/JellyV40QA.cs:40` · `JellyV40QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/JellyV40QA.cs:43` · `JellyV40QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/JellyV40QA.cs:48` · `JellyV40QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/JellyWallQA.cs:53` · `JellyWallQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/LeagueCalendarQA.cs:24` · `LeagueCalendarQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/LeagueQA.cs:42` · `LeagueQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/LeagueV66QA.cs:36` · `LeagueV66QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/LeagueV79QA.cs:22` · `LeagueV79QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/LevelFiveSettlementQA.cs:17` · `LevelFiveSettlementQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/MobileV57InputQA.cs:15` · `MobileV57InputQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/OnboardingQA.cs:31` · `OnboardingQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/OnboardingQA.cs:36` · `OnboardingQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/Opening81QA.cs:22` · `Opening81QA.Load` → `app.QAWaitForLevel()`
- `Assets/Scripts/Opening81QA.cs:31` · `Opening81QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/Opening81QA.cs:55` · `Opening81QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/Opening81QA.cs:61` · `Opening81QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/OpeningEffectsQA.cs:57` · `OpeningEffectsQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/OpeningEffectsQA.cs:60` · `OpeningEffectsQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/OpeningEffectsQA.cs:61` · `OpeningEffectsQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/PastelQA.cs:77` · `PastelQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/PastelQA.cs:127` · `PastelQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/PastelQA.cs:143` · `PastelQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/PastelQA.cs:160` · `PastelQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/PastelQA.cs:166` · `PastelQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/PastelQA.cs:167` · `PastelQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/PastelQA.cs:170` · `PastelQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/PastelQA.cs:174` · `PastelQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/PrefetchReuse020QA.cs:16` · `PrefetchReuse020QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/Refill80QA.cs:30` · `Refill80QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/Refill80QA.cs:84` · `Refill80QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/ResultNoSplashF4QA.cs:8` · `ResultNoSplashF4QA.Level` → `app.QAWaitForLevel()`
- `Assets/Scripts/SecondGroupIntegrationQA.cs:21` · `SecondGroupIntegrationQA.Level` → `app.QAWaitForLevel()`
- `Assets/Scripts/SettingsCQA.cs:13` · `SettingsCQA.LoadBattle` → `app.QAWaitForLevel()`
- `Assets/Scripts/SettingsCQA.cs:28` · `SettingsCQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/Settlement82QA.cs:42` · `Settlement82QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/SevenSpecial80QA.cs:37` · `SevenSpecial80QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/SevenSpecial80QA.cs:84` · `SevenSpecial80QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/SfxV87QA.cs:18` · `SfxV87QA.Level` → `app.QAWaitForLevel()`
- `Assets/Scripts/SystemsV64QA.cs:16` · `SystemsV64QA.Open` → `app.QAWaitForLevel()`
- `Assets/Scripts/ThirdGroupQA.cs:45` · `ThirdGroupQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/VictoryFinaleQA.cs:16` · `VictoryFinaleQA.StartLevel` → `app.QAWaitForLevel()`
- `Assets/Scripts/VictoryQA.cs:33` · `VictoryQA.Level` → `app.QAWaitForLevel()`
- `Assets/Scripts/VictoryQA.cs:103` · `VictoryQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/VictoryRace020QA.cs:10` · `VictoryRace020QA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/VictorySpeedR3QA.cs:10` · `VictorySpeedR3QA.Level` → `app.QAWaitForLevel()`
- `Assets/Scripts/YarnQA.cs:38` · `YarnQA.Start` → `app.QAWaitForLevel()`
- `Assets/Scripts/YarnQA.cs:88` · `YarnQA.Start` → `app.QAWaitForLevel()`

```csharp
public IEnumerator QAWaitForLevel(){float deadline=Time.realtimeSinceStartup+30;do{yield return null;}while(overlayRoot.Find("Loading")!=null&&Time.realtimeSinceStartup<deadline);if(CurrentPage!="battle")throw new InvalidOperationException("Level failed to enter battle after loading.");}
```

## 对象层级和素材入口

- L102：`UICraft.Root(pageTrack,page)`
- L233：`UICraft.Box(effectsRoot,"Firework spark",center.x,center.y,5,25,color)`
- L237：`UICraft.Root(effectsRoot,"Toast",0,-853,1000,130)`
- L237：`UICraft.Box(rt,"Toast backing",0,0,1000,130,new Color(.04f,.14f,.23f,.93f))`
- L237：`UICraft.Label(rt,message,0,0,950,117,31)`
- L247：`overlayRoot.Find("Loading")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){CloseCollectionGoalEntrance();CancelContentLoad();CloseTutorialGuide();CloseAlphabetIntroduction();ClearPaintingCollection();modalMotion?.Kill();KillNavigation();if(Session!=null){Session.StateChanged-=RefreshHUD;Session.Outcome-=OnOutcome;Session.Feedback-=OnFeedback;}Profile?.Save();}
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
void CancelAim(){if(aiming){Metrics?.Balance?.StrategyInput("cancel",aimDirection);NotifyTutorialPointer(false);}aiming=false;pointerId=-999;if(Session!=null)Session.SetAim(null);}
```

```csharp
void Pause(){if(victoryCelebration!=null||victoryFinale!=null||victoryPreparation!=null||StreakRewardActive||AlphabetIntroActive||results!=null||IsCollectingPainting)return;BuildPlayerSettings();}
```

## 源字段与依赖

```csharp
RectTransform canvasRoot,safeRoot,designRoot,pageRoot,hudRoot,overlayRoot,effectsRoot,navRoot,rankingBackdrop;
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
Round6Nav nav;
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
Coroutine resultSequence,walletPulse;
```
```csharp
Action skipResult;
```
```csharp
TMP_Text toast;
```
```csharp
float toastUntil;
```
```csharp
readonly List<RaycastResult> uiHits=new List<RaycastResult>();
```
```csharp
Color sky=new Color32(139,220,235,255),grass=new Color32(112,184,80,255),purple=new Color32(98,65,155,255);
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
