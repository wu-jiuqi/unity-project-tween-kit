# GameSession · Refill80QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Refill80QA.cs`
文件 SHA-256：`66553f9219608b5d5fb7ec21792d1d0a7a46c071c221894e5ca17bf4a405228e`
[原始源码快照](../source/Assets/Scripts/Refill80QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### QARefillStatus · L15

方法 SHA-256：`d90404c6fbea7a6e79c6de50cdfaee7729927e502216996494cb2dd4e0754e6e`
时间来源：Time.time, Time.timeScale
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameSession.QARefillStatus()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/LevelBalanceRunner.cs:80` · `LevelBalanceRunner.Run` → `game.QARefillStatus()`
- `Assets/Scripts/LevelFiveSettlementQA.cs:31` · `LevelFiveSettlementQA.Start` → `g.QARefillStatus()`
- `Assets/Scripts/Refill80QA.cs:64` · `Refill80QA.Start` → `g.QARefillStatus()`
- `Assets/Scripts/ThirdGroupQA.cs:26` · `ThirdGroupQA.Tick` → `g.QARefillStatus()`
- `Assets/Scripts/ThirdGroupQA.cs:40` · `ThirdGroupQA.Start` → `g.QARefillStatus()`
- `Assets/Scripts/VictoryFinaleQA.cs:28` · `VictoryFinaleQA.Start` → `g.QARefillStatus()`

```csharp
public string QARefillStatus()=>"paused="+IsPaused+" scale="+Time.timeScale+" time="+Time.time+" suction="+SevenSuctionFx.Alive(this)+" until="+sevenFxUntil+" clock="+simulationClock+" beams="+chainBeams.Count+" loot="+chainLoot.Count+" specialFx="+SevenSpecialFx.Alive(this)+" jelly="+fx.JellyActiveCount+" barrier="+sevenRefillBarrier+" await="+sevenAwaitRefill+" freezeBoard="+SevenFreezeBoard+" animations="+SevenAnimationsPending+" depth="+sevenEffectDepth+" presentation="+sevenPresentationPending+" pending="+RefillPending+" motions="+chainMotions.Count+" dirty="+chainGravityDirty+" protected="+CollectionBricks.Count(b=>b.Alive&&RefillProtected(b))+" occupied="+ChainOccupiedCells()+" generated="+RefillGenerated+" full="+RefillFullBatches+" waits="+sevenWaiting.Count;
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
