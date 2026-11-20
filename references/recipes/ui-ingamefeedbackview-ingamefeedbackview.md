# InGameFeedbackView · InGameFeedbackView

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/InGameFeedbackView.cs`
文件 SHA-256：`48566b028a2c6e9acf0b086315d96ad25416aa07fa89a52ac855629d4977e0b5`
[原始源码快照](../source/Assets/Scripts/UI/InGameFeedbackView.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### LateUpdate · L49

方法 SHA-256：`41e1081b10b7a5643c34e2bc62c2dfe67b1ac9e1a7690024e26e3c2126f90bfd`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.InGameFeedbackView.LateUpdate()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void LateUpdate(){
  if(owner==null||owner.Generation!=generation||!owner.IsPlaying){StopPresentation();return;}
  if(stopped||owner.IsPaused)return;
  float dt=Time.deltaTime;state.Advance(dt);
  if(state.TryCheer(out int tier))ShowCheer(tier);
  if(state.CheerActive)AnimateCheer();else{VisibleTier=-1;cheerRoot.gameObject.SetActive(false);}
  if(bulletVisible){bulletAge+=dt;bullet.anchoredPosition=new Vector2(Mathf.Lerp(lane.rect.width*.5f,-lane.rect.width*.5f-bulletWidth,Mathf.Clamp01(bulletAge/6f)),0);bulletGroup.alpha=state.CheerActive?.25f:1f;if(bulletAge>=6f){bulletVisible=false;bullet.gameObject.SetActive(false);}}
  if(!bulletVisible&&state.TryBullet(owner.ActiveBalls>0,18f+(float)random.NextDouble()*10f))ShowBullet();
 }
```

### AnimateCheer · L67

方法 SHA-256：`d3b1093dd43ac51cb9891e43e65a98c1110911a1f937e34e36f658a1bfe059a4`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.InGameFeedbackView.AnimateCheer()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/InGameFeedbackView.cs:54` · `InGameFeedbackView.LateUpdate` → `AnimateCheer()`

```csharp
void AnimateCheer(){
  float t=state.CheerAge/1.5f,scale=1f;if(t<.15f){float q=t/.15f;scale=Mathf.Lerp(.35f,1.12f,q);}else if(t<.28f){float q=(t-.15f)/.13f;scale=Mathf.Lerp(1.12f,.96f,q);}else if(t<.4f){float q=(t-.28f)/.12f;scale=Mathf.Lerp(.96f,1f,q);}
  float fade=Mathf.Clamp01((1-t)/.28f);cheerGroup.alpha=Mathf.Min(Mathf.Clamp01(t/.12f),fade);cheerImage.rectTransform.localScale=new Vector3(scale,scale,1);cheerRoot.anchoredPosition=new Vector2(0,-340+Mathf.Max(0,t-.72f)/.28f*48);
  for(int i=0;i<sparks.Length;i++){float angle=i*2.399f;float distance=(150+VisibleTier*28)*Mathf.Clamp01(t/.85f);sparks[i].anchoredPosition=new Vector2(Mathf.Cos(angle)*(cheerImage.rectTransform.rect.width*.43f+distance*.28f),Mathf.Sin(angle)*(cheerImage.rectTransform.rect.height*.40f+distance*.18f));sparks[i].localScale=Vector3.one*Mathf.Lerp(1,.35f,t);sparkImages[i].color=new Color(1,1,1,Mathf.Clamp01((1-t)/.5f));}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public void StopPresentation(){if(stopped)return;stopped=true;bulletVisible=false;VisibleTier=-1;state=new InGameFeedbackState();if(bullet!=null)bullet.gameObject.SetActive(false);if(cheerRoot!=null)cheerRoot.gameObject.SetActive(false);}
```

```csharp
void OnDisable(){StopPresentation();}
```

```csharp
void OnDestroy(){if(owner!=null)owner.Feedback-=OnFeedback;}
```

## 源字段与依赖

```csharp
// Icons are separate sprites, avoiding missing OS emoji glyphs.
 public static readonly string[] Messages={
  "有{0}位小伙伴和你一起闯关", "猫咪都看入迷啦！", "这个角度，能一直弹下去吧！", "果冻哗啦啦，太解压啦！",
  "弹一弹，快乐一点点", "多弹几下，多点快乐！", "给这次反弹鼓个掌！", "再弹一下，还有小惊喜！",
  "小毛线球，活力满满！", "放松一下，享受这一刻", "猫咪为你留了一朵小花", "忙里偷闲，刚刚好",
  "把小烦恼统统弹走！", "这一局，有猫咪陪着你"};
```
```csharp
static TMP_FontAsset font;
```
```csharp
static Sprite plate;
```
```csharp
bool cheersEnabled=true;
```
```csharp
GameSession owner;
```
```csharp
int generation,messageIndex;
```
```csharp
System.Random random;
```
```csharp
InGameFeedbackState state=new InGameFeedbackState();
```
```csharp
RectTransform lane,bullet,cheerRoot;
```
```csharp
CanvasGroup bulletGroup,cheerGroup;
```
```csharp
Image icon,cheerImage;
```
```csharp
TMP_Text caption;
```
```csharp
readonly RectTransform[] sparks=new RectTransform[13];
```
```csharp
readonly Image[] sparkImages=new Image[13];
```
```csharp
float bulletAge,bulletWidth;
```
```csharp
bool bulletVisible,stopped;
```
```csharp
string audience;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
