# ChainRefillV39QA · ChainRefillV39QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChainRefillV39QA.cs`
文件 SHA-256：`5e881339a386448b97a94bb9778a645422109b66f12c885d8839957170d572c1`
[原始源码快照](../source/Assets/Scripts/ChainRefillV39QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L25

方法 SHA-256：`60d2f94289f23123e5d532c0bfd51c02b0ab196b288f90e92da2551fa222233f`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainRefillV39QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"v39 production physics regression: top-only supply, random in-place rewards, wider side banks. Isolated memory profile.\n");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");yield return null;
  g=new GameObject("v39 physical fixtures").AddComponent<GameSession>();g.QAIgnoreFocusLoss=true;g.enabled=false;var previousMode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  var nest=Bridge();var ids=new HashSet<int>(g.CollectionBricks.Select(b=>b.Id));Call("ScheduleChainGravity");Steps(1);
  Check(g.CollectionBricks.All(b=>ids.Contains(b.Id)),"a chest-bridged cavity never creates a brick in its destination cell");Check(g.ChainOccupiedCells()==80&&g.ChainGridValid(false)&&g.ChainGridReady,"sealed cavity remains a valid idle hole without perpetual dirty retries");Seconds(4);Check(g.ChainOccupiedCells()==80&&nest.Alive,"waiting cannot make a sealed cavity magically refill");
  Damage(nest);ids=new HashSet<int>(g.CollectionBricks.Select(b=>b.Id));Steps(1);var arrivals=g.CollectionBricks.Where(b=>!ids.Contains(b.Id)).ToArray();Check(arrivals.Length>0&&arrivals.All(b=>Position(b.transform).y>CollectionLevels.Top+.5f),"opening the ceiling creates ordinary supply strictly above the board");Check(g.ChainTools.Count==0,"ordinary supply never consumes a special-brick reward");Seconds(4);Check(g.ChainGridValid()&&g.ChainOccupiedCells()==81,"top arrivals and released children eventually close the reachable gap");
  nest=Bridge();Call("ScheduleChainGravity");Steps(1);typeof(RuleState).GetField("<Shots>k__BackingField",Hidden).SetValue(Rules,1);Check(g.TryShoot(Vector2.up),"last move can launch with a sealed cavity");Seconds(.65f);foreach(var ball in Balls())ball.ChainBornAt=-20;int tick=0;while(g.IsPlaying&&tick++<1000)Steps(1);Check(!g.IsPlaying&&!Rules.Won&&tick<1000,"an unreachable refill cavity cannot block exhausted-run failure");
  g.StartLevel(18);Reward(1);Check(g.ChainOccupiedCells()==81&&g.ChainGridReady&&!g.ChainFalling&&g.ChainGridValid(),"reward replaces a settled full-board cell without opening gravity");g.StartLevel(18);Seconds(.2f);var link=g.ChainTools.Single(t=>t.Kind==3);g.ActivateChainTool(link,Vector2.up);var marked=g.ChainBonds.Single().Members.ToArray();var protectedBricks=g.CollectionBricks.Where(b=>b.Definition.kind!=BrickKind.Normal||b.Definition.juiceGroup>0||ChainPowerRules.IsNest(b.Definition)||marked.Contains(b)).ToArray();
  ids=new HashSet<int>(g.CollectionBricks.Select(b=>b.Id));var oldTools=new HashSet<ChainTool>(g.ChainTools);var ordinary=g.CollectionBricks.Where(b=>b.Alive).ToDictionary(b=>Position(b.transform),b=>b.GetComponent<ChainGridPiece>().FallAt);int count=g.ChainOccupiedCells(),hits=g.HitCount,broken=g.BreakCount;bool wasReady=g.ChainGridReady,wasFalling=g.ChainFalling;Reward(1,2,3);var generated=g.ChainTools.Where(t=>!oldTools.Contains(t)).ToArray();
  Check(generated.Length==3&&generated.Select(t=>t.Kind).OrderBy(k=>k).SequenceEqual(new[]{1,2,3}),"highest rating packet replaces exactly three ordinary cells");Check(generated.All(t=>ordinary.ContainsKey(Position(t.View))),"each reward starts at the physical position of an existing brick");Check(count==g.ChainOccupiedCells()&&g.ChainGridValid(false)&&g.ChainGridReady==wasReady&&g.ChainFalling==wasFalling,"replacement creates no extra gaps or gravity restart after link activation");Check(protectedBricks.All(b=>b!=null&&b.Alive)&&marked.All(b=>b.Alive),"collectibles, obstacles, chests and linked members cannot be replaced");Check(g.HitCount==hits&&g.BreakCount==broken&&g.Collected.Sum()==0,"replacement does not count as damage, a kill or collection");Check(g.CollectionBricks.All(b=>ids.Contains(b.Id))&&g.ChainPendingReplacements==0,"rewards never create a fresh normal supply brick");
  typeof(GameSession).GetField("chainRewardRandom",Hidden).SetValue(g,new System.Random(991));var rows=new HashSet<int>();for(int i=0;i<20;i++){oldTools=new HashSet<ChainTool>(g.ChainTools);Reward(1);rows.Add(g.ChainTools.Single(t=>!oldTools.Contains(t)).View.GetComponent<ChainGridPiece>().Row);}Check(rows.Count>=5,"random replacement samples the board instead of always choosing its top row");
  g.StartLevel(18);Empty();var sole=Add(4,8,8);Add(2,8,8,BrickKind.Armor);Add(6,8,7,BrickKind.Normal,1);Vector2 location=Position(sole.transform);Reward(2);Check(!sole.Alive&&g.ChainTools.Count==1&&Position(g.ChainTools.Single().View)==location&&g.ChainOccupiedCells()==3,"an existing hole is not used in place of the selected ordinary brick");
  g.StartLevel(18);Empty();Add(2,8,8,BrickKind.Armor);var goal=Add(6,8,7,BrickKind.Normal,1);Reward(1);Check(g.ChainPendingReplacements==1&&g.ChainTools.Count==0&&goal.Alive,"a packet waits safely when no eligible ordinary brick exists");sole=Add(4,8,4);Steps(1);Check(g.ChainPendingReplacements==0&&g.ChainTools.Count==1&&!sole.Alive,"deferred reward replaces the next eligible ordinary brick");
  g.StartLevel(18);Empty();BrickBody falling=null;for(int r=0;r<9;r++)for(int c=0;c<9;c++)if(!(c==4&&r==7)){var b=Add(c,r,999,c==4&&r==6?BrickKind.Normal:BrickKind.Stone);if(c==4&&r==6)falling=b;}Call("ScheduleChainGravity");Seconds(2.04f);var grid=falling.GetComponent<ChainGridPiece>();Vector2 movingAt=Position(falling.transform);float fallAt=grid.FallAt;int row=grid.Row;Check(movingAt.y<ChainGridRules.Center(4,6).y-.001f,"replacement fixture is physically descending");Reward(1);var replacement=g.ChainTools.Single();var replacementGrid=replacement.View.GetComponent<ChainGridPiece>();Check(!falling.Alive&&Position(replacement.View)==movingAt&&replacementGrid.Row==row&&replacementGrid.FallAt==fallAt,"a moving replacement inherits its exact position, destination and deadline");g.SetPaused(true);Steps(50);Check(Position(replacement.View)==movingAt,"pause freezes the inherited replacement motion");g.SetPaused(false);Seconds(2);Check(Vector2.Distance(Position(replacement.View),ChainGridRules.Center(4,7))<.002f&&g.ChainGridValid(),"the replacement continues falling on the existing path without overlap");
  g.StartLevel(0);var walls=g.GetComponentsInChildren<BoxCollider2D>();var left=walls.Single(c=>c.name=="LeftWall");var right=walls.Single(c=>c.name=="RightWall");var top=walls.Single(c=>c.name=="TopWall");Physics2D.SyncTransforms();Check(Mathf.Abs(left.bounds.max.x-FlightGeometry.Left)<.001f&&Mathf.Abs(right.bounds.min.x-FlightGeometry.Right)<.001f,"physical side walls match the widened shared flight geometry");Check(top.bounds.max.x>=right.bounds.max.x-.001f&&top.bounds.min.x<=left.bounds.min.x+.001f,"ceiling joins both widened walls without a corner gap");float gap=FlightGeometry.Right-(CollectionLevels.Cell(8,0).x+.38f);Check(Mathf.Abs(gap-.79f)<.001f&&gap>FlightGeometry.Radius*4,"side lane is now 0.79 units wide with room for bank shots");Check(Mathf.Abs(JellyWallFrame.SideCenter-JellyWallFrame.HalfWidth-FlightGeometry.Right)<.001f,"visible wall face matches the physical rebound plane");
  for(int side=-1;side<=1;side+=2){g.StartLevel(0);Empty();for(int r=0;r<9;r++)for(int c=0;c<9;c++)Add(c,r,999,BrickKind.Stone);Vector2 direction=new Vector2(side*(FlightGeometry.Right-FlightGeometry.Radius),-1.8f)-g.LauncherPosition;g.SetAim(direction.normalized);Check(g.AimReflections>0,"aim preview finds side wall for bank "+side);g.TryShoot(direction);Steps(1);var ball=Balls().Single();float peak=ball.Body.position.y;tick=0;while(ball.Active&&tick++<650){Steps(1);peak=Mathf.Max(peak,ball.Body.position.y);if(tick%150==0)yield return null;}Check(peak>CollectionLevels.Top+.38f+FlightGeometry.Radius,"real ball climbs the "+(side<0?"left":"right")+" bank lane above a full stone board");}
  g.StopLevel();Destroy(g.gameObject);Physics2D.simulationMode=previousMode;app.QASetViewport(new Vector2Int(540,1170));for(int id=1;id<=12;id++)app.Profile.MarkAlphabetIntro(id);app.QAStartLevel(25);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.15f);yield return Capture("01-wider-side-lanes");
  app.Session.QAIgnoreFocusLoss=true;app.Session.TryShoot(new Vector2(.6f,1));yield return new WaitForSecondsRealtime(1);yield return Capture("02-live-bank");app.QANavigate("home");Check(errors==0,"runtime Console has no errors or exceptions");File.AppendAllText(Path.Combine(dir,"checks.txt"),$"RESULT checks={checks} failures={failures} errors={errors}\n");Application.Quit(failures+errors==0?0:1);
 }
```

### Capture · L45

方法 SHA-256：`471a0b5b3543e458cda568fec3ec119e3de7ac7d07cc20f9eb12751d492e817e`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainRefillV39QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainRefillV39QA.cs:42` · `ChainRefillV39QA.Start` → `Capture("01-wider-side-lanes")`
- `Assets/Scripts/ChainRefillV39QA.cs:43` · `ChainRefillV39QA.Start` → `Capture("02-live-bank")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var cam=Camera.main;var target=new RenderTexture(540,1170,24);target.Create();var old=cam.targetTexture;cam.targetTexture=target;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;Canvas.ForceUpdateCanvases();cam.Render();var previous=RenderTexture.active;RenderTexture.active=target;var texture=new Texture2D(540,1170,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,540,1170),0,0);texture.Apply();File.WriteAllBytes(Path.Combine(dir,name+".png"),texture.EncodeToPNG());RenderTexture.active=previous;cam.targetTexture=old;Destroy(texture);target.Release();Destroy(target);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
const BindingFlags Hidden=BindingFlags.NonPublic|BindingFlags.Instance;
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

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
