# MobileV55HandoffQA · MobileV55HandoffQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/MobileV55HandoffQA.cs`
文件 SHA-256：`49c5f45493c14970cfed8957839780e4acdfca796d81107a1b6d86c911f1dcb7`
[原始源码快照](../source/Assets/Scripts/MobileV55HandoffQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L11

方法 SHA-256：`3cf0c02cfdf5fb297864b32e2fe11740f73bfe0c2484af66889a8822fef937a8`
时间来源：Time.timeScale, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.MobileV55HandoffQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};yield return new WaitForSecondsRealtime(1);FindFirstObjectByType<AppUI>().QANavigate("home");
  var g=new GameObject("Handoff fixture").AddComponent<GameSession>();g.enabled=false;g.QAIgnoreFocusLoss=true;var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;Time.timeScale=0;
  foreach(int level in new[]{0,11,28}){
   g.StartLevel(level);Check(g.TryShoot(Vector2.up),"first release accepted stage "+(level+1));g.QAAdvanceOneStep();int charged=g.Shots;
   Check(g.PendingBurstBalls==4,"first volley keeps five balls");Check(g.TryShoot(new Vector2(.3f,1)),"early next release is retained");Check(g.ChainLaunchQueued&&g.Shots==charged,"buffer does not charge a move early");Check(!g.TryShoot(Vector2.up),"only one next volley can be queued");
   int tick=0;while(g.Shots==charged&&tick++<40)g.QAAdvanceOneStep();Check(g.Shots==charged-1&&tick<=34&&!g.ChainLaunchQueued,"buffer fires automatically at original 0.55-second cadence");Check(g.SpawnCount==6&&g.PendingBurstBalls==4,"handoff does not drop or duplicate balls");
   Check(g.TryShoot(Vector2.up),"following release buffers");g.SetPaused(true);Check(!g.ChainLaunchQueued,"pause cancels pending input");g.SetPaused(false);int shots=g.Shots;for(int i=0;i<45;i++)g.QAAdvanceOneStep();Check(g.Shots==shots,"resume never fires a cancelled release");
   Check(g.CollectionBricks.All(b=>!b.GetComponent<Rigidbody2D>().useFullKinematicContacts),"board excludes redundant kinematic contacts");g.StopLevel();Check(!g.ChainLaunchQueued,"navigation clears retained input");yield return null;
  }
  Time.timeScale=1;Physics2D.simulationMode=mode;File.WriteAllText(dir+"/result.json",$"{{\"checks\":{checks},\"failures\":{failures},\"errors\":{errors}}}");Application.Quit(failures+errors==0?0:1);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
string dir;
```
```csharp
int checks,failures,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
