# VictoryPresentation · VictoryPresentation

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/VictoryPresentation.cs`
文件 SHA-256：`e7e7d1d0538ac616024e92c2126c8db70a044aa86b6ba96c7a3643cef86598fa`
[原始源码快照](../source/Assets/Scripts/UI/VictoryPresentation.cs.txt)

用在哪里：AppUI.ShowResults(win) → VictoryPresentation.Begin → Render / RenderHeaderStars
触发操作：结算页面猫、光环、奖励卡、按钮与星级；结果已由宿主计算
导入组件：`ProjectTweenKit.VictoryPresentation`；展馆 ID：`victory`。
复用形式：组合预制体。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L45

方法 SHA-256：`f96603d793bea00507ca6cec460507cb57f47fd289aaa6f11883c371d3d922ac`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryPresentation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(!running)return;clock+=Time.unscaledDeltaTime;if(!FeedbackProfile.Active.enabledFeedback)clock=Mathf.Max(clock,IntroEnd);Render();}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){running=false;Ready=false;if(actionAlpha!=null)actionAlpha.interactable=actionAlpha.blocksRaycasts=false;}
```

## 源字段与依赖

```csharp
public const float ActionsAt=1.85f,ActionsDuration=.30f,IntroEnd=2.34f;
```
```csharp
const float RewardsAt=1.45f;
```
```csharp
readonly Sprite[] haloFrames=new Sprite[96],burstFrames=new Sprite[16];
```
```csharp
RectTransform rewards,actions;
```
```csharp
Image halo,burst,cat;
```
```csharp
CanvasGroup rewardAlpha,actionAlpha,surfaceAlpha;
```
```csharp
Func<RectTransform> createActions;
```
```csharp
float clock;
```
```csharp
bool initialized,running;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
