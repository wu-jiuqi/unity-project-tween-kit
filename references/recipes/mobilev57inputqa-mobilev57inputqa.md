# MobileV57InputQA · MobileV57InputQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/MobileV57InputQA.cs`
文件 SHA-256：`c022f301fd986352af5205eba210b43eb2b397e483de3e4b63314d024b81f5c0`
[原始源码快照](../source/Assets/Scripts/MobileV57InputQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L14

方法 SHA-256：`ad09e097b11056662bae66d4a070ef2e348c4fa6984725cec1d5db9a0406751f`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.MobileV57InputQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");app.QAStartLevel(1);yield return app.QAWaitForLevel();for(int i=0;i<18;i++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.1f);}var g=app.Session;g.QAIgnoreFocusLoss=true;g.SetPaused(false);g.enabled=false;var previous=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  Vector2 start=Camera.main.WorldToScreenPoint(BoardDisplay.Forward(g.LauncherPosition+Vector2.up*2));Vector2 end=start+Vector2.up*100;
  int shots=g.Shots;Send(17,0,start);Check(app.QAIsAiming,"native press enters aim");Send(17,1,end);Check(app.QAIsAiming&&g.AimPredictionCount>0,"native move updates aim");Send(29,2,end);Check(app.QAIsAiming,"second finger cannot release owner");Send(17,2,end);Check(!app.QAIsAiming&&app.BattleInputStatus=="accepted","press move release in one frame is accepted");g.QAAdvanceOneStep();Check(g.Shots==shots-1&&g.SpawnCount==1,"fast drag starts exactly one volley");
  Send(17,0,start);Send(17,1,end);Send(17,2,end);Check(g.ChainLaunchQueued,"next native drag queues during volley");for(int i=0;i<40;i++)g.QAAdvanceOneStep();Check(g.Shots==shots-2,"retained next release shoots automatically");
  Send(17,0,start);Send(17,1,end);Send(17,3,end);Check(!app.QAIsAiming,"touch cancel clears aim");int accepted=app.BattleAcceptedShots;Send(17,2,end);Check(app.BattleAcceptedShots==accepted,"late end after cancel cannot fire");
  Send(17,0,start);Send(-1,4,Vector2.zero);Check(!app.QAIsAiming,"app hide cancels held gesture");Send(17,2,end);Check(app.BattleAcceptedShots==accepted,"app hide never launches on late release");
  Send(17,0,start);Send(17,2,start);Check(app.BattleAcceptedShots==accepted,"tap without drag does not shoot");
  g.SetPaused(true);Send(17,0,start);Send(17,1,end);Send(17,2,end);Check(!app.QAIsAiming&&app.BattleAcceptedShots==accepted,"pause blocks all touch phases");g.SetPaused(false);Send(17,2,end);Check(app.BattleAcceptedShots==accepted,"resume never replays a paused gesture");
  var settings=FindObjectsByType<UnityEngine.UI.Button>(FindObjectsSortMode.None).First(b=>b.name=="Battle settings");Vector2 ui=RectTransformUtility.WorldToScreenPoint(null,settings.transform.position);Send(17,0,ui);Check(!app.QAIsAiming&&app.BattleInputStatus=="ui-hit","settings UI is excluded from aim");
  Check(app.BattleTouchStarts>0&&app.BattleTouchMoves>0&&app.BattleTouchEnds>0,"input stage counters retain evidence");Physics2D.simulationMode=previous;g.enabled=true;File.WriteAllText(dir+"/result.json",$"{{\"checks\":{checks},\"failures\":{failures},\"errors\":{errors}}}");Application.Quit(failures+errors==0?0:1);
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
string dir;
```
```csharp
int checks,failures,errors;
```
```csharp
static readonly MethodInfo Touch=typeof(AppUI).GetMethod("ProcessBattleTouch",BindingFlags.NonPublic|BindingFlags.Instance);
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
