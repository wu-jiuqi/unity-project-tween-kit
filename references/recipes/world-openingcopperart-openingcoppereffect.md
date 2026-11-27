# OpeningCopperEffect · OpeningCopperArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/OpeningCopperArt.cs`
文件 SHA-256：`7ce8bef6518c7d599f30c6b642cdc5200c65fdbdde402373c824cdc207029a9d`
[原始源码快照](../source/Assets/Scripts/World/OpeningCopperArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Render · L44

方法 SHA-256：`2b90bbd0a61008d717648142c733089a740353eb17219a3ae2a9d1f41a2bfeac`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin, Mathf.SmoothStep
解析符号：`BounceGarden.OpeningCopperEffect.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/OpeningCopperArt.cs:45` · `OpeningCopperEffect.Tick` → `Render()`

```csharp
void Render(){VisibleFrame=Mathf.Clamp(Mathf.FloorToInt(age*20),0,7);OpeningCopperArt.Fit(burst,OpeningCopperArt.Frame("burst",VisibleFrame),collect?1.25f:.88f);burst.color=new Color(1,1,1,collect?1:.65f);if(coin!=null){float u=Mathf.Clamp01(age/.4f);OpeningCopperArt.Fit(coin,OpeningCopperArt.State(OpeningCopperArt.MaxHealth),Mathf.Lerp(.76f,.48f,Mathf.SmoothStep(0,1,u)));coin.transform.localPosition=Vector2.up*(Mathf.Sin(u*Mathf.PI)*.19f);coin.transform.localRotation=Quaternion.Euler(0,0,Mathf.Sin(u*Mathf.PI)*12);}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
System.Action strategyArrival;
```
```csharp
GameSession game;
```
```csharp
int generation;
```
```csharp
bool collect,credited;
```
```csharp
float age;
```
```csharp
SpriteRenderer burst,coin;
```
```csharp
Vector2 origin;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
