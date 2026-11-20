# LevelFiveSettlementQA · LevelFiveSettlementQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LevelFiveSettlementQA.cs`
文件 SHA-256：`be7dc09f308fe85f9433b0f0174620193a2cf2f1fca5804b66c4c7637e037959`
[原始源码快照](../source/Assets/Scripts/LevelFiveSettlementQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L14

方法 SHA-256：`740421ffc3bed2839bb267b5322bdab57a9b2308519ab3ecfcb8b4c4c4c107ac`
时间来源：Time.frameCount, Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelFiveSettlementQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.targetFrameRate=60;QualitySettings.vSyncCount=0;Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();
  int selected=Array.IndexOf(args,"--qa-case");int firstCase=selected<0?0:int.Parse(args[selected+1]);int lastCase=selected<0?10:firstCase+1;
  for(int mode=firstCase;mode<lastCase;mode++){
   app.QAStartLevel(4);yield return app.QAWaitForLevel();var g=app.Session;g.QAIgnoreFocusLoss=true;
   for(int i=0;i<120;i++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.02f);if(i>10&&!app.AlphabetIntroActive&&!g.IsPaused)break;}g.SetPaused(false);
   if(mode>=4&&mode<8){
    // Remove the actual yarns and surrounding cells, preserving their arrival
    // callbacks and the live gravity paths at the instant the goal is earned.
    for(int batch=0;batch<3;batch++){
     var neighbours=g.QAOpeningYarns().SelectMany(y=>g.QAOpeningNeighbours(y)).Distinct().ToArray();g.QAOpeningBatch(neighbours);
     yield return new WaitForSecondsRealtime(.08f);
    }
   }
   Check(g.TryShoot(Vector2.up),"level five volley starts "+mode);yield return new WaitForSecondsRealtime(mode==0?.04f:.65f);
   if(mode>=8)g.QAVictoryLateFlowerWindup();else if(mode>=2)g.QAFinaleSpecialRound();g.QAFinaleGoal(7,39200);float start=Time.realtimeSinceStartup;
   while(g.IsPlaying&&Time.realtimeSinceStartup-start<2)yield return null;
   Check(g.VictorySettlementActive,"winning with live balls starts settlement "+mode);if(mode%2==1)g.AccelerateVictoryPlayback();
   while(app.QAVictoryPage==null&&Time.realtimeSinceStartup-start<30){if(Time.frameCount%120==0)File.AppendAllText(dir+"/state.txt","mode="+mode+" t="+(Time.realtimeSinceStartup-start)+" phase="+(app.QAVictoryPreparation!=null?app.QAVictoryPreparation.Phase:-1)+" replacements="+g.ChainPendingReplacements+" "+g.QARefillStatus()+"\n");yield return null;}
   Check(app.QAVictoryPage!=null,"level five reaches results "+mode);Check(g.ScoreFinalized&&g.Shots==0&&g.VictoryBoardSwept,"steps rewarded and board cleared "+mode);
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
int failures,errors,checks;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
