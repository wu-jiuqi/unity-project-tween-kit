# BossBody · BossBody

状态：`mixed-physics-presentation-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/BossBody.cs`
文件 SHA-256：`c107d42693602e802e69905624fdba294fa88ed7249b73bdb9171391396a5b41`
[原始源码快照](../source/Assets/Scripts/World/BossBody.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L70

方法 SHA-256：`2d88af71a745db1c5f7c0779d9cae77b3570b9939ce89b96b20642a8a2704cac`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BossBody.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update()
        {
            if (owner == null || owner.IsPaused || !owner.IsPlaying) return;
            age += Time.deltaTime;
            float fps = AnimationRow == 0 ? 8 : AnimationRow == 1 ? 18 : AnimationRow == 2 ? 8 : 6;
            int f = Mathf.FloorToInt(age * fps);
            if (AnimationRow == 0) f %= 6;
            else if (f >= 6 && AnimationRow != 3) { AnimationRow = 0; age = 0; f = 0; }
            AnimationFrame = Mathf.Min(5, f); face.sprite = BossArt.Frame(AnimationRow, AnimationFrame);
            bool preview = owner.BossPhase > 1 && owner.BossShotCount >= 2 && !owner.BossActing && !owner.BossDefeating;
            ghost.gameObject.SetActive(preview);
            if (preview) ghost.transform.parent.position = owner.BossNextPosition;
        }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public const float ActionDuration = .75f;
```
```csharp
public const float DefeatDuration = 1.15f;
```
```csharp
GameSession owner;
```
```csharp
SpriteRenderer face, ghost;
```
```csharp
BoxCollider2D hitbox;
```
```csharp
float age;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
