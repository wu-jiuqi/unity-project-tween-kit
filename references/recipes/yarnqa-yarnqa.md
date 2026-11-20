# YarnQA · YarnQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/YarnQA.cs`
文件 SHA-256：`59cfd22b12b7d74d408599db515369d3cb9d92d088b6572c6dda9cae6439da8f`
[原始源码快照](../source/Assets/Scripts/YarnQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L24

方法 SHA-256：`ed87599d5a9c3c7ab9046cfed0a37bce35b7a2f1eaebe44efd4da70cf7cef61f`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.YarnQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/YarnQA.cs:61` · `YarnQA.Start` → `Capture("aim-"+angle.ToString("D3"))`
- `Assets/Scripts/YarnQA.cs:78` · `YarnQA.Start` → `Capture("contact-color-"+level)`
- `Assets/Scripts/YarnQA.cs:89` · `YarnQA.Start` → `Capture("integrated-level-1")`
- `Assets/Scripts/YarnQA.cs:91` · `YarnQA.Start` → `Capture("motion-"+i.ToString("D3"))`

```csharp
IEnumerator Capture(string name){
  yield return null;Canvas.ForceUpdateCanvases();Camera.main.Render();var previous=RenderTexture.active;RenderTexture.active=capture;
  var image=new Texture2D(capture.width,capture.height,TextureFormat.RGB24,false);image.ReadPixels(new Rect(0,0,capture.width,capture.height),0,0);image.Apply();
  File.WriteAllBytes(Path.Combine(dir,name+".png"),image.EncodeToPNG());RenderTexture.active=previous;Destroy(image);
 }
```

### Start · L29

方法 SHA-256：`60a4fe755063b7d03e30873372f27c4d8b5bcf9c5db4454c810853c1692aa17e`
时间来源：WaitForSecondsRealtime
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.YarnQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);
  File.WriteAllText(Path.Combine(dir,"checks.txt"),"Yarn V20 actual Windows player; isolated profile; GPU captures are not phone tests.\n");Application.logMessageReceived+=Log;
  yield return new WaitForSecondsRealtime(.8f);app=FindFirstObjectByType<AppUI>();Check(app!=null&&RuntimeQA.IsIsolated,"isolated real application");
  capture=new RenderTexture(540,1170,24);capture.Create();Camera.main.targetTexture=capture;
  var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=Camera.main;canvas.planeDistance=1;
  app.QASetViewport(new Vector2Int(540,1170));yield return null;Canvas.ForceUpdateCanvases();app.QASetViewport(new Vector2Int(540,1170));
  var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  for(int level=0;level<4;level++){
   app.QAStartLevel(level);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.7f);
   var game=app.Session;game.SetPaused(false);var cat=FindFirstObjectByType<RearCatLauncher>();
   Check(cat!=null&&cat.FrameIndex==0,"whole rear cat initialized in idle L"+level);
   var root=GameObject.Find("CannonRoot").transform;var pivot=root.Find("AimPivot");var loaded=pivot.Find("LoadedBall").GetComponent<SpriteRenderer>();
   Check(Vector2.Distance(root.position,game.LauncherPosition)<.00001f,"fixed root equals launcher position L"+level);
   Check(cat.transform.localPosition==Vector3.zero&&cat.transform.localScale==Vector3.one,"cat has no local offset/scale compensation L"+level);
   Check(loaded.sprite.name=="ball_yarn_"+YarnArt.ColorName(level),"loaded yarn matches level color L"+level);
   Check(loaded.transform.localPosition==(Vector3)(Vector2.up*GameSession.MuzzleOffset),"loaded socket is fixed local muzzle L"+level);
   Check(Mathf.Abs(loaded.sprite.bounds.size.x*.875f-FlightGeometry.Radius*2)<.0001f,"visible yarn diameter enlarged by 35 percent L"+level);
   Check(GameObject.Find("LauncherRear")==null&&GameObject.Find("LauncherFront")==null&&GameObject.Find("Fixed base guide")==null,"old bowl and guide removed L"+level);
   foreach(var b in FindObjectsByType<BrickBody>(FindObjectsSortMode.None)){
    Check(b.Face.sprite==JellyBrickArt.Rest,"existing jelly rest sprite retained L"+level+" brick="+b.Id);b.GetComponent<Collider2D>().enabled=false;
   }
   Physics2D.SyncTransforms();
   foreach(int angle in new[]{12,45,90,135,168}){
    var d=new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad),Mathf.Sin(angle*Mathf.Deg2Rad));var anchor=root.position;
    game.SetAim(d);yield return new WaitForSecondsRealtime(.58f);
    Check(root.position==anchor&&cat.transform.localPosition==Vector3.zero&&cat.transform.localScale==Vector3.one,"all aiming frames preserve fixed pivot L"+level+" angle="+angle);
    Check(Mathf.Abs(Mathf.DeltaAngle(pivot.localEulerAngles.z,angle-90))<.001f,"same whole cat rotates only by aim angle L"+level+" angle="+angle);
    Check(Vector2.Distance(loaded.transform.position,game.SpawnPosition(d))<.0001f,"loaded yarn exactly follows SpawnPosition L"+level+" angle="+angle);
    var contact=cat.transform.TransformPoint(Vector2.up*GameSession.MuzzleOffset);
    Check(Vector2.Distance(contact,game.SpawnPosition(d))<.0001f,"registered contact matches physical spawn L"+level+" angle="+angle);
    Check(cat.FrameIndex==5,"aim prep holds before contact without automatic firing L"+level+" angle="+angle);
    if(level==0)yield return Capture("aim-"+angle.ToString("D3"));
   }
   var up=Vector2.up;int ammo=game.Shots;Check(game.TryShoot(up),"shoot accepted during preparation L"+level);
   Check(game.SpawnCount==0&&game.Shots==ammo,"queued shot still follows original fixed step L"+level);
   game.QAAdvanceOneStep();
   Check(game.SpawnCount==1&&game.Shots==ammo-1,"real ball spawns immediately with no540ms animation delay L"+level);
   Check(cat.FrameIndex==6&&Mathf.Abs(cat.ClockMs-YarnArt.Calibration.contact_ms)<.01f,"actual spawn enters contact frame immediately L"+level);
   var first=FindFirstObjectByType<BallBody>();
   Check(first.GetComponent<CircleCollider2D>().radius==FlightGeometry.Radius,"collider radius matches visible yarn L"+level);
   Check(first.transform.Find("BallVisual").GetComponent<SpriteRenderer>().sprite==YarnArt.Ball(level),"physical yarn color matches loaded yarn L"+level);
   var trails=FindObjectsByType<FlightTrail>(FindObjectsSortMode.None);bool strand=false;
   foreach(var t in trails)strand|=t.GetComponent<LineRenderer>().sharedMaterial==YarnArt.Strand(level);
   Check(strand,"flight uses matching continuous yarn strand texture L"+level);
   game.SetPaused(true);int frame=cat.FrameIndex;float clock=cat.ClockMs;var pos=first.Body.position;
   yield return new WaitForSecondsRealtime(.25f);
   Check(cat.FrameIndex==frame&&cat.ClockMs==clock&&first.Body.position==pos,"pause freezes cat animation and real ball L"+level);
   Check(!loaded.gameObject.activeSelf,"just-fired loaded ball stays hidden while paused L"+level);
   yield return Capture("contact-color-"+level);
   game.SetPaused(false);for(int i=0;i<45;i++)game.QAAdvanceOneStep();
   Check(game.SpawnCount==game.CurrentLevel.volleyCount,"full original burst completes independently of cat animation L"+level);
   foreach(var b in FindObjectsByType<BallBody>(FindObjectsSortMode.None))game.LoseBall(b,b.Body.position);
   int spawned=game.SpawnCount;Check(game.TryShoot(up),"new shot accepted before full animation has ended L"+level);game.QAAdvanceOneStep();
   Check(game.SpawnCount==spawned+1&&cat.FrameIndex==6,"rapid follow-up reenters contact with no visual offset L"+level);
   Check(root.position==anchorFor(game)&&cat.transform.localPosition==Vector3.zero&&cat.transform.localScale==Vector3.one,"shooting adds no recoil translation or scaling L"+level);
   game.StopLevel();yield return null;
  }
  Physics2D.simulationMode=mode;
  app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.6f);app.Session.SetPaused(false);app.Session.SetAim(Vector2.up);
  yield return new WaitForSecondsRealtime(.58f);yield return Capture("integrated-level-1");
  measureHeads=true;app.Session.TryShoot(new Vector2(.28f,1));
  for(int i=0;i<40;i++){yield return null;if(i%2==0)yield return Capture("motion-"+i.ToString("D3"));}
  measureHeads=false;Check(headSamples>0&&maxHeadError<.003f,"rendered yarn strand remains attached during interpolation samples="+headSamples+" maxError="+maxHeadError);
  File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failed+" errors="+errors+"\n");Application.logMessageReceived-=Log;
  Application.Quit(failed==0&&errors==0?0:4);
 }
```

## 对象层级和素材入口

- L41：`GameObject.Find("CannonRoot")`
- L41：`root.Find("AimPivot")`
- L41：`pivot.Find("LoadedBall")`
- L47：`GameObject.Find("LauncherRear")`
- L70：`first.transform.Find("BallVisual")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
string dir;
```
```csharp
int checks,failed,errors;
```
```csharp
AppUI app;
```
```csharp
RenderTexture capture;
```
```csharp
bool measureHeads;
```
```csharp
int headSamples;
```
```csharp
float maxHeadError;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
