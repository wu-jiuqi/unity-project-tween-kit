# AppUI · CollectionGoalEntrance

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/CollectionGoalEntrance.cs`
文件 SHA-256：`e830eb8ef97a4c7d3c66b949778e9dba82e22790c0a865dd2a9ee6ab208fa4f8`
[原始源码快照](../source/Assets/Scripts/UI/CollectionGoalEntrance.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### ShowCollectionGoalEntrance · L8

方法 SHA-256：`2f409e0d9cdf7f3473f437167f9e82e99f0bfeb98b5ae6fcdbc07a6eabe33604`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.ShowCollectionGoalEntrance(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:183` · `AppUI.StartPreparedLevel` → `ShowCollectionGoalEntrance(generation)`

```csharp
IEnumerator ShowCollectionGoalEntrance(int generation){
  var hud=GetComponentInChildren<ChainCollectionHUD>();if(hud==null||hud.Cards.Count==0)yield break;
  var root=UICraft.Root(canvasRoot,"Collection goals center-to-header entrance");
  QAGoalEntrance=root.gameObject.AddComponent<CollectionGoalEntrance>();QAGoalEntrance.Begin(hud.GoalsRect);
  while(QAGoalEntrance!=null&&!QAGoalEntrance.Finished&&generation==viewGeneration)yield return null;
  CloseCollectionGoalEntrance();
 }
```

## 对象层级和素材入口

- L10：`UICraft.Root(canvasRoot,"Collection goals center-to-header entrance")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
