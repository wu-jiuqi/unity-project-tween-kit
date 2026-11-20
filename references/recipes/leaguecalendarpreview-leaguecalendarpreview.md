# LeagueCalendarPreview · LeagueCalendarPreview

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LeagueCalendarPreview.cs`
文件 SHA-256：`da9d816d1a2413ec314c19adbdd0cf64ed44dbfb6597f3170d96584af3371d38`
[原始源码快照](../source/Assets/Scripts/LeagueCalendarPreview.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L9

方法 SHA-256：`1340aafd3c22f8c45b96ca58e1b26d4cbf55486430e05a307b11c8506f56fbd2`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LeagueCalendarPreview.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){yield return new WaitForSecondsRealtime(3);var app=FindFirstObjectByType<AppUI>();app.QANavigate("home");bool returning=Environment.GetCommandLineArgs().Contains("--preview-return");
  long now=DateTimeOffset.UtcNow.ToUnixTimeSeconds();var clock=new Clock{now=returning?now-7*86400:now};var c=new ResourceLeagueConfigProvider().Load();app.Profile.ConfigureLeague(new JsonLeagueConfigProvider(()=>JsonUtility.ToJson(c)),clock);
  for(int i=0;i<10;i++)app.Profile.CommitAlphabetWin("preview-"+i,i,10000);clock.now=now;app.Profile.ReadLeague(true);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
