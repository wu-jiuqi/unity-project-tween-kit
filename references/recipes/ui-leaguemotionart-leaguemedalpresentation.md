# LeagueMedalPresentation · LeagueMotionArt

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/LeagueMotionArt.cs`
文件 SHA-256：`1593e0ced02d2c5e6ce513229d110104ac691a59d1016ccbbcd695056b2c68e2`
[原始源码快照](../source/Assets/Scripts/UI/LeagueMotionArt.cs.txt)

用在哪里：AppUI.TickLeagueFlow → LeagueMedalPresentation.Show(results, before, after, medals)
触发操作：胜利结算奖励区的奖章和名次变化
导入组件：`ProjectTweenKit.LeagueMedalPresentation`；展馆 ID：`medal`。
复用形式：宿主适配配方。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L23

方法 SHA-256：`932e156694a0c94b8e66ee7e10288e3570c810f079650bca9bf24b2a17d95d67`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.LeagueMedalPresentation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){age+=Mathf.Min(Time.unscaledDeltaTime,.05f);group.alpha=Mathf.Clamp01(age/.15f);if(compact){float p=Mathf.Clamp01(age/.55f);medal.anchoredPosition=new Vector2(-230,moving?Mathf.Sin(p*Mathf.PI)*28:0);medal.localScale=Vector3.one*(moving?Mathf.Lerp(1.2f,1,p):1);return;}float aspect=Mathf.Abs(transform.lossyScale.x/Mathf.Max(.001f,transform.lossyScale.y));if(arrow!=null)arrow.localScale=new Vector3(1,aspect,1);if(!moving){medal.localScale=new Vector3(1,aspect,1);return;}
  float enter=Mathf.Clamp01(age/.55f);medal.anchoredPosition=new Vector2(Mathf.Lerp(0,130,enter*enter),-35+Mathf.Sin(enter*Mathf.PI)*100);float size=Mathf.Lerp(1.8f,1,enter);medal.localScale=new Vector3(size,size*aspect,1);
  if(arrow!=null)arrow.anchoredPosition=new Vector2(310,-110+(delta>0?1:-1)*Mathf.Sin(Mathf.Clamp01((age-.3f)/.65f)*Mathf.PI)*45);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
RectTransform medal,arrow;
```
```csharp
CanvasGroup group;
```
```csharp
float age;
```
```csharp
bool moving,compact;
```
```csharp
int delta;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
