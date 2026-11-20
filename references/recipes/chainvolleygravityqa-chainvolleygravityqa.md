# ChainVolleyGravityQA · ChainVolleyGravityQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChainVolleyGravityQA.cs`
文件 SHA-256：`d870c3387c25f6d66408025fbdd69aacc464cea78f4f3b65375ef3e1966336ae`
[原始源码快照](../source/Assets/Scripts/ChainVolleyGravityQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Level · L21

方法 SHA-256：`970f17f6e4c0d99047d2c7cb0a5d8f1ccd3921e7a62cbd9f1491c19ccb920213`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainVolleyGravityQA.Level()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainVolleyGravityQA.cs:25` · `ChainVolleyGravityQA.Start` → `Level()`
- `Assets/Scripts/ChainVolleyGravityQA.cs:39` · `ChainVolleyGravityQA.Start` → `Level()`
- `Assets/Scripts/ChainVolleyGravityQA.cs:47` · `ChainVolleyGravityQA.Start` → `Level()`

```csharp
IEnumerator Level(){app.QAStartLevel(53);yield return app.QAWaitForLevel();g=app.Session;yield return new WaitForSecondsRealtime(.45f);}
```

### Start · L22

方法 SHA-256：`86ae24bf4672e27a9c077600bf31341f2f8db9d0063d85f67630d81d869a2d4b`
时间来源：WaitForSecondsRealtime
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.ChainVolleyGravityQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];if(Array.IndexOf(args,"--qa-height")>=0)height=int.Parse(args[Array.IndexOf(args,"--qa-height")+1]);Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");File.WriteAllText(Path.Combine(dir,"flight-traces.csv"),"shot,tick,active,pending,effects,falling,refills,steps\n");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();app.QASetViewport(new Vector2Int(540,height));app.Session.QAIgnoreFocusLoss=true;yield return Level();Check(g.ChainGridValid()&&g.ChainOccupiedCells()==81,"opening remains a full pixel board");yield return Capture("00-full-opening");
  var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  var fixedPositions=Positions();Check(g.TryShoot(Vector2.up),"five-ball launch accepted");Steps(1);Check(g.ActiveBalls==1&&g.PendingBurstBalls==4&&g.Shots==19,"one launch spends one step and leaves four balls waiting");
  foreach(int c in new[]{3,4,5})Hit(g.CollectionBricks.First(b=>b.GetComponent<ChainGridPiece>().Col==c&&b.GetComponent<ChainGridPiece>().Row==5));
  Steps(3);Check(g.ChainRefills==0&&!g.ChainFalling&&g.ChainOccupiedCells()<=78,"destroyed cells stay empty while later balls are pending");Check(Unmoved(fixedPositions),"surviving bricks stay in their original positions during the burst");Call("BeginChainGravity");Check(!g.ChainFalling&&g.ChainRefills==0,"gravity entry defensively rejects a live or pending volley");
  int n=0;while(g.PendingBurstBalls>0&&n++<80)Steps(1);Check(g.SpawnCount==5&&g.PendingBurstBalls==0&&g.ActiveBalls>0,"all five balls can launch without gravity pausing the burst");Check(g.ChainRefills==0&&!g.ChainFalling&&Unmoved(fixedPositions),"finishing ball spawning does not count as finishing the volley");Check(Balls().All(b=>b.Body.simulated),"live balls are never frozen for mid-flight refill");
  var live=Balls();foreach(var ball in live.Skip(1))g.LoseBall(ball,ball.Body.position);Check(g.ActiveBalls==1,"fixture keeps the last ball alive");for(int i=0;i<10;i++)Call("TickChainCollection",.12f);Check(g.ChainRefills==0&&!g.ChainFalling&&Unmoved(fixedPositions),"even one remaining ball prevents falling and supply");Check(!g.TryShoot(Vector2.up)&&g.Shots==19,"next launch remains blocked without spending a step");yield return Capture("01-holes-with-last-ball-in-flight");
  // Complete the volley, but keep one finite chain effect alive to verify resolution ordering.
  foreach(var ball in Balls())g.LoseBall(ball,ball.Body.position);Call("BeginChainVolley");for(int i=0;i<18;i++)Call("RecordChainRatingHit");
  var survivors=g.CollectionBricks.Where(b=>b.Alive).ToArray();var oldTools=new HashSet<ChainTool>(g.ChainTools);int generated=g.ChainGeneratedCount;
  Call("PowerLine",Vector2.zero,Vector2.right,Color.cyan,.1f,.4f);Call("TickChainCollection",.1f);Check(g.ActiveBalls==0&&g.PendingBurstBalls==0&&g.ChainEffectsPending&&g.ChainRefills==0,"after the last return, active chain effects still delay refill");Call("TickChainCollection",.1f);Check(g.ChainRefills==0&&!g.ChainFalling,"chain effects cannot be cut off by gravity");Call("TickChainCollection",.25f);
  Check(g.ChainFalling&&g.ChainRefills==1&&g.ActiveBalls==0&&g.PendingBurstBalls==0,"one refill begins only after balls and effects are finished");Check(g.ChainGeneratedCount-generated==3,"highest rating rewards enter in that same refill");Check(survivors.All(b=>b.Alive),"existing holes host rewards without deleting extra ordinary bricks");Check(g.CollectionBricks.Where(b=>b.Alive).All(b=>!b.GetComponent<BoxCollider2D>().enabled),"colliders are disabled only for the post-volley movement");Check(!g.TryShoot(Vector2.up)&&g.Shots==19,"settlement cannot consume another launch step");yield return Capture("02-post-volley-settling");
  g.SetPaused(true);var moving=Positions();Steps(12);Check(g.ChainFalling&&Unmoved(moving),"pause freezes the post-volley fall");g.SetPaused(false);Steps(100);Check(g.ChainGridValid()&&g.ChainRefills==1&&g.CollectionReady,"one combined refill ends in a complete ready board");Check(g.ChainTools.Where(t=>!oldTools.Contains(t)).Select(t=>t.Kind).OrderBy(k=>k).SequenceEqual(new[]{1,2,3}),"the final reward packet is laser, bomb and link exactly once");Check(g.Shots==19&&g.ChainSafeRepositions==0,"settlement neither spends a step nor repositions any ball");Steps(60);Check(g.ChainRefills==1&&g.ChainGeneratedCount-generated==3,"idle frames cannot duplicate refill or rewards");yield return Capture("03-one-complete-refill");
  // Real flight, including roof banks, must never move the grid while any ball remains.
  Physics2D.simulationMode=mode;yield return Level();Physics2D.simulationMode=SimulationMode2D.Script;
  int shot=0;foreach(float angle in new[]{90f,74f,106f}){
   var before=Positions();int refills=g.ChainRefills;bool flightStable=true,fallAfterReturn=true;float a=angle*Mathf.Deg2Rad;Check(g.TryShoot(new Vector2(Mathf.Cos(a),Mathf.Sin(a))),"physical volley accepted "+shot);n=0;
   do{Steps(1);n++;if(g.ActiveBalls>0||g.PendingBurstBalls>0)flightStable&=g.ChainRefills==refills&&!g.ChainFalling&&Unmoved(before);if(g.ChainFalling)fallAfterReturn&=g.ActiveBalls==0&&g.PendingBurstBalls==0&&!g.ChainEffectsPending;if(n%10==0)File.AppendAllText(Path.Combine(dir,"flight-traces.csv"),$"{shot},{n},{g.ActiveBalls},{g.PendingBurstBalls},{g.ChainEffectsPending},{g.ChainFalling},{g.ChainRefills},{g.Shots}\n");if(g.RecallAvailable)g.RecallBalls();if(n%250==0)yield return null;}while(g.IsPlaying&&(!g.CollectionReady||n<2)&&n<1500);
   Check(flightStable,"all survivors remain fixed for the full real volley "+shot);Check(fallAfterReturn,"actual gravity waits for every return and effect "+shot);Check(n<1500&&g.ChainGridValid()&&g.ChainRefills-refills<=1,"actual volley resolves with at most one complete refill "+shot);shot++;
  }
  Check(g.Shots==17&&g.ChainSafeRepositions==0,"three real launches cost three steps without flight warps");Physics2D.simulationMode=mode;yield return Capture("04-after-three-real-volleys");
  // On the last step, failure still waits for returns and settlement.
  yield return Level();Physics2D.simulationMode=SimulationMode2D.Script;typeof(RuleState).GetField("<Shots>k__BackingField",Hidden).SetValue(Rules,1);Check(g.TryShoot(Vector2.up),"last launch step can be used");Steps(1);Check(g.Shots==0&&g.IsPlaying&&g.PendingBurstBalls>0&&!g.ChainFalling,"zero remaining steps does not end a still-running volley");n=0;while(g.IsPlaying&&n++<1500)Steps(1);Check(!g.IsPlaying&&!Rules.Won&&g.ActiveBalls==0&&g.PendingBurstBalls==0,"unfinished collection loses only after its last volley finishes");
  Physics2D.simulationMode=mode;app.QANavigate("home");yield return null;Check(!g.ChainFalling&&FindObjectsByType<ChainGridPiece>(FindObjectsSortMode.None).Length==0,"leaving the stage clears settlement state");
  File.AppendAllText(Path.Combine(dir,"checks.txt"),$"RESULT checks={checks} failures={failures} errors={errors}\n");Application.Quit(failures+errors==0?0:1);
 }
```

### Capture · L51

方法 SHA-256：`7047d503bba25f8ca0d2b81a4ebbd1c7650492ef7332c23b085e83bc9978ffb0`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainVolleyGravityQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainVolleyGravityQA.cs:25` · `ChainVolleyGravityQA.Start` → `Capture("00-full-opening")`
- `Assets/Scripts/ChainVolleyGravityQA.cs:31` · `ChainVolleyGravityQA.Start` → `Capture("01-holes-with-last-ball-in-flight")`
- `Assets/Scripts/ChainVolleyGravityQA.cs:36` · `ChainVolleyGravityQA.Start` → `Capture("02-post-volley-settling")`
- `Assets/Scripts/ChainVolleyGravityQA.cs:37` · `ChainVolleyGravityQA.Start` → `Capture("03-one-complete-refill")`
- `Assets/Scripts/ChainVolleyGravityQA.cs:45` · `ChainVolleyGravityQA.Start` → `Capture("04-after-three-real-volleys")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var cam=Camera.main;var target=new RenderTexture(540,height,24);target.Create();var old=cam.targetTexture;cam.targetTexture=target;cam.rect=new Rect(0,0,1,1);cam.aspect=540f/height;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;Canvas.ForceUpdateCanvases();var design=(RectTransform)typeof(AppUI).GetField("designRoot",Hidden).GetValue(app);var safe=(RectTransform)design.parent;design.localScale=Vector3.one*Mathf.Min(safe.rect.width/1080f,safe.rect.height/2340f);Canvas.ForceUpdateCanvases();cam.Render();var previous=RenderTexture.active;RenderTexture.active=target;var texture=new Texture2D(540,height,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,540,height),0,0);texture.Apply();File.WriteAllBytes(Path.Combine(dir,name+".png"),texture.EncodeToPNG());RenderTexture.active=previous;cam.targetTexture=old;Destroy(texture);target.Release();Destroy(target);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
const BindingFlags Hidden=BindingFlags.Instance|BindingFlags.NonPublic;
```
```csharp
AppUI app;
```
```csharp
GameSession g;
```
```csharp
string dir;
```
```csharp
int checks,failures,errors,height=1170;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
