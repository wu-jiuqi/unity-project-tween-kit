# LevelEntry88QA · LevelEntry88QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LevelEntry88QA.cs`
文件 SHA-256：`6981991a659906bea994f719687f8d27ae2f65c373ce68633dc99460f4630e98`
[原始源码快照](../source/Assets/Scripts/LevelEntry88QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L13

方法 SHA-256：`9b357968644bf969fca0ac2976e47d4c79b638e62886055ceba9ef7be84537c2`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`LevelEntry88QA.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(entering)result.maxFrameMs=Mathf.Max(result.maxFrameMs,Time.unscaledDeltaTime*1000);}
```

### Start · L15

方法 SHA-256：`66efd2c42958cbad54c473f1e4e61e60c76f70c0af99f1299d2b87b892290f83`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`LevelEntry88QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Exception||t==LogType.Error){result.errors++;result.messages.Add(m);}};
  Application.targetFrameRate=60;QualitySettings.vSyncCount=0;yield return new WaitForSecondsRealtime(3);
  app=FindFirstObjectByType<AppUI>();
  // Returning during a partially constructed board must dispose that generation.
  app.QAStartLevel(0);float cancelDeadline=Time.realtimeSinceStartup+5;
  while(!app.Session.IsPlaying&&Time.realtimeSinceStartup<cancelDeadline)yield return null;
  Check(app.Session.IsPlaying&&app.GetComponentInChildren<LoadingPresentation>()!=null,"cancellation fixture reaches partial board construction");
  app.QANavigate("home");yield return null;yield return null;
  Check(app.CurrentPage=="home"&&!app.Session.IsPlaying&&app.GetComponentInChildren<LoadingPresentation>()==null,"cancel entry returns home without a live partial board");
  for(int index=0;index<OpeningCampaign.Count;index++){
   app.QAStartLevel(index);float start=Time.realtimeSinceStartup,deadline=start+12;entering=true;int frames=0;
   while(Time.realtimeSinceStartup<deadline){
    var loading=app.GetComponentInChildren<LoadingPresentation>();
    // A disabled presentation used to make the reveal wait depend on an Update that never ran.
    if(index==0&&loading!=null)loading.enabled=false;
    if(loading==null&&app.CurrentPage=="battle"&&app.Session.LevelIndex==index)break;
    frames++;yield return null;
   }
   entering=false;float duration=Time.realtimeSinceStartup-start;result.maxEntrySeconds=Mathf.Max(result.maxEntrySeconds,duration);
   Check(app.CurrentPage=="battle"&&app.Session.LevelIndex==index&&app.GetComponentInChildren<LoadingPresentation>()==null,"level "+(index+1)+" loading handoff "+duration.ToString("F2")+"s frames="+frames);
   Check(app.Session.ChainOccupiedCells()>0,"level "+(index+1)+" board exists");
   for(int n=0;n<90;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();if(n>8&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;yield return null;}
   app.Session.QAIgnoreFocusLoss=true;Check(!app.Session.IsPaused,"level "+(index+1)+" accepts gameplay after introductions");result.levels++;
   app.QANavigate("home");yield return null;
   if(result.failures>0)break;
  }
  File.WriteAllText(dir+"/result.json",JsonUtility.ToJson(result,true));Application.Quit(result.failures+result.errors==0?0:3);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
Result result=new Result();
```
```csharp
string dir;
```
```csharp
bool entering;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
