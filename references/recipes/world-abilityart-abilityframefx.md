# AbilityFrameFx · AbilityArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/AbilityArt.cs`
文件 SHA-256：`6500de480b769935afdd28f3041fb151728413fa09bae822c2ade28408735bbc`
[原始源码快照](../source/Assets/Scripts/World/AbilityArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L15

方法 SHA-256：`77969c5ba80bb9ee91b842c65c330927a9acd348f2bf536ad5665fd00119fbe8`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AbilityFrameFx.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(owner==null||!owner.IsPlaying){Destroy(gameObject);return;}if(owner.IsPaused)return;clock+=Time.deltaTime;int f=Mathf.FloorToInt(clock*14);if(f>=6){Destroy(gameObject);return;}image.sprite=AbilityArt.Effect(row,f);}
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
float clock;
```
```csharp
int row;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
