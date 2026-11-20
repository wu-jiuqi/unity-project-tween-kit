# VictoryCatAnimation · VictoryCatAnimation

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/VictoryCatAnimation.cs`
文件 SHA-256：`0c62df1e090ca692c2a8a1ab3d22b80052b9d7c7ec94362a6ec09aa63652681e`
[原始源码快照](../source/Assets/Scripts/UI/VictoryCatAnimation.cs.txt)

用在哪里：AppUI.ShowResults → VictoryPresentation.Render → VictoryCatAnimation.FrameAt/Get
触发操作：胜利猫16帧，逐帧时间原样保留
导入组件：`ProjectTweenKit.FrameSequence`；展馆 ID：`victorycat`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### FrameAt · L11

方法 SHA-256：`af00525295ad40f37d8e13db64e3272985f91edca8d5cd902d8f84ee43444c96`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryCatAnimation.FrameAt(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/VictoryPresentation.cs:82` · `VictoryPresentation.Render` → `VictoryCatAnimation.FrameAt(clock - .08f)`
- `Assets/Scripts/VictoryQA.cs:76` · `VictoryQA.Start` → `VictoryCatAnimation.FrameAt(age + durations[i] / 2)`

```csharp
public static int FrameAt(float age) {
        for (int i = 0; i < FrameCount - 1; i++) {
            if (age < durations[i]) return i;
            age -= durations[i];
        }
        return FrameCount - 1;
    }
```

### Get · L18

方法 SHA-256：`627f9a791a251d77e77e67380a45f0caa493725cda0c1819cdce80cbe53f263e`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryCatAnimation.Get(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/VictoryPresentation.cs:49` · `VictoryPresentation.Begin` → `VictoryCatAnimation.Get(0)`
- `Assets/Scripts/UI/VictoryPresentation.cs:83` · `VictoryPresentation.Render` → `VictoryCatAnimation.Get(CatFrame)`
- `Assets/Scripts/VictoryQA.cs:76` · `VictoryQA.Start` → `VictoryCatAnimation.Get(i)`

```csharp
public static Sprite Get(int index) {
        index = Mathf.Clamp(index, 0, FrameCount - 1);
        if (frames[index] != null) return frames[index];
        var atlas = Resources.Load<Texture2D>("Art/VictoryCatV23/cat-atlas");
        if (atlas == null) throw new InvalidOperationException("Victory cat sequence atlas is missing.");
        for (int i = 0; i < FrameCount; i++) {
            float w=atlas.width/4f,h=atlas.height/4f;
            frames[i] = Sprite.Create(atlas, new Rect(i % 4*w, (3-i/4)*h, w,h), new Vector2(.5f, .5f), 100, 0, SpriteMeshType.FullRect);
            frames[i].name = "victory_cat_" + i.ToString("D2");
        }
        return frames[index];
    }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public const int FrameCount = 16;
```
```csharp
public const float Duration = 1.92f;
```
```csharp
static readonly float[] durations = { .30f, .10f, .10f, .18f, .08f, .08f, .08f, .12f, .10f, .08f, .08f, .08f, .10f, .10f, .14f, .20f };
```
```csharp
static readonly Sprite[] frames = new Sprite[FrameCount];
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
