# FailurePresentation · FailurePresentation

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/FailurePresentation.cs`
文件 SHA-256：`a9434978cc159cf798eaeab25af26b506722301051e7964d5ce81cf7b821dda9`
[原始源码快照](../source/Assets/Scripts/UI/FailurePresentation.cs.txt)

用在哪里：AppUI.ShowResults(!win) → FailurePresentation.Begin → Render
触发操作：失败页的背景帧、猫、逐字标题、动作区入场
导入组件：`ProjectTweenKit.FailurePresentation`；展馆 ID：`failure`。
复用形式：组合预制体。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L63

方法 SHA-256：`a048997e3e08e5366ce279a56e40afec75e994d34ea00b08e3e6396cd4081bca`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FailurePresentation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(firstTick)firstTick=false;else clock+=Mathf.Min(Time.unscaledDeltaTime,.1f);if(!FeedbackProfile.Active.enabledFeedback)clock=Mathf.Max(clock,1.35f);Render();}
```

### Render · L64

方法 SHA-256：`d2c206aee7ddfd8217b7d3efea2257bef1a3e1847e5e44d61161b40cfb4ac158`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.FailurePresentation.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/FailurePresentation.cs:42` · `FailurePresentation.Begin` → `Render()`
- `Assets/Scripts/UI/FailurePresentation.cs:63` · `FailurePresentation.Update` → `Render()`

```csharp
void Render(){
        bool motion=FeedbackProfile.Active.enabledFeedback;
        for(int i=0;i<letters.Count;i++){
            var l=letters[i];float p=motion?Mathf.Clamp01((clock-.12f-i*.055f)/.38f):1,s=UICraft.OutBack(p);
            l.image.rectTransform.anchoredPosition=l.position+Vector2.down*((1-p)*44);
            l.image.rectTransform.localScale=new Vector3(s,Mathf.Max(0,s+Mathf.Sin(p*Mathf.PI*2)*.09f),1);
            l.image.rectTransform.localRotation=Quaternion.Euler(0,0,-(1-p)*(i%2==1?5:-5));l.image.color=new Color(.58f,.40f,.69f,p>0?1:0);
        }
        background.sprite=frames[motion?Mathf.FloorToInt(clock*24)%96:0];
        CatFrame=motion?FailureCatAnimation.FrameAt(clock):FailureCatAnimation.FrameCount-1;
        cat.sprite=FailureCatAnimation.Get(CatFrame);
        float breathing=motion&&clock>FailureCatAnimation.Duration?.003f*(1-Mathf.Cos((clock-FailureCatAnimation.Duration)*Mathf.PI)):0;
        cat.rectTransform.localScale=new Vector3(1,1+breathing,1);
        float a=Mathf.Clamp01((clock-1.05f)/.3f);actions.anchoredPosition=Vector2.down*(1-UICraft.OutBack(a))*30;
        actionAlpha.alpha=a;actionAlpha.interactable=actionAlpha.blocksRaycasts=Ready;
    }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
readonly List<Letter> letters=new List<Letter>();
```
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
