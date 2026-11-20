# MovingObstacle · PhysicsBodies

状态：`mixed-physics-presentation-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/PhysicsBodies.cs`
文件 SHA-256：`4a7c2a6cede5908904f677f27deef0120740c077dad28921b906f4eedea61d21`
[原始源码快照](../source/Assets/Scripts/World/PhysicsBodies.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### FixedUpdate · L359

方法 SHA-256：`4c5aa9e2a7b535ad19800dcb8c46e28e920a6b1ecd7f9e15dc957b3179f431bb`
时间来源：Time.fixedDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.MovingObstacle.FixedUpdate()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/PhysicsBodies.cs:330` · `MovingObstacle.QATick` → `FixedUpdate()`

```csharp
void FixedUpdate()
        {
            if(owner==null||!owner.IsPlaying||owner.IsPaused)return;
            clock+=Time.fixedDeltaTime;
            if(definition.kind=="rotor")body.MoveRotation(body.rotation+definition.speed*Time.fixedDeltaTime);
            else if(definition.kind=="paddle")body.MovePosition(definition.position+Vector2.right*(Mathf.Sin(clock*definition.speed+definition.phase)-Mathf.Sin(definition.phase))*definition.travel);
        }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public void QAResetPose(){clock=0;body.position=definition.position;body.rotation=definition.angle;transform.position=definition.position;transform.rotation=Quaternion.Euler(0,0,definition.angle);}
```

## 源字段与依赖

```csharp
GameSession owner;
```
```csharp
Rigidbody2D body;
```
```csharp
ObstacleSpawn definition;
```
```csharp
float clock;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
