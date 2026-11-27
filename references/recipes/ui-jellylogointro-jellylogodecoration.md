# JellyLogoDecoration · JellyLogoIntro

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/JellyLogoIntro.cs`
文件 SHA-256：`b95d3a90f696aace042f7b35ded905e0101bdc962f77faf0baf9f095c0db3470`
[原始源码快照](../source/Assets/Scripts/UI/JellyLogoIntro.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Play · L135

方法 SHA-256：`b280203ff8cac5266a6f306c36c3add5fa2d83a70a170b41c5ee3352fc4a6556`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyLogoDecoration.Play(int, float, float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Play(int index,float endAt,float amplitude){
  rect=(RectTransform)transform;rest=rect.anchoredPosition;restScale=rect.localScale;restRotation=rect.localRotation;
  started=Time.unscaledTime;ends=endAt;strength=amplitude;phase=index*.36f;running=true;
 }
```

### Update · L139

方法 SHA-256：`21fe1bdd0348639db08d784c39ec52bd1ea623b0781ce35edfc02fecca58f62c`
时间来源：Time.unscaledTime
曲线证据：Mathf.SmoothStep
解析符号：`BounceGarden.JellyLogoDecoration.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){
  if(!running)return;float now=Time.unscaledTime;
  if(now>=ends){ResetPose();return;}
  float elapsed=now-started;
  var pose=JellyLogoMotion.Sample(elapsed,ends-now,strength,phase,Mathf.SmoothStep(0,1,elapsed/.14f));
  rect.localScale=Vector3.Scale(restScale,pose.Scale);rect.anchoredPosition=rest+Vector2.up*pose.Lift;
  rect.localRotation=restRotation*Quaternion.Euler(0,0,pose.Rotation);deformation=pose.Deformation;graphic.SetVerticesDirty();
 }
```

### ResetPose · L147

方法 SHA-256：`3c9375a0522ef8b5bf8e5cb956315a771d8f5c7d1bbd4b045de9f4bbbf4e65f0`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyLogoDecoration.ResetPose()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyLogoIntro.cs:141` · `JellyLogoDecoration.Update` → `ResetPose()`
- `Assets/Scripts/UI/JellyLogoIntro.cs:152` · `JellyLogoDecoration.OnDisable` → `ResetPose()`

```csharp
void ResetPose(){
  running=false;if(rect==null)return;rect.anchoredPosition=rest;rect.localScale=restScale;rect.localRotation=restRotation;
  deformation=0;if(graphic!=null)graphic.SetVerticesDirty();
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void ResetPose(){
  running=false;if(rect==null)return;rect.anchoredPosition=rest;rect.localScale=restScale;rect.localRotation=restRotation;
  deformation=0;if(graphic!=null)graphic.SetVerticesDirty();
 }
```

```csharp
protected override void OnDisable(){ResetPose();base.OnDisable();}
```

## 源字段与依赖

```csharp
RectTransform rect;
```
```csharp
Vector2 rest;
```
```csharp
Vector3 restScale;
```
```csharp
Quaternion restRotation;
```
```csharp
float started,ends,strength,phase,deformation;
```
```csharp
bool running;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
