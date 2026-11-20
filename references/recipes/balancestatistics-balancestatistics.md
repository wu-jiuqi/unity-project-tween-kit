# BalanceStatistics · BalanceStatistics

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/BalanceStatistics.cs`
文件 SHA-256：`bf4eaef05676ab6299bc96dd225de57b5faa22a53a07eb827282674cda63d8d0`
[原始源码快照](../source/Assets/Scripts/BalanceStatistics.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Begin · L69

方法 SHA-256：`e7020e5ca27a7f0b881d3273d115654604e293183e4c2969e55d6e96188c9cae`
时间来源：Time.UtcNow, Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BalanceStatistics.Begin()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/PlaytestMetrics76.cs:33` · `PlaytestMetrics76.BeginRound` → `.Begin()`

```csharp
public void Begin(){Guard(()=>{
  if(!initialized||game.IsTutorial||game.IsStreakTrial)return;
  Finish("restarted");lastLoss=null;generation=game.Generation;launch=null;
  string config=Configuration(game),hash=BalanceReport.Hash(config);int level=game.LevelIndex+1;
  var previous=History.rounds.Where(r=>r.level==level).ToArray();
  var tally=History.attempts.FirstOrDefault(t=>t.level==level&&t.configHash==hash);
  if(tally==null){tally=new BalanceAttempt{level=level,configHash=hash,count=previous.Where(r=>r.configHash==hash).Select(r=>r.configAttempt).DefaultIfEmpty(0).Max()};History.attempts.Add(tally);}tally.count++;
  History.active=new BalanceRound{id=Guid.NewGuid().ToString("N"),startedUtc=DateTime.UtcNow.ToString("o"),version=GameVersion.Name,configJson=config,configHash=hash,level=level,attempt=Math.Max(previous.Select(r=>r.attempt).DefaultIfEmpty(0).Max()+1,History.attempts.Where(t=>t.level==level).Sum(t=>t.count)),configAttempt=tally.count,initialShots=game.Shots,remainingShots=game.Shots,initialSpecials=Specials(),streakTier=game.UsesShopBoosters?app.Profile.CampaignStreakTier:0,outcome="in_progress"};
  BeginStrategy();initialGenerated=game.ChainGeneratedCount;lastStock=Stock();Capture();dirty=true;nextSave=Time.unscaledTime;
 });}
```

### Update · L103

方法 SHA-256：`6c3f233d0288bbc81530981b6109f7c76126006715c42132f5c972762d324358`
时间来源：Time.unscaledDeltaTime, Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BalanceStatistics.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){Guard(()=>{
  if(!initialized||game==null)return;
  if(History.active==null&&lastLoss!=null&&game.Generation==generation&&game.IsPlaying){
   var r=lastLoss;History.rounds.Remove(r);r.continues++;r.continuedShots+=Math.Max(0,game.Shots-r.remainingShots);r.outcome="in_progress";r.endedUtc=null;History.active=r;ResumeStrategy();lastLoss=null;dirty=true;
  }
  if(History.active!=null){
   if(game.Generation!=generation){Finish("interrupted");return;}
   bool active=game.IsPlaying&&!game.IsPaused&&focused&&!backgrounded;TickStrategy(active);if(active)History.active.activeSeconds+=Time.unscaledDeltaTime;
   bool settled=game.ActiveBalls==0&&game.PendingBurstBalls==0&&(!game.IsChainCollection||!game.ChainEffectsPending&&game.CollectionReady);
   if(launch!=null&&settled){CloseLaunch("settled");Capture();dirty=true;}
   if(Time.unscaledTime>=nextSave){Capture();dirty=true;Save();nextSave=Time.unscaledTime+15;}
  }else if(dirty)Save();
 });}
```

### Save · L116

方法 SHA-256：`d20e9bed63788108702a309cc462bc12766af402b6b8b920206b8f8adc603153`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BalanceStatistics.Save()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/BalanceStatistics.cs:101` · `BalanceStatistics.Finish` → `Save()`
- `Assets/Scripts/BalanceStatistics.cs:113` · `BalanceStatistics.Update` → `Save()`
- `Assets/Scripts/BalanceStatistics.cs:114` · `BalanceStatistics.Update` → `Save()`
- `Assets/Scripts/BalanceStatistics.cs:138` · `BalanceStatistics.Checkpoint` → `Save()`
- `Assets/Scripts/BalanceStatisticsQA.cs:86` · `BalanceStatisticsQA.Start` → `stats.Save()`
- `Assets/Scripts/BalanceStatisticsQA.cs:87` · `BalanceStatisticsQA.Start` → `stats.Save()`
- `Assets/Scripts/BalanceStatisticsQA.cs:87` · `BalanceStatisticsQA.Start` → `stats.Save()`
- `Assets/Scripts/BalanceStatisticsQA.cs:88` · `BalanceStatisticsQA.Start` → `stats.Save()`

```csharp
public void Save(){
  if(Time.unscaledTime<saveRetryAt)return;
  bool attempted=false,succeeded=false;
  Guard(()=>{if(RuntimeQA.IsIsolated&&!testWriter||!dirty||writer==null)return;attempted=true;TrimStrategy(History);writer(JsonUtility.ToJson(History));dirty=false;succeeded=true;});
  if(attempted&&!succeeded)saveRetryAt=Time.unscaledTime+5;
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
internal void Restore(string json){
  if(string.IsNullOrEmpty(json))return;
  var data=Validate(json);
  // Unity's inline class serialization can turn a null active field into an empty instance.
  if(data.active!=null&&string.IsNullOrEmpty(data.active.id))data.active=null;
  if(data.active!=null){if(data.active.strategyVersion==2&&data.active.strategy!=null)foreach(var shot in data.active.strategy.shots.Where(s=>s.returnedAt<0))shot.returnState="not_observed_at_interrupted";data.active.outcome="interrupted";data.active.endedUtc=DateTime.UtcNow.ToString("o");foreach(var shot in data.active.launches)if(string.IsNullOrEmpty(shot.endReason))shot.endReason="interrupted";data.rounds.RemoveAll(r=>r.id==data.active.id);data.rounds.Add(data.active);data.active=null;}
  if(data.attempts==null)data.attempts=new System.Collections.Generic.List<BalanceAttempt>();
  Trim(data);History=data;dirty=true;
 }
```

```csharp
void OnApplicationFocus(bool value){focused=value;if(!value)Checkpoint();}
```

```csharp
void OnApplicationPause(bool value){backgrounded=value;if(value)Checkpoint();}
```

```csharp
void OnApplicationQuit(){Finish("interrupted");}
```

## 源字段与依赖

```csharp
public const int HistoryLimit=512,LaunchLimit=256;
```
```csharp
const string StorageKey="BounceGarden.BalanceStatistics.v1";
```
```csharp
AppUI app;
```
```csharp
GameSession game;
```
```csharp
BalanceRound lastLoss;
```
```csharp
BalanceLaunch launch;
```
```csharp
int generation,lastHits,lastBreaks,lastSpecials,lastCombos,lastStock,initialGenerated;
```
```csharp
float lastLaunchTime,nextSave;
```
```csharp
float saveRetryAt;
```
```csharp
bool initialized,focused=true,backgrounded,dirty,testWriter;
```
```csharp
Action<string> writer;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
