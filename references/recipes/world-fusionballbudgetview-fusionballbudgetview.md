# FusionBallBudgetView · FusionBallBudgetView

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/FusionBallBudgetView.cs`
文件 SHA-256：`7275f76c815ab6c098697795aeaafcd8ef1b6f524ec9347a2b6db1ad722d0380`
[原始源码快照](../source/Assets/Scripts/World/FusionBallBudgetView.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Refresh · L14

方法 SHA-256：`f60594d95aca819fdb0bd676defc05bcfce5da132f067f07bcb02f79dfdf33fd`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.FusionBallBudgetView.Refresh()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/FusionBallBudgetView.cs:11` · `FusionBallBudgetView.Initialize` → `Refresh()`
- `Assets/Scripts/World/FusionBallBudgetView.cs:13` · `FusionBallBudgetView.LateUpdate` → `Refresh()`

```csharp
void Refresh(){
  int charges=owner!=null&&ball!=null&&ball.Active?owner.FusionBallCharges(ball):0;
  for(int i=0;i<pips.Length;i++){if(pips[i]==null)continue;pips[i].enabled=i<charges;if(i>=charges)continue;float angle=(90+(i-(charges-1)*.5f)*24)*Mathf.Deg2Rad;pips[i].transform.localPosition=new Vector3(Mathf.Cos(angle)*.23f,Mathf.Sin(angle)*.23f,0);pips[i].color=StreakTrials.Color(ball.StreakPower);}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
GameSession owner;
```
```csharp
BallBody ball;
```
```csharp
readonly SpriteRenderer[] pips=new SpriteRenderer[10];
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
