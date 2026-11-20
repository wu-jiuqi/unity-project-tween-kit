# JuiceGardenView · JuiceGardenView

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/JuiceGardenView.cs`
文件 SHA-256：`044b52170644783e947c6828d7920a6fc0a2fcae39343882d5334383f8442c08`
[原始源码快照](../source/Assets/Scripts/World/JuiceGardenView.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Impact · L56

方法 SHA-256：`674f26168a6f6bbc1822e0304e83d071f8903451d83a30fb44576a54bf90d175`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JuiceGardenView.Impact(UnityEngine.Vector2, int, float, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/JuiceSession.cs:49` · `GameSession.HitJuiceBrick` → `JuiceView.Impact(point,0,tier==2?1.8f:1.25f,.040f)`
- `Assets/Scripts/Core/JuiceSession.cs:53` · `GameSession.HitJuiceBrick` → `JuiceView.Impact(point,1,1.2f+charge*.3f,charge==2?.055f:.035f)`
- `Assets/Scripts/Core/JuiceSession.cs:56` · `GameSession.HitJuiceBrick` → `JuiceView.Impact(point,3,1.6f,.05f)`
- `Assets/Scripts/Core/JuiceSession.cs:62` · `GameSession.HitJuiceBrick` → `JuiceView.Impact(point,2,1.4f,.045f)`
- `Assets/Scripts/Core/JuiceSession.cs:86` · `GameSession.TryPawReturn` → `JuiceView.Impact(new Vector2(exit.x,FlightGeometry.ReturnY+.25f),4,1.8f,.045f)`
- `Assets/Scripts/Core/MainlineSession.cs:57` · `GameSession.DrainMainline` → `JuiceView.Impact(p,d.kind==BrickKind.Core?1:2,d.kind==BrickKind.Core?2.3f:1.45f,MainlineRules.Current.hitStop)`

```csharp
public void Impact(Vector2 p,int row,float size,float stop){
  Burst(p,row,size);if(!FeedbackProfile.Active.enabledFeedback)return;
  if(owner.CombatFeel!=null){owner.CombatFeel.VisualImpact();return;}
  var camera=Camera.main.GetComponent<CameraJuice>();if(camera!=null&&FeedbackProfile.Active.screenShake)camera.Impact();
  if(Time.unscaledTime>=nextStop&&!owner.IsPaused&&owner.IsPlaying){nextStop=Time.unscaledTime+.28f;stopLeft=Mathf.Min(.06f,stop);HitStops++;stopped.Clear();foreach(var pair in halos){var b=pair.Key;if(b!=null&&b.Active){stopped[b]=b.Body.linearVelocity;b.Body.simulated=false;}}}
 }
```

### Tether · L62

方法 SHA-256：`9600b879650e0fcde206489ba817b1883fcf6adaa031e6c36ede152617b3a403`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Ease.OutQuad
解析符号：`BounceGarden.JuiceGardenView.Tether(UnityEngine.Vector2, UnityEngine.Vector2, UnityEngine.Color)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Tether(Vector2 from,Vector2 to,Color color){
  var r=WorldArt.Draw(transform,"Charge energy thread","brick",Vector2.zero,Vector2.one,color,43);Connect(r,from,to,.065f);transient.Add(r.gameObject);
  var t=DOTween.To(()=>r.color.a,a=>r.color=new Color(color.r,color.g,color.b,a),0,.24f).SetEase(Ease.OutQuad).OnComplete(()=>{if(r!=null)Destroy(r.gameObject);});tweens.Add(t);
 }
```

### Callout · L66

方法 SHA-256：`5d42dff31dac8c5e2de3cb350faa8f72a5a7afc2ab373a49cb20b4ceec31e3c3`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Ease.OutBack, Ease.OutQuad
解析符号：`BounceGarden.JuiceGardenView.Callout(UnityEngine.Vector2, string, UnityEngine.Color)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/JuiceSession.cs:56` · `GameSession.HitJuiceBrick` → `JuiceView.Callout(point,"SOFT FAMILY!",JuiceGardenView.Lilac)`
- `Assets/Scripts/Core/JuiceSession.cs:79` · `GameSession.TickJuice` → `JuiceView.Callout(p,"+"+(u.order),JuiceGardenView.Coral)`
- `Assets/Scripts/Core/JuiceSession.cs:86` · `GameSession.TryPawReturn` → `JuiceView.Callout(new Vector2(0,-4.9f),"ONE MORE!",JuiceGardenView.Mint)`
- `Assets/Scripts/Core/MainlineSession.cs:24` · `GameSession.HitMainline` → `JuiceView.Callout(point,tier==2?"BLAZING!":"WARM!",JuiceGardenView.Gold)`

```csharp
public void Callout(Vector2 p,string text,Color color){
  var label=WorldArt.Text(transform,"Juice pop caption",text,p,.065f,color,61);transient.Add(label.gameObject);label.transform.localScale=Vector3.one*.65f;
  var seq=DOTween.Sequence().Append(label.transform.DOScale(1,.15f).SetEase(Ease.OutBack)).Join(label.transform.DOMoveY(p.y+.55f,.7f).SetEase(Ease.OutQuad)).Insert(.38f,DOTween.To(()=>label.color.a,a=>label.color=new Color(color.r,color.g,color.b,a),0,.32f)).OnComplete(()=>{if(label!=null)Destroy(label.gameObject);});tweens.Add(seq);
 }
```

### PawFlick · L70

方法 SHA-256：`972be644ec9f9afc02eddb2b47d55dc23519bae960999a2767d6e8083ea24c40`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Ease.OutElastic, Ease.OutQuad
解析符号：`BounceGarden.JuiceGardenView.PawFlick()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/JuiceSession.cs:86` · `GameSession.TryPawReturn` → `JuiceView.PawFlick()`

```csharp
public void PawFlick(){
  pawAge=0;if(paw==null)return;pawTween?.Kill();paw.transform.localPosition=pawRest;
  pawTween=DOTween.Sequence().Append(paw.transform.DOLocalMoveY(pawRest.y+.28f,.07f).SetEase(Ease.OutQuad)).Append(paw.transform.DOLocalMoveY(pawRest.y,.30f).SetEase(Ease.OutElastic));tweens.Add(pawTween);
 }
```

### Update · L75

方法 SHA-256：`561deea179ddc6d30887cc886669b9c7380a5b87bd5da804ed271b6b130a83f0`
时间来源：Time.deltaTime, Time.unscaledDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.JuiceGardenView.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){
  if(owner==null||owner.Generation!=generation)return;
  if(owner.IsPaused!=wasPaused){wasPaused=owner.IsPaused;foreach(var t in tweens)if(t!=null&&t.IsActive()){if(wasPaused)t.Pause();else t.Play();}}
  if(owner.IsPaused)return;
  if(Frozen){stopLeft-=Time.unscaledDeltaTime;if(stopLeft<=0)Unfreeze();}
  if(!owner.IsPlaying){if(stopped.Count>0)Unfreeze();foreach(var h in halos.Values)h.gameObject.SetActive(false);return;}
  if(Frozen)return;age+=Time.deltaTime;pawAge+=Time.deltaTime;
  for(int i=tweens.Count-1;i>=0;i--)if(tweens[i]==null||!tweens[i].IsActive())tweens.RemoveAt(i);
  transient.RemoveAll(g=>g==null);
  foreach(var e in effects){if(!e.active)continue;e.age+=Time.deltaTime;float t=e.age/e.duration;if(t>=1){e.active=false;e.sprite.gameObject.SetActive(false);continue;}e.sprite.sprite=JuiceArt.Frame(e.row>=4?"actors":"effects",e.row==4?1:e.row==5?3:e.row,Mathf.Min(5,(int)(t*6)));float scale=e.size*(.78f+.22f*Mathf.Sin(Mathf.Min(1,t*2)*Mathf.PI*.5f));e.sprite.transform.localScale=Vector3.one*scale;e.sprite.color=new Color(1,1,1,t>.82f?(1-t)/.18f:1);}
  foreach(var pair in halos){var b=pair.Key;var r=pair.Value;if(b==null)continue;int power=owner.Mechanic==JuiceMechanic.Heat?(b.HeatHits>=8?2:b.HeatHits>=4?1:0):owner.Mechanic==JuiceMechanic.Bank?b.BankCharge:0;r.gameObject.SetActive(b.Active&&power>0);if(power>0&&b.Active){r.transform.position=b.transform.position;r.transform.localScale=Vector3.one*(.55f+power*.10f+Mathf.Sin(age*12)*.035f);r.color=(owner.Mechanic==JuiceMechanic.Bank?Mint:Gold)*new Color(1,1,1,.55f);r.transform.rotation=Quaternion.Euler(0,0,age*90);}}
  foreach(var pair in bows){var b=pair.Key;var r=pair.Value;bool head=owner.IsZipperHead(b);r.gameObject.SetActive(head);if(head){r.transform.position=b.transform.position+new Vector3(0,-.12f,0);r.transform.localScale=Vector3.one*(.41f+Mathf.Sin(age*5)*.035f);r.sprite=JuiceArt.Frame("actors",3,(int)(age*5)%3);}}
  foreach(var thread in threads)thread.line.gameObject.SetActive(thread.a.Alive&&thread.b.Alive);
  if(owner.Mechanic==JuiceMechanic.Sandwich)foreach(var b in owner.juiceBricks){if(!b.Alive)continue;int frame=(b.Definition.juiceGroup%2)*3+Mathf.Clamp(b.DamageStage,0,2);b.Face.sprite=JuiceArt.Frame("actors",2,frame);b.Face.sharedMaterial=WorldArt.BoardMaterial;b.Face.color=owner.IsSoft(b)?Color.Lerp(Color.white,Gold,.35f+.15f*Mathf.Sin(age*9)):Color.white;b.Face.transform.localScale=Vector3.one*(owner.Board.CellWidth/.75f);}
  if(paw!=null){bool ready=owner.PawReady;strip.color=ready?Color.Lerp(Mint,Color.white,.2f+.2f*Mathf.Sin(age*6)):new Color(.65f,.65f,.72f,.4f);paw.sprite=JuiceArt.Frame("actors",0,pawAge<.48f?Mathf.Min(5,(int)(pawAge/.08f)):0);paw.color=ready||pawAge<.5f?Color.white:new Color(.8f,.8f,.85f,.65f);}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
float age,stopLeft,nextStop,pawAge=1;
```

```csharp
public int HitStops {get;private set;}
```

```csharp
bool wasPaused;
```

```csharp
void OnDestroy(){foreach(var t in tweens)if(t!=null&&t.IsActive())t.Kill();if(stopped.Count>0)Unfreeze();}
```

## 源字段与依赖

```csharp
public static readonly Color Mint=new Color(.49f,.94f,.81f),Gold=new Color(1,.76f,.36f),Coral=new Color(1,.57f,.58f),Lilac=new Color(.79f,.67f,1);
```
```csharp
GameSession owner;
```
```csharp
int generation,cursor;
```
```csharp
float age,stopLeft,nextStop,pawAge=1;
```
```csharp
readonly Effect[] effects=new Effect[80];
```
```csharp
readonly Dictionary<BallBody,SpriteRenderer> halos=new Dictionary<BallBody,SpriteRenderer>();
```
```csharp
readonly Dictionary<BallBody,Vector2> stopped=new Dictionary<BallBody,Vector2>();
```
```csharp
readonly Dictionary<BrickBody,SpriteRenderer> bows=new Dictionary<BrickBody,SpriteRenderer>();
```
```csharp
readonly List<Thread> threads=new List<Thread>();
```
```csharp
readonly List<Tween> tweens=new List<Tween>();
```
```csharp
readonly List<GameObject> transient=new List<GameObject>();
```
```csharp
SpriteRenderer paw,strip;
```
```csharp
Vector3 pawRest;
```
```csharp
Tween pawTween;
```
```csharp
bool wasPaused;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
