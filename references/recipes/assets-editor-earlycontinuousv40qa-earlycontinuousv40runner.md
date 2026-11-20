# EarlyContinuousV40Runner · EarlyContinuousV40QA

状态：`editor-tool`；交付：`source-evidence-only`。

源文件：`Assets/Editor/EarlyContinuousV40QA.cs`
文件 SHA-256：`0d28f099d1cff4179a93f78dbd8dc89235f78d3218fb581e7141cb5d4ca2aac6`
[原始源码快照](../source/Assets/Editor/EarlyContinuousV40QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L18

方法 SHA-256：`aa7b9044652b84a0ca355cb45bfdb7cd9b07dfdfafe03e58f0278e242bf47660`
时间来源：Time.fixedDeltaTime
曲线证据：查原式或下级调用
解析符号：`EarlyContinuousV40Runner.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var dir=Path.GetFullPath(Path.Combine(Application.dataPath,"../../../Builds/AlphabetCampaign-v40/EarlyContinuousSurvey"));Directory.CreateDirectory(dir);var report=new Report();Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){report.errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s+"\n");}};yield return null;yield return null;FindFirstObjectByType<AppUI>().QANavigate("home");yield return null;
  var game=new GameObject("Early continuous session").AddComponent<GameSession>();game.QAIgnoreFocusLoss=true;game.enabled=false;var plannerObject=new GameObject("Read-only angle planner");plannerObject.SetActive(false);var planner=plannerObject.AddComponent<AlphabetPhysicsSurvey>();var flags=BindingFlags.NonPublic|BindingFlags.Instance;typeof(AlphabetPhysicsSurvey).GetField("g",flags).SetValue(planner,game);var choose=typeof(AlphabetPhysicsSurvey).GetMethod("Choose",flags);var prior=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  for(int level=0;level<3;level++){game.StartLevel(level);var run=new Run{letter=AlphabetCampaign.Stage(level).letter,goal=game.JellyGoal[1],valid=true};yield return null;while(game.IsPlaying&&game.Shots>0){var direction=(Vector2)choose.Invoke(planner,new object[]{run.shots});if(!game.TryShoot(direction)){report.errors++;break;}run.shots++;for(int tick=0;tick<Mathf.CeilToInt(.66f/Time.fixedDeltaTime)&&game.IsPlaying;tick++)game.QAAdvanceOneStep();run.valid&=game.ChainGridValid(false);yield return null;}int settle=0;while(game.IsPlaying&&(game.ActiveBalls>0||game.PendingBurstBalls>0||!game.ChainGridReady||game.ChainEffectsPending)&&settle++<1800){game.QAAdvanceOneStep();if(settle%200==0)yield return null;}if(settle>=1800)report.errors++;run.won=game.CollectionWon;run.blue=game.JellyCollected[1];run.fish=game.Collected[0];run.toys=game.Collected[1];report.runs.Add(run);File.WriteAllText(Path.Combine(dir,"survey.json"),JsonUtility.ToJson(report,true));}
  game.StopLevel();Destroy(game.gameObject);Destroy(plannerObject);Physics2D.simulationMode=prior;SessionState.SetBool("Bounce.EarlyContinuous40",false);EditorApplication.Exit(report.errors==0?0:1);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
