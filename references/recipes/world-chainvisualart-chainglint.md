# ChainGlint · ChainVisualArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/ChainVisualArt.cs`
文件 SHA-256：`7c8976dbc75db1f453ce6734776ff2bb08298e8b2a4c598c1aba6c4cfd6cd66c`
[原始源码快照](../source/Assets/Scripts/World/ChainVisualArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L20

方法 SHA-256：`f501821e151bd0f387719e08ae36448ab812eee8abe17bc25518095ea1dd30ae`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainGlint.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(owner==null||owner.Generation!=generation){Destroy(gameObject);return;}if(owner.IsPaused)return;age+=Time.deltaTime;image.color=new Color(1,1,1,Mathf.Clamp01(1-age/.24f));transform.localRotation=Quaternion.Euler(0,0,age*80);if(age>=.24f)Destroy(gameObject);}
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
float age;
```
```csharp
int generation;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
