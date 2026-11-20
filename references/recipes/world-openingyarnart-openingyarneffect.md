# OpeningYarnEffect · OpeningYarnArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/OpeningYarnArt.cs`
文件 SHA-256：`830334db086b32c4f88c06658d4a866fccb34ffbb203cc9279cfe907383ce7b4`
[原始源码快照](../source/Assets/Scripts/World/OpeningYarnArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Render · L43

方法 SHA-256：`bffe6d54018196098e54bb9b0b403379bbba75e50c08ab50d6fa9b3fa6362845`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.OpeningYarnEffect.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/OpeningYarnArt.cs:44` · `OpeningYarnEffect.Tick` → `Render()`

```csharp
void Render(){float duration=collect?UnravelDuration:LayerDuration;VisibleFrame=Mathf.Clamp(Mathf.FloorToInt(age/duration*16),0,15);image.sprite=OpeningYarnArt.Frame(key,VisibleFrame);float pop=collect?1:1+Mathf.Sin(Mathf.Clamp01(age/.18f)*Mathf.PI)*.035f;image.transform.localScale=OpeningYarnArt.Fit(key,VisibleFrame)*pop;}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public const float LayerDuration=.68f,UnravelDuration=.64f;
```
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
SpriteRenderer image;
```
```csharp
string key;
```
```csharp
float age;
```
```csharp
bool collect,credited,sounded;
```
```csharp
Transform followedBody;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
