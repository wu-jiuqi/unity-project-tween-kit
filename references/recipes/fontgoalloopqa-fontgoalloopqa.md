# FontGoalLoopQA · FontGoalLoopQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/FontGoalLoopQA.cs`
文件 SHA-256：`b258e05cefc1a2b7bfd4aaa7188744046823423ade89e8c491272d43be062996`
[原始源码快照](../source/Assets/Scripts/FontGoalLoopQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L7

方法 SHA-256：`13a27882af4529ccf9111b3d1d0c0388662c34c2ebeda044bfe9d3f57d8fcc63`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FontGoalLoopQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/FontGoalLoopQA.cs:12` · `FontGoalLoopQA.Start` → `Capture("00-home-font")`
- `Assets/Scripts/FontGoalLoopQA.cs:22` · `FontGoalLoopQA.Start` → `Capture("goal-center-"+level)`
- `Assets/Scripts/FontGoalLoopQA.cs:23` · `FontGoalLoopQA.Start` → `Capture("goal-flight-"+level)`
- `Assets/Scripts/FontGoalLoopQA.cs:27` · `FontGoalLoopQA.Start` → `Capture("goal-header-"+level)`
- `Assets/Scripts/FontGoalLoopQA.cs:36` · `FontGoalLoopQA.Start` → `Capture("loop-page-"+index)`
- `Assets/Scripts/FontGoalLoopQA.cs:41` · `FontGoalLoopQA.Start` → `Capture("settings-actual-new-font")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,image);File.WriteAllBytes(dir+"/"+name+".png",image.EncodeToPNG());Destroy(image);rt.Release();Destroy(rt);}
```

### WaitPlaying · L8

方法 SHA-256：`3c44e7c62d7632d4cb2d1b30c02bb9c19897e10ed1ebc94ee8ab5aa343ca9559`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FontGoalLoopQA.WaitPlaying()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/FontGoalLoopQA.cs:27` · `FontGoalLoopQA.Start` → `WaitPlaying()`

```csharp
IEnumerator WaitPlaying(){float end=Time.realtimeSinceStartup+20;while(Time.realtimeSinceStartup<end){app.QAAdvanceAlphabetIntro();app.QABattleTour?.AdvanceForQA();app.QAAdvanceBoosterGuide();if(!app.Session.IsPaused&&!app.AlphabetIntroActive&&app.QAGoalEntrance==null)yield break;yield return null;}}
```

### Start · L9

方法 SHA-256：`27975145021e659c7d0a62358794dfe3376309bdc981a65f81a56d3395cefc5d`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FontGoalLoopQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.targetFrameRate=60;QualitySettings.vSyncCount=0;
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");Check(!PlayerStrategyTestBuild.Enabled&&new ResourceLeagueConfigProvider().Load().calendarSeasons,"normal calendar league build with strategy-test disabled");Check(Application.productName=="Bounce Garden","normal profile identity preserved");Check(GameVersion.Name=="0.4.0"&&GameVersion.Code==400&&Application.version=="0.4.0","runtime version 0.4.0 and code 400");yield return Capture("00-home-font");
  Check(UICraft.Font!=null&&UICraft.Font.name=="MaokenZhuyuanTi","supplied TTF is the shared source");
  Check(GameTypography.Font.sourceFontFile==UICraft.Font&&GameTypography.HeadingFont.sourceFontFile==UICraft.Font&&BattleRoundTypography.Font.sourceFontFile==UICraft.Font,"body heading and battle numbers use supplied font");
  foreach(int level in new[]{0,17,30}){
   app.QAStartLevel(level);yield return app.QAWaitForLevel();app.Session.QAIgnoreFocusLoss=true;
   float until=Time.realtimeSinceStartup+10;while(app.QAGoalEntrance==null&&Time.realtimeSinceStartup<until)yield return null;
   var entrance=app.QAGoalEntrance;Check(entrance!=null,"goal entrance present level "+level);if(entrance==null)continue;
   while(entrance!=null&&entrance.Elapsed<.55f)yield return null;
   var hud=app.GetComponentInChildren<ChainCollectionHUD>();var cards=hud.GoalsRect;int shots=app.Session.Shots;var counts=hud.Cards.Select(c=>c.NumberRect.GetComponent<TMPro.TMP_Text>().text).ToArray();
   Check(cards.parent==entrance.transform&&cards.anchoredPosition.magnitude<1&&cards.localScale.x>1,"real targets enlarged at center level "+level);
   Check(app.Session.IsPaused,"input paused during goal emphasis level "+level);yield return Capture("goal-center-"+level);
   while(entrance!=null&&entrance.Elapsed<1.8f)yield return null;yield return Capture("goal-flight-"+level);
   while(app.QAGoalEntrance!=null)yield return null;
   Check(cards.parent==hud.transform&&Mathf.Abs(cards.anchoredPosition.y-1000)<4,"real targets restored to header level "+level);
   Check(app.Session.Shots==shots&&counts.SequenceEqual(hud.Cards.Select(c=>c.NumberRect.GetComponent<TMPro.TMP_Text>().text)),"entrance changes no counts or steps level "+level);
   yield return WaitPlaying();Check(!app.Session.IsPaused,"normal play resumes after onboarding level "+level);yield return Capture("goal-header-"+level);
  }
  foreach(int index in new[]{0,4,5,10,17,20,30}){
   app.QAGardenLesson(index);yield return new WaitForSecondsRealtime(.15f);var demo=FindFirstObjectByType<OpeningRuleDemo>();var target=demo.transform.Find("Goal").GetComponent<Image>();var flight=demo.GetComponent<LessonCollectionFlight>();
   Check(flight!=null,"paired demo and collection actors page "+index);
   bool wrapped=false;float last=0;float end=Time.realtimeSinceStartup+5.2f;int wraps=0;
   var ageField=typeof(OpeningRuleDemo).GetField("age",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance);
   var flightAge=typeof(LessonCollectionFlight).GetField("age",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance);
   while(Time.realtimeSinceStartup<end){float age=(float)ageField.GetValue(demo);if(age<last){wraps++;wrapped=true;Check(target.color.a>.9f,"target returns at cycle start page "+index);Check(Mathf.Abs(age-(float)flightAge.GetValue(flight))<.12f,"collection and mechanism loop together page "+index);}last=age;yield return null;}
   Check(wrapped&&wraps>=2,"page loops more than once "+index);yield return Capture("loop-page-"+index);app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.25f);
  }
  app.QAStartLevel(4);yield return app.QAWaitForLevel();while(app.QAGoalEntrance==null)yield return null;
  app.QANavigate("home");yield return null;yield return null;Check(FindFirstObjectByType<CollectionGoalEntrance>()==null,"leaving during emphasis removes overlay");
  Check(app.CurrentPage=="home"&&!app.Session.IsPlaying,"leaving emphasis safely restores menu");
  app.QANavigate("home");typeof(AppUI).GetMethod("Settings",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance).Invoke(app,null);yield return new WaitForSecondsRealtime(.3f);Check(!app.GetComponentsInChildren<Button>().Any(b=>b.name=="试玩统计"),"normal settings exclude playtest statistics");yield return Capture("settings-actual-new-font");
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口

- L30：`demo.transform.Find("Goal")`

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
