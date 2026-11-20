# MobileV54QA · MobileV54QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/MobileV54QA.cs`
文件 SHA-256：`5b6180e612eda310589a72ff60d49dfa179bb3318ba41d8e374982d24ff48b5d`
[原始源码快照](../source/Assets/Scripts/MobileV54QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L11

方法 SHA-256：`2a81f45c4096417d36bc64d9eed4db8593be98683820e8695cba3a1b32d2bbb5`
时间来源：Time.fixedDeltaTime, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.MobileV54QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();string dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);int errors=0;Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};yield return new WaitForSecondsRealtime(1);var app=FindFirstObjectByType<AppUI>();app.QAStartLevel(0);yield return app.QAWaitForLevel();for(int i=0;i<18;i++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.1f);}var game=app.Session;game.QAIgnoreFocusLoss=true;game.SetPaused(false);game.enabled=false;
  var tick=(Action<float>)Delegate.CreateDelegate(typeof(Action<float>),game,typeof(GameSession).GetMethod("TickChainCollection",BindingFlags.Instance|BindingFlags.NonPublic));
  for(int i=0;i<200;i++)tick(1f/60);int shots=game.Shots;var watch=new System.Diagnostics.Stopwatch();long before=GC.GetAllocatedBytesForCurrentThread();watch.Start();const int ticks=2000;for(int i=0;i<ticks;i++)tick(1f/60);watch.Stop();long bytes=GC.GetAllocatedBytesForCurrentThread()-before;
  long probeStart=GC.GetAllocatedBytesForCurrentThread();var probe=new byte[4096];bool supported=GC.GetAllocatedBytesForCurrentThread()>probeStart;GC.KeepAlive(probe);
  int failures=0;if(supported&&bytes>ticks*32)failures++;if(game.Shots!=shots||!game.ChainGridValid()||!Mathf.Approximately(Time.fixedDeltaTime,1f/60))failures++;
  var result=new Result{ticks=ticks,allocationCounterSupported=supported,allocatedBytes=supported?bytes:-1,bytesPerTick=supported?bytes/(double)ticks:-1,milliseconds=watch.Elapsed.TotalMilliseconds,failures=failures,errors=errors};File.WriteAllText(dir+"/result.json",JsonUtility.ToJson(result,true));game.enabled=true;Application.Quit(failures+errors==0?0:1);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
