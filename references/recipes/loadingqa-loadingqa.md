# LoadingQA · LoadingQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LoadingQA.cs`
文件 SHA-256：`0d37784b17d2a7da8b837cd39b49576cdb6fa99abb5a72e5a8f625f2da9292cf`
[原始源码快照](../source/Assets/Scripts/LoadingQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L15

方法 SHA-256：`a0ccf473106589f148545e5bfa29ead1620fc5773bce843fa4592c02bb4cbf36`
时间来源：Time.realtimeSinceStartup, Time.timeScale, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LoadingQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);Application.logMessageReceived+=Log;
  yield return null;app=FindFirstObjectByType<AppUI>();
  int viewport=Array.IndexOf(args,"--qa-viewport");var size=(viewport>=0?args[viewport+1]:"540x1170").Split('x');width=int.Parse(size[0]);height=int.Parse(size[1]);
  target=new RenderTexture(width,height,24);target.Create();Camera.main.targetTexture=target;
  var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=Camera.main;canvas.planeDistance=1;
  app.QASetViewport(new Vector2Int(width,height));yield return null;app.QANavigate("home");yield return null;
  float loadingStarted=Time.realtimeSinceStartup;app.QAStartLevel(0);yield return new WaitForSecondsRealtime(.3f);
  var presentation=FindFirstObjectByType<LoadingPresentation>();Check(presentation!=null,"loading presentation exists");
  Check(LoadingPresentation.MinimumDuration>=3&&LoadingPresentation.MinimumDuration<=5,"loading target is within 3 to 5 seconds");
  Check(LoadingPresentation.MinimumDuration>LoadingPresentation.CatCycleDuration,"minimum hold exceeds complete cat cycle");
  Check(app.Session.IsPaused,"prepared game simulation paused throughout loading");
  int generation=app.Session.Generation;
  var begin=typeof(AppUI).GetMethod("BeginLevel",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic);
  for(int i=0;i<20;i++)begin.Invoke(app,new object[]{0});yield return null;
  Check(app.Session.Generation==generation,"rapid duplicate starts ignored during loading");
  var first=(RectTransform)presentation.transform.Find("Pastel layered logo").GetChild(0);
  Vector2 position=first.anchoredPosition;Time.timeScale=0;float before=presentation.Elapsed;
  yield return new WaitForSecondsRealtime(.4f);
  Check(presentation.Elapsed>before+.25f,"loading clock advances with timeScale zero");
  Check(Vector2.Distance(position,first.anchoredPosition)>.1f,"individual logo glyph animates while simulation paused");
  Time.timeScale=1;
  var seen=new HashSet<string>();float lastElapsed=0;
  while(presentation!=null){
   lastElapsed=presentation.Elapsed;seen.Add(presentation.CurrentPose);
   if(lastElapsed>1.9f*3.6f/6.7f&&lastElapsed<2.05f*3.6f/6.7f)yield return Capture("cat-anticipate");
   if(lastElapsed>2.6f*3.6f/6.7f&&lastElapsed<2.75f*3.6f/6.7f)yield return Capture("cat-celebrate");
   if(lastElapsed>4.5f*3.6f/6.7f&&lastElapsed<4.65f*3.6f/6.7f)yield return Capture("cat-wave");
   if(lastElapsed>3.7f&&lastElapsed<3.85f)yield return Capture("loading-after-full-cycle");
   if(presentation!=null&&presentation.Elapsed<LoadingPresentation.MinimumDuration)CheckSilentPause();yield return null;
  }
  Check(lastElapsed>=LoadingPresentation.MinimumDuration-.1f,"overlay completes 4 visible seconds (last="+lastElapsed.ToString("F3")+")");
  Check(lastElapsed<=LoadingPresentation.MinimumDuration+.1f,"loading visible clock finishes without extra hold");
  float wallDuration=Time.realtimeSinceStartup-loadingStarted;
  File.WriteAllText(Path.Combine(dir,"duration.txt"),"Visible clock: "+lastElapsed.ToString("F3")+"s; total loading wall time: "+wallDuration.ToString("F3")+"s");
  Check(wallDuration>=3&&wallDuration<=5,"actual loading enters battle within 3 to 5 seconds ("+wallDuration.ToString("F3")+"s)");
  Check(!pauseFailed,"game remained paused until the minimum hold completed");
  Check(seen.Count==5,"all five cat poses shown before entering game");
  Check(app.CurrentPage=="battle"&&app.Session.IsPlaying&&!app.Session.IsPaused,"game enters and resumes after loading");
  yield return Capture("battle-after-loading");
  app.QANavigate("home");app.QAStartLevel(1);yield return new WaitForSecondsRealtime(.4f);app.QANavigate("home");
  yield return new WaitForSecondsRealtime(LoadingPresentation.MinimumDuration+.4f);
  Check(app.CurrentPage=="home"&&!app.Session.IsPlaying,"cancelled loading cannot start a stale level");
  Check(FindFirstObjectByType<LoadingPresentation>()==null,"cancel removes loading presentation");
  Check(errors==0,"zero runtime errors");
  File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failed+" errors="+errors+"\n");
  Application.Quit(failed==0&&errors==0?0:3);
 }
```

### Capture · L65

方法 SHA-256：`95a36644135becef96925eb2e181f3145d6a42d15a040912f6813f52316cc4f8`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LoadingQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LoadingQA.cs:40` · `LoadingQA.Start` → `Capture("cat-anticipate")`
- `Assets/Scripts/LoadingQA.cs:41` · `LoadingQA.Start` → `Capture("cat-celebrate")`
- `Assets/Scripts/LoadingQA.cs:42` · `LoadingQA.Start` → `Capture("cat-wave")`
- `Assets/Scripts/LoadingQA.cs:43` · `LoadingQA.Start` → `Capture("loading-after-full-cycle")`
- `Assets/Scripts/LoadingQA.cs:54` · `LoadingQA.Start` → `Capture("battle-after-loading")`

```csharp
IEnumerator Capture(string name){
  yield return null;Canvas.ForceUpdateCanvases();Camera.main.Render();var old=RenderTexture.active;RenderTexture.active=target;
  var image=new Texture2D(width,height,TextureFormat.RGB24,false);image.ReadPixels(new Rect(0,0,width,height),0,0);image.Apply();RenderTexture.active=old;
  File.WriteAllBytes(Path.Combine(dir,name+".png"),image.EncodeToPNG());Destroy(image);
 }
```

## 对象层级和素材入口

- L31：`presentation.transform.Find("Pastel layered logo").GetChild(0)`
- L31：`presentation.transform.Find("Pastel layered logo")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void CheckSilentPause(){if(!pauseFailed&&!app.Session.IsPaused){pauseFailed=true;Check(false,"game remained paused under loading");}}
```

## 源字段与依赖

```csharp
string dir;
```
```csharp
int checks,failed,errors,width,height;
```
```csharp
AppUI app;
```
```csharp
RenderTexture target;
```
```csharp
bool pauseFailed;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
