# SystemsV64QA · SystemsV64QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/SystemsV64QA.cs`
文件 SHA-256：`4f7c0273ec85c9efe512d303a1ca9a36b56dd0ce9ec9edfd1f9abce9347dc7ee`
[原始源码快照](../source/Assets/Scripts/SystemsV64QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L15

方法 SHA-256：`8984e384f2a90bf2f7fc197baff6ccf05e897434f6ec8585fd71dcd6bff5e0fe`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SystemsV64QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SystemsV64QA.cs:35` · `SystemsV64QA.Start` → `Capture("01-shop")`
- `Assets/Scripts/SystemsV64QA.cs:36` · `SystemsV64QA.Start` → `Capture("02-score-records")`
- `Assets/Scripts/SystemsV64QA.cs:38` · `SystemsV64QA.Start` → `Capture("03-battle")`
- `Assets/Scripts/SystemsV64QA.cs:39` · `SystemsV64QA.Start` → `Capture("04-laser")`
- `Assets/Scripts/SystemsV64QA.cs:41` · `SystemsV64QA.Start` → `Capture("06-score-result")`
- `Assets/Scripts/SystemsV64QA.cs:42` · `SystemsV64QA.Start` → `Capture("05-ranking")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### Open · L16

方法 SHA-256：`57cdd2f0c37abc5669ab4f661d54b98e68ceafb53730e9370dd6b0f0a3b9e4a4`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SystemsV64QA.Open(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SystemsV64QA.cs:38` · `SystemsV64QA.Start` → `Open(0)`

```csharp
IEnumerator Open(int level){app.QAStartLevel(level);yield return app.QAWaitForLevel();for(int n=0;n<80&&app.AlphabetIntroActive;n++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.05f);}yield return new WaitForSecondsRealtime(.15f);}
```

### Start · L17

方法 SHA-256：`12bdb362ee51c1c23c28b07e358ac4ed442cd467c4b38744846929d2df357cb1`
时间来源：Time.timeScale, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SystemsV64QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){if(expectedSaveError&&m.StartsWith("League save failed"))return;errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
 yield return new WaitForSecondsRealtime(6);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");SoundService.SetMuted(true);
 var store=new Store();var p=LocalProfile.Load(store);Check(p.BoosterStock(StreakBooster.Pierce)==1&&p.BestScoreTotal==0,"new save initializes starter inventory and empty scores");
 foreach(var k in new[]{StreakBooster.Pierce,StreakBooster.Bomb,StreakBooster.Super}){int before=p.Coins;Check(p.PurchaseBooster("buy-"+k,k)&&p.Coins==before-LocalProfile.BoosterPrice(k)&&p.BoosterStock(k)==2,"exact coin purchase "+k);Check(p.PurchaseBooster("buy-"+k,k)&&p.Coins==before-LocalProfile.BoosterPrice(k)&&p.BoosterStock(k)==2,"same purchase callback idempotent "+k);}
 File.WriteAllText(dir+"/save-roundtrip.json",store.Value);p=LocalProfile.Load(store);Check(p.StorageHealthy&&p.BoosterStock(StreakBooster.Super)==2,"inventory reloads durably");Check(!p.PurchaseBooster("invalid",StreakBooster.None),"invalid kind rejected");
 Check(p.CommitAlphabetWin("score-a",0,50000)&&p.BestScore(0)==50000,"victory commits score");int coins=p.Coins;Check(p.CommitAlphabetWin("score-a",0,999999)&&p.Coins==coins&&p.BestScore(0)==50000,"result replay cannot alter score or coins");
 Check(p.CommitAlphabetWin("score-b",0,40000)&&p.BestScoreTotal==50000,"lower replay does not inflate total");Check(p.CommitAlphabetWin("score-c",0,60000)&&p.BestScoreTotal==60000,"better replay replaces prior record");p=LocalProfile.Load(store);Check(p.BestScore(0)==60000,"score records survive reload");
 Check(ScoreRules.WinCoins(0)==40&&ScoreRules.WinCoins(50000)==65&&ScoreRules.WinCoins(int.MaxValue)==70,"bounded score economy");
 var poor=new LocalProfile();for(int i=0;i<8;i++)poor.PurchaseBooster("spend"+i,StreakBooster.Super);int balance=poor.Coins;Check(!poor.PurchaseBooster("poor",StreakBooster.Super)&&poor.Coins==balance,"insufficient funds leave balance unchanged");
 var full=new LocalProfile();full.GrantPurchase("fixture",100000);for(int n=0;n<98;n++)Check(full.PurchaseBooster("capacity"+n,StreakBooster.Pierce),"inventory purchase "+n);balance=full.Coins;Check(!full.PurchaseBooster("capacity-over",StreakBooster.Pierce)&&full.Coins==balance,"99-item cap rejects purchase without debit");
 g=new GameObject("Score and inventory fixture").AddComponent<GameSession>();g.enabled=false;g.QAIgnoreFocusLoss=true;var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;Time.timeScale=0;
 for(int level=0;level<52;level++){g.StartLevel(level);Check(g.Score==0&&g.RequiredScore==0&&g.ScoreGoalMet,"score reset and collection-only goal "+level);var b=g.CollectionBricks.FirstOrDefault(b=>b.Alive&&b.Definition.kind==BrickKind.Normal&&b.Definition.juiceGroup==0&&!ChainPowerRules.IsNest(b.Definition));
  if(b==null)continue;g.SetPaused(true);g.ApplyChainImpact(b,1,Vector2.up,b.transform.position,true);Check(g.Score==0,"paused hits do not score "+level);g.SetPaused(false);int hp=b.Definition.hp;g.ApplyChainImpact(b,int.MaxValue,Vector2.up,b.transform.position,false);Check(g.Score>=ScoreRules.Break+Mathf.Min(1,hp)*ScoreRules.Damage,"power damage awards score "+level);int score=g.Score;g.ApplyChainImpact(b,int.MaxValue,Vector2.up,b.transform.position,false);Check(g.Score==score,"dead target cannot award twice "+level);
  int final=g.FinalizeScore(true);Check(final==score+g.Shots*ScoreRules.SpareShot&&g.FinalizeScore(true)==final,"spare-step bonus exactly once "+level);
 }
 for(int n=1;n<=3;n++){var k=(StreakBooster)n;g.StartLevel(0);g.BoosterProfile=p;Check(g.SelectStreakBooster(k),"mainline accepts stocked "+k);Check(g.SelectStreakBooster(k)&&p.BoosterStock(k)==2,"cancel does not consume "+k);g.SelectStreakBooster(k);int stock=p.BoosterStock(k);g.SetPaused(true);Check(!g.TryShoot(Vector2.up)&&p.BoosterStock(k)==stock,"pause cannot spend inventory "+k);g.SetPaused(false);Check(g.TryShoot(Vector2.up),"queue shop volley "+k);Steps(32);var balls=g.GetComponentsInChildren<BallBody>().Where(b=>b.Active).ToArray();Check(g.SpawnCount==5&&balls.Length>0&&balls.All(b=>b.StreakPower==k),"all five balls receive purchased power "+k);Check(p.BoosterStock(k)==stock-1,"one inventory item consumed per volley "+k);int before=g.StreakLasers+g.StreakExplosions;if(k!=StreakBooster.Pierce&&balls.Length>0){g.TriggerStreakImpact(balls[0],Vector2.zero);Check(g.StreakLasers+g.StreakExplosions==before+1,"shop power triggers outside trial "+k);}}
 var failingStore=new Store();var failingProfile=LocalProfile.Load(failingStore);failingProfile.PurchaseBooster("durable-tool",StreakBooster.Bomb);string durable=failingStore.Value;g.StartLevel(0);g.BoosterProfile=failingProfile;g.SelectStreakBooster(StreakBooster.Bomb);failingStore.Fail=true;expectedSaveError=true;Check(g.TryShoot(Vector2.up),"failed-save volley can be requested");Steps();expectedSaveError=false;Check(g.SpawnCount==0&&g.Shots==20&&failingProfile.BoosterStock(StreakBooster.Bomb)==2&&failingStore.Value==durable,"failed inventory save cancels launch and preserves both inventory and shots");
 g.StartStreakTrial(2);Check(!g.UsesShopBoosters&&g.StreakStock(StreakBooster.Super)==1,"trial inventory remains isolated");g.StopLevel();Destroy(g.gameObject);Time.timeScale=1;Physics2D.simulationMode=mode;
 app.QANavigate("shop");yield return new WaitForSecondsRealtime(.4f);Check(FindObjectsByType<TMP_Text>(FindObjectsSortMode.None).Any(t=>t.text=="喵喵小铺"),"renamed shop title");Check(FindObjectsByType<Image>(FindObjectsSortMode.None).Count(i=>i.name.StartsWith("Shop booster ")&&i.sprite!=null)==3,"all three shop icons supplied");yield return Capture("01-shop");
 app.OpenScoreRecords();yield return new WaitForSecondsRealtime(.35f);yield return Capture("02-score-records");app.QANavigate("home");
 foreach(var k in new[]{StreakBooster.Pierce,StreakBooster.Bomb,StreakBooster.Super})app.Profile.PurchaseBooster("ui-"+k,k);
 yield return Open(0);g=app.Session;g.QAIgnoreFocusLoss=true;Check(g.UsesShopBoosters&&FindObjectsByType<TMP_Text>(FindObjectsSortMode.None).Any(t=>t.text.StartsWith("积分  ")),"battle binds persistent backpack");yield return Capture("03-battle");
 var tool=g.SpawnChainTool(1,ChainGridRules.Center(4,4));tool.ReadyAt=0;g.ActivateChainTool(tool,Vector2.up);yield return Capture("04-laser");yield return new WaitForSecondsRealtime(.48f);Check(!g.GetComponentsInChildren<SpriteRenderer>().Any(s=>s.name=="Chain power trail"||s.name=="Cross laser luminous layer"||s.name=="Generated jelly laser beam"),"laser layers expire");
 int oldCoins=app.Profile.Coins;for(int pass=0;pass<6;pass++)foreach(var b in g.CollectionBricks.Where(b=>b.Alive&&(b.Definition.juiceGroup>0||g.IsJellyTarget(b))).ToArray())g.ApplyChainImpact(b,int.MaxValue,Vector2.up,b.transform.position,false);
 yield return new WaitForSecondsRealtime(1.7f);Check(g.ScoreFinalized&&app.Profile.BestScore(0)==g.Score&&app.Profile.Coins==oldCoins+ScoreRules.WinCoins(g.Score)+20,"real outcome saves score and computed coin reward together");yield return new WaitForSecondsRealtime(2);yield return Capture("06-score-result");
 app.QANavigate("ranking");yield return new WaitForSecondsRealtime(.5f);var rank=FindFirstObjectByType<LeagueRankingView>();Check(rank!=null&&rank.RowCount==21,"league preserves 21 entries");Check(FindObjectsByType<Button>(FindObjectsSortMode.None).Any(b=>b.GetComponentInChildren<TMP_Text>()?.text=="积分纪录"),"ranking exposes score records");yield return Capture("05-ranking");
 File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures+errors==0?0:3);
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
bool expectedSaveError;
```
```csharp
GameSession g;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
