# ChainRatingView · ChainRatingView

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/ChainRatingView.cs`
文件 SHA-256：`cbecd5f784954e8766c92f5ea8ec6bd1ea4a8a992296988868668092cbd1906a`
[原始源码快照](../source/Assets/Scripts/UI/ChainRatingView.cs.txt)

用在哪里：ChainCollectionHUD.Initialize → ChainRatingView.Initialize；Update观察ChainRatingSerial
触发操作：连击评级变动后字图放大与星光爆发
导入组件：`ProjectTweenKit.CheerBurst`；展馆 ID：`rating`。
复用形式：组合预制体。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L14

方法 SHA-256：`368a6018e8f5e5d817c6723c6db66c7a52854bc0fbd0fe9b731beb45b190adb0`
时间来源：Time.deltaTime
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.ChainRatingView.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){
  if(game==null||game.Generation!=generation||!game.IsPlaying){root.gameObject.SetActive(false);VisibleTier=-1;return;}if(game.IsPaused)return;
  if(seen!=game.ChainRatingSerial){seen=game.ChainRatingSerial;age=0;VisibleTier=game.ChainRatingTier;ChineseArt.SetCheer(art,VisibleTier);SoundService.Cheer(VisibleTier);art.rectTransform.sizeDelta=new Vector2(420+VisibleTier*40,160);root.gameObject.SetActive(true);root.SetAsLastSibling();}
  age+=Time.deltaTime;if(age>=1.8f){root.gameObject.SetActive(false);VisibleTier=-1;return;}float t=age/1.8f;float pop=t<.12f?Mathf.Lerp(.45f,1.12f,t/.12f):Mathf.Lerp(1.12f,1,Mathf.Clamp01((t-.12f)/.17f));art.transform.localScale=Vector3.one*pop;group.alpha=Mathf.Min(Mathf.Clamp01(t/.06f),Mathf.Clamp01((1-t)/.22f));
  for(int i=0;i<sparks.Length;i++){bool visible=i<4+VisibleTier*3;sparks[i].gameObject.SetActive(visible);if(!visible)continue;float angle=i*2.399f,distance=(110+VisibleTier*14)*Mathf.Clamp01(t/.7f);sparks[i].anchoredPosition=new Vector2(Mathf.Cos(angle)*(art.rectTransform.rect.width*.43f+distance*.35f),Mathf.Sin(angle)*(art.rectTransform.rect.height*.43f+distance*.22f));sparks[i].localScale=Vector3.one*Mathf.Lerp(1,.3f,t);sparkImages[i].color=new Color(1,1,1,Mathf.Clamp01((1-t)/.45f));}
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
int generation,seen;
```
```csharp
float age=3;
```
```csharp
RectTransform root;
```
```csharp
Image art;
```
```csharp
CanvasGroup group;
```
```csharp
readonly RectTransform[] sparks=new RectTransform[13];
```
```csharp
readonly Image[] sparkImages=new Image[13];
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
