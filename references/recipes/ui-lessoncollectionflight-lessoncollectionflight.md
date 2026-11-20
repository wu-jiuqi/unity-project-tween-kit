# LessonCollectionFlight · LessonCollectionFlight

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/LessonCollectionFlight.cs`
文件 SHA-256：`b9a2d17e70f581fb9692b6a977721253c5212285e07ba1db4bc4f0e8bcffc30a`
[原始源码快照](../source/Assets/Scripts/UI/LessonCollectionFlight.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L16

方法 SHA-256：`7220882554c615c75686dd3ca1babfc197aeebf0af4835723b28890cb73c74fd`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.LessonCollectionFlight.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){age=Mathf.Repeat(age+Time.unscaledDeltaTime*(7f/2.2f),7f);float t=age%7,start=page==511?1.65f:page==521?5.15f:page==531?5.2f:page==518?5.8f:page==506?4.25f:page==505?2.25f:1.8f;
  if(page==511&&t>=3.5f)start=3.65f;
  float u=Mathf.Clamp01((t-start)/.65f);bool flying=t>=start&&t<start+.65f;token.enabled=flying;
  token.rectTransform.anchoredPosition=Vector2.Lerp(new Vector2(0,0),new Vector2(0,330),u)+Vector2.right*(Mathf.Sin(u*Mathf.PI)*70);token.rectTransform.localScale=Vector3.one*Mathf.Lerp(1,.55f,u);
  int initial=page==511?8:page==521?10:6;int collected=t>=start+.65f?(page==521?5:page==511&&t>3.5f?3:1):page==511&&t>3.5f?1:0;count.text=(initial-collected).ToString();
  if(page==521)for(int i=0;i<5;i++){ingots[i].enabled=t>=4.5f&&t<5.8f;ingots[i].rectTransform.anchoredPosition=new Vector2((i-2)*90,-160+Mathf.Sin(Mathf.Clamp01((t-4.5f)/.5f)*Mathf.PI)*45);}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
int page;
```
```csharp
float age;
```
```csharp
Image token;
```
```csharp
TMP_Text count;
```
```csharp
readonly Image[] ingots=new Image[5];
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
