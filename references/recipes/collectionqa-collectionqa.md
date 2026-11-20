# CollectionQA · CollectionQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/CollectionQA.cs`
文件 SHA-256：`f7e148d80bbfbdb9701e0bb755628c3cd25425764e0f73b89cb60da6e3c0793b`
[原始源码快照](../source/Assets/Scripts/CollectionQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L21

方法 SHA-256：`95b1a8c1ef930067c6708170d0e8dad02346794ba542de51f426b5cd46bc2b08`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CollectionQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/CollectionQA.cs:26` · `CollectionQA.Start` → `Capture("00-picker")`
- `Assets/Scripts/CollectionQA.cs:27` · `CollectionQA.Start` → `Capture("0"+(n+1)+"-stage")`
- `Assets/Scripts/CollectionQA.cs:28` · `CollectionQA.Start` → `Capture("04-blocked-treats")`
- `Assets/Scripts/CollectionQA.cs:28` · `CollectionQA.Start` → `Capture("05-harvest")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var cam=Camera.main;var target=new RenderTexture(width,height,24);target.Create();var old=cam.targetTexture;cam.targetTexture=target;cam.rect=new Rect(0,0,1,1);cam.aspect=(float)width/height;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;Canvas.ForceUpdateCanvases();var design=(RectTransform)typeof(AppUI).GetField("designRoot",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).GetValue(app);var safe=(RectTransform)design.parent;design.localScale=Vector3.one*Mathf.Min(safe.rect.width/1080f,safe.rect.height/2340f);Canvas.ForceUpdateCanvases();cam.Render();var prev=RenderTexture.active;RenderTexture.active=target;var texture=new Texture2D(width,height,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,width,height),0,0);texture.Apply();File.WriteAllBytes(Path.Combine(dir,name+".png"),texture.EncodeToPNG());RenderTexture.active=prev;cam.targetTexture=old;Destroy(texture);target.Release();Destroy(target);}
```

### Start · L23

方法 SHA-256：`b4e6edf16cf14570d624d1930d8ba4f1c0575e4282a92773a70147c2c1e8d75d`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CollectionQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];if(Array.IndexOf(args,"--qa-width")>=0)width=int.Parse(args[Array.IndexOf(args,"--qa-width")+1]);if(Array.IndexOf(args,"--qa-height")>=0)height=int.Parse(args[Array.IndexOf(args,"--qa-height")+1]);Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();app.QASetViewport(new Vector2Int(width,height));app.Session.QAIgnoreFocusLoss=true;
  Check(RuntimeQA.IsIsolated,"isolated profile");Check(LevelCatalog.Count==40,"legacy catalog preserved");Check(!string.IsNullOrEmpty(RuleSelfTests.RunAll()),"existing rule regression");Check(!string.IsNullOrEmpty(LocalProfile.RunSelfTests()),"existing save regression");
  app.OpenCollectionPicker();yield return new WaitForSecondsRealtime(.4f);yield return Capture("00-picker");
  for(int n=0;n<3;n++){app.QAStartLevel(50+n);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.5f);Check(app.Session.IsCollection&&app.Session.LevelIndex==50+n,"collection picker stage "+n);yield return Capture("0"+(n+1)+"-stage");}
  g=app.Session;Empty();var demoBlock=Add(4,9);for(int row=5;row<8;row++)Break(Add(4,row,2));Set("collectionPhase",1);yield return new WaitForSecondsRealtime(.6f);yield return Capture("04-blocked-treats");Break(demoBlock);Set("collectionPhase",1);yield return new WaitForSecondsRealtime(.35f);yield return Capture("05-harvest");
  app.QANavigate("home");var go=new GameObject("Collection rule fixtures");g=go.AddComponent<GameSession>();g.QAIgnoreFocusLoss=true;var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  g.StartLevel(50);yield return new WaitForSecondsRealtime(.4f);Empty();var blocker=Add(4,5);var sandwich=Add(4,2,1);Break(sandwich);
  Check(g.Collected[0]==0&&g.CollectionTreats.Count==1,"breaking sandwich releases without collecting");Settle();Check(g.Collected[0]==0&&Mathf.Abs(g.CollectionTreats[0].View.position.y-(blocker.transform.position.y+.65f))<.01f,"released item stops above same-column blocker");
  int round=g.CollectionRound;Break(blocker);Settle();Check(g.Collected[0]==1&&g.CollectionTreats.Count==0,"open channel collects automatically");Check(g.CollectionRound==round,"tool settling does not descend or advance round");
  for(int i=0;i<50;i++)Break(Add(0,0));Check(g.CollectionEnergy==40,"energy caps at 40");
  var low=Add(4,6);var mid=Add(4,5);var high=Add(4,4);var keep=Add(4,3);var filled=Add(4,2,1);Break(Add(4,0,1));Settle();
  g.SelectCollectionTool(2);Check(g.UseCollectionTool(CollectionLevels.Cell(4,4)),"clear yarn usable between volleys");Check(!low.Alive&&!mid.Alive&&!high.Alive&&keep.Alive&&filled.Alive,"clear yarn removes bottommost three ordinary bricks only");Check(g.CollectionEnergy==20,"tool destruction grants no energy");Settle();
  g.SelectCollectionTool(1);Check(!g.UseCollectionTool(filled.transform.position),"paw rejects still-wrapped item");var released=g.CollectionTreats[0];Check(g.UseCollectionTool(released.View.position),"paw collects released item through blocking bricks");Check(g.CollectionEnergy==8&&g.Collected[0]==2,"paw costs exactly 12 and counts once");Settle();Check(g.CollectionRound==round,"both tools preserve round");
  g.SelectCollectionTool(2);Check(!g.UseCollectionTool(CollectionLevels.Cell(4,3))&&g.CollectionEnergy==8,"insufficient energy has no effects");g.SelectCollectionTool(2);
  g.SetPaused(true);int before=g.CollectionRound;Check(!g.TryShoot(Vector2.up)&&!g.SelectCollectionTool(1),"pause blocks shooting and tools");Steps(100);Check(g.CollectionRound==before,"pause freezes settling");g.SetPaused(false);
  g.StartLevel(50);yield return new WaitForSecondsRealtime(.4f);var first=g.CollectionBricks.First();float y=first.transform.position.y;Check(g.TryShoot(Vector2.up),"shot accepted at idle");g.QAAdvanceOneStep();Check(!g.TryShoot(Vector2.up)&&!g.SelectCollectionTool(1),"volley blocks overlapping shots and tools");Steps(25);Check(g.SpawnCount==5&&first.transform.position.y==y,"five balls and fixed bricks during volley");Check(g.Shots==int.MaxValue,"shots never deplete");
  g.StartLevel(50);yield return null;Empty();var danger=Add(0,11);Settle(true);Check(!g.IsPlaying&&!g.CanContinue,"brick crossing danger line loses without shot-based continue");
  g.StartLevel(50);yield return null;Empty();Add(0,11);g.Collected[0]=5;Break(Add(8,1,1));Settle(true);Check(!g.IsPlaying&&Rules.Won&&g.CollectionRound==1,"last collection wins before dangerous descent");
  g.StartLevel(52);yield return null;Empty();g.Collected[0]=5;g.Collected[1]=4;Call("PlanCollectionRow");Check(g.NextCollectionRow.Contains(1)&&!g.NextCollectionRow.Contains(2),"missing final fish guaranteed and completed berry excluded");
  g.StartLevel(51);yield return null;Empty();for(int row=0;row<3;row++)Break(Add(4,row,2));Settle();Check(g.Collected[1]==3,"three stacked released items collect in one open channel");
  for(int n=0;n<3;n++){
   g.StartLevel(50+n);yield return new WaitForSecondsRealtime(.4f);int turns=0;float seconds=0;
   while(g.IsPlaying&&turns<30){var target=g.CollectionBricks.Where(b=>b.Alive).OrderBy(b=>b.transform.position.y).ThenBy(b=>Mathf.Abs(b.transform.position.x)).First();Check(g.TryShoot((Vector2)target.transform.position-g.LauncherPosition),"natural physics accepts round "+n+"/"+turns);turns++;int step=0;do{g.QAAdvanceOneStep();step++;if(g.RecallAvailable)g.RecallBalls();}while(g.IsPlaying&&(!g.CollectionReady||step<2)&&step<2400);seconds+=step/60f;yield return new WaitForSecondsRealtime(.35f);if(step>=2400){Check(false,"round terminates");break;}}
   File.AppendAllText(Path.Combine(dir,"natural-play.txt"),$"C{n+1} turns={turns} simulationSeconds={seconds:F1} won={Rules.Won} collected={g.Collected[0]}/{g.CollectionGoal[0]},{g.Collected[1]}/{g.CollectionGoal[1]}\n");Check(!g.IsPlaying,"natural run reaches outcome C"+(n+1));
  }
  for(int i=0;i<50;i++){g.StartLevel(i);Check(!g.IsCollection&&g.CollectionBricks.Count==0&&g.CollectionTreats.Count==0,"legacy reset "+i);}g.StopLevel();Destroy(go);Physics2D.simulationMode=mode;
  File.AppendAllText(Path.Combine(dir,"checks.txt"),$"RESULT checks={checks} failures={failures} errors={errors}\n");Application.Quit(failures+errors==0?0:1);
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
int width=540,height=1170;
```
```csharp
const BindingFlags Hidden=BindingFlags.NonPublic|BindingFlags.Instance;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
