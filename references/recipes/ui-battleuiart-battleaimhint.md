# BattleAimHint · BattleUIArt

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/BattleUIArt.cs`
文件 SHA-256：`5c921778acbeb37724d2a7c3296581b48c6e0619d3f53502e227f9cd6077cd50`
[原始源码快照](../source/Assets/Scripts/UI/BattleUIArt.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `HintFade` / `hintfade`：提取每秒2透明度变化；宿主传Visible，不监听游戏事件，所有提示组不挡射线

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### Update · L17

方法 SHA-256：`1e1984428837fb54810f38e079b4cc07b3410c6225c331914b68ecc48563a8e0`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BattleAimHint.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(game==null||game.IsPaused)return;age+=Time.deltaTime;if(instruction==null)instruction=GetComponentInChildren<TMPro.TMP_Text>();bool queued=game.ChainLaunchQueued;if(wasQueued!=queued&&instruction!=null){instruction.text=queued?"下一组已准备好":"拖动瞄准，松手发射";wasQueued=queued;}if(game.Shots!=shots||age>=16)activity=1;group.alpha=game.IsPlaying?Mathf.MoveTowards(group.alpha,queued||activity==0?1:0,Time.deltaTime*2):0;}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(game!=null)game.Feedback-=Feedback;}
```

## 源字段与依赖

```csharp
GameSession game;
```
```csharp
CanvasGroup group;
```
```csharp
float age,activity;
```
```csharp
int shots;
```
```csharp
TMPro.TMP_Text instruction;
```
```csharp
bool wasQueued;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
