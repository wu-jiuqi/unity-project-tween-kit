# MobileV55StressQA · MobileV55StressQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/MobileV55StressQA.cs`
文件 SHA-256：`1569093290d89b920d16d39387e0feec47614c52e4beada2b60e64d8e3fd9bcd`
[原始源码快照](../source/Assets/Scripts/MobileV55StressQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L13

方法 SHA-256：`65baa8ce067838e7fe8bca0c810ed405f0997f5926da6f31184e2a5611915c22`
时间来源：Time.timeScale, WaitForSecondsRealtime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.MobileV55StressQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();string dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);var report=new Report();var rows=new List<Sample>();
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){report.errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};
  long probeStart=GC.GetAllocatedBytesForCurrentThread();var probe=new byte[4096];report.allocationCounterSupported=GC.GetAllocatedBytesForCurrentThread()>probeStart;GC.KeepAlive(probe);
  yield return new WaitForSecondsRealtime(1);FindFirstObjectByType<AppUI>().QANavigate("home");SoundService.SetMuted(true);
  var g=new GameObject("Rapid volley fixture").AddComponent<GameSession>();g.enabled=false;g.QAIgnoreFocusLoss=true;
  var previous=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;Time.timeScale=0;
  foreach(int level in new[]{1,11,28}){
   g.StartLevel(level);foreach(var b in g.CollectionBricks)b.gameObject.AddComponent<MobileV55ContactProbe>();
   MobileV55ContactProbe.Contacts=MobileV55ContactProbe.BoardContacts=0;var row=new Sample{level=level+1};var times=new List<double>();long allocated=0;var watch=new System.Diagnostics.Stopwatch();
   for(int step=0;step<960;step++){
    if(step%36==0&&g.TryShoot(new Vector2(Mathf.Sin(step*.013f)*.55f,1)))row.volleys++;
    watch.Reset();long before=GC.GetAllocatedBytesForCurrentThread();watch.Start();g.QAAdvanceOneStep();watch.Stop();allocated+=GC.GetAllocatedBytesForCurrentThread()-before;times.Add(watch.Elapsed.TotalMilliseconds);row.peakBalls=Math.Max(row.peakBalls,g.ActiveBalls);
    if(step%60==59)yield return null;
   }
   times.Sort();row.medianMs=times[times.Count/2];row.p95Ms=times[(int)(times.Count*.95)];row.maxMs=times[times.Count-1];row.allocatedBytesPerTick=allocated/960.0;row.hits=g.HitCount;row.breaks=g.BreakCount;row.spawned=g.SpawnCount;row.contacts=MobileV55ContactProbe.Contacts;row.boardContacts=MobileV55ContactProbe.BoardContacts;
   if(!report.allocationCounterSupported)row.allocatedBytesPerTick=-1;
   if(row.volleys<5||row.peakBalls<5||row.hits==0||g.SpawnCount-g.LostCount!=g.ActiveBalls)report.failures++;
   rows.Add(row);File.WriteAllText(dir+"/progress.json",JsonUtility.ToJson(row,true));g.StopLevel();yield return null;
  }
  report.samples=rows.ToArray();Time.timeScale=1;Physics2D.simulationMode=previous;File.WriteAllText(dir+"/result.json",JsonUtility.ToJson(report,true));Application.Quit(report.failures+report.errors==0?0:1);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
