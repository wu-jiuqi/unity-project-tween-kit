# CombatFeedback79 · CombatFeedback79

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/CombatFeedback79.cs`
文件 SHA-256：`4338ba34f391dbf757988c5e51c08511df54e4ac548d7418993302d313663a1c`
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

### Initialize · L36

方法 SHA-256：`9820fafb1621e28e7870e6684e2f7689c83e5c5a30878bd4e8e2b439579bb886`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79.Initialize(BounceGarden.GameSession)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/GameSession.cs:164` · `GameSession.BuildSession` → `CombatFeel.Initialize(this)`

```csharp
public void Initialize(GameSession game) {
  owner=game;cameraJuice=Camera.main.GetComponent<CameraJuice>();
  var board=owner.Board;boardFlash=WorldArt.Draw(transform,"Soft board flash","brick",board.Origin+(board.U*board.Columns+board.V*board.Rows)*.5f,new Vector2(board.Columns*board.CellWidth,board.Rows*board.CellHeight),new Color(1,1,1,0),76);
  var white=new MaterialPropertyBlock();white.SetFloat("_Flash",1);boardFlash.SetPropertyBlock(white);
  for(int i=0;i<drops.Length;i++){var go=new GameObject("Pooled paint spray");go.transform.SetParent(transform,false);var v=go.AddComponent<SpriteRenderer>();v.sprite=Frame(i%4);v.sharedMaterial=WorldArt.BoardMaterial;v.sortingOrder=78;go.SetActive(false);drops[i].view=v;}
 }
```

### Cue · L42

方法 SHA-256：`110b20b63d5248718dd150f1d1125b487d896b539160856ca835cd25b39df821`
时间来源：Time.unscaledTime
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.CombatFeedback79.Cue(UnityEngine.Vector2, string, UnityEngine.Color, UnityEngine.Vector2)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/GameSession.cs:364` · `GameSession.EmitFeedback` → `CombatFeel.Cue(position,cue,color,feedbackIncoming)`

```csharp
public void Cue(Vector2 position,string cue,Color color,Vector2 incoming) {
  if(!FeedbackProfile.Active.enabledFeedback||owner==null||owner.IsPaused)return;
  if(cue=="hit"){Hits++;return;}
  if(cue!="break")return;
  Breaks++;frameBreaks++;float cell=owner.Board.CellWidth;
  Add(position,position,Color.white,cell*.72f,.045f,0,0,true);
  var profile=FeedbackProfile.Active;float min=Mathf.Max(0,profile.paintMinRadius),max=Mathf.Max(min,profile.paintMaxRadius);
  LastMinRadiusCells=float.MaxValue;LastMaxRadiusCells=0;
  float lobe=Mathf.Atan2(incoming.y,incoming.x)+R(-1.2f,1.2f);
  for(int i=0;i<12;i++){
   float angle=i<8?lobe+R(-1.05f,1.05f):R(0,Mathf.PI*2);
   float radius=Mathf.Sqrt(R(min*min,max*max));Vector2 target=position+new Vector2(Mathf.Cos(angle),Mathf.Sin(angle))*cell*radius;
   if(Add(position,target,color,cell*(i<4?R(.75f,1.15f):R(.25f,.55f)),profile.paintLife+R(-.10f,.12f),R(.08f,.16f),4+random.Next(4),false)){LastMinRadiusCells=Mathf.Min(LastMinRadiusCells,radius);LastMaxRadiusCells=Mathf.Max(LastMaxRadiusCells,radius);}
  }
  float now=Time.unscaledTime;if(now>rollingUntil){rollingBreaks=0;rollingUntil=now+.22f;}
  if(++rollingBreaks>=4){VisualImpact();rollingBreaks=0;rollingUntil=now+.22f;}
 }
```

### RequestStop · L64

方法 SHA-256：`fc3f55d17d2cfa6e6db78f17cf253e5be2da7b1e383eebecafcb215ac6ef0adb`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79.RequestStop(float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
void RequestStop(float duration){pendingStop=Mathf.Max(pendingStop,duration);pendingAt=Time.unscaledTime;}
```

### Update · L77

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

### AdvanceParticles · L81

方法 SHA-256：`eaef8c5bb6edb59f9565f99898036b8ac5a2a23b60f496452e3171c48a6b2dce`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.CombatFeedback79.AdvanceParticles(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/CombatFeedback79QA.cs:42` · `CombatFeedback79QA.Start` → `g.CombatFeel.AdvanceParticles(.02f)`
- `Assets/Scripts/CombatFeedback79QA.cs:44` · `CombatFeedback79QA.Start` → `g.CombatFeel.AdvanceParticles(.04f)`
- `Assets/Scripts/CombatFeedback79QA.cs:46` · `CombatFeedback79QA.Start` → `g.CombatFeel.AdvanceParticles(.02f)`
- `Assets/Scripts/CombatFeedback79QA.cs:51` · `CombatFeedback79QA.Start` → `g.CombatFeel.AdvanceParticles(.02f)`
- `Assets/Scripts/CombatFeedback79QA.cs:53` · `CombatFeedback79QA.Start` → `g.CombatFeel.AdvanceParticles(.02f)`
- `Assets/Scripts/CombatFeedback79QA.cs:55` · `CombatFeedback79QA.Start` → `g.CombatFeel.AdvanceParticles(.2f)`
- `Assets/Scripts/World/CombatFeedback79.cs:79` · `CombatFeedback79.Update` → `AdvanceParticles(owner.PresentationDelta(Time.deltaTime))`

```csharp
public void AdvanceParticles(float dt) {
  if(dt<=0||owner==null||owner.IsPaused)return;
  flashLeft=Mathf.Max(0,flashLeft-dt);boardFlash.color=new Color(1,1,1,.08f*flashLeft/.065f);
  for(int i=0;i<drops.Length;i++){ref var d=ref drops[i];if(!d.view.gameObject.activeSelf)continue;float before=d.age;d.age+=dt;float t=d.age/d.life;
   if(t>=1){d.view.gameObject.SetActive(false);ActiveParticles--;continue;}
   if(d.flash){d.view.color=new Color(1,1,1,(1-t)*.65f);Size(ref d,1+t*.25f);continue;}
   if(d.age<d.travel){float flight=1-Mathf.Pow(1-d.age/d.travel,2);d.view.transform.position=Vector2.Lerp(d.origin,d.target,flight);d.view.color=new Color(d.color.r,d.color.g,d.color.b,.9f);Size(ref d,Mathf.Lerp(.45f,.75f,flight),1.8f);}
   else {
    if(before<d.travel){d.view.sprite=Frame(d.sprite);d.view.sortingOrder=38;d.view.transform.position=d.target;d.view.transform.rotation=Quaternion.Euler(0,0,d.rotation);LandedParticles++;}
    float settle=Mathf.Clamp01((d.age-d.travel)/.10f);Size(ref d,1+.12f*Mathf.Sin(settle*Mathf.PI));float fade=Mathf.Clamp01((d.life-d.age)/.40f);d.view.color=new Color(d.color.r,d.color.g,d.color.b,.90f*fade);
   }
  }
 }
```

### LateUpdate · L94

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

### ReleaseClock · L113

方法 SHA-256：`ff41c390cdd5b2bfa2b7eb70b2936fc77e5176b7168535c7649c32e041d4120b`
时间来源：Time.timeScale, Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79.ReleaseClock()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/CombatFeedback79.cs:79` · `CombatFeedback79.Update` → `ReleaseClock()`
- `Assets/Scripts/World/CombatFeedback79.cs:115` · `CombatFeedback79.CancelStop` → `ReleaseClock()`

```csharp
void ReleaseClock(){if(ownsClock){windowUsed+=Mathf.Max(0,Time.unscaledTime-stopStarted-LastStop);nextStop=Mathf.Max(nextStop,Time.unscaledTime+.5f);if(Mathf.Approximately(Time.timeScale,0))Time.timeScale=savedScale;}ownsClock=false;stopLeft=0;}
```

### ReleaseSlow · L114

方法 SHA-256：`991b147000068b1d9d8d88b3c7c030240062ef86b266cf4f2038ec8032a1393c`
时间来源：Time.timeScale
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79.ReleaseSlow()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/CombatFeedback79.cs:100` · `CombatFeedback79.LateUpdate` → `ReleaseSlow()`
- `Assets/Scripts/World/CombatFeedback79.cs:103` · `CombatFeedback79.LateUpdate` → `ReleaseSlow()`
- `Assets/Scripts/World/CombatFeedback79.cs:108` · `CombatFeedback79.LateUpdate` → `ReleaseSlow()`
- `Assets/Scripts/World/CombatFeedback79.cs:115` · `CombatFeedback79.CancelStop` → `ReleaseSlow()`

```csharp
void ReleaseSlow(){if(ownsSlow&&Mathf.Approximately(Time.timeScale,Mathf.Min(slowSavedScale,SpecialSlowScale)))Time.timeScale=slowSavedScale;ownsSlow=false;slowLeft=0;}
```

## 对象层级和素材入口

- L40：`go.transform.SetParent(transform,false)`

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
void OnDisable(){CancelStop();}
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
