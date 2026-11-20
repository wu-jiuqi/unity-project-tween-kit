# ChainCubeBurst · ChainCubeArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/ChainCubeArt.cs`
文件 SHA-256：`ad5006d51aac98c515c5957af427ccbe1e50c4e37299633038dc1b0c928abc76`
[原始源码快照](../source/Assets/Scripts/World/ChainCubeArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L41

方法 SHA-256：`12939040b9ed7709b17ec2a56d772185c6f940de18245314002db252a28e553d`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainCubeBurst.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(owner==null||owner.Generation!=generation){Destroy(gameObject);return;}if(owner.IsPaused)return;age+=Time.deltaTime;image.sprite=ChainCubeArt.Frame(3,Mathf.Min(5,(int)(age*18)));if(age>=6f/18)Destroy(gameObject);}
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
int generation;
```
```csharp
float age;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
