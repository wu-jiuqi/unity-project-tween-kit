# SevenSuctionFx · SevenSpecialCinematics

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/SevenSpecialCinematics.cs`
文件 SHA-256：`ba1d34624edba93f5a13507d89e413ace978773343f4d9bb47b5552cf3b20f7d`
[原始源码快照](../source/Assets/Scripts/World/SevenSpecialCinematics.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L34

方法 SHA-256：`dd48231602816cd00026ee07415370d00e68438dd91d49dc2a49c63082aa2a9d`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SevenSuctionFx.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(BalanceClock.Active)return;Advance(Time.unscaledDeltaTime);}
```

### Advance · L35

方法 SHA-256：`649e3a18b4d9054634e2980c418f19ce9ea5f11f1db8bc68170322066a555347`
时间来源：PresentationDelta
曲线证据：Mathf.Cos, Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.SevenSuctionFx.Advance(float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Advance(float dt){if(owner==null||owner.Generation!=generation){Destroy(gameObject);return;}if(owner.IsPaused)return;age+=owner.PresentationDelta(dt);float t=Mathf.Clamp01(age/duration);if(image!=null){var delta=start-center;float angle=t*t*4.4f;float c=Mathf.Cos(angle),s=Mathf.Sin(angle);var rotated=new Vector2(delta.x*c-delta.y*s,delta.x*s+delta.y*c);transform.position=center+rotated*Mathf.Pow(1-t,1.55f);transform.localScale=scale*Mathf.Pow(1-t,.7f);transform.rotation=Quaternion.Euler(0,0,t*210);var color=image.color;color.a=Mathf.Min(color.a,(1-t)*6);image.color=color;}if(age<duration)return;var action=done;done=null;if(BalanceClock.Active){OnDestroy();owner=null;gameObject.SetActive(false);}Destroy(gameObject);action?.Invoke();}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(!ReferenceEquals(owner,null)&&alive.TryGetValue(owner,out int n)){if(n<=1)alive.Remove(owner);else alive[owner]=n-1;}}
```

## 源字段与依赖

```csharp
static readonly Dictionary<GameSession,int> alive=new Dictionary<GameSession,int>();
```
```csharp
GameSession owner;
```
```csharp
int generation;
```
```csharp
Vector2 start,center;
```
```csharp
Vector3 scale;
```
```csharp
SpriteRenderer image;
```
```csharp
float age,duration;
```
```csharp
Action done;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
