# ChainPresentationQA · ChainPresentationQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChainPresentationQA.cs`
文件 SHA-256：`2e731c3dca7b2662ac316bd54b61520c2742843e2f0216c10a8cfe6cfb3c5408`
[原始源码快照](../source/Assets/Scripts/ChainPresentationQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Level · L19

方法 SHA-256：`d00a772daba9bf5eab01dd5751e4b7dd4490548237d7f9a32ec721e1bb7bdcc8`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainPresentationQA.Level()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainPresentationQA.cs:23` · `ChainPresentationQA.Start` → `Level()`
- `Assets/Scripts/ChainPresentationQA.cs:30` · `ChainPresentationQA.Start` → `Level()`
- `Assets/Scripts/ChainPresentationQA.cs:31` · `ChainPresentationQA.Start` → `Level()`
- `Assets/Scripts/ChainPresentationQA.cs:38` · `ChainPresentationQA.Start` → `Level()`

```csharp
IEnumerator Level(){app.QAStartLevel(53);yield return app.QAWaitForLevel();g=app.Session;yield return new WaitForSecondsRealtime(.45f);}
```

### Start · L20

方法 SHA-256：`22a981759a3d3eb15538b93a3eba4c574e37546492c24d52e0a63175efc9152d`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainPresentationQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];if(Array.IndexOf(args,"--qa-height")>=0)height=int.Parse(args[Array.IndexOf(args,"--qa-height")+1]);Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();app.QASetViewport(new Vector2Int(width,height));app.Session.QAIgnoreFocusLoss=true;fake=new GameObject("Direct rating event fixture").AddComponent<BallBody>();yield return Level();
  var numeric=g.CollectionBricks.Where(b=>b.Alive&&b.Definition.kind!=BrickKind.Stone).ToArray();Check(numeric.All(Centered),"all numeric brick HP labels are exactly centered");
  Check(numeric.Where(b=>b.CurrentHealth==1).All(Centered)&&numeric.Any(b=>b.CurrentHealth==1&&b.Definition.kind==BrickKind.Normal),"one HP ordinary bricks also show a centered digit");
  Check(numeric.Where(b=>b.Definition.juiceGroup==1).All(Centered),"fish box HP is centered");Check(numeric.Where(b=>b.Definition.juiceGroup==2).All(Centered),"toy box HP is centered");Check(numeric.Where(b=>b.Definition.kind==BrickKind.RearWeak).All(Centered),"rear guard HP is centered");Check(numeric.Where(b=>b.Definition.kind==BrickKind.Armor).All(Centered),"armor HP is centered");Check(numeric.Where(b=>ChainPowerRules.IsNest(b.Definition)).All(Centered),"large block HP is centered");
  Check(g.CollectionBricks.Where(b=>b.Definition.kind==BrickKind.Stone).All(b=>b.Face.sprite==AbilityArt.Brick(BrickKind.Stone)),"stone keeps its original centered infinity artwork");
  Check(Header.text==g.Shots.ToString()&&g.Shots==20,"top resource starts at twenty remaining launch steps");Check(app.GetComponentsInChildren<TMP_Text>().Any(t=>t.text=="STEPS")&&!app.GetComponentsInChildren<TMP_Text>().Any(t=>t.text=="ROUND"),"chain UI labels the resource STEPS rather than ROUND");
  typeof(GameSession).GetField("<CollectionRound>k__BackingField",Hidden).SetValue(g,99);typeof(AppUI).GetMethod("RefreshHUD",Hidden).Invoke(app,null);Check(Header.text=="20","internal settlement count cannot replace the remaining-step display");yield return Capture("00-centered-hp-and-steps");
  yield return Level();var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;Check(g.TryShoot(Vector2.up)&&g.Shots==20,"aim acceptance does not debit before launch");Check(!g.TryShoot(Vector2.up)&&g.Shots==20,"a queued duplicate input costs no extra step");Steps(1);Check(g.Shots==19&&Header.text=="19","one accepted five-ball launch consumes one step and updates the header");int n=0;while(g.SpawnCount<5&&n++<150)Steps(1);Check(g.SpawnCount==5&&g.Shots==19,"all five balls share the same one-step cost");g.SetPaused(true);Check(!g.TryShoot(Vector2.up)&&g.Shots==19,"paused launch input costs no step");g.SetPaused(false);Check(!g.TryShoot(Vector2.down)&&g.Shots==19,"invalid aim costs no step");Call("ScheduleChainGravity");Steps(100);Check(g.Shots==19,"gravity refill and ongoing chain effects cost no extra step");Physics2D.simulationMode=mode;
  yield return Level();var view=FindFirstObjectByType<ChainRatingView>();Check(view!=null,"chain battle creates the actual rating presenter");var nest=g.CollectionBricks.First(b=>ChainPowerRules.IsNest(b.Definition));
  for(int i=0;i<3;i++)Hit(nest,Vector2.up);Check(g.ChainRatingHits==3&&g.ChainRatingTier==-1&&nest.CurrentHealth==21,"three nonlethal direct hits accumulate below Nice");Hit(nest,Vector2.up);Check(g.ChainRatingHits==4&&g.ChainRatingTier==0&&g.BreakCount==0&&nest.CurrentHealth==20,"four effective hits award Nice without requiring any brick to break");Check(g.NextChainRowTools.Count(k=>k>0)==1&&g.NextChainRowTools.Contains(1),"Nice queues exactly one laser reward");yield return new WaitForSecondsRealtime(.25f);Check(view.IsVisible&&view.VisibleTier==0&&view.VisibleText==InGameFeedbackView.CheerText(0),"the real Nice artwork is visible during battle");g.SetPaused(true);float age=view.Age;yield return Capture("01-nice-effective-hits");yield return new WaitForSecondsRealtime(.2f);Check(view.Age==age&&view.IsVisible,"pause freezes the evaluation animation");g.SetPaused(false);
  for(int i=0;i<4;i++)Hit(nest,Vector2.up);yield return new WaitForSecondsRealtime(.25f);Check(g.ChainRatingHits==8&&g.ChainRatingTier==1&&view.IsVisible&&view.VisibleTier==1,"eight effective hits visibly upgrade to Great");Check(g.NextChainRowTools.Count(k=>k>0)==1&&g.NextChainRowTools.Contains(2),"Great replaces the pending laser with one bomb");g.SetPaused(true);yield return Capture("02-great-effective-hits");g.SetPaused(false);
  for(int i=0;i<4;i++)Hit(nest,Vector2.up);yield return new WaitForSecondsRealtime(.25f);Check(g.ChainRatingHits==12&&g.ChainRatingTier==2&&view.IsVisible&&view.VisibleText==InGameFeedbackView.CheerText(2),"twelve effective hits display the actual Amazing artwork");Check(g.NextChainRowTools.Where(k=>k>0).OrderBy(k=>k).SequenceEqual(new[]{1,2}),"Amazing queues laser and bomb together");g.SetPaused(true);yield return Capture("03-amazing-effective-hits");g.SetPaused(false);
  for(int i=0;i<6;i++)Hit(nest,Vector2.up);yield return new WaitForSecondsRealtime(.25f);Check(g.ChainRatingHits==18&&g.ChainRatingTier==3&&view.IsVisible&&view.VisibleText==InGameFeedbackView.CheerText(3),"eighteen effective hits display Incredible");Check(g.NextChainRowTools.Where(k=>k>0).OrderBy(k=>k).SequenceEqual(new[]{1,2,3})&&g.ChainRatingSerial==4,"highest tier queues all three types without stacking lower packets");g.SetPaused(true);yield return Capture("04-incredible-effective-hits");g.SetPaused(false);
  Hit(nest,Vector2.up);Check(g.ChainRatingHits==19&&g.ChainRatingSerial==4,"further hits do not repeat the highest-tier award");int count=g.ChainRatingHits;var stone=g.CollectionBricks.First(b=>b.Definition.kind==BrickKind.Stone);Hit(stone,Vector2.up);var rear=g.CollectionBricks.First(b=>b.Definition.kind==BrickKind.RearWeak);Hit(rear,Vector2.up);Hit(rear,Vector2.left);Check(g.ChainRatingHits==count,"stone and blocked front or side hits do not count");var armor=g.CollectionBricks.First(b=>b.Definition.kind==BrickKind.Armor);Hit(armor,Vector2.up);Check(g.ChainRatingHits==count+1&&!armor.ShellIntact&&armor.CurrentHealth==6,"a valid shell-breaking impact counts exactly once");count=g.ChainRatingHits;var laser=g.ChainTools.First(t=>t.Kind==1);laser.ReadyAt=0;g.ActivateChainTool(laser,Vector2.up);Check(g.ChainRatingHits==count,"power damage and its collateral cannot farm evaluation hits");Check(g.Shots==20,"ratings, armor opening and tool activation do not consume launch steps");
  // Real five-ball collisions must also produce evaluation, not just the controlled event fixture.
  yield return Level();Physics2D.simulationMode=SimulationMode2D.Script;Check(g.TryShoot(Vector2.up),"normal full-board shot accepted for live evaluation");n=0;do{Steps(1);n++;if(g.RecallAvailable)g.RecallBalls();}while(g.IsPlaying&&(!g.CollectionReady||n<2)&&n<1500);Check(n<1500&&g.ChainRatingHits>=4&&g.ChainRatingTier>=0,"a real five-ball flight reaches Nice through effective collision hits");Check(g.Shots==19&&g.ChainGridValid(),"live rating keeps a full board and exactly one-step cost");Physics2D.simulationMode=mode;yield return new WaitForSecondsRealtime(.25f);view=FindFirstObjectByType<ChainRatingView>();Check(view.IsVisible&&view.VisibleTier==g.ChainRatingTier,"a real collision-driven evaluation is actually on screen");g.SetPaused(true);yield return Capture("05-natural-shot-rating");g.SetPaused(false);
  int steps=g.Shots;Check(g.TryShoot(Vector2.up),"next launch is accepted after reward refill");Physics2D.simulationMode=SimulationMode2D.Script;Steps(1);Check(g.Shots==steps-1&&g.ChainRatingHits==0&&g.ChainRatingTier==-1,"next accepted launch spends one step and resets the rating counter");Physics2D.simulationMode=mode;
  app.QANavigate("home");yield return null;Check(FindObjectsByType<ChainRatingView>(FindObjectsSortMode.None).Length==0,"leaving battle removes the evaluation presenter");Destroy(fake.gameObject);
  File.AppendAllText(Path.Combine(dir,"checks.txt"),$"RESULT checks={checks} failures={failures} errors={errors}\n");Application.Quit(failures+errors==0?0:1);
 }
```

### Capture · L43

方法 SHA-256：`3445183ff0747e013d6735d535c58c7bff38410171af23b649358847b80689e7`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainPresentationQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainPresentationQA.cs:29` · `ChainPresentationQA.Start` → `Capture("00-centered-hp-and-steps")`
- `Assets/Scripts/ChainPresentationQA.cs:32` · `ChainPresentationQA.Start` → `Capture("01-nice-effective-hits")`
- `Assets/Scripts/ChainPresentationQA.cs:33` · `ChainPresentationQA.Start` → `Capture("02-great-effective-hits")`
- `Assets/Scripts/ChainPresentationQA.cs:34` · `ChainPresentationQA.Start` → `Capture("03-amazing-effective-hits")`
- `Assets/Scripts/ChainPresentationQA.cs:35` · `ChainPresentationQA.Start` → `Capture("04-incredible-effective-hits")`
- `Assets/Scripts/ChainPresentationQA.cs:38` · `ChainPresentationQA.Start` → `Capture("05-natural-shot-rating")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var cam=Camera.main;var target=new RenderTexture(width,height,24);target.Create();var old=cam.targetTexture;cam.targetTexture=target;cam.rect=new Rect(0,0,1,1);cam.aspect=(float)width/height;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;Canvas.ForceUpdateCanvases();var design=(RectTransform)typeof(AppUI).GetField("designRoot",Hidden).GetValue(app);var safe=(RectTransform)design.parent;design.localScale=Vector3.one*Mathf.Min(safe.rect.width/1080f,safe.rect.height/2340f);Canvas.ForceUpdateCanvases();cam.Render();var previous=RenderTexture.active;RenderTexture.active=target;var texture=new Texture2D(width,height,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,width,height),0,0);texture.Apply();File.WriteAllBytes(Path.Combine(dir,name+".png"),texture.EncodeToPNG());RenderTexture.active=previous;cam.targetTexture=old;Destroy(texture);target.Release();Destroy(target);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
const BindingFlags Hidden=BindingFlags.Instance|BindingFlags.NonPublic;
```
```csharp
AppUI app;
```
```csharp
GameSession g;
```
```csharp
BallBody fake;
```
```csharp
string dir;
```
```csharp
int checks,failures,errors,width=540,height=1170;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
