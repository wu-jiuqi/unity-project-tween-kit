# FailurePresentation · FailurePresentation

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/FailurePresentation.cs`
文件 SHA-256：`f3fa36d5a24799a4b4be72a72562e74e4961356ae92c59b7b6d42f7cd84835de`
[原始源码快照](../source/Assets/Scripts/UI/FailurePresentation.cs.txt)

用在哪里：AppUI.ShowResults(!win) → FailurePresentation.Begin → Render
触发操作：失败页的背景帧、猫、逐字标题、动作区入场
导入组件：`ProjectTweenKit.FailurePresentation`；展馆 ID：`failure`。
复用形式：组合预制体。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L29

方法 SHA-256：`4e5fc3ca51a4f141b04c12a6e1053c3b2a9c863b3953ce5203e2b6e7e01127b6`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FailurePresentation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(firstTick)firstTick=false;else clock+=Mathf.Min(Time.unscaledDeltaTime,.1f);if(!FeedbackProfile.Active.enabledFeedback)clock=Mathf.Max(clock,2.32f);Render();}
```

### Render · L30

方法 SHA-256：`f999c0a806083a1e759ebcc6326ab95771bff8da1a5933acc7a5847a41ee3e94`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos
解析符号：`BounceGarden.FailurePresentation.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/FailurePresentation.cs:27` · `FailurePresentation.Begin` → `Render()`
- `Assets/Scripts/UI/FailurePresentation.cs:29` · `FailurePresentation.Update` → `Render()`

```csharp
void Render(){
  bool motion=FeedbackProfile.Active.enabledFeedback;background.sprite=frames[motion?Mathf.FloorToInt(clock*24)%96:0];CatFrame=motion?ResultsC048Art.FrameAt(false,clock):7;cat.sprite=ResultsC048Art.Frame(false,CatFrame);
  float breath=motion&&clock>2.32f?.003f*(1-Mathf.Cos((clock-2.32f)*Mathf.PI)):0;cat.rectTransform.localScale=new Vector3(1,1+breath,1);
  float a=Mathf.Clamp01((clock-1.05f)/.3f);actions.anchoredPosition=Vector2.down*(1-UICraft.OutBack(a))*30;actionAlpha.alpha=a;actionAlpha.interactable=actionAlpha.blocksRaycasts=Ready;
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){if(actionAlpha!=null)actionAlpha.interactable=actionAlpha.blocksRaycasts=false;}
```

## 源字段与依赖

```csharp
readonly Sprite[] frames=new Sprite[96];
```
```csharp
Image background,cat;
```
```csharp
RectTransform actions;
```
```csharp
CanvasGroup actionAlpha;
```
```csharp
float clock;
```
```csharp
bool firstTick=true;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
