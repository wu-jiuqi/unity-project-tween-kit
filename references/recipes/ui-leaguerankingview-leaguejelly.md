# LeagueJelly · LeagueRankingView

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/LeagueRankingView.cs`
文件 SHA-256：`04143f33f8a659d50a1c82925fad238eeb83aaeb08bbf1345da6910054f29356`
[原始源码快照](../source/Assets/Scripts/UI/LeagueRankingView.cs.txt)

用在哪里：LeagueRankingView.Refresh / LeaguePodiumView.Build → LeagueJelly.Play
触发操作：首屏错峰或名次/积分更新时卡片果冻抖动
导入组件：`ProjectTweenKit.LeagueJelly`；展馆 ID：`leaguejelly`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L24

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
