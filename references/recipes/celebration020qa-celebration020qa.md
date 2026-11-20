# Celebration020QA · Celebration020QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Celebration020QA.cs`
文件 SHA-256：`ada4e40fdad6987a1caa114a269cabcd2c5d499eca9c1faf529023ed10345fc4`
[原始源码快照](../source/Assets/Scripts/Celebration020QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L21

方法 SHA-256：`13a27882af4529ccf9111b3d1d0c0388662c34c2ebeda044bfe9d3f57d8fcc63`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Celebration020QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Celebration020QA.cs:34` · `Celebration020QA.Start` → `Capture("mechanism-"+index)`
- `Assets/Scripts/Celebration020QA.cs:52` · `Celebration020QA.Start` → `Capture("goal-celebration")`
- `Assets/Scripts/Celebration020QA.cs:57` · `Celebration020QA.Start` → `Capture("victory-result")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,image);File.WriteAllBytes(dir+"/"+name+".png",image.EncodeToPNG());Destroy(image);rt.Release();Destroy(rt);}
```

### Level · L22

方法 SHA-256：`0c60faeaa6fa178585c64a24dcbb0681b258c35eb35d9fcd31d87bf97c7d0630`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Celebration020QA.Level(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Celebration020QA.cs:27` · `Celebration020QA.Start` → `Level()`
- `Assets/Scripts/Celebration020QA.cs:37` · `Celebration020QA.Start` → `Level(4)`
- `Assets/Scripts/Celebration020QA.cs:40` · `Celebration020QA.Start` → `Level()`
- `Assets/Scripts/Celebration020QA.cs:43` · `Celebration020QA.Start` → `Level()`
- `Assets/Scripts/Celebration020QA.cs:47` · `Celebration020QA.Start` → `Level()`
- `Assets/Scripts/Celebration020QA.cs:59` · `Celebration020QA.Start` → `Level()`
- `Assets/Scripts/Celebration020QA.cs:59` · `Celebration020QA.Start` → `Level(4)`

```csharp
IEnumerator Level(int index=0){if(game!=null)game.enabled=true;app.QAStartLevel(index);yield return app.QAWaitForLevel();game=app.Session;game.QAIgnoreFocusLoss=true;for(int n=0;n<150;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.02f);if(n>8&&!app.AlphabetIntroActive&&!game.IsPaused)break;}game.SetPaused(false);Check(game.IsPlaying&&!game.IsPaused,"level loads and resumes "+index);}
```

### WaitResult · L23

方法 SHA-256：`cdc545efb395a199a84dd845298a3a71736929542c21e8fd2f1474e953254f9c`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Celebration020QA.WaitResult()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Celebration020QA.cs:55` · `Celebration020QA.Start` → `WaitResult()`

```csharp
IEnumerator WaitResult(){float end=Time.realtimeSinceStartup+24;while(app.QAVictoryPage==null&&Time.realtimeSinceStartup<end)yield return null;Check(app.QAVictoryPage!=null,"reward sequence reaches the victory page");}
```

### Start · L24

方法 SHA-256：`90c961ff259d11647d9b108a3fe8f037d18889476a051399cba01f60d2372719`
时间来源：Time.realtimeSinceStartup, Time.timeScale, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Celebration020QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");int fps=Array.IndexOf(args,"--qa-fps");Application.targetFrameRate=fps>=0?int.Parse(args[fps+1]):60;QualitySettings.vSyncCount=0;
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception||t==LogType.Assert){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();yield return Level();
  foreach(string key in new[]{"ribbon","star","confetti","halo"}){var sprite=GoalCelebrationArt.Get(key);Check(sprite!=null&&sprite.texture.width>0,"generated transparent art bound "+key);}
  foreach(int index in new[]{0,4,5,10,17,20,30}){
   app.QAGardenLesson(index);yield return null;var view=app.QAAlphabetIntro;
   Check(view!=null&&view.Ready&&view.UsesDimMechanismLayout&&view.LayoutFits,"mechanism ready immediately and fits safe area "+index);
   Check(view.GetComponentsInChildren<Image>().All(i=>!i.name.Contains("lesson card")),"mechanism has no large painted panel "+index);
   Check(view.GetComponentsInChildren<OpeningRuleDemo>().Length==1&&view.GetComponentInChildren<LessonCollectionFlight>()!=null,"mechanism keeps independent gameplay demonstration "+index);
   if(index==4||index==20||index==30){yield return new WaitForSecondsRealtime(.55f);yield return Capture("mechanism-"+index);}
   var button=view.GetComponentsInChildren<Button>().First(b=>b.name=="Mechanism understood immediately");button.onClick.Invoke();yield return new WaitForSecondsRealtime(.22f);Check(app.QAAlphabetIntro==null,"understood closes without waiting for animation "+index);
  }
  yield return Level(4);game.QASevenClear();foreach(int kind in SevenSpecialRules.Kinds){var tool=game.QASevenTool(kind,kind,8,kind%5);tool.Waiting=true;}
  yield return new WaitForSecondsRealtime(.15f);Check(!app.AlphabetIntroActive&&!game.IsPaused,"all five first appearances have no page or teaching pause");Check(game.TryShoot(Vector2.up),"all five new specials do not gate firing");
  // Singles keep moving; combinations retain only a tiny, rate-limited stop.
  yield return Level();game.enabled=false;game.QASevenClear();var target=game.QALightFeedbackHealthBrick(4,3);var source=game.QASevenTool(2,4,4,0);int stops=game.CombatFeel.HitStops;game.ActivateChainTool(source,Vector2.up);float until=Time.realtimeSinceStartup+.42f,minScale=1;
  while(Time.realtimeSinceStartup<until){minScale=Mathf.Min(minScale,Time.timeScale);yield return null;}
  Check(game.CombatFeel.HitStops==stops&&minScale>=.5f&&minScale<1,"single uses gentle slow motion without a hard stop");Check(target.CurrentHealth==15,"lighter single preserves ten damage hp="+target.CurrentHealth);Check(Mathf.Approximately(Time.timeScale,1),"single restores normal time");
  yield return Level();game.enabled=false;game.QASevenClear();target=game.QALightFeedbackHealthBrick(4,3);source=game.QASevenTool(6,4,4,0);game.QASevenTool(2,5,5,3);stops=game.CombatFeel.HitStops;game.ActivateChainTool(source,Vector2.up);yield return new WaitForSecondsRealtime(.45f);
  Check(game.CombatFeel.HitStops==stops+1&&game.CombatFeel.LastStop<=.0251f,"combo has exactly one stop capped at 25 ms");Check(target.CurrentHealth==15&&game.SevenCombos==1,"lighter combination preserves damage and one activation hp="+target.CurrentHealth+" combos="+game.SevenCombos);
  stops=game.CombatFeel.HitStops;for(int n=0;n<4;n++){game.CombatFeel.Power(Vector2.zero,.025f);yield return new WaitForSecondsRealtime(.025f);}Check(game.CombatFeel.HitStops==stops,"nearby repeated effects do not stack pauses");
  // Reward sequencing, duplicate outcome protection and suspension.
  yield return Level();game.QASpeedFixture();int coins=app.Profile.Coins;game.QAFinaleGoal(5,1800);int score=game.Score;
  until=Time.realtimeSinceStartup+2;while(app.QAGoalCelebration==null&&Time.realtimeSinceStartup<until)yield return null;
  var celebration=app.QAGoalCelebration;Check(celebration!=null&&game.IsPaused&&!game.VictorySettlementActive,"celebration starts before any reward preparation");
  Check(app.QAVictoryPreparation==null&&app.QAVictoryFinale==null&&!game.ScoreFinalized&&app.Profile.Coins==coins,"no reward page, score finalization or coins during celebration");
  app.SendMessage("OnOutcome",true);Check(app.GetComponentsInChildren<GoalCelebration>().Length==1,"duplicate win event does not start another celebration");
  yield return new WaitForSecondsRealtime(.35f);Check(celebration!=null&&celebration.LayoutFits&&game.Score==score,"generated celebration fits and score stays fixed");yield return Capture("goal-celebration");
  if(celebration!=null){celebration.SendMessage("OnApplicationPause",true);float elapsed=celebration.Elapsed;yield return new WaitForSecondsRealtime(.18f);Check(Mathf.Abs(celebration.Elapsed-elapsed)<.001f,"background suspension freezes celebration clock");celebration.SendMessage("OnApplicationPause",false);}
  until=Time.realtimeSinceStartup+3;while(app.QAGoalCelebration!=null&&Time.realtimeSinceStartup<until)yield return null;
  Check(game.VictorySettlementActive&&app.QAVictoryPreparation!=null,"reward preparation starts after celebration completes");game.AccelerateVictoryPlayback();yield return WaitResult();
  Check(game.ScoreFinalized&&game.VictoryBoardSwept&&game.CompletionBonus==15000&&game.Shots==0,"celebration preserves five spare steps, score and terminal sweep");
  coins=app.Profile.Coins;score=game.Score;app.SendMessage("OnOutcome",true);yield return null;Check(app.Profile.Coins==coins&&game.Score==score,"duplicate outcome cannot pay twice");yield return Capture("victory-result");
  // Replacing the board cancels the old callback.
  yield return Level();game.QAFinaleGoal(2,1800);until=Time.realtimeSinceStartup+2;while(app.QAGoalCelebration==null&&Time.realtimeSinceStartup<until)yield return null;int generation=game.Generation;yield return Level(4);yield return new WaitForSecondsRealtime(1.3f);Check(game.Generation!=generation&&game.IsPlaying&&app.QAGoalCelebration==null&&app.QAVictoryPreparation==null&&app.QAVictoryPage==null,"restart during celebration never settles the replacement board");
  // Legacy committed keys remain harmless and normal progress stays readable.
  var store=new Store();var profile=LocalProfile.Load(store);profile.MarkOpeningIntro(0);store.Json=store.Json.Replace("opening-dim-v3/intro/0","special-brick-intro-v2/5");profile=LocalProfile.Load(store);int oldCoins=profile.Coins;profile.MarkOpeningIntro(17);var loaded=LocalProfile.Load(store);Check(loaded.Coins==oldCoins&&new[]{4,5,17}.All(loaded.HasOpeningIntro),"legacy keys coexist with new tier-aware mechanism flags");
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+",\"fps\":"+Application.targetFrameRate+"}");Application.Quit(failures==0&&errors==0?0:3);
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
string dir;
```
```csharp
int checks,failures,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
