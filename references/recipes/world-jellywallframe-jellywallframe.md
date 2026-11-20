# JellyWallFrame · JellyWallFrame

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/JellyWallFrame.cs`
文件 SHA-256：`3e783f19c52575a7cc5401a9d4a00cc627ba6e0395b26d65a151e22c485aa407`
[原始源码快照](../source/Assets/Scripts/World/JellyWallFrame.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Envelope · L84

方法 SHA-256：`7e77930b1ac295f8487c48223a936d87f382d8acefc710f375c0019ddacc9cc0`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Exp, Mathf.Sin
解析符号：`BounceGarden.JellyWallFrame.Envelope(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/JellyWallQA.cs:63` · `JellyWallQA.Start` → `JellyWallFrame.Envelope(0)`
- `Assets/Scripts/JellyWallQA.cs:63` · `JellyWallQA.Start` → `JellyWallFrame.Envelope(.04f)`
- `Assets/Scripts/JellyWallQA.cs:64` · `JellyWallQA.Start` → `JellyWallFrame.Envelope(.19f)`
- `Assets/Scripts/JellyWallQA.cs:64` · `JellyWallQA.Start` → `JellyWallFrame.Envelope(JellyWallFrame.Duration)`
- `Assets/Scripts/World/JellyWallFrame.cs:95` · `JellyWallFrame.PulseShape` → `Envelope(travel)`
- `Assets/Scripts/World/JellyWallFrame.cs:95` · `JellyWallFrame.PulseShape` → `Envelope(age)`

```csharp
public static float Envelope(float age)
        {
            float t = Mathf.Clamp01(age / Duration);
            return Mathf.Sin(age * (Mathf.PI * 2 / .22f)) * Mathf.Exp(-9f * age) * (1-t)*(1-t) * 1.85f;
        }
```

### Influence · L90

方法 SHA-256：`7a8eb3d38bcd9edd50b9fb8de858338043cbe69e2b0876912a96df81a397e85c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Exp
解析符号：`BounceGarden.JellyWallFrame.Influence(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/JellyWallFrame.cs:95` · `JellyWallFrame.PulseShape` → `Influence(distance)`

```csharp
public static float Influence(float distance)
        {
            float core=distance/.38f;return Mathf.Exp(-core*core*2);
        }
```

### PulseShape · L95

方法 SHA-256：`d6d9ff767e4e26dbc9dc2852b14c08bac8e3f91b8fcdf6ea1f653a5e93cfc93c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Exp
解析符号：`BounceGarden.JellyWallFrame.PulseShape(float, float, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChainVisualQA.cs:42` · `ChainVisualQA.Start` → `JellyWallFrame.PulseShape(.25f,.8f,1)`
- `Assets/Scripts/ChainVisualQA.cs:42` · `ChainVisualQA.Start` → `JellyWallFrame.PulseShape(.04f,.8f,1)`
- `Assets/Scripts/World/JellyWallFrame.cs:101` · `JellyWallFrame.Deformation` → `PulseShape(pulses[i].age,Arc(point,side)-Arc(pulses[i].point,pulses[i].side),pulses[i].strength)`

```csharp
public static float PulseShape(float age,float distance,float strength){float travel=age-Mathf.Abs(distance)/4f;float wave=travel>0?.04f*Envelope(travel)*Mathf.Exp(-Mathf.Abs(distance)*1.5f):0;return strength*(.14f*Envelope(age)*Influence(distance)+wave);}
```

### LateUpdate · L118

方法 SHA-256：`2ba2ff0716b1df7c0b43d308032883a38e209a62525795dd1d9aefb968f247b8`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyWallFrame.LateUpdate()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void LateUpdate() { Advance(Time.deltaTime); }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable() { Clear(); }
```

```csharp
void OnDestroy()
        {
            if (material != null) Destroy(material);
            if (mesh != null) Destroy(mesh);
        }
```

## 源字段与依赖

```csharp
public const int Capacity = 16;
```
```csharp
public const float Duration = .50f;
```
```csharp
public const float MaximumDent = .155f;
```
```csharp
public const float HalfWidth = .18f;
```
```csharp
// Stop above the sleeping cat, matching the approved garden painting.
        // This is visual only; collider bounds and the return line are unchanged.
        public const float Bottom = -7.82f;
```
```csharp
readonly Pulse[] pulses = new Pulse[Capacity];
```
```csharp
readonly Vector4[] shaderPulses = new Vector4[Capacity];
```
```csharp
readonly Vector4[] shaderAges = new Vector4[Capacity];
```
```csharp
GameSession owner;
```
```csharp
Material material;
```
```csharp
Mesh mesh;
```
```csharp
int count;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
