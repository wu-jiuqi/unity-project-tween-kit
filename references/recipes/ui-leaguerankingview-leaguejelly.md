# LeagueJelly · LeagueRankingView

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/LeagueRankingView.cs`
文件 SHA-256：`e02ec449c21cb64354b47245aee1f660e2a05b161062dc9e25a30388c720d753`
[原始源码快照](../source/Assets/Scripts/UI/LeagueRankingView.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L26

方法 SHA-256：`792ba5dd562b98b45543a716e0509da3a9076150e90eac1d75b74825345ede04`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Exp, Mathf.Sin
解析符号：`BounceGarden.LeagueJelly.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(!FeedbackProfile.Active.enabledFeedback){Finish();return;}time+=Time.unscaledDeltaTime;if(time<delay)return;float t=(time-delay)/Mathf.Max(.05f,duration);if(t>=1){Finish();return;}float wave=Mathf.Sin(t*Mathf.PI*5)*Mathf.Exp(-4*t)*(1-t);transform.localScale=Vector3.Scale(rest,new Vector3(1+wave*.09f,1-wave*.12f,1));transform.localRotation=rotation*Quaternion.Euler(0,0,wave*2.2f);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){if(ready){transform.localScale=rest;transform.localRotation=rotation;}}
```

## 源字段与依赖

```csharp
Vector3 rest;
```
```csharp
Quaternion rotation;
```
```csharp
float time,delay;
```
```csharp
bool ready;
```
```csharp
public float duration=.65f;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
