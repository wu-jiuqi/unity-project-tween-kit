# ChineseV60OutcomeQA · ChineseV60OutcomeQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChineseV60OutcomeQA.cs`
文件 SHA-256：`5c1e72a8fb3395abb16db88757650520919a9729c9ad1d2f1408fb3a69a9c416`
[原始源码快照](../source/Assets/Scripts/ChineseV60OutcomeQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L20

方法 SHA-256：`38f5f5db279002096bd214723d0fc99752d385cdbd0e72bd289db3d31ab3c549`
时间来源：Time.timeScale, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChineseV60OutcomeQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(8);FindFirstObjectByType<AppUI>().QANavigate("home");g=new GameObject("Target victory fixture").AddComponent<GameSession>();g.enabled=false;g.QAIgnoreFocusLoss=true;g.Outcome+=value=>{outcomes++;won=value;};var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;Time.timeScale=0;
  foreach(int level in new[]{0,11,28}){
   Reset(level);Check(g.TryShoot(Vector2.up),"fixture accepts first live ball");Step();Check(g.ActiveBalls>0,"live-ball fixture really contains an active ball");Goal();StepWithLiveBalls();Check(g.CollectionVictoryPending&&g.IsPlaying&&outcomes==0,"goal starts a countdown with live balls stage "+(level+1));Check(!g.TryShoot(Vector2.up)&&!g.ChainCanLaunch&&!g.ChainLaunchQueued,"countdown rejects new and buffered shots");
   StepWithLiveBalls(58);Check(g.IsPlaying&&outcomes==0&&g.CollectionVictoryRemaining>0,"victory does not fire before one second");Check(g.ActiveBalls>0,"physical ball remains active immediately before deadline");
   StepWithLiveBalls(2);Check(!g.IsPlaying&&won&&outcomes==1,"victory fires at one second regardless of balls stage "+(level+1));Check(g.ActiveBalls>0,"winning result commits while physical balls still exist");Step(10);Check(outcomes==1,"victory emits once");yield return null;
  }
  Reset(0,1);Check(g.TryShoot(Vector2.up),"partial volley fixture accepts");Step();Goal();Step();int spawned=g.SpawnCount;Check(g.PendingBurstBalls>0&&g.CollectionVictoryPending,"goal latches with not-yet-spawned balls");Step(61);Check(won&&outcomes==1&&g.SpawnCount==spawned&&g.PendingBurstBalls==0,"reserved balls never spawn during countdown and cannot block victory");
  Reset(0,1);Fire();Goal();var target=g.CollectionBricks.First(b=>b.Alive&&g.IsJellyTarget(b));int color=Enumerable.Range(0,g.JellyGoal.Length).First(c=>g.JellyGoal[c]>0);g.JellyCollected[color]--;g.ApplyChainImpact(target,999999,Vector2.up,target.transform.position,true);Check(!g.CollectionWon,"last target still has its HUD flight");Step();Check(g.CollectionVictoryPending,"last earned target starts countdown before HUD arrival");ReturnBalls();Step(61);Check(won&&outcomes==1&&g.CollectionWon,"last-shot victory wins over failure while loot is in flight");int collected=g.JellyCollected.Sum();Step(10);Check(g.JellyCollected.Sum()==collected,"earned target counted only once");
  Reset(0);Goal();Step();Step(20);g.SetPaused(true);float remaining=g.CollectionVictoryRemaining;Step(90);Check(g.CollectionVictoryRemaining==remaining&&outcomes==0,"pause freezes the one-second clock");g.SetPaused(false);Step(41);Check(won&&outcomes==1,"resume completes the remaining delay");
  Reset(0);Goal();Step();Step(20);g.StartLevel(1);outcomes=0;Step(90);Check(!g.CollectionVictoryPending&&g.IsPlaying&&outcomes==0,"restart cancels old victory countdown");Goal();Step();g.StopLevel();Step(90);Check(!g.CollectionVictoryPending&&outcomes==0,"leaving level cancels old countdown");
  Reset(0,1);Fire();ReturnBalls();Step();Check(!g.IsPlaying&&!won&&outcomes==1,"failure still waits for final physical ball and resolves immediately");
  Reset(0,2);Fire();ReturnBalls();Step();Check(g.IsPlaying&&outcomes==0&&!g.CollectionVictoryPending,"incomplete goals with shots remaining keep playing");
  Physics2D.simulationMode=mode;Time.timeScale=1;g.StopLevel();Destroy(g.gameObject);
  var app=FindFirstObjectByType<AppUI>();app.QAStartLevel(0);yield return app.QAWaitForLevel();for(int i=0;i<40&&app.AlphabetIntroActive;i++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.15f);}g=app.Session;g.QAIgnoreFocusLoss=true;
  Check(g.TryShoot(Vector2.up),"real UI fixture accepts volley");yield return new WaitForSecondsRealtime(.15f);Goal();yield return null;Check(FindFirstObjectByType<VictoryPresentation>()==null,"real result UI stays hidden at goal completion");yield return new WaitForSecondsRealtime(.75f);Check(FindFirstObjectByType<VictoryPresentation>()==null,"real result UI remains hidden before one second");yield return new WaitForSecondsRealtime(.4f);Check(FindFirstObjectByType<VictoryPresentation>()!=null&&!g.IsPlaying,"real victory animation starts after the one-second delay");
  Check(errors==0,"zero runtime errors");File.WriteAllText(dir+"/result.json",$"{{\"checks\":{checks},\"failures\":{failures},\"errors\":{errors}}}");Application.Quit(failures+errors==0?0:1);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(int level,int shots=2){g.StartLevel(level);Shots.SetValue(Rules.GetValue(g),shots);outcomes=0;won=false;}
```

## 源字段与依赖

```csharp
string dir;
```
```csharp
int checks,failures,errors,outcomes;
```
```csharp
bool won;
```
```csharp
GameSession g;
```
```csharp
const BindingFlags Hidden=BindingFlags.Instance|BindingFlags.NonPublic;
```
```csharp
static readonly FieldInfo Rules=typeof(GameSession).GetField("rules",Hidden),Shots=typeof(RuleState).GetField("<Shots>k__BackingField",Hidden);
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
