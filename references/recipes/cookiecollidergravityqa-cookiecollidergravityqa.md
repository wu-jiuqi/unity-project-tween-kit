# CookieColliderGravityQA · CookieColliderGravityQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/CookieColliderGravityQA.cs`
文件 SHA-256：`92505e6fbf23ba7d361b0cbb8e05a9cd69b761c27f0b33beac8c3d2b055309c9`
[原始源码快照](../source/Assets/Scripts/CookieColliderGravityQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L34

方法 SHA-256：`996c17d89ad94f67034b36346e3e1122548dc809e915bc3f3a3313a9bc71b453`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CookieColliderGravityQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();g=app.Session;Physics2D.simulationMode=SimulationMode2D.Script;
  for(int level=10;level<20;level++){
   g.StartLevel(level);g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);Physics2D.SyncTransforms();
   foreach(var box in g.QASecondBoxes()){
    var collider=box.Brick.GetComponent<BoxCollider2D>();
    Check(Vector2.Distance(collider.size,CookieBoxArtView.CollisionSize)<.00001f,"authored level "+(level+1)+" collider covers complete four cells");
    Check(box.Brick.GetComponentsInChildren<Collider2D>().Length==1&&box.Brick.GetComponent<ChainGridPiece>().Size==2,"one rigid collider and grid owner "+(level+1));
   }
  }
  Reset();var isolated=g.QASecondBox();var cb=isolated.Brick.GetComponent<BoxCollider2D>();var normal=g.QAOpeningSpawn(0,0,0).GetComponent<BoxCollider2D>();
  Check(Vector2.Distance(cb.size,normal.size*2)<.00001f,"cookie bounds equal two ordinary cells on each axis");
  Physics2D.SyncTransforms();Vector2 anchor=cb.bounds.center;float p=CollectionLevels.Pitch;
  foreach(float x in new[]{-.75f,-.5f,0,.5f,.75f})foreach(float y in new[]{-.75f,-.5f,0,.5f,.75f})Check(cb.OverlapPoint(anchor+new Vector2(x,y)*p),"four quadrants and central seams are solid "+x+","+y);
  foreach(var d in new[]{Vector2.left,Vector2.right,Vector2.up,Vector2.down})Check(!cb.OverlapPoint(anchor+d*(p+.01f)),"collider stops at outer four-cell boundary "+d);
  CastEdges(isolated);Vector2 size=cb.size,position=isolated.Brick.GetComponent<Rigidbody2D>().position;isolated.Collect(0,90001,1);isolated.Tick(.08f);Physics2D.SyncTransforms();
  Check(isolated.VisualHeight>.1f&&cb.size==size&&isolated.Brick.GetComponent<Rigidbody2D>().position==position,"whole-box hop leaves complete collision boundary stationary");CastEdges(isolated);
  for(int count=0;count<=8;count++){isolated.GetComponentInChildren<CookieBoxArtView>().SetState(count);Check(cb.size==size,"sprite count "+count+" never changes collider dimensions");}
  foreach(int layers in new[]{2,3}){
   Reset();Floor(5);var plain=g.QAOpeningSpawn(2,3,0);var yarn=g.QASupportYarn(4,3,layers);var supportPlain=g.QAOpeningSpawn(2,4,0);var supportYarn=g.QAOpeningSpawn(4,4,0);
   Vector2 plainStart=plain.GetComponent<Rigidbody2D>().position,yarnStart=yarn.Brick.GetComponent<Rigidbody2D>().position;Break(supportPlain);Break(supportYarn);Tick(10);yield return new WaitForEndOfFrame();
   Vector2 plainNow=plain.GetComponent<Rigidbody2D>().position,yarnNow=yarn.Brick.GetComponent<Rigidbody2D>().position;
   Check(yarn.Brick.Alive&&yarn.Layers==layers-1,"support elimination peels one layer without collecting remaining yarn "+layers);
   Check(plainNow.y<plainStart.y-.1f&&yarnNow.y<yarnStart.y-.1f,"normal and yarn start dropping after support removal "+layers);
   Check(Mathf.Abs(plainNow.y-yarnNow.y)<.015f&&yarn.Brick.GetComponent<ChainGridPiece>().Row==4,"yarn fills the same one-row gap as ordinary brick "+layers);
   var effect=g.QASupportEffects().FirstOrDefault();Check(effect!=null&&Vector3.Distance(effect.transform.position,yarn.Brick.transform.position+Vector3.down*(p*.5f))<.01f,"visible peel animation follows falling yarn "+layers);
   Tick(50);Check(g.ChainGridValid(false),"yarn and floor settle without overlap "+layers);
  }
  Reset();Floor(5);var whole=g.QASecondBox(3,2);var left=g.QAOpeningSpawn(3,4,0);var right=g.QAOpeningSpawn(4,4,0);Vector2 start=whole.Brick.GetComponent<Rigidbody2D>().position;Break(left);Tick(10);
  Check(Vector2.Distance(whole.Brick.GetComponent<Rigidbody2D>().position,start)<.002f,"one remaining supporting column prevents overlap of whole 2x2 box");
  Break(right);Tick(10);yield return new WaitForEndOfFrame();
  Check(whole.Brick.Alive&&whole.Brick.GetComponent<ChainGridPiece>().Row==3&&Mathf.Abs(whole.Brick.GetComponent<Rigidbody2D>().position.y-(start.y-p))<.015f,"box drops one row as a unit when both supporting cells are cleared");
  Check(whole.Remaining==6&&whole.Brick.GetComponent<BoxCollider2D>().size==CookieBoxArtView.CollisionSize,"support clears collect two biscuits without altering full collision size");
  Tick(70);Check(g.ChainGridValid(false),"four-cell box settles into gap without overlap or splitting");
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+",\"colliderSide\":"+(2*p).ToString(System.Globalization.CultureInfo.InvariantCulture)+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口

- L46：`g.QASecondBox()`
- L64：`g.QASecondBox(3,2)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(){g.StartLevel(10);g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);g.QASevenClear();g.CollectionGoal[0]=g.CollectionGoal[1]=999;}
```

## 源字段与依赖

```csharp
GameSession g;
```
```csharp
AppUI app;
```
```csharp
string dir;
```
```csharp
int checks,failures,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
