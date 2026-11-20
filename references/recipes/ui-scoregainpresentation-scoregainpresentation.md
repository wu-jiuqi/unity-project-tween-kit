# ScoreGainPresentation · ScoreGainPresentation

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/ScoreGainPresentation.cs`
文件 SHA-256：`fbbcbadb8b375b0e23631266d4aa2f05d6002bbbb9ada8fa3476a395bf227fa4`
[原始源码快照](../source/Assets/Scripts/UI/ScoreGainPresentation.cs.txt)

用在哪里：AppUI.BuildScoreHUD（非Opening/Tutorial的ChainCollection分支） → ScoreGainPresentation.Initialize → StateChanged
触发操作：短窗口合并加分，提示放大、上移与淡出
导入组件：`ProjectTweenKit.ScoreGain`；展馆 ID：`scoregain`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L8

方法 SHA-256：`25df5768bbc6a4611f335bfa1e354e4908e2b97bfd73a897396d89b95dff6cee`
时间来源：Time.deltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.ScoreGainPresentation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(game==null||game.IsPaused)return;if(pending>0){wait-=Time.deltaTime;if(wait<=0){text.text="+"+pending.ToString("N0");pending=0;age=0;}}age+=Time.deltaTime;float t=Mathf.Clamp01(age/.65f);group.alpha=(1-t)*Mathf.Clamp01(age/.05f);content.anchoredPosition=Vector2.up*(t*32);content.localScale=Vector3.one*(1+Mathf.Sin(t*Mathf.PI)*.06f);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(game!=null)game.StateChanged-=Changed;}
```

## 源字段与依赖

```csharp
GameSession game;
```
```csharp
int seen,pending;
```
```csharp
float age=2,wait;
```
```csharp
TMP_Text text;
```
```csharp
CanvasGroup group;
```
```csharp
RectTransform content;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
