# JellyV40QA · JellyV40QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/JellyV40QA.cs`
文件 SHA-256：`cd110b26e06ee70fb56867e25f7ac45d79f23db6225e86135566a46afc201251`
[原始源码快照](../source/Assets/Scripts/JellyV40QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L23

方法 SHA-256：`d9c6258650860cdb04d69fcdf04cfce82d1a9513a03346cb198f0ab2a70e09e2`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyV40QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"v40 production audit: break ratings, finite color collection, white HP curriculum and aligned header. Isolated profile.\n");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");yield return null;g=new GameObject("v40 isolated production session").AddComponent<GameSession>();g.QAIgnoreFocusLoss=true;g.enabled=false;var oldMode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  int sum=0;for(int level=0;level<26;level++){g.StartLevel(level);var stage=AlphabetCampaign.Stage(level);int expected=stage.glyph.Sum(row=>row.Count(c=>c=='1'));sum+=expected;Check(g.JellyGoal[1]==expected&&stage.blueJelly==expected&&g.CollectionBricks.Count(b=>g.IsJellyTarget(b)&&b.Definition.colorIndex==1)==expected,"blue count matches glyph "+stage.letter);int white=level<3?1:level<5?3:4;Check(g.CollectionBricks.Where(b=>b.Definition.kind==BrickKind.Normal&&b.Definition.juiceGroup==0&&!g.IsJellyTarget(b)&&!ChainPowerRules.IsNest(b.Definition)).All(b=>b.CurrentHealth<=white),"white starting durability curriculum "+stage.letter);if(level%5==0)yield return null;}
  Check(sum==400,"26 authored letters contain exactly 400 blue target bricks");Check(AlphabetCampaign.RefillHealth(0,12,4)==1&&AlphabetCampaign.RefillHealth(2,12,4)==1&&AlphabetCampaign.RefillHealth(3,12,4)==3&&AlphabetCampaign.RefillHealth(4,12,4)==3&&AlphabetCampaign.RefillHealth(5,1,4)==4&&AlphabetCampaign.RefillHealth(8,12,4)>=6,"white refill HP follows unlocks and later pressure");
  g.StartLevel(18);Empty();Check(g.TryShoot(Vector2.up),"rating fixture launches a real group");Steps(1);var first=Balls().Single();int firstId=first.ChainBurstId;var body=Add(0,0,4);Hit(body,first);var armor=Add(1,0,8,BrickKind.Armor);Hit(armor,first);var stone=Add(2,0,1,BrickKind.Stone);Hit(stone,first);Check(Groups[firstId].Breaks==0&&g.ChainRatingTier==-1&&g.ChainRatingBreaks==0,"nonlethal damage, armor removal and stone contacts earn no break rating");
  for(int n=1;n<=18;n++){var target=Add((n-1)%9,1+(n-1)/9);Hit(target,first);int tier=n<4?-1:n<8?0:n<12?1:n<18?2:3;Check(Groups[firstId].Breaks==n&&Groups[firstId].Tier==tier,"direct break threshold "+n);}
  int serial=g.ChainRatingSerial;for(int r=3;r<=5;r++)for(int c=3;c<=5;c++)if(c!=4||r!=4)Add(c,r);var bomb=g.SpawnChainTool(2,ChainGridRules.Center(4,4));bomb.ReadyAt=0;g.ActivateChainTool(bomb,Vector2.up);Check(Groups[firstId].Breaks==18&&g.ChainRatingSerial==serial,"bomb collateral cannot farm another rating");
  Seconds(.65f);for(int c=0;c<9;c++)Add(c,7,8);foreach(var ball in Balls().Where(b=>b.ChainBurstId==firstId).ToArray())g.LoseBall(ball,ball.Body.position);File.AppendAllText(Path.Combine(dir,"checks.txt"),"RATING_RETURN groups="+Groups.Count+" pending="+g.ChainPendingReplacements+" tools="+string.Join(",",g.ChainTools.Select(t=>t.Kind))+"\\n");var waiting=(Queue<int>)typeof(GameSession).GetField("chainReplacementRewards",Hidden).GetValue(g);Check(Groups.Count==0&&g.ChainTools.Select(t=>t.Kind).Concat(waiting).OrderBy(k=>k).SequenceEqual(new[]{1,2,3}),"group returns issue exactly the highest packet once, placed or deferred for eligible bricks");
  // Continuous refill permits real collisions during the 0.65-second overlap. Compare direct-hit deltas against that live baseline.
  g.StartLevel(18);Empty();g.TryShoot(Vector2.up);Steps(1);first=Balls().Single();firstId=first.ChainBurstId;for(int n=0;n<4;n++)Hit(Add(n,0),first);Seconds(.65f);g.TryShoot(Vector2.up);Steps(1);var second=Balls().First(b=>b.ChainBurstId!=firstId);int previousFirstBreaks=Groups[firstId].Breaks,previousFirstTier=Groups[firstId].Tier,previousSecondBreaks=Groups[second.ChainBurstId].Breaks;for(int n=0;n<8;n++)Hit(Add(n,1),second);Check(Groups[firstId].Breaks==previousFirstBreaks&&Groups[firstId].Tier==previousFirstTier&&Groups[second.ChainBurstId].Breaks==previousSecondBreaks+8&&Groups[second.ChainBurstId].Tier==ChainRatingRules.Tier(previousSecondBreaks+8),"overlapping groups keep independent direct-break counters during live refill");
  g.StartLevel(1);g.Collected[0]=g.CollectionGoal[0];Check(!g.CollectionWon&&g.JellyCollected[1]==0,"finishing fish does not skip the blue letter task");var blue=g.CollectionBricks.First(g.IsJellyTarget);Damage(blue,1);Check(g.JellyCollected[1]==0&&g.IsJellyTarget(blue),"a nonlethal blue hit never collects jelly");Damage(blue);Check(g.JellyCollected[1]==0&&!g.IsJellyTarget(blue)&&!g.CollectionWon,"a broken jelly flies before its counter settles");g.SetPaused(true);Seconds(1);Check(g.JellyCollected[1]==0,"pause freezes blue collection flight");g.SetPaused(false);Seconds(.7f);Check(g.JellyCollected[1]==1&&!g.CollectionWon,"one arrived blue jelly advances exactly one unit");Damage(blue);Seconds(.2f);Check(g.JellyCollected[1]==1,"repeat damage cannot collect the same blue identity twice");
  foreach(var target in g.CollectionBricks.Where(g.IsJellyTarget).ToArray())Damage(target);Check(!g.CollectionWon,"last blue break still waits for its collection flight");Seconds(.8f);Check(g.CollectionWon&&g.JellyCollected[1]==20,"B wins only after all 20 blue jellies and fish settle");
  g.StartLevel(0);blue=g.CollectionBricks.First(g.IsJellyTarget);var ids=new HashSet<int>(g.CollectionBricks.Select(b=>b.Id));Damage(blue);Seconds(4);Check(g.JellyGoal[1]==18&&g.JellyCollected[1]==1&&g.CollectionBricks.Count(g.IsJellyTarget)==17,"collecting blue decreases finite remaining targets without growing its goal");Check(g.CollectionBricks.Where(b=>b.Alive&&!ids.Contains(b.Id)).All(b=>b.Definition.colorIndex==0&&!g.IsJellyTarget(b))&&g.CollectionBricks.Any(b=>b.Alive&&!ids.Contains(b.Id)),"refills are white and do not recreate blue collection targets");
  g.StartLevel(3);Seconds(.2f);var laser=g.ChainTools.First(t=>t.Kind==1);g.ActivateChainTool(laser,Vector2.up);Seconds(1);Check(g.JellyCollected[1]>0&&g.ChainRatingBreaks==0,"laser-cleared blue jellies collect without adding direct-ball break rewards");
  g.StartLevel(18);var originalBlue=g.CollectionBricks.Where(g.IsJellyTarget).ToArray();Call("QueueChainLiveReward",(object)Enumerable.Repeat(1,100).ToArray());Check(originalBlue.All(b=>b.Alive&&g.IsJellyTarget(b))&&g.JellyCollected[1]==0&&g.JellyGoal[1]==15,"random power replacement cannot erase or auto-collect blue objectives");Check(g.ChainPendingReplacements>0&&g.ChainGridValid(),"an exhausted ordinary replacement pool defers safely");
  g.StopLevel();Destroy(g.gameObject);Physics2D.simulationMode=oldMode;yield return null;for(int id=1;id<=12;id++)app.Profile.MarkAlphabetIntro(id);
  foreach(var viewport in new[]{new Vector2Int(540,960),new Vector2Int(540,1170),new Vector2Int(1080,2340)})foreach(int level in new[]{1,7,25}){
   app.QASetViewport(viewport);app.QAStartLevel(level);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.7f);var hud=FindFirstObjectByType<ChainCollectionHUD>();Check(hud.Cards.Count==(level==1?2:3)&&hud.Cards.All(c=>c.Rect.parent==hud.GoalsRect),"all goals share one tray "+viewport+" / "+level);Check(hud.StepsRect.anchoredPosition.x-hud.StepsRect.rect.width*.5f>hud.GoalsRect.anchoredPosition.x+hud.GoalsRect.rect.width*.5f+20,"steps have a separate visible gutter "+viewport+" / "+level);Check(hud.Cards.All(c=>{var face=c.ContentRect.rect;var a=c.Icon.anchoredPosition;var b=c.NumberRect.anchoredPosition;var iconHalf=Vector2.Scale(c.Icon.rect.size,Vector2.one*1.1f)*.5f;var textHalf=c.NumberRect.rect.size*1.04f*.5f;return face.Contains(a-iconHalf)&&face.Contains(a+iconHalf)&&face.Contains(b-textHalf)&&face.Contains(b+textHalf);}),"icons and numbers stay inside each capsule face at maximum pulse "+viewport+" / "+level);yield return Capture("header-"+viewport.x+"x"+viewport.y+"-"+AlphabetCampaign.Stage(level).letter,viewport.x,viewport.y);
  }
  // Exercise the relocated art using a real five-ball volley and production break credit.
  app.QASetViewport(new Vector2Int(540,1170));app.QAStartLevel(7);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.7f);g=app.Session;g.QAIgnoreFocusLoss=true;g.enabled=false;Physics2D.simulationMode=SimulationMode2D.Script;
  Check(g.TryShoot(Vector2.up),"raised launcher accepts a real shot");Steps(1);var live=Balls().First();var origin=g.SpawnPosition(Vector2.up);Check(Vector2.Distance(live.Body.position,origin)<.5f,"actual ball spawns at raised visible muzzle");Check(g.Shots==19,"one five-ball volley charges exactly one step");
  var ordinary=g.CollectionBricks.Where(b=>b.Alive&&!g.IsJellyTarget(b)&&b.Definition.kind==BrickKind.Normal&&!ChainPowerRules.IsNest(b.Definition)).Reverse().Take(18).ToArray();
  for(int n=0;n<ordinary.Length;n++){var b=ordinary[n];for(int hit=0;hit<12&&b.Alive;hit++)Hit(b,live);if(n==3||n==7||n==11||n==17){yield return new WaitForSecondsRealtime(.25f);var view=FindFirstObjectByType<ChainRatingView>();var rating=(RectTransform)view.transform.Find("Rating reward");Check(view.IsVisible&&view.VisibleTier==(n==3?0:n==7?1:n==11?2:3),"production rating visible for break "+(n+1));Check(rating.anchoredPosition.y< -200&&rating.anchoredPosition.y>g.ReturnLineY*100+120,"rating sits between board and launch line");yield return Capture("rating-"+(n+1),540,1170);g.SetPaused(true);float pausedAge=view.Age;yield return new WaitForSecondsRealtime(.2f);Check(Mathf.Abs(view.Age-pausedAge)<.001f,"pause freezes relocated rating");g.SetPaused(false);}}
  Seconds(14);Check(g.ActiveBalls==0&&g.PendingBurstBalls==0,"raised return line recycles all five real balls");g.enabled=true;Physics2D.simulationMode=oldMode;
  app.QAStartLevel(7);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.7f);
  app.Session.enabled=false;for(int food=0;food<2;food++)app.Session.Collected[food]=app.Session.CollectionGoal[food];for(int color=0;color<8;color++)app.Session.JellyCollected[color]=app.Session.JellyGoal[color];foreach(var card in FindFirstObjectByType<ChainCollectionHUD>().Cards)card.Refresh();yield return new WaitForSecondsRealtime(.18f);yield return Capture("header-complete-pulse",1080,2340);yield return new WaitForSecondsRealtime(.5f);yield return Capture("header-complete",1080,2340);app.Session.enabled=true;app.QANavigate("home");Check(errors==0,"runtime Console has zero errors or exceptions");File.AppendAllText(Path.Combine(dir,"checks.txt"),$"RESULT checks={checks} failures={failures} errors={errors}\n");Application.Quit(failures+errors==0?0:1);
 }
```

### Capture · L51

方法 SHA-256：`d83e882780f67614c66e22f95f3f69f1341db5e501e29c6f3c3e6b7894220456`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyV40QA.Capture(string, int, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/JellyV40QA.cs:46` · `JellyV40QA.Start` → `Capture("rating-"+(n+1),540,1170)`
- `Assets/Scripts/JellyV40QA.cs:49` · `JellyV40QA.Start` → `Capture("header-complete-pulse",1080,2340)`
- `Assets/Scripts/JellyV40QA.cs:49` · `JellyV40QA.Start` → `Capture("header-complete",1080,2340)`

```csharp
IEnumerator Capture(string name,int width,int height){yield return new WaitForEndOfFrame();var cam=Camera.main;var target=new RenderTexture(width,height,24);target.Create();var old=cam.targetTexture;cam.targetTexture=target;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;Canvas.ForceUpdateCanvases();cam.Render();var previous=RenderTexture.active;RenderTexture.active=target;var texture=new Texture2D(width,height,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,width,height),0,0);texture.Apply();File.WriteAllBytes(Path.Combine(dir,name+".png"),texture.EncodeToPNG());RenderTexture.active=previous;cam.targetTexture=old;Destroy(texture);target.Release();Destroy(target);}
```

## 对象层级和素材入口

- L46：`view.transform.Find("Rating reward")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
const BindingFlags Hidden=BindingFlags.NonPublic|BindingFlags.Instance;
```
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
