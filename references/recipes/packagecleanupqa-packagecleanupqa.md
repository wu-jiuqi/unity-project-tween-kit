# PackageCleanupQA · PackageCleanupQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/PackageCleanupQA.cs`
文件 SHA-256：`0dbc612206dc5f3a9b98ee8c7e7648c8cf588ab18ecb39c0b13f8a89f8c37d4c`
[原始源码快照](../source/Assets/Scripts/PackageCleanupQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L13

方法 SHA-256：`76a9c0097d2223f094a3e41ca1ea1c43072996fa144849141734473b12c6ce53`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.PackageCleanupQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);var app=FindFirstObjectByType<AppUI>();var g=app.Session;g.QAIgnoreFocusLoss=true;
  Check(LevelCatalog.Count==45&&AlphabetCampaign.Count==45,"campaign ends at 45");
  for(int level=0;level<45;level++){
   g.StartLevel(level);Check(g.IsOpening,"new opening route "+(level+1));Check(g.Shots==OpeningCampaign.Stage(level).moves,"authored moves "+(level+1));Check(g.ChainGridValid(false),"non-overlapping grid "+(level+1));Check(g.ChainOccupiedCells()==OpeningCampaign.Stage(level).rows.Sum(row=>row.Count(c=>c!='.')),"authored occupied cells "+(level+1));Check(g.OpeningTargets.Length>0,"collection targets "+(level+1));
   if(level%5==0)yield return null;
  }
  g.StartLevel(77);Check(g.LevelIndex==44&&g.IsOpening,"old out-of-range entry is bounded to final new level");
  app.QANavigate("home");yield return null;
  typeof(AppUI).GetMethod("Settings",BindingFlags.NonPublic|BindingFlags.Instance).Invoke(app,null);yield return new WaitForSecondsRealtime(.3f);
  Check(!app.GetComponentsInChildren<Button>().Any(b=>b.isActiveAndEnabled&&b.name=="试玩统计"),"statistics entry is hidden");
  Check(app.Metrics!=null&&app.Metrics.Balance!=null,"statistics collection remains attached");
  Check(typeof(AppUI).GetMethod("OpenBalanceStatistics",BindingFlags.NonPublic|BindingFlags.Instance)!=null,"statistics interface retained");
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures+errors==0?0:3);
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
