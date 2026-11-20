# ChainTaskCard · ChainCollectionHUD

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/ChainCollectionHUD.cs`
文件 SHA-256：`2953ce988acbd87980173e979b2d0ce9f600bd19771161e61af2df2fbbc56e40`
[原始源码快照](../source/Assets/Scripts/UI/ChainCollectionHUD.cs.txt)

用在哪里：ChainCollectionHUD.Initialize → AddCard → ChainTaskCard.Initialize / Refresh
触发操作：实际收集值改变触发目标到达挤压；完成显示印章
导入组件：`ProjectTweenKit.TaskArrival`；展馆 ID：`taskarrival`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L53

方法 SHA-256：`848a02225fc5f0beac477b38b653bfaa5dc400630ead81dacec3c074b651630f`
时间来源：Time.deltaTime
曲线证据：Mathf.Exp, Mathf.LerpUnclamped, Mathf.Sin
解析符号：`BounceGarden.ChainTaskCard.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(game==null||game.Generation!=generation||!game.IsPlaying||game.IsPaused)return;float dt=Time.deltaTime;age+=dt;pulseAge+=dt;float intro=Mathf.Min(1.025f,Mathf.LerpUnclamped(.9f,1,UICraft.OutBack(Mathf.Clamp01(age/.48f)))),pulse=Mathf.Sin(Mathf.Clamp01(pulseAge/.38f)*Mathf.PI);root.localScale=new Vector3(intro*(1+pulse*.025f),intro*(1-pulse*.025f),1);
  icon.rectTransform.localScale=new Vector3(1+pulse*.1f,1-pulse*.08f,1);icon.rectTransform.localRotation=Quaternion.identity;count.transform.localScale=Vector3.one*(1+pulse*.04f);
  bool complete=target>=Goal;stamp.gameObject.SetActive(complete);if(complete)stamp.rectTransform.localScale=Vector3.one*Mathf.LerpUnclamped(.4f,1,UICraft.OutBack(Mathf.Clamp01(pulseAge/.4f)));
  float wobble=Mathf.Sin(pulseAge*27)*Mathf.Exp(-pulseAge*8)*.11f;jellyVisual.localScale=new Vector3(1+wobble,1-wobble,1);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
GameSession game;
```
```csharp
int target,generation;
```
```csharp
float pulseAge=2,age,width;
```
```csharp
RectTransform root,content;
```
```csharp
Image icon,stamp;
```
```csharp
RectTransform jellyVisual;
```
```csharp
TMP_Text count;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
