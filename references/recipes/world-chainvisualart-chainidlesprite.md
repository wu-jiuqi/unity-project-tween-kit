# ChainIdleSprite · ChainVisualArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/ChainVisualArt.cs`
文件 SHA-256：`7c8976dbc75db1f453ce6734776ff2bb08298e8b2a4c598c1aba6c4cfd6cd66c`
[原始源码快照](../source/Assets/Scripts/World/ChainVisualArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Tick · L26

方法 SHA-256：`576e7359c74adf4d2706c2e8f425f2ab381e098e125d62cad88194851d95b72d`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.ChainIdleSprite.Tick(float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Tick(float dt){if(owner==null||generation!=owner.Generation||owner.IsPaused||!owner.IsPlaying)return;age+=dt;VisibleFrame=Mathf.FloorToInt(age*10)%6;image.sprite=ChainVisualArt.Frame(0,row,VisibleFrame);image.transform.localPosition=Vector3.up*(Mathf.Sin(age*3)*.025f);}
```

### Update · L27

方法 SHA-256：`be95860bb548db39ada0063b46b33a80fa9535878dc072a592839eec8a5f6c03`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainIdleSprite.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){Tick(Time.deltaTime);}
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
int row,generation;
```
```csharp
float age;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
