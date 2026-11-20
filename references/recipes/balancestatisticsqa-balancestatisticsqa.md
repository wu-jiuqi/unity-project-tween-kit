# BalanceStatisticsQA · BalanceStatisticsQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/BalanceStatisticsQA.cs`
文件 SHA-256：`e1dfc4543dad5fa7294f498a0f72de1380a7517b6ce6a13002638da0f0e127b6`
[原始源码快照](../source/Assets/Scripts/BalanceStatisticsQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L19

方法 SHA-256：`3bdcaaebc0915feba90ca1ae3f8d9d17630fa1829d1fe346a28ab5c1750c4f9a`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BalanceStatisticsQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/BalanceStatisticsQA.cs:92` · `BalanceStatisticsQA.Start` → `Capture("statistics-540")`
- `Assets/Scripts/BalanceStatisticsQA.cs:93` · `BalanceStatisticsQA.Start` → `Capture("summary-540")`
- `Assets/Scripts/BalanceStatisticsQA.cs:94` · `BalanceStatisticsQA.Start` → `Capture("detail-540")`
- `Assets/Scripts/BalanceStatisticsQA.cs:95` · `BalanceStatisticsQA.Start` → `Capture("export-540")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var texture=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,texture);File.WriteAllBytes(Path.Combine(dir,name+".png"),texture.EncodeToPNG());Destroy(texture);rt.Release();Destroy(rt);}
```

### Battle · L20

方法 SHA-256：`63b33c61f525a0cd72bdc262204f92d952038ba2c015dbe4b13042dbf7cc7b77`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BalanceStatisticsQA.Battle(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/BalanceStatisticsQA.cs:47` · `BalanceStatisticsQA.Start` → `Battle(4)`
- `Assets/Scripts/BalanceStatisticsQA.cs:68` · `BalanceStatisticsQA.Start` → `Battle(4)`
- `Assets/Scripts/BalanceStatisticsQA.cs:72` · `BalanceStatisticsQA.Start` → `Battle(4)`
- `Assets/Scripts/BalanceStatisticsQA.cs:81` · `BalanceStatisticsQA.Start` → `Battle(4)`

```csharp
IEnumerator Battle(int level){
  app.QAStartLevel(level);yield return app.QAWaitForLevel();float deadline=Time.realtimeSinceStartup+15;
  while(Time.realtimeSinceStartup<deadline){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();if(!app.AlphabetIntroActive&&!app.Session.IsPaused&&stats.History.active!=null)break;yield return null;}
  game.QAIgnoreFocusLoss=true;StatsEvent("OnApplicationFocus",true);Check(stats.History.active!=null,"round starts after onboarding");
 }
```

### Start · L43

方法 SHA-256：`6655c042cc697fca04e70589e95e2073e75fb5a41130c2ccf119d32ec437703f`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BalanceStatisticsQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();int at=Array.IndexOf(args,"--qa-out");dir=at>=0?args[at+1]:Path.Combine(Application.temporaryCachePath,"BalanceQA");Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Exception||t==LogType.Error){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();Check(app!=null,"AppUI exists");game=app.Session;stats=app.Metrics.Balance;app.QANavigate("home");ModelChecks();
  yield return Battle(4);game.SetPaused(false);var round=stats.History.active;
  Check(round.level==5&&round.initialShots==OpeningCampaign.Stage(4).moves,"authored initial shots captured");
  Check(round.goals.Length==game.OpeningTargets.Length&&round.configHash.Length==64,"goals and SHA256 config captured");
  Check(round.generatedSpecials==0,"initial special pieces not counted as newly generated");
  string config=BalanceStatistics.Configuration(game);var randomBefore=UnityEngine.Random.state;string same=BalanceStatistics.Configuration(game);Check(config==same&&randomBefore.Equals(UnityEngine.Random.state),"config read stable and does not consume random");
  int moves=game.Shots,score=game.Score,stock=app.Profile.BoosterStock(StreakBooster.Pierce);stats.ExportJson();Check(game.Shots==moves&&game.Score==score&&app.Profile.BoosterStock(StreakBooster.Pierce)==stock,"reading statistics does not alter game or inventory");
  int oldMoves=game.CurrentLevel.initialShots;game.CurrentLevel.initialShots++;Check(config!=BalanceStatistics.Configuration(game),"moves change configuration fingerprint");game.CurrentLevel.initialShots=oldMoves;
  string originalColor=game.OpeningTargets[0].color;game.OpeningTargets[0].color="qa-test";Check(config!=BalanceStatistics.Configuration(game),"goal change configuration fingerprint");game.OpeningTargets[0].color=originalColor;
  game.OpeningCollected[0]++;Check(config==BalanceStatistics.Configuration(game),"collected progress does not change fingerprint");game.OpeningCollected[0]--;
  float seconds=round.activeSeconds;game.SetPaused(true);yield return new WaitForSecondsRealtime(.2f);Check(Mathf.Abs(round.activeSeconds-seconds)<.04f,"pause excluded from active seconds");game.SetPaused(false);StatsEvent("OnApplicationFocus",true);yield return new WaitForSecondsRealtime(.15f);Check(round.activeSeconds>seconds+.05f,"active time advances while playing");
  seconds=round.activeSeconds;StatsEvent("OnApplicationPause",true);yield return new WaitForSecondsRealtime(.15f);Check(Mathf.Abs(round.activeSeconds-seconds)<.04f,"background excluded from active time");StatsEvent("OnApplicationPause",false);
  // Score old-formula equivalence including pause, finalization, and cap saturation.
  var brick=game.CollectionBricks.First(b=>b.Alive);int damageSum=game.DamageScore;
  foreach(int hp in new[]{0,1,3,10001}){int prior=game.Score;Call("ScoreImpact",brick,hp,false);Check(game.Score-prior==Mathf.Clamp(hp,0,10000)*100,"damage score legacy equivalence "+hp);}
  game.SetPaused(true);score=game.Score;Call("ScoreImpact",brick,2,true);Check(game.Score==score,"paused scoring still rejected");game.SetPaused(false);
  Check(game.DamageScore>damageSum,"damage breakdown tracks credited points");
  Call("ResetScore");Call("AddScore",ScoreRules.Cap-50);Call("ScoreImpact",brick,1,true);Check(game.Score==ScoreRules.Cap&&game.DamageScore==50&&game.BreakScore==0,"score cap breakdown only credits available points");game.FinalizeScore(true);Check(game.CreditedCompletionBonus==0,"capped completion records actual credited bonus");stats.ExportJson();Check(round.otherScore>=0&&round.damageScore+round.breakScore+round.objectiveScore+round.otherScore+round.completionBonus==round.score,"score components reconcile at cap");Call("ResetScore");
  PlayerStrategyChecks.Run(game,Check,dir);
  moves=game.Shots;Check(game.TryShoot(Vector2.up),"real launch accepted");yield return new WaitForSecondsRealtime(.25f);Check(round.usedShots==1&&game.Shots==moves-1&&round.launches.Count==1,"one accepted volley records one move");
  stats.ExportJson();Check(round.launches[0].number==1,"launch detail included");
  // Restart and return use the real AppUI lifecycle (no synthetic outcome shortcut).
  yield return Battle(4);Check(stats.History.rounds.Any(r=>r.id==round.id&&r.outcome=="restarted"),"restart preserves previous attempt");
  Check(stats.History.active.configAttempt==2,"same config attempt increments");round=stats.History.active;
  app.QANavigate("home");yield return null;Check(stats.History.active==null&&round.outcome=="abandoned","return home records exit");
  // Force only fixture readiness; run actual LateUpdate outcome and AppUI finalization.
  yield return Battle(4);round=stats.History.active;game.enabled=false;game.SetPaused(false);
  for(int i=0;i<game.OpeningTargets.Length;i++)game.OpeningCollected[i]=game.OpeningTargets[i].count;
  Physics2D.simulationMode=SimulationMode2D.Script;
  for(int i=0;i<180&&game.IsPlaying;i++)game.QAAdvanceOneStep();
  float waitUntil=Time.realtimeSinceStartup+15;while(stats.History.active!=null&&Time.realtimeSinceStartup<waitUntil)yield return null;
  Check(round.hasSettlementBreakdown&&round.playScore+round.victoryClearScore+round.completionBonus==round.score,"phase score split reconciles without double counting");Check(BalanceReport.RoundsCsv(new[]{round}).Contains("通关清场奖励"),"phase scores exported to CSV");
  Check(!game.IsPlaying&&round.outcome=="win"&&stats.History.active==null,"actual goal outcome finalizes winning record");
  Check(round.completionBonus==game.CreditedCompletionBonus&&round.score==game.Score,"win recorded after completion bonus");
  int count=stats.History.rounds.Count;app.Metrics.Finish("win");Check(stats.History.rounds.Count==count,"duplicate finish is idempotent");
  app.QANavigate("home");game.enabled=true;yield return Battle(4);round=stats.History.active;game.enabled=false;game.SetPaused(false);
  typeof(GameSession).GetField("rules",BindingFlags.Instance|BindingFlags.NonPublic).GetValue(game).GetType().GetMethod("Reset").Invoke(typeof(GameSession).GetField("rules",BindingFlags.Instance|BindingFlags.NonPublic).GetValue(game),new object[]{0});
  for(int i=0;i<180&&game.IsPlaying;i++)game.QAAdvanceOneStep();Check(!game.IsPlaying&&round.outcome=="loss","actual exhausted-shot outcome records failure");Check(round.goals.Any(g=>g.collected<g.required)&&round.completionBonus==0,"failure retains missing goals without win bonus");
  app.QANavigate("home");yield return null;
  // Deliberate collector failure must not propagate or touch gameplay state.
  score=game.Score;moves=game.Shots;int writes=0;stats.SetWriterForTest(_=>{writes++;throw new IOException("QA injected storage failure");});stats.Save();Check(game.Score==score&&game.Shots==moves&&stats.LastError.Contains("QA injected"),"storage failure contained");
  stats.Save();stats.Save();Check(writes==1,"storage failures are throttled instead of retrying every frame");
  string retried=null;stats.SetWriterForTest(json=>{retried=json;File.WriteAllText(Path.Combine(dir,"persisted-history.json"),json);});stats.Save();Check(!string.IsNullOrEmpty(retried),"failed storage write can be retried");
  var diskRestore=new GameObject("Disk recovery fixture").AddComponent<BalanceStatistics>();diskRestore.Restore(File.ReadAllText(Path.Combine(dir,"persisted-history.json")));Check(diskRestore.History.active==null&&diskRestore.History.rounds.Count==stats.History.rounds.Count,"disk round trip does not invent an active record");Destroy(diskRestore.gameObject);
  File.WriteAllText(Path.Combine(dir,"sample-rounds.json"),stats.ExportJson());File.WriteAllText(Path.Combine(dir,"sample-levels.csv"),BalanceReport.SummaryCsv(stats.History.rounds));
  typeof(AppUI).GetMethod("Settings",BindingFlags.Instance|BindingFlags.NonPublic).Invoke(app,null);yield return new WaitForSecondsRealtime(.25f);
  Check(app.GetComponentsInChildren<Button>().Any(b=>b.isActiveAndEnabled&&b.name=="试玩统计")==PlayerStrategyTestBuild.Enabled,"statistics entry follows the Windows playtest build flag");typeof(AppUI).GetMethod("OpenBalanceStatistics",BindingFlags.Instance|BindingFlags.NonPublic).Invoke(app,null);yield return new WaitForSecondsRealtime(.3f);yield return Capture("statistics-540");
  Click("Button 查看关卡汇总");yield return new WaitForSecondsRealtime(.3f);yield return Capture("summary-540");
  Click("Button 查看逐局");yield return new WaitForSecondsRealtime(.2f);var recordButton=app.GetComponentsInChildren<Button>().FirstOrDefault(b=>b.name.StartsWith("Balance round "));Check(recordButton!=null,"round details entry");recordButton?.onClick.Invoke();yield return new WaitForSecondsRealtime(.25f);yield return Capture("detail-540");
  Click("Button 返回统计");yield return new WaitForSecondsRealtime(.2f);Click("Button 导出试玩数据");yield return new WaitForSecondsRealtime(.25f);Check(!string.IsNullOrEmpty(stats.LastExportDirectory)&&File.Exists(Path.Combine(stats.LastExportDirectory,"rounds.json"))&&File.Exists(Path.Combine(stats.LastExportDirectory,"rounds.csv"))&&File.Exists(Path.Combine(stats.LastExportDirectory,"levels.csv")),"export creates JSON and two CSV files");yield return Capture("export-540");
  File.WriteAllText(Path.Combine(dir,"result.json"),$"{{\"checks\":{checks},\"failures\":{failures},\"errors\":{errors}}}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
AppUI app;
```
```csharp
GameSession game;
```
```csharp
BalanceStatistics stats;
```
```csharp
string dir;
```
```csharp
int checks,failures,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
