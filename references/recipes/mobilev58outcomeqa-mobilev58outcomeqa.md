# MobileV58OutcomeQA · MobileV58OutcomeQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/MobileV58OutcomeQA.cs`
文件 SHA-256：`5ebb40936b3ddeb173478505b78ddfc53087c028d8a755f64105f6e2bd41d9c8`
[原始源码快照](../source/Assets/Scripts/MobileV58OutcomeQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L18

方法 SHA-256：`2d4703ff6996f1a78366e8fddcdd8094bf95f536db998e0473ee36596ae90f46`
时间来源：Time.timeScale, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.MobileV58OutcomeQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);FindFirstObjectByType<AppUI>().QANavigate("home");g=new GameObject("Last-ball fixture").AddComponent<GameSession>();g.enabled=false;g.QAIgnoreFocusLoss=true;g.Outcome+=value=>{outcomes++;won=value;};var previous=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;Time.timeScale=0;
  foreach(int level in new[]{0,11,28}){
   Reset(level);Fire();var victim=g.CollectionBricks.First(b=>b.Alive&&b.Definition.kind==BrickKind.Normal&&!g.IsJellyTarget(b)&&(string.IsNullOrEmpty(b.Definition.reveal)||b.Definition.reveal=="picnic-N"));g.ApplyChainImpact(victim,999999,Vector2.up,victim.transform.position,true);if(level<26){Step();Check(g.ChainFalling,"fixture really has moving refill bricks stage "+(level+1));}else Check(!g.ChainGridReady,"fixture really has pending refill planning stage "+(level+1));ReturnBalls();Step();Check(!g.IsPlaying&&outcomes==1&&!won,"loss commits on first no-ball step despite falling board");Step(5);Check(outcomes==1,"loss emits exactly once");yield return null;
  }
  Reset(0);Check(g.TryShoot(Vector2.up),"pending fixture accepts volley");Step();ReturnBalls();Step();Check(g.IsPlaying&&g.PendingBurstBalls>0&&outcomes==0,"pending sub-balls prevent premature loss");
  Reset(0);Fire();Array.Copy(g.CollectionGoal,g.Collected,g.Collected.Length);Array.Copy(g.JellyGoal,g.JellyCollected,g.JellyCollected.Length);Step();Check(g.IsPlaying&&outcomes==0,"reached goal waits for remaining physical balls");ReturnBalls();Step();Check(!g.IsPlaying&&won&&outcomes==1,"victory commits immediately after last ball returns");
  Reset(0);Fire();Array.Copy(g.CollectionGoal,g.Collected,g.Collected.Length);Array.Copy(g.JellyGoal,g.JellyCollected,g.JellyCollected.Length);var target=g.CollectionBricks.First(b=>b.Alive&&g.IsJellyTarget(b));int color=Enumerable.Range(0,g.JellyGoal.Length).First(c=>g.JellyGoal[c]>0);g.JellyCollected[color]--;g.ApplyChainImpact(target,999999,Vector2.up,target.transform.position,true);Check(!g.CollectionWon,"last earned reward is still flying to HUD");ReturnBalls();Step();Check(!g.IsPlaying&&won&&g.CollectionWon,"flying earned reward credited before immediate verdict");int collected=g.JellyCollected.Sum();Step(5);Check(g.JellyCollected.Sum()==collected&&outcomes==1,"flushed loot cannot be counted twice");
  Reset(0,2);Fire();ReturnBalls();Step();Check(g.IsPlaying&&outcomes==0,"empty field with shots remaining keeps playing");
  Reset(0);Fire();ReturnBalls();var miniView=new GameObject("Active split ball fixture").transform;g.ChainMinis.Add(new ChainMini{View=miniView,Position=Vector2.zero,Direction=Vector2.up,Life=10});Step();Check(g.IsPlaying&&outcomes==0,"visible split balls also keep the field active");g.ChainMinis.Clear();Destroy(miniView.gameObject);Step();Check(!g.IsPlaying&&outcomes==1,"last split ball exit allows immediate verdict");
  Reset(0);Fire();ReturnBalls();g.SetPaused(true);Step();Check(g.IsPlaying&&outcomes==0,"pause suppresses result processing");g.SetPaused(false);Step();Check(!g.IsPlaying&&outcomes==1,"resume resolves exhausted no-ball field without waiting for refill");
  Physics2D.simulationMode=previous;Time.timeScale=1;File.WriteAllText(dir+"/result.json",$"{{\"checks\":{checks},\"failures\":{failures},\"errors\":{errors}}}");Application.Quit(failures+errors==0?0:1);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(int level,int shots=1){g.StartLevel(level);Shots.SetValue(Rules.GetValue(g),shots);outcomes=0;won=false;}
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
static readonly FieldInfo Rules=typeof(GameSession).GetField("rules",BindingFlags.NonPublic|BindingFlags.Instance);
```
```csharp
static readonly FieldInfo Shots=typeof(RuleState).GetField("<Shots>k__BackingField",BindingFlags.NonPublic|BindingFlags.Instance);
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
