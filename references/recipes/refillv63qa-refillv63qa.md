# RefillV63QA · RefillV63QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/RefillV63QA.cs`
文件 SHA-256：`b6311b009e93eb36647922fdaa87ced979bbb64dcb747e24c89dce0296ab00b0`
[原始源码快照](../source/Assets/Scripts/RefillV63QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L38

方法 SHA-256：`8a2bc244942078aea392d85aa8f0bd68e6e027b537cdfcf25913ffa96429f0d4`
时间来源：Time.fixedDeltaTime, Time.timeScale, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.RefillV63QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(1);FindFirstObjectByType<AppUI>().QANavigate("home");SoundService.SetMuted(true);g=new GameObject("Flow traffic fixture").AddComponent<GameSession>();g.enabled=false;g.QAIgnoreFocusLoss=true;var old=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;Time.timeScale=0;
  TrafficProjection();Plain();Remove(Cell(0,8));Step(3);var left=Routes();var versions=left.Keys.ToDictionary(p=>p,p=>p.MotionVersion);Remove(Cell(8,8));Step();var after=Routes();
  Check(g.FlowPlanColumns==1,"independent right-column removal plans exactly one column");Check(left.Count>0&&left.All(p=>after.TryGetValue(p.Key,out var route)&&ReferenceEquals(route,p.Value)&&p.Key.MotionVersion==versions[p.Key]),"remote in-flight routes keep their identity and version");Settle("independent column removal");
  int revision=g.FlowPlanRevision,refills=g.ChainRefills;Call("ScheduleChainGravity");Step();Check(g.FlowPlanRevision==revision&&g.ChainRefills==refills&&g.FlowSkippedPlans>0,"duplicate dirty event cannot replan or duplicate supply");
  Plain();foreach(var b in g.CollectionBricks.Where(b=>b.Alive&&b.GetComponent<ChainGridPiece>().Row>=2).ToArray())Remove(b);Step();
  Check(OnBoard()==18,"large clear retains a short intentional opening before new supply enters");
  int visibleAtHalf=0,visibleAtOne=0;for(int i=1;i<=Mathf.CeilToInt(1.8f/Time.fixedDeltaTime);i++){Step();if(i==Mathf.RoundToInt(.5f/Time.fixedDeltaTime))visibleAtHalf=OnBoard();if(i==Mathf.RoundToInt(1f/Time.fixedDeltaTime))visibleAtOne=OnBoard();}
  File.WriteAllText(dir+"/mass-clear.json",$"{{\"removed\":63,\"remainingImmediately\":18,\"visibleAtHalfSecond\":{visibleAtHalf},\"visibleAtOneSecond\":{visibleAtOne},\"visibleAt1_8Seconds\":{OnBoard()}}}");
  Check(visibleAtHalf>=27,"63-cell clear restores at least one supply row within about half a second");Check(visibleAtOne>=54,"63-cell clear restores at least two thirds within about one second");Settle("63-cell clear");Check(g.ChainOccupiedCells()==81,"large clear reserves exactly 81 final cells");
  Plain();foreach(var b in g.CollectionBricks.Where(b=>b.Alive).ToArray())Remove(b);Step();Check(OnBoard()==0,"complete clear initially exposes the authored strategy window");Step(Mathf.CeilToInt(.4f/Time.fixedDeltaTime));Check(OnBoard()>=9,"complete clear returns at least a row within about 0.4 seconds");Settle("81-cell clear");Check(OnBoard()==81,"complete clear returns a full board without duplicate supply");
  Plain();for(int c=0;c<9;c++){Cell(c,4).Remove();Call("SpawnChainCell",c,4,0,1,BrickKind.Stone,false);}Call("BeginBoardFlowGravity");Remove(Cell(4,8));Step();Settle("sealed cavity");Check(g.ChainOccupiedCells()==80,"sealed cavity stays empty instead of spawning through its roof");revision=g.FlowPlanRevision;Step(120);Check(g.FlowPlanRevision==revision,"sealed cavity does not retry on idle frames");
  // Repeated interrupted routes on all authored boards. Destroy ordinary
  // cells only so collection victory cannot bypass the settling assertions.
  for(int level=0;level<52;level++){
   g.StartLevel(level);overlap=false;bool valid=true;var stones=g.CollectionBricks.Where(b=>b.Alive&&b.Definition.kind==BrickKind.Stone).ToDictionary(b=>b,b=>b.GetComponent<Rigidbody2D>().position);
   for(int burst=0;burst<4;burst++){
    var victims=g.CollectionBricks.Where(b=>b.Alive&&b.Definition.kind==BrickKind.Normal&&b.Definition.juiceGroup==0&&!g.IsJellyTarget(b)&&!ChainPowerRules.IsNest(b.Definition)).OrderByDescending(b=>b.GetComponent<ChainGridPiece>().Row).ThenBy(b=>(b.Id+burst*7)%17).Take(7).ToArray();
    foreach(var b in victims)Remove(b);Step(5+burst);valid&=g.ChainGridValid(false);
   }
   Check(valid,"unique target occupancy after interrupted plans Level "+(level+1));
   var positions=Pieces().ToDictionary(p=>p,p=>Position(p));g.SetPaused(true);Step(8);Check(positions.All(p=>Vector2.Distance(p.Value,Position(p.Key))<.001f),"pause freezes all flow bodies Level "+(level+1));g.SetPaused(false);
   Settle("interrupted refill Level "+(level+1));Check(stones.All(p=>p.Key.Alive&&Vector2.Distance(p.Value,p.Key.GetComponent<Rigidbody2D>().position)<.001f),"stones remain fixed Level "+(level+1));yield return null;
  }
  // A new session invalidates the previous draft, paths and pool lifetimes.
  Plain();Remove(Cell(0,8));Step(2);g.StartLevel(28);Step(2);Check(g.FlowPlanRevision==0&&g.ChainGridValid(false),"switching levels cancels old routes and reservations");
  g.StopLevel();Destroy(g.gameObject);Time.timeScale=1;Physics2D.simulationMode=old;Check(errors==0,"zero runtime errors");File.WriteAllText(dir+"/result.json",$"{{\"checks\":{checks},\"failures\":{failures},\"errors\":{errors}}}");Application.Quit(failures+errors==0?0:1);
 }
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
string dir;
```
```csharp
GameSession g;
```
```csharp
int checks,failures,errors;
```
```csharp
bool overlap;
```
```csharp
readonly List<ChainGridPiece> bodies=new List<ChainGridPiece>(96);
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
