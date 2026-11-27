# CameraJuice · CameraJuice

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/CameraJuice.cs`
文件 SHA-256：`424592abcbfe92fd7403d823fbfd2e82667a6ea9786495a77328b71a469ec73e`
[原始源码快照](../source/Assets/Scripts/World/CameraJuice.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Impact · L13

方法 SHA-256：`30adbec7a0d7015ba3060b707c34b78a7ffaf764c77ccefc16bd3e3a68cbbf62`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CameraJuice.Impact(float, float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Impact(float strength=.12f,float seconds=.20f){
  if(Owner==null||!Owner.IsPlaying||Owner.IsPaused||!FeedbackProfile.Active.enabledFeedback||!FeedbackProfile.Active.screenShake)return;
  float limit=cameraView.orthographicSize*2*.0042f;strength=Mathf.Clamp(strength,0,limit);
  // Repeated impulses coalesce without extending the shake forever.
  if(left>0){amplitude=Mathf.Max(amplitude,strength);if(strength>=amplitude*.9f&&left<duration*.5f&&Time.unscaledTime<episodeUntil-.08f)left=Mathf.Min(duration*.7f,episodeUntil-Time.unscaledTime);return;}
  amplitude=strength;duration=Mathf.Clamp(seconds,.18f,.34f);left=duration;episodeUntil=Time.unscaledTime+.40f;
 }
```

### LateUpdate · L22

方法 SHA-256：`8ac2d87794a0757cefba962c28a9386854154f404c282f4b4f628a23e88c4faa`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Exp, Mathf.Sin
解析符号：`BounceGarden.CameraJuice.LateUpdate()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void LateUpdate(){
  if(Owner==null||!Owner.IsPlaying||Owner.IsPaused||!FeedbackProfile.Active.enabledFeedback||!FeedbackProfile.Active.screenShake||left<=0){ResetImpact();return;}
  float t=1-left/duration,envelope=Mathf.Exp(-3*t)*(1-t),phase=t*Mathf.PI;
  // Soft compression and one small rebound, with no instant sideways kick.
  transform.localPosition=rest+new Vector3(Mathf.Sin(phase*2)*.32f,Mathf.Sin(phase*2.8f)*.82f,0)*amplitude*envelope;
  left=Mathf.Max(0,left-Time.unscaledDeltaTime);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public void ResetImpact(){left=0;amplitude=0;transform.localPosition=rest;}
```

```csharp
void OnApplicationFocus(bool focus){if(!focus)ResetImpact();}
```

```csharp
void OnApplicationPause(bool pause){if(pause)ResetImpact();}
```

```csharp
void OnDisable(){ResetImpact();}
```

## 源字段与依赖

```csharp
public GameSession Owner;
```
```csharp
float left,duration=.20f,amplitude,episodeUntil;
```
```csharp
Vector3 rest;
```
```csharp
Camera cameraView;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
