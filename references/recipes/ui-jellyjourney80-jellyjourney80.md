# JellyJourney80 · JellyJourney80

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/JellyJourney80.cs`
文件 SHA-256：`9468a7f3f17a1a25f0716170d5b114fefe8c8f2c8d6065608055287e9bb36b87`
[原始源码快照](../source/Assets/Scripts/UI/JellyJourney80.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### MotionClock · L15

方法 SHA-256：`fc9e73f897cb4f5200fef26562ce5a1f88745660acf3f6adeca48f9e783e9882`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyJourney80.MotionClock`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public float MotionClock=>AuditActive?auditClock:Time.unscaledTime;
```

### Sway · L36

方法 SHA-256：`67ae868968541d53aa0a9acfc89cd58d60869400578045654c6096ab2483873c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.JellyJourney80.Sway(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyJourney80.cs:37` · `JellyJourney80.RoutePoint` → `Sway(index)`
- `Assets/Scripts/UI/JellyJourneyMotion80.cs:31` · `JellyLevelMotion80.Apply` → `Owner.Sway(index)`

```csharp
public float Sway(int index)=>DragPull*18*Mathf.Sin(index*1.31f+.8f);
```

### PullJelly · L39

方法 SHA-256：`83d6bda392b6f95effc83b5583bc1248e0af39b1089262d1489e76f8cd2a896a`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyJourney80.PullJelly(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyJourneyMotion80.cs:49` · `JellyMapDrag80.OnDrag` → `.PullJelly(e.delta.y)`

```csharp
public void PullJelly(float pixels){dragSpring?.Kill();float design=pixels*1080/Mathf.Max(1,Screen.width);pull=Mathf.Lerp(pull,Mathf.Clamp(design/90,-1,1),.5f);}
```

### EndJellyDrag · L40

方法 SHA-256：`69575d7d04a1c98f0ddcd98ad6b630b796f1d5c569bb3688213c2785c5256d0d`
时间来源：SetUpdate(true)
曲线证据：Ease.OutElastic
解析符号：`BounceGarden.JellyJourney80.EndJellyDrag()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyJourneyMotion80.cs:50` · `JellyMapDrag80.OnEndDrag` → `.EndJellyDrag()`
- `Assets/Scripts/UI/JellyJourneyMotion80.cs:51` · `JellyMapDrag80.OnDisable` → `.EndJellyDrag()`

```csharp
public void EndJellyDrag(){dragging=false;dragSpring?.Kill();if(!gameObject.activeInHierarchy){pull=0;return;}dragSpring=DOTween.To(()=>pull,v=>pull=v,0,.8f).SetEase(Ease.OutElastic,1.1f,.28f).SetUpdate(true).SetTarget(this);}
```

### LateUpdate · L74

方法 SHA-256：`e44650874994cca4bb0d93e6b32f0c5978a98d8f85303bcb85ebf2d116980174`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyJourney80.LateUpdate()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void LateUpdate(){if(profile==null)return;if(progress!=profile.AlphabetUnlocked)RefreshProfile();if(MotionAllowed){if(!dragging&&dragSpring==null)pull=Mathf.MoveTowards(pull,0,Time.unscaledDeltaTime*4);route.RefreshMotion();}else if(pull!=0){dragSpring?.Kill();pull=0;route.RefreshMotion();}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
bool positioned,dragging,focused=true,backgroundPaused;
```

```csharp
void OnApplicationFocus(bool value){focused=value;if(!value)CancelMotion();}
```

```csharp
void OnApplicationPause(bool value){backgroundPaused=value;if(value)CancelMotion();}
```

```csharp
void CancelMotion(){dragSpring?.Kill();dragSpring=null;pull=0;dragging=false;scroll?.StopMovement();if(route!=null)route.RefreshMotion();}
```

```csharp
void OnDisable(){dragSpring?.Kill();dragSpring=null;pull=0;dragging=false;scroll?.StopMovement();}
```

## 源字段与依赖

```csharp
public const float Step=440,Padding=400;
```
```csharp
LocalProfile profile;
```
```csharp
AppUI app;
```
```csharp
Action<int> start;
```
```csharp
Action<string> notify;
```
```csharp
ScrollRect scroll;
```
```csharp
RectTransform viewport,content,header,footer,background,islands,nodes;
```
```csharp
Image gardenLeft,gardenRight;
```
```csharp
Button locate;
```
```csharp
JellyRouteRibbon80 route;
```
```csharp
readonly Dictionary<int,Node> live=new Dictionary<int,Node>();
```
```csharp
readonly Stack<Node> spare=new Stack<Node>();
```
```csharp
readonly List<int> retire=new List<int>();
```
```csharp
float height,area,lastH=-1,lastReserve=-1,lastTop=-1,lastBottom=-1,pull;
```
```csharp
int progress=-1,rangeMin=-1,rangeMax=-1;
```
```csharp
bool positioned,dragging,focused=true,backgroundPaused;
```
```csharp
Tween dragSpring;
```
```csharp
float auditClock=float.NaN;
```
```csharp
float lastFooterReserve=-1;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
