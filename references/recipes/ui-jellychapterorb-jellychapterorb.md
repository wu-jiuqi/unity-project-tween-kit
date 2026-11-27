# JellyChapterOrb · JellyChapterOrb

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/JellyChapterOrb.cs`
文件 SHA-256：`59552a136c50ddf11ddfb19228c263daff98490b443e953a9b2a925ba720bea6`
[原始源码快照](../source/Assets/Scripts/UI/JellyChapterOrb.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L20

方法 SHA-256：`bc015e9b93d4bfe92a9802b4f88449c967773150d14ec1f0cd233faecf6bd0d2`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Exp, Mathf.Sin
解析符号：`BounceGarden.JellyChapterOrb.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){var owner=GetComponentInParent<AppUI>();if(owner!=null&&!owner.HomeMotionAllowed){velocity=Vector2.zero;return;}float dt=Mathf.Min(Time.unscaledDeltaTime,.033f);
  if(!dragging){rect.anchoredPosition+=velocity*dt;Constrain(true);velocity*=Mathf.Exp(-1.25f*dt);if(velocity.sqrMagnitude<9)velocity=Vector2.zero;}
  if(hint!=null){hintClock+=dt;float t=hintClock%5f;float bounce=t<.6f?Mathf.Sin(t/.6f*Mathf.PI*2)*Mathf.Exp(-t*5):0;hint.anchoredPosition=new Vector2(-43,43+6*Mathf.Abs(bounce));hint.localScale=new Vector3(1-bounce*.1f,1+bounce*.14f,1);}
  if(dragging){RenderPull();return;}
  visual.anchoredPosition=Vector2.Lerp(visual.anchoredPosition,Vector2.zero,1-Mathf.Exp(-22*dt));
  if(squash>0){squash=Mathf.Max(0,squash-dt);float f=Mathf.Sin((.24f-squash)/.24f*Mathf.PI*2)*Mathf.Exp(-(.24f-squash)*9)*.23f;visual.localScale=squashAxis.x>0?new Vector3(1-f,1+f,1):new Vector3(1+f,1-f,1);}else visual.localScale=Vector3.Lerp(visual.localScale,Vector3.one,1-Mathf.Exp(-18*dt));
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){if(dragging&&moved)SoundService.Play("orb_jelly_release");dragging=false;pointer=-1;velocity=pull=Vector2.zero;if(visual!=null){visual.localScale=Vector3.one;visual.anchoredPosition=Vector2.zero;}}
```

```csharp
void OnApplicationFocus(bool focus){if(!focus)OnDisable();}
```

## 源字段与依赖

```csharp
RectTransform rect,visual,parent,hint;
```
```csharp
Action open;
```
```csharp
Vector2 velocity,pointerStart,anchor,pull;
```
```csharp
float squash,hintClock;
```
```csharp
bool dragging,moved;
```
```csharp
int pointer=-1;
```
```csharp
Vector2 squashAxis;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
