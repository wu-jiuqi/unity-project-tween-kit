# ResultAudioTimeline · ResultAudioTimeline

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/ResultAudioTimeline.cs`
文件 SHA-256：`6ef73c61d15787677b29d06c582f81a910ac735c0f02c88be3afd7601ddbd3a1`
[原始源码快照](../source/Assets/Scripts/UI/ResultAudioTimeline.cs.txt)

用在哪里：VictoryPresentation.Begin / FailurePresentation.Begin → ResultAudioTimeline.Initialize
触发操作：按原节点发声；Skip / OnDisable 取消瞬态音频
导入组件：`ProjectTweenKit.ResultAudioTimeline`；展馆 ID：`victory`。
复用形式：宿主适配配方。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L7

方法 SHA-256：`662841b41cbdc94dce6a3a52993a94beca693f8963671e000a1c2713d1cf3802`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ResultAudioTimeline.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(!active)return;age+=Time.unscaledDeltaTime;if(win){Cue(0,.38f,"cat_jump_soft");Cue(1,.55f,"cat_happy");Cue(2,.76f,"cat_land_soft");Cue(3,1,"result_win_burst");Cue(4,1.45f,"result_reward_reveal");}else{Cue(0,.55f,"cat_retry");Cue(1,.72f,"cat_droop_soft");}if(age>=2.1f&&(fired&128)==0){fired|=128;SoundService.Speak(win?"vo_win_zh":"vo_retry_zh",90);}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){active=false;SoundService.CancelTransient();}
```

## 源字段与依赖

```csharp
bool win,active;
```
```csharp
float age;
```
```csharp
int fired;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
