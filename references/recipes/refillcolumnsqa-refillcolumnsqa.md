# RefillColumnsQA · RefillColumnsQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/RefillColumnsQA.cs`
文件 SHA-256：`c6261811d299b0528144d4ab6d627b705670f1df1a1ffc0e0ab6160b7438983e`
[原始源码快照](../source/Assets/Scripts/RefillColumnsQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L13

方法 SHA-256：`076ba657775b4197eb3cfb6491a08f5fbddd1b9e74aad06bf5922cd61219f644`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.RefillColumnsQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  Application.logMessageReceived+=(s,t,k)=>{if(k==LogType.Error||k==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",s+"\n"+t+"\n");}};
  yield return new WaitForSecondsRealtime(4);g=FindFirstObjectByType<AppUI>().Session;Physics2D.simulationMode=SimulationMode2D.Script;
  Reset();Tick(10);Check(g.RefillLowBoardBatches==0&&g.RefillPending==0,"initial empty fixture does not start repeated refill");
  for(int i=0;i<28;i++)g.QAThirdNormal(i%9,i/9+5);
  g.QARefillRemove(g.CollectionBricks.Last());Tick(1);Check(g.ChainOccupiedCells()==27&&g.RefillLowBoardBatches==0,"exactly one third does not trigger low-board refill");
  g.QARefillRemove(g.CollectionBricks.Last(b=>b.Alive));Tick(1);Check(g.RefillLowBoardBatches==1&&g.RefillGapSnapshot==55&&g.RefillPending+g.RefillGenerated==36,"crossing below one third reserves floor of two thirds of all gaps");
  Tick(800);Check(g.RefillLowBoardBatches==1&&g.RefillGenerated==36&&g.ChainOccupiedCells()==62&&g.ChainGridReady&&g.ChainGridValid(false),"low-board batch settles once without topping off repeatedly");
  foreach(var b in g.CollectionBricks.Where(b=>b.Alive).Take(36).ToArray())g.QARefillRemove(b);Tick(800);Check(g.RefillLowBoardBatches==2&&g.ChainGridReady,"density trigger rearms after recovery and a new drop below threshold");
  Reset();var cat=g.QAThirdCat(4,3);var anchor=cat.transform.position;
  for(int r=0;r<9;r++)for(int c=0;c<9;c++)if(c!=4||r<3)g.QAThirdNormal(c,r);
  // Only the five cells below the fixed cat are vacant; all other columns are full.
  g.QARefillFull();bool crossed=false,protectedAll=true;for(int i=0;i<800;i++){
   Tick(1);foreach(var b in g.CollectionBricks.Where(b=>b.Alive&&g.QARefillIsProtected(b))){protectedAll&=!b.GetComponent<BoxCollider2D>().enabled&&b.Face.color.a<.6f;if(Mathf.Abs(b.GetComponent<Rigidbody2D>().position.y-anchor.y)<.35f)crossed=true;}
   if(i%80==0)yield return null;
  }
  Check(crossed&&protectedAll,"ghost arrivals visibly cross the cat with collision disabled and reduced opacity");
  Check(g.RefillGenerated==5&&g.ChainOccupiedCells()==81&&g.ChainGridReady&&g.ChainGridValid(),"column capacity selects only the cat column and fills every hidden gap");
  Check(cat.transform.position==anchor&&cat.Brick.Alive,"ghost supply leaves fixed cat anchor and health intact");
  Check(g.CollectionBricks.Where(b=>b.Alive&&!g.OpeningCatBrick(b)).All(b=>!g.QARefillIsProtected(b)&&b.GetComponent<BoxCollider2D>().enabled&&b.Face.color.a>.99f),"landed arrivals regain solid colliders and full opacity");
  Reset();g.QAThirdNormal(0,8);g.QARefillRemove(g.CollectionBricks.Last());Tick(800);Check(g.RefillLowBoardBatches==1&&g.RefillGenerated==54&&g.ChainOccupiedCells()==54,"cleared board also uses the agreed two-thirds allowance");
  File.WriteAllText(dir+"/report.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(){g.QAThirdFixture();}
```

## 源字段与依赖

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
