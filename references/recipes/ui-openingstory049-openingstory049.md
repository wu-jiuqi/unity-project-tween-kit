# OpeningStory049 · OpeningStory049

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/OpeningStory049.cs`
文件 SHA-256：`52543b2c315cd7de5ff960db59eb45811dcf5c8ce34d3b98772b2befd2ceca55`
[原始源码快照](../source/Assets/Scripts/UI/OpeningStory049.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `OpeningStory049` / `teaching`：0.5.1表现切片；KitClock替换Unity时钟；宿主业务与屏幕布局适配见 current-051.md
- `ThreeStepLoop` / `teaching`：手工抽取纯表现接口；保留时序，业务事件由宿主输入

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### Update · L25

方法 SHA-256：`e7f62440c3cab624a2296bcd2d8fc89360eb3646c77523933ae00e6df0255026`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OpeningStory049.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){Age+=Time.unscaledDeltaTime;Render();}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public const float LoopDuration=6.72f;
```
```csharp
public static readonly Color Cream=new Color32(255,241,214,255),Gold=new Color32(255,218,140,255),Mint=new Color32(188,233,204,255),Muted=new Color32(210,200,182,255);
```
```csharp
OpeningStoryStep049[] steps=new OpeningStoryStep049[3];
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
