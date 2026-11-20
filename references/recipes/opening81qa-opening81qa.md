# Opening81QA · Opening81QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Opening81QA.cs`
文件 SHA-256：`2b12984eea509d96857348a32caa05405b1197d22e3a4fa606d1f2b34b345aa8`
[原始源码快照](../source/Assets/Scripts/Opening81QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Dismiss · L21

方法 SHA-256：`86ea8e74180eeb1976ee45e13a8239d536e2de9a518ed8f4de2e3065d6be43df`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Opening81QA.Dismiss()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Opening81QA.cs:22` · `Opening81QA.Load` → `Dismiss()`
- `Assets/Scripts/Opening81QA.cs:31` · `Opening81QA.Start` → `Dismiss()`
- `Assets/Scripts/Opening81QA.cs:55` · `Opening81QA.Start` → `Dismiss()`
- `Assets/Scripts/Opening81QA.cs:61` · `Opening81QA.Start` → `Dismiss()`

```csharp
IEnumerator Dismiss(){app.Session.enabled=true;for(int n=0;n<100;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.025f);if(n>12&&!app.AlphabetIntroActive&&!app.Session.IsPaused)yield break;}}
```

### Load · L22

方法 SHA-256：`34628f5331b35f3ae0747a9710e7b6ae61ada70bd8d0433467388311e1d05945`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Opening81QA.Load(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Opening81QA.cs:44` · `Opening81QA.Start` → `Load(4)`
- `Assets/Scripts/Opening81QA.cs:49` · `Opening81QA.Start` → `Load(5)`

```csharp
IEnumerator Load(int index){app.QAStartLevel(index);yield return app.QAWaitForLevel();yield return Dismiss();g=app.Session;g.SetPaused(false);g.QAIgnoreFocusLoss=true;g.enabled=false;}
```

### Capture · L23

方法 SHA-256：`13a27882af4529ccf9111b3d1d0c0388662c34c2ebeda044bfe9d3f57d8fcc63`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Opening81QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Opening81QA.cs:40` · `Opening81QA.Start` → `Capture("level-"+(i+1).ToString("00"))`
- `Assets/Scripts/Opening81QA.cs:48` · `Opening81QA.Start` → `Capture("yarn-unravel-mid")`
- `Assets/Scripts/Opening81QA.cs:49` · `Opening81QA.Start` → `Capture("yarn-purple-wrap-peeling")`
- `Assets/Scripts/Opening81QA.cs:49` · `Opening81QA.Start` → `Capture("yarn-layer-one-after-peel")`
- `Assets/Scripts/Opening81QA.cs:57` · `Opening81QA.Start` → `Capture("level-22-container-fixed")`
- `Assets/Scripts/Opening81QA.cs:58` · `Opening81QA.Start` → `Capture("level-22-seven-hp-chest")`
- `Assets/Scripts/Opening81QA.cs:62` · `Opening81QA.Start` → `Capture("physical-volley")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,image);File.WriteAllBytes(dir+"/"+name+".png",image.EncodeToPNG());Destroy(image);rt.Release();Destroy(rt);}
```

### Start · L24

方法 SHA-256：`797deceef341ff6562618ab9f6fa9744e3fd0708940b2ae93c81626fcb594b18`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Opening81QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();int at=Array.IndexOf(args,"--qa-out");dir=at>=0?args[at+1]:Application.persistentDataPath+"/Opening81";Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  Application.logMessageReceived+=(message,stack,type)=>{if(type==LogType.Error||type==LogType.Exception||type==LogType.Assert){errors++;File.AppendAllText(dir+"/errors.txt",message+"\n"+stack+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();g=app.Session;
  Physics2D.simulationMode=SimulationMode2D.Script;
  Check(OpeningCampaign.Validate()>0,"ten authored layouts validate");
  for(int i=0;i<10;i++){
   app.QAStartLevel(i);yield return app.QAWaitForLevel();yield return Dismiss();g=app.Session;g.SetPaused(false);g.QAIgnoreFocusLoss=true;g.enabled=false;
   var stage=OpeningCampaign.Stage(i);Check(g.IsOpening&&g.ChainGridValid(false),"level "+(i+1)+" starts new sparse campaign");
   int authored=stage.rows.Sum(row=>row.Count(c=>c!='.'));Check(g.ChainOccupiedCells()==authored,"level "+(i+1)+" exact non-empty pattern count");
   foreach(var brick in g.CollectionBricks.Where(b=>b.Alive&&!g.OpeningYarnBrick(b))){var grid=brick.GetComponent<ChainGridPiece>();Check(brick.Definition.colorIndex==OpeningCampaign.Color(stage.rows[grid.Row][grid.Col].ToString())&&brick.CurrentHealth==1,"authored colour and one-hit ordinary "+(i+1)+"/"+grid.Col+","+grid.Row);}
   Check(g.ChainTools.All(t=>OpeningCampaign.Allows(i,t.Kind)),"authored special whitelist "+(i+1));
   foreach(var tool in g.ChainTools){var grid=tool.View.GetComponent<ChainGridPiece>();Check(tool.ColorIndex==OpeningCampaign.Color(stage.rows[grid.Row][grid.Col].ToString()),"authored special colour "+(i+1));}
   for(int tier=-1;tier<4;tier++)Check(g.QASevenRewardPacket(tier).All(kind=>OpeningCampaign.Allows(i,kind)),"reward generation whitelist "+(i+1)+" tier "+tier);
   if(i==4||i==5)Check(g.CollectionGoal[0]==6&&!g.RefillActive&&g.QAOpeningYarns().Length==6,"yarn only collection and stable teaching grid "+(i+1));
   else Check(g.ActiveJellyColors.Count()==1&&g.JellyGoal[OpeningCampaign.Color(stage.goal.color)]==stage.goal.count&&g.CollectionGoal.All(v=>v==0),"single actual-colour goal "+(i+1));
   yield return Capture("level-"+(i+1).ToString("00"));
  }
  Reset(0);int goal=g.JellyGoal[0];var extra=g.QAOpeningSpawn(0,0,0);Check(g.IsJellyTarget(extra)&&g.JellyGoal[0]==goal,"new matching-colour supply is collectible without inflating quota");g.QAOpeningConvert(extra);Tick(40);Check(g.JellyCollected[0]==0,"conversion does not credit ordinary collection");
  Reset(0);var goals=g.CollectionBricks.Where(g.IsJellyTarget).Take(12).ToArray();foreach(var b in goals)g.ApplyChainImpact(b,1,Vector2.up,b.transform.position,true);Tick(100);Check(g.JellyCollected[0]==12&&g.CollectionWon,"actual eliminated colour reaches exact collection quota");
  yield return Load(4);var single=g.QAOpeningYarns().First();Vector2 fixedPosition=single.Brick.GetComponent<Rigidbody2D>().position;
  g.ApplyChainImpact(single.Brick,99,Vector2.up,fixedPosition,true);g.SevenHit(single.Brick,99);Check(single.Layers==1&&single.Brick.Alive&&single.BounceCount==1,"ball and direct special damage cannot break yarn");
  var diagonal=g.CollectionBricks.FirstOrDefault(b=>b.Alive&&!g.OpeningYarnBrick(b)&&Mathf.Abs(Mathf.Abs(b.transform.position.x-fixedPosition.x)-CollectionLevels.Pitch)<.02f&&Mathf.Abs(Mathf.Abs(b.transform.position.y-fixedPosition.y)-CollectionLevels.Pitch)<.02f);if(diagonal!=null){g.SevenHit(diagonal);Check(single.Layers==1,"diagonal elimination does not loosen yarn");}
  var adjacent=g.QAOpeningNeighbours(single).First();g.ApplyChainImpact(adjacent,1,Vector2.up,adjacent.transform.position,true);Check(!single.Brick.Alive&&g.Collected[0]==0&&g.OpeningYarnPending>0,"single yarn starts unravel before credit");
  var effect=g.GetComponentsInChildren<OpeningYarnEffect>().First(e=>e.Pending);int frame=effect.VisibleFrame;g.SetPaused(true);Tick(20);Check(effect.VisibleFrame==frame&&g.Collected[0]==0,"pause freezes animation frame and collection");g.SetPaused(false);Tick(12);yield return Capture("yarn-unravel-mid");Tick(90);Check(g.Collected[0]>=1,"unravel and flight credit yarn once");
  yield return Load(5);var doubleYarn=g.QAOpeningYarns().First(y=>y.Layers==2);var neighbours=g.QAOpeningNeighbours(doubleYarn);Check(neighbours.Length>=2,"double yarn has independently eliminable neighbours");g.QAOpeningBatch(neighbours.Take(2).ToArray());Check(doubleYarn.Layers==1&&g.Collected[0]==0,"one effect clearing two neighbours removes only one layer");Tick(14);yield return Capture("yarn-purple-wrap-peeling");Tick(25);Check(doubleYarn.Brick.Alive&&doubleYarn.Layers==1,"layer-loss sequence returns to intact first-level body");yield return Capture("yarn-layer-one-after-peel");
  var finalNeighbour=g.QAOpeningNeighbours(doubleYarn).First();g.QAOpeningBatch(new[]{finalNeighbour});Tick(110);Check(!doubleYarn.Brick.Alive&&g.Collected[0]>=1,"second independent effect completes two-level collection");
  Reset(5);doubleYarn=g.QAOpeningYarns().First(y=>y.Layers==2);neighbours=g.QAOpeningNeighbours(doubleYarn);g.QAOpeningBatch(new[]{neighbours[0]});g.QAOpeningBatch(new[]{neighbours[1]});Check(!doubleYarn.Brick.Alive&&g.OpeningYarnPending>=2,"rapid independent hits queue both visual stages");Tick(130);Check(g.Collected[0]>=1,"queued layer loss then unravel credits once");
  Reset(5);doubleYarn=g.QAOpeningYarns().First(y=>y.Layers==2);g.QAOpeningBatch(new[]{g.QAOpeningNeighbours(doubleYarn).First()});Reset(0);Tick(100);Check(g.Collected.All(n=>n==0)&&g.OpeningYarnPending==0,"restart cancels pending yarn visuals and callbacks");
  for(int i=10;i<78;i++){Reset(i);Check(!g.IsOpening&&g.ChainGridValid(),"legacy level "+(i+1)+" retains full valid layout");if(i%12==0)yield return null;}
  for(int kind=1;kind<=3;kind++)app.Profile.MarkBoosterGuide((StreakBooster)kind);
  app.QAStartLevel(21);yield return app.QAWaitForLevel();yield return Dismiss();g=app.Session;g.SetPaused(false);g.enabled=false;
  foreach(var cube in g.GetComponentsInChildren<ChainCubeView>()){Check(cube.transform.Cast<Transform>().All(child=>child.name!="Inner square"&&!child.name.StartsWith("Hidden collection")),"closed container has no exposed miniature contents");Check(cube.transform.Find("Contained HP badge")!=null,"container has isolated readable HP badge");}
  yield return Capture("level-22-container-fixed");
  var chest=g.CollectionBricks.FirstOrDefault(b=>b.Alive&&ChainPowerRules.IsNest(b.Definition));if(chest!=null){if(chest.ShellIntact)g.ApplyChainImpact(chest,1,Vector2.up,chest.transform.position,true);while(chest.Alive&&chest.CurrentHealth>7)g.ApplyChainImpact(chest,1,Vector2.up,chest.transform.position,true);Check(chest.Alive&&chest.CurrentHealth==7&&!chest.ShellIntact,"damaged seven-HP chest keeps one clear badge and closed shell");yield return Capture("level-22-seven-hp-chest");}
  for(int row=0;row<4;row++)for(int col=0;col<6;col++){var sprite=ChainCubeArt.Frame(row,col);Check(sprite.rect.xMax<=sprite.texture.width+.01f&&sprite.rect.yMax<=sprite.texture.height+.01f,"container frame within runtime atlas "+row+"/"+col);}
  // Exercise actual five-ball Rigidbody contacts on the new production level.
  app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return Dismiss();g=app.Session;g.SetPaused(false);g.QAIgnoreFocusLoss=true;g.enabled=true;Physics2D.simulationMode=SimulationMode2D.FixedUpdate;
  var target=g.CollectionBricks.Where(b=>b.Alive).OrderBy(b=>b.transform.position.y).First();Check(g.TryShoot((Vector2)target.transform.position-g.LauncherPosition),"new level accepts physical volley");yield return new WaitForSecondsRealtime(4);Check(g.SpawnCount==5&&g.HitCount>0,"five actual balls contact and damage new authored bricks");yield return Capture("physical-volley");g.SetPaused(true);
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Debug.Log("OPENING81_QA checks="+checks+" failures="+failures+" errors="+errors);Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口

- L56：`cube.transform.Find("Contained HP badge")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(int index){g.StartLevel(index);g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);}
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
