# CombatFeedback79QA · CombatFeedback79QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/CombatFeedback79QA.cs`
文件 SHA-256：`24c81bebb03a3a119301f3c3ca5e8e0889bdb40657f0b94a7a322fc1b6fdecf9`
[原始源码快照](../source/Assets/Scripts/CombatFeedback79QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L12

方法 SHA-256：`8984e384f2a90bf2f7fc197baff6ccf05e897434f6ec8585fd71dcd6bff5e0fe`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/CombatFeedback79QA.cs:22` · `CombatFeedback79QA.Start` → `Capture("01-real-volley-trails")`
- `Assets/Scripts/CombatFeedback79QA.cs:32` · `CombatFeedback79QA.Start` → `Capture("02-jelly-break")`
- `Assets/Scripts/CombatFeedback79QA.cs:37` · `CombatFeedback79QA.Start` → `Capture("03-dense-burst")`
- `Assets/Scripts/CombatFeedback79QA.cs:40` · `CombatFeedback79QA.Start` → `Capture("chapter-"+(level+1))`
- `Assets/Scripts/CombatFeedback79QA.cs:50` · `CombatFeedback79QA.Start` → `Capture("04-paint-spray")`
- `Assets/Scripts/CombatFeedback79QA.cs:52` · `CombatFeedback79QA.Start` → `Capture("05-paint-marks")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### Open · L13

方法 SHA-256：`7e1ca5b910e34b4eebeefe099c5a05d020a70e582a58845b7cfdecdd4f7ea72d`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79QA.Open(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/CombatFeedback79QA.cs:18` · `CombatFeedback79QA.Start` → `Open(0)`
- `Assets/Scripts/CombatFeedback79QA.cs:40` · `CombatFeedback79QA.Start` → `Open(level)`

```csharp
IEnumerator Open(int level){app.QANavigate("home");app.QAStartLevel(level);yield return app.QAWaitForLevel();for(int i=0;i<100&&app.AlphabetIntroActive;i++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.02f);}app.QAAdvanceBoosterGuide();g=app.Session;g.QAIgnoreFocusLoss=true;g.SetPaused(false);if(g.SelectedStreakBooster!=StreakBooster.None)g.SelectStreakBooster(g.SelectedStreakBooster);yield return new WaitForSecondsRealtime(.25f);}
```

### Start · L14

方法 SHA-256：`ecbe4c485bdf3196ddf889d8e97dc2ed1aa988de33de2e478f664bca73def76b`
时间来源：Time.fixedDeltaTime, Time.timeScale, Time.unscaledTime, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CombatFeedback79QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];int ix=Array.IndexOf(args,"--qa-fps");if(ix>=0)fps=int.Parse(args[ix+1]);Application.targetFrameRate=fps;QualitySettings.vSyncCount=0;
  Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(4);app=FindFirstObjectByType<AppUI>();Check(Application.version==GameVersion.Name&&int.Parse(GameVersion.Name.Split('.').Last())==GameVersion.Code,"built player product version matches authoritative source and version code");
  yield return Open(0);Check(Physics2D.GetIgnoreLayerCollision(9,9),"all balls ignore ball layer while retaining board contacts");Check(g.CombatFeel!=null,"production battle initializes generated particle pool");Check(FeedbackProfile.Active.ordinaryHitStop==0&&FeedbackProfile.Active.ordinaryBreakStop==0&&Mathf.Abs(FeedbackProfile.Active.hitStopBudget-.12f)<.0001f,"ordinary hit and break hitstop disabled; special budget retained");var blue=g.CollectionBricks.First(b=>b.Alive&&b.Definition.colorIndex==1&&b.Definition.juiceGroup==0);var cream=g.CollectionBricks.First(b=>b.Alive&&b.Definition.colorIndex==0&&b.Definition.juiceGroup==0);var blueTint=g.CombatParticleTint(blue);var creamTint=g.CombatParticleTint(cream);Check(blueTint.b>blueTint.r&&blueTint.g>blueTint.r&&creamTint.r>.97f&&creamTint.g>.92f,"blue and cream jelly particles follow the visible body palette");
  Check(g.TryShoot(new Vector2(.18f,1)),"production volley accepted");yield return new WaitForSecondsRealtime(.30f);
  Check(g.HitCount>0&&g.CombatFeel.Hits+g.CombatFeel.Breaks>0,"real physics collisions trigger combat feedback");Check(g.CombatFeel.HitStops==0&&Time.timeScale==1,"normal collisions keep simulation running without hitstop");
  var balls=g.GetComponentsInChildren<BallBody>().Where(b=>b.Active).ToArray();Check(balls.Length>0,"real volley retains active balls");Check(balls.All(b=>b.Body.gravityScale==0&&b.Body.linearDamping==0&&b.Body.angularDamping==0&&b.Body.sharedMaterial.friction==0),"active balls have zero gravity, damping and friction");
  var trails=g.GetComponentsInChildren<FlightTrail>().Where(t=>t.Count>1).ToArray();Check(trails.Length>0&&trails.All(t=>t.GetComponentsInChildren<LineRenderer>().Length==2),"active balls render dual-layer yarn strands");yield return Capture("01-real-volley-trails");
  // Safe empty space: verify straight, constant-speed travel and two crossing balls.
  var previous=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;g.CombatFeel.CancelStop();g.enabled=false;
  if(balls.Length>=2){var a=balls[0];var b=balls[1];float speed=g.CurrentLevel.ballSpeed;a.ResumeChainFlight(new Vector2(-.45f,g.ReturnLineY+1),Vector2.right*speed);b.ResumeChainFlight(new Vector2(.45f,g.ReturnLineY+1),Vector2.left*speed);
   for(int n=0;n<8;n++){a.QATick();b.QATick();Physics2D.Simulate(Time.fixedDeltaTime);}Check(a.Body.position.x>b.Body.position.x,"opposed balls cross without rebounding");Check(Mathf.Abs(a.Body.position.y-(g.ReturnLineY+1))<.001f&&Mathf.Abs(a.Body.linearVelocity.magnitude-speed)<.001f&&a.Body.linearVelocity.x>0,"unobstructed flight stays straight at constant speed");}
  else Check(false,"two real balls available for crossing test");
  g.enabled=true;Physics2D.simulationMode=previous;g.SetPaused(true);yield return new WaitForSecondsRealtime(.15f);g.SetPaused(false);
  // Use the actual brick damage route for hit and break, then inspect duration merge.
  yield return new WaitForSecondsRealtime(1.1f);var brick=g.CollectionBricks.First(b=>b.Alive&&b.Definition.kind==BrickKind.Normal&&b.Definition.juiceGroup==0);int before=g.CombatFeel.HitStops;
  g.ApplyChainImpact(brick,int.MaxValue,Vector2.up,brick.transform.position,false);yield return new WaitForEndOfFrame();
  Check(g.CombatFeel.Breaks>0&&g.CombatFeel.ActiveParticles>0,"actual brick destruction emits generated colored jelly fragments");yield return new WaitForSecondsRealtime(.1f);yield return Capture("02-jelly-break");
  yield return new WaitForSecondsRealtime(.25f);g.CombatFeel.CancelStop();g.SetPaused(true);var pose=g.GetComponentsInChildren<BallBody>().Where(b=>b.Active).ToDictionary(b=>b,b=>b.Body.position);int count=g.CombatFeel.ActiveParticles;yield return new WaitForSecondsRealtime(.2f);
  Check(Time.timeScale==1&&pose.All(p=>p.Key.Body.position==p.Value)&&g.CombatFeel.ActiveParticles==count,"pause restores clock and freezes ball and particle poses");g.SetPaused(false);yield return new WaitForSecondsRealtime(1.1f);
  // Many simultaneous events coalesce; resources remain bounded.
  before=g.CombatFeel.HitStops;for(int n=0;n<40;n++)g.EmitFeedback(new Vector2((n%5-2)*.35f,0),"break",new Color(.3f,.85f,1));yield return new WaitForEndOfFrame();
  Check(g.CombatFeel.HitStops==before&&Time.timeScale==1,"forty simultaneous ordinary breaks cannot promote themselves to hitstop");Check(g.CombatFeel.ActiveParticles<=128&&g.CombatFeel.Peak<=128,"dense bursts respect the prewarmed 128-particle cap");yield return Capture("03-dense-burst");
  g.SetPaused(true);yield return new WaitForSecondsRealtime(.1f);Check(Time.timeScale==1&&!g.CombatFeel.Frozen,"pause during hitstop never leaves global time frozen");g.SetPaused(false);yield return new WaitForSecondsRealtime(1);
  g.CombatFeel.Power(Vector2.zero);yield return new WaitForEndOfFrame();app.QANavigate("home");yield return null;Check(Time.timeScale==1,"navigation clears hitstop clock ownership");
  foreach(int level in new[]{26,44}){yield return Open(level);Check(g.CombatFeel!=null&&Physics2D.GetIgnoreLayerCollision(9,9),"chapter "+(level+1)+" retains no-ball-collision feedback");g.TryShoot(new Vector2(.2f,1));yield return new WaitForSecondsRealtime(2);Check(g.HitCount>0,"chapter "+(level+1)+" actual volley hits bricks");yield return Capture("chapter-"+(level+1));}
  // Paint flies to bounded random neighbors, settles briefly, then fades.
  g.CombatFeel.CancelStop();for(int n=0;n<150;n++)g.CombatFeel.AdvanceParticles(.02f);
  Vector2 paintOrigin=g.Board.CellToLocal(4,4);Color paintColor=new Color(.3f,.8f,1);g.EmitFeedback(paintOrigin,"break",paintColor);g.CombatFeel.CancelStop();
  g.CombatFeel.AdvanceParticles(.04f);
  Check(g.CombatFeel.ActivePaintMarks==0&&g.CombatFeel.ActiveParticles>=12,"paint begins as twelve outward-moving liquid streaks");
  for(int n=0;n<9;n++)g.CombatFeel.AdvanceParticles(.02f);
  var marks=g.CombatFeel.GetComponentsInChildren<SpriteRenderer>().Where(v=>v.gameObject.name=="Pooled paint spray"&&v.sprite.name.StartsWith("paint-splat-")).ToArray();
  Check(marks.Length==12&&g.CombatFeel.ActivePaintMarks==12,"liquid turns into irregular deposited paint rather than falling to the bottom");
  Check(marks.All(v=>{float radius=Vector2.Distance(v.transform.position,paintOrigin)/g.Board.CellWidth;return radius>=FeedbackProfile.Active.paintMinRadius-.001f&&radius<=FeedbackProfile.Active.paintMaxRadius+.001f;}),"every paint landing center lies within the configured n-m radius");
  Check(marks.Select(v=>v.sprite.name).Distinct().Count()>=2&&marks.All(v=>Mathf.Abs(v.color.r-paintColor.r)<.001f&&Mathf.Abs(v.color.g-paintColor.g)<.001f&&Mathf.Abs(v.color.b-paintColor.b)<.001f),"random splash shapes retain the source brick pigment color");yield return Capture("04-paint-spray");
  var paintPoses=marks.ToDictionary(v=>v,v=>v.transform.position);for(int n=0;n<12;n++)g.CombatFeel.AdvanceParticles(.02f);
  Check(paintPoses.All(pair=>pair.Key.transform.position==pair.Value),"deposited paint stays around the broken brick without gravity drift");yield return Capture("05-paint-marks");
  for(int n=0;n<80;n++)g.CombatFeel.AdvanceParticles(.02f);Check(g.CombatFeel.ActiveParticles==0,"temporary paint fades and recycles without permanent board clutter");
  float savedMin=FeedbackProfile.Active.paintMinRadius,savedMax=FeedbackProfile.Active.paintMaxRadius;FeedbackProfile.Active.paintMinRadius=.8f;FeedbackProfile.Active.paintMaxRadius=1;
  g.EmitFeedback(paintOrigin,"break",paintColor);g.CombatFeel.CancelStop();g.CombatFeel.AdvanceParticles(.2f);
  Check(g.CombatFeel.LastMinRadiusCells>=.8f&&g.CombatFeel.LastMaxRadiusCells<=1.001f,"changing n-m configuration changes the actual spray radius");
  FeedbackProfile.Active.paintMinRadius=savedMin;FeedbackProfile.Active.paintMaxRadius=savedMax;
  // Verify actual solver contacts from front, side and back, with one target.
  app.QANavigate("home");yield return null;var directional=new GameObject("Directional hit fixture").AddComponent<GameSession>();directional.QAIgnoreFocusLoss=true;directional.StartLevel(0);
  var target=directional.CollectionBricks.First(b=>b.Definition.hp>=4&&b.Definition.kind==BrickKind.Normal&&b.Definition.juiceGroup==0);
  foreach(var b in directional.CollectionBricks)if(b!=target)b.Remove();target.gameObject.SetActive(false);
  directional.TryShoot(Vector2.up);yield return new WaitForSecondsRealtime(.65f);
  var testBall=directional.GetComponentsInChildren<BallBody>().First(b=>b.Active);
  foreach(var b in directional.GetComponentsInChildren<BallBody>().Where(b=>b.Active&&b!=testBall).ToArray())directional.LoseBall(b,b.Body.position);
  target.gameObject.SetActive(true);
  foreach(var direction in new[]{Vector2.up,Vector2.right,Vector2.down}){
   directional.SetPaused(true);yield return new WaitForSecondsRealtime(1.1f);directional.SetPaused(false);
   int hits=directional.HitCount,stops=directional.CombatFeel.HitStops;testBall.ResumeChainFlight((Vector2)target.transform.position-direction*1.1f,direction*directional.CurrentLevel.ballSpeed);
   float until=Time.unscaledTime+.5f;while(directional.HitCount==hits&&Time.unscaledTime<until)yield return null;yield return new WaitForEndOfFrame();
   Check(directional.HitCount>hits&&directional.CombatFeel.HitStops==stops&&Time.timeScale==1,"actual "+direction+" ordinary brick contact does not stop time");
  }
  directional.SetPaused(true);yield return new WaitForSecondsRealtime(1.1f);directional.SetPaused(false);testBall.Body.simulated=false;int actualStops=directional.CombatFeel.HitStops;var special=directional.SpawnChainTool(2,new Vector2(2,0));special.ReadyAt=0;directional.ActivateChainTool(special,Vector2.up);yield return new WaitForEndOfFrame();Check(directional.CombatFeel.HitStops>actualStops&&directional.CombatFeel.Frozen&&directional.CombatFeel.LastStop>=.044f&&directional.CombatFeel.LastStop<=.06f,"actual special bomb brick activation retains bounded hitstop");
  var shake=Camera.main.GetComponent<CameraJuice>();float pixelScale=Camera.main.pixelHeight/(Camera.main.orthographicSize*2),kickPixels=shake.WorldOffset.magnitude*pixelScale;
  Check(kickPixels<=Camera.main.pixelHeight*.0042f,"special brick jelly recoil starts within reduced viewport cap");
  directional.CombatFeel.CancelStop();Time.timeScale=0;var frozenKick=shake.WorldOffset;var pointer=new Vector3(Screen.width*.6f,Screen.height*.65f,0);var stablePointer=CameraJuice.StableScreenToWorld(Camera.main,pointer);int frozenStep=directional.PhysicsStep;
  yield return null;yield return new WaitForEndOfFrame();
  Check(Time.timeScale==0&&(shake.WorldOffset-frozenKick).magnitude>.001f,"camera shake continues on real time while gameplay is frozen");
  Check(directional.PhysicsStep==frozenStep,"real-time camera shake does not advance frozen physics");
  Check((CameraJuice.StableScreenToWorld(Camera.main,pointer)-stablePointer).magnitude<.0001f,"stationary aim input stays stable while the camera moves");
  Time.timeScale=1;directional.SetPaused(true);yield return new WaitForEndOfFrame();Check(shake.WorldOffset.magnitude<.0001f&&!shake.Active,"pause restores the camera exactly to its rest position");
  directional.SetPaused(false);testBall.Body.simulated=false;shake.ResetImpact();for(int i=0;i<100;i++)shake.ImpactScreen(1,1);yield return new WaitForEndOfFrame();float cappedPixels=shake.WorldOffset.magnitude*pixelScale;
  Check(cappedPixels<=Camera.main.pixelHeight*.0042f,"simultaneous extreme shake requests respect the amplitude cap");yield return new WaitForSecondsRealtime(.35f);
  Check(!shake.Active&&shake.WorldOffset.magnitude<.0001f,"coalesced shake ends promptly with no residual camera offset");File.WriteAllText(dir+"/shake-stats.txt","kickPixels="+kickPixels+" cappedPixels="+cappedPixels+" screen="+Screen.width+"x"+Screen.height);
  directional.SetPaused(true);yield return new WaitForSecondsRealtime(1.1f);directional.SetPaused(false);testBall.Body.simulated=false;int priorStops=directional.CombatFeel.HitStops;
  directional.CombatFeel.Power(Vector2.zero);yield return new WaitForEndOfFrame();
  while(directional.CombatFeel.Frozen)yield return null;
  directional.CombatFeel.Power(Vector2.zero);yield return new WaitForSecondsRealtime(.25f);
  Check(directional.CombatFeel.HitStops>=priorStops+2,"a special activation during recovery is retained and played");directional.StopLevel();Destroy(directional.gameObject);
  // Dedicated isolated fixture verifies existing 12s lifetime under ideal flight.
  app.QANavigate("home");yield return null;var fixture=new GameObject("Lifetime fixture").AddComponent<GameSession>();fixture.enabled=false;fixture.QAIgnoreFocusLoss=true;fixture.StartLevel(0);var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;fixture.TryShoot(Vector2.up);
  for(int n=0;n<750&&fixture.IsPlaying;n++){if(n<40)foreach(var ball in fixture.GetComponentsInChildren<BallBody>().Where(b=>b.Active))ball.ResumeChainFlight(new Vector2(0,fixture.ReturnLineY+.6f),Vector2.right*fixture.CurrentLevel.ballSpeed);fixture.QAAdvanceOneStep();}
  Check(fixture.ActiveBalls==0&&fixture.PendingBurstBalls==0&&fixture.ChainAutoReturns>0,"existing flight limit returns horizontally trapped balls without gravity or ball collisions");fixture.StopLevel();Destroy(fixture.gameObject);Physics2D.simulationMode=mode;
  Check(Time.timeScale==1,"final clock restored");Check(errors==0,"no runtime errors");File.WriteAllText(dir+"/result.json",JsonUtility.ToJson(new Report{checks=checks,failures=failures,errors=errors,fps=fps},true));Application.Quit(failures+errors==0?0:3);
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
AppUI app;
```
```csharp
GameSession g;
```
```csharp
int fps=60;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
