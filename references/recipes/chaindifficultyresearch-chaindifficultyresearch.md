# ChainDifficultyResearch · ChainDifficultyResearch

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChainDifficultyResearch.cs`
文件 SHA-256：`982ef1653780784fbd32fd59c0f7204a8ea1d5129437fd30f044ca66f998c2b2`
[原始源码快照](../source/Assets/Scripts/ChainDifficultyResearch.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L22

方法 SHA-256：`a5338e8c838096c4ba43cc5af33b3418995ce59192dce6337d1baee6236f70b3`
时间来源：WaitForSecondsRealtime
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.ChainDifficultyResearch.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){report.errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);FindFirstObjectByType<AppUI>().QANavigate("home");
  game=new GameObject("Research session").AddComponent<GameSession>();game.QAIgnoreFocusLoss=true;
  var originalMode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  string[] names={"vertical","fixed_angle","random","bottom_direct","tools_and_banks","objective_first","rear_aware"};
  for(int policy=0;policy<names.Length;policy++)for(int variant=0;variant<3;variant++){
   game.StartLevel(53);yield return new WaitForSecondsRealtime(.4f);
   var run=new Run{policy=names[policy],variant=variant};var random=new System.Random(913+variant*101);
   while(game.IsPlaying&&run.turns<25){
    var turn=new Turn{round=game.CollectionRound,bricksBefore=game.CollectionBricks.Count(b=>b.Alive),healthBefore=game.CollectionBricks.Where(b=>b.Alive).Sum(b=>b.CurrentHealth)};
    var ids=new HashSet<int>(game.CollectionBricks.Where(b=>b.Alive).Select(b=>b.Id));var oldTools=new HashSet<ChainTool>(game.ChainTools);
    int breaks=game.BreakCount,generated=game.ChainGeneratedCount,activated=game.ChainActivationCount,progress=game.Collected.Sum();
    Vector2 direction=Vector2.up;
    var target=game.CollectionBricks.Where(b=>b.Alive).OrderBy(b=>b.transform.position.y).ThenBy(b=>Mathf.Abs(b.transform.position.x)).FirstOrDefault();
    if(policy==1||policy==2){float a=(policy==1?new[]{30f,90f,150f}[variant]:12+(float)random.NextDouble()*156)*Mathf.Deg2Rad;direction=new Vector2(Mathf.Cos(a),Mathf.Sin(a));}
    else if(policy>=3){
     var tool=game.ChainTools.OrderBy(t=>t.Position.y).FirstOrDefault();
     if(policy==5&&target!=null&&target.transform.position.y>-2)target=game.CollectionBricks.Where(b=>b.Alive).OrderByDescending(b=>b.Definition.juiceGroup>0).ThenBy(b=>b.transform.position.y).FirstOrDefault();
     if(policy>=4&&run.turns==0){float a=new[]{160f,60f,120f}[variant]*Mathf.Deg2Rad;direction=new Vector2(Mathf.Cos(a),Mathf.Sin(a));}
     else if(policy>=4&&tool!=null)direction=tool.Position-game.LauncherPosition;
     else if(target!=null){Vector2 p=target.transform.position;bool bank=policy>=4&&run.turns%3==0;direction=(bank?new Vector2((p.x<0?-8.2f:8.2f)-p.x,p.y):p)-game.LauncherPosition;}
    }
    if(policy==6)direction=PickGuardShot(variant);
    if(!game.TryShoot(direction)){report.errors++;break;}
    int steps=0;float low=Clearance();do{game.QAAdvanceOneStep();steps++;low=Mathf.Min(low,Clearance());if(game.RecallAvailable)game.RecallBalls();}while(game.IsPlaying&&(!game.CollectionReady||steps<2)&&steps<3000);
    if(steps>=3000){report.errors++;break;}
    turn.bricksAfter=game.CollectionBricks.Count(b=>b.Alive);turn.healthAfter=game.CollectionBricks.Where(b=>b.Alive).Sum(b=>b.CurrentHealth);
    turn.broken=game.BreakCount-breaks;turn.spawned=game.CollectionBricks.Count(b=>b.Alive&&!ids.Contains(b.Id));turn.generated=game.ChainGeneratedCount-generated;turn.activated=game.ChainActivationCount-activated;
    turn.generatedSurvived=game.ChainTools.Count(t=>!oldTools.Contains(t));turn.sameVolleyActivated=turn.generated-turn.generatedSurvived;
    turn.clearanceRows=low;turn.goalProgress=game.Collected.Sum()-progress;run.minimumClearanceRows=Mathf.Min(run.minimumClearanceRows,low);if(low<=2)run.riskyTurns++;
    run.trace.Add(turn);run.turns++;yield return new WaitForSecondsRealtime(.3f);
   }
   run.ended=!game.IsPlaying;run.fish=game.Collected[0];run.toys=game.Collected[1];run.won=game.CollectionWon;run.generated=game.ChainGeneratedCount;run.activated=game.ChainActivationCount;run.blockedHits=game.ChainBlocks;run.weakHits=game.ChainRearHits;run.armorBroken=game.ChainArmorBreaks;report.runs.Add(run);
   File.WriteAllText(Path.Combine(dir,"research.json"),JsonUtility.ToJson(report,true));
   File.AppendAllText(Path.Combine(dir,"summary.txt"),$"{run.policy}/{variant} turns={run.turns} won={run.won} minimumClearanceRows={run.minimumClearanceRows:F2} riskyTurns={run.riskyTurns} tools={run.generated}/{run.activated} blocked={run.blockedHits} rear={run.weakHits} armor={run.armorBroken}\n");
  }
  game.StopLevel();Destroy(game.gameObject);Physics2D.simulationMode=originalMode;File.WriteAllText(Path.Combine(dir,"research.json"),JsonUtility.ToJson(report,true));Application.Quit(report.errors==0?0:1);
 }
```

### PickGuardShot · L62

方法 SHA-256：`a3157bcdd848c6be79cb474be3be83a066aa48d2a7dd2829085611386070420a`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.ChainDifficultyResearch.PickGuardShot(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainDifficultyResearch.cs:46` · `ChainDifficultyResearch.Start` → `PickGuardShot(variant)`

```csharp
Vector2 PickGuardShot(int variant){
  float best=float.NegativeInfinity;Vector2 choice=Vector2.up;
  // An analysis-only geometric planner. It predicts one ball, then fires five real balls.
  // No game state is rolled back or edited and no damage is issued by this function.
  for(int angle=12+variant;angle<=168;angle+=3){
   float a=angle*Mathf.Deg2Rad;Vector2 direction=new Vector2(Mathf.Cos(a),Mathf.Sin(a)),d=direction,p=game.SpawnPosition(d);
   float score=0,budget=90;var hp=new System.Collections.Generic.Dictionary<int,int>();var opened=new System.Collections.Generic.HashSet<int>();Collider2D previous=null;
   for(int bounce=0;bounce<70&&budget>.01f;bounce++){
    var hits=Physics2D.CircleCastAll(p,FlightGeometry.Radius,d,budget,1<<8);
    RaycastHit2D hit=default;bool found=false;
    foreach(var candidate in hits.OrderBy(h=>h.distance)){
     if(candidate.collider==null||candidate.collider.isTrigger||candidate.distance<.005f&&candidate.collider==previous)continue;
     var cb=candidate.collider.GetComponentInParent<BrickBody>();if(cb!=null&&(!cb.Alive||hp.TryGetValue(cb.Id,out int gone)&&gone<=0))continue;
     hit=candidate;found=true;break;
    }
    if(!found)break;
    if(d.y<0&&hit.centroid.y<-6.1f)break;
    budget-=Mathf.Max(.02f,hit.distance);var b=hit.collider.GetComponentInParent<BrickBody>();
    if(b!=null){
     if(!hp.TryGetValue(b.Id,out int health))health=b.CurrentHealth;
     float urgency=Mathf.Max(0,7-(b.transform.position.y-.38f-CollectionLevels.Danger)/CollectionLevels.Pitch);
     if(b.Definition.kind==BrickKind.Stone)score-=.1f;
     else if(b.Definition.kind==BrickKind.RearWeak&&!game.ChainRearEntry(b,d,hit.point))score-=.2f;
     else if(b.ShellIntact&&!opened.Contains(b.Id)){opened.Add(b.Id);score+=1+urgency;}
     else {
      int damage=b.Definition.kind==BrickKind.RearWeak?3:1;
      score+=(b.Definition.kind==BrickKind.RearWeak?4+urgency:b.Definition.kind==BrickKind.Armor?1+urgency:0)+Mathf.Min(damage,health)*.3f;
      health-=damage;hp[b.Id]=health;
      if(health<=0)score+=b.Definition.juiceGroup>0?7:b.Definition.kind!=BrickKind.Normal?5+urgency*2:1.5f;
     }
    }
    previous=hit.collider;d=Vector2.Reflect(d,hit.normal).normalized;p=hit.centroid+hit.normal*.02f;
   }
   if(score>best){best=score;choice=direction;}
  }
  return choice;
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
GameSession game;
```
```csharp
Report report=new Report();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
