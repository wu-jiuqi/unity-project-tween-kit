# FusionDeviceQA · FusionDeviceQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LevelEditor/FusionDeviceQA.cs`
文件 SHA-256：`b840a7838018c3a5b5ee1d255f534180ebb9a50bc16ddc18586be91a70e008f9`
[原始源码快照](../source/Assets/Scripts/LevelEditor/FusionDeviceQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Run · L15

方法 SHA-256：`30d15fb90635cbcb84f3f3cee74c78fa0669c806afe0f5b120857d6c14a7f41c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.FusionDeviceQA.Run(BounceGarden.GameSession, System.Action<string, bool, string>)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/FusionQA.cs:113` · `FusionQA.Start` → `FusionDeviceQA.Run(session, Check)`

```csharp
public static IEnumerator Run(GameSession session, Action<string, bool, string> check)
        {
            var fixtures = new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>[]
            {
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("cannon", Cannon),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("cannon goal retirement", CannonGoalRetirement),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("producer", Producer),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("vine", Vine),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("battery", Battery),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("gate", Gate),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("assistant protection", AssistantProtection),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("assistant retarget", AssistantRetarget),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("assistant locked region", AssistantLockedRegion),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("reward ticket", Reward),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("milestones", Milestones),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("snow shrink snapshot", SnowShrinkSnapshot),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("snow removed snapshot", SnowRemovedSnapshot),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("frozen snow neighbor", FrozenSnowNeighbor),
                new KeyValuePair<string, Action<GameSession, Action<string, bool, string>>>("cocoa one stage", CocoaOneStage)
            };
            foreach (var fixture in fixtures)
            {
                try { fixture.Value(session, check); }
                catch (Exception error) { check("device_rule_fixture " + fixture.Key + " executes", false, error.ToString()); }
                session.StopLevel();
                yield return null; // Complete actual Unity object destruction before reusing the session.
            }
        }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
