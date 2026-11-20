# ThirdGroupQA · ThirdGroupQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ThirdGroupQA.cs`
文件 SHA-256：`d1933c72622a19b1790d34670ec9fed71ecab4a4ba6ba9b685dc0f3fbde8b4b2`
[原始源码快照](../source/Assets/Scripts/ThirdGroupQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L27

方法 SHA-256：`13a27882af4529ccf9111b3d1d0c0388662c34c2ebeda044bfe9d3f57d8fcc63`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ThirdGroupQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ThirdGroupQA.cs:45` · `ThirdGroupQA.Start` → `Capture("level-"+(index+1))`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,image);File.WriteAllBytes(dir+"/"+name+".png",image.EncodeToPNG());Destroy(image);rt.Release();Destroy(rt);}
```

### Start · L28

方法 SHA-256：`f3a5f9f31cc43645bf8f4cf885f3c640b5b6a8e9ebab1c68d920f807fb9d69ac`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ThirdGroupQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();g=app.Session;Physics2D.simulationMode=SimulationMode2D.Script;
  Check(OpeningCampaign.Count>=30&&OpeningCampaign.Validate()>900,"all 30 authored levels validate");
  for(int i=0;i<30;i++){g.StartLevel(i);g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);var s=OpeningCampaign.Stage(i);Check(g.IsOpening&&g.Shots==s.moves&&g.ChainGridValid(false),"valid grid and authored moves "+(i+1));Check(g.QAThirdMatches,"all exact manual goal counts "+(i+1));Check(g.QAThirdCats().Length==s.pieces.Count(p=>p.kind=="lucky_cat"),"exact cat count "+(i+1));Check(g.ChainOccupiedCells()==s.rows.Sum(r=>r.Count(c=>c!='.')),"all occupied cells preserved "+(i+1));}
  Reset();var cat=g.QAThirdCat(4,4);var anchor=cat.transform.position;g.ApplyChainImpact(cat.Brick,999,Vector2.up,anchor,true);Check(cat.Brick.Alive&&cat.Charge==0&&cat.ImpactAnimating,"cat cannot be damaged or charged by direct ball but starts jelly tween");
  g.TickOpeningThird(.04f);Check(cat.ImpactStretch.x>1&&cat.ImpactStretch.y<1&&cat.transform.position==anchor,"vertical hit squashes artwork without moving cat collider");var stretch=cat.ImpactStretch;g.SetPaused(true);g.TickOpeningThird(.2f);Check(cat.ImpactStretch==stretch,"pause freezes cat impact tween");g.SetPaused(false);g.ApplyChainImpact(cat.Brick,1,Vector2.right,anchor,true);g.TickOpeningThird(.04f);Check(cat.ImpactStretch.x<1&&cat.ImpactStretch.y>1,"repeated side hit restarts tween along horizontal axis");g.TickOpeningThird(.5f);Check(!cat.ImpactAnimating&&cat.ImpactStretch==Vector2.one,"impact tween restores exact original scale");g.ApplyChainImpact(cat.Brick,1,Vector2.up,anchor,false);Check(!cat.ImpactAnimating&&cat.Charge==0,"special damage neither animates ball impact nor charges cat");
  for(int i=0;i<6;i++)g.QAThirdNormal(i,8,i%5);var tool=g.QASevenTool(SevenSpecialRules.Cross,8,8,0);
  g.QAThirdCharge(ChainGridRules.Center(3,4));Check(cat.Charge==1,"first distinct adjacent clear lifts single paw");g.QAThirdCharge(ChainGridRules.Center(4,3));Check(cat.Charge==2,"second clear lifts both paws");g.QAThirdCharge(ChainGridRules.Center(5,4));Check(cat.Queued==1&&cat.Charge==0,"third clear queues one cycle");g.QAThirdStep(.01f);Check(g.OpeningCatBusy&&!g.RefillFreezeBalls,"replacement reserves local targets without freezing active balls");g.SetPaused(true);float age=cat.WaveAge;g.TickOpeningThird(.5f);Check(cat.WaveAge==age,"pause freezes entire presentation");g.SetPaused(false);Tick(110);Check(g.OpeningIngotConversions==5&&g.OpeningThirdPending==0,"one cycle replaces exactly five and settles all ghosts");Check(cat.transform.position==anchor&&cat.Brick.Alive&&cat.WaveAge<0,"cat returns to original pose at fixed anchor");Check(g.OpeningCollected.All(n=>n==0),"replacement does not collect old or new brick");
  Reset();cat=g.QAThirdCat(4,4);for(int i=0;i<3;i++)g.QASevenTool(new[]{6,5,1}[i],i,8,i);for(int i=0;i<3;i++)g.QAThirdCharge(ChainGridRules.Center(3,4));Tick(110);Check(g.ChainTools.Count==3&&g.ChainTools.All(t=>t.Ingot&&t.ColorIndex==1),"all special targets retain skill and become ingot");Check(g.ChainTools.Select(t=>t.Kind).OrderBy(k=>k).SequenceEqual(new[]{1,5,6}),"special kind identity preserved");
  Reset();cat=g.QAThirdCat(4,4);for(int i=0;i<3;i++)g.QAThirdCharge(ChainGridRules.Center(3,4));Tick(500);Check(g.OpeningIngotConversions==5&&g.CollectionBricks.Count(b=>b.Alive&&g.OpeningIngotBrick(b))==5,"empty board receives exactly five real ingots");Check(g.ChainGridReady&&g.ChainGridValid(false)&&g.CollectionBricks.Where(b=>b.Alive&&g.OpeningIngotBrick(b)).All(b=>b.GetComponent<ChainGridPiece>().Row==8||g.CollectionBricks.Any(o=>o.Alive&&o!=b&&o.GetComponent<ChainGridPiece>().Col==b.GetComponent<ChainGridPiece>().Col&&o.GetComponent<ChainGridPiece>().Row==b.GetComponent<ChainGridPiece>().Row+1)),"thrown ingots join normal vertical gravity and settle without overlaps");Check(g.OpeningCollected.All(n=>n==0)&&(Vector2)cat.transform.position==ChainGridRules.Center(4,4),"fallback spawning grants no collection and preserves fixed cat");
  Reset();cat=g.QAThirdCat(4,4);var otherCat=g.QAThirdCat(6,4);var cross=g.QASevenTool(1,0,8,0);g.QAThirdNormal(1,8);cat.Queued=1;otherCat.Queued=1;Tick(500);Check(g.OpeningIngotConversions==10&&cross.Ingot&&cross.Kind==1,"two cats reserve distinct targets and top up sparse replacement pool to ten ingots");Check(g.ChainGridReady&&g.ChainGridValid(false)&&g.OpeningThirdPending==0,"multiple fallback casts complete and settle with unique cells");
  Reset();cat=g.QAThirdCat(4,4);for(int r=0;r<9;r++)for(int c=0;c<9;c++)if(c!=4||r!=4)g.QAThirdIngot(g.QAThirdNormal(c,r));cat.Queued=1;Tick(5);Check(!g.OpeningCatBusy&&g.OpeningThirdPending==0&&cat.Queued==1,"full ingot board retains cat charge without blocking settlement");foreach(var b in g.CollectionBricks.Where(b=>b.Alive&&!g.OpeningCatBrick(b)&&b.GetComponent<ChainGridPiece>().Row==8).Take(5).ToArray())g.QARefillRemove(b);Tick(500);Check(g.OpeningIngotConversions==5&&cat.Queued==0&&g.ChainGridReady&&g.ChainGridValid(false),"saved charge throws five ingots once real space becomes available");
  Reset();var yellow=g.QAThirdNormal(0,8,1);var ingot=g.QAThirdNormal(8,8,1);g.QAThirdIngot(ingot);g.SevenHit(yellow);g.SevenHit(ingot);Tick(60);Check(g.OpeningCollected[0]==1&&g.OpeningCollected[1]==1,"ingot and ordinary yellow collect independently");
  Reset();cat=g.QAThirdCat(4,3);anchor=cat.transform.position;for(int r=0;r<9;r++)for(int c=0;c<9;c++)if(c!=4||r!=3)g.QAThirdNormal(c,r);var below=g.CollectionBricks.Where(b=>b.Alive&&!g.OpeningCatBrick(b)&&b.GetComponent<ChainGridPiece>().Col==4&&b.GetComponent<ChainGridPiece>().Row>3).ToArray();foreach(var b in below)g.QARefillRemove(b);g.QAThirdFill();Tick(950);Check(cat.transform.position==anchor&&cat.Brick.Alive,"flow planning keeps cat anchored");Check(g.ChainGridValid(false),"fluid refill preserves unique cells");File.AppendAllText(dir+"/flow-status.txt",g.QARefillStatus()+"\n");Check(g.ChainOccupiedCells()==81&&g.ChainGridReady,"ghost supply fills fixed-cat shadow and settles without side feed");Check(g.FlowSideSlides==0,"fixed-cat shadow fills without sideways movement");
  Reset();for(int r=0;r<9;r++)for(int c=0;c<9;c++)g.QAThirdNormal(c,r);foreach(var b in g.CollectionBricks.Where(b=>b.Alive&&b.GetComponent<ChainGridPiece>().Col==0&&b.GetComponent<ChainGridPiece>().Row>=7).ToArray())g.QARefillRemove(b);g.QAThirdFill();Tick(950);Check(g.ChainGridReady&&g.ChainGridValid(false)&&g.ChainOccupiedCells()==81,"open columns still fall vertically and replenish from top");Check(g.FlowSideSlides==0,"normal refill does not shift columns");
  Reset();g.QAThirdGoals(new OpeningTarget{kind="yarn",count=1},new OpeningTarget{kind="cookie",count=8});g.QASecondBox(1,1);g.QASecondYarn(3);g.QASecondArea(3,0,0,2,true);g.QASecondArea(3,0,0,1,true);Tick(220);Check(g.OpeningCollected.SequenceEqual(new[]{1,8})&&g.CollectionWon,"mixed yarn and fish goals finish only after registered collection flights");
  Reset();g.QAThirdGoals(new OpeningTarget{kind="ingot",count=1},new OpeningTarget{kind="horizontal",count=1});var goldTool=g.QASevenTool(6,4,4,1);goldTool.Ingot=true;g.ActivateChainTool(goldTool,Vector2.up);Tick(160);Check(g.OpeningCollected.SequenceEqual(new[]{1,1}),"special ingot credits currency and selected skill goal exactly once");
  Reset();var fixedCats=new[]{g.QAThirdCat(2,3),g.QAThirdCat(4,3),g.QAThirdCat(6,3)};var fixedPoints=fixedCats.Select(c=>c.transform.position).ToArray();var mixedBox=g.QASecondBox(3,5);var used=new System.Collections.Generic.HashSet<int>(new[]{3*9+2,3*9+4,3*9+6,5*9+3,5*9+4,6*9+3,6*9+4});for(int r=0;r<9;r++)for(int c=0;c<9;c++)if(!used.Contains(r*9+c))g.QAThirdNormal(c,r);foreach(var b in g.CollectionBricks.Where(b=>b.Alive&&!g.OpeningCatBrick(b)&&!g.OpeningCookieBrick(b)&&b.GetComponent<ChainGridPiece>().Row>=7&&new[]{2,4,6}.Contains(b.GetComponent<ChainGridPiece>().Col)).ToArray())g.QARefillRemove(b);g.QAThirdFill();Tick(1800);Check(g.ChainGridReady&&g.ChainGridValid(false)&&g.ChainOccupiedCells()==81&&g.FlowSideSlides==0,"multiple fixed cats and whole 2x2 box fill blocked gaps without side feed");Check(fixedCats.Select((c,i)=>c.transform.position==fixedPoints[i]).All(x=>x)&&mixedBox.Brick.GetComponent<ChainGridPiece>().Size==2,"multi-cat anchors and whole-box footprint remain intact");
  for(int index=20;index<30;index++){app.QAStartLevel(index);yield return app.QAWaitForLevel();for(int n=0;n<70;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return null;if(n>10&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}g=app.Session;g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);Check(app.GetComponentsInChildren<ChainTaskCard>().Count()==g.OpeningTargets.Length,"live HUD has every target "+(index+1));if(index==20||index==28||index==29)yield return Capture("level-"+(index+1));}
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口

- L42：`g.QASecondBox(1,1)`
- L44：`g.QASecondBox(3,5)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(){g.QAThirdFixture();}
```

## 源字段与依赖

```csharp
AppUI app;
```
```csharp
GameSession g;
```
```csharp
string dir;
```
```csharp
int checks,failures,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
