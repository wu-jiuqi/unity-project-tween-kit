# AlphabetTweenExtensions · AlphabetTweenExtensions

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AlphabetTweenExtensions.cs`
文件 SHA-256：`415f89b076b8b9b90ae3af01e69ab62325b8ffbfa79cf5879f7a0fee2b7fef99`
[原始源码快照](../source/Assets/Scripts/UI/AlphabetTweenExtensions.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### DOAnchorPos · L7

方法 SHA-256：`4b3162c5eb3702e874fb37b6844d369cb4f21fa12c82b46e4dbf39041ea744d9`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetTweenExtensions.DOAnchorPos(UnityEngine.RectTransform, UnityEngine.Vector2, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AlphabetIntroductionView.cs:36` · `AlphabetIntroductionView.Enter` → `target.DOAnchorPos(settled,.36f)`
- `Assets/Scripts/UI/SettingsSwitch77.cs:17` · `SettingsSwitch77.Refresh` → `knob.DOAnchorPos(new Vector2(x,0),.16f)`

```csharp
public static Tweener DOAnchorPos(this RectTransform r,Vector2 end,float duration)=>DOTween.To(()=>r.anchoredPosition,v=>r.anchoredPosition=v,end,duration).SetTarget(r);
```

### DOAnchorPosY · L8

方法 SHA-256：`205faac4bb77e47cd683cb7d8863497b595a17d50029d70d4f821f8e19dd1ace`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetTweenExtensions.DOAnchorPosY(UnityEngine.RectTransform, float, float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public static Tweener DOAnchorPosY(this RectTransform r,float end,float duration)=>DOTween.To(()=>r.anchoredPosition.y,v=>r.anchoredPosition=new Vector2(r.anchoredPosition.x,v),end,duration).SetTarget(r);
```

### DOFade · L9

方法 SHA-256：`585b21b4b62d21191848d15a483737215a50ce38b2b612672a130aca71d4195b`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetTweenExtensions.DOFade(UnityEngine.UI.Graphic, float, float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public static Tweener DOFade(this Graphic graphic,float end,float duration)=>DOTween.To(()=>graphic.color.a,v=>{var c=graphic.color;c.a=v;graphic.color=c;},end,duration).SetTarget(graphic);
```

### DOFade · L10

方法 SHA-256：`1daf5009192fdacf8b026bb01cce6023773aeb7179914d5983137f5a074cd60c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetTweenExtensions.DOFade(UnityEngine.CanvasGroup, float, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AlphabetIntroductionView.cs:42` · `AlphabetIntroductionView.Advance` → `group.DOFade(0,.18f)`

```csharp
public static Tweener DOFade(this CanvasGroup group,float end,float duration)=>DOTween.To(()=>group.alpha,v=>group.alpha=v,end,duration).SetTarget(group);
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
