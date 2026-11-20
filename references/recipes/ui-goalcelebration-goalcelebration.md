# GoalCelebration · GoalCelebration

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/GoalCelebration.cs`
文件 SHA-256：`2c1acd79e66e66f27e51d405173f9883e4ce3d63ed356ed1f270f3ca2e93b61e`
[原始源码快照](../source/Assets/Scripts/UI/GoalCelebration.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L43

方法 SHA-256：`04b1b2471bbc4f69448b4b3f075236471a539a52579308ddf00c99e206c909a8`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GoalCelebration.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){
  if(game==null||game.Generation!=generation){completed=null;Destroy(gameObject);return;}
  if(suspended||(!focused&&!game.QAIgnoreFocusLoss))return;
  if(viewport!=app.AlphabetViewport||safe!=app.AlphabetSafeRect||layoutRevision!=WeChatLayoutMetrics.Revision||!LayoutFits)Layout();
  age+=Mathf.Min(Time.unscaledDeltaTime,.05f);Render();
  if(age<Duration||finished)return;finished=true;var next=completed;completed=null;
  gameObject.SetActive(false);Destroy(gameObject);next?.Invoke();
 }
```

### Render · L51

方法 SHA-256：`81c218306f455a4a551f5377e4ddea5d5179b4b2aa9637ec82aec5f33cfd4968`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.LerpUnclamped, Mathf.Sin, Mathf.SmoothStep
解析符号：`BounceGarden.GoalCelebration.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/GoalCelebration.cs:32` · `GoalCelebration.Begin` → `Render()`
- `Assets/Scripts/UI/GoalCelebration.cs:47` · `GoalCelebration.Update` → `Render()`

```csharp
void Render(){
  float t=Mathf.Clamp01(age/Duration),fade=1-Mathf.SmoothStep(0,1,(age-.92f)/.23f);
  group.alpha=Mathf.Min(1,age/.10f)*fade;dim.color=new Color(.035f,.025f,.02f,.36f*Mathf.Min(1,age/.1f)*fade);
  bool moving=FeedbackProfile.Active.enabledFeedback;float motion=moving?t:0;
  float pop=moving?Mathf.LerpUnclamped(.7f,1,UICraft.OutBack(Mathf.Clamp01(age/.28f))):1;
  plaque.localScale=Vector3.one*pop;star.localScale=Vector3.one*(1+(moving?.045f*Mathf.Sin(Mathf.Clamp01(age/.4f)*Mathf.PI):0));
  rays.localRotation=Quaternion.Euler(0,0,motion*12);rays.localScale=Vector3.one*(.9f+.15f*motion);
  for(int i=0;i<sprays.Length;i++)sprays[i].anchoredPosition=new Vector2((i==0?-1:1)*(345+45*motion),30+110*motion);
  for(int i=0;i<sparks.Length;i++){sparks[i].enabled=moving;float u=Mathf.Max(0,age-.1f-(i%4)*.025f);sparks[i].rectTransform.anchoredPosition=origins[i]+velocities[i]*u+Vector2.down*(440*u*u);sparks[i].rectTransform.localRotation=Quaternion.Euler(0,0,i*27+u*140);sparks[i].color=new Color(1,1,1,Mathf.Clamp01(u/.08f)*Mathf.Clamp01((1.05f-u)/.3f));}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnApplicationPause(bool paused){suspended=paused;}
```

```csharp
void OnApplicationFocus(bool focus){focused=focus;}
```

```csharp
void OnDestroy(){completed=null;}
```

## 源字段与依赖

```csharp
public const float Duration=1.15f;
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
RectTransform content,plaque,star,rays;
```
```csharp
Image dim;
```
```csharp
CanvasGroup group;
```
```csharp
readonly RectTransform[] sprays=new RectTransform[2];
```
```csharp
readonly Image[] sparks=new Image[14];
```
```csharp
readonly Vector2[] origins=new Vector2[14],velocities=new Vector2[14];
```
```csharp
bool suspended,focused=true,finished;
```
```csharp
Vector2Int viewport;
```
```csharp
Rect safe;
```
```csharp
int layoutRevision=-1;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
