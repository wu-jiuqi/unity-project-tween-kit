# MainlineFrameFx · MainlineArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/MainlineArt.cs`
文件 SHA-256：`2ebd3b54eaa82386d127f624a448b55496272a37109321ab3407fd6fc16f91b8`
[原始源码快照](../source/Assets/Scripts/World/MainlineArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L42

方法 SHA-256：`8585b2369e004ba5268134162dcb6a1e9ec833dba145694e20cfe5e0281ed193`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.MainlineFrameFx.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(owner==null){Destroy(gameObject);return;}if(owner.IsPaused)return;age+=Time.deltaTime;int f=first+(int)(age*14);if(f>last){Destroy(gameObject);return;}image.sprite=MainlineArt.Frame(sheet,row,f);}
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
string sheet;
```
```csharp
int row,first,last;
```
```csharp
float age;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
