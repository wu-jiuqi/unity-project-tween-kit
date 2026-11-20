# SecondGroupIntegrationQA · SecondGroupIntegrationQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/SecondGroupIntegrationQA.cs`
文件 SHA-256：`b008b3c4521c3ffaa3c8029e7349f870836e9e577a58af8b2a330f91412dbebc`
[原始源码快照](../source/Assets/Scripts/SecondGroupIntegrationQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L20

方法 SHA-256：`13a27882af4529ccf9111b3d1d0c0388662c34c2ebeda044bfe9d3f57d8fcc63`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SecondGroupIntegrationQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SecondGroupIntegrationQA.cs:52` · `SecondGroupIntegrationQA.Start` → `Capture("level-"+(index+1))`
- `Assets/Scripts/SecondGroupIntegrationQA.cs:54` · `SecondGroupIntegrationQA.Start` → `Capture("cookie-hop")`
- `Assets/Scripts/SecondGroupIntegrationQA.cs:55` · `SecondGroupIntegrationQA.Start` → `Capture("special-shake")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,image);File.WriteAllBytes(dir+"/"+name+".png",image.EncodeToPNG());Destroy(image);rt.Release();Destroy(rt);}
```

### Level · L21

方法 SHA-256：`f2248f4bedb054771a5b7adce4dc4f9e044090e4c9f764d365f52dd36555e766`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SecondGroupIntegrationQA.Level(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SecondGroupIntegrationQA.cs:52` · `SecondGroupIntegrationQA.Start` → `Level(index)`
- `Assets/Scripts/SecondGroupIntegrationQA.cs:53` · `SecondGroupIntegrationQA.Start` → `Level(10)`

```csharp
IEnumerator Level(int index){app.QAStartLevel(index);yield return app.QAWaitForLevel();for(int n=0;n<80;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.015f);if(n>8&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}g=app.Session;g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);}
```

### Start · L22

方法 SHA-256：`44ba8c4578daaaf6a9a51eb6b77fe97213b708f7ca5747449aef569ad720f378`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SecondGroupIntegrationQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();g=app.Session;Physics2D.simulationMode=SimulationMode2D.Script;
  Check(OpeningCampaign.Count==20&&OpeningCampaign.Validate()>0,"all twenty exact user layouts validate");
  for(int i=0;i<20;i++){
   g.StartLevel(i);g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);var s=OpeningCampaign.Stage(i);
   Check(g.IsOpening&&g.ChainGridValid(false)&&g.Shots==s.moves,"layout, positions and authored moves "+(i+1));
   Check(g.ChainOccupiedCells()==s.rows.Sum(row=>row.Count(c=>c!='.')),"exact occupied cells "+(i+1));
   Check(g.QASecondBoxes().Length==s.pieces.Count(p=>p.kind=="cookie_box"),"one entity per box "+(i+1));
   if(s.goal.kind=="mechanisms")Check(g.CollectionGoal[0]==s.goal.count&&g.CollectionGoal[1]==s.goal.cookies,"two independent exact collection goals "+(i+1));
   foreach(var box in g.QASecondBoxes())Check(box.Brick.GetComponent<ChainGridPiece>().Size==2&&box.Brick.GetComponentsInChildren<Collider2D>().Length==1&&box.Remaining==8,"rigid 2x2 box, one collider, eight biscuits "+(i+1));
   Check(g.QAOpeningYarns().Sum(y=>y.Layers)==s.pieces.Where(p=>p.kind=="yarn").Sum(p=>p.layers),"authored yarn layers "+(i+1));
  }
  Reset();var box1=g.QASecondBox();var pos=box1.Brick.GetComponent<Rigidbody2D>().position;g.ApplyChainImpact(box1.Brick,99,Vector2.up,pos,true);Check(box1.Remaining==8&&box1.CollectionHops==0,"direct ball neither collects nor launches box");
  var near=g.QAOpeningSpawn(2,3,1);g.ApplyChainImpact(near,1,Vector2.up,near.transform.position,true);Check(box1.Remaining==7&&g.Collected[1]==0,"one adjacent elimination releases one delayed collectible");box1.Tick(.07f);Check(box1.VisualHeight>.1f&&box1.Brick.GetComponent<Rigidbody2D>().position==pos,"visible whole-box hop without physical motion");float height=box1.VisualHeight;g.SetPaused(true);box1.Tick(.1f);Check(box1.VisualHeight==height,"paused hop freezes");g.SetPaused(false);box1.Tick(.4f);Check(box1.VisualHeight==0,"hop returns exactly to rest");
  Reset();box1=g.QASecondBox();g.QASecondArea(6);Check(box1.Remaining==6,"horizontal laser crosses two box cells");g.QASecondArea(6);Check(box1.Remaining==4,"independent laser can collect again");
  Reset();box1=g.QASecondBox();g.QASecondArea(5,3,0);Check(box1.Remaining==6,"vertical laser crosses two cells");
  Reset();box1=g.QASecondBox();g.QASecondArea(6,0,3,1,false,true);Check(box1.Remaining==6,"same attack cell deduplication");
  Reset();box1=g.QASecondBox();g.QASecondCell(3,3);Check(box1.Remaining==7,"one precise covered cell equals one biscuit");
  Reset();box1=g.QASecondBox();g.QASecondArea(2,3,3);Check(box1.Remaining==4,"bomb covers four cells, four biscuits");
  Reset();box1=g.QASecondBox();near=g.QAOpeningSpawn(2,3,1);var top=g.QAOpeningSpawn(3,2,1);g.QAOpeningBatch(new[]{near,top});Check(box1.Remaining==7,"same cell adjacent eliminations deduplicate within one attack");
  Reset();box1=g.QASecondBox();near=g.QAOpeningSpawn(2,2,1);g.SevenHit(near);Check(box1.Remaining==8,"diagonal elimination does not open box");near=g.QAEffectsBrick(2,3,30,1);g.SevenHit(near);Check(near.Alive&&near.CurrentHealth==20&&box1.Remaining==8,"nonlethal adjacent damage does not collect");
  Reset();box1=g.QASecondBox();g.QASecondArea(3,0,0,2,true);Check(box1.Remaining==0&&box1.Brick.Alive&&g.OpeningCookiesReleased==8,"double flower consumes at most stock and lets final hop finish");Tick(100);Check(!box1.Brick.Alive&&g.Collected[1]==8&&g.OpeningCookiePending==0,"empty whole box retires and all eight flights settle");
  Reset();box1=g.QASecondBox();g.QASecondArea(6);Reset();Tick(100);Check(g.Collected[1]==0&&g.OpeningCookiePending==0,"restart clears pending cookie collections and hops");
  Reset();var yarn=g.QASecondYarn(3);g.ApplyChainImpact(yarn.Brick,99,Vector2.up,yarn.Brick.transform.position,true);Check(yarn.Layers==3,"direct ball cannot remove third yarn layer");g.QASecondArea(6,0,4);Check(yarn.Layers==2,"third yarn loses teal layer once");g.QASecondArea(6,0,4);Check(yarn.Layers==1,"second independent attack loses purple layer");g.QASecondArea(6,0,4);Check(!yarn.Brick.Alive&&g.Collected[0]==0,"third hit starts final unravel without early credit");Tick(160);Check(g.Collected[0]==1&&g.OpeningYarnPending==0,"ordered peel animations and unravel credit once");
  Reset();yarn=g.QASecondYarn(3);g.QASecondArea(3,0,0,2,true);Check(yarn.Layers==1&&g.Collected[0]==0,"double flower removes two of three layers without collecting");
  Reset();box1=g.QASecondBox(3,1);Tick(220);Check(g.ChainGridValid(false)&&box1.Brick.Alive&&box1.Brick.GetComponent<ChainGridPiece>().Size==2,"gravity and refill retain one non-overlapping 2x2 box");
  Reset();g.CollectionGoal[0]=0;g.CollectionGoal[1]=8;box1=g.QASecondBox();g.QASecondArea(3,0,0,2,true);Tick(220);g.QASevenOutcome();Tick(100);Check(g.CollectionWon&&g.QASevenOutcome(),"cookie-only goal completes through hop, loot and victory settlement");
  Reset();g.CollectionGoal[0]=1;g.CollectionGoal[1]=8;box1=g.QASecondBox(1,1);yarn=g.QASecondYarn(3);g.QASecondArea(3,0,0,2,true);g.QASecondArea(3,0,0,1,true);Tick(220);g.QASevenOutcome();Tick(100);Check(g.Collected[0]==1&&g.Collected[1]==8&&g.QASevenOutcome(),"mixed yarn and cookie goals settle without waiting for more supply");
  foreach(int index in new[]{10,17,19}){yield return Level(index);yield return Capture("level-"+(index+1));Check(app.GetComponentsInChildren<ChainTaskCard>().All(c=>c.JellyColor>=0||c.FoodIndex!=1||c.Icon!=null),"live collection HUD exists "+(index+1));}
  yield return Level(10);box1=g.QASecondBoxes().First();pos=box1.Brick.GetComponent<Rigidbody2D>().position;near=g.CollectionBricks.FirstOrDefault(b=>b.Alive&&!g.OpeningCookieBrick(b)&&!g.OpeningYarnBrick(b)&&Vector2.Distance(b.transform.position,box1.Cell(2))<.9f);
  if(near!=null){g.ApplyChainImpact(near,99,Vector2.up,near.transform.position,true);box1.Tick(.08f);yield return Capture("cookie-hop");Check(box1.VisualHeight>.1f&&box1.Brick.GetComponent<Rigidbody2D>().position==pos,"actual authored box visibly hops at stable grid anchor");}
  var shake=Camera.main.GetComponent<CameraJuice>();shake.ResetImpact();g.CombatFeel.Power(Vector2.zero);yield return new WaitForEndOfFrame();float pixels=shake.WorldOffset.magnitude*Camera.main.pixelHeight/(Camera.main.orthographicSize*2);Check(pixels>=Camera.main.pixelHeight*.007&&pixels<=Camera.main.pixelHeight*.012,"enhanced special shake measured in screen pixels");yield return Capture("special-shake");g.CombatFeel.CancelStop();yield return new WaitForSecondsRealtime(.4f);Check(shake.WorldOffset.magnitude<.0001f&&!shake.Active,"shake settles with no residual camera displacement");
  for(int i=0;i<4;i++){g.CombatFeel.Cue(Vector2.zero,"break",Color.yellow,Vector2.up);yield return null;}yield return new WaitForEndOfFrame();Check(shake.Active,"rapid breaks across different frames trigger burst shake");g.SetPaused(true);yield return null;Check(!shake.Active,"pause resets enhanced shake");
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+",\"specialShakePixels\":"+pixels.ToString(System.Globalization.CultureInfo.InvariantCulture)+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口

- L36：`g.QASecondBox()`
- L38：`g.QASecondBox()`
- L39：`g.QASecondBox()`
- L40：`g.QASecondBox()`
- L41：`g.QASecondBox()`
- L42：`g.QASecondBox()`
- L43：`g.QASecondBox()`
- L44：`g.QASecondBox()`
- L45：`g.QASecondBox()`
- L46：`g.QASecondBox()`
- L49：`g.QASecondBox(3,1)`
- L50：`g.QASecondBox()`
- L51：`g.QASecondBox(1,1)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(){g.StartLevel(10);g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);g.QASevenClear();g.CollectionGoal[0]=g.CollectionGoal[1]=999;}
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
