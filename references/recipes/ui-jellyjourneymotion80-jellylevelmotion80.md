# JellyLevelMotion80 · JellyJourneyMotion80

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/JellyJourneyMotion80.cs`
文件 SHA-256：`03c5ac68c7c3f15a1d9602399a3b064b6ae514ff746384240e55b113f6f2162b`
[原始源码快照](../source/Assets/Scripts/UI/JellyJourneyMotion80.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Clock · L16

方法 SHA-256：`8fa78afdb94aa88a52d0cb468201e5f0d0e0322376174cc720d8d86379be3316`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyLevelMotion80.Clock`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
float Clock=>Owner!=null?Owner.MotionClock:Time.unscaledTime;
```

### Entrance · L18

方法 SHA-256：`ebd18349bb1fee1ef1713afcca5cef33b135dabd575ddeccf1450124e0635cf1`
时间来源：SetUpdate(true)
曲线证据：Ease.OutElastic
解析符号：`BounceGarden.JellyLevelMotion80.Entrance()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyJourneyMotion80.cs:26` · `JellyLevelMotion80.LateUpdate` → `Entrance()`

```csharp
void Entrance(){entryTween?.Kill();enter=.3f;entryTween=DOTween.To(()=>enter,v=>enter=v,1,.7f).SetDelay(IsControl?0:Mathf.Repeat(index,4)*.055f).SetEase(Ease.OutElastic,.95f,.26f).SetUpdate(true).SetTarget(this);}
```

### OnPointerDown · L19

方法 SHA-256：`e3e076fefbdf9dce6b68e79a8609ad0698d90aa8e3ff3206b5113e787135c15f`
时间来源：SetUpdate(true)
曲线证据：Ease.OutQuad
解析符号：`BounceGarden.JellyLevelMotion80.OnPointerDown(UnityEngine.EventSystems.PointerEventData)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void OnPointerDown(PointerEventData e){if(!Allowed||locked||e.button!=PointerEventData.InputButton.Left)return;held=true;pressTween?.Kill();pressTween=DOTween.To(()=>press,v=>press=v,1,.11f).SetEase(Ease.OutQuad).SetUpdate(true).SetTarget(this);}
```

### Release · L22

方法 SHA-256：`a038d51f9f1e03835f76fbbf6332757864e9ac95def9b13c59e7ea87ff71541e`
时间来源：SetUpdate(true)
曲线证据：Ease.OutElastic
解析符号：`BounceGarden.JellyLevelMotion80.Release()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyJourneyMotion80.cs:20` · `JellyLevelMotion80.OnPointerUp` → `Release()`
- `Assets/Scripts/UI/JellyJourneyMotion80.cs:21` · `JellyLevelMotion80.OnPointerExit` → `Release()`

```csharp
public void Release(){if(!held)return;held=false;kickAt=Clock;pressTween?.Kill();pressTween=DOTween.To(()=>press,v=>press=v,0,.8f).SetEase(Ease.OutElastic,1.25f,.21f).SetUpdate(true).SetTarget(this);}
```

### Apply · L27

方法 SHA-256：`121f4e8764cc951c8b1bc8befc325a8372cb5cf42bd220acf20daf04d49bbd01`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Exp, Mathf.LerpUnclamped, Mathf.Sin
解析符号：`BounceGarden.JellyLevelMotion80.Apply(float, bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyJourneyMotion80.cs:24` · `JellyLevelMotion80.ApplyAuditPose` → `Apply(clock,true)`
- `Assets/Scripts/UI/JellyJourneyMotion80.cs:26` · `JellyLevelMotion80.LateUpdate` → `Apply(Clock,false)`

```csharp
void Apply(float time,bool audit){if(Body==null)return;float age=time-kickAt;float rebound=age>=0&&age<1.4f?Mathf.Sin(age*23)*Mathf.Exp(-age*4.3f):0;float idle=Mathf.Sin(time*2.55f+phase)*(current?.09f:.048f);float drift=Mathf.Sin(time*1.65f+phase);float tug=Owner!=null?Owner.DragPull:0;
  float sx=1+idle+press*.25f-rebound*.21f,sy=1-idle-press*.33f+rebound*.30f;
  sx*=Mathf.LerpUnclamped(.46f,1,enter);sy*=Mathf.LerpUnclamped(1.38f,1,enter);
  if(IsControl){sx=Mathf.Clamp(sx,.97f,1.04f);sy=Mathf.Clamp(1-press*.13f+rebound*.12f,.84f,1.08f);}else{sx=Mathf.Clamp(sx,.70f,1.31f);sy=Mathf.Clamp(sy,.65f,1.34f);}
  Body.localScale=new Vector3(sx,sy,1);float sway=IsControl?0:Owner.Sway(index);Body.anchoredPosition=new Vector2(sway,IsControl?0:drift*7+rebound*15-press*9);Body.localRotation=Quaternion.Euler(0,0,IsControl?0:drift*2.6f-tug*5+rebound*6);
  if(Island!=null){Island.localScale=new Vector3(1+drift*.035f+press*.10f-rebound*.075f,1-drift*.075f-press*.12f+rebound*.13f,1);Island.anchoredPosition=new Vector2(sway,-Mathf.Sin(time*1.65f+phase-.6f)*13+rebound*18);Island.localRotation=Quaternion.Euler(0,0,drift*2-tug*3+rebound*4);}
  if(Cat!=null&&Cat.gameObject.activeSelf){Cat.localScale=new Vector3(1-drift*.035f,1+drift*.055f,1);Cat.localRotation=Quaternion.Euler(0,0,drift*4+rebound*7);Cat.anchoredPosition=new Vector2(154+sway,-15+Mathf.Abs(drift)*9+Mathf.Max(0,rebound)*20);}
  if(Tag!=null&&Tag.gameObject.activeSelf){Tag.anchoredPosition=new Vector2(sway,Mathf.Max(188,120*sy+Body.anchoredPosition.y+54));Tag.localScale=Vector3.one*(1+Mathf.Sin(time*2.8f+phase)*.035f);}
  if(Bubbles!=null&&Bubbles.gameObject.activeSelf){Bubbles.anchoredPosition=new Vector2(-155+Mathf.Sin(time*.9f+phase)*11,25+Mathf.Sin(time*1.2f+phase)*19);Bubbles.localScale=Vector3.one*(1+Mathf.Sin(time*1.4f+phase)*.10f);}
  if(Halo!=null&&Halo.gameObject.activeSelf){Halo.localScale=new Vector3(sx*1.07f,sy*1.07f,1);Halo.anchoredPosition=Body.anchoredPosition;Halo.localRotation=Body.localRotation;}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void ResetPose(){if(Body!=null){Body.localScale=Vector3.one;Body.anchoredPosition=Vector2.zero;Body.localRotation=Quaternion.identity;}if(Island!=null){Island.localScale=Vector3.one;Island.anchoredPosition=Vector2.zero;Island.localRotation=Quaternion.identity;}if(Cat!=null){Cat.localScale=Vector3.one;Cat.localRotation=Quaternion.identity;Cat.anchoredPosition=new Vector2(154,-15);}if(Tag!=null){Tag.anchoredPosition=new Vector2(0,188);Tag.localScale=Vector3.one;}if(Halo!=null){Halo.localScale=Vector3.one;Halo.anchoredPosition=Vector2.zero;Halo.localRotation=Quaternion.identity;}}
```

```csharp
void Kill(){entryTween?.Kill();pressTween?.Kill();entryTween=pressTween=null;}
```

```csharp
void OnDisable(){Kill();wasAllowed=held=false;press=0;ResetPose();}
```

```csharp
void OnDestroy(){Kill();}
```

```csharp
void OnApplicationFocus(bool value){focused=value;if(!value){Kill();held=false;press=0;wasAllowed=false;ResetPose();}}
```

```csharp
void OnApplicationPause(bool value){paused=value;if(value){Kill();held=false;press=0;wasAllowed=false;ResetPose();}}
```

## 源字段与依赖

```csharp
public JellyJourney80 Owner;
```
```csharp
public RectTransform Body,Island,Cat,Tag,Halo,Bubbles;
```
```csharp
public bool IsControl;
```
```csharp
int index=-999;
```
```csharp
bool current,locked,held,focused=true,paused,wasAllowed;
```
```csharp
float enter=1,press,kickAt=-100,phase;
```
```csharp
Tween entryTween,pressTween;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
