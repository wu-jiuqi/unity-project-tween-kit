# GoalCelebration · GoalCelebration

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/GoalCelebration.cs`
文件 SHA-256：`ec8d22c1f342def6353f5d331e4165604d430c148097c09d610391ec82fb938f`
[原始源码快照](../source/Assets/Scripts/UI/GoalCelebration.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `GoalCelebration` / `celebration`：0.5.1表现切片；KitClock替换Unity时钟；宿主业务与屏幕布局适配见 current-051.md

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### S · L49

方法 SHA-256：`0746942b18e83cb3617e6c504776733ce83fb5b9efae0dab3de94f47b112455c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.SmoothStep
解析符号：`BounceGarden.GoalCelebration.S(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/GoalCelebration.cs:52` · `GoalCelebration.TitlePose` → `S((t-TitleTimes[i-1])/(TitleTimes[i]-TitleTimes[i-1]))`
- `Assets/Scripts/UI/GoalCelebration.cs:66` · `GoalCelebration.Render` → `S((age-.64f)/.24f)`
- `Assets/Scripts/UI/GoalCelebration.cs:70` · `GoalCelebration.Render` → `S((age-1.65f)/.15f)`
- `Assets/Scripts/UI/GoalCelebration.cs:74` · `GoalCelebration.Render` → `S((age-2.4f)/.16f)`
- `Assets/Scripts/UI/GoalCelebration.cs:75` · `GoalCelebration.Render` → `S((age-2.56f)/.14f)`
- `Assets/Scripts/UI/GoalCelebration.cs:76` · `GoalCelebration.Render` → `S((age-2.7f)/.15f)`
- `Assets/Scripts/UI/GoalCelebration.cs:78` · `GoalCelebration.Render` → `S((age-times[i-1])/(times[i]-times[i-1]))`

```csharp
static float S(float x)=>Mathf.SmoothStep(0,1,Mathf.Clamp01(x));
```

### TitlePose · L52

方法 SHA-256：`51fd364b67363315455d1bcf012fdc6529f94c16e9c8fdcd536049d148cd9052`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GoalCelebration.TitlePose(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/GoalCelebration.cs:64` · `GoalCelebration.Render` → `TitlePose(clock)`

```csharp
static Vector4 TitlePose(float t){for(int i=1;i<TitleTimes.Length;i++)if(t<=TitleTimes[i])return Vector4.Lerp(TitleKeys[i-1],TitleKeys[i],S((t-TitleTimes[i-1])/(TitleTimes[i]-TitleTimes[i-1])));return TitleKeys[TitleKeys.Length-1];}
```

### Update · L53

方法 SHA-256：`e6566ab01312b0fecb3e4c0b0943130be1878eb9556902ee9399d9547e64b350`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GoalCelebration.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){
  if(game==null||game.Generation!=generation){Cancel();return;}
  if(suspended||(!focused&&!game.QAIgnoreFocusLoss))return;
  if(viewport!=app.AlphabetViewport||safe!=app.AlphabetSafeRect||layoutRevision!=WeChatLayoutMetrics.Revision||parentScale!=transform.lossyScale||!LayoutFits)Layout();
  age+=Mathf.Min(Time.unscaledDeltaTime,.05f);Render();
  if(age<Duration||finished)return;finished=true;dim.raycastTarget=false;var next=completed;completed=null;next?.Invoke();
 }
```

### Render · L60

方法 SHA-256：`94fc89d481709268ace0759214460a76271033497632d9cc7c2e9d9a85a8f759`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.GoalCelebration.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/GoalCelebration.cs:29` · `GoalCelebration.Begin` → `Render()`
- `Assets/Scripts/UI/GoalCelebration.cs:33` · `GoalCelebration.DockTo` → `Render()`
- `Assets/Scripts/UI/GoalCelebration.cs:57` · `GoalCelebration.Update` → `Render()`

```csharp
void Render(){
  bool motion=FeedbackProfile.Active.enabledFeedback;float clock=motion?age:1;
  FrameIndex=motion?(int)(age*12)%12:0;cat.sprite=GoalCelebration047Art.Frame(FrameIndex);
  Vector4 pose;
  if(clock<.78f)pose=TitlePose(clock);else{float p=(clock-.78f)*Mathf.PI*2;pose=motion?new Vector4(1+.18f*Mathf.Sin(p),1-.16f*Mathf.Sin(p),6*Mathf.Sin(p),22*Mathf.Sin(p)):new Vector4(1,1,0,0);}
  float h=800*GoalCelebration047Art.TitleAspect*pose.y,follow=Mathf.Max(0,760+pose.w-h*.5f-600);
  float reveal=motion?S((age-.64f)/.24f):1;
  character.anchoredPosition=new Vector2(0,206-follow-60*(1-reveal));characterAlpha.alpha=reveal;flowersAlpha.alpha=reveal;
  word.sizeDelta=new Vector2(800*pose.x,h);word.anchoredPosition=new Vector2(0,-184-pose.w);word.localRotation=Quaternion.Euler(0,0,pose.z);word.GetComponent<Image>().color=new Color(1,1,1,motion?Mathf.Clamp01(age/.14f):1);
  float w=940,rotation=0,dimAlpha=1;Vector2 point=center;
  if(age>=1.65f&&age<1.8f){float p=S((age-1.65f)/.15f);point.y+=75*unit*p;w=Mathf.Lerp(940,990,p);rotation=-5*p;}
  else if(age>=1.8f&&age<2.4f){float u=(age-1.8f)/.6f,p=1-Mathf.Pow(1-u,3);point=Vector2.Lerp(center+Vector2.up*75*unit,destination-Vector2.up*80*unit,p);point.x+=90*unit*Mathf.Sin(Mathf.PI*u);w=Mathf.Lerp(990,540,p);rotation=-5*(1-u)+18*Mathf.Sin(Mathf.PI*u);dimAlpha=1-p;}
  else if(age>=2.4f){
   point=destination;dimAlpha=0;
   if(age<2.56f){float p=S((age-2.4f)/.16f);point.y+=Mathf.Lerp(-80,40,p)*unit;w=Mathf.Lerp(540,620,p);}
   else if(age<2.7f){float p=S((age-2.56f)/.14f);point.y+=Mathf.Lerp(40,-25,p)*unit;w=Mathf.Lerp(620,560,p);}
   else{float p=S((age-2.7f)/.15f);point.y+=Mathf.Lerp(-25,0,p)*unit;w=Mathf.Lerp(560,580,p);}
  }
  if(motion&&age<.6f){float[] times={0,.18f,.32f,.46f,.6f};float[] scales={.5f,1.13f,.93f,1.06f,1};float[] angles={-8,4,-2,1,0};for(int i=1;i<times.Length;i++)if(age<=times[i]){float p=S((age-times[i-1])/(times[i]-times[i-1]));w*=Mathf.Lerp(scales[i-1],scales[i],p);rotation+=Mathf.Lerp(angles[i-1],angles[i],p);break;}}
  if(!motion)rotation=0;
  // Scale compensation lives on an unrotated fitting root, so rotating its child stays circular on short screens.
  fitRoot.localScale=new Vector3(basis.x*w/1280,basis.y*w/1280,1);
  RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform,point,canvas.renderMode==RenderMode.ScreenSpaceOverlay?null:canvas.worldCamera,out var local);
  fitRoot.anchoredPosition=local;
  content.localRotation=Quaternion.Euler(0,0,motion?rotation:0);
  dim.color=new Color(.035f,.025f,.02f,.37f*dimAlpha*Mathf.Clamp01(age/.1f));
  rays.GetComponent<Image>().color=new Color(1,1,1,.65f*dimAlpha);rays.localRotation=Quaternion.Euler(0,0,motion?age*10:0);
  for(int i=0;i<3;i++){float p=motion?.7f+.55f*Mathf.Pow(Mathf.Sin(age*Mathf.PI*3+i),2):1;sparkles[i].localScale=Vector3.one*p;sparkles[i].localRotation=Quaternion.Euler(0,0,motion?8*Mathf.Sin(age*Mathf.PI*2+i):0);}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public void Cancel(){completed=null;gameObject.SetActive(false);Destroy(gameObject);}
```

```csharp
void OnApplicationPause(bool value){suspended=value;}
```

```csharp
void OnApplicationFocus(bool value){focused=value;}
```

```csharp
void OnDisable(){completed=null;}
```

```csharp
void OnDestroy(){completed=null;}
```

## 源字段与依赖

```csharp
public const float Duration=2.85f;
```
```csharp
AppUI app;
```
```csharp
GameSession game;
```
```csharp
Action completed;
```
```csharp
int generation;
```
```csharp
float age;
```
```csharp
RectTransform fitRoot,content,character,word,rays;
```
```csharp
Image dim,cat;
```
```csharp
CanvasGroup characterAlpha,flowersAlpha;
```
```csharp
readonly RectTransform[] sparkles=new RectTransform[3];
```
```csharp
Canvas canvas;
```
```csharp
Vector2Int viewport;
```
```csharp
Rect safe;
```
```csharp
Vector3 parentScale;
```
```csharp
bool suspended,focused=true,finished;
```
```csharp
int layoutRevision=-1;
```
```csharp
Vector3 basis;
```
```csharp
Vector2 center,destination;
```
```csharp
float unit;
```
```csharp
static readonly Vector4[] TitleKeys={new Vector4(.12f,.12f,-22,110),new Vector4(1.48f,1.65f,12,-50),new Vector4(1.30f,.62f,-10,32),new Vector4(.82f,1.28f,7,-20),new Vector4(1.14f,.88f,-4,12),new Vector4(1,1,0,0)};
```
```csharp
static readonly float[] TitleTimes={0,.14f,.26f,.40f,.56f,.78f};
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
