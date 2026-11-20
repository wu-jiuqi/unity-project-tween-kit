# StreakV61QA · StreakV61QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/StreakV61QA.cs`
文件 SHA-256：`fbd96f5a188dff9ab89244094e252057eab449ca629bb59a1e03d9c78f600e47`
[原始源码快照](../source/Assets/Scripts/StreakV61QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L17

方法 SHA-256：`bf36043bc452e066212a69df24130f358bfbafe76456af000f2770f9fbb58568`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.StreakV61QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/StreakV61QA.cs:55` · `StreakV61QA.Start` → `Capture("settings-trials")`
- `Assets/Scripts/StreakV61QA.cs:59` · `StreakV61QA.Start` → `Capture("trial-"+new[]{3,5,10}[t])`
- `Assets/Scripts/StreakV61QA.cs:62` · `StreakV61QA.Start` → `Capture("trial-"+new[]{3,5,10}[t]+"-flight")`
- `Assets/Scripts/StreakV61QA.cs:64` · `StreakV61QA.Start` → `Capture("trial-"+new[]{3,5,10}[t]+"-result")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var target=new RenderTexture(Screen.width,Screen.height,24);target.Create();var tex=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,target,tex);File.WriteAllBytes(dir+"/"+name+".png",tex.EncodeToPNG());Destroy(tex);target.Release();Destroy(target);yield return new WaitForSecondsRealtime(.15f);}
```

### Start · L19

方法 SHA-256：`12e9b7f54052fd39e21c31dbdc2c105ab9edfe5a9d133f3e11dba380f1404216`
时间来源：Time.timeScale, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.StreakV61QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(8);var app=FindFirstObjectByType<AppUI>();app.QANavigate("home");SoundService.SetMuted(true);
  var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;Time.timeScale=0;
  g=new GameObject("Booster physics fixture").AddComponent<GameSession>();g.enabled=false;g.QAIgnoreFocusLoss=true;
  for(int i=0;i<52;i++)Check(LevelCatalog.Get(i).label=="Level "+(i+1),"mainline numeric title "+(i+1));
  for(int t=0;t<3;t++){
   g.StartStreakTrial(t);Check(g.IsStreakTrial&&g.SimulatedStreak==new[]{3,5,10}[t],"independent trial identity "+t);Check(g.CurrentLevel.label=="Level "+(t+1),"trial numeric title "+t);Check(LevelCatalog.Count==52,"mainline remains 52 stages "+t);
   Check(g.ChainGridValid()&&g.CollectionBricks.Count==81,"valid authored board "+t);
   Check(g.ChainSupplyCount(1)==g.CollectionGoal[0]&&g.ChainSupplyCount(2)==g.CollectionGoal[1],"goals exactly match available authored collectibles "+t);
   Check(g.StreakStock(StreakBooster.Pierce)==1&&g.StreakStock(StreakBooster.Bomb)==(t>=1?1:0)&&g.StreakStock(StreakBooster.Super)==(t>=2?1:0),"cumulative temporary loadout "+t);
   Check(g.SelectStreakBooster(StreakBooster.Pierce)&&g.SelectStreakBooster(StreakBooster.Pierce)&&g.SelectedStreakBooster==StreakBooster.None&&g.StreakStock(StreakBooster.Pierce)==1,"selection cancel costs nothing "+t);
   var kind=(StreakBooster)(t+1);Fire(kind);Check(g.StreakStock(kind)==0,"stock charged at actual launch "+t);Check(!g.SelectStreakBooster(StreakBooster.Pierce),"no selection during pending five-ball volley "+t);
   Steps(25);Check(g.SpawnCount==5&&Balls().All(b=>b.StreakPower==kind),"all five balls share captured booster "+t);
   g.SetPaused(true);int spawned=g.SpawnCount,procs=g.StreakExplosions+g.StreakLasers;var poses=Balls().ToDictionary(b=>b,b=>b.Body.position);Steps(30);
   Check(g.SpawnCount==spawned&&g.StreakExplosions+g.StreakLasers==procs&&poses.All(p=>p.Key.Body.position==p.Value),"pause freezes boosted balls "+t);g.SetPaused(false);
   foreach(var b in Balls())g.LoseBall(b,b.Body.position);Check(g.TryShoot(Vector2.up),"next ordinary volley accepted "+t);Steps(40);Check(g.SpawnCount==10&&Balls().All(b=>b.StreakPower==StreakBooster.None),"booster expires after its own volley "+t);
   g.StartStreakTrial(t);Check(g.StreakStock(kind)==1,"restart gives fresh per-run loadout "+t);g.StartLevel(20);Check(!g.IsStreakTrial&&g.StreakStock(kind)==0,"return to mainline clears temporary tools "+t);yield return null;
  }
  // Actual collision traversal: two surviving bricks receive exactly one point each.
  g.StartStreakTrial(0);var bottom=Cell(0,8);var above=Cell(0,7);int hp0=bottom.CurrentHealth,hp1=above.CurrentHealth;var pierce=Fire(StreakBooster.Pierce);
  Vector2 origin=ChainGridRules.Center(0,8)+Vector2.down*.62f;pierce.ResumeChainFlight(origin,Vector2.up*g.CurrentLevel.ballSpeed);Steps(12);
  File.AppendAllText(dir+"/pierce-diagnostic.txt",$"hp={bottom.CurrentHealth}/{hp0} above={above.CurrentHealth}/{hp1} pos={pierce.Body.position} velocity={pierce.Body.linearVelocity} power={pierce.StreakPower} hits={g.HitCount}\n");Check(bottom.CurrentHealth==hp0-1&&above.CurrentHealth==hp1-1,"pierce applies unchanged one-point damage once per traversal");Check(pierce.Body.position.y>ChainGridRules.Center(0,7).y&&pierce.Body.linearVelocity.y>0,"pierce crosses two live bricks without reflection");
  // Bomb area uses real brick health. Same-step callbacks are coalesced.
  g.StartStreakTrial(1);var bomb=Fire(StreakBooster.Bomb);var center=Cell(4,3);var nearby=Cell(3,3);var distant=Cell(0,3);int hc=center.CurrentHealth,hn=nearby.CurrentHealth,hd=distant.CurrentHealth;
  g.TriggerStreakImpact(bomb,ChainGridRules.Center(4,3));g.TriggerStreakImpact(bomb,ChainGridRules.Center(4,3));
  Check(center.CurrentHealth==hc-2&&nearby.CurrentHealth==hn-2&&distant.CurrentHealth==hd,"3x3 blast deals two damage only in its area");Check(g.StreakExplosions==1,"simultaneous corner callbacks do not double-explode");
  Steps();int explosions=g.StreakExplosions;bomb.ResumeChainFlight(new Vector2(FlightGeometry.Left+.18f,-2.2f),Vector2.left*g.CurrentLevel.ballSpeed);Steps(3);Check(g.StreakExplosions>explosions,"real wall reflection triggers bomb");
  // Laser is the production cross-ray effect, including blocking stones.
  g.StartStreakTrial(2);var light=Fire(StreakBooster.Super);var stone=Cell(6,4);var behind=Cell(6,3);int before=behind.CurrentHealth,horizontalBefore=Cell(5,6).CurrentHealth;g.TriggerStreakImpact(light,ChainGridRules.Center(6,6));
  Check(g.StreakLasers==1&&stone.Alive&&behind.CurrentHealth==before,"super cross laser respects stone occlusion");Check(horizontalBefore<=2?!CellAlive(5,6):Cell(5,6).CurrentHealth==horizontalBefore-2,"super cross deals two damage to ordinary horizontal target");
  Steps(2);g.SetPaused(true);int beams=g.StreakLasers;g.TriggerStreakImpact(light,Vector2.zero);Check(g.StreakLasers==beams,"paused collision cannot trigger laser");g.SetPaused(false);
  g.StopLevel();Destroy(g.gameObject);Physics2D.simulationMode=mode;Time.timeScale=1;
  // Exercise visible settings, selection, restart, result and profile isolation.
  app.QANavigate("home");yield return null;typeof(AppUI).GetMethod("Settings",BindingFlags.Instance|BindingFlags.NonPublic).Invoke(app,null);yield return new WaitForSecondsRealtime(.25f);
  var entry=FindObjectsByType<Button>(FindObjectsSortMode.None).FirstOrDefault(b=>b.GetComponentInChildren<TMPro.TMP_Text>()?.text=="连胜道具测试");Check(entry!=null,"settings exposes trial entry without development-tool gating");if(entry!=null)entry.onClick.Invoke();yield return new WaitForSecondsRealtime(.25f);yield return Capture("settings-trials");
  for(int t=0;t<3;t++){
   int coins=app.Profile.Coins,badges=app.Profile.Badges,unlocked=app.Profile.AlphabetUnlocked;var feedback=(IHomeFeedbackSource)typeof(AppUI).GetField("savedFeedback",BindingFlags.Instance|BindingFlags.NonPublic).GetValue(app);int streakBefore=feedback.Read().WinStreak;
   app.QAStartStreakTrial(t);yield return app.QAWaitForLevel();g=app.Session;g.QAIgnoreFocusLoss=true;yield return new WaitForSecondsRealtime(.3f);
   Check(g.IsStreakTrial&&g.SimulatedStreak==new[]{3,5,10}[t]&&!g.IsPaused&&!app.AlphabetIntroActive,"actual loading opens correct playable trial "+t);Check(FindFirstObjectByType<StreakTrialHUD>()!=null,"trial controls visible "+t);yield return Capture("trial-"+new[]{3,5,10}[t]);
   var hud=FindFirstObjectByType<StreakTrialHUD>();var button=hud.GetComponentsInChildren<Button>().First(b=>b.name=="Skill slot "+(StreakBooster)(t+1));button.onClick.Invoke();
   Check(g.SelectedStreakBooster==(StreakBooster)(t+1),"visible booster control selects correctly "+t);app.QADragAim(new Vector2(.55f,1));yield return new WaitForSecondsRealtime(.8f);
   Check(g.SpawnCount==5&&g.StreakStock((StreakBooster)(t+1))==0,"UI drag fires five boosted balls and consumes once "+t);yield return Capture("trial-"+new[]{3,5,10}[t]+"-flight");
   Goal();yield return new WaitForSecondsRealtime(1.2f);Check(!g.IsPlaying&&FindObjectsByType<TMPro.TMP_Text>(FindObjectsSortMode.None).Any(x=>x.text=="试炼完成！"),"trial victory uses isolated result "+t);
   Check(app.Profile.Coins==coins&&app.Profile.Badges==badges&&app.Profile.AlphabetUnlocked==unlocked,"trial victory grants no mainline rewards or progress "+t);Check(feedback.Read().WinStreak==streakBefore,"trial result leaves real streak unchanged "+t);yield return Capture("trial-"+new[]{3,5,10}[t]+"-result");
   var restart=FindObjectsByType<Button>(FindObjectsSortMode.None).First(b=>b.GetComponentInChildren<TMPro.TMP_Text>()?.text=="重新挑战");restart.onClick.Invoke();yield return app.QAWaitForLevel();Check(app.Session.StreakTrialIndex==t&&app.Session.StreakStock((StreakBooster)(t+1))==1,"result restart stays in same trial and refills temporary stock "+t);
   app.QANavigate("home");yield return null;
  }
  Check(errors==0,"zero runtime errors");File.WriteAllText(dir+"/result.json",$"{{\"checks\":{checks},\"failures\":{failures},\"errors\":{errors}}}");Application.Quit(failures+errors==0?0:1);
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
GameSession g;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
