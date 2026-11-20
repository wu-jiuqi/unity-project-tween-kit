# GameSession · BossLevel

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/BossLevel.cs`
文件 SHA-256：`811683e3441febdade1bb986d2dac9c4160b567bc7c6148eb7327f42e184b890`
[原始源码快照](../source/Assets/Scripts/Core/BossLevel.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### TickBoss · L64

方法 SHA-256：`4e74d1802896cffe97a52d221322e10e1a513da91cbea60d87227f7cd55b7be6`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameSession.TickBoss(float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
void TickBoss(float dt)
        {
            if (Boss == null) return;
            bossComboClock += dt;
            if (bossComboClock > 2f && BossCombo != 0) { BossCombo = 0; changed = true; }
            if (bossDefeatPending)
            {
                bossDefeatTime += dt;
                if (bossDefeatTime >= BossBody.DefeatDuration)
                {
                    rules.CompleteTargetStage();
                    // Leave the flag set until outcome; no pending balls can spawn into the outro.
                }
                return;
            }
            if (bossActionTime > 0)
            {
                bossActionTime = Mathf.Max(0, bossActionTime - dt);
                float t = 1 - bossActionTime / BossBody.ActionDuration;
                Boss.transform.position = Vector2.Lerp(bossMoveFrom, bossMoveTo, t * t * (3 - 2 * t));
                if (bossActionTime == 0)
                {
                    Boss.transform.position = bossMoveTo;
                    if (BossPhase == 3) RefillBossBricks();
                    bossShots = 0; bossActions++; changed = true;
                    Physics2D.SyncTransforms();
                }
                return;
            }
            if (bossShots >= 3 && ActiveBalls == 0 && PendingBurstBalls == 0 && Shots > 0)
            {
                bossMoveFrom = Boss.transform.position; bossMoveTo = BossNextPosition;
                bossActionTime = BossBody.ActionDuration;
                SoundService.Play("boss_action");Boss.Play(2); SetAim(null); changed = true;
            }
        }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
int bossId, bossShots, bossActions;
```
```csharp
float bossActionTime, bossDefeatTime;
```
```csharp
Vector2 bossMoveFrom, bossMoveTo;
```
```csharp
bool bossDefeatPending;
```
```csharp
readonly List<BrickBody> bossRefills = new List<BrickBody>();
```
```csharp
float bossComboClock;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
