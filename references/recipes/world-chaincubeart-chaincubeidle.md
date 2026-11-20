# ChainCubeIdle · ChainCubeArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/ChainCubeArt.cs`
文件 SHA-256：`ad5006d51aac98c515c5957af427ccbe1e50c4e37299633038dc1b0c928abc76`
[原始源码快照](../source/Assets/Scripts/World/ChainCubeArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L36

方法 SHA-256：`cf9b2ad55321efdb8f03b42c3c8678e50693ed630a46edeb3a781579ce91ff53`
时间来源：Time.deltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.ChainCubeIdle.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(owner==null||owner.Generation!=generation){Destroy(gameObject);return;}if(owner.IsPaused||!owner.IsPlaying)return;age+=Time.deltaTime;image.transform.localScale=Vector3.one*((.86f+Mathf.Sin(age*4)*.012f)/image.sprite.bounds.size.x);}
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
SpriteRenderer image;
```
```csharp
int kind,generation;
```
```csharp
float age;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
