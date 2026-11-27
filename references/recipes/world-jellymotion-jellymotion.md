# JellyMotion · JellyMotion

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/World/JellyMotion.cs`
文件 SHA-256：`7d09a4f80920e73b6d6db1ce1b0c6727a8618ddeee185a2a3fb6c9d6760fc164`
[原始源码快照](../source/Assets/Scripts/World/JellyMotion.cs.txt)

用在哪里：OpeningCopperView.ImpactStretch / OpeningThirdArt.ImpactStretch → JellyMotion.HitStretch；VictoryPresentation.Stars 复用 Float*
触发操作：铜钱／招财猫方向受击；结算星星与炸弹待机浮动
导入组件：`ProjectTweenKit.JellyHit`；展馆 ID：`hit`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### HitStretch · L6

方法 SHA-256：`42b52d377450315b10420ebccb41783bd665469713a367a231f556b1963a6e2c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Exp, Mathf.Sin
解析符号：`BounceGarden.JellyMotion.HitStretch(float, UnityEngine.Vector2)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/OpeningCopperArt.cs:23` · `OpeningCopperView.ImpactStretch` → `JellyMotion.HitStretch(age,impactDirection)`
- `Assets/Scripts/World/OpeningThirdArt.cs:23` · `OpeningCatView.ImpactStretch` → `JellyMotion.HitStretch(impactAge,impactDirection)`

```csharp
public static Vector2 HitStretch(float age,Vector2 incoming){float t=Mathf.Clamp01(age/HitDuration);float wave=t>=1?0:Mathf.Sin(t*Mathf.PI*4)*Mathf.Exp(-5*t)*.28f;return Mathf.Abs(incoming.y)>=Mathf.Abs(incoming.x)?new Vector2(1+wave,1-wave):new Vector2(1-wave,1+wave);}
```

### FloatOffset · L7

方法 SHA-256：`bdf0b1659b5572b3a16f493729fbb04c3c4318f3a7c6d7e323d5a9c86759904c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.JellyMotion.FloatOffset(float, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/VictoryFinale.cs:37` · `VictoryFinale.Advance` → `JellyMotion.FloatOffset(hold,i)`
- `Assets/Scripts/UI/VictoryPresentation.Stars.cs:9` · `VictoryPresentation.RenderHeaderStars` → `JellyMotion.FloatOffset(idle,i)`

```csharp
public static Vector2 FloatOffset(float age,int index)=>new Vector2(Mathf.Sin(age*7+index)*5,Mathf.Sin(age*5.3f+index*2)*8);
```

### FloatScale · L8

方法 SHA-256：`fa15f52cf4d8ec2bf9ee450d7d670cc2b54c5ee8c6816f62f82393b049fae6d2`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.JellyMotion.FloatScale(float, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/VictoryFinale.cs:37` · `VictoryFinale.Advance` → `JellyMotion.FloatScale(hold,i)`
- `Assets/Scripts/UI/VictoryPresentation.Stars.cs:9` · `VictoryPresentation.RenderHeaderStars` → `JellyMotion.FloatScale(idle,i)`

```csharp
public static Vector3 FloatScale(float age,int index)=>new Vector3(1+Mathf.Sin(age*9+index)*.1f,1+Mathf.Sin(age*7.7f+index*2)*.12f,1);
```

### FloatAngle · L9

方法 SHA-256：`c480b59cdd6d12a4abf899986660cb73b619e3bf31502134a974dbfe16dba328`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.JellyMotion.FloatAngle(float, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/VictoryFinale.cs:37` · `VictoryFinale.Advance` → `JellyMotion.FloatAngle(hold,i)`
- `Assets/Scripts/UI/VictoryPresentation.Stars.cs:9` · `VictoryPresentation.RenderHeaderStars` → `JellyMotion.FloatAngle(idle,i)`

```csharp
public static float FloatAngle(float age,int index)=>Mathf.Sin(age*6+index)*7;
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public const float HitDuration=.45f;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
