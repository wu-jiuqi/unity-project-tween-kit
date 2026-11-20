# FlightQA · FlightQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/FlightQA.cs`
文件 SHA-256：`86f328326a77621129d3928ab6b518fcdedec416e8f4e4cf132f08f862733646`
[原始源码快照](../source/Assets/Scripts/FlightQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L12

方法 SHA-256：`252a647b1497f92acdc5330ee8ff5dfb45b1e3b9d6fea6080f683a6b0ac65700`
时间来源：Time.fixedDeltaTime, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FlightQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"Flight v15: automated Windows player physics regression; memory-only profile.\n");Application.logMessageReceived+=Log;
  yield return new WaitForSecondsRealtime(.5f);Check(RuntimeQA.IsIsolated,"isolated profile");var app=FindFirstObjectByType<AppUI>();
  app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(1);
  var game=app.Session;var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  for(int level=0;level<6;level++){
   game.StartLevel(level);yield return new WaitForSecondsRealtime(.8f);
   foreach(var brick in FindObjectsByType<BrickBody>(FindObjectsSortMode.None))brick.GetComponent<Collider2D>().enabled=false;
   Physics2D.SyncTransforms();int shots=game.Shots;
   Check(!game.TryShoot(Vector2.down)&&game.Shots==shots,"invalid shot leaves ammo unchanged L"+level);
   Check(game.TryShoot(Vector2.up),"valid shot queued L"+level);Check(game.Shots==shots,"queued shot costs nothing until spawn L"+level);
   game.QAAdvanceOneStep();Check(game.Shots==shots-1&&game.SpawnCount==1,"successful first spawn charges one volley L"+level);
   var balls=FindObjectsByType<BallBody>(FindObjectsSortMode.None);var b=balls[0];
   Check(Vector2.Distance(b.Body.position,game.SpawnPosition(Vector2.up)+Vector2.up*game.CurrentLevel.ballSpeed*Time.fixedDeltaTime)<.02f,"muzzle equals physical spawn L"+level);
   for(int i=0;i<40;i++)game.QAAdvanceOneStep();Check(game.SpawnCount==game.CurrentLevel.volleyCount,"continuous burst spawns all balls L"+level);
   balls=FindObjectsByType<BallBody>(FindObjectsSortMode.None);foreach(var ball in balls)game.LoseBall(ball,ball.Body.position);
   Check(game.TryShoot(Vector2.up),"second volley accepted L"+level);game.QAAdvanceOneStep();b=FindFirstObjectByType<BallBody>();
   b.Body.gravityScale=4;b.Body.linearDamping=3;game.SetPaused(true);var pos=b.Body.position;var vel=b.Body.linearVelocity;
   for(int i=0;i<12;i++)game.QAAdvanceOneStep();Check(b.Body.position==pos&&!game.TryShoot(Vector2.up),"pause freezes physical ball and rejects shooting L"+level);
   game.SetPaused(false);Check(b.Body.gravityScale==0&&b.Body.linearDamping==0&&b.Body.linearVelocity==vel,"resume repairs gravity and preserves velocity L"+level);
   foreach(var ball in FindObjectsByType<BallBody>(FindObjectsSortMode.None))if(ball!=b)game.LoseBall(ball,ball.Body.position);
   // Finish pending burst before the free flight fixture.
   for(int i=0;i<40;i++)game.QAAdvanceOneStep();foreach(var ball in FindObjectsByType<BallBody>(FindObjectsSortMode.None))if(ball!=b)game.LoseBall(ball,ball.Body.position);
   b.Initialize(b.Id,game.Generation,new Vector2(0,-4),new Vector2(game.CurrentLevel.ballSpeed,0),999);
   for(int i=0;i<8;i++){b.QATick();Physics2D.Simulate(Time.fixedDeltaTime);}
   Check(Mathf.Abs(b.Body.position.y+4)<.001f&&Mathf.Abs(b.Body.position.x-game.CurrentLevel.ballSpeed*8*Time.fixedDeltaTime)<.015f,"zero gravity straight flight on reused body L"+level);
   for(int i=0;i<200;i++){b.QATick();Physics2D.Simulate(Time.fixedDeltaTime);}
   Check(Mathf.Abs(b.Body.linearVelocity.magnitude-game.CurrentLevel.ballSpeed)<.02f&&Mathf.Abs(b.Body.position.y+4)<.001f,"horizontal reflections retain speed and direction axis L"+level);
   b.Body.linearVelocity=Vector2.zero;b.QATick();Check(b.Active&&Mathf.Abs(b.Body.linearVelocity.magnitude-game.CurrentLevel.ballSpeed)<.001f,"zero speed recovers without recall L"+level);
   for(int i=0;i<Mathf.CeilToInt(FlightGeometry.RecallDelay/Time.fixedDeltaTime)+2;i++)game.QAAdvanceOneStep();
   Check(game.RecallAvailable,"ten seconds without damage offers recall L"+level);
   int lost=game.LostCount;int count=game.ActiveBalls;game.RecallBalls();Check(game.ActiveBalls==0&&game.LostCount==lost+count&&!game.RecallAvailable,"explicit recall returns active balls L"+level);
   // Analytic two-wall forecast: starts at the exact muzzle, no ball layer participates.
   Vector2 d=GameSession.NormalizeAimDirection(new Vector2(1,.22f));Vector2 p=game.SpawnPosition(d);int reflections=0;
   for(int k=0;k<3;k++){float tx=d.x>0?(FlightGeometry.Right-FlightGeometry.Radius-p.x)/d.x:(FlightGeometry.Left+FlightGeometry.Radius-p.x)/d.x;float ty=(FlightGeometry.Top-FlightGeometry.Radius-p.y)/d.y;float t=Mathf.Min(tx,ty);p+=d*t;if(k==2)break;if(tx<ty){d.x=-d.x;p.x+=Mathf.Sign(d.x)*.001f;}else{d.y=-d.y;p.y-=.001f;}reflections++;}
   game.PredictAim(new Vector2(1,.22f));Check(game.AimReflections==2&&Vector2.Distance(game.AimEndpoint,p)<.025f,"two reflections match expanded-wall analytic solution L"+level+" actual="+game.AimEndpoint+" expected="+p+" reflections="+game.AimReflections);
   var fixture=new GameObject("QA static target");fixture.layer=8;fixture.transform.position=game.SpawnPosition(Vector2.up)+Vector2.up*2;var box=fixture.AddComponent<BoxCollider2D>();box.size=Vector2.one;
   Physics2D.SyncTransforms();game.PredictAim(Vector2.up);Check(game.AimReflections==0&&Mathf.Abs(game.AimEndpoint.y-(fixture.transform.position.y-.5f-FlightGeometry.Radius))<.015f,"forecast stops radius away from target L"+level);DestroyImmediate(fixture);
   game.PredictAim(new Vector2(FlightGeometry.Right-FlightGeometry.Radius,FlightGeometry.Top-FlightGeometry.Radius)-game.LauncherPosition);Check(game.AimReflections==2,"corner forecast resolves both wall contacts L"+level);
   var gapLeft=new GameObject("QA gap left");var gapRight=new GameObject("QA gap right");gapLeft.layer=gapRight.layer=8;
   var gl=gapLeft.AddComponent<BoxCollider2D>();var gr=gapRight.AddComponent<BoxCollider2D>();gl.size=gr.size=new Vector2(.5f,1);
   float gapY=game.SpawnPosition(Vector2.up).y+2;gapLeft.transform.position=new Vector2(-.35f,gapY);gapRight.transform.position=new Vector2(.35f,gapY);Physics2D.SyncTransforms();game.PredictAim(Vector2.up);Check(game.AimEndpoint.y<gapY,"gap narrower than diameter stops forecast L"+level);
   gapLeft.transform.position=new Vector2(-.44f,gapY);gapRight.transform.position=new Vector2(.44f,gapY);Physics2D.SyncTransforms();game.PredictAim(Vector2.up);Check(game.AimReflections>0,"gap wider than diameter lets forecast pass L"+level);DestroyImmediate(gapLeft);DestroyImmediate(gapRight);
   game.PredictAim(Vector2.up);Vector2 forecast=game.AimEndpoint;var otherBall=new GameObject("QA future moving ball");otherBall.layer=9;otherBall.transform.position=game.SpawnPosition(Vector2.up)+Vector2.up*2;otherBall.AddComponent<Rigidbody2D>().gravityScale=0;otherBall.AddComponent<CircleCollider2D>().radius=.5f;Physics2D.SyncTransforms();game.PredictAim(Vector2.up);Check(Vector2.Distance(forecast,game.AimEndpoint)<.001f,"moving ball excluded from forecast L"+level);DestroyImmediate(otherBall);
   var brickState=FindFirstObjectByType<BrickBody>();int hp=brickState.Definition.hp;brickState.Damage(Mathf.Max(1,hp/3));int stage=brickState.DamageStage;brickState.Damage(hp);Check(brickState.DamageStage==stage&&brickState.CurrentHealth<=Mathf.Max(1,hp/3),"damage state cannot flash back under stale damage update L"+level);
   game.StopLevel();yield return null;
  }
  Physics2D.simulationMode=mode;
  app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(1);game.SetAim(new Vector2(1,.3f));
  foreach(var viewport in new[]{new Vector2Int(1080,1920),new Vector2Int(1080,2340),new Vector2Int(1600,1200)}){app.QASetViewport(viewport);var rect=Camera.main.rect;Check(rect.xMin>=0&&rect.yMin>=0&&rect.xMax<=1.0001f&&rect.yMax<=1.0001f,"board camera fits viewport "+viewport);}
  app.QASetViewport(null);yield return new WaitForSecondsRealtime(.3f);
  game.TryShoot(Vector2.up);yield return new WaitForSecondsRealtime(.3f);game.SetPaused(true);var tails=FindObjectsByType<FlightTrail>(FindObjectsSortMode.None);int before=0;foreach(var t in tails)before+=t.Count;
  yield return new WaitForSecondsRealtime(.4f);int after=0;foreach(var t in tails)after+=t.Count;Check(before>0&&before==after,"pause retains trail samples without aging");game.SetPaused(false);
  yield return new WaitForSecondsRealtime(.4f);foreach(var ball in FindObjectsByType<BallBody>(FindObjectsSortMode.None))game.LoseBall(ball,ball.Body.position);
  yield return new WaitForSecondsRealtime(.4f);after=0;foreach(var t in tails)after+=t.Count;Check(after==0,"returned ball trails expire");
  File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failed+" errors="+errors+"\n");Application.logMessageReceived-=Log;Application.Quit(failed==0&&errors==0?0:4);
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
int checks,failed,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
