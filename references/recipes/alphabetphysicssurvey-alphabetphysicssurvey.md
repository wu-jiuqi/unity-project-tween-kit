# AlphabetPhysicsSurvey · AlphabetPhysicsSurvey

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/AlphabetPhysicsSurvey.cs`
文件 SHA-256：`fc7c54e26e0243bca8ec691973d4f192774588131ce27f2a4ae05c615c3313ea`
[原始源码快照](../source/Assets/Scripts/AlphabetPhysicsSurvey.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L13

方法 SHA-256：`739884b6478c8edbf5ad8c116595cb12d56ba09199ea3b616d1f5dcda1de2e6e`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetPhysicsSurvey.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){report.errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(1);FindFirstObjectByType<AppUI>().QANavigate("home");g=new GameObject("Survey production session").AddComponent<GameSession>();g.QAIgnoreFocusLoss=true;g.enabled=false;var previous=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  for(int level=0;level<26;level++){
   g.StartLevel(level);var run=new Run{letter=AlphabetCampaign.Stage(level).letter,fishGoal=g.CollectionGoal[0],toyGoal=g.CollectionGoal[1],valid=true};yield return null;
   while(g.IsPlaying&&g.Shots>0&&run.shots<20){var direction=Choose(run.shots);if(!g.TryShoot(direction)){report.errors++;break;}run.shots++;int n=0;do{g.QAAdvanceOneStep();n++;if(n%150==0)yield return null;}while(g.IsPlaying&&(n<32||g.ActiveBalls>0||g.PendingBurstBalls>0||!g.ChainGridReady||g.ChainEffectsPending)&&n<2400);if(n>=2400){report.errors++;break;}run.valid&=g.ChainGridValid(false);}
   run.blueJelly=g.JellyCollected[1];run.blueJellyGoal=g.JellyGoal[1];run.fish=g.Collected[0];run.toys=g.Collected[1];run.won=g.CollectionWon;run.hits=g.HitCount;run.breaks=g.BreakCount;run.activations=g.ChainActivationCount;run.refills=g.ChainRefills;run.settled=g.ActiveBalls==0&&g.PendingBurstBalls==0;report.runs.Add(run);Save();yield return null;
  }
  g.StopLevel();Destroy(g.gameObject);Physics2D.simulationMode=previous;Save();Application.Quit(report.errors==0?0:1);
 }
```

### Choose · L23

方法 SHA-256：`d9047800fcd07dba05b7c53a39194882168c58785fdd1b7fac0dad18a4405234`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.AlphabetPhysicsSurvey.Choose(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/AlphabetPhysicsSurvey.cs:17` · `AlphabetPhysicsSurvey.Start` → `Choose(run.shots)`

```csharp
Vector2 Choose(int turn){
  float best=float.NegativeInfinity;Vector2 chosen=Vector2.up;
  for(int a=12;a<=168;a+=3){float radians=a*Mathf.Deg2Rad;Vector2 direction=new Vector2(Mathf.Cos(radians),Mathf.Sin(radians)),d=direction,p=g.SpawnPosition(direction);float score=0,distance=70;Collider2D previous=null;var health=new Dictionary<int,int>();var opened=new HashSet<int>();
   for(int bounce=0;bounce<40&&distance>.01f;bounce++){
    var hits=Physics2D.CircleCastAll(p,FlightGeometry.Radius,d,distance,1<<8);RaycastHit2D hit=default;bool found=false;
    foreach(var candidate in hits.OrderBy(h=>h.distance)){if(candidate.collider==null||candidate.collider.isTrigger||candidate.distance<.005f&&candidate.collider==previous)continue;var brick=candidate.collider.GetComponentInParent<BrickBody>();if(brick!=null&&(!brick.Alive||health.TryGetValue(brick.Id,out int gone)&&gone<=0))continue;hit=candidate;found=true;break;}
    if(!found||d.y<0&&hit.centroid.y<-6.1f)break;distance-=Mathf.Max(.02f,hit.distance);var b=hit.collider.GetComponentInParent<BrickBody>();
    if(b!=null){if(!health.TryGetValue(b.Id,out int hp))hp=b.CurrentHealth;if(b.Definition.kind==BrickKind.Stone)score-=.25f;else if(b.Definition.kind==BrickKind.RearWeak&&!g.ChainRearEntry(b,d,hit.point))score-=.35f;else {int damage=b.Definition.kind==BrickKind.RearWeak?hp:1;score+=(b.Definition.juiceGroup>0||g.IsJellyTarget(b))?2f:ChainPowerRules.IsNest(b.Definition)?1.1f:.7f;hp-=damage;health[b.Id]=hp;if(hp<=0)score+=(b.Definition.juiceGroup>0||g.IsJellyTarget(b))?12:3;}}
    previous=hit.collider;d=Vector2.Reflect(d,hit.normal).normalized;p=hit.centroid+hit.normal*.02f;
   }
   // Alternate equally good symmetric routes without relying on a hidden random seed.
   score+=Mathf.Sin((a+turn*23)*.12f)*.08f;if(score>best){best=score;chosen=direction;}
  }
  return chosen;
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
GameSession g;
```
```csharp
Report report=new Report();
```
```csharp
string dir;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
