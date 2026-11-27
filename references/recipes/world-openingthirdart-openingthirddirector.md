# OpeningThirdDirector · OpeningThirdArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/OpeningThirdArt.cs`
文件 SHA-256：`18a62aaee2aa40e8a034894b9c497fcf840718696c0be5821f8d0c3abb7f81f7`
[原始源码快照](../source/Assets/Scripts/World/OpeningThirdArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L36

方法 SHA-256：`f91b8d4fd50f5f2b65b00a1f33d034d601f537eee060905f1965a67c759d405e`
时间来源：PresentationDelta, Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OpeningThirdDirector.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(game==null||generation!=game.Generation){Destroy(gameObject);return;}if(Physics2D.simulationMode!=SimulationMode2D.Script)game.TickOpeningThird(game.PresentationDelta(Time.unscaledDeltaTime));}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
GameSession game;
```
```csharp
int generation;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
