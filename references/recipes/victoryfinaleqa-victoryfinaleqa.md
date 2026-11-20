# VictoryFinaleQA · VictoryFinaleQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/VictoryFinaleQA.cs`
文件 SHA-256：`73e3cc29e67ccbd1045018477ebcd57dc6096f11103bc9cfb276457bd9d8243c`
[原始源码快照](../source/Assets/Scripts/VictoryFinaleQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L15

方法 SHA-256：`8984e384f2a90bf2f7fc197baff6ccf05e897434f6ec8585fd71dcd6bff5e0fe`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryFinaleQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/VictoryFinaleQA.cs:20` · `VictoryFinaleQA.Start` → `Capture("gameplay-star-charge")`
- `Assets/Scripts/VictoryFinaleQA.cs:27` · `VictoryFinaleQA.Start` → `Capture("after-all-field-specials")`
- `Assets/Scripts/VictoryFinaleQA.cs:34` · `VictoryFinaleQA.Start` → `Capture("five-bombs-holding-and-score")`
- `Assets/Scripts/VictoryFinaleQA.cs:35` · `VictoryFinaleQA.Start` → `Capture("score-and-stars-complete-before-blast")`
- `Assets/Scripts/VictoryFinaleQA.cs:36` · `VictoryFinaleQA.Start` → `Capture("blast-"+moves+"-colors")`
- `Assets/Scripts/VictoryFinaleQA.cs:37` · `VictoryFinaleQA.Start` → `Capture("three-stars-enlarged")`
- `Assets/Scripts/VictoryFinaleQA.cs:43` · `VictoryFinaleQA.Start` → `Capture("result-three-stars")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### StartLevel · L16

方法 SHA-256：`bdfd41411df60cb2bfce18b7f21a8dffb5fe22cc74a6859c9e1db54fc1cbede1`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryFinaleQA.StartLevel()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/VictoryFinaleQA.cs:20` · `VictoryFinaleQA.Start` → `StartLevel()`
- `Assets/Scripts/VictoryFinaleQA.cs:22` · `VictoryFinaleQA.Start` → `StartLevel()`
- `Assets/Scripts/VictoryFinaleQA.cs:45` · `VictoryFinaleQA.Start` → `StartLevel()`

```csharp
IEnumerator StartLevel(){app.QAStartLevel(0);yield return app.QAWaitForLevel();for(int n=0;n<90;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.02f);if(n>8&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}g=app.Session;g.QAIgnoreFocusLoss=true;g.SetPaused(false);}
```

### Start · L17

方法 SHA-256：`6acec84958a9b62eb14a3f9eb8e9d6964b758fb501597ce8d70270417f7b2776`
时间来源：Time.frameCount, Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryFinaleQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");int fps=Array.IndexOf(args,"--qa-fps");Application.targetFrameRate=fps<0?60:int.Parse(args[fps+1]);QualitySettings.vSyncCount=0;Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();
  foreach(int c in new[]{1,2,3,4,5})for(int f=0;f<16;f++)Check(VictoryBombArt.Frame(c,f)!=null,"authored bomb sequence "+c+"/"+f);
  yield return StartLevel();Check(app.GetComponentInChildren<StreakTrialHUD>()==null,"bottom tools and hints not displayed");var meter=app.GetComponentInChildren<BattleScoreMeter>();g.QAFinaleScore(18000);yield return null;float start=meter.Charge(0).Fill;yield return new WaitForSecondsRealtime(.12f);Check(meter.Charge(0).Fill<=start+.01f,"starlight travels before gold filling");yield return new WaitForSecondsRealtime(.23f);Check(meter.Charge(0).Fill>start&&meter.Charge(0).Fill<meter.Charge(0).Target,"star fills progressively");Check(meter.Charge(0).EdgeVisible,"gold white particle edge marks partial fill");yield return Capture("gameplay-star-charge");yield return new WaitForSecondsRealtime(.5f);Check(meter.ChargeSettled,"normal gameplay charge reaches target");
  foreach(int moves in (Array.IndexOf(args,"--qa-moves")>=0?new[]{int.Parse(args[Array.IndexOf(args,"--qa-moves")+1])}:new[]{0,1,2,3,4,5,17})){
   yield return StartLevel();meter=app.GetComponentInChildren<BattleScoreMeter>();var original=Enumerable.Range(0,3).Select(i=>(RectTransform)meter.Charge(i).transform).ToArray();var positions=g.CollectionBricks.Where(b=>b.Alive).ToDictionary(b=>b.Id,b=>b.transform.position);
   Check(g.TryShoot(Vector2.up),"live ball fixture launches "+moves);yield return new WaitForSecondsRealtime(.04f);var ball=g.QARefillFirstBall();int spawned=g.SpawnCount;
   if(moves==3){yield return new WaitForSecondsRealtime(.55f);g.QAFinaleDeferPending();spawned=g.SpawnCount;Check(g.VictoryDeferredGroups==1,"group completing during target flight retains owed rewards");}if(moves==5)g.QAFinaleSpecialRound();g.QAFinaleGoal(moves,1800);int frame=Time.frameCount;float begin=Time.realtimeSinceStartup;while(g.IsPlaying&&Time.realtimeSinceStartup-begin<1)yield return null;
   Check(!g.IsPlaying&&!g.ChainCanLaunch&&Time.frameCount-frame<=2,"task completion stops input immediately "+moves);while(app.QAGoalCelebration!=null&&Time.realtimeSinceStartup-begin<3)yield return null;Check(g.ActiveBalls==0&&g.PendingBurstBalls==0&&g.ChainLiveGroups==0,"balls cleared and final group closed "+moves);
   while(app.QAVictoryFinale==null&&Time.realtimeSinceStartup-begin<20)yield return null;
   if(moves==5){Check(g.VictoryGroupsClosed==1&&g.VictorySpecialsTriggered>=5,"final group rewards delivered then all specials triggered");Check(g.VictoryClearScore>0&&g.Score-g.CreditedCompletionBonus-g.VictoryClearScore==1800,"clear score separated from play and remaining steps");Check(g.Score>1800+moves*ScoreRules.SpareShot,"real post goal special damage adds score");yield return Capture("after-all-field-specials");}
   if(app.QAVictoryFinale==null)File.AppendAllText(dir+"/settlement-state.txt","phase="+(app.QAVictoryPreparation!=null?app.QAVictoryPreparation.Phase:-1)+" replacements="+g.ChainPendingReplacements+" "+g.QARefillStatus()+"\n");if(moves==3)Check(g.VictoryGroupsClosed==1&&g.VictorySpecialsTriggered>0&&g.VictoryDeferredGroups==0,"deferred group restores and activates rewards exactly once");
   var finale=app.QAVictoryFinale;Check(finale!=null&&finale.BombCount==Mathf.Min(moves,5),"correct merged bomb count "+moves);if(finale==null)continue;
   int finalScore=g.Score,coins=app.Profile.Coins;bool held=false,pre=false,swept=false,peak=false,monotonic=true;int lastScore=finale.DisplayScore;
   float deadline=Time.realtimeSinceStartup+15;
   while(app.QAVictoryFinale!=null&&Time.realtimeSinceStartup<deadline){
    finale=app.QAVictoryFinale;monotonic&=finale.DisplayScore>=lastScore&&finale.DisplayScore<=finalScore;lastScore=finale.DisplayScore;
    if(!held&&finale.Elapsed>=.9f&&finale.Elapsed<finale.BlastAt){held=true;Check(!g.ChainCanLaunch&&g.ActiveBalls==0,"input stays stopped while supply can continue "+moves);Check(ball==null||!ball.Active,"live ball cleared "+moves);Check(g.SpawnCount==spawned,"pending volley stopped "+moves);app.QARepeatFinaleWin();Check(app.Profile.Coins==coins,"duplicate win cannot repeat rewards during finale "+moves);if(moves==5)yield return Capture("five-bombs-holding-and-score");}
    if(!pre&&finale.Elapsed>=finale.BlastAt-.06f&&finale.Elapsed<finale.BlastAt){pre=true;Check(finale.ThrownCount==finale.BombCount&&g.Shots==0,"every bomb thrown and every remaining step converted before blast "+moves);Check(finale.DisplayScore==finalScore&&meter.ChargeSettled,"score and star fill finish before simultaneous blast "+moves);if(moves==5)yield return Capture("score-and-stars-complete-before-blast");}
    if(!swept&&finale.Elapsed>=finale.BlastAt+.32f){swept=true;Check(g.VictoryBoardSwept&&g.CollectionBricks.All(b=>!b.Alive)&&g.ChainTools.Count==0&&g.RemainingTargets==0,"one burst clears all board objects and logical targets "+moves);Check(g.Score==finalScore&&app.Profile.Coins==coins,"terminal sweep adds no duplicate score or loot "+moves);if(moves>0&&moves<=5)yield return Capture("blast-"+moves+"-colors");}
    if(!peak&&finale.Elapsed>=finale.BlastAt+1.36f){peak=true;if(moves==5)yield return Capture("three-stars-enlarged");}
    yield return null;
   }
   Check(monotonic,"score animation is monotonic and bounded "+moves);Check(app.QAVictoryFinale==null,"finale finishes "+moves);var page=app.QAVictoryPage;Check(page!=null,"success page appears after enlarged stars "+moves);if(page==null)continue;
   yield return new WaitForSecondsRealtime(2.4f);Check(page.Ready,"buttons wait until presentation completes "+moves);Check(!page.GetComponentsInChildren<TMP_Text>().Any(t=>t.text.Contains("挑战成功")),"stars replace challenge success headline "+moves);
   for(int i=0;i<3;i++)Check(page.HeaderStars[i]==original[i],"actual hanging star transfers into result slot "+moves+"/"+i);
   app.QARepeatFinaleWin();Check(app.Profile.Coins==coins,"result reentry grants reward once "+moves);Check(!page.GetComponentsInChildren<TMP_Text>().Any(t=>t.text.Contains("位对手")||t.text.Contains("花园联赛 第")),"league text removed from reward gap "+moves);for(int i=0;i<3;i++)Check(Mathf.Abs(page.HeaderStars[i].sizeDelta.x-(i==1?207:184))<.1f,"larger result star "+moves+"/"+i);var idleScale=page.HeaderStars[0].localScale;yield return new WaitForSecondsRealtime(.15f);Check((page.HeaderStars[0].localScale-idleScale).magnitude>.01f,"result stars reuse bomb jelly motion "+moves);if(moves==5)yield return Capture("result-three-stars");
  }
  yield return StartLevel();g.enabled=false;var colorProbe=g.QAFiveColorProbe();Check(colorProbe[0]==100,"one hundred actual five ball groups remain internally same color");Check(colorProbe[1]==5,"all five group colors sampled");Check(colorProbe[2]==500&&colorProbe[3]==500,"five hundred balls and trails match group color including reuse");File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
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
GameSession g;
```
```csharp
string dir;
```
```csharp
int checks,failures,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
