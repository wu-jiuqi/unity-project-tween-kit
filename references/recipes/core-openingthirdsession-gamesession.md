# GameSession · OpeningThirdSession

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/OpeningThirdSession.cs`
文件 SHA-256：`e98d8c98bfedd7d8db8123229e4493a50bbe9ba913582d78573da8999dd3cd82`
[原始源码快照](../source/Assets/Scripts/Core/OpeningThirdSession.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### TickOpeningThird · L84

方法 SHA-256：`4e1c96fad5625d404c8900f1409a9f26ef3d7dffce220e10c2aea673686af750`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin, Mathf.SmoothStep
解析符号：`BounceGarden.GameSession.TickOpeningThird(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/CatFlowQA.cs:14` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.01f)`
- `Assets/Scripts/CatFlowQA.cs:20` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.01f)`
- `Assets/Scripts/CatFlowQA.cs:26` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.01f)`
- `Assets/Scripts/CatFlowQA.cs:30` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.20f)`
- `Assets/Scripts/CatFlowQA.cs:31` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.02f)`
- `Assets/Scripts/CatFlowQA.cs:34` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.01f)`
- `Assets/Scripts/CatFlowQA.cs:39` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.02f)`
- `Assets/Scripts/CatFlowQA.cs:42` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.01f)`
- `Assets/Scripts/CatFlowQA.cs:47` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.01f)`
- `Assets/Scripts/CatFlowQA.cs:55` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.01f)`
- `Assets/Scripts/CatFlowQA.cs:56` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.02f)`
- `Assets/Scripts/CatFlowQA.cs:61` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.01f)`
- `Assets/Scripts/CatFlowQA.cs:62` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.5f)`
- `Assets/Scripts/CatFlowQA.cs:63` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.70f)`
- `Assets/Scripts/CatFlowQA.cs:64` · `GameSession.QACatFlowChecks` → `TickOpeningThird(.10f)`
- `Assets/Scripts/SfxV87QA.cs:32` · `SfxV87QA.Start` → `game.TickOpeningThird(.02f)`
- `Assets/Scripts/ThirdGroupQA.cs:13` · `GameSession.QAThirdStep` → `TickOpeningThird(dt)`
- `Assets/Scripts/ThirdGroupQA.cs:32` · `ThirdGroupQA.Start` → `g.TickOpeningThird(.04f)`
- `Assets/Scripts/ThirdGroupQA.cs:32` · `ThirdGroupQA.Start` → `g.TickOpeningThird(.2f)`
- `Assets/Scripts/ThirdGroupQA.cs:32` · `ThirdGroupQA.Start` → `g.TickOpeningThird(.04f)`
- `Assets/Scripts/ThirdGroupQA.cs:32` · `ThirdGroupQA.Start` → `g.TickOpeningThird(.5f)`
- `Assets/Scripts/ThirdGroupQA.cs:34` · `ThirdGroupQA.Start` → `g.TickOpeningThird(.5f)`
- `Assets/Scripts/World/OpeningThirdArt.cs:36` · `OpeningThirdDirector.Update` → `game.TickOpeningThird(game.PresentationDelta(Time.unscaledDeltaTime))`

```csharp
public void TickOpeningThird(float dt){
  if(!IsOpening||IsPaused||!IsPlaying&&!VictorySettlementActive||dt<=0)return;
  foreach(var cat in openingCats)if(cat!=null)cat.TickImpact(dt);
  if(OpeningGoalsEarned)foreach(var c in openingCats)c.Queued=c.PendingIngots=0;
  if(openingCasts.Count==0)BeginCatBatch();
  foreach(var cast in openingCasts.ToArray()){
   cast.Age+=dt;cast.Cat.WaveAge=cast.Age;
   if(!cast.Light&&cast.Age>=CatWindup){cast.Light=true;OpeningPlayRing(cast.Cat.transform.position);SoundService.Play("lucky_cat_cast");}
   foreach(var f in cast.Flights){
    if(f.Cancelled)continue;
    if(!f.Arrived&&!TrackCatTarget(f)&&!RetargetCatFlight(f,cast.Age)){
     // A live clear may temporarily exhaust all legal targets. Preserve only
     // this undelivered ingot, without holding the board or settlement open.
     f.Arrived=f.Cancelled=true;f.LandAge=cast.Age;cast.Cat.PendingIngots++;openingCatWaitingForSpace=true;f.Image.enabled=f.Ray.enabled=false;ScheduleChainGravity();continue;
    }
    float rayProgress=Mathf.Clamp01((cast.Age-CatWindup)/.10f);var rayTip=Vector2.Lerp(f.Start,f.End,rayProgress*.38f);var rayDelta=rayTip-f.Start;f.Ray.transform.position=(f.Start+rayTip)*.5f;f.Ray.transform.rotation=Quaternion.Euler(0,0,Mathf.Atan2(rayDelta.y,rayDelta.x)*Mathf.Rad2Deg);f.Ray.transform.localScale=new Vector3(rayDelta.magnitude,.045f,1);f.Ray.color=new Color(1,.79f,.16f,Mathf.Clamp01((.40f-cast.Age)/.10f));float u=Mathf.Clamp01((cast.Age-f.Delay)/f.Duration);if(cast.Age<f.Delay)continue;f.Image.enabled=true;
    Vector2 control=f.Start+Vector2.up*(.75f+Vector2.Distance(f.Start,f.End)*.2f);f.Image.transform.position=(1-u)*(1-u)*f.Start+2*(1-u)*u*control+u*u*f.End;
    float size=Mathf.Lerp(.018f,.76f,u*u);if(u>=1){if(!f.Arrived){IngotDisplace(f);f.Arrived=true;f.LandAge=cast.Age;}float t=Mathf.Clamp01((cast.Age-f.LandAge)/CatTail);size=.76f*(1+Mathf.Sin(t*Mathf.PI)*.13f);f.Image.color=new Color(1,1,1,1-t);}
    OpeningThirdArt.Fit(f.Image,"ingot",OpeningThirdArt.Variant(f.Tool?.Kind??0),size);
   }
   if(cast.Age>=.8f&&cast.Flights.All(f=>f.Arrived&&cast.Age>=f.LandAge+CatTail)){cast.Cat.WaveAge=-1;foreach(var f in cast.Flights){if(f.Image!=null)Destroy(f.Image.gameObject);if(f.Ray!=null)Destroy(f.Ray.gameObject);}openingCasts.Remove(cast);}
  }
  foreach(var ghost in openingGhosts.ToArray()){ghost.Age+=dt;float t=ghost.Age/.55f;ghost.Image.transform.position=ghost.Start+ghost.Velocity*ghost.Age+Vector2.down*5*ghost.Age*ghost.Age;ghost.Image.transform.localRotation=Quaternion.Euler(0,0,ghost.Spin*ghost.Age);ghost.Image.color=new Color(1,1,1,1-Mathf.SmoothStep(0,1,Mathf.InverseLerp(.08f,.55f,ghost.Age)));if(t>=1){Destroy(ghost.Image.gameObject);openingGhosts.Remove(ghost);}}
  foreach(var ring in openingRings.ToArray()){ring.Age+=dt;OpeningThirdArt.Fit(ring.Image,"ring",Mathf.Clamp((int)(ring.Age*24),0,15),2.0f);if(ring.Age>=16f/24){Destroy(ring.Image.gameObject);openingRings.Remove(ring);}}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
readonly List<OpeningCatView> openingCats=new List<OpeningCatView>();
```
```csharp
const int CatIngotCount=5;
```
```csharp
const float CatWindup=.18f,CatFlightTime=.36f,CatFlightStagger=.04f,CatTail=.10f;
```
```csharp
readonly List<CatCast> openingCasts=new List<CatCast>();
```
```csharp
readonly List<ThirdGhost> openingGhosts=new List<ThirdGhost>();
```
```csharp
readonly List<ThirdRing> openingRings=new List<ThirdRing>();
```
```csharp
bool openingCatWaitingForSpace;
```
```csharp
System.Random openingCatRandom;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
