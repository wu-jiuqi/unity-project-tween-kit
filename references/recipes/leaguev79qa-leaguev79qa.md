# LeagueV79QA · LeagueV79QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LeagueV79QA.cs`
文件 SHA-256：`a8545607af08c33bd36b99136dd9d46b325ac336c8528fec4a5e387d24f8356c`
[原始源码快照](../source/Assets/Scripts/LeagueV79QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L16

方法 SHA-256：`8075ecc97d83e425f6a34c11188163820dec0373cb01878aa81c40e5397f43a6`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LeagueV79QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LeagueV79QA.cs:33` · `LeagueV79QA.Start` → `Capture("win-"+(level+1))`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### Start · L17

方法 SHA-256：`2144e21dbbf7a3d1cd560445081386ecbc3cb3cbf27b175e5cb5ed07fb074cb8`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LeagueV79QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(4);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");
  Check(Period().chaseRounds==0,"fresh profile starts without a chase");
  for(int level=0;level<3;level++){
   app.QAStartLevel(level);yield return app.QAWaitForLevel();for(int i=0;i<100&&app.AlphabetIntroActive;i++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.02f);}
   var game=app.Session;game.QAIgnoreFocusLoss=true;game.SetPaused(false);
   string before=Scores();yield return new WaitForSecondsRealtime(6);Check(before==Scores(),"active battle time cannot grow rivals "+level);
   int rounds=Period().chaseRounds;int medals=app.Profile.Badges;string resultKey=app.QAResultKey;
   // Exercise the actual target-clear -> OnOutcome -> reward transaction -> result UI path.
   for(int pass=0;pass<6;pass++)foreach(var brick in game.CollectionBricks.Where(b=>b.Alive&&(b.Definition.juiceGroup>0||game.IsJellyTarget(b))).ToArray())game.ApplyChainImpact(brick,int.MaxValue,Vector2.up,brick.transform.position,false);
   yield return new WaitForSecondsRealtime(4);
   Check(app.Profile.Badges==medals+1&&Period().chaseRounds==rounds+1,"actual level clear awards one medal and one chase "+level);
   Check(Period().lastChaseRivals>0&&before!=Scores(),"rival medals change immediately after level clear "+level);
   Check(FindObjectsByType<TMP_Text>(FindObjectsSortMode.None).Any(t=>t.text.Contains("位对手已追赶")),"result shows the committed chase "+level);
   string saved=Scores();app.Profile.CommitAlphabetWin(resultKey,level,game.Score);Check(saved==Scores()&&Period().chaseRounds==rounds+1,"replayed reward cannot rerun chase "+level);
   yield return Capture("win-"+(level+1));app.QANavigate("ranking");yield return new WaitForSecondsRealtime(.3f);
   var view=FindFirstObjectByType<LeagueRankingView>();Check(view!=null&&view.Snapshot.player.medals==app.Profile.Badges,"ranking reflects the committed result "+level);
   Check(saved==Scores(),"opening ranking does not cause another chase "+level);app.QANavigate("home");
  }
  File.WriteAllText(dir+"/profile.json",app.Profile.ExportForQA());File.WriteAllText(dir+"/result.json",JsonUtility.ToJson(new Result{checks=checks,failures=failures,errors=errors},true));Application.Quit(failures+errors==0?0:3);
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
