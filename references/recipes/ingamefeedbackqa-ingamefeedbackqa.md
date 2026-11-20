# InGameFeedbackQA · InGameFeedbackQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/InGameFeedbackQA.cs`
文件 SHA-256：`abeb341120e90438ea55aa9328b4db1bcb787bdfe0bdfd8717c3948abf352577`
[原始源码快照](../source/Assets/Scripts/InGameFeedbackQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Level · L19

方法 SHA-256：`47ec71e0a6de8c857f6a87cc6777e2d811ffa0940d5cfca188b4478087a313d6`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.InGameFeedbackQA.Level(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/InGameFeedbackQA.cs:26` · `InGameFeedbackQA.Start` → `Level(1)`
- `Assets/Scripts/InGameFeedbackQA.cs:32` · `InGameFeedbackQA.Start` → `Level(6)`
- `Assets/Scripts/InGameFeedbackQA.cs:34` · `InGameFeedbackQA.Start` → `Level(11)`
- `Assets/Scripts/InGameFeedbackQA.cs:41` · `InGameFeedbackQA.Start` → `Level(11)`

```csharp
IEnumerator Level(int index){app.QAStartLevel(index);yield return app.QAWaitForLevel();yield return null;view=FindFirstObjectByType<InGameFeedbackView>();Check(view!=null,"feedback view attached to stage "+index);}
```

### Capture · L20

方法 SHA-256：`dfff0a8122b36c830ac21b28ab5c22a6fe8fbc69787e17c5af29423f3001711a`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.InGameFeedbackQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/InGameFeedbackQA.cs:30` · `InGameFeedbackQA.Start` → `Capture("classic-nice")`
- `Assets/Scripts/InGameFeedbackQA.cs:31` · `InGameFeedbackQA.Start` → `Capture("classic-danmaku")`
- `Assets/Scripts/InGameFeedbackQA.cs:37` · `InGameFeedbackQA.Start` → `Capture("ability-tier-"+total)`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();Canvas.ForceUpdateCanvases();cam.Render();var old=RenderTexture.active;RenderTexture.active=target;var im=new Texture2D(width,height,TextureFormat.RGB24,false);im.ReadPixels(new Rect(0,0,width,height),0,0);im.Apply();RenderTexture.active=old;File.WriteAllBytes(Path.Combine(dir,name+".png"),im.EncodeToPNG());Destroy(im);}
```

### Start · L22

方法 SHA-256：`8813630cfbb109baf522ab24b73d2c0437e9f09c51d5ce0e916031ee4f52dad8`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.InGameFeedbackQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");int size=Array.IndexOf(args,"--qa-size");if(size>=0){var v=args[size+1].Split('x');width=int.Parse(v[0]);height=int.Parse(v[1]);}
  Application.logMessageReceived+=(m,stack,type)=>{if(type==LogType.Error||type==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+stack+"\n");}};
  ModelTests();yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();g=app.Session;cam=Camera.main;target=new RenderTexture(width,height,24);target.Create();cam.targetTexture=target;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;app.QASetViewport(new Vector2Int(width,height));Check(RuntimeQA.IsIsolated,"player save isolated");
  yield return Level(1);Check(!view.State.HasShot&&!view.BulletVisible,"no pre-shot bullet");Check(g.TryShoot(Vector2.up),"classic launch accepted");float burstEnd=Time.realtimeSinceStartup+.55f;while(Time.realtimeSinceStartup<burstEnd){yield return new WaitForFixedUpdate();foreach(var held in g.GetComponentsInChildren<BallBody>())if(held.Active){held.Body.simulated=false;held.Body.position=new Vector2(0,-4);}}Check(g.SpawnCount==5&&g.Shots==g.CurrentLevel.initialShots-1,"five ball volley and one shot cost retained");
  var ball=g.GetComponentsInChildren<BallBody>().First(b=>b.Active);foreach(var b in g.GetComponentsInChildren<BallBody>())if(b.Active){b.Body.position=new Vector2(0,-4);b.Body.linearVelocity=Vector2.right*g.CurrentLevel.ballSpeed;}
  var brick=g.GetComponentsInChildren<BrickBody>().First(b=>b.Alive&&b.CurrentHealth>1);int before=g.BreakCount;ClearContacts();g.HitBrick(brick,ball,brick.transform.position);Check(g.BreakCount==before&&view.State.ChainCount==0,"nonlethal HP damage does not count");
  foreach(var b in g.GetComponentsInChildren<BrickBody>().Where(b=>b.Alive).Take(4)){while(b.Alive){ClearContacts();g.HitBrick(b,ball,b.transform.position);}}
  yield return null;Check(view.VisibleTier==0&&view.State.ChainCount==4,"real classic eliminations trigger NICE");yield return new WaitForSecondsRealtime(.45f);g.SetPaused(true);yield return Capture("classic-nice");float c=view.State.Clock,a=view.BulletAge;yield return new WaitForSecondsRealtime(.25f);Check(view.State.Clock==c&&view.BulletAge==a,"pause freezes chain and both presentation clocks");g.SetPaused(false);
  yield return new WaitForSecondsRealtime(1.5f);Check(view.BulletVisible,"first shot eventually produces companion bullet");Check(view.Caption.text.Contains(view.Audience),"stable in-session audience inserted");float x=view.BulletX;yield return new WaitForSecondsRealtime(.3f);Check(view.BulletX<x,"bubble moves from right to left");yield return new WaitForSecondsRealtime(1.5f);Check(!view.Caption.textInfo.characterInfo.Take(view.Caption.textInfo.characterCount).Any(ch=>ch.character=='\u25a1'),"Chinese caption contains no replacement boxes");g.SetPaused(true);yield return Capture("classic-danmaku");var bubble=view.transform.Find("Danmaku viewport/Danmaku bubble");Check(bubble.GetComponent<Image>().sprite.border.x==20,"gray plate uses 20px nine slice");Check(view.GetComponentsInChildren<Graphic>(true).All(i=>!i.raycastTarget),"feedback never intercepts aim input");g.SetPaused(false);
  yield return Level(6);Check(view.State.ChainCount==0&&!view.State.HasShot,"new level discards old chain and pending bullet");string audience=view.Audience;
  foreach(int total in new[]{4,8,12,18}){
   yield return Level(11);int remaining=total;
   foreach(var b in g.GetComponentsInChildren<BrickBody>().Where(b=>b.Alive&&b.Definition.kind==BrickKind.Normal).Take(remaining))g.ApplyAbilityDamage(b,999,true);
   yield return null;Check(view.VisibleTier==Array.IndexOf(new[]{4,8,12,18},total),"ability eliminations show tier at "+total);
   yield return new WaitForSecondsRealtime(.45f);g.SetPaused(true);yield return Capture("ability-tier-"+total);g.SetPaused(false);
  }
  Check(!string.IsNullOrEmpty(view.Audience),"audience generated for ability stage");
  app.QAResult(false);yield return null;Check(!view.BulletVisible&&view.VisibleTier<0&&view.State.PendingTier<0,"result immediately clears feedback");app.QANavigate("home");yield return null;Check(FindFirstObjectByType<InGameFeedbackView>()==null,"leaving battle destroys subscriptions and views");
  yield return Level(11);foreach(var b in g.GetComponentsInChildren<BrickBody>().Where(b=>b.Alive&&b.Definition.kind==BrickKind.Normal).Take(18))g.ApplyAbilityDamage(b,999,true);yield return null;Check(view.VisibleTier==3,"same-frame ability burst skips lower tiers");
  string result="RESULT checks="+checks+" failed="+failed+" errors="+errors;File.AppendAllText(Path.Combine(dir,"checks.txt"),result+"\n");Debug.Log(result);Application.Quit(failed==0&&errors==0?0:1);
 }
```

## 对象层级和素材入口

- L31：`view.transform.Find("Danmaku viewport/Danmaku bubble")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
const BindingFlags Private=BindingFlags.Instance|BindingFlags.NonPublic;
```
```csharp
AppUI app;
```
```csharp
GameSession g;
```
```csharp
InGameFeedbackView view;
```
```csharp
string dir;
```
```csharp
int checks,failed,errors,width=540,height=1170;
```
```csharp
Camera cam;
```
```csharp
RenderTexture target;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
