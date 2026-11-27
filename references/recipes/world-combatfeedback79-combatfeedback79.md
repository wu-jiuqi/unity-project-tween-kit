# CombatFeedback79 · CombatFeedback79

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/CombatFeedback79.cs`
文件 SHA-256：`3959a9e444ebe6b7ffa10252e58aa3ac1fd5a54c8cb6294c6ac6678a3ad1b2e0`
[原始源码快照](../source/Assets/Scripts/World/CombatFeedback79.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### EmphasizeSpecial · L28

方法 SHA-256：`1abb7f129816a31ca5b78171d7830b06dd3e38a98df524e4391897380a605382`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79.EmphasizeSpecial(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/SevenSpecialCinematics.cs:17` · `SevenWindup.Advance` → `.EmphasizeSpecial(duration-age+.05f)`

```csharp
public void EmphasizeSpecial(float duration){if(owner==null||owner.IsPaused||!owner.IsPlaying||Physics2D.simulationMode==SimulationMode2D.Script)return;if(Time.unscaledTime<nextSlow)return;nextSlow=Time.unscaledTime+.5f;slowLeft=Mathf.Min(.20f,Mathf.Max(.12f,duration));}
```

### Initialize · L38

方法 SHA-256：`4f6537cf3fe119161533f88fc3cf1bcbbc41d7242286af0da07a9611640534ca`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79.Initialize(BounceGarden.GameSession)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/GameSession.cs:150` · `GameSession.BuildSession` → `CombatFeel.Initialize(this)`

```csharp
public void Initialize(GameSession game) {
  owner=game;cameraJuice=Camera.main.GetComponent<CameraJuice>();
  var board=owner.Board;boardFlash=WorldArt.Draw(transform,"Soft board flash","brick",board.Origin+(board.U*board.Columns+board.V*board.Rows)*.5f,new Vector2(board.Columns*board.CellWidth,board.Rows*board.CellHeight),new Color(1,1,1,0),76);
  var white=new MaterialPropertyBlock();white.SetFloat("_Flash",1);boardFlash.SetPropertyBlock(white);
  for(int i=0;i<drops.Length;i++){var go=new GameObject("Pooled break petal");go.transform.SetParent(transform,false);var v=go.AddComponent<SpriteRenderer>();v.sprite=Frame(1+i%16);v.sharedMaterial=WorldArt.BoardMaterial;v.sortingOrder=78;go.SetActive(false);drops[i].view=v;}
 }
```

### Cue · L44

方法 SHA-256：`569c5f36e768a3ef4951798f3d53ecc6f8a4cd169c56b76d3bd02da14ed5e5c4`
时间来源：Time.unscaledTime
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.CombatFeedback79.Cue(UnityEngine.Vector2, string, UnityEngine.Color, UnityEngine.Vector2)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/GameSession.cs:306` · `GameSession.EmitFeedback` → `CombatFeel.Cue(position,cue,color,feedbackIncoming)`

```csharp
public void Cue(Vector2 position,string cue,Color color,Vector2 incoming) {
  if(!FeedbackProfile.Active.enabledFeedback||owner==null||owner.IsPaused)return;
  if(cue=="hit"){Hits++;return;}
  if(cue!="break")return;
  Breaks++;frameBreaks++;float cell=owner.Board.CellWidth;
  Add(position,position,Color.white,cell*.72f,.045f,0,0,true);
  var profile=FeedbackProfile.Active;float min=Mathf.Clamp(profile.paintMinRadius,.3f,1f),max=Mathf.Clamp(profile.paintMaxRadius,min,2.8f);
  LastMinRadiusCells=float.MaxValue;LastMaxRadiusCells=0;
  float lobe=Mathf.Atan2(incoming.y,incoming.x)+R(-1.2f,1.2f);
  for(int i=0;i<12;i++){
   float angle=i<8?lobe+R(-1.05f,1.05f):R(0,Mathf.PI*2);
   float radius=Mathf.Sqrt(R(min*min,max*max));Vector2 ground=BoardDisplay.Forward(position)+new Vector2(Mathf.Cos(angle),Mathf.Sin(angle)*.55f-.45f)*cell*radius;
   ground.x=Mathf.Clamp(ground.x,-4.35f,4.35f);ground.y=Mathf.Clamp(ground.y,-9.0f,7.0f);Vector2 target=BoardDisplay.Inverse(ground);
   float travel=R(.85f,1.5f),life=travel+Mathf.Max(.8f,profile.paintLife)+R(.15f,.65f)+.55f;
   if(Add(position,target,Color.white,cell*R(.30f,.48f),life,travel,1+random.Next(16),false)){LastMinRadiusCells=Mathf.Min(LastMinRadiusCells,radius);LastMaxRadiusCells=Mathf.Max(LastMaxRadiusCells,radius);}
  }
  float now=Time.unscaledTime;if(now>rollingUntil){rollingBreaks=0;rollingUntil=now+.22f;}
  if(++rollingBreaks>=4){VisualImpact();rollingBreaks=0;rollingUntil=now+.22f;}
 }
```

### RequestStop · L68

方法 SHA-256：`fc3f55d17d2cfa6e6db78f17cf253e5be2da7b1e383eebecafcb215ac6ef0adb`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79.RequestStop(float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
void RequestStop(float duration){pendingStop=Mathf.Max(pendingStop,duration);pendingAt=Time.unscaledTime;}
```

### Update · L82

方法 SHA-256：`9fe5603c3111ea3cbc86e84833772f388cd893f76e023bd57d0961bb2d018414`
时间来源：PresentationDelta, Time.deltaTime, Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update() {
  if(owner==null){CancelStop();return;}if(!owner.IsPlaying)CancelStop();if(owner.IsPaused){CancelStop();return;}
  slowLeft=Mathf.Max(0,slowLeft-Time.unscaledDeltaTime);if(ownsClock){stopLeft-=Time.unscaledDeltaTime;if(stopLeft<=0)ReleaseClock();}AdvanceParticles(owner.PresentationDelta(Time.deltaTime));
 }
```

### AdvanceParticles · L86

方法 SHA-256：`7fea9788e75ca74057474b026a5e323ffdd21132e33fa8e23211ae511b2434fd`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.CombatFeedback79.AdvanceParticles(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/CombatFeedback79.cs:84` · `CombatFeedback79.Update` → `AdvanceParticles(owner.PresentationDelta(Time.deltaTime))`

```csharp
public void AdvanceParticles(float dt) {
  if(dt<=0||owner==null||owner.IsPaused)return;
  flashLeft=Mathf.Max(0,flashLeft-dt);boardFlash.color=new Color(1,1,1,.08f*flashLeft/.065f);
  for(int i=0;i<drops.Length;i++){ref var d=ref drops[i];if(!d.view.gameObject.activeSelf)continue;float before=d.age;d.age+=dt;float t=d.age/d.life;
   if(t>=1){d.view.gameObject.SetActive(false);ActiveParticles--;continue;}
   if(d.flash){d.view.color=new Color(1,1,1,(1-t)*.65f);Size(ref d,1+t*.25f);continue;}
   if(d.age<d.travel){float u=d.age/d.travel,arc=Mathf.Sin(u*Mathf.PI);float flight=1-Mathf.Pow(1-u,2);var ground=Vector2.Lerp(BoardDisplay.Forward(d.origin),BoardDisplay.Forward(d.target),flight);
    ground+=new Vector2(Mathf.Sin(u*11+d.phase)*arc*owner.Board.CellWidth*.16f,arc*d.lift);d.view.transform.position=BoardDisplay.Inverse(ground);d.view.transform.rotation=Quaternion.Euler(0,0,d.rotation+d.spin*u+Mathf.Sin(u*9+d.phase)*arc*20);d.view.color=Color.white;
    Size(ref d,1);var scale=d.view.transform.localScale;scale.x*=Mathf.Lerp(1,Mathf.Max(.2f,Mathf.Abs(Mathf.Cos(u*10+d.phase))),arc);d.view.transform.localScale=scale;}
   else {
    if(before<d.travel){d.view.sortingOrder=-22;d.view.transform.position=d.target;d.view.transform.rotation=Quaternion.Euler(0,0,d.rotation+d.spin);LandedParticles++;}
    float settle=Mathf.Clamp01((d.age-d.travel)/.16f);Size(ref d,1+.04f*Mathf.Sin(settle*Mathf.PI));float fade=Mathf.Clamp01((d.life-d.age)/.55f);d.view.color=new Color(1,1,1,fade*.92f);
   }
  }
 }
```

### LateUpdate · L101

方法 SHA-256：`0243b84ec517bdb32db5de7e0ea6910530ebf904410a1f5b7836ae0f77dd7f5d`
时间来源：Time.timeScale, Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79.LateUpdate()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void LateUpdate() {
  if(owner==null||!owner.IsPlaying||owner.IsPaused){CancelStop();return;}
  if(pendingShake>0&&FeedbackProfile.Active.screenShake&&cameraJuice!=null)cameraJuice.ImpactScreen(pendingShake,pendingLife);
  float now=Time.unscaledTime;
  if(now-pendingAt>.18f)pendingStop=0;
  if(now-windowStart>=1){windowStart=now;windowUsed=0;}
  if(Physics2D.simulationMode==SimulationMode2D.Script){pendingStop=0;ReleaseSlow();}
  else if(!ownsClock){
   if(pendingStop>0&&now>=nextStop){
    ReleaseSlow();float duration=Mathf.Min(.025f,Mathf.Min(pendingStop,Mathf.Max(0,FeedbackProfile.Active.hitStopBudget-windowUsed)));
    if(duration>.005f&&Time.timeScale>0){savedScale=Time.timeScale;Time.timeScale=0;ownsClock=true;stopStarted=now;stopLeft=duration;LastStop=duration;windowUsed+=duration;nextStop=now+.5f;HitStops++;}
    pendingStop=0;
   }
   if(!ownsClock&&slowLeft>0){if(!ownsSlow){slowSavedScale=Time.timeScale;ownsSlow=true;}Time.timeScale=Mathf.Min(slowSavedScale,SpecialSlowScale);}
   else if(!ownsClock)ReleaseSlow();
  }
  pendingShake=pendingLife=0;frameBreaks=0;
 }
```

### ReleaseClock · L120

方法 SHA-256：`ff41c390cdd5b2bfa2b7eb70b2936fc77e5176b7168535c7649c32e041d4120b`
时间来源：Time.timeScale, Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79.ReleaseClock()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/CombatFeedback79.cs:84` · `CombatFeedback79.Update` → `ReleaseClock()`
- `Assets/Scripts/World/CombatFeedback79.cs:122` · `CombatFeedback79.CancelStop` → `ReleaseClock()`

```csharp
void ReleaseClock(){if(ownsClock){windowUsed+=Mathf.Max(0,Time.unscaledTime-stopStarted-LastStop);nextStop=Mathf.Max(nextStop,Time.unscaledTime+.5f);if(Mathf.Approximately(Time.timeScale,0))Time.timeScale=savedScale;}ownsClock=false;stopLeft=0;}
```

### ReleaseSlow · L121

方法 SHA-256：`991b147000068b1d9d8d88b3c7c030240062ef86b266cf4f2038ec8032a1393c`
时间来源：Time.timeScale
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79.ReleaseSlow()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/CombatFeedback79.cs:107` · `CombatFeedback79.LateUpdate` → `ReleaseSlow()`
- `Assets/Scripts/World/CombatFeedback79.cs:110` · `CombatFeedback79.LateUpdate` → `ReleaseSlow()`
- `Assets/Scripts/World/CombatFeedback79.cs:115` · `CombatFeedback79.LateUpdate` → `ReleaseSlow()`
- `Assets/Scripts/World/CombatFeedback79.cs:122` · `CombatFeedback79.CancelStop` → `ReleaseSlow()`

```csharp
void ReleaseSlow(){if(ownsSlow&&Mathf.Approximately(Time.timeScale,Mathf.Min(slowSavedScale,SpecialSlowScale)))Time.timeScale=slowSavedScale;ownsSlow=false;slowLeft=0;}
```

## 对象层级和素材入口

- L42：`go.transform.SetParent(transform,false)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
float pendingStop,pendingAt,pendingShake,pendingLife,nextStop,nextSlow,windowStart,windowUsed,stopLeft,stopStarted,savedScale=1;
```

```csharp
public int HitStops {get;private set;}
```

```csharp
public float LastStop {get;private set;}
```

```csharp
void RequestStop(float duration){pendingStop=Mathf.Max(pendingStop,duration);pendingAt=Time.unscaledTime;}
```

```csharp
public void CancelStop(){rollingBreaks=0;rollingUntil=0;ReleaseClock();ReleaseSlow();nextStop=nextSlow=0;pendingStop=pendingShake=pendingLife=0;frameBreaks=0;}
```

```csharp
void OnApplicationPause(bool pause){if(pause)CancelStop();}
```

```csharp
void OnApplicationFocus(bool focus){if(!focus)CancelStop();}
```

```csharp
void OnDisable(){CancelStop();foreach(var d in drops)if(d.view!=null)d.view.gameObject.SetActive(false);ActiveParticles=0;if(boardFlash!=null)boardFlash.color=Color.clear;flashLeft=0;}
```

```csharp
void OnDestroy(){CancelStop();}
```

## 源字段与依赖

```csharp
readonly Drop[] drops=new Drop[128];
```
```csharp
readonly System.Random random=new System.Random(79012);
```
```csharp
static Sprite[] sprites;
```
```csharp
GameSession owner;
```
```csharp
CameraJuice cameraJuice;
```
```csharp
int rollingBreaks;
```
```csharp
float rollingUntil;
```
```csharp
int cursor,frameBreaks;
```
```csharp
float pendingStop,pendingAt,pendingShake,pendingLife,nextStop,nextSlow,windowStart,windowUsed,stopLeft,stopStarted,savedScale=1;
```
```csharp
bool ownsClock;
```
```csharp
bool ownsSlow;
```
```csharp
float slowLeft,slowSavedScale=1;
```
```csharp
const float SpecialSlowScale=.55f;
```
```csharp
SpriteRenderer boardFlash;
```
```csharp
float flashLeft;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
