# CollisionHapticsQA · CollisionHapticsQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/CollisionHapticsQA.cs`
文件 SHA-256：`6ae425e2c365000c1408c1700f2395399264f5080c2906d43675d543c6c305de`
[原始源码快照](../source/Assets/Scripts/CollisionHapticsQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L14

方法 SHA-256：`ae92de4afd23d46f25f493cfe271f6c77856cfeb49b75466caba89a4e3e532cd`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CollisionHapticsQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);
  Check(RuntimeQA.IsIsolated,"QA uses memory profile");
  PlayerHaptics71.MobilePulse=(strength,done)=>{pulses.Add(strength);done?.Invoke(true);};PlayerHaptics71.Enabled=true;
  PlayerHaptics71.Pulse();PlayerHaptics71.Pulse();PlayerHaptics71.Pulse(PlayerHaptics71.Strength.Heavy);
  Check(pulses.SequenceEqual(new[]{PlayerHaptics71.Strength.Medium,PlayerHaptics71.Strength.Medium,PlayerHaptics71.Strength.Heavy}),"rapid collisions retain both medium pulses and immediate heavy pulse");
  int longCount=pulses.Count;PlayerHaptics71.Pulse();PlayerHaptics71.Pulse(PlayerHaptics71.Strength.Heavy);Check(pulses.Count==longCount,"active long vibration cannot be interrupted by medium or restarted by a chain");
  yield return new WaitForSecondsRealtime(.45f);PlayerHaptics71.Pulse();Check(pulses.Count==longCount+1&&pulses.Last()==PlayerHaptics71.Strength.Medium,"medium collisions resume immediately after long vibration window");
  pulses.Clear();PlayerHaptics71.Enabled=false;PlayerHaptics71.Pulse();PlayerHaptics71.Pulse(PlayerHaptics71.Strength.Heavy);Check(pulses.Count==0,"disabled switch blocks both strengths");PlayerHaptics71.Enabled=true;Check(pulses.Count==0,"enabling switch has no preview");
  bool? result=null;PlayerHaptics71.MobilePulse=(strength,done)=>throw new Exception("test failure");PlayerHaptics71.TryPulse(PlayerHaptics71.Strength.Heavy,ok=>result=ok);Check(result==false,"platform exception reports failure");
  PlayerHaptics71.MobilePulse=(strength,done)=>{pulses.Add(strength);done?.Invoke(true);};
  int failedCount=pulses.Count;PlayerHaptics71.Pulse();Check(pulses.Count==failedCount+1,"failed long request releases motor priority immediately");
  var app=FindFirstObjectByType<AppUI>();app.QANavigate("home");var g=app.Session;g.StartLevel(0);g.QAIgnoreFocusLoss=true;g.SetPaused(false);g.enabled=false;Physics2D.simulationMode=SimulationMode2D.Script;g.QASevenClear();
  var material=new PhysicsMaterial2D("Haptic contact test"){friction=0,bounciness=1};var obj=new GameObject("Haptic test ball");obj.transform.SetParent(g.transform);var ball=obj.AddComponent<BallBody>();ball.Create(g,material);
  pulses.Clear();ball.Initialize(9000,g.Generation,new Vector2(FlightGeometry.Right-.5f,0),Vector2.right*g.CurrentLevel.ballSpeed,0);Physics2D.SyncTransforms();
  for(int i=0;i<8;i++){ball.QATick();Physics2D.Simulate(.02f);}
  Check(pulses.Count>0&&pulses.All(s=>s==PlayerHaptics71.Strength.Medium),"real wall contact emits medium short impact");ball.Deactivate();
  g.QASevenClear();var brick=g.QASevenBrick(4,4,999);Vector2 pos=brick.transform.position;pulses.Clear();ball.Initialize(9000,g.Generation,pos+Vector2.left*.8f,Vector2.right*g.CurrentLevel.ballSpeed,0);Physics2D.SyncTransforms();
  for(int i=0;i<7;i++){ball.QATick();Physics2D.Simulate(.02f);}
  Check(pulses.Count>0&&pulses.All(s=>s==PlayerHaptics71.Strength.Medium),"real ordinary brick contact emits medium short impact");ball.Deactivate();
  SoundService.Played+=sounds.Add;
  foreach(int kind in new[]{1,2,3,5,6}){
   SoundService.CancelTransient();g.QASevenClear();var tool=g.QASevenTool(kind,4,4,0);pulses.Clear();sounds.Clear();g.ActivateChainTool(tool,Vector2.up);
   Check(pulses.Count==1&&pulses[0]==PlayerHaptics71.Strength.Heavy,"special activation requests one protected long vibration kind="+kind);
   string cue=kind==3?"rainbow_sweep":kind==2?"bomb":"cross_laser";
   Check(sounds.Contains(cue),"special activation plays matching existing SFX "+cue);
   pulses.Clear();g.ActivateChainTool(tool,Vector2.up);Check(pulses.Count==0,"consumed special cannot pulse again kind="+kind);
   yield return new WaitForSecondsRealtime(.45f);
  }
  foreach(var pair in new[]{new[]{2,2},new[]{2,1},new[]{1,5},new[]{3,3},new[]{3,2}}){
   SoundService.CancelTransient();g.QASevenClear();var first=g.QASevenTool(pair[0],4,4,0);g.QASevenTool(pair[1],5,4,0);pulses.Clear();sounds.Clear();g.ActivateChainTool(first,Vector2.up);
   Check(pulses.Count>0&&pulses.All(s=>s==PlayerHaptics71.Strength.Heavy),"combo requests protected long vibration "+pair[0]+"+"+pair[1]);
   string cue=pair.Contains(3)?"rainbow_sweep":pair.Contains(2)?"bomb":"cross_laser";
   Check(sounds.Contains(cue),"combo sound matches effect "+pair[0]+"+"+pair[1]);
   if(pair[0]==2&&pair[1]==1)Check(sounds.Contains("cross_laser"),"bomb and laser combo layers existing blast and beam SFX");
   yield return new WaitForSecondsRealtime(.45f);
  }
  g.QASevenClear();var locked=g.QASevenTool(2,4,4,0);locked.ReadyAt=float.MaxValue;pulses.Clear();g.ActivateChainTool(locked,Vector2.up);Check(pulses.Count==0,"protected special emits no impact before activation");
  foreach(string cue in new[]{"wall","hit","break","bomb","cross_laser","rainbow_sweep"})Check(SoundService.QAClip(cue)!=null,"runtime audio asset resolves "+cue);
  Check(errors==0,"zero runtime errors");SoundService.Played-=sounds.Add;PlayerHaptics71.MobilePulse=null;g.StopLevel();
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");
  #if UNITY_EDITOR
  UnityEditor.EditorApplication.Exit(failures+errors==0?0:1);
  #else
  Application.Quit(failures+errors==0?0:1);
  #endif
 }
```

## 对象层级和素材入口

- L29：`obj.transform.SetParent(g.transform)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
readonly List<PlayerHaptics71.Strength> pulses=new List<PlayerHaptics71.Strength>();
```
```csharp
readonly List<string> sounds=new List<string>();
```
```csharp
string dir;
```
```csharp
int checks,failures,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
