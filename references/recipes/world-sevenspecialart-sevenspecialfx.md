# SevenSpecialFx · SevenSpecialArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/SevenSpecialArt.cs`
文件 SHA-256：`2d36491873ecc5e803d76a0591aa6c9fb15cd8e3c01055eca399d9e0550d5c72`
[原始源码快照](../source/Assets/Scripts/World/SevenSpecialArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L87

方法 SHA-256：`8a1722c3149f3f2f410d3292de30584cfb7e666a0032cf28287f6ea6d460f338`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SevenSpecialFx.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(BalanceClock.Active)return;Advance(Time.deltaTime);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(!ReferenceEquals(owner,null)&&counts.TryGetValue(owner,out int n)){if(n<=1)counts.Remove(owner);else counts[owner]=n-1;}}
```

## 源字段与依赖

```csharp
static readonly Dictionary<GameSession,int> counts=new Dictionary<GameSession,int>();
```
```csharp
GameSession owner;
```
```csharp
SpriteRenderer face;
```
```csharp
float age,duration=.66f,baseSize;
```
```csharp
int generation,kind,color,colorB,first=7,last=15;
```
```csharp
string sheet;
```
```csharp
MaterialPropertyBlock block;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
