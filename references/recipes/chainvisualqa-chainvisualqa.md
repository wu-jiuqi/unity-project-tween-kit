# ChainVisualQA · ChainVisualQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChainVisualQA.cs`
文件 SHA-256：`ce87d77760c728aaf6f44b11c19070f1abf92c39b7b6df17374b39a001c0d7fe`
[原始源码快照](../source/Assets/Scripts/ChainVisualQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L18

方法 SHA-256：`78c8bd6b1a68448d892a14f8ec1d087caad5cd47c3abf0b2b62e141db695d137`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainVisualQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];if(Array.IndexOf(args,"--qa-height")>=0)height=int.Parse(args[Array.IndexOf(args,"--qa-height")+1]);Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();app.QASetViewport(new Vector2Int(width,height));app.Session.QAIgnoreFocusLoss=true;
  for(int sheet=0;sheet<4;sheet++)for(int row=0;row<4;row++)for(int frame=0;frame<6;frame++){var sprite=ChainVisualArt.Frame(sheet,row,frame);Check(sprite!=null&&sprite.texture.width==1536&&sprite.texture.height==1024&&sprite.rect.width>0&&sprite.rect.height>0,"RGBA frame loads "+sheet+"/"+row+"/"+frame);}
  app.QAStartLevel(53);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.5f);g=app.Session;
  Check(!PreviewExists(),"no top next-row label or predicted block objects");Check(g.NextCollectionRow.Any(x=>x!=0),"hidden supply planning remains active");
  var jar=g.CollectionBricks.First(b=>b.Definition.juiceGroup==1);var box=g.CollectionBricks.First(b=>b.Definition.juiceGroup==2);var rear=g.CollectionBricks.First(b=>b.Definition.kind==BrickKind.RearWeak);
  Check(!jar.Face.enabled&&!box.Face.enabled&&!rear.Face.enabled,"placeholder jelly faces hidden for painted actors");
  var jarSprite=jar.transform.Find("Container shell").GetComponentInChildren<SpriteRenderer>();var old=jarSprite.sprite;
  g.ApplyChainImpact(jar,1,Vector2.up,(Vector2)jar.transform.position+Vector2.down*.41f,true);Check(jarSprite.sprite!=old&&jarSprite.sprite.texture==ChainVisualArt.Frame(0,0,1).texture,"damage swaps actual jar artwork");
  var boxSprite=box.transform.Find("Container shell").GetComponentInChildren<SpriteRenderer>();old=boxSprite.sprite;g.ApplyChainImpact(box,1,Vector2.up,(Vector2)box.transform.position+Vector2.down*.41f,true);Check(boxSprite.sprite!=old&&box.transform.Find("Container shell/Lid").localPosition.y>.4f,"first hit opens the painted toy chest");
  yield return Capture("00-painted-opening-no-preview");
  var armor=g.CollectionBricks.First(b=>b.Definition.kind==BrickKind.Armor);g.ApplyChainImpact(armor,1,Vector2.up,(Vector2)armor.transform.position+Vector2.down*.41f,true);g.ApplyChainImpact(rear,1,Vector2.down,(Vector2)rear.transform.position+Vector2.up*.41f,true);
  yield return new WaitForSecondsRealtime(.08f);yield return Capture("01-shell-and-rear-hit");
  var needle=g.SpawnChainTool(1,new Vector2(-1.6f,.8f));var drum=g.SpawnChainTool(2,new Vector2(1.6f,.8f));var idle=needle.View.GetComponent<ChainIdleSprite>();idle.enabled=false;idle.Tick(.13f);int visible=idle.VisibleFrame;Check(visible>0,"needle idle uses sequential frames");g.SetPaused(true);idle.Tick(.3f);Check(idle.VisibleFrame==visible,"pause freezes idle animation");g.SetPaused(false);idle.enabled=true;
  var fx=ChainVisualArt.Effect(g,1,0,new Vector2(0,1),1);fx.enabled=false;fx.Tick(.08f);int effectFrame=fx.VisibleFrame;g.SetPaused(true);fx.Tick(.2f);Check(fx.VisibleFrame==effectFrame,"pause freezes frame effects");g.SetPaused(false);fx.Tick(.10f);Check(fx.VisibleFrame>effectFrame,"frame effect advances after resume");
  for(int i=0;i<4;i++)Call("RecordChainRatingBreak");yield return new WaitForSecondsRealtime(.12f);Check(!PreviewExists()&&g.NextChainRowTools.Any(x=>x>0),"earned reward remains hidden until row entry");yield return Capture("02-painted-tools-and-rating");
  g.StartLevel(53);yield return null;Empty();Add(new Vector2(.598f,1.9f),1,2);Add(new Vector2(1.638f,2.5f),1,2);drum=g.SpawnChainTool(2,new Vector2(-2,.4f));g.SpawnChainTool(1,new Vector2(-1.134f,.9f));yield return new WaitForSecondsRealtime(.4f);g.ActivateChainTool(drum,Vector2.up);yield return new WaitForSecondsRealtime(.12f);yield return Capture("03-animated-chain");yield return new WaitForSecondsRealtime(.65f);Check(g.Collected[0]==2&&g.ChainActivationCount==2,"painted drum-to-needle chain still collects two jars");yield return Capture("04-collection-arrival");
  // Native frame capture: four real effect players stepped at their actual frame rate.
  g.StartLevel(53);yield return null;Empty();var effects=new ChainSpriteFx[4];for(int r=0;r<4;r++){effects[r]=ChainVisualArt.Effect(g,1,r,new Vector2(r%2==0?-2:2,r<2?2:-1),1.5f);effects[r].enabled=false;}Directory.CreateDirectory(Path.Combine(dir,"actions"));
  for(int f=0;f<6;f++){if(f>0)foreach(var effect in effects)effect.Tick(1f/18);yield return Capture("actions/frame-"+f.ToString("000"));}foreach(var effect in effects)if(effect!=null)Destroy(effect.gameObject);
  var walls=g.JellyWalls;walls.enabled=false;walls.Clear();Vector2 contact=new Vector2(FlightGeometry.Right,-1);
  walls.Impact(contact,2,12);walls.Advance(.04f);float strong=walls.Deformation(contact,2);walls.Clear();walls.Impact(contact,2,3);walls.Advance(.04f);float light=walls.Deformation(contact,2);Check(strong>light*2&&strong<=JellyWallFrame.MaximumDent,"wall amplitude scales with normal impact strength and remains bounded");
  Check(Mathf.Abs(JellyWallFrame.PulseShape(.25f,.8f,1))>Mathf.Abs(JellyWallFrame.PulseShape(.04f,.8f,1))*5,"ripple reaches its neighbor after a delay");
  walls.Clear();walls.Impact(new Vector2(FlightGeometry.Right,FlightGeometry.Top),2,12);walls.Advance(.16f);Check(Mathf.Abs(walls.Deformation(new Vector2(3.9f,FlightGeometry.Top),1))>.001f,"ripple travels around the top corner");Check(Mathf.Abs(walls.Deformation(new Vector2(FlightGeometry.Left,-1),0))<.00001f,"opposite wall does not move");
  var left=g.transform.Find("WorldRoot/LeftWall").GetComponent<BoxCollider2D>();var right=g.transform.Find("WorldRoot/RightWall").GetComponent<BoxCollider2D>();Check(Mathf.Abs(left.transform.position.x+4.2f)<.00001f&&Mathf.Abs(right.transform.position.x-4.2f)<.00001f&&right.size==new Vector2(.2f,15.99f),"visual wall update preserves collider coordinates and dimensions");
  walls.Clear();Directory.CreateDirectory(Path.Combine(dir,"border"));yield return Capture("border/frame-000");walls.Impact(contact,2,12);walls.Impact(new Vector2(FlightGeometry.Right,FlightGeometry.Top-.1f),2,10);walls.Impact(new Vector2(0,FlightGeometry.Top),1,8);
  for(int f=1;f<=24;f++){walls.Advance(1f/30);yield return Capture("border/frame-"+f.ToString("000"));}
  Check(walls.ActiveImpacts==0&&walls.Deformation(contact,2)==0,"border returns exactly to rest without drift");walls.enabled=true;
  app.QANavigate("home");yield return new WaitForSecondsRealtime(.1f);Check(FindObjectsByType<ChainBrickView>(FindObjectsSortMode.None).Length==0&&FindObjectsByType<ChainSpriteFx>(FindObjectsSortMode.None).Length==0,"leaving level clears painted actors and frame players");
  File.AppendAllText(Path.Combine(dir,"checks.txt"),$"RESULT checks={checks} failures={failures} errors={errors}\n");Application.Quit(failures+errors==0?0:1);
 }
```

### Capture · L51

方法 SHA-256：`95b1a8c1ef930067c6708170d0e8dad02346794ba542de51f426b5cd46bc2b08`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChainVisualQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainVisualQA.cs:30` · `ChainVisualQA.Start` → `Capture("00-painted-opening-no-preview")`
- `Assets/Scripts/ChainVisualQA.cs:32` · `ChainVisualQA.Start` → `Capture("01-shell-and-rear-hit")`
- `Assets/Scripts/ChainVisualQA.cs:35` · `ChainVisualQA.Start` → `Capture("02-painted-tools-and-rating")`
- `Assets/Scripts/ChainVisualQA.cs:36` · `ChainVisualQA.Start` → `Capture("03-animated-chain")`
- `Assets/Scripts/ChainVisualQA.cs:36` · `ChainVisualQA.Start` → `Capture("04-collection-arrival")`
- `Assets/Scripts/ChainVisualQA.cs:39` · `ChainVisualQA.Start` → `Capture("actions/frame-"+f.ToString("000"))`
- `Assets/Scripts/ChainVisualQA.cs:45` · `ChainVisualQA.Start` → `Capture("border/frame-000")`
- `Assets/Scripts/ChainVisualQA.cs:46` · `ChainVisualQA.Start` → `Capture("border/frame-"+f.ToString("000"))`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var cam=Camera.main;var target=new RenderTexture(width,height,24);target.Create();var old=cam.targetTexture;cam.targetTexture=target;cam.rect=new Rect(0,0,1,1);cam.aspect=(float)width/height;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;Canvas.ForceUpdateCanvases();var design=(RectTransform)typeof(AppUI).GetField("designRoot",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).GetValue(app);var safe=(RectTransform)design.parent;design.localScale=Vector3.one*Mathf.Min(safe.rect.width/1080f,safe.rect.height/2340f);Canvas.ForceUpdateCanvases();cam.Render();var prev=RenderTexture.active;RenderTexture.active=target;var texture=new Texture2D(width,height,TextureFormat.RGB24,false);texture.ReadPixels(new Rect(0,0,width,height),0,0);texture.Apply();File.WriteAllBytes(Path.Combine(dir,name+".png"),texture.EncodeToPNG());RenderTexture.active=prev;cam.targetTexture=old;Destroy(texture);target.Release();Destroy(target);}
```

## 对象层级和素材入口

- L27：`jar.transform.Find("Container shell")`
- L29：`box.transform.Find("Container shell")`
- L44：`g.transform.Find("WorldRoot/LeftWall")`

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
string dir;
```
```csharp
int checks,failures,errors,width=540,height=1170;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
