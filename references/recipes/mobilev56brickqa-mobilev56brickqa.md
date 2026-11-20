# MobileV56BrickQA · MobileV56BrickQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/MobileV56BrickQA.cs`
文件 SHA-256：`33c7ff1b14ae122abc186e7804f32082164c70054daa61bcddbb55dc9f532c3b`
[原始源码快照](../source/Assets/Scripts/MobileV56BrickQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L13

方法 SHA-256：`4b81601f8b3be1794f65ad598c11a02973441a5da0bc92bae32e8a1f42427e28`
时间来源：Time.timeScale, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.MobileV56BrickQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();string dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);var report=new Report();var rows=new List<Sample>();Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){report.errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};yield return new WaitForSecondsRealtime(1);FindFirstObjectByType<AppUI>().QANavigate("home");SoundService.SetMuted(true);
  var g=new GameObject("Brick lifecycle fixture").AddComponent<GameSession>();g.enabled=false;g.QAIgnoreFocusLoss=true;var previous=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;Time.timeScale=0;
  foreach(int level in new[]{1,11,28}){
   g.StartLevel(level);var row=new Sample{level=level+1};var ids=new HashSet<int>();var plans=new List<double>();var watch=new System.Diagnostics.Stopwatch();foreach(var b in g.GetComponentsInChildren<BrickBody>(true))ids.Add(b.GetInstanceID());row.initialBrickInstances=ids.Count;
   for(int cycle=0;cycle<12;cycle++){
    var victims=g.CollectionBricks.Where(b=>b.Alive&&b.Definition.kind==BrickKind.Normal&&b.Definition.colorIndex==0&&b.Definition.juiceGroup==0&&(string.IsNullOrEmpty(b.Definition.reveal)||b.Definition.reveal=="picnic-N")).OrderByDescending(b=>b.GetComponent<ChainGridPiece>().Row).Take(6).ToArray();
    foreach(var b in victims)g.ApplyChainImpact(b,999,Vector2.up,b.transform.position,true);row.destroyed+=victims.Length;
    int step=0;double last=g.FlowLastPlanMilliseconds;
    do{watch.Restart();g.QAAdvanceOneStep();watch.Stop();row.maxStepMs=Math.Max(row.maxStepMs,watch.Elapsed.TotalMilliseconds);if(g.FlowLastPlanMilliseconds!=last){last=g.FlowLastPlanMilliseconds;plans.Add(last);}}while(!g.ChainGridReady&&step++<1800);
    if(step>=1800||!g.ChainGridValid(false))report.failures++;
    var bricks=g.GetComponentsInChildren<BrickBody>(true);foreach(var b in bricks)ids.Add(b.GetInstanceID());row.peakResidentBricks=Math.Max(row.peakResidentBricks,bricks.Length);row.cycles++;yield return null;
   }
   plans.Sort();if(plans.Count>0){row.medianPlanMs=plans[plans.Count/2];row.p95PlanMs=plans[(int)(plans.Count*.95)];}row.uniqueBrickInstances=ids.Count;if(row.destroyed<60)report.failures++;rows.Add(row);g.StopLevel();yield return null;
  }
  Time.timeScale=1;Physics2D.simulationMode=previous;report.samples=rows.ToArray();File.WriteAllText(dir+"/result.json",JsonUtility.ToJson(report,true));Application.Quit(report.failures+report.errors==0?0:1);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
