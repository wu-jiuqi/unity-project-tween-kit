# FlightV22QA · FlightV22QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/FlightV22QA.cs`
文件 SHA-256：`dfee06a09b10f28dd868777655c530fca195a5a77c846aa70e51eca7bda63ac0`
[原始源码快照](../source/Assets/Scripts/FlightV22QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L13

方法 SHA-256：`431f2a79fffa3e38754b1995303bdc318d2e2a63a91c7fa8471d76d86ce91387`
时间来源：Time.fixedDeltaTime, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FlightV22QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"V22 real player physics and recall regression\n");Application.logMessageReceived+=Log;
  yield return new WaitForSecondsRealtime(1);var app=FindFirstObjectByType<AppUI>();Check(RuntimeQA.IsIsolated&&app!=null,"isolated application");
  app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.8f);
  var game=app.Session;var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  int ten=Mathf.RoundToInt(FlightGeometry.RecallDelay/Time.fixedDeltaTime);
  for(int level=0;level<6;level++){
   game.StartLevel(level);yield return new WaitForSecondsRealtime(.8f);
   var bricks=FindObjectsByType<BrickBody>(FindObjectsSortMode.None);
   bool aligned=true;foreach(var brick in bricks){var box=brick.GetComponent<BoxCollider2D>();if(!brick.Definition.shutter)aligned&=Vector2.Distance(box.size,new Vector2(game.Board.CellWidth,game.Board.CellHeight))<.0001f;}
   Check(aligned,"all visible brick footprints match colliders L"+level);
   Check(!game.RecallAvailable,"empty board hides recall L"+level);
   Check(game.TryShoot(Vector2.up),"shot accepted L"+level);game.QAAdvanceOneStep();
   var ball=FindFirstObjectByType<BallBody>();
   Check(Mathf.Abs(ball.GetComponent<CircleCollider2D>().radius-FlightGeometry.Radius)<.0001f,"ball collider matches visible radius L"+level);
   Check(Mathf.Abs(ball.GetComponentInChildren<SpriteRenderer>().sprite.bounds.size.x*.875f-FlightGeometry.Radius*2)<.0001f,"visible yarn size retained L"+level);
   // A grazing shot through the old visual-only margin must now hit the real brick.
   // Pick a central target so the test launch cannot start behind a side wall.
   BrickBody target=bricks[0];foreach(var brick in bricks)if(Mathf.Abs(brick.transform.position.x)<Mathf.Abs(target.transform.position.x))target=brick;
   foreach(var brick in bricks)brick.GetComponent<Collider2D>().enabled=brick==target;
   var bounds=target.GetComponent<Collider2D>().bounds;
   float y=bounds.max.y-.015f;ball.Initialize(ball.Id,game.Generation,new Vector2(bounds.min.x-.7f,y),Vector2.right,0);
   Physics2D.SyncTransforms();int hits=game.HitCount;
   for(int step=0;step<8&&game.HitCount==hits;step++)game.QAAdvanceOneStep();
   Check(game.HitCount>hits,"grazing visible brick edge damages target L"+level);
   Check(Mathf.Abs(ball.Body.linearVelocity.magnitude-game.CurrentLevel.ballSpeed)<.002f,"edge contact retains fixed speed L"+level);
   // Remove targets from this fixture, then keep the volley bouncing horizontally.
   foreach(var brick in bricks)if(brick!=null)brick.GetComponent<Collider2D>().enabled=false;
   for(int step=0;step<40;step++){
    foreach(var b in FindObjectsByType<BallBody>(FindObjectsSortMode.None))if(b.Active)b.Initialize(b.Id,game.Generation,new Vector2((b.Id%5-2)*.65f,-4),Vector2.right,0);
    game.QAAdvanceOneStep();
   }
   var active=FindObjectsByType<BallBody>(FindObjectsSortMode.None);ball=active[0];
   // Opposing equal-speed balls exercise energy transfer, including head-on stops.
   if(active.Length>1){active[0].Initialize(active[0].Id,game.Generation,new Vector2(-.5f,-4),Vector2.right,0);active[1].Initialize(active[1].Id,game.Generation,new Vector2(.5f,-4),Vector2.left,0);}
   float maxError=0;for(int step=0;step<80;step++){game.QAAdvanceOneStep();foreach(var b in active)if(b.Active)maxError=Mathf.Max(maxError,Mathf.Abs(b.Body.linearVelocity.magnitude-game.CurrentLevel.ballSpeed));}
   Check(maxError<.002f,"wall and ball collisions keep constant speed L"+level+" error="+maxError);
   ball.Body.linearVelocity=Vector2.zero;ball.QATick();Check(Mathf.Abs(ball.Body.linearVelocity.magnitude-game.CurrentLevel.ballSpeed)<.002f,"zero solver velocity recovers L"+level);
   ball.Body.linearVelocity*=3;ball.QATick();Check(Mathf.Abs(ball.Body.linearVelocity.magnitude-game.CurrentLevel.ballSpeed)<.002f,"excess energy normalizes L"+level);
   // A successful damage operation starts a fresh global ten-second window.
   BrickBody durable=null;foreach(var brick in bricks)if(brick!=null&&brick.Alive){durable=brick;break;}
   hits=game.HitCount;game.HitBrick(durable,ball,durable.transform.position);Check(game.HitCount==hits+1&&!game.RecallAvailable,"actual damage resets recall L"+level);
   for(int step=0;step<ten-1;step++)game.QAAdvanceOneStep();Check(!game.RecallAvailable,"9.98 seconds hides recall L"+level);
   game.SetPaused(true);for(int step=0;step<50;step++)game.QAAdvanceOneStep();Check(!game.RecallAvailable,"pause excludes time from recall L"+level);game.SetPaused(false);
   game.QAAdvanceOneStep();Check(game.RecallAvailable,"exactly ten seconds offers recall while moving L"+level);
   var oldCount=game.ActiveBalls;game.SetPaused(true);game.RecallBalls();Check(game.ActiveBalls==oldCount,"paused recall cannot remove balls L"+level);game.SetPaused(false);
   foreach(var brick in bricks)if(brick!=null&&brick.Alive){durable=brick;break;}
   hits=game.HitCount;game.HitBrick(durable,ball,durable.transform.position);Check(game.HitCount==hits+1&&!game.RecallAvailable,"damage hides already visible recall L"+level);
   // Wait another full window after the second damage event.
   for(int step=0;step<ten;step++)game.QAAdvanceOneStep();Check(game.RecallAvailable,"new full window offers recall again L"+level);
   int lost=game.LostCount;game.RecallBalls();Check(game.ActiveBalls==0&&game.LostCount==lost+oldCount&&!game.RecallAvailable,"recall returns every active ball and hides button L"+level);
   Check(game.TryShoot(Vector2.up),"new volley accepted L"+level);game.QAAdvanceOneStep();Check(!game.RecallAvailable,"new volley starts fresh timer L"+level);
   game.StopLevel();Check(!game.RecallAvailable,"level exit clears recall L"+level);yield return null;
  }
  Physics2D.simulationMode=mode;
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
