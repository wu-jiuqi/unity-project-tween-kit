# SecondGroupPreview · SecondGroupIntegrationQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/SecondGroupIntegrationQA.cs`
文件 SHA-256：`b008b3c4521c3ffaa3c8029e7349f870836e9e577a58af8b2a330f91412dbebc`
[原始源码快照](../source/Assets/Scripts/SecondGroupIntegrationQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L60

方法 SHA-256：`80d5661aa5127de809709047ef4ea9939e2af6002fadb1bc2dd8b308d04c817f`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SecondGroupPreview.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){yield return new WaitForSecondsRealtime(3);var a=Environment.GetCommandLineArgs();int id=int.Parse(a[Array.IndexOf(a,"--second-group-preview")+1]);var app=FindFirstObjectByType<AppUI>();app.QAStartLevel(id-1);yield return app.QAWaitForLevel();app.Session.QAIgnoreFocusLoss=true;}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
