# ChainGravityQA · ChainGravityQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChainGravityQA.cs`
文件 SHA-256：`679fccbcd051d668e83b30210b9aecec37e798085bc3b8bafeac449b1399339f`
[原始源码快照](../source/Assets/Scripts/ChainGravityQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L25

方法 SHA-256：`f3402b9812938fdda79da57b2c3efa077e34d4f48e15d722124eac3b4ed5b646`
时间来源：WaitForSecondsRealtime
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.ChainGravityQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];if(Array.IndexOf(args,"--qa-height")>=0)height=int.Parse(args[Array.IndexOf(args,"--qa-height")+1]);Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();app.QASetViewport(new Vector2Int(width,height));app.Session.QAIgnoreFocusLoss=true;app.QAStartLevel(53);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.6f);g=app.Session;
  Check(g.ChainGridValid()&&g.ChainOccupiedCells()==81,"opening fills all 81 cells exactly once");
  Check(ChainGridRules.Cat.Length==9&&ChainGridRules.Cat.All(r=>r.Length==9),"authored pixel cat is a complete 9x9 color map");
  var ordinary=g.CollectionBricks.Where(b=>b.Alive&&b.Definition.kind==BrickKind.Normal&&b.Definition.juiceGroup<=0&&!ChainPowerRules.IsNest(b.Definition)).ToArray();
  Check(ordinary.All(b=>{var p=b.GetComponent<ChainGridPiece>();return b.Definition.colorIndex==ChainGridRules.Pixel(p.Col,p.Row)&&b.Face.sprite==MainlineArt.Frames(b.Definition.colorIndex)[0];}),"ordinary cells render actual palette sprites matching the pixel map");
  Check(ordinary.Select(b=>b.Definition.colorIndex).Distinct().Count()>=5,"pixel cat uses five distinguishable palette colors");
  Check(g.Shots==20&&!Rules.UnlimitedShots&&g.CurrentLevel.volleyCount==5,"fixed board has twenty five-ball moves");
  Check(g.ChainSupplyCount(1)==8&&g.ChainSupplyCount(2)==6,"visible plus hidden containers cover both collection goals");
  Check(g.CollectionBricks.Count(b=>ChainPowerRules.IsNest(b.Definition))==2&&g.CollectionBricks.Where(b=>ChainPowerRules.IsNest(b.Definition)).All(b=>b.Definition.chainContents.Contains(1)&&b.Definition.chainContents.Contains(2)),"two opening large blocks visibly contain collection blocks");
  Check(!g.GetComponentsInChildren<Transform>(true).Any(t=>t.name=="Container danger line"||t.name=="Next chain row"||t.name=="Next chain slot"),"no old danger line or next-row preview");
  yield return Capture("00-full-pixel-cat");var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  var rear=g.CollectionBricks.First(b=>b.Definition.kind==BrickKind.RearWeak);Check(rear.CurrentHealth==1&&!Hit(rear,999)&&rear.Alive,"rear guard is one HP but remains immune from the front");Check(Hit(rear,1,Vector2.down)&&!rear.Alive,"one actual rear entry immediately shatters the guard");
  Reset();rear=Add(4,4,50,0,BrickKind.RearWeak);Check(!g.ApplyChainImpact(rear,999,Vector2.left,(Vector2)rear.transform.position+Vector2.right*.41f,true)&&rear.CurrentHealth==50,"side overkill cannot bypass the front plate");Check(!Hit(rear,0,Vector2.down)&&rear.Alive,"zero damage is not a rear hit");Check(Hit(rear,1,Vector2.down)&&!rear.Alive,"one rear hit breaks even a legacy high HP guard");
  Reset();rear=Add(4,4,1,0,BrickKind.RearWeak);var bomb=Power(2,4,5);g.ActivateChainTool(bomb,Vector2.up);Check(rear.Alive,"bomb below the plate remains blocked");var laser=Power(1,4,2);g.ActivateChainTool(laser,Vector2.down);Check(!rear.Alive,"a downward laser breaks the exposed rear in one event");
  g.StartLevel(53);var stable=g.CollectionBricks.Where(b=>b.Alive).ToDictionary(b=>b.Id,b=>(Vector2)b.transform.position);Set("collectionRoundPending",true);Steps(3);Check(g.ChainRefills==0&&g.ChainGridValid()&&g.CollectionBricks.All(b=>Vector2.Distance(stable[b.Id],b.transform.position)<.001f),"an empty volley does not push the whole board down or append a row");
  Reset();var top=Add(4,0,4);var middle=Add(4,2,4);var removed=Add(4,8,1);var anchor=Add(2,8,4);int color=top.Definition.colorIndex;Vector2 anchorAt=anchor.transform.position;Hit(removed);Settle();Check(middle.GetComponent<ChainGridPiece>().Row==8&&top.GetComponent<ChainGridPiece>().Row==7,"survivors compact downward in their original column and order");Check(Vector2.Distance(anchor.transform.position,anchorAt)<.001f,"a supported neighboring column stays fixed");Check(top.Definition.colorIndex==color&&top.Face.sprite==MainlineArt.Frames(color)[0],"falling pixels retain their own colors");Check(g.ChainGridValid()&&g.ChainOccupiedCells()==81&&g.CollectionRound==1&&g.Shots==20,"refill restores a full nonoverlapping board immediately without consuming a turn");
  Reset();var durable=Add(4,8,6);Hit(durable,1);Steps(20);Check(durable.CurrentHealth==5&&g.ChainRefills==0&&g.ChainGridReady,"damage without destruction does not cause gravity or refill");
  Reset();for(int r=5;r<9;r++)Add(4,r);Add(3,8);var nest=g.SpawnChainNest(ChainGridRules.Center(3,2,2),new[]{0,0,0,0});Call("ScheduleChainGravity");Settle();Check(nest.GetComponent<ChainGridPiece>().Row==3&&nest.GetComponent<ChainGridPiece>().Size==2,"large block drops rigidly until either support column stops it");Check(g.ChainGridValid()&&g.ChainOccupiedCells()==81,"sealed pockets under a large block refill without overlaps or holes");
  Reset();nest=g.SpawnChainNest(ChainGridRules.Center(2,3,2),new[]{1,2,0,1});Check(g.ChainSupplyCount(1)==2&&g.ChainSupplyCount(2)==1,"hidden collection counts are included before opening a large block");Hit(nest,23);Check(nest.Alive&&nest.CurrentHealth==1&&g.Collected.All(v=>v==0),"damaging a large shell does not collect hidden contents");Hit(nest,999);var kids=g.CollectionBricks.Where(b=>b.Alive).ToArray();Check(kids.Length==4&&kids.Count(b=>b.Definition.juiceGroup==1)==2&&kids.Count(b=>b.Definition.juiceGroup==2)==1&&kids.Count(b=>b.Definition.juiceGroup==0)==1,"large shell releases its exact mixed four-block contents");
  Check(kids.All(b=>b.CurrentHealth==(b.Definition.juiceGroup==1?6:b.Definition.juiceGroup==2?5:4))&&g.ChainNestSplits==1,"new task children keep container HP and receive no shell overkill");Check(g.ChainGridValid(false)&&g.Collected.All(v=>v==0),"four children occupy original cells and must be broken to collect");var fish=kids.First(b=>b.Definition.juiceGroup==1);Hit(fish);Settle();Check(g.Collected[0]==1&&g.ChainGridValid(),"breaking a revealed fish box collects once then refills");Check(g.ChainSupplyCount(1)+g.Collected[0]>=8&&g.ChainSupplyCount(2)>=6,"refill guarantees enough missing task containers including hidden contents");
  Reset();nest=g.SpawnChainNest(ChainGridRules.Center(3,3,2),new[]{0,0,0,0});Hit(nest);Check(g.CollectionBricks.Count(b=>b.Alive)==4&&g.CollectionBricks.Where(b=>b.Alive).All(b=>b.Definition.juiceGroup==0&&b.CurrentHealth==4),"ordinary-only four-block contents remain supported");
  Reset();var stone=Add(4,8,1,0,BrickKind.Stone);Check(!Hit(stone)&&stone.Alive,"stone remains indestructible");var armor=Add(3,8,6,0,BrickKind.Armor);Check(!Hit(armor)&&armor.Alive&&!armor.ShellIntact&&armor.CurrentHealth==6,"armor still requires a separate shell-breaking hit");Check(Hit(armor)&&!armor.Alive,"opened armor can subsequently break");
  Reset();for(int i=0;i<4;i++)Call("RecordChainRatingBreak");Check(g.NextChainRowTools.Count(k=>k>0)==1&&g.ChainTools.Count==0,"Nice queues a refill reward without placing it during flight");Call("ScheduleChainGravity");Settle();Check(g.ChainTools.Count==0,"mid-volley gravity does not prematurely pay the highest-tier reward");Set("collectionRoundPending",true);Steps(160);Check(g.ChainTools.Count==1&&g.ChainTools[0].Kind==1&&g.ChainGridValid()&&g.CollectionRound==2,"queued laser enters through one refill after the volley");int generated=g.ChainGeneratedCount;Steps(80);Check(g.ChainGeneratedCount==generated&&g.NextChainRowTools.All(k=>k==0),"refill rewards are paid only once");
  Reset();Call("ScheduleChainGravity");Settle();for(int i=0;i<18;i++)Call("RecordChainRatingBreak");var goals=g.CollectionBricks.Where(b=>b.Definition.juiceGroup>0).ToArray();Set("collectionRoundPending",true);Steps(160);Check(g.ChainTools.Select(t=>t.Kind).OrderBy(k=>k).SequenceEqual(new[]{1,2,3})&&goals.All(b=>b.Alive),"highest rating inserts all three powers without replacing collection targets");Check(g.ChainGridValid()&&g.ChainRatingBreaks==18,"reward insertion keeps a full board and cannot add indirect rating credit");
  g.StartLevel(53);Check(g.TryShoot(Vector2.up),"timeout test volley launches");Steps(25);Set("chainVolleyAge",ChainGridRules.FlightLimit);Steps(1);Check(g.ActiveBalls==0&&g.ChainAutoReturns==1&&g.Shots==19,"twelve-second auto return closes a persistent volley without charging another move");Steps(150);Check(g.CollectionReady&&g.ChainGridValid(),"automatic return still waits for a valid full-grid settlement");
  // Flight freezes only while the grid is moving, including across user pause/resume.
  app.QAStartLevel(53);yield return app.QAWaitForLevel();g=app.Session;yield return new WaitForSecondsRealtime(.45f);Check(g.TryShoot(new Vector2(.3f,1)),"five-ball volley accepted on the full board");Steps(1);var ball=FindObjectsByType<BallBody>(FindObjectsSortMode.None).First(b=>b.Active&&b.SessionGeneration==g.Generation);float speed=ball.Body.linearVelocity.magnitude;Hit(g.CollectionBricks.First(b=>b.GetComponent<ChainGridPiece>().Col==2&&b.GetComponent<ChainGridPiece>().Row==8));int n=0;while(!g.ChainFalling&&n++<80)Steps(1);Check(g.ChainFalling&&!ball.Body.simulated,"gravity suspends the live ball rigidbody");var at=ball.Body.position;int spawned=g.SpawnCount;Steps(4);Check(Vector2.Distance(at,ball.Body.position)<.001f&&g.SpawnCount==spawned,"falling neither moves balls nor advances pending ball spawning");Check(g.CollectionBricks.Where(b=>b.Alive).All(b=>!b.GetComponent<BoxCollider2D>().enabled),"moving board colliders are disabled");Check(!g.TryShoot(Vector2.up),"new volleys cannot launch while settling");g.SetPaused(true);var moving=g.CollectionBricks.First(b=>b.Alive);Vector2 movingAt=moving.transform.position;Steps(15);Check(Vector2.Distance(movingAt,moving.transform.position)<.001f&&g.ChainFalling,"user pause freezes gravity itself");g.SetPaused(false);Check(!ball.Body.simulated,"unpausing does not reactivate a ball before gravity finishes");ball.Body.position=ChainGridRules.Center(4,4);n=0;while(g.ChainFalling&&n++<100)Steps(1);Check(g.ChainSafeRepositions==1&&!Physics2D.OverlapCircleAll(ball.Body.position,FlightGeometry.Radius,1<<8).Any(c=>c.GetComponentInParent<BrickBody>()!=null),"a ball inside a refilled cell resumes outside the solid board without being lost");Check(!g.ChainFalling&&ball.Body.simulated&&Mathf.Abs(ball.Body.linearVelocity.magnitude-speed)<.01f,"settling restores ball speed and simulation");Check(g.ChainGridValid()&&g.Shots==19,"one five-ball volley consumes exactly one of twenty moves");
  // Last-move loss waits for settlement and last-move collection wins before loss.
  Reset();Add(4,0,99);LastMove();Check(g.TryShoot(Vector2.up),"last available move can launch");Steps(30);LoseAll();Steps(150);Check(!g.IsPlaying&&!Rules.Won&&g.Shots==0&&!g.TryShoot(Vector2.up),"unfinished collection loses when the final volley and refill are over");
  app.QAStartLevel(53);yield return app.QAWaitForLevel();g=app.Session;Empty();yield return null;var finalFish=Add(4,0,1,1);g.CollectionGoal[0]=1;g.CollectionGoal[1]=0;LastMove();Check(g.TryShoot(Vector2.up),"winning final move launches");Steps(30);Hit(finalFish);LoseAll();Steps(1);Check(g.IsPlaying&&g.Collected[0]==0,"last move does not fail while its collected item is flying");Steps(150);Check(!g.IsPlaying&&Rules.Won&&g.Collected[0]==1,"collection on the final move wins instead of timing out");
  Physics2D.simulationMode=mode;
  app.QAStartLevel(53);yield return app.QAWaitForLevel();g=app.Session;yield return new WaitForSecondsRealtime(.5f);
  // Native visual sequence: one normal kill, survivors fall, and top refill arrives.
  Directory.CreateDirectory(Path.Combine(dir,"gravity"));yield return Capture("gravity/frame-000");var target=g.CollectionBricks.First(b=>b.GetComponent<ChainGridPiece>().Col==4&&b.GetComponent<ChainGridPiece>().Row==5);Hit(target);for(int f=1;f<=24;f++){yield return new WaitForSecondsRealtime(1f/30);yield return Capture("gravity/frame-"+f.ToString("000"));}Check(g.ChainGridValid()&&g.ChainRefills>0,"native gravity sequence returns to a full board");yield return Capture("01-refilled-pixel-board");
  app.QAStartLevel(53);yield return app.QAWaitForLevel();g=app.Session;Empty();yield return null;nest=g.SpawnChainNest(ChainGridRules.Center(3,4,2),new[]{1,2,0,1});yield return new WaitForSecondsRealtime(.35f);yield return Capture("02-hidden-collection-preview");Hit(nest);Set("chainGravityDelay",1f);yield return new WaitForSecondsRealtime(.35f);g.SetPaused(true);yield return Capture("03-mixed-four-children");g.SetPaused(false);yield return new WaitForSecondsRealtime(1.4f);Check(g.ChainGridValid(),"mixed contents join ordinary gravity and refill");
  app.QAStartLevel(53);yield return app.QAWaitForLevel();g=app.Session;yield return new WaitForSecondsRealtime(.5f);Physics2D.simulationMode=SimulationMode2D.Script;int turn=0,totalRefills=0;
  foreach(float degrees in new[]{90f,74f,106f}){if(!g.IsPlaying)break;float rad=degrees*Mathf.Deg2Rad;Check(g.TryShoot(new Vector2(Mathf.Cos(rad),Mathf.Sin(rad))),"natural full-board volley "+turn);int ticks=0;do{Steps(1);ticks++;if(g.RecallAvailable)g.RecallBalls();if(ticks%250==0)yield return null;}while(g.IsPlaying&&(!g.CollectionReady||ticks<2)&&ticks<4000);Check(ticks<4000&&g.ChainGridValid(),"natural volley settles with complete occupancy "+turn);totalRefills=g.ChainRefills;File.AppendAllText(Path.Combine(dir,"natural-volleys.txt"),$"angle={degrees} ticks={ticks} moves={g.Shots} refills={g.ChainRefills} hits={g.HitCount} breaks={g.BreakCount} autoReturns={g.ChainAutoReturns} fish={g.Collected[0]} toys={g.Collected[1]}\n");turn++;}
  Check(turn==3&&g.Shots==17&&totalRefills>0,"three real physics volleys preserve the move budget and trigger gravity");Physics2D.simulationMode=mode;yield return Capture("04-after-three-physical-volleys");
  app.QANavigate("home");yield return null;Check(FindObjectsByType<ChainGridPiece>(FindObjectsSortMode.None).Length==0&&!g.ChainFalling,"leaving the stage clears the grid and frozen-flight state");
  g.StartLevel(50);Check(g.Shots==int.MaxValue&&Rules.UnlimitedShots&&!g.ChainFalling,"legacy collection keeps its original unlimited-shot mode");g.StopLevel();
  File.AppendAllText(Path.Combine(dir,"checks.txt"),$"RESULT checks={checks} failures={failures} errors={errors}\n");Application.Quit(failures+errors==0?0:1);
 }
```

### Capture · L70

方法 SHA-256：`caec7ee369dccaaed8b3cd6053a01f8c91391f0dfe2858caf6ef682070d51ae4`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainGravityQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainGravityQA.cs:38` · `ChainGravityQA.Start` → `Capture("00-full-pixel-cat")`
- `Assets/Scripts/ChainGravityQA.cs:61` · `ChainGravityQA.Start` → `Capture("gravity/frame-000")`
- `Assets/Scripts/ChainGravityQA.cs:61` · `ChainGravityQA.Start` → `Capture("gravity/frame-"+f.ToString("000"))`
- `Assets/Scripts/ChainGravityQA.cs:61` · `ChainGravityQA.Start` → `Capture("01-refilled-pixel-board")`
- `Assets/Scripts/ChainGravityQA.cs:62` · `ChainGravityQA.Start` → `Capture("02-hidden-collection-preview")`
- `Assets/Scripts/ChainGravityQA.cs:62` · `ChainGravityQA.Start` → `Capture("03-mixed-four-children")`
- `Assets/Scripts/ChainGravityQA.cs:65` · `ChainGravityQA.Start` → `Capture("04-after-three-physical-volleys")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var cam=Camera.main;var target=new RenderTexture(width,height,24);target.Create();var old=cam.targetTexture;cam.targetTexture=target;cam.rect=new Rect(0,0,1,1);cam.aspect=(float)width/height;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;Canvas.ForceUpdateCanvases();var design=(RectTransform)typeof(AppUI).GetField("designRoot",Hidden).GetValue(app);var safe=(RectTransform)design.parent;design.localScale=Vector3.one*Mathf.Min(safe.rect.width/1080f,safe.rect.height/2340f);Canvas.ForceUpdateCanvases();cam.Render();var previous=RenderTexture.active;RenderTexture.active=target;var texture=new Texture2D(width,height,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,width,height),0,0);texture.Apply();File.WriteAllBytes(Path.Combine(dir,name+".png"),texture.EncodeToPNG());RenderTexture.active=previous;cam.targetTexture=old;Destroy(texture);target.Release();Destroy(target);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(){g.StartLevel(53);Empty();}
```

## 源字段与依赖

```csharp
const BindingFlags Hidden=BindingFlags.NonPublic|BindingFlags.Instance;
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
int checks,failures,errors,width=540,height=1170;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
