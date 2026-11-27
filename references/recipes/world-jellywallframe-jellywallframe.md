# JellyWallFrame · JellyWallFrame

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/JellyWallFrame.cs`
文件 SHA-256：`3911af7eaa86d53d363e68ec58771577a8ac75da63979b7bb579d4ffa87d51e6`
[原始源码快照](../source/Assets/Scripts/World/JellyWallFrame.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### LateUpdate · L57

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
public const float HalfWidth = .18f;
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
