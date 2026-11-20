# AlphabetIntroductionView · AlphabetIntroductionView

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AlphabetIntroductionView.cs`
文件 SHA-256：`1aaa88a69395bbd7d9aed8760887abc7303e80bb10cf4cd30c4a0060a66cd321`
[原始源码快照](../source/Assets/Scripts/UI/AlphabetIntroductionView.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Begin · L20

方法 SHA-256：`8dcd4c45a26cee1100c94c36caaecd430ea8eeb53c91bfe93153c8f6bd1eba1e`
时间来源：SetUpdate(true), Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetIntroductionView.Begin(BounceGarden.AppUI, BounceGarden.AlphabetIntro, System.Action)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OnboardingQA.cs:9` · `AppUI.QAGardenLesson` → `alphabetIntroduction.Begin(this,OpeningIntroduction.Page(index),()=>alphabetIntroduction=null)`
- `Assets/Scripts/UI/AppUI.Alphabet.cs:20` · `AppUI.ShowAlphabetIntroductions` → `alphabetIntroduction.Begin(this,page,()=>{Profile.MarkAlphabetIntro(page.id);complete=true;})`
- `Assets/Scripts/UI/AppUI.Alphabet.cs:28` · `AppUI.QAShowAlphabetIntro` → `alphabetIntroduction.Begin(this,AlphabetCampaign.Intro(id),()=>{alphabetIntroduction=null;})`
- `Assets/Scripts/UI/AppUI.BoosterUnlocks.cs:12` · `AppUI.ShowBoosterUnlocks` → `alphabetIntroduction.Begin(this,BoosterIntroduction.Copy(k),()=>{Profile.MarkBoosterIntro(k);complete=true;})`
- `Assets/Scripts/UI/AppUI.Tutorial.cs:16` · `AppUI.ShowFirstBounceIntroduction` → `alphabetIntroduction.Begin(this,OpeningIntroduction.Page(0),()=>complete=true)`
- `Assets/Scripts/UI/OpeningIntroduction.cs:30` · `AppUI.ShowOpeningIntroduction` → `alphabetIntroduction.Begin(this,OpeningIntroduction.Page(index),()=>{Profile.MarkOpeningIntro(index);complete=true;})`

```csharp
public void Begin(AppUI app,AlphabetIntro intro,Action done){
  owner=app;page=intro;completed=done;started=Time.unscaledTime;
  var root=(RectTransform)transform;root.anchorMin=Vector2.zero;root.anchorMax=Vector2.one;root.sizeDelta=Vector2.zero;root.anchoredPosition=Vector2.zero;
  var mask=gameObject.AddComponent<Image>();mask.color=new Color(.025f,.02f,.015f,OpeningIntroduction.IsPage(page.id)?.68f:.87f);mask.raycastTarget=true;
  var tap=gameObject.AddComponent<Button>();tap.transition=Selectable.Transition.None;tap.targetGraphic=mask;tap.onClick.AddListener(Advance);
  group=gameObject.AddComponent<CanvasGroup>();group.alpha=0;
  content=UICraft.Root(transform,"Independent animated elements",0,0,1080,2340);
  entrance=DOTween.Sequence().SetUpdate(true).SetId(this);
  entrance.Append(DOTween.To(()=>group.alpha,x=>group.alpha=x,1,.24f));AnimatedElementCount++;
  if(OpeningIntroduction.IsPage(page.id)){BuildGardenOpening();return;}if(page.id>=301){BuildBoosterIntroduction();return;}
  if(UsesMotion){BuildMotionIntroduction();return;}
  throw new InvalidOperationException("Retired introduction page: "+page.id);
 }
```

### Enter · L33

方法 SHA-256：`e9901480770cb84da230e171135d283b2d6418b5a6d0468dc78df54e584bec13`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Ease.OutBack, Ease.OutCubic
解析符号：`BounceGarden.AlphabetIntroductionView.Enter(UnityEngine.RectTransform, float, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AlphabetIntroductionView.Boosters.cs:10` · `AlphabetIntroductionView.BuildBoosterIntroduction` → `Enter(holder,.2f+i*.2f,.9f)`
- `Assets/Scripts/UI/AlphabetIntroductionView.Lessons.cs:13` · `AlphabetIntroductionView.BuildGardenOpening` → `Enter(demo,.04f,.98f)`
- `Assets/Scripts/UI/AlphabetIntroductionView.Motion.cs:14` · `AlphabetIntroductionView.BuildMotionIntroduction` → `Enter(holder,.2f,.96f)`

```csharp
void Enter(RectTransform target,float delay,float initialScale){
  Vector2 settled=target.anchoredPosition;target.anchoredPosition=settled+Vector2.down*28;target.localScale=Vector3.one*initialScale;
  var alpha=target.gameObject.AddComponent<CanvasGroup>();alpha.alpha=0;alpha.blocksRaycasts=false;
  entrance.Insert(delay,target.DOAnchorPos(settled,.36f).SetEase(Ease.OutCubic));entrance.Insert(delay,target.DOScale(1,.46f).SetEase(Ease.OutBack));entrance.Insert(delay,DOTween.To(()=>alpha.alpha,v=>alpha.alpha=v,1,.3f));AnimatedElementCount++;
 }
```

### Advance · L42

方法 SHA-256：`4f819f611d852cd853da395a8cd0f1e28a732717c09bfbf7c13a4fffd90769d6`
时间来源：SetUpdate(true), Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetIntroductionView.Advance()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AlphabetIntroductionView.cs:43` · `AlphabetIntroductionView.AdvanceForQA` → `Advance()`

```csharp
public void Advance(){if(closing||(!OpeningIntroduction.IsPage(page.id)&&Time.unscaledTime-started<.25f))return;if(!ready){entrance.Complete(true);ready=true;if(!OpeningIntroduction.IsPage(page.id))return;}if(AdvanceMotion())return;closing=true;StopMotion();group.blocksRaycasts=true;group.DOFade(0,.18f).SetUpdate(true).OnComplete(()=>{var next=completed;completed=null;gameObject.SetActive(false);Destroy(gameObject);next?.Invoke();});}
```

### AdvanceForQA · L43

方法 SHA-256：`bbd01f47efed4015987355834979644804365abb9e73ba1724b5a607a275ca02`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetIntroductionView.AdvanceForQA()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OnboardingQA.cs:32` · `OnboardingQA.Start` → `app.QAAlphabetIntro.AdvanceForQA()`
- `Assets/Scripts/OnboardingQA.cs:37` · `OnboardingQA.Start` → `app.QAAlphabetIntro.AdvanceForQA()`
- `Assets/Scripts/UI/AppUI.Alphabet.cs:27` · `AppUI.QAAdvanceAlphabetIntro` → `alphabetIntroduction.AdvanceForQA()`

```csharp
public void AdvanceForQA(){motionSecond=true;if(entrance!=null&&!ready)entrance.Complete(true);ready=true;started=Time.unscaledTime-1;Advance();}
```

### Cancel · L45

方法 SHA-256：`af9503fd02d3f599e99bcccbd93f946bfac2db897042bb76a11f22fe95e07417`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetIntroductionView.Cancel()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.Alphabet.cs:11` · `AppUI.CloseAlphabetIntroduction` → `view.Cancel()`

```csharp
public void Cancel(){completed=null;StopMotion();DOTween.Kill(group);gameObject.SetActive(false);Destroy(gameObject);}
```

### OnDestroy · L46

方法 SHA-256：`d65a2da3a384a54dcb44dde116e047898f693e2c927369fb45c244f07efdb209`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetIntroductionView.OnDestroy()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void OnDestroy(){StopMotion();if(group!=null)DOTween.Kill(group);}
```

## 对象层级和素材入口

- L26：`UICraft.Root(transform,"Independent animated elements",0,0,1080,2340)`

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
