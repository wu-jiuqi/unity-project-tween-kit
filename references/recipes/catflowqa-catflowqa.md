# CatFlowQA · CatFlowQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/CatFlowQA.cs`
文件 SHA-256：`e6baa8acb847a8d862b2392fe90dfa21ab23aedf5ea9ac3623e5fdaeecbe90bf`
[原始源码快照](../source/Assets/Scripts/CatFlowQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L70

方法 SHA-256：`8f5cfbf2accc60b0429aba5f6140c185a7a60c18f6771777746432f4b24cb657`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CatFlowQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();string dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);int checks=0,failures=0,errors=0;
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);Physics2D.simulationMode=SimulationMode2D.Script;
  try{FindFirstObjectByType<AppUI>().Session.QACatFlowChecks((ok,name)=>{checks++;if(!ok)failures++;File.AppendAllText(dir+"/checks.txt",(ok?"PASS ":"FAIL ")+name+"\n");});}
  catch(Exception e){errors++;File.AppendAllText(dir+"/errors.txt",e.ToString());}
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
