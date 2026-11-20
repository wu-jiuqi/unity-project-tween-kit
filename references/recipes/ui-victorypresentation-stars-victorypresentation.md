# VictoryPresentation · VictoryPresentation.Stars

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/VictoryPresentation.Stars.cs`
文件 SHA-256：`c21e4fc7be0638dc272b38ff8d7c5a033e93bf2baa257515c736a314b86aa0dc`
[原始源码快照](../source/Assets/Scripts/UI/VictoryPresentation.Stars.cs.txt)

用在哪里：AppUI.ShowResults(win) → VictoryPresentation.Begin → Render / RenderHeaderStars
触发操作：结算页面猫、光环、奖励卡、按钮与星级；结果已由宿主计算
导入组件：`ProjectTweenKit.VictoryPresentation`；展馆 ID：`victory`。
复用形式：组合预制体。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### RenderHeaderStars · L9

方法 SHA-256：`3565a8bd81a3a269b4318e012473c827b555f58656cf31ae4992fa5c030696e5`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Pow, Mathf.Sin, Mathf.SmoothStep
解析符号：`BounceGarden.VictoryPresentation.RenderHeaderStars(bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/VictoryPresentation.cs:85` · `VictoryPresentation.Render` → `RenderHeaderStars(motion)`
- `Assets/Scripts/UI/VictoryPresentation.Stars.cs:8` · `VictoryPresentation.ReceiveVictoryStars` → `RenderHeaderStars(true)`

```csharp
void RenderHeaderStars(bool motion){float p=motion?Mathf.Clamp01((clock-.08f)/.8f):1;float move=1-Mathf.Pow(1-p,3);for(int i=0;i<3;i++){var star=headerStars[i];if(star==null)continue;var end=new Vector2((i-1)*250,i==1?740:625);var size=Vector2.one*(i==1?207:184);star.anchoredPosition=Vector2.Lerp(headerFrom[i],end,move);star.sizeDelta=Vector2.Lerp(headerSizes[i],size,move);star.localRotation=Quaternion.Euler(0,0,Mathf.LerpAngle(headerAngles[i],(i-1)*-10,move));star.GetComponent<StarChargeView>()?.RefreshGeometry(Time.unscaledDeltaTime);float idle=Mathf.Max(0,clock-.88f);float blend=motion?Mathf.SmoothStep(0,1,Mathf.Clamp01(idle/.25f)):0;star.anchoredPosition+=JellyMotion.FloatOffset(idle,i)*blend;star.localScale=Vector3.Lerp(Vector3.one*(1+Mathf.Sin(p*Mathf.PI)*.08f),JellyMotion.FloatScale(idle,i),blend);star.localRotation*=Quaternion.Euler(0,0,JellyMotion.FloatAngle(idle,i)*blend);}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
readonly RectTransform[] headerStars=new RectTransform[3];
```
```csharp
readonly Vector2[] headerFrom=new Vector2[3],headerSizes=new Vector2[3];
```
```csharp
readonly float[] headerAngles=new float[3];
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
