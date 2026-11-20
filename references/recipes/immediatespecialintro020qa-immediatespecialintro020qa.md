# ImmediateSpecialIntro020QA · ImmediateSpecialIntro020QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ImmediateSpecialIntro020QA.cs`
文件 SHA-256：`42a79443f8e4870990385a0f55903748585cb3ceec5ed27f50ce97ebe2a32a7a`
[原始源码快照](../source/Assets/Scripts/ImmediateSpecialIntro020QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Load · L7

方法 SHA-256：`877e1b434021af0c746dca10f08ede87d2f29a7844bd0b0200379cd17023bd3f`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ImmediateSpecialIntro020QA.Load(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ImmediateSpecialIntro020QA.cs:9` · `ImmediateSpecialIntro020QA.Start` → `Load(0)`
- `Assets/Scripts/ImmediateSpecialIntro020QA.cs:17` · `ImmediateSpecialIntro020QA.Start` → `Load(4)`

```csharp
IEnumerator Load(int level){app.QAStartLevel(level);yield return app.QAWaitForLevel();app.Session.QAIgnoreFocusLoss=true;for(int n=0;n<120;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.02f);if(n>10&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}app.Session.SetPaused(false);}
```

### DelayResult · L8

方法 SHA-256：`fdb01a3352be4b5ec2b42062ff05eeb6d910fece030030e338fd7df118cbe8e6`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ImmediateSpecialIntro020QA.DelayResult(string)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
IEnumerator DelayResult(string page){if(page=="result-win"){yield return new WaitForSecondsRealtime(.4f);resultReady=true;}}
```

### Start · L9

方法 SHA-256：`d02d6450db25fe6f893e80de1ab6819e6408a3ff548824e8ac778a55a80c1be9`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ImmediateSpecialIntro020QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.targetFrameRate=60;QualitySettings.vSyncCount=0;Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();yield return Load(0);var g=app.Session;
  g.QASevenClear();for(int r=2;r<7;r++)for(int c=1;c<8;c++)g.QAThirdNormal(c,r,c%5);
  Check(g.TryShoot(Vector2.up),"live volley accepted before first special");yield return new WaitForSecondsRealtime(.035f);
  int spawned=g.SpawnCount,shots=g.Shots;
  foreach(int kind in SevenSpecialRules.Kinds){var tool=g.QASevenTool(kind,kind,8,kind%5);tool.Waiting=true;}
  yield return new WaitForSecondsRealtime(.35f);
  Check(!app.AlphabetIntroActive&&!g.IsPaused,"all five first appearances leave the current volley running");
  Check(g.SpawnCount>spawned&&g.Shots==shots,"first appearances neither gate spawning nor charge extra steps");
  yield return Load(4);g=app.Session;GameContent.Ready=page=>page!="result-win"||resultReady;GameContent.Page=DelayResult;g.QAFinaleGoal(7,1800);yield return new WaitForSecondsRealtime(.15f);
  Check(!g.IsPlaying&&app.QAGoalCelebration!=null&&app.QAVictoryPreparation==null,"cold result resources do not postpone goal celebration");
  float deadline=Time.realtimeSinceStartup+20;while(app.QAVictoryPage==null&&Time.realtimeSinceStartup<deadline)yield return null;Check(app.QAVictoryPage!=null&&g.ScoreFinalized&&g.Shots==0,"deferred result resources resume full victory and rewards");
  GameContent.Ready=null;GameContent.Page=null;
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
```csharp
bool resultReady;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
