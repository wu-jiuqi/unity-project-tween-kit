# HomeBannerQA · HomeBannerQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/HomeBannerQA.cs`
文件 SHA-256：`9b18745465ae254abd978befebc53116ce920e4185e4d831143a6171da354dce`
[原始源码快照](../source/Assets/Scripts/HomeBannerQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L14

方法 SHA-256：`57c2ca5ea09800067ea34dfb9e90943b7bea3d6347b5772c035a510d2921d672`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.HomeBannerQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/HomeBannerQA.cs:30` · `HomeBannerQA.Start` → `Capture("01-home-wechat")`
- `Assets/Scripts/HomeBannerQA.cs:42` · `HomeBannerQA.Start` → `Capture("02b-ranking-info")`
- `Assets/Scripts/HomeBannerQA.cs:44` · `HomeBannerQA.Start` → `Capture("02-ranking")`
- `Assets/Scripts/HomeBannerQA.cs:53` · `HomeBannerQA.Start` → `Capture("03b-shop-guide")`
- `Assets/Scripts/HomeBannerQA.cs:54` · `HomeBannerQA.Start` → `Capture("03-shop")`
- `Assets/Scripts/HomeBannerQA.cs:55` · `HomeBannerQA.Start` → `Capture("04-frame-shop")`
- `Assets/Scripts/HomeBannerQA.cs:61` · `HomeBannerQA.Start` → `Capture("05-dressing-safe")`
- `Assets/Scripts/HomeBannerQA.cs:62` · `HomeBannerQA.Start` → `Capture("05b-shop-equipped")`
- `Assets/Scripts/HomeBannerQA.cs:64` · `HomeBannerQA.Start` → `Capture("06-home-native")`
- `Assets/Scripts/HomeBannerQA.cs:76` · `HomeBannerQA.Start` → `Capture("07-battle-wechat")`
- `Assets/Scripts/HomeBannerQA.cs:80` · `HomeBannerQA.Start` → `Capture("08-fullscreen-garden")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();Canvas.ForceUpdateCanvases();var backdrop=GameObject.Find("Full screen cream clear");if(backdrop!=null)backdrop.GetComponent<Camera>().Render();Camera.main.Render();var old=RenderTexture.active;RenderTexture.active=target;var tex=new Texture2D(width,height,TextureFormat.RGB24,false);tex.ReadPixels(new Rect(0,0,width,height),0,0);tex.Apply();File.WriteAllBytes(dir+"/"+name+".png",tex.EncodeToPNG());RenderTexture.active=old;Destroy(tex);}
```

### Start · L16

方法 SHA-256：`4dd47c936a8cfbaa1665e38b3d2769c4906f5ca0ab418ad1575b63045f8e20ec`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.HomeBannerQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");int n=Array.IndexOf(args,"--qa-size");if(n>=0){var size=args[n+1].Split('x');width=int.Parse(size[0]);height=int.Parse(size[1]);}
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();target=new RenderTexture(width,height,24);target.Create();Camera.main.targetTexture=target;canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=Camera.main;canvas.planeDistance=1;
  app.QASetViewport(new Vector2Int(width,height));WeChatLayoutMetrics.Set(390,844,new Rect(0,44,390,766),new Rect(295,52,87,32));app.QAStaticNavigation("home");yield return new WaitForSecondsRealtime(.7f);
  Check(FindObjectsByType<Round6Nav>(FindObjectsSortMode.None).Length==0,"no bottom navigation created");
  var settings=FindButton("Settings medallion");var corners=new Vector3[4];settings.GetComponent<RectTransform>().GetWorldCorners(corners);float high=Camera.main.WorldToScreenPoint(corners[2]).y;
  Check(ClearsCapsule(settings),"home settings hit region avoids only the capsule");
  var homeTop=GameObject.Find("Home top content").GetComponent<RectTransform>();var header=GameObject.Find("Fixed top bar").GetComponent<RectTransform>();var portrait=GameObject.Find("Portrait layout").GetComponent<RectTransform>();
  Check(Mathf.Abs(homeTop.anchoredPosition.y-header.anchoredPosition.y)<.01f&&homeTop.anchoredPosition.y<(portrait.rect.height-2340)*.5f&&homeTop.localScale==Vector3.one&&settings.GetComponent<RectTransform>().anchoredPosition==new Vector2(446,1010),"avatar wordmark settings and banners move together without rescaling or moving settings independently");
  var play=FindButton("Play current level");play.GetComponent<RectTransform>().GetWorldCorners(corners);Check(Camera.main.WorldToScreenPoint(corners[0]).y>=0,"start button stays on screen");
  var carousel=FindFirstObjectByType<ActivityCarousel>();Check(carousel.transform.Find("Activity cards/Style Shop/Jelly visual/Shop portrait layers")!=null,"shop card uses independent original portrait layers");
  var first=carousel.transform.Find("Activity cards/Garden League").GetComponent<RectTransform>();first.GetWorldCorners(corners);Check(Camera.main.WorldToScreenPoint(corners[0]).x<width*.10f,"banner retains full authored width on short safe-area screens");
  var feedback=GameObject.Find("Home achievement feedback").transform;Check(feedback.localScale.x>=.65f,"short-screen welcome text remains readable");
  yield return Capture("01-home-wechat");
  carousel.QADrag(new Vector2(-400,5));yield return new WaitForSecondsRealtime(.4f);Check(carousel.SelectedPage==1,"horizontal swipe selects next carousel page");carousel.QADrag(new Vector2(400,5));yield return new WaitForSecondsRealtime(.4f);Check(carousel.SelectedPage==0,"carousel returns to first page");
  // Click through the actual pointer event handler, not a separate navigation implementation.
  var card=carousel.transform.Find("Activity cards/Garden League");var e=new PointerEventData(EventSystem.current){button=PointerEventData.InputButton.Left,position=Camera.main.WorldToScreenPoint(card.position),pointerCurrentRaycast=new RaycastResult{module=canvas.GetComponent<GraphicRaycaster>()}};carousel.OnPointerDown(e);carousel.OnPointerUp(e);carousel.OnPointerClick(e);yield return new WaitForSecondsRealtime(.5f);
  Check(app.CurrentPage=="ranking"&&FindFirstObjectByType<LeagueRankingView>()!=null,"leaderboard banner opens actual league");
  var view=FindFirstObjectByType<LeagueRankingView>();var podium=FindFirstObjectByType<LeaguePodiumView>();Check(podium!=null&&GameObject.Find("League ribbon navigation")!=null,"reference ribbon navigation and podium exist");
  Check(FindButton("Settings medallion")==null,"ranking uses back/title/info without old settings chrome");
  var expected=new[]{2,1,3}.Select(rank=>view.Snapshot.entries.First(entry=>entry.rank==rank).id).ToArray();Check(podium.DisplayedIds.SequenceEqual(expected),"podium displays actual second, first, third entries");
  Check(view.ListContent.anchoredPosition.y<1,"ranking opens with leaders visible");
  var clock=GameObject.Find("League countdown").GetComponent<TMPro.TMP_Text>();string clockBefore=clock.text;yield return new WaitForSecondsRealtime(1.3f);Check(clock.text!=clockBefore,"countdown ticks each second");
  var back=FindButton("Return home").GetComponent<RectTransform>();back.GetWorldCorners(corners);Check(ClearsCapsule(FindButton("League rewards preview")),"ranking info avoids capsule without moving left header");
  Rect systemZone;WeChatLayoutMetrics.TryGetBlockedRect(new Vector2Int(width,height),out systemZone);var ribbon=GameObject.Find("League ribbon navigation").transform.Find("Title ribbon").GetComponent<RectTransform>();ribbon.GetWorldCorners(corners);Check(Camera.main.WorldToScreenPoint(corners[2]).y<systemZone.yMin,"entire ranking title and back navigation sit below reserved system strip");
  int coinsBefore=app.Profile.Coins;FindButton("League rewards preview").onClick.Invoke();yield return new WaitForSecondsRealtime(.3f);Check(GameObject.Find("Modal")!=null&&app.Profile.Coins==coinsBefore,"info button opens existing reward rules without modifying balance");yield return Capture("02b-ranking-info");
  app.QAStaticNavigation("ranking");yield return new WaitForSecondsRealtime(.4f);FindButton("League locate player").onClick.Invoke();yield return null;var own=view.ListContent.Find("League row you");Check(Mathf.Abs(view.Viewport.InverseTransformPoint(own.position).y)<view.Viewport.rect.height/2,"footer locates player in full roster");
  view.Viewport.GetComponent<ScrollRect>().verticalNormalizedPosition=1;yield return new WaitForSecondsRealtime(.3f);yield return Capture("02-ranking");
  FindButton("Return home").onClick.Invoke();yield return new WaitForSecondsRealtime(.5f);Check(app.CurrentPage=="home","ranking has top return home");
  carousel.ActivateCard(1);yield return new WaitForSecondsRealtime(.5f);Check(app.CurrentPage=="shop"&&FindButton("Button Frames")!=null,"shop banner opens purchase page");
  Check(GameObject.Find("Shop ribbon navigation")!=null&&FindButton("Shop information")!=null,"shop shares back/ribbon/info navigation with ranking");
  Check(FindButton("Button Settings")==null,"shop removes old settings from navigation");
  var shopBack=FindButton("Button Home").GetComponent<RectTransform>();shopBack.GetWorldCorners(corners);Check(ClearsCapsule(FindButton("Shop information")),"shop info avoids capsule without shrinking shop");
  var shopRibbon=GameObject.Find("Shop ribbon navigation").transform.Find("Title ribbon").GetComponent<RectTransform>();shopRibbon.GetWorldCorners(corners);Check(Camera.main.WorldToScreenPoint(corners[2]).y<systemZone.yMin,"entire shop navigation sits below reserved system strip");
  GameObject.Find("Shop wallet capsule").GetComponent<RectTransform>().GetWorldCorners(corners);float walletBottom=Camera.main.WorldToScreenPoint(corners[0]).y;FindButton("Button Avatars").GetComponent<RectTransform>().GetWorldCorners(corners);Check(walletBottom>Camera.main.WorldToScreenPoint(corners[2]).y,"wallet toolbar does not overlap category tabs");
  float tabsBottom=Camera.main.WorldToScreenPoint(corners[0]).y;GameObject.Find("Reference grid viewport").GetComponent<RectTransform>().GetWorldCorners(corners);Check(Camera.main.WorldToScreenPoint(corners[2]).y<tabsBottom,"product scroll viewport starts below category tabs");
  int shopGuideBalance=app.Profile.Coins;FindButton("Shop information").onClick.Invoke();yield return new WaitForSecondsRealtime(.3f);Check(FindButton("Button Continue shopping")!=null&&app.Profile.Coins==shopGuideBalance,"shop info opens guide without purchasing");yield return Capture("03b-shop-guide");FindButton("Button Continue shopping").onClick.Invoke();yield return new WaitForSecondsRealtime(.3f);
  Check(!FindObjectsByType<Button>(FindObjectsSortMode.None).Any(b=>b.gameObject.activeInHierarchy&&b.name=="Button Rank"),"shop has no bottom nav controls");yield return Capture("03-shop");
  FindButton("Button Frames").onClick.Invoke();yield return new WaitForSecondsRealtime(.4f);Check(GameObject.Find("Reference grid").transform.childCount==10,"all ten purchasable frames shown");yield return Capture("04-frame-shop");
  int balance=app.Profile.Coins;var item=CatCosmeticCatalog.Items[1];app.OpenCosmeticOffer(item.id,true);yield return new WaitForSecondsRealtime(.4f);FindButton("Button Buy with coins").onClick.Invoke();yield return new WaitForSecondsRealtime(.4f);
  Check(app.Profile.OwnsShopCosmetic(item.id,true)&&app.Profile.Coins==balance-LocalProfile.CosmeticPrice(item.id,true),"banner path purchases frame with exact coin debit");
  FindButton("Button Customize").onClick.Invoke();yield return new WaitForSecondsRealtime(.4f);Check(FindButton("Button Save style")!=null,"purchase opens existing dressing modal");
  FindButton("Button 选择 头像框 "+item.id).onClick.Invoke();yield return new WaitForSecondsRealtime(.3f);
  WeChatLayoutMetrics.Set(390,844,new Rect(0,59,390,751),new Rect(295,66,87,32));yield return new WaitForSecondsRealtime(.4f);
  var modal=GameObject.Find("Modal").GetComponent<RectTransform>();modal.GetWorldCorners(corners);var safe=WeChatLayoutMetrics.Fit(new Rect(0,0,width,height),new Vector2Int(width,height));Check(Camera.main.WorldToScreenPoint(corners[2]).y<=safe.yMax+1&&Camera.main.WorldToScreenPoint(corners[0]).y>=safe.yMin-1,"open dressing retains native safe bounds after capsule moves");yield return Capture("05-dressing-safe");
  FindButton("Button Save style").onClick.Invoke();yield return new WaitForSecondsRealtime(.3f);var shopEquipped=GameObject.Find("Shop equipped style").GetComponentInChildren<CatCosmeticPortrait>();Check(app.Profile.EquippedFrameId==item.id&&shopEquipped.FrameId==item.id,"saving purchased frame refreshes equipped shop toolbar immediately");yield return Capture("05b-shop-equipped");FindButton("Button Home").onClick.Invoke();yield return new WaitForSecondsRealtime(.5f);Check(app.CurrentPage=="home","shop has top return home");
  WeChatLayoutMetrics.Set(390,844,new Rect(0,44,390,766),Rect.zero);yield return new WaitForSecondsRealtime(.3f);Check(FindButton("Settings medallion")!=null,"invalid capsule retains usable fallback layout");
  WeChatLayoutMetrics.Reset();yield return new WaitForSecondsRealtime(.3f);yield return Capture("06-home-native");
  Check(FindButton("Play current level").GetComponent<UIBounce>()!=null,"play retains existing bounce component");
  WeChatLayoutMetrics.Set(390,844,new Rect(0,44,390,766),new Rect(295,52,87,32));yield return null;FindButton("Play current level").onClick.Invoke();yield return app.QAWaitForLevel();Check(app.CurrentPage=="battle"&&app.Session.IsPlaying,"start button enters actual gameplay");
  var cameraRect=Camera.main.rect;var design=GameObject.Find("Portrait layout").transform;var designScale=design.localScale;
  WeChatLayoutMetrics.Reset();yield return new WaitForSecondsRealtime(.2f);
  Check(Camera.main.rect==cameraRect&&design.localScale==designScale,"capsule does not shrink battle camera or HUD scale");
  var before=FindButton("Battle settings").GetComponent<RectTransform>().anchoredPosition;
  // Force a capsule over the authored settings hit region, proving local displacement.
  var button=FindButton("Battle settings");button.GetComponent<RectTransform>().GetWorldCorners(corners);var p0=Camera.main.WorldToScreenPoint(corners[0]);var p2=Camera.main.WorldToScreenPoint(corners[2]);
  WeChatLayoutMetrics.Set(width,height,new Rect(0,0,width,height),Rect.MinMaxRect(p0.x,height-p2.y,p2.x,height-p0.y));yield return new WaitForSecondsRealtime(.2f);
  Check(ClearsCapsule(button)&&button.GetComponent<RectTransform>().anchoredPosition.y<before.y,"only overlapping battle settings moves below capsule");
  Check(Camera.main.rect==cameraRect&&design.localScale==designScale,"moving settings preserves battle dimensions");
  WeChatLayoutMetrics.Set(390,844,new Rect(0,44,390,766),new Rect(295,52,87,32));yield return new WaitForSecondsRealtime(.2f);yield return Capture("07-battle-wechat");

  app.Session.SetAim(Vector2.up);int changes=app.Session.AimVisibilityChanges;for(int j=0;j<120;j++)app.Session.SetAim(Vector2.up);Check(app.Session.AimVisibilityChanges==changes,"stable aim reuses all visible points without 120 frames of disable-enable churn");app.Session.SetAim(null);
  int spawned=app.Session.SpawnCount;bool accepted=app.Session.TryShoot(Vector2.up);yield return new WaitForFixedUpdate();yield return new WaitForFixedUpdate();Check(accepted&&app.Session.SpawnCount>spawned,"accepted shot begins spawning within two physics ticks without animation delay");
  app.QASetSafeArea(new Rect(0,24,width,height-48));yield return new WaitForSecondsRealtime(.2f);var fill=GameObject.Find("Full screen original garden fill");var fillCamera=GameObject.Find("Full screen cream clear").GetComponent<Camera>();Check(fill!=null&&fill.activeSelf&&fillCamera.rect==new Rect(0,0,1,1)&&fillCamera.cullingMask==(1<<30),"original garden background independently fills top and bottom outside safe gameplay viewport");yield return Capture("08-fullscreen-garden");
  File.AppendAllText(dir+"/checks.txt",$"RESULT checks={checks} failed={failures} errors={errors}\n");Camera.main.targetTexture=null;target.Release();Application.Quit(failures+errors==0?0:1);
 }
```

## 对象层级和素材入口

- L14：`GameObject.Find("Full screen cream clear")`
- L24：`GameObject.Find("Home top content")`
- L27：`carousel.transform.Find("Activity cards/Style Shop/Jelly visual/Shop portrait layers")`
- L28：`carousel.transform.Find("Activity cards/Garden League")`
- L29：`GameObject.Find("Home achievement feedback")`
- L33：`carousel.transform.Find("Activity cards/Garden League")`
- L35：`GameObject.Find("League ribbon navigation")`
- L39：`GameObject.Find("League countdown")`
- L41：`GameObject.Find("League ribbon navigation").transform.Find("Title ribbon")`
- L41：`GameObject.Find("League ribbon navigation")`
- L42：`GameObject.Find("Modal")`
- L43：`view.ListContent.Find("League row you")`
- L47：`GameObject.Find("Shop ribbon navigation")`
- L50：`GameObject.Find("Shop ribbon navigation").transform.Find("Title ribbon")`
- L50：`GameObject.Find("Shop ribbon navigation")`
- L51：`GameObject.Find("Shop wallet capsule")`
- L52：`GameObject.Find("Reference grid viewport")`
- L55：`GameObject.Find("Reference grid")`
- L61：`GameObject.Find("Modal")`
- L62：`GameObject.Find("Shop equipped style")`
- L67：`GameObject.Find("Portrait layout")`
- L80：`GameObject.Find("Full screen original garden fill")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
int checks,failures,errors,width=540,height=1170;
```
```csharp
string dir;
```
```csharp
AppUI app;
```
```csharp
RenderTexture target;
```
```csharp
Canvas canvas;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
