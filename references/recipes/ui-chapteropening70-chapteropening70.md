# ChapterOpening70 · ChapterOpening70

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/ChapterOpening70.cs`
文件 SHA-256：`aeb8f6fb011ca27a0a7ef720ba07879469e1ea9a85ac3316b035e3592d7d8201`
[原始源码快照](../source/Assets/Scripts/UI/ChapterOpening70.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Sample · L14

方法 SHA-256：`b6d17d3c237c3a45d64b6c64cb2d1b86f06c3c9b9aeb5ff9b6797f988d2d313f`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.SmoothStep
解析符号：`BounceGarden.ChapterOpening70.Sample(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/ChapterOpening70.cs:10` · `ChapterOpening70.Build` → `Sample(0)`
- `Assets/Scripts/UI/ChapterOpening70.cs:15` · `ChapterOpening70.Update` → `Sample(age)`
- `Assets/Scripts/UI/ChapterOpening70.cs:16` · `ChapterOpening70.QAComplete` → `Sample(age)`

```csharp
public void Sample(float seconds){float enter=Mathf.SmoothStep(0,1,Mathf.Clamp01(seconds/.65f));float turn=Mathf.SmoothStep(0,1,Mathf.Clamp01((seconds-1.2f)/1.25f));float reveal=Mathf.SmoothStep(0,1,Mathf.Clamp01((seconds-2.2f)/1.55f));window.sizeDelta=Vector2.Lerp(new Vector2(840,1040),new Vector2(2400,3600),reveal);window.localScale=Vector3.one*Mathf.Lerp(.92f,1,enter);nextBackground.anchoredPosition=new Vector2(0,Mathf.Lerp(650,50,reveal));if(homeChrome!=null)homeChrome.alpha=1-enter;if(homeExtra!=null)homeExtra.alpha=1-enter;leftAlpha.alpha=rightAlpha.alpha=1-Mathf.SmoothStep(0,1,turn);left.anchoredPosition=new Vector2(-440-turn*200,-50);right.anchoredPosition=new Vector2(440+turn*200,-50);left.localScale=right.localScale=new Vector3(1-turn,1,1)*enter;veil.alpha=1-reveal*.35f;title.text=seconds<1.3f?"第"+chapter+"章完成！":ChapterSceneArt.Title(chapter)+(committed?"已开启":"即将开启");copy.text=seconds<1.3f?"收好回忆，回到花园":seconds<3.7f?"推开花园门，迎接新的旅程":"新的关卡，等你来探索";group.alpha=committed?Mathf.Clamp01(1-(seconds-4.5f)/.5f):1;}
```

### Update · L15

方法 SHA-256：`3eadb492bd207e2c0d7570c4c2d24d3fe6c1e3565d677da7592f204935e1a1d5`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChapterOpening70.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(!focused)return;age+=Time.unscaledDeltaTime;Sample(age);if(age>=1.2f&&age-Time.unscaledDeltaTime<1.2f)SoundService.Play("chapter_unlock");if(age>=4.5f&&!committed){if(open?.Invoke()==true){committed=true;SoundService.Play("chapter_unlock");}else{age=4.2f;copy.text="保存失败，正在重试";}}if(committed&&age>=5.05f){closed?.Invoke();closed=null;Destroy(gameObject);}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnApplicationFocus(bool value){if(!RuntimeQA.IsIsolated)focused=value;}
```

```csharp
void OnApplicationPause(bool value){if(!RuntimeQA.IsIsolated)focused=!value;}
```

```csharp
void OnDestroy(){if(homeChrome!=null)homeChrome.alpha=1;if(homeExtra!=null)homeExtra.alpha=1;if(closed!=null)closed();}
```

## 源字段与依赖

```csharp
RectTransform window,left,right,nextBackground;
```
```csharp
CanvasGroup veil,group,homeChrome,homeExtra,leftAlpha,rightAlpha;
```
```csharp
TMPro.TMP_Text title,copy;
```
```csharp
Func<bool> open;
```
```csharp
Action closed;
```
```csharp
float age;
```
```csharp
bool committed,focused=true;
```
```csharp
int chapter;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
