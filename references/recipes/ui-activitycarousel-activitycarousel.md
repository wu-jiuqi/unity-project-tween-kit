# ActivityCarousel · ActivityCarousel

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/ActivityCarousel.cs`
文件 SHA-256：`d0710db9d415183f4575d38437e99215d90d3d3c966702b95b4bdc8b6bbaef97`
[原始源码快照](../source/Assets/Scripts/UI/ActivityCarousel.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Settle · L51

方法 SHA-256：`7ad634567d478700f5df31972fd5d80bc7ddd139c09a92904857c0c0b1ad2d1e`
时间来源：SetUpdate(true)
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.ActivityCarousel.Settle(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/ActivityCarousel.cs:47` · `ActivityCarousel.OnEndDrag` → `Settle(Offset>=MaxOffset*.5f?1:0)`
- `Assets/Scripts/UI/ActivityCarousel.cs:57` · `ActivityCarousel.QADrag` → `Settle(Offset>=MaxOffset*.5f?1:0)`

```csharp
void Settle(int page){page=0;SelectedPage=page;SetDots();snap?.Kill();snap=DOTween.To(()=>Offset,SetOffset,page*MaxOffset,.25f).SetEase(Ease.OutCubic).SetUpdate(true).OnComplete(()=>{foreach(var m in motions)m.Celebrate(.65f);});}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){Hover(-1);pressed=-1;snap?.Kill();if(content!=null)SetOffset(SelectedPage*MaxOffset);}
```

```csharp
void OnApplicationFocus(bool value){focused=value;if(!value){Hover(-1);pressed=-1;snap?.Kill();if(content!=null)SetOffset(SelectedPage*MaxOffset);foreach(var m in motions)if(m!=null)m.ResetPose();}}
```

```csharp
void OnApplicationPause(bool paused){OnApplicationFocus(!paused);}
```

## 源字段与依赖

```csharp
const float CardWidth=440,Gap=32,ViewWidth=1000,MaxOffset=0;
```
```csharp
const int VisibleCards=2;
```
```csharp
RectTransform content,viewport;
```
```csharp
Image[] dots;
```
```csharp
Tween snap;
```
```csharp
Action<string> notify,route;
```
```csharp
Vector2 dragStart;
```
```csharp
float offsetStart;
```
```csharp
bool horizontal,dragged;
```
```csharp
ActivityJellyMotion[] motions=new ActivityJellyMotion[VisibleCards];
```
```csharp
int hover=-1,pressed=-1;
```
```csharp
bool focused=true;
```
```csharp
AppUI app;
```
```csharp
static readonly string[] Titles={"花园联赛","喵喵小铺","每日挑战"};
```
```csharp
static readonly string[] Status={"排行榜","逛逛小铺","第20关解锁"};
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
