# PrefetchReuse020QA · PrefetchReuse020QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/PrefetchReuse020QA.cs`
文件 SHA-256：`76ee780095a6f31f0f09bae1930d45d4a4419617324c9c3324683cf807190c77`
[原始源码快照](../source/Assets/Scripts/PrefetchReuse020QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Download · L11

方法 SHA-256：`bd5a90f5db476c46a332d6b205b19f3f39e88b3b98b3c48c240cf505b9d30edb`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.PrefetchReuse020QA.Download(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/PrefetchReuse020QA.cs:12` · `PrefetchReuse020QA.Fixture` → `Download("level-"+level)`
- `Assets/Scripts/PrefetchReuse020QA.cs:12` · `PrefetchReuse020QA.Fixture` → `Download("level-"+level)`

```csharp
IEnumerator Download(string key){if(key!=target)yield break;while(loading)yield return null;if(ready)yield break;starts++;loading=true;bool finished=false;try{float until=Time.realtimeSinceStartup+.45f;while(Time.realtimeSinceStartup<until)yield return null;ready=finished=true;}finally{loading=false;if(!finished)aborts++;}}
```

### Start · L13

方法 SHA-256：`e837feb492d01211e68c938fa85160547d06266836229a65d13ca8967228ce82`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.PrefetchReuse020QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.targetFrameRate=60;QualitySettings.vSyncCount=0;Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();
  Fixture("chapters");app.QAStartTrackedPrefetch("chapters");yield return new WaitForSecondsRealtime(.12f);app.QANavigate("chapters");float limit=Time.realtimeSinceStartup+2;while(app.CurrentPage!="chapters"&&Time.realtimeSinceStartup<limit)yield return null;Check(app.CurrentPage=="chapters"&&starts==1&&aborts==0,"opening map joins its in-flight preload without abort or duplicate download");
  GameContent.Ready=null;app.QANavigate("home");Fixture("chapters");app.QAStartTrackedPrefetch("chapters");yield return new WaitForSecondsRealtime(.12f);app.QANavigate("shop");yield return new WaitForSecondsRealtime(.1f);Check(aborts==1&&app.CurrentPage=="shop","unrelated navigation cancels idle work and takes priority");
  GameContent.Ready=null;app.QANavigate("home");Fixture("level-4");app.QAStartTrackedPrefetch(null,4);yield return new WaitForSecondsRealtime(.12f);app.QAStartLevel(4);yield return app.QAWaitForLevel();for(int n=0;n<120;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.02f);if(n>10&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}app.Session.QAIgnoreFocusLoss=true;Check(app.Session.LevelIndex==4&&starts==1&&aborts==0,"starting the prefetched level preserves its current request");
  Fixture("result-win");app.QAStartTrackedPrefetch("result-win");yield return new WaitForSecondsRealtime(.12f);app.Session.QAFinaleGoal(1,1800);limit=Time.realtimeSinceStartup+15;while(app.QAVictoryPage==null&&Time.realtimeSinceStartup<limit)yield return null;Check(app.QAVictoryPage!=null&&starts==1&&aborts==0,"victory joins result prefetch and completes rewards without restarting download");
  GameContent.Ready=null;GameContent.Page=null;GameContent.Level=null;GameContent.Prefetch=null;
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
int checks,failures,errors,starts,aborts;
```
```csharp
bool ready,loading;
```
```csharp
string target;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
