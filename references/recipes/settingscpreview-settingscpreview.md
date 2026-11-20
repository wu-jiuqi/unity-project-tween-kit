# SettingsCPreview · SettingsCPreview

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/SettingsCPreview.cs`
文件 SHA-256：`efb0e233b3df9a4607730a8db332f47a6cb85357163763b00d2f007c81e897b7`
[原始源码快照](../source/Assets/Scripts/SettingsCPreview.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L6

方法 SHA-256：`929db6a7c7e301207b3180ddf93a8ec963767986a7ff6cf3a036fc183d81da49`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SettingsCPreview.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  yield return new WaitForSecondsRealtime(3);var app=FindFirstObjectByType<AppUI>();var args=Environment.GetCommandLineArgs();int arg=Array.IndexOf(args,"--settings-c-preview");string page=arg+1<args.Length?args[arg+1]:"settings";app.QANavigate("home");
  if(page=="pause"||page=="restart"||page=="home"){
   app.QAStartLevel(20);yield return app.QAWaitForLevel();app.Session.QAIgnoreFocusLoss=true;float until=Time.realtimeSinceStartup+30;
   while(Time.realtimeSinceStartup<until){app.QAAdvanceAlphabetIntro();app.QABattleTour?.AdvanceForQA();app.QAAdvanceBoosterGuide();if(app.QAGoalEntrance==null&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;yield return null;}
  }
  var flags=BindingFlags.Instance|BindingFlags.NonPublic;string method=page=="help"?"OpenPlayerHelp71":page=="about"?"OpenPlayerAbout71":page=="audio"?"OpenAudioSettings":page=="pause"?"Pause":page=="restart"||page=="home"?"ConfirmLeaveRound71":"Settings";
  typeof(AppUI).GetMethod(method,flags).Invoke(app,method=="ConfirmLeaveRound71"?new object[]{page=="restart"}:null);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
