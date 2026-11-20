# LeagueV66QA · LeagueV66QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LeagueV66QA.cs`
文件 SHA-256：`cf5d19b2c74274f552e58fa4566d52a30b2cc4cfa402977752ecc2af62f7abde`
[原始源码快照](../source/Assets/Scripts/LeagueV66QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L15

方法 SHA-256：`8075ecc97d83e425f6a34c11188163820dec0373cb01878aa81c40e5397f43a6`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LeagueV66QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LeagueV66QA.cs:23` · `LeagueV66QA.Start` → `Capture("01-matching")`
- `Assets/Scripts/LeagueV66QA.cs:25` · `LeagueV66QA.Start` → `Capture("02-match-complete")`
- `Assets/Scripts/LeagueV66QA.cs:27` · `LeagueV66QA.Start` → `Capture("03-ranking")`
- `Assets/Scripts/LeagueV66QA.cs:30` · `LeagueV66QA.Start` → `Capture("04-home-status")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### Start · L16

方法 SHA-256：`22f97fb03fbd01a0ac22ef945d553168b70d31c232f633e9f367dae74bb43d23`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LeagueV66QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(5);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");
  Debug.Log(LeagueActiveSelfTests.RunAll());Check(true,"active model protection migration and transaction checks pass");
  var clock=new LeagueSelfTests.Clock();var config=new LeagueSelfTests.ConfigProvider{Value=new LeagueConfig{activeCompetition=true,refreshSeconds=5}};app.Profile.ConfigureLeague(config,clock);var initial=app.Profile.ReadLeague(true);
  string roster=string.Join(",",initial.entries.OrderBy(e=>e.id).Select(e=>e.id+":"+e.medals));
  app.QANavigateAnimated("ranking");yield return new WaitForSecondsRealtime(1f);
  Check(FindFirstObjectByType<LeagueMatchPresentation>()!=null,"first entry displays placement match animation");yield return Capture("01-matching");
  var view=FindFirstObjectByType<LeagueRankingView>();Check(view!=null&&view.RowCount==21,"match uses actual 21-entry saved league");
  yield return new WaitForSecondsRealtime(2.2f);yield return Capture("02-match-complete");
  yield return new WaitForSecondsRealtime(1.5f);Check(FindFirstObjectByType<LeagueMatchPresentation>()==null,"matching completes and releases input");Check(app.Profile.LeagueMatchSeen(initial.periodId),"match completion persisted");
  yield return Capture("03-ranking");app.QANavigate("home");yield return new WaitForSecondsRealtime(.2f);app.QANavigate("ranking");yield return new WaitForSecondsRealtime(.2f);Check(FindFirstObjectByType<LeagueMatchPresentation>()==null,"same period entry does not replay matching");
  clock.Now+=3600;var resumed=app.Profile.ReadLeague(true);Check(roster==string.Join(",",resumed.entries.OrderBy(e=>e.id).Select(e=>e.id+":"+e.medals)),"one-hour offline preserves all scores and ranks");Check(app.Profile.LeagueReturnRounds==2,"return protects first two rounds");
  for(int i=0;i<2;i++){app.Profile.BeginLeagueRound();clock.Now+=5;app.Profile.TickLeague(5);app.Profile.CommitAlphabetWin("v66-return-"+i,1);}Check(app.Profile.LeagueReturnRounds==0,"two production wins consume grace normally");
  app.QANavigate("home");yield return new WaitForSecondsRealtime(.2f);yield return Capture("04-home-status");
  clock.Now+=9*86400L;var next=app.Profile.ReadLeague(true);Check(next.periodId!=initial.periodId,"calendar deadline opens next fixed cohort");app.QANavigate("ranking");yield return new WaitForSecondsRealtime(.5f);
  var button=FindObjectsByType<Button>(FindObjectsSortMode.None).FirstOrDefault(b=>b.name=="League match skip");Check(button!=null,"new period replays matching with skip control");button?.onClick.Invoke();yield return null;Check(app.Profile.LeagueMatchSeen(next.periodId),"skip acknowledges same generated cohort");Check(FindFirstObjectByType<LeagueMatchPresentation>()==null,"skip immediately restores ranking interaction");
  Check(SoundService.HasCue("vo_cheer_nice_en")&&SoundService.HasCue("vo_cheer_amazing_en"),"English voice groups registered");foreach(var tier in new[]{"nice","great","amazing","incredible"})foreach(var variant in new[]{"a","b"}){var clip=SoundService.QAClip("vo_cheer_"+tier+"_en_"+variant);Check(clip!=null&&clip.length>.1f,"English cheer decoded "+tier+" "+variant);}
  foreach(string repair in new[]{"cosmetic_equip","key_unlock","paw_pick","zip_step","painting_collect","boss_defeat","cross_laser"}){var clip=SoundService.QAClip(repair);Check(clip!=null&&!clip.name.StartsWith("candidate_"),"updated replacement cue active "+repair);}
  Check(!FindObjectsByType<TMP_Text>(FindObjectsSortMode.None).Any(t=>t.text.Contains("寻找真人")),"matching makes no unsupported real-player claim");
  app.QAStartLevel(2);yield return app.QAWaitForLevel();for(int i=0;i<90&&app.AlphabetIntroActive;i++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.04f);}app.Session.QAIgnoreFocusLoss=true;app.SendMessage("OnApplicationFocus",true);app.Session.SetPaused(false);
  double activeBefore=ActiveSeconds();yield return new WaitForSecondsRealtime(6);Check(ActiveSeconds()>activeBefore,"production activity tracker advances during normal foreground battle");
  typeof(AppUI).GetMethod("FlushLeagueActivity",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance).Invoke(app,new object[]{false});app.Session.SetPaused(true);activeBefore=ActiveSeconds();yield return new WaitForSecondsRealtime(6);Check(ActiveSeconds()==activeBefore,"paused battle does not count active time");
  app.SendMessage("OnApplicationFocus",false);activeBefore=ActiveSeconds();yield return new WaitForSecondsRealtime(6);Check(ActiveSeconds()==activeBefore,"background focus loss does not count active time");
  File.WriteAllText(dir+"/result.json",JsonUtility.ToJson(new Result{checks=checks,failures=failures,errors=errors},true));Application.Quit(failures==0&&errors==0?0:3);
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
