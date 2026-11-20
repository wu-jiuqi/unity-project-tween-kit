# JellyJourney80Preview · JellyJourney80QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/JellyJourney80QA.cs`
文件 SHA-256：`0e5f5876cb847692f627924045e28d9c95cdca786d73b70c2f637fb5314e63a7`
[原始源码快照](../source/Assets/Scripts/JellyJourney80QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L34

方法 SHA-256：`8385f460dd0432bb7f5f4d8916ca4a7fdecb0974cbb0859aab7390110a0ec990`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyJourney80Preview.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){yield return new WaitForSecondsRealtime(4);var app=FindFirstObjectByType<AppUI>();if(!RuntimeQA.IsIsolated)yield break;int progress=35;var args=Environment.GetCommandLineArgs();int at=Array.IndexOf(args,"--jelly-progress");if(at>=0&&at+1<args.Length)int.TryParse(args[at+1],out progress);progress=Mathf.Clamp(progress,0,AlphabetCampaign.Count);for(int i=0;i<progress;i++)app.Profile.CommitAlphabetWin("tree-preview-"+i,i,i%3==0?65000:i%3==1?35000:10000);app.QAOpenChapters();}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
