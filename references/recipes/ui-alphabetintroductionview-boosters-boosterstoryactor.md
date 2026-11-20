# BoosterStoryActor · AlphabetIntroductionView.Boosters

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AlphabetIntroductionView.Boosters.cs`
文件 SHA-256：`9af3bf81283fd6419bc588618bedf73c21328e3bfc467cd437857026883a4b29`
[原始源码快照](../source/Assets/Scripts/UI/AlphabetIntroductionView.Boosters.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L18

方法 SHA-256：`712632bf3ce8308e8b006bd56a04f3b4563a456fbc9c1da112767e09df1c7747`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.BoosterStoryActor.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){age+=Time.unscaledDeltaTime;if(step==0)image.rectTransform.localScale=Vector3.one*(1+Mathf.Sin(age*3)*.035f);else if(step<3){image.sprite=StreakBallArt.Frame(kind,step==2?8+(int)(age*12)%4:4+(int)(age*12)%4);image.rectTransform.anchoredPosition=new Vector2(Mathf.Sin(age*2)*70,0);}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
StreakBooster kind;
```
```csharp
int step;
```
```csharp
Image image;
```
```csharp
float age;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
