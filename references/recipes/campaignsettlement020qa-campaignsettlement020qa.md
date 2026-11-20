# CampaignSettlement020QA · CampaignSettlement020QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/CampaignSettlement020QA.cs`
文件 SHA-256：`0e3fd71931c0a455ffa3ff0e65d1c22f93bc008fe46bf407962c3cd915028449`
[原始源码快照](../source/Assets/Scripts/CampaignSettlement020QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L7

方法 SHA-256：`b86eafc4e1b1b9344a56b9af21a73c631cdc19e8024ac76fbc9ec16dd42f3fd6`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CampaignSettlement020QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.targetFrameRate=60;QualitySettings.vSyncCount=0;Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();
  int selected=Array.IndexOf(args,"--qa-level");var levels=selected<0?Enumerable.Range(0,OpeningCampaign.Count):new[]{int.Parse(args[selected+1])-1};
  foreach(int level in levels){
   app.QAStartLevel(level);yield return app.QAWaitForLevel();var g=app.Session;g.QAIgnoreFocusLoss=true;for(int n=0;n<120;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.02f);if(n>10&&!app.AlphabetIntroActive&&!g.IsPaused)break;}g.SetPaused(false);
   Check(g.TryShoot(new Vector2(.15f,1)),"volley starts level="+(level+1));yield return new WaitForSecondsRealtime(.65f);g.TryShoot(new Vector2(-.15f,1));yield return new WaitForSecondsRealtime(.12f);
   g.QAFinaleGoal(1,78200);float started=Time.realtimeSinceStartup;
   while((g.IsPlaying||app.QAGoalCelebration!=null)&&Time.realtimeSinceStartup-started<4)yield return null;g.AccelerateVictoryPlayback();
   while(app.QAVictoryPage==null&&Time.realtimeSinceStartup-started<20)yield return null;
   bool success=app.QAVictoryPage!=null;Check(success,"complete victory reaches result level="+(level+1));Check(success&&g.ScoreFinalized&&g.Shots==0&&g.VictoryBoardSwept,"reward and terminal cleanup level="+(level+1));
   if(!success)File.AppendAllText(dir+"/blocked.txt","phase="+(app.QAVictoryPreparation!=null?app.QAVictoryPreparation.Phase:-1)+" "+g.VictorySettlementStatus+"\n");
   File.WriteAllText(dir+"/progress.txt","completedLevel="+(level+1)+" checks="+checks+" failures="+failures+" errors="+errors);
  }
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
string dir;
```
```csharp
int checks,failures,errors;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
