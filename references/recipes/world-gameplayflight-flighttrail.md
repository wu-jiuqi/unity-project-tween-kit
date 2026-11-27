# FlightTrail · GameplayFlight

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/GameplayFlight.cs`
文件 SHA-256：`b7e6543790b143ba33906fd637b772182797dc015a74655d0f50cbff277ef077`
[原始源码快照](../source/Assets/Scripts/World/GameplayFlight.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Configure · L22

方法 SHA-256：`ff06248140651520985d78b0a9a1df80187084f086116c6b13df00d52270b119`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：AnimationCurve
解析符号：`BounceGarden.FlightTrail.Configure(UnityEngine.LineRenderer, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/GameplayFlight.cs:21` · `FlightTrail.Create` → `Configure(line,44)`
- `Assets/Scripts/World/GameplayFlight.cs:21` · `FlightTrail.Create` → `Configure(glow,43)`

```csharp
static void Configure(LineRenderer renderer,int order){renderer.useWorldSpace=true;renderer.sortingOrder=order;renderer.numCornerVertices=0;renderer.numCapVertices=3;renderer.textureMode=LineTextureMode.Stretch;renderer.positionCount=0;renderer.widthCurve=new AnimationCurve(new Keyframe(0,.02f),new Keyframe(.30f,.45f),new Keyframe(1,1));}
```

### BindYarn · L23

方法 SHA-256：`03aefad8e72d32d394d723cb3a1b63ae033909ad881947823b5fa753e2224554`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FlightTrail.BindYarn(UnityEngine.Material)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/GameplayFlight.cs:24` · `FlightTrail.SetYarnVariant` → `BindYarn(YarnArt.Strand(variant))`
- `Assets/Scripts/World/GameplayFlight.cs:25` · `FlightTrail.SetStreakBooster` → `BindYarn(StreakBallArt.Trail(kind))`

```csharp
void BindYarn(Material source){line.sharedMaterial=glow.sharedMaterial=VisibleYarn(source);var outer=new MaterialPropertyBlock();outer.SetFloat("_Glow",1);glow.SetPropertyBlock(outer);}
```

### LateUpdate · L34

方法 SHA-256：`420d95513bb4e13f8a0c00a86e34118ad63134a89179373056254263ce4ddd8b`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FlightTrail.LateUpdate()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void LateUpdate(){
  if(owner==null||owner.IsPaused||(!emitting&&count==0))return;
  if(!owner.IsPlaying)emitting=false;
  clock+=Time.deltaTime;var f=FeedbackProfile.Active;
  // Rigidbody interpolation advances the rendered ball between fixed ticks.
  // Sample its rendered Transform too, so the strand stays attached to it.
  // Authoritative fixed-step samples retain sharp bounce corners. Only the last
  // rendered vertex follows interpolation, avoiding backtracking hooks each frame.
  int power=0;
  float lifetime=f.trailDuration+power*.025f,maxLength=f.trailMaxLength+power*.25f;
  while(count>0&&clock-times[0]>lifetime)DropFirst();
  float length=0;for(int i=count-1;i>0;i--){float segment=Vector3.Distance(points[i],points[i-1]);if(length+segment>maxLength){float keep=Mathf.Max(0,maxLength-length);points[i-1]=Vector3.Lerp(points[i],points[i-1],segment>0?keep/segment:0);while(i>1){DropFirst();i--;}break;}length+=segment;}
  line.enabled=glow.enabled=f.enabledFeedback;float width=f.trailStartWidth*(tracked!=null&&tracked.StreakPower!=StreakBooster.None?1.25f:1)*(1+power*.22f);line.widthMultiplier=width;glow.widthMultiplier=width*2.1f;
  float fade=count>0?Mathf.Clamp01(1-(clock-times[count-1])/lifetime):0;
  line.endColor=new Color(1,1,1,fade*f.trailOpacity);line.startColor=new Color(1,1,1,0);glow.endColor=new Color(1,1,1,fade*.22f);glow.startColor=new Color(1,1,1,0);
  int visible=count;if(emitting&&head!=null)while(visible>1&&Vector3.Dot(head.position-points[visible-2],points[visible-1]-points[visible-2])<0)visible--;
  line.positionCount=glow.positionCount=visible;if(visible>0){line.SetPositions(points);glow.SetPositions(points);if(emitting&&head!=null){line.SetPosition(visible-1,head.position);glow.SetPosition(visible-1,head.position);}}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public void ResetTrail(Vector2 p){count=0;clock=0;emitting=true;Sample(p);}
```

```csharp
public void Stop(bool fade){emitting=false;if(!fade){count=0;line.positionCount=glow.positionCount=0;}}
```

## 源字段与依赖

```csharp
readonly Vector3[] points=new Vector3[256];
```
```csharp
readonly float[] times=new float[256];
```
```csharp
LineRenderer line,glow;
```
```csharp
GameSession owner;
```
```csharp
Transform head;
```
```csharp
int count;
```
```csharp
float clock;
```
```csharp
bool emitting;
```
```csharp
BallBody tracked;
```
```csharp
static readonly System.Collections.Generic.Dictionary<Material,Material> yarnMaterials=new System.Collections.Generic.Dictionary<Material,Material>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
