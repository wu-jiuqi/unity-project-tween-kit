# OpeningBalanceChecks · OpeningBalanceChecks

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LevelEditor/OpeningBalanceChecks.cs`
文件 SHA-256：`c25c8ab6557b0a80ed5947131f170fb33a16edff270151622b963fad257f99e3`
[原始源码快照](../source/Assets/Scripts/LevelEditor/OpeningBalanceChecks.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Run · L8

方法 SHA-256：`94be6be8e28a5b84eace7c3bd8e5ee207483c39d7f0fcca1ab43a7fd12b36521`
时间来源：Time.fixedDeltaTime, Time.timeScale
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.OpeningBalanceChecks.Run(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/LevelBalanceRunner.cs:36` · `LevelBalanceRunner.Run` → `OpeningBalanceChecks.Run(LevelEditorRuntime.Arg("--qa-out"))`

```csharp
public static int Run(string directory){
  Directory.CreateDirectory(directory);var checks=new List<string>();int failures=0;
  Action<bool,string> check=(ok,name)=>{checks.Add((ok?"PASS ":"FAIL ")+name);if(!ok)failures++;};
  var root=new GameObject("Balance regression checks");var game=root.AddComponent<GameSession>();game.enabled=false;game.QAIgnoreFocusLoss=true;
  Physics2D.simulationMode=SimulationMode2D.Script;Time.fixedDeltaTime=1f/60;Time.timeScale=0;
  try{
   var data=JsonUtility.FromJson<OpeningData>(JsonUtility.ToJson(OpeningCampaign.Data));var s=data.levels[0];s.health=new OpeningHealth{ordinaryHp=3,refillHp=5};s.hpOverrides=new[]{new OpeningHpOverride{c=4,r=4,hp=7}};
   OpeningCampaign.LoadBalanceData(JsonUtility.ToJson(data));game.StartLevel(0);game.SetPaused(false);
   check(game.CollectionBricks.Where(b=>b.Definition.referenceCol==4&&b.Definition.referenceRow==4).Single().CurrentHealth==7,"local HP reaches physical brick");
   check(game.CollectionBricks.Where(b=>b.Definition.referenceCol!=4||b.Definition.referenceRow!=4).All(b=>b.CurrentHealth==3),"ordinary HP reaches every other physical brick");
   check(game.BalanceCheckRefillHp()==5,"refill HP reaches actual spawned body");
   game.StartLevel(37);game.SetPaused(false);game.BalanceCheckPendingArrival(check);game.BalanceCheckWaitingRoute(check);
   data.levels[0].health=null;data.levels[0].hpOverrides=null;OpeningCampaign.LoadBalanceData(JsonUtility.ToJson(data));game.StartLevel(0);
   check(game.CollectionBricks.All(b=>b.CurrentHealth==1),"old levels keep one-hit default");
   game.BalanceCheckSeeds(check);
   var weights=new BalanceWeights();var timing=new BalanceTimingState{CanLaunch=true,Groups=1,SinceShot=2};
   check(BalanceFirePolicy.ShouldFire("rapid",timing,weights,out _),"rapid mode permits overlapping groups");
   check(!BalanceFirePolicy.ShouldFire("settled",timing,weights,out _),"settled mode rejects an unfinished previous group");
   timing.Groups=0;timing.Settled=true;check(BalanceFirePolicy.ShouldFire("settled",timing,weights,out _),"settled mode fires after complete settlement");
   timing.Groups=1;timing.Settled=false;check(BalanceFirePolicy.ShouldFire("hybrid",timing,weights,out _),"hybrid can overlap on an open board");
   timing.SpecialOpportunity=1;timing.Effects=true;timing.Moving=true;check(!BalanceFirePolicy.ShouldFire("hybrid",timing,weights,out _),"hybrid waits for a valuable special/settlement opportunity");
   timing.Groups=weights.hybridMaxGroups;check(!BalanceFirePolicy.ShouldFire("hybrid",timing,weights,out _),"hybrid respects its live-group cap");
   check(BalanceFirePolicy.RayBox(Vector2.zero,Vector2.up,new Vector2(.7f,3),Vector2.one*.82f,out float distance)&&Mathf.Abs(distance-2.18f)<.001f,"aiming respects the whole 2x2 box footprint");
  }catch(Exception e){check(false,e.ToString());}
  finally{game.StopLevel();UnityEngine.Object.Destroy(root);}
  File.WriteAllLines(Path.Combine(directory,"checks.txt"),checks);File.WriteAllText(Path.Combine(directory,"summary.json"),"{\"checks\":"+checks.Count+",\"failures\":"+failures+"}");
  Debug.Log("OPENING_BALANCE_CHECKS "+checks.Count+" failures="+failures);return failures;
 }
```

## 对象层级和素材入口

- L30：`BalanceFirePolicy.RayBox(Vector2.zero,Vector2.up,new Vector2(.7f,3),Vector2.one*.82f,out float distance)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
