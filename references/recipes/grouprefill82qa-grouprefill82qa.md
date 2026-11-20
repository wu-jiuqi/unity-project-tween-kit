# GroupRefill82QA · GroupRefill82QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/GroupRefill82QA.cs`
文件 SHA-256：`5fa51862d7b51122f94621e4795260438e3521397f7196bdfe05efa266728d46`
[原始源码快照](../source/Assets/Scripts/GroupRefill82QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Settle · L18

方法 SHA-256：`a3bbf7763a9a5dead033de2aa6a03f101ebb440df99a7e638d65ac7ba535257d`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GroupRefill82QA.Settle()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/GroupRefill82QA.cs:22` · `GroupRefill82QA.Start` → `Settle()`
- `Assets/Scripts/GroupRefill82QA.cs:23` · `GroupRefill82QA.Start` → `Settle()`
- `Assets/Scripts/GroupRefill82QA.cs:24` · `GroupRefill82QA.Start` → `Settle()`
- `Assets/Scripts/GroupRefill82QA.cs:25` · `GroupRefill82QA.Start` → `Settle()`
- `Assets/Scripts/GroupRefill82QA.cs:26` · `GroupRefill82QA.Start` → `Settle()`
- `Assets/Scripts/GroupRefill82QA.cs:27` · `GroupRefill82QA.Start` → `Settle()`
- `Assets/Scripts/GroupRefill82QA.cs:28` · `GroupRefill82QA.Start` → `Settle()`
- `Assets/Scripts/GroupRefill82QA.cs:30` · `GroupRefill82QA.Start` → `Settle()`
- `Assets/Scripts/GroupRefill82QA.cs:32` · `GroupRefill82QA.Start` → `Settle()`
- `Assets/Scripts/GroupRefill82QA.cs:32` · `GroupRefill82QA.Start` → `Settle()`

```csharp
IEnumerator Settle(){int steps=0;while((!g.ChainGridReady||g.ChainEffectsPending)&&steps++<1800){g.QASevenTick(.02f);if(steps%8==0)yield return null;}Check(steps<1800&&g.ChainGridValid(false),"queued arrivals settle with valid occupancy");}
```

### Capture · L19

方法 SHA-256：`13a27882af4529ccf9111b3d1d0c0388662c34c2ebeda044bfe9d3f57d8fcc63`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GroupRefill82QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/GroupRefill82QA.cs:25` · `GroupRefill82QA.Start` → `Capture("01-reward-and-refill")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,image);File.WriteAllBytes(dir+"/"+name+".png",image.EncodeToPNG());Destroy(image);rt.Release();Destroy(rt);}
```

### Start · L20

方法 SHA-256：`22ed3c396b6c40ce3cc84d7e18dcc1f786491ddafddb2cdaa81ae455a3a2b78d`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GroupRefill82QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");int fpsAt=Array.IndexOf(args,"--qa-fps");Application.targetFrameRate=fpsAt>=0?int.Parse(args[fpsAt+1]):60;QualitySettings.vSyncCount=0;Application.logMessageReceived+=(s,t,k)=>{if(k==LogType.Error||k==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",s+"\n"+t+"\n");}};
  yield return new WaitForSecondsRealtime(8);app=FindFirstObjectByType<AppUI>();g=app.Session;app.QAStartLevel(1);yield return app.QAWaitForLevel();for(int i=0;i<100;i++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.035f);}g.SetPaused(false);g.QAIgnoreFocusLoss=true;var old=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  g.QAGRFixture();int first=Launch(false);Check(g.RefillShots==1&&g.PendingBurstBalls==4&&g.RefillOrdinaryBatches==0,"launching never starts ordinary supply");var early=g.QAGRBalls(first).Single();g.LoseBall(early,early.Body.position);Check(g.QAGRRemaining(first)==4&&g.RefillSettledGroups==0&&g.RefillPending==0,"first ball cannot settle four unspawned siblings");g.QAGRSteps(45);Check(g.QAGRBalls(first).Length==4&&g.RefillGenerated==0,"remaining first-group balls fly without refill");g.QAGRFinish(first);Check(g.RefillSettledGroups==1&&g.RefillLastSettledGroup==first&&g.RefillOrdinaryBatches==1&&g.RefillGapSnapshot==8&&g.RefillPending==5,"last ball of first group snapshots two thirds of eight gaps without another shot");Check(g.ChainTools.Count==0&&g.ChainPendingReplacements==0,"a group below rating threshold still refills");g.QARefillFinishGroup(first);g.LoseBall(early,early.Body.position);Check(g.RefillOrdinaryBatches==1&&g.RefillPending==5,"duplicate group or ball callbacks cannot double refill");yield return Settle();Check(g.RefillGenerated==5&&g.ChainOccupiedCells()==78,"exactly five arrivals committed");
  g.QAGRFixture(20);first=Launch();int second=Launch(false);Check(g.RefillGenerated==0&&g.RefillOrdinaryBatches==0&&g.ActiveBalls==6,"two launches alone leave the corridor open");var secondEarly=g.QAGRBalls(second).Single();g.LoseBall(secondEarly,secondEarly.Body.position);g.QAGRFinish(first);Check(g.RefillSettledGroups==1&&g.RefillPending==13&&g.PendingBurstBalls==4&&g.QAGRRemaining(second)==4,"first group settles while second still has pending siblings");g.QAGRSteps(45);Check(g.RefillGenerated+g.RefillPending==13&&g.RefillSettledGroups==1&&g.QAGRBalls(second).Length==4,"second group remains independent during first-group supply");Check(g.ChainCanLaunch,"ordinary arrival queue does not gate continuous shooting");g.QAGRFinish(second);Check(g.RefillSettledGroups==2&&g.RefillOrdinaryBatches==2&&g.RefillGapSnapshot==7&&g.RefillGenerated+g.RefillPending==17,"second group later refills two thirds of the remaining seven gaps");yield return Settle();Check(g.RefillGenerated==17&&g.ChainOccupiedCells()==78,"two settlements turn twenty gaps into three remaining gaps");
  g.QAGRFixture(8);first=Launch();second=Launch();g.QAGRFinish(second);Check(g.RefillSettledGroups==1&&g.RefillLastSettledGroup==second&&g.QAGRBalls(first).Length==5&&g.RefillPending==5,"newer group can settle before older group");g.QAGRFinish(first);Check(g.RefillSettledGroups==2&&g.RefillPending==7&&g.RefillGapSnapshot==3,"same-frame settlements reserve pending cells rather than double-booking gaps");yield return Settle();
  g.QAGRFixture(0);first=Launch();var hitter=g.QAGRBalls(first)[0];foreach(var b in g.CollectionBricks.Where(b=>b.Alive&&b.GetComponent<ChainGridPiece>().Row==8).Take(4).ToArray())g.HitBrick(b,hitter,b.transform.position);Check(g.RefillGenerated==0&&g.ChainTools.Count==0&&g.ChainRatingTier==0,"four real direct breaks earn a laser but do not deliver before group return");foreach(var b in g.QAGRBalls(first).Take(4).ToArray())g.LoseBall(b,b.Body.position);Check(g.RefillOrdinaryBatches==0&&g.ChainTools.Count==0,"penultimate ball does not deliver rewards or supply");g.QAGRFinish(first);Check(g.ChainTools.Count==1&&g.RefillOrdinaryBatches==1&&g.RefillPending==2,"laser reward and two-thirds-gap refill are scheduled in the same group-return event");Check(g.ChainTools[0].Kind==1||g.ChainTools[0].Kind==5||g.ChainTools[0].Kind==6,"existing laser reward variety preserved");yield return Capture("01-reward-and-refill");yield return Settle();
  g.QAGRFixture();first=Launch();foreach(var b in g.QAGRBalls(first).Take(4))b.ChainBornAt=-100;g.QAGRSteps(1);Check(g.QAGRRemaining(first)==1&&g.RefillSettledGroups==0,"partial timeout cannot close group");g.QAGRBalls(first).Single().ChainBornAt=-100;g.QAGRSteps(1);Check(g.RefillSettledGroups==1&&g.RefillOrdinaryBatches==1,"final timed-out ball also settles its group once");yield return Settle();
  g.QAGRFixture(20);Launch();Launch();g.QAGRCanRecall();g.SetPaused(true);g.RecallBalls();Check(g.ActiveBalls==10&&g.RefillSettledGroups==0,"paused recall cannot settle any group");g.SetPaused(false);g.QAGRCanRecall();g.RecallBalls();Check(g.ActiveBalls==0&&g.RefillSettledGroups==2&&g.RefillPending==17,"manual recall settles each group once with reserved capacity");yield return Settle();
  g.QAGRFixture(8,1);first=Launch(false);Check(g.Shots==0&&!g.QAGRResolve(out _),"last move waits for live and pending group balls");g.QAGRSteps(45);g.QAGRFinish(first);Check(g.RefillPending==5&&!g.QAGRResolve(out _),"last-move group schedules supply without waiting for an impossible next input");yield return Settle();Check(g.QAGRResolve(out bool won)&&!won,"unfinished last move resolves after its one group refill");
  g.QAGRFixture();first=Launch();g.QAThirdGoals(new OpeningTarget{kind="ingot",count=0});g.QAGRFinish(first);Check(g.VictoryDeferredGroups==1&&g.RefillPending==0&&g.RefillOrdinaryBatches==0&&g.ChainPendingReplacements==0,"earned victory defers final group supply and rewards until victory settlement");
  g.QAGRFixture();first=Launch();g.QARefillFull();Check(g.RefillPending==8,"explicit full refill reserves current gaps");g.QAGRFinish(first);Check(g.RefillGapSnapshot==0&&g.RefillPending==8,"group settlement does not duplicate an already pending full refill");yield return Settle();
  g.QAGRFixture(0);first=Launch();g.QAGRFinish(first);Check(g.RefillOrdinaryBatches==1&&g.RefillPending==0&&g.RefillGapSnapshot==0,"zero-gap group settles without spawning or retry loops");
  g.QAGRFixture(5);first=Launch();g.QAGRFinish(first);Check(g.RefillPending==3&&g.RefillGapSnapshot==5,"odd gap count floors two thirds without accumulating duplicate credit");yield return Settle();second=Launch();g.QAGRFinish(second);Check(g.RefillPending==1&&g.RefillGapSnapshot==2,"a later group reuses old unfilled gaps even with no new breaks");yield return Settle();g.QAGRFixture(0);Check(g.RefillSettledGroups==0&&g.RefillLastSettledGroup==0&&g.RefillGenerated==0,"restart clears all group settlement identities and supply");
  for(int i=0;i<78;i++){g.StartLevel(i);g.enabled=false;Check(g.ChainGridValid(false)&&g.RefillOrdinaryBatches==0&&g.RefillGenerated==0,"level "+(i+1)+" authored layout preserved without launch-triggered supply");if(i%8==0)yield return null;}
  Physics2D.simulationMode=old;File.WriteAllText(dir+"/report.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+",\"version\":\""+GameVersion.Name+"\"}");Debug.Log("GROUP_REFILL82_QA checks="+checks+" failures="+failures+" errors="+errors);Application.Quit(failures==0&&errors==0?0:3);
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
GameSession g;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
