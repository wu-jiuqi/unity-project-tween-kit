# EconomyV67QA · EconomyV67QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/EconomyV67QA.cs`
文件 SHA-256：`38b404c02bdef356116a329ed4fd15aa5e1f7e9b6a8790d087095f2392513efb`
[原始源码快照](../source/Assets/Scripts/EconomyV67QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L12

方法 SHA-256：`8075ecc97d83e425f6a34c11188163820dec0373cb01878aa81c40e5397f43a6`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.EconomyV67QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/EconomyV67QA.cs:22` · `EconomyV67QA.Start` → `Capture("01-rescue-ready")`
- `Assets/Scripts/EconomyV67QA.cs:26` · `EconomyV67QA.Start` → `Capture("02-shop")`
- `Assets/Scripts/EconomyV67QA.cs:27` · `EconomyV67QA.Start` → `Capture("03-economy-guide")`
- `Assets/Scripts/EconomyV67QA.cs:33` · `EconomyV67QA.Start` → `Capture("04-milestone-result")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### Open · L13

方法 SHA-256：`626e5e7c443ee206caaf5b0caafb9efda97ba0f662a29fdd73535eb771a276ff`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.EconomyV67QA.Open(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/EconomyV67QA.cs:19` · `EconomyV67QA.Start` → `Open(0)`
- `Assets/Scripts/EconomyV67QA.cs:20` · `EconomyV67QA.Start` → `Open(0)`
- `Assets/Scripts/EconomyV67QA.cs:28` · `EconomyV67QA.Start` → `Open(2)`

```csharp
IEnumerator Open(int level){app.QAStartLevel(level);yield return app.QAWaitForLevel();for(int n=0;n<90&&app.AlphabetIntroActive;n++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.04f);}app.Session.QAIgnoreFocusLoss=true;app.Session.SetPaused(false);yield return new WaitForSecondsRealtime(.15f);}
```

### Start · L14

方法 SHA-256：`9497b678584ee4654f7f837505815cc95505cd0aa9ac8bac44849cd234f03106`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.EconomyV67QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(5);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");
  Check(app.Profile.Coins==400&&app.Profile.BoosterStock(StreakBooster.Super)==1,"fresh runtime uses starter wallet and tools");
  // Controlled outcome callbacks exercise the production loss-to-retry path; these are not natural gameplay wins/losses.
  for(int n=0;n<3;n++){yield return Open(0);app.SendMessage("OnOutcome",false);app.Session.SetPaused(true);Check(app.Profile.RescueFailures(0)==(n==2?0:n+1),"production loss callback "+n);}
  Check(app.Profile.HasRescue(0),"three controlled losses grant rescue");yield return Open(0);
  var g=app.Session;int stock=app.Profile.BoosterStock(StreakBooster.Pierce);Check(g.StreakStock(StreakBooster.Pierce)==stock+1,"HUD stock includes current rescue");
  Check(FindObjectsByType<TMP_Text>(FindObjectsSortMode.None).Any(t=>t.text.Contains("本关救助")),"rescue explained in skill tray");yield return Capture("01-rescue-ready");
  Check(g.SelectStreakBooster(StreakBooster.Pierce)&&g.SelectStreakBooster(StreakBooster.Pierce)&&app.Profile.HasRescue(0),"selection and cancellation preserve rescue");
  g.SelectStreakBooster(StreakBooster.Pierce);Check(g.TryShoot(Vector2.up),"rescue volley requested");yield return new WaitForSecondsRealtime(.65f);
  Check(!app.Profile.HasRescue(0)&&app.Profile.BoosterStock(StreakBooster.Pierce)==stock&&g.SpawnCount==5,"actual five-ball launch spends rescue without paid stock");
  app.QANavigate("shop");yield return new WaitForSecondsRealtime(.3f);yield return Capture("02-shop");
  typeof(AppUI).GetMethod("OpenEconomyGuide",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance).Invoke(app,null);yield return new WaitForSecondsRealtime(.3f);yield return Capture("03-economy-guide");
  app.QANavigate("home");yield return Open(2);g=app.Session;int coins=app.Profile.Coins;
  for(int pass=0;pass<6;pass++)foreach(var b in g.CollectionBricks.Where(b=>b.Alive&&(b.Definition.juiceGroup>0||g.IsJellyTarget(b))).ToArray())g.ApplyChainImpact(b,int.MaxValue,Vector2.up,b.transform.position,false);
  yield return new WaitForSecondsRealtime(1.8f);int reward=ScoreRules.WinCoins(g.Score)+120;
  Check(g.ScoreFinalized&&app.Profile.Coins==coins+reward&&app.Profile.NewPlayerMilestone(2)==0,"controlled production win grants first clear and level-three milestone");
  int wallet=app.Profile.Coins;Check(app.Profile.CommitAlphabetWin(app.QAResultKey,2,g.Score)&&app.Profile.Coins==wallet,"repeated real result callback cannot repeat milestone");
  yield return new WaitForSecondsRealtime(4);yield return Capture("04-milestone-result");
  File.WriteAllText(dir+"/profile.json",app.Profile.ExportForQA());File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures+errors==0?0:3);
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
