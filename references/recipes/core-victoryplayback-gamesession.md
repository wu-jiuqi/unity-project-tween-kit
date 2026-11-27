# GameSession · VictoryPlayback

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/VictoryPlayback.cs`
文件 SHA-256：`1f47a6306a3ea304b9c07572462686988906b3ad35bf32754ddfbefa82099fbd`
[原始源码快照](../source/Assets/Scripts/Core/VictoryPlayback.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### PresentationDelta · L9

方法 SHA-256：`62118c00d7098f7e2d69e6ab9ab423fef8db8c028f17977946085f2401226028`
时间来源：PresentationDelta, Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameSession.PresentationDelta(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/IngotSpecialFx.cs:21` · `IngotSpecialFx.Advance` → `owner.PresentationDelta(dt)`
- `Assets/Scripts/World/SevenSpecialArt.cs:88` · `SevenSpecialFx.Advance` → `owner.PresentationDelta(dt)`
- `Assets/Scripts/World/SevenSpecialCinematics.cs:17` · `SevenWindup.Advance` → `owner.PresentationDelta(dt)`
- `Assets/Scripts/World/SevenSpecialCinematics.cs:35` · `SevenSuctionFx.Advance` → `owner.PresentationDelta(dt)`

```csharp
// Clamp before multiplying so 30 FPS still receives the full 2x step.
 public float PresentationDelta(float ordinaryDelta)=>VictoryPlaybackPending?Mathf.Min(Time.unscaledDeltaTime,.05f)*VictoryPlaybackSpeed:ordinaryDelta;
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
