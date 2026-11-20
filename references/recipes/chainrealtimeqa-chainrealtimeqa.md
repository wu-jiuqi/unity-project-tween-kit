# ChainRealtimeQA · ChainRealtimeQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChainRealtimeQA.cs`
文件 SHA-256：`c931aba7715bbef11247222d6b6af6cbba2f2b084730f01f71057bf9ba3bb66a`
[原始源码快照](../source/Assets/Scripts/ChainRealtimeQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Level · L27

方法 SHA-256：`f17cac77d8245dec058bd595a65d11f0db10ea7e211eebba0537a60de123d797`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainRealtimeQA.Level()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainRealtimeQA.cs:31` · `ChainRealtimeQA.Start` → `Level()`
- `Assets/Scripts/ChainRealtimeQA.cs:38` · `ChainRealtimeQA.Start` → `Level()`
- `Assets/Scripts/ChainRealtimeQA.cs:43` · `ChainRealtimeQA.Start` → `Level()`
- `Assets/Scripts/ChainRealtimeQA.cs:50` · `ChainRealtimeQA.Start` → `Level()`
- `Assets/Scripts/ChainRealtimeQA.cs:51` · `ChainRealtimeQA.Start` → `Level()`
- `Assets/Scripts/ChainRealtimeQA.cs:52` · `ChainRealtimeQA.Start` → `Level()`
- `Assets/Scripts/ChainRealtimeQA.cs:53` · `ChainRealtimeQA.Start` → `Level()`
- `Assets/Scripts/ChainRealtimeQA.cs:56` · `ChainRealtimeQA.Start` → `Level()`
- `Assets/Scripts/ChainRealtimeQA.cs:57` · `ChainRealtimeQA.Start` → `Level()`
- `Assets/Scripts/ChainRealtimeQA.cs:59` · `ChainRealtimeQA.Start` → `Level()`
- `Assets/Scripts/ChainRealtimeQA.cs:61` · `ChainRealtimeQA.Start` → `Level()`
- `Assets/Scripts/ChainRealtimeQA.cs:64` · `ChainRealtimeQA.Start` → `Level()`

```csharp
IEnumerator Level(){app.QAStartLevel(53);yield return app.QAWaitForLevel();g=app.Session;yield return new WaitForSecondsRealtime(.4f);}
```

### Start · L28

方法 SHA-256：`0061fe608b7a34fd83649dd4c87fc22aa6aa9b45f48df9dd623332645b17d655`
时间来源：Time.fixedDeltaTime, WaitForSecondsRealtime
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.ChainRealtimeQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];if(Array.IndexOf(args,"--qa-height")>=0)height=int.Parse(args[Array.IndexOf(args,"--qa-height")+1]);Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();app.QASetViewport(new Vector2Int(540,height));app.Session.QAIgnoreFocusLoss=true;yield return Level();
  var chests=g.CollectionBricks.Where(b=>ChainPowerRules.IsNest(b.Definition)).ToArray();Check(g.ChainGridValid()&&chests.Length==4,"opening has four large protected chests on a full board");Check(chests.All(b=>b.CurrentHealth==30)&&chests.Count(b=>b.ShellIntact)==3&&chests.Count(b=>b.Definition.kind==BrickKind.RearWeak)==1,"three armored chests and one rear-locked chest retain thirty body HP");Check(!g.CollectionBricks.Any(b=>b.Definition.juiceGroup>0)&&g.ChainSupplyCount(1)==8&&g.ChainSupplyCount(2)==6,"all eight fish and six toys start inside the large chests");Check(g.CollectionBricks.Where(b=>b.Alive).All(b=>b.GetComponent<Rigidbody2D>()!=null&&b.GetComponent<Rigidbody2D>().bodyType==RigidbodyType2D.Kinematic&&b.GetComponent<BoxCollider2D>().enabled),"every tile uses an enabled kinematic collider");
  Check(ChainCollectionBalance.OrdinaryHealth(1,4)==4&&ChainCollectionBalance.JarHealth==7&&ChainCollectionBalance.BoxHealth==6&&ChainCollectionBalance.ArmorHealth==8,"ordinary, goal and armor body durability increased");
  var text=app.GetComponentsInChildren<TMP_Text>().Where(t=>t.gameObject.activeInHierarchy).Select(t=>t.text).ToArray();Check(!text.Any(t=>t.Contains("HITS ")||t.Contains("TOOLS ")||t.Contains("CYAN:")||t.Contains("GREEN BACK")||t.Contains("2 × 2")||t.Contains("ON REFILL")),"footer instructions, statistics and chest size labels are absent");Check(app.GetComponentsInChildren<ChainRatingView>().Length==1&&app.GetComponentsInChildren<InGameFeedbackView>().Length==1,"chain evaluation and danmaku presenters are both attached");yield return Capture("00-protected-chests-clean-ui");
  Check(g.TryShoot(Vector2.up),"first real volley launches");yield return new WaitForSecondsRealtime(.6f);Check(g.TryShoot(new Vector2(.2f,1)),"second volley launches before old balls must return");yield return new WaitForSecondsRealtime(.7f);var feedback=FindFirstObjectByType<InGameFeedbackView>();Check(feedback.BulletVisible&&feedback.Caption.gameObject.activeInHierarchy&&!string.IsNullOrEmpty(feedback.Caption.text),"the actual scrolling danmaku is visible after shooting");Check(feedback.VisibleTier==-1&&!feedback.State.CheerActive,"danmaku does not create a duplicate Nice/Amazing layer");Check(g.Shots==18,"two volleys consume two launch steps");yield return new WaitForSecondsRealtime(1.2f);yield return Capture("01-live-danmaku");
  // Controlled production collection for flight/impact visual review; reset before physical fixtures.
  Call("CreateBrick",new BrickSpawn{position=new Vector2(0,-2.5f),size=Vector2.one*.76f,hp=1,kind=BrickKind.Normal,juiceGroup=1,colorIndex=3},0f);Damage(g.CollectionBricks.Last());yield return new WaitForSecondsRealtime(.25f);yield return Capture("01a-collection-flight");yield return new WaitForSecondsRealtime(.4f);yield return Capture("01b-collection-impact");
  var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;yield return Level();Empty();yield return null;
  var nest=g.SpawnChainNest(ChainGridRules.Center(0,0,2),new[]{1,2,1,2});Check(g.TryShoot(Vector2.up),"group accounting first launch");Steps(1);var first=Balls().Single();int firstId=first.ChainBurstId;for(int i=0;i<4;i++)Hit(nest,first);Check(Groups[firstId].Hits==4&&Groups[firstId].Tier==0,"nonlethal hits and opening armor count for that volley only");Check(!g.TryShoot(Vector2.up)&&g.Shots==19,"pending five-ball emission prevents overlapping emission queues");Seconds(.65f);Check(g.PendingBurstBalls==0&&g.ActiveBalls>0&&g.CollectionReady,"launch gate reopens while old balls are still flying");Check(g.TryShoot(Vector2.up),"another volley can overlap active balls");Steps(1);var second=Balls().First(b=>b.ChainBurstId!=firstId);int secondId=second.ChainBurstId;for(int i=0;i<8;i++)Hit(nest,second);Check(Groups[firstId].Hits==4&&Groups[secondId].Hits==8&&Groups[secondId].Tier==1,"overlapping volley evaluations stay independent");
  int active=g.ActiveBalls;first.ChainBornAt=-20;Steps(1);Check(g.ActiveBalls==active-1&&g.ChainAutoReturns==1&&Balls().Any(b=>b.ChainBurstId==secondId),"expiry removes only the old ball and cannot reset a newer volley");foreach(var b in Balls().Where(b=>b.ChainBurstId==firstId).ToArray())g.LoseBall(b,b.Body.position);Check(!Groups.ContainsKey(firstId)&&Groups.ContainsKey(secondId)&&Rewards.SequenceEqual(new[]{1}),"returning the first group queues its laser while the second group continues");
  g.LoseBall(second,second.Body.position);Check(Groups[secondId].Remaining==4&&Rewards.Count==1,"an emitted ball returning cannot prematurely reward its four pending siblings");Seconds(.5f);foreach(var b in Balls().Where(b=>b.ChainBurstId==secondId).ToArray())g.LoseBall(b,b.Body.position);Check(Groups.Count==0&&Rewards.Contains(2),"the second group queues its own bomb only after all five finish");Check(g.Shots==18&&g.SpawnCount==10,"two overlapping groups still emit exactly ten balls for two steps");
  // Timers belong to each unsupported piece, not to a board-wide cooldown.
  yield return Level();Empty();yield return null;for(int r=0;r<9;r++)for(int c=0;c<9;c++)Add(c,r,9);var early=g.CollectionBricks.Single(b=>b.GetComponent<ChainGridPiece>().Col==2&&b.GetComponent<ChainGridPiece>().Row==7);var support=g.CollectionBricks.Single(b=>b.GetComponent<ChainGridPiece>().Col==2&&b.GetComponent<ChainGridPiece>().Row==8);Damage(support);Steps(1);float earlyAt=early.GetComponent<ChainGridPiece>().FallAt;Vector2 earlyStart=early.GetComponent<Rigidbody2D>().position;
  Seconds(1);var late=g.CollectionBricks.Single(b=>b.Alive&&b.GetComponent<ChainGridPiece>().Col==6&&b.GetComponent<ChainGridPiece>().Row==7);support=g.CollectionBricks.Single(b=>b.Alive&&b.GetComponent<ChainGridPiece>().Col==6&&b.GetComponent<ChainGridPiece>().Row==8);Damage(support);Steps(1);float lateAt=late.GetComponent<ChainGridPiece>().FallAt;
  Check(Mathf.Abs(early.GetComponent<ChainGridPiece>().FallAt-earlyAt)<.001f&&lateAt-earlyAt>.9f,"a later gap starts its own countdown without resetting the earlier piece");
  Seconds(.8f);Check(Vector2.Distance(earlyStart,early.GetComponent<Rigidbody2D>().position)<.001f&&early.GetComponent<BoxCollider2D>().enabled,"unsupported bricks stay still and collide during their two second delay");
  g.SetPaused(true);Steps(120);Check(early.GetComponent<ChainGridPiece>().FallAt==earlyAt&&Vector2.Distance(earlyStart,early.GetComponent<Rigidbody2D>().position)<.001f,"pause consumes no independent fall countdown");g.SetPaused(false);
  Seconds(.3f);Check(early.GetComponent<Rigidbody2D>().position.y<earlyStart.y-.01f&&Mathf.Abs(late.GetComponent<Rigidbody2D>().position.y-ChainGridRules.Center(6,7).y)<.001f,"early tile falls while the later unsupported tile is still waiting");
  Seconds(1.1f);Check(late.GetComponent<Rigidbody2D>().position.y<ChainGridRules.Center(6,7).y-.01f,"later tile falls on its own deadline");
  yield return Level();Empty();yield return null;for(int r=0;r<9;r++)for(int c=0;c<9;c++)Add(c,r,9);early=g.CollectionBricks.Single(b=>b.GetComponent<ChainGridPiece>().Col==4&&b.GetComponent<ChainGridPiece>().Row==5);support=g.CollectionBricks.Single(b=>b.GetComponent<ChainGridPiece>().Col==4&&b.GetComponent<ChainGridPiece>().Row==6);Damage(support);Steps(1);Seconds(1);late=g.CollectionBricks.Single(b=>b.Alive&&b.GetComponent<ChainGridPiece>().Col==4&&b.GetComponent<ChainGridPiece>().Row==7);support=g.CollectionBricks.Single(b=>b.Alive&&b.GetComponent<ChainGridPiece>().Col==4&&b.GetComponent<ChainGridPiece>().Row==8);Damage(support);Steps(1);Seconds(1.4f);Check(PhysicalClear()&&early.GetComponent<Rigidbody2D>().position.y>=ChainGridRules.Center(4,6).y-.001f&&Mathf.Abs(late.GetComponent<Rigidbody2D>().position.y-ChainGridRules.Center(4,7).y)<.001f,"early upper tile stops above the later waiting support without physical overlap");Seconds(4);Check(g.ChainGridValid()&&PhysicalClear(),"staggered same-column timers settle to a full physically separated grid");
  yield return Level();Empty();yield return null;var waiting=Add(4,7,1);Call("ScheduleChainGravity");Steps(1);Damage(waiting);Check(!waiting.Alive,"a brick can be destroyed before its fall delay expires");
  yield return Level();var lockBox=g.CollectionBricks.Single(b=>ChainPowerRules.IsNest(b.Definition)&&b.Definition.kind==BrickKind.RearWeak);var lockBody=lockBox.GetComponent<Rigidbody2D>();Vector2 lockPos=lockBody.position;g.ApplyChainImpact(lockBox,999,Vector2.up,lockPos+Vector2.down*.82f,false);Check(lockBox.Alive&&lockBox.CurrentHealth==30,"rear-locked large chest is immune to front attacks");g.ApplyChainImpact(lockBox,1,Vector2.down,lockPos+Vector2.up*.82f,false);Check(!lockBox.Alive&&g.ChainNestSplits==1&&g.Collected.All(v=>v==0),"one hit on the large chest physical top releases uncollected protected contents");
  yield return Level();g.SetAim((ChainGridRules.Center(2,8)-g.LauncherPosition).normalized);Check(g.ChainPreviewCells>1,"aiming at a laser previews its row and column footprint");g.SetAim(null);Check(g.ChainPreviewCells==0,"power footprint disappears when aiming ends");
  var authoredLaser=g.ChainTools.Single(t=>t.Kind==1&&t.View.GetComponent<ChainGridPiece>().Col==2);int activations=g.ChainActivationCount;g.ActivateChainTool(authoredLaser,Vector2.up);Check(g.ChainActivationCount>=activations+2&&g.ChainDepth>=2,"authored laser route triggers a bomb in the same column");
  // Actual physics collision with a brick that is currently descending.
  yield return Level();Empty();yield return null;var falling=Add(4,7,1);var rb=falling.GetComponent<Rigidbody2D>();Check(g.TryShoot(Vector2.up),"moving-brick collision fixture launches a real ball");Steps(1);var liveBall=Balls().Single();liveBall.ResumeChainFlight(new Vector2(3,-2),Vector2.right*g.CurrentLevel.ballSpeed);Call("ScheduleChainGravity");Seconds(.45f);foreach(var sibling in Balls().Where(b=>b!=liveBall))g.LoseBall(sibling,sibling.Body.position);Seconds(1.57f);liveBall.ResumeChainFlight(new Vector2(0,-1.9f),Vector2.up*g.CurrentLevel.ballSpeed);Steps(1);Check(g.ChainFalling&&g.ActiveBalls>0&&falling.GetComponent<BoxCollider2D>().enabled&&liveBall.Body.simulated,"falling starts during flight without disabling either collision body");bool movingBreak=false;int ticks=0;while(falling!=null&&falling.Alive&&ticks++<40){float beforeY=rb.position.y;bool descending=beforeY<ChainGridRules.Center(4,7).y-.001f&&beforeY>ChainGridRules.Center(4,8).y+.001f;Steps(1);if(!falling.Alive&&descending)movingBreak=true;}Check(movingBreak&&g.ChainMovingBreaks>0,"a real flying ball destroys a physically moving falling brick");Check(liveBall.Body.linearVelocity.y<0,"moving-tile collision physically reflects the ball");Check(g.Shots==19&&g.ChainSafeRepositions==0,"live falling costs no extra step and never warps the ball");yield return Capture("02-moving-brick-collision");
  yield return Level();Empty();yield return null;var rear=Add(4,7,1,BrickKind.RearWeak);g.TryShoot(Vector2.up);Steps(1);var rearBall=Balls().Single();rearBall.ResumeChainFlight(new Vector2(3,-2),Vector2.right*g.CurrentLevel.ballSpeed);Call("ScheduleChainGravity");Seconds(.45f);foreach(var sibling in Balls().Where(b=>b!=rearBall))g.LoseBall(sibling,sibling.Body.position);Seconds(1.57f);rearBall.ResumeChainFlight(new Vector2(0,.65f),Vector2.down*g.CurrentLevel.ballSpeed);ticks=0;while(rear.Alive&&ticks++<40)Steps(1);Check(!rear.Alive&&g.ChainRearHits==1,"a real downward ball shatters a moving rear guard through its physical top");
  // Splitting a falling armored chest must preserve its four logical destination cells.
  yield return Level();Empty();yield return null;nest=g.SpawnChainNest(ChainGridRules.Center(3,2,2),new[]{1,2,1,0});Call("ScheduleChainGravity");Seconds(2.02f);var grid=nest.GetComponent<ChainGridPiece>();int col=grid.Col,row=grid.Row;Damage(nest);Check(nest.CurrentHealth==30&&!nest.ShellIntact,"outer armor absorbs an entire hit without body overflow");var oldIds=new HashSet<int>(g.CollectionBricks.Select(b=>b.Id));Damage(nest);var kids=g.CollectionBricks.Where(b=>b.Alive&&b.Definition.juiceGroup>0&&!oldIds.Contains(b.Id)).ToArray();Check(kids.Length==3&&kids.All(b=>b.CurrentHealth==(b.Definition.juiceGroup==1?7:6))&&kids.Any(b=>b.ShellIntact),"a falling chest releases tougher goal boxes with some inner armor");Check(kids.All(b=>{var p=b.GetComponent<ChainGridPiece>();return p.Col>=col&&p.Col<col+2&&p.Row>=row&&p.Row<row+2;}),"falling chest children inherit its destination footprint");Check(g.Collected.All(v=>v==0),"opening a protected chest does not collect the contents");Seconds(8);Check(g.ChainGridValid(),"replanning after a moving chest splits fills the board without duplicate cells");yield return Capture("03-released-protected-contents");
  // Sustained overlapping fire exercises live refill and collision over many simultaneous balls.
  yield return Level();int peak=0;bool valid=true,physical=true;for(int shot=0;shot<6;shot++){float a=new[]{90f,65f,115f,74f,106f,90f}[shot]*Mathf.Deg2Rad;Check(g.TryShoot(new Vector2(Mathf.Cos(a),Mathf.Sin(a))),"continuous physical launch "+shot);for(int t=0;t<Mathf.CeilToInt(.65f/Time.fixedDeltaTime);t++){Steps(1);peak=Mathf.Max(peak,g.ActiveBalls);valid&=g.ChainGridValid(false);physical&=PhysicalClear();}}
  Check(peak>5&&g.SpawnCount==30&&g.Shots==14,"six rapid launches create overlapping groups of thirty balls for six steps");Check(physical,"all live tile bodies remain physically separated during rapid fire and delayed falls");Check(valid&&g.ChainRefills>0,"logical occupancy remains unique while shooting and falling simultaneously");var positions=g.CollectionBricks.Where(b=>b.Alive).ToDictionary(b=>b,b=>b.GetComponent<Rigidbody2D>().position);g.SetPaused(true);Steps(20);Check(positions.All(k=>k.Key==null||!k.Key.Alive||Vector2.Distance(k.Value,k.Key.GetComponent<Rigidbody2D>().position)<.001f),"pause freezes kinematic falling tiles");g.SetPaused(false);ticks=0;while(g.IsPlaying&&(g.ActiveBalls>0||g.PendingBurstBalls>0||!g.ChainGridReady||g.ChainEffectsPending)&&ticks++<2200){Steps(1);if(ticks%200==0)yield return null;}Check(ticks<2200&&g.ActiveBalls==0&&g.PendingBurstBalls==0&&g.ChainGridValid(),"overlapping fire eventually settles to a complete nonoverlapping board");Check(g.ChainLiveGroups==0&&g.Shots==14,"all per-group accounting clears without extra resource cost");yield return Capture("04-after-continuous-fire");
  // Exhaustion cannot lose while balls or active gravity still need to resolve.
  yield return Level();typeof(RuleState).GetField("<Shots>k__BackingField",Hidden).SetValue(Rules,1);Check(g.TryShoot(Vector2.up),"last step launch accepted");Seconds(.55f);Check(g.IsPlaying&&g.Shots==0&&g.ActiveBalls>0,"last step remains active while its balls are flying");foreach(var b in Balls())b.ChainBornAt=-20;ticks=0;while(g.IsPlaying&&ticks++<1500)Steps(1);Check(!g.IsPlaying&&!Rules.Won&&g.ActiveBalls==0&&g.PendingBurstBalls==0&&!g.ChainFalling,"failure waits for last balls, effects and live gravity");
  Physics2D.simulationMode=mode;app.QANavigate("home");yield return null;Check(g.ChainLiveGroups==0&&FindObjectsByType<ChainGridPiece>(FindObjectsSortMode.None).Length==0,"leaving clears all live groups and tiles");
  File.AppendAllText(Path.Combine(dir,"checks.txt"),$"RESULT checks={checks} failures={failures} errors={errors}\n");Application.Quit(failures+errors==0?0:1);
 }
```

### Capture · L68

方法 SHA-256：`dadb7364acb3ff3ad34707040b04b793772cfb700e41cbaaf35a34fa9d388795`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainRealtimeQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainRealtimeQA.cs:34` · `ChainRealtimeQA.Start` → `Capture("00-protected-chests-clean-ui")`
- `Assets/Scripts/ChainRealtimeQA.cs:35` · `ChainRealtimeQA.Start` → `Capture("01-live-danmaku")`
- `Assets/Scripts/ChainRealtimeQA.cs:37` · `ChainRealtimeQA.Start` → `Capture("01a-collection-flight")`
- `Assets/Scripts/ChainRealtimeQA.cs:37` · `ChainRealtimeQA.Start` → `Capture("01b-collection-impact")`
- `Assets/Scripts/ChainRealtimeQA.cs:56` · `ChainRealtimeQA.Start` → `Capture("02-moving-brick-collision")`
- `Assets/Scripts/ChainRealtimeQA.cs:59` · `ChainRealtimeQA.Start` → `Capture("03-released-protected-contents")`
- `Assets/Scripts/ChainRealtimeQA.cs:62` · `ChainRealtimeQA.Start` → `Capture("04-after-continuous-fire")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var cam=Camera.main;var target=new RenderTexture(540,height,24);target.Create();var old=cam.targetTexture;cam.targetTexture=target;cam.rect=new Rect(0,0,1,1);cam.aspect=540f/height;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;Canvas.ForceUpdateCanvases();var design=(RectTransform)typeof(AppUI).GetField("designRoot",Hidden).GetValue(app);var safe=(RectTransform)design.parent;design.localScale=Vector3.one*Mathf.Min(safe.rect.width/1080f,safe.rect.height/2340f);Canvas.ForceUpdateCanvases();cam.Render();var previous=RenderTexture.active;RenderTexture.active=target;var image=new Texture2D(540,height,TextureFormat.RGB24,false);image.ReadPixels(new Rect(0,0,540,height),0,0);image.Apply();File.WriteAllBytes(Path.Combine(dir,name+".png"),image.EncodeToPNG());RenderTexture.active=previous;cam.targetTexture=old;Destroy(image);target.Release();Destroy(target);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
const BindingFlags Hidden=BindingFlags.NonPublic|BindingFlags.Instance;
```
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
int checks,failures,errors,height=1170;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
