# SevenSpecialIdle · SevenSpecialArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/SevenSpecialArt.cs`
文件 SHA-256：`27e81783a35de093faa96cba51fed1f8ee7dcaa0bfe508d9e00b45f7a80ad03e`
[原始源码快照](../source/Assets/Scripts/World/SevenSpecialArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L61

方法 SHA-256：`900fa4d4c8d5cb15bd20a48d52a443f3fd6b93851dae135f3060434d7f0d4790`
时间来源：PresentationDelta, Time.deltaTime
曲线证据：Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.SevenSpecialIdle.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(owner==null||owner.Generation!=generation){Destroy(gameObject);return;}if(owner.IsPaused)return;age+=owner.PresentationDelta(Time.deltaTime);born=Mathf.Min(1,born+owner.PresentationDelta(Time.deltaTime)/.2f);if(waiting){float u=Mathf.Clamp01((age-waitAt)/.16f);transform.localScale=Vector3.one*Mathf.Lerp(1,1.14f,1-Mathf.Pow(1-u,3));return;}Render();float grow=1-Mathf.Pow(1-born,3);transform.localScale=Vector3.one*(grow*(1+Mathf.Sin(age*3)*.015f));}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
GameSession owner;
```
```csharp
ChainTool tool;
```
```csharp
SpriteRenderer face;
```
```csharp
float age,born,waitAt;
```
```csharp
bool waiting;
```
```csharp
int generation;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
