# BattleScoreMeter · BattleScoreMeter

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/BattleScoreMeter.cs`
文件 SHA-256：`ebe6d6a89afdedf69eb5408b3d7b3868911ef4524e835afba8ef8176b049da52`
[原始源码快照](../source/Assets/Scripts/UI/BattleScoreMeter.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### OnBall · L26

方法 SHA-256：`37b2ed23a895d969b1805ec3a8c832ef81e3aabb17da857bf4c85333b1305581`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Exp
解析符号：`BounceGarden.BattleScoreMeter.OnBall(UnityEngine.Vector2, float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
void OnBall(Vector2 point,float speed){if(!Allowed)return;BallResponses++;for(int i=0;i<3;i++){float distance=Mathf.Abs(point.x-xs[i]);float direction=point.x>=xs[i]?1:-1;float influence=.48f+.52f*Mathf.Exp(-distance*.5f);Impulse(i,direction*Mathf.Clamp(speed*16,75,190)*influence);}}
```

### LateUpdate · L29

方法 SHA-256：`8fdb1e55ab0de7dee2e3a26b237cb1c4f04ba52bfaba513889aaea48da316b45`
时间来源：PresentationDelta, Time.deltaTime, Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BattleScoreMeter.LateUpdate()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void LateUpdate(){if(victoryCounting){Place();foreach(var charge in charges)charge.Advance(game.PresentationDelta(Mathf.Min(.05f,Time.unscaledDeltaTime)));}else Advance(Time.deltaTime);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(game!=null){game.VineBallContact-=OnBall;game.VineFallingContact-=OnFalling;}}
```

## 源字段与依赖

```csharp
GameSession game;
```
```csharp
int generation;
```
```csharp
RectTransform root;
```
```csharp
int[] thresholds;
```
```csharp
float clock;
```
```csharp
readonly RectTransform[] hinges=new RectTransform[3];
```
```csharp
readonly Image[] stars=new Image[3];
```
```csharp
readonly StarChargeView[] charges=new StarChargeView[3];
```
```csharp
bool victoryCounting;
```
```csharp
int displayScore;
```
```csharp
readonly float[] angle=new float[3],velocity=new float[3],lastBrick={-10,-10,-10};
```
```csharp
readonly float[] xs={-2.85f,0,2.85f};
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
