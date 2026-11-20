# PixelDropBody · PixelDropSession

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/PixelDropSession.cs`
文件 SHA-256：`bdfa6570561822ac732c706190eb2b0ac1cdeaaa80fc1d9463393a20246b3ffe`
[原始源码快照](../source/Assets/Scripts/Core/PixelDropSession.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Tick · L42

方法 SHA-256：`37822effe85e90efe2ba477fc5421577c4d69239ed123a0b5f51422c10e34af3`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.PixelDropBody.Tick(float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Tick(float dt){if(!owner.IsPlaying||owner.IsPaused||!Brick.Alive)return;
 wobble+=dt;
 if(box.bounds.min.x>FlightGeometry.Right||box.bounds.max.x<FlightGeometry.Left){owner.RecyclePixelDrop(this);return;}
 if(!Detached){Vector2 delta=anchor-Body.position;float softness=.7f+.3f*Stability/grade;
 Body.AddForce((delta*(30f*softness)-Body.linearVelocity*7f)*Body.mass);
 Body.AddTorque((-Mathf.DeltaAngle(0,Body.rotation)*.12f-Body.angularVelocity*.065f)*Body.mass);
 // Large displacement/rotation can tear a weakened tether before its nominal count reaches zero.
 if(delta.magnitude>.18f+.085f*Stability||Mathf.Abs(Mathf.DeltaAngle(0,Body.rotation))>30+Stability*9)Detach();
 }else{
 // Table friction opposes motion in both axes; no vertical force or automatic drift.
 var v=Body.linearVelocity;Body.linearVelocity=Vector2.MoveTowards(v,Vector2.zero,(.65f+.12f*Body.mass)*dt);
 Body.angularVelocity=Mathf.MoveTowards(Body.angularVelocity,0,45*dt);
 }
 if(Body.position.y>FlightGeometry.Top-.4f){Body.position=new Vector2(Body.position.x,FlightGeometry.Top-.4f);Body.linearVelocity=new Vector2(Body.linearVelocity.x,-Mathf.Abs(Body.linearVelocity.y)*.8f);}
 if(Body.position.y<FlightGeometry.ReturnY+.4f){Body.position=new Vector2(Body.position.x,FlightGeometry.ReturnY+.4f);Body.linearVelocity=new Vector2(Body.linearVelocity.x,Mathf.Abs(Body.linearVelocity.y)*.8f);}

 }
```

### LateUpdate · L62

方法 SHA-256：`363cb5e5574e4121be1ee8079024f349a4035baf0d0186b808c4530601c296f7`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Exp
解析符号：`BounceGarden.PixelDropBody.LateUpdate()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void LateUpdate(){if(owner==null||owner.IsPaused||!Brick.Alive)return;
 float f=grade<=2?20:15;float pulse=Mathf.Exp(-wobble*5)*Mathf.Cos(wobble*f)*(.22f/Mathf.Sqrt(Body.mass));
 // Rotate the visual deformation axes; the collider retains its rigid square shape.
 visual.localRotation=Quaternion.Euler(0,0,axis);Brick.Face.transform.localRotation=Quaternion.Euler(0,0,-axis);
 visual.localScale=new Vector3(1-pulse,1+pulse,1);Brick.Face.transform.localScale=faceScale;
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(box!=null&&box.sharedMaterial!=null)Destroy(box.sharedMaterial);}
```

## 源字段与依赖

```csharp
static Material jellyMaterial;
```
```csharp
GameSession owner;
```
```csharp
Collider2D box;
```
```csharp
Vector2 anchor;
```
```csharp
Vector3 faceScale;
```
```csharp
Transform visual;
```
```csharp
float wobble=10,axis;
```
```csharp
int grade;
```
```csharp
readonly Dictionary<int,float> lastBallHit=new Dictionary<int,float>();
```
```csharp
readonly List<SpriteRenderer> marks=new List<SpriteRenderer>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
