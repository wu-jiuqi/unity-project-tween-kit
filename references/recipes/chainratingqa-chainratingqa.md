# ChainRatingQA · ChainRatingQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChainRatingQA.cs`
文件 SHA-256：`f5c1851d0c61115f958d0a1302834b328496cfd5714be90f263a3229c25ace60`
[原始源码快照](../source/Assets/Scripts/ChainRatingQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L23

方法 SHA-256：`cdf520e1e172c9d949caa9666d05761727a5cfe6a94e0a35613e1e4783676f5a`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainRatingQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];if(Array.IndexOf(args,"--qa-height")>=0)height=int.Parse(args[Array.IndexOf(args,"--qa-height")+1]);Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();app.QASetViewport(new Vector2Int(width,height));app.Session.QAIgnoreFocusLoss=true;fake=new GameObject("Direct credit fixture").AddComponent<BallBody>();
  app.QAStartLevel(53);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.5f);g=app.Session;
  var stone=g.CollectionBricks.First(b=>b.Definition.kind==BrickKind.Stone);var armor=g.CollectionBricks.First(b=>b.Definition.kind==BrickKind.Armor);
  Check(g.CollectionBricks.Count(b=>b.Definition.kind==BrickKind.Stone)==2,"two authored stones");
  Check(stone.Face.sprite==AbilityArt.Brick(BrickKind.Stone)&&armor.Face.sprite==AbilityArt.Brick(BrickKind.Armor),"original stone and armor sprites are the actual brick faces");
  Check(armor.CurrentHealth==6&&g.CollectionBricks.First(b=>b.Definition.kind==BrickKind.RearWeak).CurrentHealth==8,"guard health raised to 6 and 8");yield return Capture("00-legacy-art-opening");
  Empty();Credit(4);Check(g.ChainRatingTier==0&&Queued==1&&g.NextChainRowTools.Contains(1)&&g.ChainTools.Count==0,"Nice queues a needle without creating a tool in flight");yield return new WaitForSecondsRealtime(.12f);yield return Capture("01-nice-next-row");
  Credit(8);Check(g.ChainRatingTier==2&&Queued==2,"Amazing upgrades to a two-tool packet");yield return new WaitForSecondsRealtime(.12f);yield return Capture("02-amazing-next-row");
  Set("collectionRoundPending",true);yield return new WaitForSecondsRealtime(.8f);Check(g.ChainGeneratedCount==2&&g.ChainTools.Count==2&&g.ChainTools.All(t=>Mathf.Abs(t.Position.y-CollectionLevels.Top)<.01f),"reward packet enters with the next descending row");yield return Capture("03-tools-entered");
  app.QANavigate("home");g=new GameObject("Rating rule fixtures").AddComponent<GameSession>();g.QAIgnoreFocusLoss=true;var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  Reset();Credit(3);Check(g.ChainRatingTier==-1&&Queued==0,"three kills do not rate");Credit(1);Check(g.ChainRatingTier==0&&g.ChainRatingBreaks==4&&Queued==1,"four kills Nice");Credit(3);Check(g.ChainRatingTier==0&&Queued==1,"Nice does not repeat below eight");Credit(1);Check(g.ChainRatingTier==1&&Queued==1&&g.NextChainRowTools.Contains(2)&&!g.NextChainRowTools.Contains(1),"eight kills Great replaces needle with drum");Credit(4);Check(g.ChainRatingTier==2&&Queued==2&&g.NextChainRowTools.Count(x=>x==1)==1&&g.NextChainRowTools.Count(x=>x==2)==1,"twelve kills Amazing gives needle plus drum");Credit(6);Check(g.ChainRatingTier==3&&Queued==3&&g.NextChainRowTools.Count(x=>x==1)==2,"eighteen kills Incredible gives two needles and a drum");Credit(10);Check(Queued==3&&g.ChainRatingSerial==4&&g.ChainTools.Count==0,"highest packet only, no stacked tiers or mid-flight tools");
  int counted=g.ChainRatingBreaks;g.SetPaused(true);Credit(4);Check(g.ChainRatingBreaks==counted&&Queued==3,"pause prevents credit and queue mutations");g.SetPaused(false);
  Set("collectionRoundPending",true);Steps(80);Check(g.ChainGeneratedCount==3&&g.ChainTools.Count==3&&Queued==0,"packet spawns once and clears next-row queue");Check(g.ChainTools.All(t=>!g.CollectionBricks.Any(b=>b.Alive&&Vector2.Distance(b.transform.position,t.Position)<.2f)),"reward cells do not overlap incoming bricks");Set("collectionRoundPending",true);Steps(80);Check(g.ChainGeneratedCount==3,"old rating is not granted on subsequent descent");Call("BeginChainVolley");Check(g.ChainRatingBreaks==0&&g.ChainRatingTier==-1&&Queued==0,"accepted new volley resets rating");
  Reset();for(int i=0;i<30;i++)g.EmitFeedback(Vector2.zero,"break",Color.white);Check(g.ChainRatingBreaks==0&&Queued==0,"cosmetic break feedback cannot create rewards");
  var normal=Add(Vector2.zero,BrickKind.Normal,9);var needle=g.SpawnChainTool(1,new Vector2(0,-2));Arm();g.ActivateChainTool(needle,Vector2.up);Check(!normal.Alive&&g.ChainRatingBreaks==0&&Queued==0,"needle kills cannot earn rating rewards");
  Reset();armor=Add(Vector2.zero,BrickKind.Armor,1);Call("HitChainBrick",armor,fake,Vector2.down*.41f);Check(!armor.ShellIntact&&g.ChainRatingBreaks==0,"shell opening is not a kill");Check(armor.Face.sprite==JellyBrickArt.Rest,"old armor art changes to jelly after shell opens");Call("HitChainBrick",armor,fake,Vector2.down*.41f);Check(g.ChainRatingBreaks==1,"armor body destruction counts once");
  Reset();var goal=Add(new Vector2(0,2),BrickKind.Normal,1,1);Call("HitChainBrick",goal,fake,new Vector2(0,1.59f));Check(g.ChainRatingBreaks==1,"direct container destruction counts toward rating");Steps(45);Check(g.ChainRatingBreaks==1&&g.Collected[0]==1,"loot arrival feedback does not count again");
  Reset();Set("<CollectionRound>k__BackingField",4);Call("PlanChainRow");var baseline=(int[])g.NextCollectionRow.Clone();var roles=(BrickKind[])g.NextChainRowKinds.Clone();Check(roles.Contains(BrickKind.Stone)&&roles.Contains(BrickKind.Armor),"protection fixture contains both stone and armor");Credit(18);Check(Enumerable.Range(0,9).Where(c=>baseline[c]>0||roles[c]!=BrickKind.Normal).All(c=>g.NextCollectionRow[c]==baseline[c]&&g.NextChainRowKinds[c]==roles[c]&&g.NextChainRowTools[c]==0),"rewards never replace goals or obstacles");
  Reset();stone=Add(Vector2.zero,BrickKind.Stone);int targets=g.RemainingTargets;for(int i=0;i<20;i++)g.ApplyChainImpact(stone,999,Vector2.down,Vector2.up*.41f,true);Check(stone.Alive&&stone.CurrentHealth==1&&g.BreakCount==0&&g.ChainRatingBreaks==0,"stone is unbreakable and never earns credit");Check(targets==0&&Rules.Health(stone.Id)==0,"stone is not a registered task target");
  var behind=Add(new Vector2(0,1.2f),BrickKind.Normal,6,1);needle=g.SpawnChainTool(1,new Vector2(0,-2));var drum=g.SpawnChainTool(2,new Vector2(0,2));Arm();g.ActivateChainTool(needle,Vector2.up);Check(stone.Alive&&behind.CurrentHealth==6&&g.ChainTools.Contains(drum),"stone blocks the needle and protects targets and tools behind it");
  Reset();stone=Add(new Vector2(0,-3.7f),BrickKind.Stone);Set("collectionRoundPending",true);Steps(80);Check(!stone.Alive&&g.IsPlaying&&g.ChainStoneExits==1&&g.CollectionRound==2&&g.BreakCount==0,"stone exits at danger without unavoidable failure or reward");
  Reset();Add(new Vector2(0,-3.7f),BrickKind.Stone);Add(new Vector2(2,-3.7f),BrickKind.Normal,6);Set("collectionRoundPending",true);Steps(80);Check(!g.IsPlaying&&!Rules.Won,"ordinary bricks still lose at danger even alongside stone");
  g.StartLevel(0);Check(g.ChainRatingBreaks==0&&g.ChainRatingTier==-1&&g.NextChainRowTools.All(t=>t==0),"rating state clears on legacy stage");Check(InGameFeedbackView.CheerText(0)!=null&&InGameFeedbackView.CheerText(2)!=null,"original Nice and Amazing artwork loads");g.StopLevel();Destroy(g.gameObject);Destroy(fake.gameObject);Physics2D.simulationMode=mode;
  File.AppendAllText(Path.Combine(dir,"checks.txt"),$"RESULT checks={checks} failures={failures} errors={errors}\n");Application.Quit(failures+errors==0?0:1);
 }
```

### Capture · L51

方法 SHA-256：`95b1a8c1ef930067c6708170d0e8dad02346794ba542de51f426b5cd46bc2b08`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainRatingQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainRatingQA.cs:31` · `ChainRatingQA.Start` → `Capture("00-legacy-art-opening")`
- `Assets/Scripts/ChainRatingQA.cs:32` · `ChainRatingQA.Start` → `Capture("01-nice-next-row")`
- `Assets/Scripts/ChainRatingQA.cs:33` · `ChainRatingQA.Start` → `Capture("02-amazing-next-row")`
- `Assets/Scripts/ChainRatingQA.cs:34` · `ChainRatingQA.Start` → `Capture("03-tools-entered")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var cam=Camera.main;var target=new RenderTexture(width,height,24);target.Create();var old=cam.targetTexture;cam.targetTexture=target;cam.rect=new Rect(0,0,1,1);cam.aspect=(float)width/height;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;Canvas.ForceUpdateCanvases();var design=(RectTransform)typeof(AppUI).GetField("designRoot",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).GetValue(app);var safe=(RectTransform)design.parent;design.localScale=Vector3.one*Mathf.Min(safe.rect.width/1080f,safe.rect.height/2340f);Canvas.ForceUpdateCanvases();cam.Render();var prev=RenderTexture.active;RenderTexture.active=target;var texture=new Texture2D(width,height,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,width,height),0,0);texture.Apply();File.WriteAllBytes(Path.Combine(dir,name+".png"),texture.EncodeToPNG());RenderTexture.active=prev;cam.targetTexture=old;Destroy(texture);target.Release();Destroy(target);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(){g.StartLevel(53);Empty();}
```

## 源字段与依赖

```csharp
const BindingFlags Hidden=BindingFlags.Instance|BindingFlags.NonPublic;
```
```csharp
GameSession g;
```
```csharp
AppUI app;
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
