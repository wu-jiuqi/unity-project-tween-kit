# FourthGroupQA · FourthGroupQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/FourthGroupQA.cs`
文件 SHA-256：`8be829fba89422bfc8682863b068ef9786740184dbdc602360f72efccd7d53a2`
[原始源码快照](../source/Assets/Scripts/FourthGroupQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L25

方法 SHA-256：`8984e384f2a90bf2f7fc197baff6ccf05e897434f6ec8585fd71dcd6bff5e0fe`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FourthGroupQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/FourthGroupQA.cs:47` · `FourthGroupQA.Start` → `Capture("copper-five-hp-states")`
- `Assets/Scripts/FourthGroupQA.cs:48` · `FourthGroupQA.Start` → `Capture("level-"+(index+1))`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### Start · L26

方法 SHA-256：`2d2b09bf3a4fa498ca14bd21956401032b3121e7e873168475cb64a2f7693fd4`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FourthGroupQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();g=app.Session;Physics2D.simulationMode=SimulationMode2D.Script;app.QAStartLevel(30);yield return app.QAWaitForLevel();for(int n=0;n<100;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.02f);if(n>15&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}g=app.Session;
  Check(OpeningCampaign.Count==45&&OpeningCampaign.Validate()>1000,"45 user-authored stages validate");
  for(int i=0;i<45;i++){g.StartLevel(i);g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);var s=OpeningCampaign.Stage(i);Check(g.IsOpening&&g.Shots==s.moves&&g.ChainGridValid(false),"authored grid and moves "+(i+1));Check(g.QAThirdMatches,"manual goals preserved "+(i+1));Check(g.ChainOccupiedCells()==s.rows.Sum(r=>r.Count(c=>c!='.')),"occupied cells preserved "+(i+1));Check(g.QACopperViews().Length==s.pieces.Count(p=>p.kind=="copper_coin"),"exact copper placement "+(i+1));foreach(var p in s.pieces.Where(p=>p.kind=="copper_coin")){var v=g.QACopperViews().FirstOrDefault(v=>v.Brick.GetComponent<ChainGridPiece>().Col==p.c&&v.Brick.GetComponent<ChainGridPiece>().Row==p.r);Check(v!=null&&v.Brick.CurrentHealth==p.hp,"authored copper HP "+(i+1)+"/"+p.c+","+p.r);}}
  Reset(1);var coin=g.QACopper();Vector3 footprint=coin.Image.bounds.size;
  for(int hp=4;hp>=1;hp--){Check(!Hit(coin)&&coin.Brick.CurrentHealth==hp&&coin.Brick.Alive,"ball decrements to "+hp);Step(.25f);Check(Vector3.Distance(footprint,coin.Image.bounds.size)<.005f,"identical resting footprint HP "+hp);Check(coin.GetComponentsInChildren<TMPro.TMP_Text>().Any(t=>t.gameObject.activeSelf&&t.text==hp.ToString()),"visible remaining HP "+hp);}
  Check(Hit(coin)&&!coin.Brick.Alive&&g.OpeningCollected[0]==0&&g.ChainEffectsPending,"fifth hit removes but does not credit before animation");Step(.12f);Check(g.QACopperFrame==2,"8-frame clip uses elapsed time at 20fps");g.SetPaused(true);int frame=g.QACopperFrame;Step(.5f);Check(g.QACopperFrame==frame&&g.OpeningCollected[0]==0,"pause freezes collection and frame animation");g.SetPaused(false);Tick(120);Check(g.OpeningCollected[0]==1&&g.OpeningCopperPending==0&&g.CollectionWon,"single credit after burst then HUD flight and victory");
  Reset();coin=g.QACopper();g.QAThirdCharge(ChainGridRules.Center(3,3));Check(coin.Brick.Alive&&coin.Brick.CurrentHealth==5,"diagonal does not collect");g.QAThirdCharge(ChainGridRules.Center(3,4));Check(!coin.Brick.Alive,"orthogonal clear ignores full HP");g.QAThirdCharge(ChainGridRules.Center(3,4));Tick();Check(g.OpeningCollected[0]==1,"multiple neighbor notifications cannot double credit");
  Reset();coin=g.QACopper();g.QACopperDoubleArea(coin.Brick);Check(coin.Brick.CurrentHealth==4,"same attack overlapping range hits deduplicated");g.SevenHit(coin.Brick,1);Check(coin.Brick.CurrentHealth==3,"independent range attack damages again");g.QAThirdCharge(ChainGridRules.Center(5,4));Check(!coin.Brick.Alive,"adjacency still overrides a prior range hit");Tick();Check(g.OpeningCollected[0]==1,"direct plus adjacency shares one collection");
  Reset();coin=g.QACopper();g.QACopperProtected(coin.Brick,true);Hit(coin);g.QAThirdCharge(ChainGridRules.Center(3,4));Check(coin.Brick.Alive&&coin.Brick.CurrentHealth==5,"refill protection blocks direct and adjacency");g.QACopperProtected(coin.Brick,false);Check(!g.SevenColorTarget(coin.Brick),"coin excluded from flower color conversion and ordinary targets");
  Reset();coin=g.QACopper();g.QAThirdCharge(ChainGridRules.Center(3,4));Step(.2f);Reset();Tick();Check(g.OpeningCollected[0]==0&&g.OpeningCopperPending==0,"restart discards pending frames and collections");
  foreach(float dt in new[]{1f/30,1f/60}){Reset(1);coin=g.QACopper();for(int k=0;k<5;k++)Hit(coin);Tick((int)(2/dt),dt);Check(g.OpeningCollected[0]==1&&g.OpeningCopperPending==0,"30/60fps completion "+dt);}
  Reset();coin=g.QACopper(6,4);var rewardYarn=g.QASecondYarn(3);var rewardBox=g.QASecondBox(0,0);var rewardCat=g.QAThirdCat(2,4);
  g.QASevenCompleteReward(4);
  Check(coin.Brick.Alive&&coin.Brick.CurrentHealth==5,"rating reward never replaces copper or changes HP");
  Check(rewardYarn.Brick.Alive&&rewardYarn.Layers==3&&rewardBox.Brick.Alive&&rewardBox.Remaining==8&&rewardCat.Brick.Alive,"rating reward preserves yarn box and cat");
  Check(g.ChainTools.Count==0&&g.QACopperRewardWaiting>0,"reward remains queued when only mechanisms exist");
  var rewardNormal=g.QAThirdNormal(8,8);g.QACopperRetryRewards();
  Check(!rewardNormal.Alive&&g.ChainTools.Count==1&&g.QACopperRewardWaiting==0,"pending rating reward replaces a new ordinary brick");
  Check(coin.Brick.Alive&&rewardYarn.Brick.Alive&&rewardBox.Brick.Alive&&rewardCat.Brick.Alive&&g.OpeningCollected[0]==0,"replacement preserves all mechanism identities without fake collection");
  Reset();var views=Enumerable.Range(1,5).Select(h=>g.QACopper(h+1,4,h)).ToArray();yield return Capture("copper-five-hp-states");
  foreach(int index in new[]{30,34,38,40,43,44}){app.QAStartLevel(index);yield return app.QAWaitForLevel();for(int n=0;n<80;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.025f);if(n>10&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}g=app.Session;g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);Check(app.GetComponentsInChildren<ChainTaskCard>().Count()==g.OpeningTargets.Length,"HUD all targets "+(index+1));yield return Capture("level-"+(index+1));}
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口

- L39：`g.QASecondBox(0,0)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(int goal=99){g.QACopperFixture(goal);}
```

## 源字段与依赖

```csharp
GameSession g;
```
```csharp
AppUI app;
```
```csharp
string dir;
```
```csharp
int checks,failures,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
