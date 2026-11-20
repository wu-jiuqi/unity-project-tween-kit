# ChainCollectionQA · ChainCollectionQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChainCollectionQA.cs`
文件 SHA-256：`8f9293801b344600ef7fea51c852c33583e0a4d43235c5db1365dee3579110dd`
[原始源码快照](../source/Assets/Scripts/ChainCollectionQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L23

方法 SHA-256：`016a1d2b853a896ae4f7b0956b3d9e89fc0ab46a637f828e07027105018fc2c8`
时间来源：WaitForSecondsRealtime
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.ChainCollectionQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];if(Array.IndexOf(args,"--qa-width")>=0)width=int.Parse(args[Array.IndexOf(args,"--qa-width")+1]);if(Array.IndexOf(args,"--qa-height")>=0)height=int.Parse(args[Array.IndexOf(args,"--qa-height")+1]);Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();app.QASetViewport(new Vector2Int(width,height));app.Session.QAIgnoreFocusLoss=true;
  Check(RuntimeQA.IsIsolated,"isolated profile");Check(LevelCatalog.Count==40,"mainline catalog preserved");Check(!string.IsNullOrEmpty(RuleSelfTests.RunAll()),"legacy rule self tests");Check(!string.IsNullOrEmpty(LocalProfile.RunSelfTests()),"save self tests");
  app.OpenCollectionPicker();yield return new WaitForSecondsRealtime(.4f);yield return Capture("00-picker");
  app.QAStartLevel(53);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.6f);g=app.Session;
  Check(g.IsChainCollection&&g.CollectionGoal[0]==8&&g.CollectionGoal[1]==6,"raised goal starts at 8 fish and 6 toys");
  Check(g.CollectionBricks.Count(b=>b.Definition.juiceGroup==1)==4&&g.CollectionBricks.Count(b=>b.Definition.juiceGroup==2)==4,"visible authored opening targets");
  Check(!g.SelectCollectionTool(1)&&!g.SelectCollectionTool(2),"energy tools unavailable in chain mode");yield return Capture("01-opening");
  // Controlled art fixture: a cracked jar, opened box and both waiting tools.
  Damage(g.CollectionBricks.First(b=>b.Definition.juiceGroup==1),1);Damage(g.CollectionBricks.First(b=>b.Definition.juiceGroup==2),1);
  g.SpawnChainTool(1,new Vector2(-1.5f,.8f));g.SpawnChainTool(2,new Vector2(1.5f,.8f));yield return new WaitForSecondsRealtime(.2f);yield return Capture("02-damaged-and-tools");
  app.QAStartLevel(53);yield return app.QAWaitForLevel();g=app.Session;Empty();
  Add(new Vector2(.598f,1.9f),1,2);Add(new Vector2(1.638f,2.5f),1,2);Add(new Vector2(2.7f,4.4f),2,2);
  var demoDrum=g.SpawnChainTool(2,new Vector2(-2,.4f));g.SpawnChainTool(1,new Vector2(-1.134f,.9f));yield return new WaitForSecondsRealtime(.5f);yield return Capture("03-chain-before");
  g.ActivateChainTool(demoDrum,Vector2.up);yield return new WaitForSecondsRealtime(.12f);yield return Capture("04-chain-beam");yield return new WaitForSecondsRealtime(.7f);
  Check(g.ChainActivationCount==2&&g.Collected[0]==2,"visible drum-to-needle chain collects two jars");yield return Capture("05-chain-collected");
  g.StartLevel(53);Empty();Set("<CollectionRound>k__BackingField",7);Call("CreateChainBrick",3,3,0,0);Call("CreateChainBrick",5,3,0,0);Call("CreateChainBrick",2,1,1,0);Call("PlanChainRow");yield return new WaitForSecondsRealtime(.5f);yield return Capture("06-pressure-preview");
  app.QANavigate("home");var host=new GameObject("Chain fixtures");g=host.AddComponent<GameSession>();g.QAIgnoreFocusLoss=true;var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  Reset();yield return new WaitForSecondsRealtime(.4f);var jar=Add(Vector2.zero,1,3);Damage(jar,1);Check(jar.Alive&&jar.CurrentHealth==2,"jar first crack");Damage(jar,1);Check(jar.Alive&&jar.CurrentHealth==1,"jar second crack");Damage(jar,1);Check(!jar.Alive&&g.Collected[0]==0&&g.ChainEffectsPending,"third hit releases direct flight");Steps(40);Check(g.Collected[0]==1&&g.CollectionTreats.Count==0,"jar flight directly collects without basket");Damage(jar,10);Steps(40);Check(g.Collected[0]==1,"destroyed container cannot double collect");
  var box=Add(Vector2.zero,2,2);Damage(box,1);Check(box.CurrentHealth==1&&box.transform.Find("Container shell/Lid").localPosition.y>.4f,"first box hit opens lid");Damage(box,1);Steps(40);Check(g.Collected[1]==1,"second box hit collects toy");
  Reset();var ball=new GameObject("Counter fixture").AddComponent<BallBody>();for(int i=0;i<3;i++)Hit(Add(new Vector2(i,0)),ball);Check(g.ChainRatingTier==-1&&g.ChainTools.Count==0,"three direct kills do not rate");Hit(Add(new Vector2(0,1)),ball);Check(g.ChainRatingTier==0&&g.NextChainRowTools.Count(x=>x>0)==1,"fourth direct kill queues Nice reward");Check(g.ChainTools.Count==0,"rating reward waits for next row");for(int i=0;i<8;i++)Hit(Add(Vector2.zero),ball);Check(g.ChainRatingTier==2&&g.NextChainRowTools.Count(x=>x>0)==2,"highest rating packet replaces lower tiers");
  Reset();g.OnChainWall(ball,0);for(int i=0;i<8;i++)Hit(Add(Vector2.zero),ball);Check(g.ChainRatingTier==1&&g.NextChainRowTools.Contains(2),"banked direct kills use the same Great reward threshold");
  Reset();g.OnChainWall(ball,0);Hit(Add(Vector2.zero),ball);g.OnChainWall(ball,1);Hit(Add(Vector2.one),ball);Check(g.ChainRatingBreaks==2&&g.ChainRatingTier==-1,"two kills with a ceiling bounce do not earn a rating");
  Reset();var tough=Add(Vector2.zero,0,2);Hit(tough,ball);Check(tough.Alive&&tough.CurrentHealth==1&&g.ChainRatingBreaks==0,"partial damage earns no kill credit");Hit(tough,ball);Check(!tough.Alive&&g.ChainRatingBreaks==1,"destruction earns exactly one rating kill");
  Reset();var needle=g.SpawnChainTool(1,Vector2.zero);var jarA=Add(new Vector2(1,0),1,2);var jarB=Add(new Vector2(2,0),1,2);var behind=Add(new Vector2(-1,0),1,2);var normal=Add(new Vector2(3,0),0,9);
  g.ActivateChainTool(needle,Vector2.right);Check(g.ChainTools.Contains(needle)&&jarA.Alive,"generation protection blocks immediate activation");Arm();g.SetPaused(true);g.ActivateChainTool(needle,Vector2.right);Check(jarA.Alive&&g.ChainActivationCount==0,"pause blocks tools");g.SetPaused(false);g.ActivateChainTool(needle,Vector2.right);Check(!jarA.Alive&&!jarB.Alive&&!normal.Alive&&behind.Alive,"forward needle clears ordinary brick and damages two aligned jars");Check(g.ChainGeneratedCount==1,"tool destruction cannot generate tools");Steps(40);Check(g.Collected[0]==2,"two needle kills collect twice");
  Reset();needle=g.SpawnChainTool(1,Vector2.zero);var drum=g.SpawnChainTool(2,new Vector2(1,0));Arm();g.ActivateChainTool(needle,Vector2.right);Check(g.ChainActivationCount==2&&g.ChainMinis.Count==6&&g.ChainDepth==2,"needle activates drum producing exactly six minis");Check(g.ChainTools.Count==0&&g.ChainGeneratedCount==2,"chain consumes tools without generating more");Steps(160);Check(g.ChainMinis.Count==0,"all minis expire");
  Reset();drum=g.SpawnChainTool(2,new Vector2(-1,0));needle=g.SpawnChainTool(1,new Vector2(-.13f,.5f));Arm();g.ActivateChainTool(drum,Vector2.up);Steps(12);Check(g.ChainActivationCount==2,"drum mini sweeps into needle");
  Reset();var danger=Add(new Vector2(0,-3.7f));needle=g.SpawnChainTool(1,new Vector2(0,-3.8f));Arm();Set("collectionRoundPending",true);Steps(80);Check(!danger.Alive&&g.IsPlaying&&g.CollectionRound==2,"bottom auto needle clears danger before loss");
  Reset();Add(new Vector2(2,-3.7f));Set("collectionRoundPending",true);Steps(80);Check(!g.IsPlaying&&!Rules.Won,"surviving ordinary brick at danger loses");
  Reset();g.Collected[0]=g.CollectionGoal[0]-1;g.Collected[1]=g.CollectionGoal[1];jar=Add(Vector2.zero,1,1);Add(new Vector2(2,-3.7f));Damage(jar,1);Set("collectionRoundPending",true);Steps(80);Check(!g.IsPlaying&&Rules.Won&&g.CollectionRound==1,"final loot wins before dangerous descent");
  Reset();g.Collected[0]=g.CollectionGoal[0];Call("PlanChainRow");Check(!g.NextCollectionRow.Contains(1)&&g.NextCollectionRow.Contains(2),"supply excludes completed kind and guarantees missing kind");
  g.StartLevel(53);Check(g.CollectionBricks.Count(b=>b.Definition.kind==BrickKind.Normal&&b.Definition.juiceGroup==0&&b.CurrentHealth==1)==7&&g.CollectionBricks.Any(b=>b.Definition.kind==BrickKind.RearWeak),"fragile tutorial row and rear-guard opening");
  Check(ChainCollectionBalance.Tier(2)==0&&ChainCollectionBalance.Tier(3)==1&&ChainCollectionBalance.Tier(7)==2,"authored pressure boundaries");
  Check(ChainCollectionBalance.IsBreather(6)&&ChainCollectionBalance.IsBreather(10)&&!ChainCollectionBalance.IsBreather(7),"predictable relief rounds");
  Set("<CollectionRound>k__BackingField",3);Call("PlanChainRow");Check(g.NextChainRowHealth.Where((hp,c)=>g.NextCollectionRow[c]<0).Any(hp=>hp==5),"middle supply includes five-hit jellies");
  int dense=g.NextCollectionRow.Count(x=>x!=0);Set("<CollectionRound>k__BackingField",5);Call("PlanChainRow");Check(g.NextChainRowHealth.Where((hp,c)=>g.NextCollectionRow[c]<0).All(hp=>hp==3)&&g.NextCollectionRow.Count(x=>x!=0)<dense,"relief supply has fewer and weaker ordinary bricks");
  Reset();Set("<CollectionRound>k__BackingField",7);var moved=Add(new Vector2(2,2));var movingTool=g.SpawnChainTool(1,new Vector2(-2,2));Arm();Call("PlanChainRow");var preview=(int[])g.NextChainRowHealth.Clone();Set("collectionRoundPending",true);Steps(80);
  Check(Mathf.Abs(moved.transform.position.y-(2-CollectionLevels.Pitch*1.3f))<.005f&&Mathf.Abs(movingTool.Position.y-moved.transform.position.y)<.005f,"late descent moves bricks and tools by 1.3 actual rows");
  Check(g.CollectionBricks.Where(b=>b.Alive&&Mathf.Abs(b.transform.position.y-CollectionLevels.Top)<.01f).All(b=>b.CurrentHealth==preview[b.Definition.referenceCol])&&g.CollectionRound==8,"spawned row health matches the cached preview");
  Reset();Set("<CollectionRound>k__BackingField",3);moved=Add(new Vector2(2,2));Set("collectionRoundPending",true);Steps(80);Check(Mathf.Abs(moved.transform.position.y-(2-CollectionLevels.Pitch*1.15f))<.005f,"middle descent moves 1.15 actual rows");
  Reset();Set("<CollectionRound>k__BackingField",6);moved=Add(new Vector2(2,2));Set("collectionRoundPending",true);Steps(80);Check(Mathf.Abs(moved.transform.position.y-(2-CollectionLevels.Pitch))<.005f,"relief restores one-row descent");
  g.StartLevel(53);yield return new WaitForSecondsRealtime(.4f);var first=g.CollectionBricks.First();float y=first.transform.position.y;Check(g.TryShoot(Vector2.up),"normal shot accepted");Steps(25);Check(g.SpawnCount==5&&g.Shots==int.MaxValue,"five balls with unlimited rounds");Check(first.transform.position.y==y&&!g.TryShoot(Vector2.up),"bricks stay fixed and second volley blocked");g.SetPaused(true);int step=g.PhysicsStep;Steps(20);Check(g.PhysicsStep==step,"pause freezes simulation");g.SetPaused(false);
  int bestRatingAngle=0;for(int angle=20;angle<=160;angle+=10){g.StartLevel(53);yield return new WaitForSecondsRealtime(.35f);float a=angle*Mathf.Deg2Rad;g.TryShoot(new Vector2(Mathf.Cos(a),Mathf.Sin(a)));int n=0;do{Steps(1);n++;if(g.RecallAvailable)g.RecallBalls();}while(g.IsPlaying&&(!g.CollectionReady||n<2)&&n<3000);File.AppendAllText(Path.Combine(dir,"opening-angles.txt"),$"angle={angle} generated={g.ChainGeneratedCount} activated={g.ChainActivationCount} breaks={g.BreakCount}\n");if(g.ChainRatingTier>=0)bestRatingAngle=angle;}
  Check(bestRatingAngle>0,"opening supports a physical rated volley");
  File.WriteAllText(Path.Combine(dir,"physical-play.txt"),"");
  for(int policy=0;policy<3;policy++){
  g.StartLevel(53);yield return new WaitForSecondsRealtime(.4f);int turns=0;while(g.IsPlaying&&turns<25){
   Vector2 direction=Vector2.up;var power=g.ChainTools.OrderBy(t=>t.Position.y).FirstOrDefault();var target=g.CollectionBricks.Where(b=>b.Alive).OrderBy(b=>b.transform.position.y).ThenBy(b=>Mathf.Abs(b.transform.position.x)).FirstOrDefault();
   if(policy==1&&target!=null&&target.transform.position.y>-2)target=g.CollectionBricks.Where(b=>b.Alive).OrderByDescending(b=>b.Definition.juiceGroup>0).ThenBy(b=>b.transform.position.y).FirstOrDefault();
   if(turns==0&&bestRatingAngle>0){float a=(policy==0?bestRatingAngle:policy==1?60:120)*Mathf.Deg2Rad;direction=new Vector2(Mathf.Cos(a),Mathf.Sin(a));}
   else if(power!=null)direction=power.Position-g.LauncherPosition;else if(target!=null){Vector2 p=target.transform.position;bool bank=policy==2?turns%3!=0:turns%3==0;direction=bank?new Vector2((p.x<0?-8.2f:8.2f)-p.x,p.y)-g.LauncherPosition:p-g.LauncherPosition;}
   Check(g.TryShoot(direction),"physical volley "+turns);turns++;int n=0;do{Steps(1);n++;if(g.RecallAvailable)g.RecallBalls();}while(g.IsPlaying&&(!g.CollectionReady||n<2)&&n<3000);Check(n<3000,"physical volley settles "+turns);yield return new WaitForSecondsRealtime(.35f);
  }
  File.AppendAllText(Path.Combine(dir,"physical-play.txt"),$"policy={policy} turns={turns} won={Rules.Won} fish={g.Collected[0]} toys={g.Collected[1]} generated={g.ChainGeneratedCount} activated={g.ChainActivationCount}\n");Check(!g.IsPlaying,"physical play reaches outcome policy "+policy);
  }
  for(int i=0;i<53;i++){g.StartLevel(i);Check(!g.IsChainCollection&&g.ChainTools.Count==0&&g.ChainMinis.Count==0,"legacy stage reset "+i);}g.StopLevel();Destroy(ball.gameObject);Destroy(host);Physics2D.simulationMode=mode;
  File.AppendAllText(Path.Combine(dir,"checks.txt"),$"RESULT checks={checks} failures={failures} errors={errors}\n");Application.Quit(failures+errors==0?0:1);
 }
```

### Capture · L84

方法 SHA-256：`95b1a8c1ef930067c6708170d0e8dad02346794ba542de51f426b5cd46bc2b08`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainCollectionQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainCollectionQA.cs:28` · `ChainCollectionQA.Start` → `Capture("00-picker")`
- `Assets/Scripts/ChainCollectionQA.cs:32` · `ChainCollectionQA.Start` → `Capture("01-opening")`
- `Assets/Scripts/ChainCollectionQA.cs:35` · `ChainCollectionQA.Start` → `Capture("02-damaged-and-tools")`
- `Assets/Scripts/ChainCollectionQA.cs:38` · `ChainCollectionQA.Start` → `Capture("03-chain-before")`
- `Assets/Scripts/ChainCollectionQA.cs:39` · `ChainCollectionQA.Start` → `Capture("04-chain-beam")`
- `Assets/Scripts/ChainCollectionQA.cs:40` · `ChainCollectionQA.Start` → `Capture("05-chain-collected")`
- `Assets/Scripts/ChainCollectionQA.cs:41` · `ChainCollectionQA.Start` → `Capture("06-pressure-preview")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var cam=Camera.main;var target=new RenderTexture(width,height,24);target.Create();var old=cam.targetTexture;cam.targetTexture=target;cam.rect=new Rect(0,0,1,1);cam.aspect=(float)width/height;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;Canvas.ForceUpdateCanvases();var design=(RectTransform)typeof(AppUI).GetField("designRoot",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).GetValue(app);var safe=(RectTransform)design.parent;design.localScale=Vector3.one*Mathf.Min(safe.rect.width/1080f,safe.rect.height/2340f);Canvas.ForceUpdateCanvases();cam.Render();var prev=RenderTexture.active;RenderTexture.active=target;var texture=new Texture2D(width,height,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,width,height),0,0);texture.Apply();File.WriteAllBytes(Path.Combine(dir,name+".png"),texture.EncodeToPNG());RenderTexture.active=prev;cam.targetTexture=old;Destroy(texture);target.Release();Destroy(target);}
```

## 对象层级和素材入口

- L44：`box.transform.Find("Container shell/Lid")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(){g.StartLevel(53);Empty();}
```

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
GameSession g;
```
```csharp
int width=540,height=1170;
```
```csharp
const BindingFlags Hidden=BindingFlags.NonPublic|BindingFlags.Instance;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
