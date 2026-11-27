# SevenWindup · SevenSpecialCinematics

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/SevenSpecialCinematics.cs`
文件 SHA-256：`abeddcf4671a69c5b9b36ee0faf35bd2114fd68601f5cf76d74448d6f8c175d6`
[原始源码快照](../source/Assets/Scripts/World/SevenSpecialCinematics.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L16

方法 SHA-256：`dd48231602816cd00026ee07415370d00e68438dd91d49dc2a49c63082aa2a9d`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SevenWindup.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(BalanceClock.Active)return;Advance(Time.unscaledDeltaTime);}
```

### Advance · L17

方法 SHA-256：`c357aef8e59bd87c9b8bc2e060a16bee3068ab0032750fb4b029a1e4ab43293b`
时间来源：PresentationDelta
曲线证据：Mathf.Pow, Mathf.Sin, Mathf.SmoothStep
解析符号：`BounceGarden.SevenWindup.Advance(float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Advance(float dt){if(owner==null||owner.Generation!=generation){Destroy(gameObject);return;}if(owner.IsPaused){wasPaused=true;return;}if(wasPaused){owner.CombatFeel?.EmphasizeSpecial(duration-age+.05f);wasPaused=false;}age+=owner.PresentationDelta(dt);float t=Mathf.Clamp01(age/duration);
  // Source bricks remain at their original anchors and rebound at 1.22x.
  // Single specials never freeze the game.
  float scale=t<.25f?Mathf.Lerp(1,1.22f,1-Mathf.Pow(1-t/.25f,3)):t<.55f?1.22f:Mathf.Lerp(1.22f,1,Mathf.SmoothStep(0,1,(t-.55f)/.45f));
  float wobble=t>.55f?Mathf.Sin((t-.55f)/.45f*Mathf.PI*4)*.12f*(1-t):Mathf.Sin(t*Mathf.PI*4)*.04f;
  foreach(var actor in actors)actor.localScale=new Vector3(scale+wobble,scale-wobble,1);
   if(age<duration)return;var action=release;release=null;gameObject.SetActive(false);Destroy(gameObject);action?.Invoke();}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
bool wasPaused;
```

## 源字段与依赖

```csharp
GameSession owner;
```
```csharp
int generation;
```
```csharp
float age,duration;
```
```csharp
Action release;
```
```csharp
readonly List<Transform> actors=new List<Transform>();
```
```csharp
bool wasPaused;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
