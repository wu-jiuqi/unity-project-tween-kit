# Round4Audit · Round4Audit

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Round4Audit.cs`
文件 SHA-256：`f130e2b47949ae49463033aa85a739acf878d389c019fa3a844306634eb523c3`
[原始源码快照](../source/Assets/Scripts/Round4Audit.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L14

方法 SHA-256：`1b749d36a14515650aa048fb871b04d658a6962f6f37ea62ec71596ef1f24f13`
时间来源：Time.fixedDeltaTime, Time.fixedTime, WaitForSecondsRealtime
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.Round4Audit.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();int i=Array.IndexOf(args,"--qa-out");directory=i>=0?args[i+1]:Path.Combine(Application.persistentDataPath,"Round4Audit");Directory.CreateDirectory(directory);
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();
  yield return Capture("home");app.QANavigate("shop");yield return new WaitForSecondsRealtime(.5f);yield return Capture("shop");app.QANavigate("ranking");yield return new WaitForSecondsRealtime(.5f);yield return Capture("ranking");app.QANavigate("home");yield return null;
  app.QAStartLevel(2);yield return new WaitForSecondsRealtime(1);app.Session.SetPaused(true);foreach(var mechanism in FindObjectsByType<MovingObstacle>(FindObjectsSortMode.None))mechanism.QAResetPose();Physics2D.SyncTransforms();yield return Capture("round27-initial");ExportBounds();app.Session.SetPaused(false);
  ContactFile=Path.Combine(directory,"contacts.csv");File.WriteAllText(ContactFile,"time_s,ball_id,target,point_x,point_y,normal_x,normal_y,pre_vx,pre_vy,post_vx,post_vy\n");
  foreach(bool feedback in new[]{true,false}){
   FeedbackProfile.Active.enabledFeedback=feedback;app.QAStartLevel(2);yield return new WaitForSecondsRealtime(.8f);UnityEngine.Random.InitState(20261026);
   float angle=27.2262f*Mathf.Deg2Rad;Vector2 direction=new Vector2(-Mathf.Sin(angle),Mathf.Cos(angle));app.Session.TryShoot(direction);yield return new WaitForFixedUpdate();
   tracked=null;foreach(var b in FindObjectsByType<BallBody>(FindObjectsSortMode.None))if(b.Active&&b.Id==1)tracked=b;
   if(tracked==null)throw new Exception("Calibration first ball missing");
   var point=(Vector2)Camera.main.ScreenToWorldPoint(new Vector3(219.42f,1280-922.63f,0));tracked.Initialize(1,app.Session.Generation,point,direction*app.Session.CurrentLevel.ballSpeed,app.Session.CurrentLevel.gravity);
   tracked.gameObject.AddComponent<Round4ContactProbe>();zero=Time.fixedTime;ContactZero=zero;
   trace=new StreamWriter(Path.Combine(directory,feedback?"trajectory-on.csv":"trajectory-off.csv"));trace.WriteLine("time_s,ball_id,x_px,y_px,world_x,world_y,vx,vy");WriteSample();
   for(int step=0;step<28;step++){yield return new WaitForFixedUpdate();if(tracked.Active)WriteSample();}
   trace.Dispose();trace=null;
  }
  FeedbackProfile.Active.enabledFeedback=true;ContactFile=null;
  app.QAStartLevel(2);yield return new WaitForSecondsRealtime(.8f);app.Session.TryShoot(Vector2.up);yield return new WaitForSecondsRealtime(.55f);app.Session.TryShoot(new Vector2(-.45f,.89f));yield return new WaitForSecondsRealtime(.7f);yield return Capture("round27-live");
  app.QAStartLevel(0);yield return new WaitForSecondsRealtime(.8f);app.QAResult(true);foreach(float time in new[]{.6f,1.5f,2.9f,3.25f,3.6f,4.3f,4.7f,5.2f}){var v=FindFirstObjectByType<VictoryPresentation>();while(v!=null&&v.Elapsed<time)yield return null;yield return Capture("victory-"+time.ToString("F2",CultureInfo.InvariantCulture));}
  app.QANavigate("home");yield return AlphaSheets();File.WriteAllText(Path.Combine(directory,"audit-info.txt"),"Displayed geometry and pose screenshots; victory is a non-granting visual preview. Mechanism pose reset to its configured phase for static bounds. Trajectory fixture begins at source PTS3.083 first observed point; gravity unchanged; source launch time is not asserted. Alpha sheets are diagnostic overlays.\nFixedDelta="+F(Time.fixedDeltaTime)+"\nResolution="+Screen.width+"x"+Screen.height+"\n");
  yield return null;Application.Quit();
 }
```

### AlphaSheets · L39

方法 SHA-256：`7e7eec0aa32e917670fd6af374fc17c62a6b7dbba832bd211b0d7147d0e702a2`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Round4Audit.AlphaSheets()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Round4Audit.cs:34` · `Round4Audit.Start` → `AlphaSheets()`

```csharp
IEnumerator AlphaSheets(){string[] keys={"home_background_empty","home_hero_group","victory_cake","victory_partyhat","victory_confetti","anticipate","celebrate","settle","logo_backing","logo_letter_B","logo_star_O","logo_letters_UNCE","logo_letters_BUSTERS","coin_single","coin_pile","badge_spade","settings_gold","home_nav","home_simple","ad_tv","shop_row","ranking_row","ranking_row_selected","tab_cyan","tab_purple","tab_orange","well_done"};var canvas=FindFirstObjectByType<Canvas>();for(int sheet=0;sheet<3;sheet++){var root=UICraft.Root(canvas.transform,"Round4 alpha diagnostic");for(int col=0;col<3;col++){Color color=col==0?Color.white:col==1?new Color(.04f,.08f,.16f):new Color(1,.2f,.6f);UICraft.Box(root,"Contrast background",(col-1)*360,0,360,2340,color);for(int row=0;row<9;row++){string key=keys[sheet*9+row];float y=1030-row*245;UICraft.Art(root,"round4/"+key,(col-1)*360,y,310,190);UICraft.Label(root,key,(col-1)*360,y+105,350,35,18,col==0?Color.black:Color.white);}}yield return Capture("alpha-"+sheet);Destroy(root.gameObject);yield return null;}}
```

### Capture · L40

方法 SHA-256：`895b33311f880d47744392bd91ab7f073472c51871cd7029f892495692d9c4ed`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Round4Audit.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Round4Audit.cs:17` · `Round4Audit.Start` → `Capture("home")`
- `Assets/Scripts/Round4Audit.cs:17` · `Round4Audit.Start` → `Capture("shop")`
- `Assets/Scripts/Round4Audit.cs:17` · `Round4Audit.Start` → `Capture("ranking")`
- `Assets/Scripts/Round4Audit.cs:18` · `Round4Audit.Start` → `Capture("round27-initial")`
- `Assets/Scripts/Round4Audit.cs:32` · `Round4Audit.Start` → `Capture("round27-live")`
- `Assets/Scripts/Round4Audit.cs:33` · `Round4Audit.Start` → `Capture("victory-"+time.ToString("F2",CultureInfo.InvariantCulture))`
- `Assets/Scripts/Round4Audit.cs:39` · `Round4Audit.AlphaSheets` → `Capture("alpha-"+sheet)`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var t=ScreenCapture.CaptureScreenshotAsTexture();File.WriteAllBytes(Path.Combine(directory,name+".png"),t.EncodeToPNG());Destroy(t);}
```

## 对象层级和素材入口

- L39：`UICraft.Root(canvas.transform,"Round4 alpha diagnostic")`
- L39：`UICraft.Box(root,"Contrast background",(col-1)*360,0,360,2340,color)`
- L39：`UICraft.Art(root,"round4/"+key,(col-1)*360,y,310,190)`
- L39：`UICraft.Label(root,key,(col-1)*360,y+105,350,35,18,col==0?Color.black:Color.white)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
string directory;
```
```csharp
AppUI app;
```
```csharp
StreamWriter trace;
```
```csharp
float zero;
```
```csharp
BallBody tracked;
```
```csharp
public static string ContactFile;
```
```csharp
public static float ContactZero;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
