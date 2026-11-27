# BattleScoreMeter · BattleScoreMeter

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/BattleScoreMeter.cs`
文件 SHA-256：`700779743353107445b65ee85a9be138e307085e6cc488dce0592869ab574c29`
[原始源码快照](../source/Assets/Scripts/UI/BattleScoreMeter.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `PendantSwing` / `pendulum`：提取纯视觉四子步弹簧；默认示例冲量120，保留30/3.6/36度/-.25；碰撞影响由宿主换算后输入

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### OnBall · L29

方法 SHA-256：`37b2ed23a895d969b1805ec3a8c832ef81e3aabb17da857bf4c85333b1305581`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Exp
解析符号：`BounceGarden.BattleScoreMeter.OnBall(UnityEngine.Vector2, float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
void OnBall(Vector2 point,float speed){if(!Allowed)return;BallResponses++;for(int i=0;i<3;i++){float distance=Mathf.Abs(point.x-xs[i]);float direction=point.x>=xs[i]?1:-1;float influence=.48f+.52f*Mathf.Exp(-distance*.5f);Impulse(i,direction*Mathf.Clamp(speed*16,75,190)*influence);}}
```

### Advance · L31

方法 SHA-256：`2ce9821cec2de4772a0101fa7b19af137b3d38cc41fdfa9ab04d052b15cc6b92`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Exp
解析符号：`BounceGarden.BattleScoreMeter.Advance(float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Advance(float dt){if(!Allowed)return;dt=Mathf.Clamp(dt,0,.05f);clock+=dt;if(FeedbackProfile.Active.enabledFeedback)BallPass(dt);for(int i=0;i<3;i++){if(!FeedbackProfile.Active.enabledFeedback){angle[i]=velocity[i]=0;continue;}velocity[i]*=Mathf.Exp(-1.35f*dt);angle[i]+=velocity[i]*dt;}Place();foreach(var charge in charges)if(charge!=null)charge.Advance(dt);}
```

### LateUpdate · L32

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
readonly Vector4[] ballSamples=new Vector4[32];
```
```csharp
readonly RectTransform[] anchors=new RectTransform[3];
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
readonly float[] xs={-2.15f,0,2.15f};
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
