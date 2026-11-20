# LevelBalanceRunner · LevelBalanceRunner

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LevelEditor/LevelBalanceRunner.cs`
文件 SHA-256：`21aaca4d705f9ae610219efe12ec1fa1f4d5ade1a09d9783f180123bdefce3e3`
[原始源码快照](../source/Assets/Scripts/LevelEditor/LevelBalanceRunner.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L33

方法 SHA-256：`118cd7721870c9a13a909cd4f3de3be60fbe6a0aa9ce06fc3b743206338e055f`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.LevelBalanceRunner.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var task=Run();while(true){bool more;try{more=task.MoveNext();}catch(Exception e){Debug.LogError(e);Application.Quit(2);yield break;}if(!more)yield break;yield return task.Current;}}
```

### Run · L34

方法 SHA-256：`20defa00fbe60eafea7aa21a5f8c7fcc464829c4c079c1a9f265848de9c52043`
时间来源：Time.fixedDeltaTime, Time.timeScale
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.LevelBalanceRunner.Run()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/LevelBalanceRunner.cs:33` · `LevelBalanceRunner.Start` → `Run()`

```csharp
IEnumerator Run(){
  Active=true;BalanceClock.Active=true;Application.runInBackground=true;Application.logMessageReceived+=OnLog;
  if(LevelEditorRuntime.HasArg("--balance-selfcheck")){int failures=OpeningBalanceChecks.Run(LevelEditorRuntime.Arg("--qa-out"));Application.Quit(failures==0?0:3);yield break;}
  string path=LevelEditorRuntime.Arg("--balance-request");
  try{request=JsonUtility.FromJson<BalanceRequest>(File.ReadAllText(path));
   if(request.first<1||request.last>45||request.first>request.last||request.trials<1||request.trials>10000||request.maxSteps<600||(request.requestVersion!=2||request.policy!="advanced"||!new[]{"all","rapid","settled","hybrid"}.Contains(request.fireMode))||!new[]{"screen","validation","holdout"}.Contains(request.split))throw new ArgumentException("Invalid balance request");
   request.weights.Validate();Directory.CreateDirectory(request.output);OpeningCampaign.LoadBalanceData(File.ReadAllText(request.input));
  }catch(Exception e){Debug.LogError(e);Application.Quit(2);yield break;}
  
  Physics2D.simulationMode=SimulationMode2D.Script;Time.fixedDeltaTime=1f/60;Time.timeScale=0;
  SoundService.SetMuted(true);AlphabetTypography.Install();
  var modes=request.fireMode=="all"?new[]{"rapid","settled","hybrid"}:new[]{request.fireMode};
  string requestHash=Hash(File.ReadAllText(path));
  foreach(int level in Enumerable.Range(request.first,request.last-request.first+1))foreach(string mode in modes)for(int trial=0;trial<request.trials;trial++){
   string resultPath=Path.Combine(request.output,$"L{level:00}-{mode}-{trial:0000}.json");
   if(File.Exists(resultPath)){var old=JsonUtility.FromJson<BalanceRun>(File.ReadAllText(resultPath));if(old.requestHash!=requestHash)throw new InvalidOperationException("Resume request mismatch");continue;}
   int seed=unchecked(request.seed+level*100003+trial*7919+(request.split=="screen"?0:request.split=="validation"?100000000:200000000));
   UnityEngine.Random.InitState(seed);var root=new GameObject("Isolated balance session");
   game=root.AddComponent<GameSession>();game.enabled=false;game.QAIgnoreFocusLoss=true;
   game.BoosterProfile=LocalProfile.CreateLevelEditorProfile(level-1,"bare");
   game.StartLevel(level-1);game.BalanceSeed(seed);
   StrategyRound strategyData=null;PlayerStrategyRecorder strategyRecorder=null;
   if(request.recordPlayerStrategy){strategyData=new StrategyRound{engineHash=PlayerStrategyReport.EngineFingerprint,platform=Application.platform.ToString()};strategyRecorder=new PlayerStrategyRecorder(game,strategyData);game.StrategyObserver=strategyRecorder.Observe;}game.SetPaused(false);game.CombatFeel?.CancelStop();
   outcome=won=false;game.Outcome+=OnOutcome;int initialErrors=errorCount,step=0,nextCheck=0,stable=0,lastShot=-100000,lastProgress=0,waitSteps=0,maxGroups=0;var shots=new List<BalanceShot>();
   while(!outcome&&step<request.maxSteps&&errorCount==initialErrors){
    stable=game.BalanceFullySettled?stable+1:0;BalanceShot attempt=null;int beforeShots=game.Shots;
    if(step>=nextCheck&&game.ChainCanLaunch){
     float since=(step-lastShot)*Time.fixedDeltaTime;int used=game.CurrentLevel.initialShots-game.Shots;
     bool settled=stable*Time.fixedDeltaTime>=request.weights.stableSeconds;
     if(since+.0001f>=request.weights.interval&&(mode!="settled"||settled)){
      // Waiting does not consume a different aiming RNG stream. The same shot
      // index receives the same candidate angles/noise in all firing modes.
      var aim=BalanceFirePolicy.Choose(game,request.weights,new System.Random(unchecked(seed^(used+1)*1640531527)));
      var state=new BalanceTimingState{CanLaunch=true,Settled=settled,Groups=game.ChainLiveGroups,Moving=game.BalanceBoardMoving,Effects=game.BalanceEffectsPending,PendingRewardGroups=game.BalancePendingRewardGroups,Progress=game.BalanceGoalProgress>lastProgress,SinceShot=since,SpecialOpportunity=aim.SpecialOpportunity};
      if(BalanceFirePolicy.ShouldFire(mode,state,request.weights,out string reason)&&game.TryShoot(aim.Direction))attempt=new BalanceShot{step=step,used=used+1,x=aim.Direction.x,y=aim.Direction.y,score=game.Score,collected=(int[])game.OpeningCollected.Clone(),activeBalls=game.ActiveBalls,liveGroups=game.ChainLiveGroups,settled=settled,reason=reason,utility=aim.Utility,availableSpecials=game.ChainTools.Count(t=>!t.Consumed&&!t.Waiting)};
      else waitSteps+=6;
      nextCheck=step+6;
     }else if(mode!="rapid")waitSteps++;
    }
    try{strategyRecorder?.Tick(Time.fixedDeltaTime,true);game.BalanceStep();}catch(Exception e){Debug.LogError(e);}
    if(attempt!=null&&game.Shots<beforeShots){shots.Add(attempt);lastShot=step;lastProgress=attempt.collected.Sum();}
    maxGroups=Math.Max(maxGroups,game.ChainLiveGroups);step++;if(step%300==0)yield return null;
   }
   int score=game.Score;bool valid=outcome&&errorCount==initialErrors;
   if(valid)game.FinalizeScore(won);
   var row=new BalanceRun{level=level,trial=trial,seed=seed,policy="advanced",fireMode=mode,weightsHash=request.weightsHash,policyVersion=PolicyVersion,overlapShots=shots.Count(s=>s.liveGroups>0||s.activeBalls>0),settledShots=shots.Count(s=>s.settled),waitSteps=waitSteps,maxLiveGroups=maxGroups,split=request.split,status=errorCount>initialErrors?"error":!outcome?"timeout":won?"win":"loss",error=errorCount>initialErrors?lastError:null,
    requestHash=requestHash,diagnostics=valid?null:game.VictorySettlementStatus+"\n"+game.QARefillStatus(),levelHash=Hash(JsonUtility.ToJson(OpeningCampaign.Stage(level-1))),steps=step,seconds=step*Time.fixedDeltaTime,
    used=game.CurrentLevel.initialShots-game.Shots,remaining=game.Shots,scoreAtOutcome=score,scoreWithSpareBonus=game.Score,spareBonus=game.CreditedCompletionBonus,
    damageScore=game.DamageScore,breakScore=game.BreakScore,objectiveScore=game.ObjectiveScore,hits=game.HitCount,breaks=game.BreakCount,activations=game.ChainActivationCount,
    refills=game.ChainRefills,ingotConversions=game.OpeningIngotConversions,goals=game.OpeningTargets,collected=(int[])game.OpeningCollected.Clone(),shots=shots.ToArray()};
   if(strategyData!=null){var history=new BalanceHistory();history.rounds.Add(new BalanceRound{id=$"batch-{level}-{mode}-{trial}",level=level,version=GameVersion.Name,configHash=row.levelHash,strategyVersion=2,strategy=strategyData,usedShots=row.used,initialShots=row.used+row.remaining,remainingShots=row.remaining,score=row.scoreAtOutcome,outcome=row.status,activeSeconds=row.seconds,configAttempt=1,goals=row.goals.Select((g,i)=>new BalanceGoal{kind=g.kind,color=g.color,required=g.count,collected=row.collected[i]}).ToArray()});File.WriteAllText(Path.Combine(request.output,$"strategy-L{level:00}-{mode}-{trial:0000}.json"),BalanceReport.Json(history));}
   File.WriteAllText(resultPath+".tmp",JsonUtility.ToJson(row,true));File.Move(resultPath+".tmp",resultPath);
   game.Outcome-=OnOutcome;game.StopLevel();Destroy(root);game=null;yield return null;
   if(trial==request.trials-1)Debug.Log($"BALANCE_PROGRESS level={level} fireMode={mode} trials={request.trials}");
   if(File.Exists(Path.Combine(request.output,"cancel.request"))){File.WriteAllText(Path.Combine(request.output,"cancelled.json"),"{\"status\":\"cancelled\"}");Application.Quit(0);yield break;}
  }
  File.WriteAllText(Path.Combine(request.output,"complete.json"),"{\"status\":\"complete\",\"policyVersion\":\""+PolicyVersion+"\"}");
  Debug.Log("LEVEL_BALANCE_COMPLETE");Application.Quit(errorCount==0?0:3);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){Application.logMessageReceived-=OnLog;Active=false;}
```

## 源字段与依赖

```csharp
public const string PolicyVersion="advanced-fire-v4";
```
```csharp
BalanceRequest request;
```
```csharp
GameSession game;
```
```csharp
bool outcome,won;
```
```csharp
int errorCount;
```
```csharp
string lastError;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
