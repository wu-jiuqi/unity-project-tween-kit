# ResultNoSplashF4QA · ResultNoSplashF4QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ResultNoSplashF4QA.cs`
文件 SHA-256：`ebda6774f3bc7f56f6970578a5860c53a92218626b2c0f4d20eb6e77965a1b02`
[原始源码快照](../source/Assets/Scripts/ResultNoSplashF4QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Page · L7

方法 SHA-256：`527cc33dd95336f2496e6f0a37a26a6380a37fe40b399607e1c9d0cd7431f16b`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ResultNoSplashF4QA.Page(string)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
IEnumerator Page(string page){if(page!="result-win")yield break;starts++;if(failOnce&&starts==1)throw new InvalidOperationException("Injected offline result request");while(!release)yield return null;ready=true;}
```

### Level · L8

方法 SHA-256：`c9e459483115fe4d0166e72d049eb18747193b2b98dc9ff8317f16da54582f23`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ResultNoSplashF4QA.Level(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ResultNoSplashF4QA.cs:12` · `ResultNoSplashF4QA.Start` → `Level(5)`
- `Assets/Scripts/ResultNoSplashF4QA.cs:14` · `ResultNoSplashF4QA.Start` → `Level(11)`

```csharp
IEnumerator Level(int n){GameContent.Ready=null;GameContent.Page=null;app.QAStartLevel(n);yield return app.QAWaitForLevel();app.Session.QAIgnoreFocusLoss=true;for(int i=0;i<150;i++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.02f);if(i>12&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}app.Session.SetPaused(false);}
```

### Result · L10

方法 SHA-256：`409e35c2a8a4120dd2f77677ca1684963cdb09a294b97870822f6ad37d6810d5`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ResultNoSplashF4QA.Result()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ResultNoSplashF4QA.cs:13` · `ResultNoSplashF4QA.Start` → `Result()`
- `Assets/Scripts/ResultNoSplashF4QA.cs:15` · `ResultNoSplashF4QA.Start` → `Result()`

```csharp
IEnumerator Result(){float deadline=Time.realtimeSinceStartup+20;while(app.QAVictoryPage==null&&Time.realtimeSinceStartup<deadline){app.Session.AccelerateVictoryPlayback();yield return null;}Check(app.QAVictoryPage!=null&&app.Session.ScoreFinalized,"network completion resumes full earned victory");}
```

### Start · L11

方法 SHA-256：`05d3484d4588e4bcaf757f1f20857b310b2d48c6328dac45e08331257c6029fa`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ResultNoSplashF4QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);Application.targetFrameRate=60;QualitySettings.vSyncCount=0;Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();
  yield return Level(5);Fixture(false);int coins=app.Profile.Coins;app.Session.QAFinaleGoal(2,40000);float deadline=Time.realtimeSinceStartup+5;while(app.PendingMenuPage!="result-win"&&Time.realtimeSinceStartup<deadline)yield return null;
  Check(app.PendingMenuPage=="result-win","fixture is waiting for actual result art");Check(app.GetComponentInChildren<LoadingPresentation>()==null,"no second logo/cat/loading splash over earned board");Check(app.Profile.Coins==coins,"waiting cannot duplicate or pre-award wallet rewards");release=true;yield return Result();Check(starts==1&&app.Profile.Coins>coins,"one result request awards the completed round once");
  yield return Level(11);Fixture(true);release=true;app.Session.QAFinaleGoal(1,40000);deadline=Time.realtimeSinceStartup+5;Button retry=null;while(retry==null&&Time.realtimeSinceStartup<deadline){retry=app.GetComponentsInChildren<Button>().FirstOrDefault(b=>b.GetComponentInChildren<TMP_Text>()?.text=="重试");yield return null;}
  Check(retry!=null&&app.GetComponentInChildren<LoadingPresentation>()==null,"failed request exposes compact recovery without startup artwork");retry?.onClick.Invoke();yield return Result();Check(starts==2,"retry re-enters the original outcome without a request loop");
  GameContent.Ready=null;GameContent.Page=null;File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
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
string dir;
```
```csharp
int checks,failures,errors,starts;
```
```csharp
bool ready,release,failOnce;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
