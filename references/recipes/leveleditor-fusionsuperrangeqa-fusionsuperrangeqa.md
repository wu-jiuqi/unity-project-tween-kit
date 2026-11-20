# FusionSuperRangeQA · FusionSuperRangeQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LevelEditor/FusionSuperRangeQA.cs`
文件 SHA-256：`6341318af2a0d7d5040ee5296353504a545ac08b701bd889a78264c06b47ee97`
[原始源码快照](../source/Assets/Scripts/LevelEditor/FusionSuperRangeQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Run · L11

方法 SHA-256：`5179d7ba5b789d6dcfa36005d0797f48e48aa18f844530e4402f96a404471a3d`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.FusionSuperRangeQA.Run(BounceGarden.GameSession, string, System.Action<string, bool, string>)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/FusionQA.cs:108` · `FusionQA.Start` → `FusionSuperRangeQA.Run(session, output, Check)`

```csharp
public static IEnumerator Run(GameSession game, string output, Action<string, bool, string> check) {
            try { Data(output, check); } catch (Exception e) { Assert(check, "authoring executes", false, e.ToString()); }
            foreach (string variant in new[] { "finite3", "unlimited3", "legacy_missing9", "useful_finite3", "useful_unlimited3", "ordinary_line" }) {
                try {
                    if (variant.StartsWith("useful_", StringComparison.Ordinal)) Useful(game, check, variant == "useful_finite3");
                    else if (variant == "ordinary_line") OrdinaryLine(game, check);
                    else Collision(game, check, variant);
                } catch (Exception e) { Assert(check, variant + " executes", false, e.ToString()); }
                game.StopLevel(); game.BoosterProfile = null; yield return null;
            }
        }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
