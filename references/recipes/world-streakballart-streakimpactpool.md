# StreakImpactPool · StreakBallArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/StreakBallArt.cs`
文件 SHA-256：`8fcead37d6992a1f5a2038b7ee56d122340d32408ed3ef58875d043899f0ee19`
[原始源码快照](../source/Assets/Scripts/World/StreakBallArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L39

方法 SHA-256：`6fd41b2c20e907a9d5373824a8fdc5cc6b7693c5870074fce1ffc594db37b9b6`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.StreakImpactPool.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(owner==null||owner.IsPaused)return;ActiveCount=0;for(int i=0;i<slots.Length;i++){ref var s=ref slots[i];if(!s.Active)continue;s.Age+=Time.deltaTime;int frame=(int)(s.Age*16);if(!owner.IsPlaying||frame>=8){s.Active=false;s.View.gameObject.SetActive(false);continue;}s.View.sprite=StreakBallArt.Frame(s.Kind,8+frame);ActiveCount++;}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
readonly Slot[] slots=new Slot[32];
```
```csharp
GameSession owner;
```
```csharp
int cursor;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
