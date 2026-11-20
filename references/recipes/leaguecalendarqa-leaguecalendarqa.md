# LeagueCalendarQA · LeagueCalendarQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LeagueCalendarQA.cs`
文件 SHA-256：`c5243e97ba6f403687ffa44d283fb9ccf9785b406b604895e07537b60b83dfac`
[原始源码快照](../source/Assets/Scripts/LeagueCalendarQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L15

方法 SHA-256：`84c0765f5adb16cd3f0e2501dd4867c0e0b428902261a29e51aa2919b2efdfb7`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LeagueCalendarQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LeagueCalendarQA.cs:23` · `LeagueCalendarQA.Start` → `Capture("01-locked")`
- `Assets/Scripts/LeagueCalendarQA.cs:31` · `LeagueCalendarQA.Start` → `Capture("02-first-entry")`
- `Assets/Scripts/LeagueCalendarQA.cs:33` · `LeagueCalendarQA.Start` → `Capture("03-medal-reward")`
- `Assets/Scripts/LeagueCalendarQA.cs:36` · `LeagueCalendarQA.Start` → `Capture("04-previous-season-reward")`
- `Assets/Scripts/LeagueCalendarQA.cs:38` · `LeagueCalendarQA.Start` → `Capture("05-next-season-entry")`
- `Assets/Scripts/LeagueCalendarQA.cs:39` · `LeagueCalendarQA.Start` → `Capture("06-ranking")`
- `Assets/Scripts/LeagueCalendarQA.cs:42` · `LeagueCalendarQA.Start` → `Capture("07-rank-up-motion")`
- `Assets/Scripts/LeagueCalendarQA.cs:43` · `LeagueCalendarQA.Start` → `Capture("08-rank-down-motion")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,image);File.WriteAllBytes(dir+"/"+name+".png",image.EncodeToPNG());Destroy(image);rt.Release();Destroy(rt);}
```

### Start · L16

方法 SHA-256：`e37c9e4e9752418fe200f808cb40d7cb7191320f3a1983f64807e04bbc6aa652`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LeagueCalendarQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.targetFrameRate=60;QualitySettings.vSyncCount=0;
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");clock=new Clock{now=LeagueEngine.CalendarAnchor+3600};
  var config=new ResourceLeagueConfigProvider().Load();app.Profile.ConfigureLeague(new JsonLeagueConfigProvider(()=>JsonUtility.ToJson(config)),clock);
  Check(!PlayerStrategyTestBuild.Enabled,"normal production build, no level-select test define");
  for(int i=0;i<9;i++)app.Profile.CommitAlphabetWin("runtime-unlock-"+i,i,1000);
  app.QANavigate("ranking");yield return null;Check(app.Profile.ReadLeague()==null&&!app.LeagueFlowActive,"league stays locked through level 9");yield return Capture("01-locked");
  app.QAStartLevel(9);yield return app.QAWaitForLevel();var g=app.Session;g.QAIgnoreFocusLoss=true;
  for(int n=0;n<120;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.02f);if(n>10&&!app.AlphabetIntroActive&&!g.IsPaused)break;}g.SetPaused(false);
  g.QAFinaleGoal(1,78200);float began=Time.realtimeSinceStartup;
  while((g.IsPlaying||app.QAGoalCelebration!=null)&&Time.realtimeSinceStartup-began<5)yield return null;g.AccelerateVictoryPlayback();
  while(!app.LeagueFlowActive&&Time.realtimeSinceStartup-began<25)yield return null;
  Check(app.RewardReady&&app.RewardClaimed,"actual tenth-level victory commits before entry");Check(app.LeagueFlowActive,"tenth-level entry auto plays without visiting ranking");
  var entry=FindFirstObjectByType<LeagueMatchPresentation>();Check(entry!=null,"independent entry overlay exists");
  yield return new WaitForSecondsRealtime(2.4f);yield return Capture("02-first-entry");
  if(entry!=null)entry.Continue();yield return new WaitForSecondsRealtime(.5f);
  var snap=app.Profile.ReadLeague();Check(snap.player.medals==1&&app.Profile.LeagueMatchSeen(snap.periodId),"first medal and entry acknowledgement saved");Check(!app.LeagueFlowActive,"entry returns to victory result");yield return Capture("03-medal-reward");
  app.QANavigate("home");yield return new WaitForSecondsRealtime(.5f);Check(!app.LeagueFlowActive,"same-season home return does not replay entry");
  clock.now=LeagueEngine.CalendarAnchor+7*86400;app.Profile.ReadLeague(true);yield return new WaitForSecondsRealtime(.6f);
  Check(app.LeagueFlowActive&&FindFirstObjectByType<LeagueMatchPresentation>()==null,"next login flow shows previous reward before new entry");yield return Capture("04-previous-season-reward");
  var claim=app.GetComponentsInChildren<Button>().FirstOrDefault(b=>b.GetComponentInChildren<TMPro.TMP_Text>()?.text=="领取并继续");Check(claim!=null,"automatic reward claim action available");claim?.onClick.Invoke();yield return new WaitForSecondsRealtime(2.8f);
  entry=FindFirstObjectByType<LeagueMatchPresentation>();Check(entry!=null&&app.Profile.ReadLeague().player.medals==0,"new season auto entry without new victory");yield return Capture("05-next-season-entry");entry?.Continue();yield return new WaitForSecondsRealtime(.4f);
  app.QANavigate("ranking");yield return new WaitForSecondsRealtime(.6f);Check(FindFirstObjectByType<LeagueMatchPresentation>()==null,"opening ranking does not replay entry");yield return Capture("06-ranking");
  // Visual fixtures show both directions independently of natural competition outcomes.
  var canvas=app.GetComponentsInChildren<Canvas>().First();var preview=UICraft.Root(canvas.transform,"Motion fixture",0,0,1080,2340);UICraft.Box(preview,"Preview backdrop",0,0,1080,2340,new Color32(248,244,235,255));
  UICraft.Label(preview,"奖章与排名上升演出",0,500,1000,110,50,UICraft.Ink);var frame=UICraft.Root(preview,"Victory rewards",0,0,832,270);LeagueMedalPresentation.Show(frame,12,8,1);yield return new WaitForSecondsRealtime(.35f);yield return Capture("07-rank-up-motion");Destroy(preview.gameObject);yield return null;
  preview=UICraft.Root(canvas.transform,"Down motion fixture",0,0,1080,2340);UICraft.Box(preview,"Preview backdrop",0,0,1080,2340,new Color32(248,244,235,255));UICraft.Label(preview,"排名下降演出",0,500,1000,110,50,UICraft.Ink);frame=UICraft.Root(preview,"Victory rewards",0,0,832,270);LeagueMedalPresentation.Show(frame,8,9,1);yield return new WaitForSecondsRealtime(.35f);yield return Capture("08-rank-down-motion");Destroy(preview.gameObject);
  clock.now=LeagueEngine.CalendarAnchor+15*86400;app.QANavigate("home");yield return new WaitForSecondsRealtime(.7f);Check(FindFirstObjectByType<LeagueMatchPresentation>()!=null,"skips unplayed season reward and opens current entry");
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures+errors==0?0:3);
 }
```

## 对象层级和素材入口

- L41：`UICraft.Root(canvas.transform,"Motion fixture",0,0,1080,2340)`
- L41：`UICraft.Box(preview,"Preview backdrop",0,0,1080,2340,new Color32(248,244,235,255))`
- L42：`UICraft.Label(preview,"奖章与排名上升演出",0,500,1000,110,50,UICraft.Ink)`
- L42：`UICraft.Root(preview,"Victory rewards",0,0,832,270)`
- L43：`UICraft.Root(canvas.transform,"Down motion fixture",0,0,1080,2340)`
- L43：`UICraft.Box(preview,"Preview backdrop",0,0,1080,2340,new Color32(248,244,235,255))`
- L43：`UICraft.Label(preview,"排名下降演出",0,500,1000,110,50,UICraft.Ink)`

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
Clock clock;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
