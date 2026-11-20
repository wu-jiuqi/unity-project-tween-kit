# ChainGuardQA · ChainGuardQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChainGuardQA.cs`
文件 SHA-256：`bc53a7900b28c8c1ab49a29bd465279c848acf3c30483d838d7eaf3c26deefc1`
[原始源码快照](../source/Assets/Scripts/ChainGuardQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L22

方法 SHA-256：`04ae88757f092b7e6fd2d7f00a21efa6d07324f745eebd1a4b9d032d095cf646`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainGuardQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];if(Array.IndexOf(args,"--qa-height")>=0)height=int.Parse(args[Array.IndexOf(args,"--qa-height")+1]);Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();app.QASetViewport(new Vector2Int(width,height));app.Session.QAIgnoreFocusLoss=true;
  app.QAStartLevel(53);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.5f);g=app.Session;
  Check(g.CollectionBricks.Count(b=>b.Definition.kind==BrickKind.Armor)==2&&g.CollectionBricks.Count(b=>b.Definition.kind==BrickKind.RearWeak)==3,"two armor entrances and three lower rear guards");
  Check(g.CollectionBricks.Where(b=>b.Definition.juiceGroup==1).All(b=>b.CurrentHealth==6)&&g.CollectionBricks.Where(b=>b.Definition.juiceGroup==2).All(b=>b.CurrentHealth==5),"container health 6 and 5");yield return Capture("00-guards-opening");
  var armor=g.CollectionBricks.First(b=>b.Definition.kind==BrickKind.Armor);Hit(armor,Vector2.up,(Vector2)armor.transform.position+Vector2.down*.41f);var rear=g.CollectionBricks.First(b=>b.Definition.kind==BrickKind.RearWeak);Hit(rear,Vector2.down,(Vector2)rear.transform.position+Vector2.up*.41f);yield return new WaitForSecondsRealtime(.1f);yield return Capture("01-shell-open-and-rear-hit");
  // Controlled front-blocking beam, then a beam entering the green top.
  g.StartLevel(53);Empty();rear=Add(Vector2.zero,BrickKind.RearWeak);Add(new Vector2(0,1.5f),BrickKind.Normal,4,1);var needle=g.SpawnChainTool(1,new Vector2(0,-1.5f));yield return new WaitForSecondsRealtime(.4f);g.ActivateChainTool(needle,Vector2.up);yield return new WaitForSecondsRealtime(.04f);yield return Capture("02-front-blocks-needle");
  needle=g.SpawnChainTool(1,new Vector2(0,1));yield return new WaitForSecondsRealtime(.4f);g.ActivateChainTool(needle,Vector2.down);yield return new WaitForSecondsRealtime(.04f);yield return Capture("03-rear-needle-damage");
  app.QANavigate("home");g=new GameObject("Guard rule fixtures").AddComponent<GameSession>();g.QAIgnoreFocusLoss=true;var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  Reset();rear=Add(Vector2.zero,BrickKind.RearWeak);for(int i=0;i<10;i++)Hit(rear,Vector2.up,Vector2.down*.41f,99);Check(rear.Alive&&rear.CurrentHealth==4&&g.HitCount==0&&g.ChainBlocks==10,"front immunity rejects repeated and oversized ball damage");
  Hit(rear,Vector2.left,Vector2.right*.41f,99);Check(rear.CurrentHealth==4,"side immunity");Hit(rear,Vector2.up,Vector2.up*.41f,99);Check(rear.CurrentHealth==4,"a fake top point with upward incoming direction is rejected");
  Hit(rear,Vector2.down,Vector2.right*.41f,99);Check(rear.CurrentHealth==4,"downward direction does not make a side contact a rear hit");
  Hit(rear,Vector2.down,Vector2.up*.41f);Check(rear.CurrentHealth==1&&g.ChainRearHits==1,"real rear ball hit deals three damage");Hit(rear,Vector2.down,Vector2.up*.41f);Check(!rear.Alive,"second rear ball hit destroys four-health guard");Check(g.ChainGeneratedCount==0,"guard destruction does not count as a normal skill brick");
  Reset();rear=Add(Vector2.zero,BrickKind.RearWeak);Hit(rear,Vector2.up,Vector2.down*.41f,1,false);Check(rear.CurrentHealth==4,"mini ball cannot damage front");Hit(rear,Vector2.down,Vector2.up*.41f,1,false);Check(rear.CurrentHealth==3,"mini rear hit deals one damage");
  Reset();armor=Add(Vector2.zero,BrickKind.Armor);Hit(armor,Vector2.up,Vector2.down*.41f,999);Check(!armor.ShellIntact&&armor.CurrentHealth==4&&armor.transform.Find("Armor shell").gameObject.activeSelf==false,"armor first event breaks shell without damage overflow");Hit(armor,Vector2.up,Vector2.down*.41f,1);Check(armor.CurrentHealth==3,"next independent impact damages armor body");
  Reset();rear=Add(Vector2.zero,BrickKind.RearWeak);var behind=Add(new Vector2(0,1.2f),BrickKind.Normal,4,1);needle=g.SpawnChainTool(1,new Vector2(0,-2));var chained=g.SpawnChainTool(2,new Vector2(0,2));Arm();g.ActivateChainTool(needle,Vector2.up);Check(rear.CurrentHealth==4&&behind.CurrentHealth==4&&g.ChainTools.Contains(chained),"front plate stops needle before targets and tools behind it");
  needle=g.SpawnChainTool(1,new Vector2(0,.9f));Arm();g.ActivateChainTool(needle,Vector2.down);Check(rear.CurrentHealth==2,"needle from above deals two damage to weak rear");
  Reset();rear=Add(Vector2.zero,BrickKind.RearWeak);needle=g.SpawnChainTool(1,new Vector2(-2,0));Arm();g.ActivateChainTool(needle,Vector2.right);Check(rear.CurrentHealth==4&&g.ChainBlocks==1,"side-entry needle is blocked without rear damage");
  Reset();rear=Add(Vector2.zero,BrickKind.RearWeak);yield return new WaitForSecondsRealtime(.4f);var drum=g.SpawnChainTool(2,new Vector2(0,-1));Arm();g.ActivateChainTool(drum,Vector2.up);Steps(6);Check(rear.CurrentHealth==4&&g.ChainBlocks>0,"real swept mini ball is blocked by the front");
  Reset();rear=Add(Vector2.zero,BrickKind.RearWeak);yield return new WaitForSecondsRealtime(.4f);drum=g.SpawnChainTool(2,new Vector2(0,1));Arm();g.ActivateChainTool(drum,Vector2.down);Steps(6);Check(rear.CurrentHealth==3&&g.ChainRearHits==1,"real swept mini ball hits the rear for one damage");
  Reset();armor=Add(Vector2.zero,BrickKind.Armor);behind=Add(new Vector2(0,1.2f),BrickKind.Normal,4,1);needle=g.SpawnChainTool(1,new Vector2(0,-2));Arm();g.ActivateChainTool(needle,Vector2.up);Check(!armor.ShellIntact&&armor.CurrentHealth==4&&behind.CurrentHealth==4,"needle shell-breaking event stops at armor");needle=g.SpawnChainTool(1,new Vector2(0,-2));Arm();g.ActivateChainTool(needle,Vector2.up);Check(armor.CurrentHealth==2&&behind.CurrentHealth==4,"needle cannot bypass surviving armor body");needle=g.SpawnChainTool(1,new Vector2(0,-2));Arm();g.ActivateChainTool(needle,Vector2.up);Check(!armor.Alive&&behind.CurrentHealth==2,"needle continues after destroying open armor body");
  Reset();rear=Add(Vector2.zero,BrickKind.RearWeak);g.SetPaused(true);Hit(rear,Vector2.down,Vector2.up*.41f);Check(rear.CurrentHealth==4,"pause blocks weak-point damage");g.SetPaused(false);
  Set("<CollectionRound>k__BackingField",2);Call("PlanChainRow");var kinds=(BrickKind[])g.NextChainRowKinds.Clone();var health=(int[])g.NextChainRowHealth.Clone();Check(kinds.Contains(BrickKind.Armor),"armor is included in cached preview");Set("collectionRoundPending",true);Steps(80);Check(g.CollectionBricks.Where(b=>b.Alive&&Mathf.Abs(b.transform.position.y-CollectionLevels.Top)<.01f).All(b=>b.Definition.kind==kinds[b.Definition.referenceCol]&&b.CurrentHealth==health[b.Definition.referenceCol]),"preview special role and health match spawned row");
  Reset();Set("<CollectionRound>k__BackingField",3);Call("PlanChainRow");Check(g.NextChainRowKinds.Contains(BrickKind.RearWeak),"rear guard supply is previewed");
  Reset();rear=Add(new Vector2(0,-3.7f),BrickKind.RearWeak);needle=g.SpawnChainTool(1,new Vector2(0,-3.8f));Arm();Set("collectionRoundPending",true);Steps(100);Check(!g.IsPlaying&&!Rules.Won&&rear.CurrentHealth==4,"bottom auto needle cannot rescue a front-facing guard");
  // Real ball collision callback: front remains intact; a roof bank reaches the back.
  g.StartLevel(53);Empty();rear=Add(Vector2.zero,BrickKind.RearWeak);yield return new WaitForSecondsRealtime(.4f);g.TryShoot(Vector2.up);Steps(120);Check(rear.CurrentHealth==4&&g.ChainBlocks>0,"actual upward volley reflects from immune front");
  g.StartLevel(53);Empty();rear=Add(new Vector2(1.64f,0),BrickKind.RearWeak);yield return new WaitForSecondsRealtime(.4f);Vector2 reflected=new Vector2(8.2f-1.64f,14.98f-.41f);Check(g.TryShoot(reflected-g.LauncherPosition),"roof-bank shot accepted");int n=0;do{Steps(1);n++;if(g.RecallAvailable)g.RecallBalls();}while(g.IsPlaying&&(!g.CollectionReady||n<2)&&n<3000);Check(g.ChainRearHits>0&&!rear.Alive,"real side-and-roof bank hits rear and destroys the guard");
  g.StartLevel(0);Check(g.ChainBlocks==0&&g.ChainRearHits==0&&g.ChainArmorBreaks==0,"special counters reset on legacy stage");g.StopLevel();Destroy(g.gameObject);Physics2D.simulationMode=mode;
  File.AppendAllText(Path.Combine(dir,"checks.txt"),$"RESULT checks={checks} failures={failures} errors={errors}\n");Application.Quit(failures+errors==0?0:1);
 }
```

### Capture · L56

方法 SHA-256：`95b1a8c1ef930067c6708170d0e8dad02346794ba542de51f426b5cd46bc2b08`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainGuardQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainGuardQA.cs:28` · `ChainGuardQA.Start` → `Capture("00-guards-opening")`
- `Assets/Scripts/ChainGuardQA.cs:29` · `ChainGuardQA.Start` → `Capture("01-shell-open-and-rear-hit")`
- `Assets/Scripts/ChainGuardQA.cs:31` · `ChainGuardQA.Start` → `Capture("02-front-blocks-needle")`
- `Assets/Scripts/ChainGuardQA.cs:32` · `ChainGuardQA.Start` → `Capture("03-rear-needle-damage")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var cam=Camera.main;var target=new RenderTexture(width,height,24);target.Create();var old=cam.targetTexture;cam.targetTexture=target;cam.rect=new Rect(0,0,1,1);cam.aspect=(float)width/height;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;Canvas.ForceUpdateCanvases();var design=(RectTransform)typeof(AppUI).GetField("designRoot",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).GetValue(app);var safe=(RectTransform)design.parent;design.localScale=Vector3.one*Mathf.Min(safe.rect.width/1080f,safe.rect.height/2340f);Canvas.ForceUpdateCanvases();cam.Render();var prev=RenderTexture.active;RenderTexture.active=target;var texture=new Texture2D(width,height,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,width,height),0,0);texture.Apply();File.WriteAllBytes(Path.Combine(dir,name+".png"),texture.EncodeToPNG());RenderTexture.active=prev;cam.targetTexture=old;Destroy(texture);target.Release();Destroy(target);}
```

## 对象层级和素材入口

- L39：`armor.transform.Find("Armor shell")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(){g.StartLevel(53);Empty();}
```

## 源字段与依赖

```csharp
const BindingFlags Hidden=BindingFlags.Instance|BindingFlags.NonPublic;
```
```csharp
GameSession g;
```
```csharp
AppUI app;
```
```csharp
string dir;
```
```csharp
int checks,failures,errors;
```
```csharp
int width=540,height=1170;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
