# OpeningCopperView · OpeningCopperArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/OpeningCopperArt.cs`
文件 SHA-256：`ed09cfd557c70bd1a483965b03ae24bcfc1939043dc4a02e27b648be628b103d`
[原始源码快照](../source/Assets/Scripts/World/OpeningCopperArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Render · L28

方法 SHA-256：`668d7f1ef72683655ed85626e7de545c84fbbc4cd998d299f3005235a2af9898`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.SmoothStep
解析符号：`BounceGarden.OpeningCopperView.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/OpeningCopperArt.cs:24` · `OpeningCopperView.Initialize` → `Render()`
- `Assets/Scripts/World/OpeningCopperArt.cs:26` · `OpeningCopperView.Hit` → `Render()`
- `Assets/Scripts/World/OpeningCopperArt.cs:27` · `OpeningCopperView.Tick` → `Render()`

```csharp
void Render(){
  if(Brick==null||!Brick.Alive)return;Brick.Face.enabled=false;
  // Both state sprites use exactly the same .76 square; no per-state shrink or position drift.
  OpeningCopperArt.Fit(Image,OpeningCopperArt.State(Brick.CurrentHealth),.76f);Image.color=Color.white;
  float blend=Mathf.SmoothStep(0,1,Mathf.Clamp01(age/.12f));previous.enabled=age<.12f;
  if(previous.enabled){OpeningCopperArt.Fit(previous,OpeningCopperArt.State(oldHp),.76f);previous.color=new Color(1,1,1,1-blend);Image.color=new Color(1,1,1,blend);}
  // Match the cat impact, deforming both sprite states and number together.
  var stretch=ImpactStretch;visual.localScale=new Vector3(stretch.x,stretch.y,1);visual.localRotation=Quaternion.identity;
  number.text=Brick.CurrentHealth.ToString();
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
GameSession game;
```
```csharp
SpriteRenderer previous;
```
```csharp
Transform visual;
```
```csharp
TMP_Text number;
```
```csharp
Vector2 impactDirection=Vector2.up;
```
```csharp
float age=1;
```
```csharp
int oldHp;
```
```csharp
readonly HashSet<long> attacks=new HashSet<long>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
