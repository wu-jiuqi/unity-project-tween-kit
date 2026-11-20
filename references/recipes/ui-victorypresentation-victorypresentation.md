# VictoryPresentation · VictoryPresentation

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/VictoryPresentation.cs`
文件 SHA-256：`dd182b5626327b842d30a7563d36a082d24542e2f68ab14d600603ba11c35d64`
[原始源码快照](../source/Assets/Scripts/UI/VictoryPresentation.cs.txt)

用在哪里：AppUI.ShowResults(win) → VictoryPresentation.Begin → Render / RenderHeaderStars
触发操作：结算页面猫、光环、奖励卡、按钮与星级；结果已由宿主计算
导入组件：`ProjectTweenKit.VictoryPresentation`；展馆 ID：`victory`。
复用形式：组合预制体。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L74

方法 SHA-256：`dc3f5d99ccae29a09d7b2e14b7152be633d60452f37a9e0cf391696aee2c105e`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryPresentation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update() {
        if (!running) return;
        clock += Time.unscaledDeltaTime;
        if (!FeedbackProfile.Active.enabledFeedback) clock = Mathf.Max(clock, IntroEnd);
        Render();
    }
```

### Render · L80

方法 SHA-256：`290f161f2590298cb8789f0301892ba1a44be1924f91d4dd6c3b2fa0e690e5b6`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.VictoryPresentation.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/VictoryPresentation.cs:67` · `VictoryPresentation.Begin` → `Render()`
- `Assets/Scripts/UI/VictoryPresentation.cs:78` · `VictoryPresentation.Update` → `Render()`
- `Assets/Scripts/UI/VictoryPresentation.cs:121` · `VictoryPresentation.Finish` → `Render()`
- `Assets/Scripts/UI/VictoryPresentation.cs:122` · `VictoryPresentation.OnEnable` → `Render()`

```csharp
void Render() {
        bool motion = FeedbackProfile.Active.enabledFeedback;
        CatFrame = motion ? VictoryCatAnimation.FrameAt(clock - .08f) : VictoryCatAnimation.FrameCount - 1;
        cat.sprite = VictoryCatAnimation.Get(CatFrame);
        surfaceAlpha.alpha=Mathf.Clamp01(clock/.28f);
        RenderHeaderStars(motion);
        for (int i = 0; i < brandLetters.Count; i++) {
            float p = ((clock % 5.5f) - .7f - i * .09f) / .48f;
            float wave = motion && p > 0 && p < 1 ? Mathf.Sin(p * Mathf.PI * 8) * Mathf.Sin(p * Mathf.PI) : 0;
            brandLetters[i].anchoredPosition = brandPositions[i] + new Vector2(wave * 1.2f, wave * 3.2f);
            brandLetters[i].localRotation = Quaternion.Euler(0, 0, wave * 2.2f);
            brandLetters[i].localScale = new Vector3(1 + wave * .018f, 1 - wave * .025f, 1);
        }
        float haloAge = motion ? Mathf.Max(0, clock - .35f) * 8 : 0;
        int haloIndex = Mathf.FloorToInt(haloAge);
        halo.sprite = haloFrames[haloIndex % haloFrames.Length];
        halo.rectTransform.localRotation = Quaternion.Euler(0, 0, -(haloAge - haloIndex) * 3.75f);
        halo.color = new Color(1, 1, 1, .78f * (motion ? Mathf.Clamp01((clock - .15f) / .45f) : 1));
        float burstAge = clock - 1;
        bool bursting = motion && burstAge >= 0 && burstAge < burstFrames.Length / 12f;
        burst.enabled = bursting;
        if (bursting) burst.sprite = burstFrames[Mathf.Min(burstFrames.Length - 1, Mathf.FloorToInt(burstAge * 12))];
        subtitleAlpha.alpha = Mathf.Clamp01((clock - .6f) / .3f);
        float reward = Mathf.Clamp01((clock - RewardsAt) / .3f);
        rewards.anchoredPosition = new Vector2(0, -469 - (1 - reward) * 28);
        rewards.localScale = Vector3.one * (.96f + .04f * UICraft.OutBack(reward));
        rewardAlpha.alpha = reward;
        if (clock >= ActionsAt && actions == null) {
            actions = createActions?.Invoke();
            if (actions != null) actionAlpha = actions.gameObject.AddComponent<CanvasGroup>();
        }
        Ready = false;
        if (actions != null) {
            float p = Mathf.Clamp01((clock - ActionsAt) / ActionsDuration);
            actions.anchoredPosition = Vector2.down * ((1 - p) * 28);
            actions.localScale = Vector3.one * (.96f + .04f * UICraft.OutBack(p));
            actionAlpha.alpha = p;
            Ready = p >= .999f;
            actionAlpha.interactable = actionAlpha.blocksRaycasts = Ready;
        }
    }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable() {
        running = false; Ready = false;
        if (actionAlpha != null) actionAlpha.interactable = actionAlpha.blocksRaycasts = false;
    }
```

## 源字段与依赖

```csharp
public const float ActionsAt = 1.85f, ActionsDuration = .30f, IntroEnd = 2.34f;
```
```csharp
const float RewardsAt = 1.45f;
```
```csharp
readonly List<RectTransform> brandLetters = new List<RectTransform>();
```
```csharp
readonly List<Vector2> brandPositions = new List<Vector2>();
```
```csharp
readonly Sprite[] haloFrames = new Sprite[96], burstFrames = new Sprite[16];
```
```csharp
RectTransform rewards, actions;
```
```csharp
Image halo, burst, cat;
```
```csharp
CanvasGroup rewardAlpha, actionAlpha, subtitleAlpha;
```
```csharp
Func<RectTransform> createActions;
```
```csharp
CanvasGroup surfaceAlpha;
```
```csharp
float clock;
```
```csharp
bool initialized, running;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
