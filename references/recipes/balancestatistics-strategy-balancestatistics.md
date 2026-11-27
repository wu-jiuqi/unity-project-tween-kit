# BalanceStatistics · BalanceStatistics.Strategy

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/BalanceStatistics.Strategy.cs`
文件 SHA-256：`9931817e46295420262c2bebcd98b4fd8e53ebccfdd5024155b455f8de408212`
[原始源码快照](../source/Assets/Scripts/BalanceStatistics.Strategy.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### TickStrategy · L19

方法 SHA-256：`134183b5617d595577a728ea1d0055f0ef3a6f3797519d403537f68b72b4812d`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BalanceStatistics.TickStrategy(bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/BalanceStatistics.cs:111` · `BalanceStatistics.Update` → `TickStrategy(active)`

```csharp
void TickStrategy(bool active){strategyRecorder?.Tick(Time.unscaledDeltaTime,active);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
PlayerStrategyRecorder strategyRecorder;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
