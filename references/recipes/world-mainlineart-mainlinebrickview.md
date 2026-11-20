# MainlineBrickView · MainlineArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/MainlineArt.cs`
文件 SHA-256：`2ebd3b54eaa82386d127f624a448b55496272a37109321ab3407fd6fc16f91b8`
[原始源码快照](../source/Assets/Scripts/World/MainlineArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L35

方法 SHA-256：`7c986b058d97714b9013929eabaf792c2f038d3fd009fd9befa23c177cd3a89e`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.MainlineBrickView.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(owner==null||owner.IsPaused||owner.JuiceFrozen||feature==null)return;age+=Time.deltaTime;Render();}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){foreach(var line in links)if(line!=null)line.gameObject.SetActive(false);}
```

## 源字段与依赖

```csharp
GameSession owner;
```
```csharp
BrickBody brick;
```
```csharp
SpriteRenderer feature;
```
```csharp
float age;
```
```csharp
readonly List<SpriteRenderer> links=new List<SpriteRenderer>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
