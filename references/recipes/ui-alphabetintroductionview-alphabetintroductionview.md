# AlphabetIntroductionView · AlphabetIntroductionView

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AlphabetIntroductionView.cs`
文件 SHA-256：`e36078244cd9165da6ef2e3d509b16fbb45abf267c0084793edf44ae1fe47b65`
[原始源码快照](../source/Assets/Scripts/UI/AlphabetIntroductionView.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Begin · L20

方法 SHA-256：`27c5aaf984e979060d0229573a0ab957d9175d25757fe34c1cc5830a1e720f26`
时间来源：SetUpdate(true), Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetIntroductionView.Begin(BounceGarden.AppUI, BounceGarden.AlphabetIntro, System.Action)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/OpeningIntroduction.cs:30` · `AppUI.ShowOpeningIntroduction` → `alphabetIntroduction.Begin(this,OpeningIntroduction.Page(index),()=>{Profile.MarkOpeningIntro(index);complete=true;})`

```csharp
public void Begin(AppUI app,AlphabetIntro intro,Action done){
  owner=app;page=intro;completed=done;started=Time.unscaledTime;
  var root=(RectTransform)transform;root.anchorMin=Vector2.zero;root.anchorMax=Vector2.one;root.sizeDelta=Vector2.zero;root.anchoredPosition=Vector2.zero;
  var mask=gameObject.AddComponent<Image>();mask.color=new Color(.025f,.02f,.015f,OpeningIntroduction.IsPage(page.id)?.88f:.87f);mask.raycastTarget=true;
  var tap=gameObject.AddComponent<Button>();tap.transition=Selectable.Transition.None;tap.targetGraphic=mask;if(!OpeningIntroduction.IsPage(page.id))tap.onClick.AddListener(Advance);
  group=gameObject.AddComponent<CanvasGroup>();group.alpha=0;
  content=UICraft.Root(transform,"Independent animated elements",0,0,1080,OpeningIntroduction.IsPage(page.id)?1920:2340);
  entrance=DOTween.Sequence().SetUpdate(true).SetId(this);
  entrance.Append(DOTween.To(()=>group.alpha,x=>group.alpha=x,1,.24f));AnimatedElementCount++;
  if(!OpeningIntroduction.IsPage(page.id))throw new InvalidOperationException("Unsupported current introduction: "+page.id);BuildGardenOpening();
 }
```

### Advance · L46

方法 SHA-256：`2fa51c7a2a47c8a4ec9f717f8a104194b0b058dc99783c46646485c9c284ba5d`
时间来源：SetUpdate(true), Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetIntroductionView.Advance()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AlphabetIntroductionView.cs:47` · `AlphabetIntroductionView.AdvanceForQA` → `Advance()`

```csharp
public void Advance(){if(closing||(!OpeningIntroduction.IsPage(page.id)&&Time.unscaledTime-started<.25f))return;if(!ready){entrance.Complete(true);ready=true;if(!OpeningIntroduction.IsPage(page.id))return;}closing=true;StopMotion();group.blocksRaycasts=true;group.DOFade(0,.18f).SetUpdate(true).OnComplete(()=>{var next=completed;completed=null;gameObject.SetActive(false);Destroy(gameObject);next?.Invoke();});}
```

### AdvanceForQA · L47

方法 SHA-256：`ae77177ed788836482502430b0970c7592bfc6acb6f0a55be8e19b3192ee6a56`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetIntroductionView.AdvanceForQA()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.Alphabet.cs:12` · `AppUI.QAAdvanceAlphabetIntro` → `alphabetIntroduction.AdvanceForQA()`

```csharp
public void AdvanceForQA(){if(entrance!=null&&!ready)entrance.Complete(true);ready=true;started=Time.unscaledTime-1;Advance();}
```

### Cancel · L49

方法 SHA-256：`af9503fd02d3f599e99bcccbd93f946bfac2db897042bb76a11f22fe95e07417`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetIntroductionView.Cancel()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.Alphabet.cs:11` · `AppUI.CloseAlphabetIntroduction` → `view.Cancel()`

```csharp
public void Cancel(){completed=null;StopMotion();DOTween.Kill(group);gameObject.SetActive(false);Destroy(gameObject);}
```

### OnDestroy · L50

方法 SHA-256：`d65a2da3a384a54dcb44dde116e047898f693e2c927369fb45c244f07efdb209`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetIntroductionView.OnDestroy()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void OnDestroy(){StopMotion();if(group!=null)DOTween.Kill(group);}
```

## 对象层级和素材入口

- L26：`UICraft.Root(transform,"Independent animated elements",0,0,1080,OpeningIntroduction.IsPage(page.id)?1920:2340)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void StopMotion(){entrance?.Kill(false);foreach(var t in loops)t?.Kill(false);loops.Clear();}
```

```csharp
public void Cancel(){completed=null;StopMotion();DOTween.Kill(group);gameObject.SetActive(false);Destroy(gameObject);}
```

```csharp
void OnDestroy(){StopMotion();if(group!=null)DOTween.Kill(group);}
```

## 源字段与依赖

```csharp
AppUI owner;
```
```csharp
AlphabetIntro page;
```
```csharp
Action completed;
```
```csharp
RectTransform content;
```
```csharp
CanvasGroup group;
```
```csharp
Sequence entrance;
```
```csharp
readonly List<Tween> loops=new List<Tween>();
```
```csharp
bool ready,closing;
```
```csharp
float started;
```
```csharp
Vector2Int layoutSize;
```
```csharp
Rect lastSafe;
```
```csharp
int layoutRevision=-1;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
