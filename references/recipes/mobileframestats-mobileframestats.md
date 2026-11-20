# MobileFrameStats · MobileFrameStats

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/MobileFrameStats.cs`
文件 SHA-256：`5963f5c4c9442d4fbfcc7275b7eefefb418bc66f3c7706e1f73617b0bfcf1c0e`
[原始源码快照](../source/Assets/Scripts/MobileFrameStats.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L12

方法 SHA-256：`fae438fd2da812d83c78b6ab773ed829434041c8aaee9a4ea2a39685cd859292`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.MobileFrameStats.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){yield return null;game=FindFirstObjectByType<AppUI>()?.Session;var canvas=gameObject.AddComponent<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceOverlay;canvas.sortingOrder=32000;var text=new GameObject("Measured frame timing",typeof(RectTransform)).AddComponent<TextMeshProUGUI>();text.transform.SetParent(transform,false);text.font=GameTypography.Font;text.fontSize=14;text.color=new Color(.18f,.15f,.13f,1);text.raycastTarget=false;text.alignment=TextAlignmentOptions.BottomLeft;var rect=text.rectTransform;rect.anchorMin=Vector2.zero;rect.anchorMax=new Vector2(1,0);rect.pivot=Vector2.zero;rect.anchoredPosition=new Vector2(8,Mathf.Max(8,Screen.safeArea.yMin));rect.sizeDelta=new Vector2(-16,80);label=text;}
```

### Update · L13

方法 SHA-256：`0fdc6128c1d03d1f379316745aba40e5ac1403166fcb0375ca51f4118049ba08`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.MobileFrameStats.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(label==null)return;if(app==null)app=FindFirstObjectByType<AppUI>();if(game==null)game=app?.Session;float dt=Time.unscaledDeltaTime;if(dt<=0)return;frames[cursor++]=dt;cursor%=frames.Length;count=Mathf.Min(count+1,frames.Length);intervalFrames++;elapsed+=dt;total+=dt;worst=Mathf.Max(worst,dt);if(elapsed<1)return;Array.Copy(frames,ordered,count);Array.Sort(ordered,0,count);float p95=ordered[Mathf.Clamp(Mathf.CeilToInt(count*.95f)-1,0,count-1)]*1000;float p99=ordered[Mathf.Clamp(Mathf.CeilToInt(count*.99f)-1,0,count-1)]*1000;int step=game!=null?game.PhysicsStep:0;float hz=Mathf.Max(0,step-lastPhysicsStep)/elapsed;lastPhysicsStep=step;label.text=$"v{GameVersion.Name} {intervalFrames/elapsed:F1} FPS p95 {p95:F0}ms p99 {p99:F0}ms max {worst*1000:F0}ms\nPhysics {hz:F0}Hz balls {game?.ActiveBalls??0} pending {game?.PendingBurstBalls??0} queued {(game!=null&&game.ChainLaunchQueued?1:0)} {Screen.width}x{Screen.height}\nTouch {(app!=null&&app.NativeTouchReady?"wx":"unity")} {app?.BattleTouchStarts??0}/{app?.BattleTouchMoves??0}/{app?.BattleTouchEnds??0} shot {app?.BattleAcceptedShots??0} {app?.BattleInputStatus}";if(total>=10){Debug.Log("BOUNCE_FRAME_STATS "+label.text);total=0;}elapsed=0;intervalFrames=0;worst=0;}
```

## 对象层级和素材入口

- L12：`text.transform.SetParent(transform,false)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
readonly float[] frames=new float[240],ordered=new float[240];
```
```csharp
int count,cursor,intervalFrames,lastPhysicsStep;
```
```csharp
float elapsed,total,worst;
```
```csharp
TMP_Text label;
```
```csharp
GameSession game;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
