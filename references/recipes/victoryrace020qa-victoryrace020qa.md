# VictoryRace020QA · VictoryRace020QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/VictoryRace020QA.cs`
文件 SHA-256：`8cad500639523b04a2796bc59a39ef510bb9380a6248ab26fe53d1ff8d8c2889`
[原始源码快照](../source/Assets/Scripts/VictoryRace020QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L7

方法 SHA-256：`5b64a91b1ddc9713e097a7a8c2970f57eebbac9bc527dcd14ce5c918a23d01a5`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryRace020QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};Application.targetFrameRate=60;QualitySettings.vSyncCount=0;yield return new WaitForSecondsRealtime(2);app=FindFirstObjectByType<AppUI>();
  int selected=Array.IndexOf(args,"--qa-scenario");int first=selected>=0?int.Parse(args[selected+1]):0;
  for(int scenario=first;scenario<(selected>=0?first+1:20);scenario++){
   int level=new[]{5,11,4}[scenario%3];app.QAStartLevel(level);yield return app.QAWaitForLevel();var g=app.Session;g.QAIgnoreFocusLoss=true;
   for(int n=0;n<100;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.02f);if(n>10&&!app.AlphabetIntroActive&&!g.IsPaused)break;}g.SetPaused(false);
   if(scenario>=18){g.QASevenClear();for(int row=0;row<9;row++)for(int col=0;col<9;col++){if(scenario==18)g.QASevenTool(2,col,row,(col+row)%5);else g.QASevenBrick(col,row,25,(col+row)%5,1,0,true);}if(scenario==18)g.QAVictoryUnplaceableReward();}
   else if(scenario<12){g.QASevenClear();
    for(int row=0;row<9;row++)for(int col=0;col<9;col++)if(!(row==7&&(col==3||col==4))&&!(row==2&&(col==0||col==8)))g.QASevenBrick(col,row,25,(col+row)%5);
    g.QASevenTool(3,0,2,0);g.QASevenTool(3,8,2,1);
    var a=g.QASevenTool(3,3,7,0);g.QASevenTool(scenario<6?3:2,4,7,1);g.ActivateChainTool(a,Vector2.up);
   }else{g.TryShoot(new Vector2((scenario-14)*.15f,1));}
   yield return new WaitForSecondsRealtime(new[]{.01f,.10f,.19f,.30f,.60f,1.1f}[scenario%6]);g.QAFinaleGoal(1,78200);
   if(scenario==19){float deadline=Time.realtimeSinceStartup+5;while(!g.VictorySpecialsReleased&&Time.realtimeSinceStartup<deadline)yield return null;g.QAVictoryUnplaceableReward();}
   float start=Time.realtimeSinceStartup;int last=-1;
   while(app.QAVictoryPage==null&&Time.realtimeSinceStartup-start<18){g.AccelerateVictoryPlayback();int second=(int)(Time.realtimeSinceStartup-start);if(second!=last&&second>=3){last=second;File.AppendAllText(dir+"/trace.txt","scenario="+scenario+" second="+second+" phase="+(app.QAVictoryPreparation!=null?app.QAVictoryPreparation.Phase:-1)+" "+g.VictorySettlementStatus+"\n");}yield return null;}
   bool ok=app.QAVictoryPage!=null&&g.ScoreFinalized&&(scenario<18||g.VictoryUnplacedRewards==0&&g.ChainActivationCount>=(scenario==18?82:1));checks++;if(!ok)failures++;File.AppendAllText(dir+"/checks.txt",(ok?"PASS ":"FAIL ")+"scenario="+scenario+" level="+(level+1)+" activations="+g.ChainActivationCount+" "+g.VictorySettlementStatus+"\n");File.WriteAllText(dir+"/progress.json",JsonUtility.ToJson(new Result{checks=checks,failures=failures,errors=errors}));
  }
  File.WriteAllText(dir+"/result.json",JsonUtility.ToJson(new Result{checks=checks,failures=failures,errors=errors}));Application.Quit(failures==0&&errors==0?0:3);
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
