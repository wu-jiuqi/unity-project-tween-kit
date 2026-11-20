# OnboardingQA · OnboardingQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/OnboardingQA.cs`
文件 SHA-256：`f55d6b7b4ae0e463b7bd4979f55da46aa6d73e77b4ae9f2014d945a81527b4f8`
[原始源码快照](../source/Assets/Scripts/OnboardingQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L21

方法 SHA-256：`8984e384f2a90bf2f7fc197baff6ccf05e897434f6ec8585fd71dcd6bff5e0fe`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OnboardingQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OnboardingQA.cs:34` · `OnboardingQA.Start` → `Capture("tour-"+i)`
- `Assets/Scripts/OnboardingQA.cs:37` · `OnboardingQA.Start` → `Capture("lesson-"+(index+1))`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### Dismiss · L22

方法 SHA-256：`433f6449f7b04852f9b31e5f78296d8b9dcd53c81a63c14d8e7f06e658abcc1f`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OnboardingQA.Dismiss()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OnboardingQA.cs:35` · `OnboardingQA.Start` → `Dismiss()`
- `Assets/Scripts/OnboardingQA.cs:36` · `OnboardingQA.Start` → `Dismiss()`

```csharp
IEnumerator Dismiss(){for(int n=0;n<90;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.025f);if(n>8&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}}
```

### Start · L24

方法 SHA-256：`85a19b5ae1173396cf10cf43a03ef4b663e8fe952201868c1993dc63cd716601`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OnboardingQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  dir=Environment.GetCommandLineArgs()[Array.IndexOf(Environment.GetCommandLineArgs(),"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  var args=Environment.GetCommandLineArgs();int fps=Array.IndexOf(args,"--qa-fps");Application.targetFrameRate=fps>=0?int.Parse(args[fps+1]):60;QualitySettings.vSyncCount=0;
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();g=app.Session;
  var store=new Store();app.QAUseIntroProfile(LocalProfile.Load(store));
  for(int a=1;a<=7;a++)for(int c=0;c<6;c++){var sprite=OnboardingArt.Get(a,c);Check(sprite!=null&&sprite.rect.width>0&&sprite.rect.height>0,"authored atlas region "+a+"/"+c);}
  app.QAStartFirstBounce();yield return app.QAWaitForLevel();g.QAIgnoreFocusLoss=true;
  for(int n=0;n<90&&app.QABattleTour==null;n++){if(app.QAAlphabetIntro!=null)app.QAAlphabetIntro.AdvanceForQA();yield return new WaitForSecondsRealtime(.025f);}
  Check(app.QABattleTour!=null,"first tutorial enters real HUD tour");int moves=g.Shots;
  for(int i=0;i<5&&app.QABattleTour!=null;i++){yield return new WaitForSecondsRealtime(.12f);Check(app.QABattleTour.Step==i&&app.QABattleTour.FocusRect.width>30&&app.QABattleTour.FocusRect.height>30,"tour focus "+i);Check(g.IsPaused&&g.Shots==moves&&g.SpawnCount==0,"tour freezes gameplay "+i);yield return Capture("tour-"+i);app.QABattleTour.AdvanceForQA();yield return null;}
  yield return Dismiss();Check(app.Profile.HasBattleInterfaceTour&&LocalProfile.Load(store).HasBattleInterfaceTour,"tour completion persists");Check(!g.IsPaused,"tutorial resumes after all tour steps");
  app.QAStartFirstBounce();yield return app.QAWaitForLevel();yield return Dismiss();Check(app.QABattleTour==null,"completed tour skipped on retry");
  foreach(int index in new[]{0,4,5,10,17,20,30}){app.QAGardenLesson(index);yield return new WaitForSecondsRealtime(1.1f);Check(app.QAAlphabetIntro.LayoutFits&&app.QAAlphabetIntro.GetComponentInChildren<LessonCollectionFlight>()!=null,"mechanism lesson and flight "+index);yield return Capture("lesson-"+(index+1));app.QAAlphabetIntro.AdvanceForQA();yield return new WaitForSecondsRealtime(.25f);}
  g.SetPaused(false);var meter=app.GetComponentInChildren<BattleScoreMeter>();
  foreach(int side in new[]{0,1,2}){for(int i=0;i<400;i++)meter.Advance(1f/60);int before=meter.BallResponses;var point=side==1?new Vector2(0,JellyWallFrame.TopCenter):new Vector2(side==0?-JellyWallFrame.SideCenter:JellyWallFrame.SideCenter,-4);g.JellyWalls.Impact(point,side,12);float[] peak=new float[3];for(int i=0;i<45;i++){meter.Advance(1f/60);for(int j=0;j<3;j++)peak[j]=Mathf.Max(peak[j],Mathf.Abs(meter.Swing(j)));}Check(meter.BallResponses==before+1&&peak.All(v=>v>=7&&v<=36),"wall "+side+" noticeably swings every star peaks="+string.Join(",",peak));}
  g.SetPaused(true);float paused=meter.Swing(0);g.JellyWalls.Impact(new Vector2(0,7),1,12);meter.Advance(.05f);Check(meter.Swing(0)==paused,"pause freezes stronger swing");g.SetPaused(false);
  Physics2D.simulationMode=SimulationMode2D.Script;
  g.enabled=false;Check(g.TryShoot(Vector2.up),"real wall collision fixture launches a live ball");for(int i=0;i<3;i++)g.QAAdvanceOneStep();var realBall=g.QARefillFirstBall();Check(realBall!=null,"live ball exists for wall physics");
  if(realBall!=null)foreach(int side in new[]{0,1,2}){int before=meter.BallResponses;var velocity=side==0?Vector2.left:side==1?Vector2.up:Vector2.right;var p=side==0?new Vector2(FlightGeometry.Left+.4f,-4):side==1?new Vector2(0,FlightGeometry.Top-.4f):new Vector2(FlightGeometry.Right-.4f,-4);realBall.ResumeChainFlight(p,velocity*12);for(int i=0;i<5;i++)g.QAAdvanceOneStep();bool reflected=side==0?realBall.Body.linearVelocity.x>0:side==1?realBall.Body.linearVelocity.y<0:realBall.Body.linearVelocity.x<0;Check(meter.BallResponses>before&&reflected,"actual Rigidbody2D ball collision drives star response and reflection "+side);}
  Check(OpeningIntroduction.LessonKeys(31).Contains(30),"copper introduction follows actual appearance beyond fixed level");
  Fixture();for(int i=0;i<6;i++)g.QAThirdNormal(i,8);g.QAOnboardRefillTick();Check(g.RefillDirectBreakables==6&&g.RefillDirectLowBatches==0,"six directly breakable pieces do not trigger new gate");g.QARefillRemove(g.CollectionBricks.First(b=>b.Alive));g.QARefillHold(true);g.QAOnboardRefillTick();Check(g.RefillDirectLowBatches==1&&g.RefillGapSnapshot==76&&g.RefillPending==50,"five pieces reserve normal floor(gaps*2/3) immediately during effect barrier");for(int i=0;i<20;i++)g.QAOnboardRefillTick();Check(g.RefillDirectLowBatches==1&&g.RefillPending==50,"persistent low count does not duplicate batch");g.QARefillHold(false);
  Fixture();g.QAThirdNormal(0,8);var ingot=g.QAThirdNormal(1,8);g.QAThirdIngot(ingot);g.QAOnboardCopper(2,8);g.QASevenTool(2,3,8,1);g.QASevenTool(3,4,8,0);g.QAThirdCat(6,5);g.QAAuditYarn(7,5);Check(g.RefillDirectBreakables==5,"ordinary, ingot, copper and two specials count; cat and yarn excluded");g.QAOnboardRefillTick();Check(g.RefillDirectLowBatches==1,"mixed sparse board triggers");
  Fixture();for(int i=0;i<5;i++)g.QAOnboardCopper(i,8);g.QAThirdGoals(new OpeningTarget{kind="copper_coin",count=5});g.QAOnboardCompleteGoals();g.QAOnboardRefillTick();Check(g.RefillDirectLowBatches==0&&g.RefillPending==0,"earned victory prevents emergency supply");
  Fixture();for(int i=0;i<5;i++)g.QAThirdNormal(i,8);g.QAOnboardRefillTick();Check(g.RefillDirectLowBatches==1,"new round resets low gate");g.QAThirdStep(.02f);for(int i=0;i<900;i++)g.QAThirdStep(.02f);Check(g.RefillDirectLowBatches==1&&g.RefillGenerated==50&&g.ChainGridValid(false),"one emergency batch settles without overlap or repeat");
  Fixture();for(int i=5;i<81;i++)g.QASevenBrick(i%9,i/9,1,0,1,0,true);for(int i=0;i<4;i++)g.QAThirdNormal(i,0);g.QAOnboardRefillTick();int prior=g.RefillDirectLowBatches;foreach(var b in g.CollectionBricks.Where(b=>b.Alive&&b.Definition.kind!=BrickKind.Stone).Take(2).ToArray())g.QARefillRemove(b);g.QAOnboardRefillTick();Check(g.RefillDirectBreakables==2&&g.RefillDirectLowBatches==prior+1&&g.RefillPending==2,"dense terrain can rearm after a new removal while count stays below six");
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

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
