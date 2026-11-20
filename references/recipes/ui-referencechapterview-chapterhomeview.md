# ChapterHomeView · ReferenceChapterView

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/ReferenceChapterView.cs`
文件 SHA-256：`2c45047e7db37e1de26351ab6a2f79f94f3f8dd9ad9418df5911d52c503da51d`
[原始源码快照](../source/Assets/Scripts/UI/ReferenceChapterView.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Transition · L56

方法 SHA-256：`c74fb4fe14e4921466a6a5610037f345a70b4f35482efe3090aa9849dff05cd5`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChapterHomeView.Transition(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/ReferenceChapterView.cs:48` · `ChapterHomeView.Build` → `Transition(previousTheme)`

```csharp
IEnumerator Transition(string oldTheme){TransitionActive=true;oldBackground=CreateBackground(transform,oldTheme,"Theme dissolve background only");oldBackground.SetSiblingIndex(background.GetSiblingIndex()+1);var group=oldBackground.gameObject.AddComponent<CanvasGroup>();group.blocksRaycasts=false;float time=0;while(time<.8f){time+=Time.unscaledDeltaTime;float t=Mathf.Clamp01(time/.8f);group.alpha=1-t*t*(3-2*t);yield return null;}Destroy(oldBackground.gameObject);oldBackground=null;TransitionActive=false;orb?.Pulse();}
```

## 对象层级和素材入口

- L56：`oldBackground.SetSiblingIndex(background.GetSiblingIndex()+1)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(background!=null)Destroy(background.gameObject);if(oldBackground!=null)Destroy(oldBackground.gameObject);}
```

## 源字段与依赖

```csharp
RectTransform frame,bounds,background,oldBackground,avatarWidget,footer,quote;
```
```csharp
JellyChapterOrb orb;
```
```csharp
string theme,avatarResource,frameResource;
```
```csharp
static string previousTheme;
```
```csharp
bool initialized;
```
```csharp
static readonly Dictionary<string,Sprite> compressedBackgrounds=new Dictionary<string,Sprite>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
