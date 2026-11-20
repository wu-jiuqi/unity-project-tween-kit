# VictoryRoundPreparation · VictoryRoundPreparation

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/VictoryRoundPreparation.cs`
文件 SHA-256：`332f90d5c28e75181f031f33083ddfc8892ceef7cc3d1ba46c981a52f2527950`
[原始源码快照](../source/Assets/Scripts/UI/VictoryRoundPreparation.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L9

方法 SHA-256：`32ecd253cdb6b6891c587888b7cec3459219414f3b0c446fc678a9721a0874bd`
时间来源：PresentationDelta, Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryRoundPreparation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){float dt=game.PresentationDelta(Mathf.Min(Time.unscaledDeltaTime,.05f));game.TickVictoryBoard(dt);shown=Mathf.MoveTowards(shown,game.Score,Mathf.Max(3000,(game.Score-shown)*10)*dt);summary?.ShowVictoryScore(Mathf.RoundToInt(shown));meter?.CountVictoryScore(Mathf.RoundToInt(shown));
  {waitAge+=Time.unscaledDeltaTime;if(diagnostics<3&&waitAge>=(diagnostics+1)*5){diagnostics++;Debug.LogWarning("BOUNCE_VICTORY_WAIT phase="+phase+" shown="+shown+" score="+game.Score+" "+game.VictorySettlementStatus);}}
  if(phase==0){stable=game.VictoryRoundReady?stable+dt:0;if(stable>=.12f){phase=1;stable=0;diagnostics=0;waitAge=0;game.TriggerVictorySpecials();}}
  else {stable=game.VictoryEffectsReady&&Mathf.Abs(shown-game.Score)<.5f?stable+dt:0;if(stable>=.12f){game.RefillVictoryEmptyBoard();game.LockVictoryDamage();summary?.ShowVictoryScore(game.Score,true);meter?.CountVictoryScore(game.Score);var next=done;done=null;enabled=false;next?.Invoke();}}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
GameSession game;
```
```csharp
BattleHUDSummary summary;
```
```csharp
BattleScoreMeter meter;
```
```csharp
Action done;
```
```csharp
int phase,diagnostics;
```
```csharp
float stable,shown,waitAge;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
