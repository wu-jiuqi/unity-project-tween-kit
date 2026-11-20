# VictorySpeedR3QA · VictorySpeedR3QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/VictorySpeedR3QA.cs`
文件 SHA-256：`aacca10826a97db6f9714ddb08c8bb7b200faceb13649fecd81e2f9ee535f8bc`
[原始源码快照](../source/Assets/Scripts/VictorySpeedR3QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Level · L10

方法 SHA-256：`fc9676a3ad0330d328ff8bfaeb69c51ba6f495b7e35fcf3b15838e4555c7d4ad`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictorySpeedR3QA.Level()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/VictorySpeedR3QA.cs:15` · `VictorySpeedR3QA.Start` → `Level()`
- `Assets/Scripts/VictorySpeedR3QA.cs:22` · `VictorySpeedR3QA.Start` → `Level()`
- `Assets/Scripts/VictorySpeedR3QA.cs:22` · `VictorySpeedR3QA.Start` → `Level()`

```csharp
IEnumerator Level(){app.QAStartLevel(0);yield return app.QAWaitForLevel();for(int i=0;i<150;i++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.02f);if(i>10&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}g=app.Session;g.QAIgnoreFocusLoss=true;g.SetPaused(false);Check(g.VictoryPlaybackSpeed==1&&!g.VictoryPlaybackPending,"new level starts at ordinary speed");}
```

### Capture · L12

方法 SHA-256：`8984e384f2a90bf2f7fc197baff6ccf05e897434f6ec8585fd71dcd6bff5e0fe`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictorySpeedR3QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/VictorySpeedR3QA.cs:16` · `VictorySpeedR3QA.Start` → `Capture("early-2x-hint")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### Start · L13

方法 SHA-256：`39eff0edb0a90b190c0417464d5a14355dc0d8aa5667d18a12b9dc7de893d050`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictorySpeedR3QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.targetFrameRate=60;QualitySettings.vSyncCount=0;Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();float normal=0;int expectedScore=0;
  for(int mode=0;mode<4;mode++){
   yield return Level();g.QASpeedFixture();g.QAFinaleGoal(5,1800);float begin=Time.realtimeSinceStartup;while(g.IsPlaying&&Time.realtimeSinceStartup-begin<1)yield return null;while(app.QAGoalCelebration!=null&&Time.realtimeSinceStartup-begin<3)yield return null;Check(g.ActiveBalls==0&&g.PendingBurstBalls==0&&g.VictoryPlaybackPending,"click is enabled at ball cleanup "+mode);var preparation=app.QAVictoryPreparation;var control=preparation!=null?preparation.GetComponent<VictoryPlaybackControl>():null;Check(control!=null&&control.Hint.Contains("点击屏幕"),"Chinese hint visible during field settlement "+mode);
   if(mode==1){Check(control.GetComponentInChildren<TMPro.TMP_Text>().color.r>.9f,"hint stays bright and readable on dark backing");Click(control,PointerEventData.InputButton.Right);Check(g.VictoryPlaybackSpeed==1,"right click does not accelerate");Click(control);Click(control);Check(g.VictoryPlaybackSpeed==2&&control.Hint.Contains("已开启2倍速"),"early click enables persistent 2x without stacking");yield return Capture("early-2x-hint");}
   while(app.QAVictoryFinale==null&&Time.realtimeSinceStartup-begin<15)yield return null;var finale=app.QAVictoryFinale;Check(finale!=null,"field effects and refill finish "+mode);if(finale==null)continue;control=finale.GetComponent<VictoryPlaybackControl>();Check(g.VictoryPlaybackSpeed==(mode==1?2:1),"speed preserved across settlement stage change "+mode);if(mode==2){while(finale!=null&&finale.ThrownCount<2)yield return null;Click(control);Check(g.VictoryPlaybackSpeed==2,"can accelerate midway through throwing");}
   if(mode==3){while(finale!=null&&!finale.BoardCleared)yield return null;Click(control);Check(g.VictoryPlaybackSpeed==2,"can accelerate during flash and star enlargement");}
   float sample=finale.Elapsed,start=Time.realtimeSinceStartup;yield return new WaitForSecondsRealtime(.1f);if(app.QAVictoryFinale!=null){float ratio=(finale.Elapsed-sample)/(Time.realtimeSinceStartup-start);Check(Mathf.Abs(ratio-g.VictoryPlaybackSpeed)<.45f,"timeline advances at requested real time rate "+mode+" ratio="+ratio);}
   while(app.QAVictoryPage==null&&Time.realtimeSinceStartup-begin<18)yield return null;float duration=Time.realtimeSinceStartup-begin;Check(app.QAVictoryPage!=null&&g.VictoryPlaybackSpeed==1&&!g.VictoryPlaybackPending,"acceleration ends when result page appears "+mode);Check(g.ScoreFinalized&&g.CompletionBonus==15000&&g.Shots==0,"acceleration preserves rewards and all five steps "+mode);Check(g.VictoryBoardSwept&&g.VictorySpecialsTriggered>0,"special effects and terminal clear not skipped "+mode);if(mode==0){normal=duration;expectedScore=g.Score;}else Check(g.Score==expectedScore,"same deterministic final score at accelerated rate "+mode);if(mode==1)Check(duration/normal>.35f&&duration/normal<.72f,"early acceleration approximately halves full presentation "+duration+"/"+normal);g.AccelerateVictoryPlayback();Check(g.VictoryPlaybackSpeed==1,"result page cannot retain settlement speed "+mode);Check(!app.GetComponentsInChildren<VictoryPlaybackControl>().Any(),"hint and click catcher removed on results "+mode);
  }
  yield return Level();g.QAFinaleGoal(17,1800);while(g.IsPlaying||app.QAGoalCelebration!=null)yield return null;g.AccelerateVictoryPlayback();Check(g.VictoryPlaybackSpeed==2,"restart fixture accelerates");yield return Level();Check(g.VictoryPlaybackSpeed==1&&!g.VictoryPlaybackPending,"restarting accelerated settlement resets clock");File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
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
```csharp
GameSession g;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
