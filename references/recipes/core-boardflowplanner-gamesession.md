# GameSession · BoardFlowPlanner

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/BoardFlowPlanner.cs`
文件 SHA-256：`1555cb813a5f808ce108bf6ce9e6707118fca21bf5dfff50922dc80040149300`
[原始源码快照](../source/Assets/Scripts/Core/BoardFlowPlanner.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### FlowRelativeCollision · L39

方法 SHA-256：`be887890ca9ca50180cfc119165a14ba7b7322407e22c1e9e094904b08a1170d`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameSession.FlowRelativeCollision(UnityEngine.Vector2, UnityEngine.Vector2, float, float, BounceGarden.GameSession.GridMotion, float, float, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/BoardFlowPlanner.cs:34` · `GameSession.FlowShortcutSafe` → `FlowRelativeCollision(from,to,start,end,other,cursor,a,half)`
- `Assets/Scripts/Core/BoardFlowPlanner.cs:34` · `GameSession.FlowShortcutSafe` → `FlowRelativeCollision(from,to,start,end,other,cursor,b,half)`
- `Assets/Scripts/Core/BoardFlowPlanner.cs:35` · `GameSession.FlowShortcutSafe` → `FlowRelativeCollision(from,to,start,end,other,cursor,end,half)`

```csharp
static bool FlowRelativeCollision(Vector2 from,Vector2 to,float start,float end,GridMotion other,float a,float b,float half){var p=Vector2.Lerp(from,to,Mathf.InverseLerp(start,end,a))-FlowPositionAt(other,a);var q=Vector2.Lerp(from,to,Mathf.InverseLerp(start,end,b))-FlowPositionAt(other,b);return FlowSegmentBox(p,q,Vector2.zero,half);}
```

### FlowPositionAt · L40

方法 SHA-256：`ea76d3f6164f745528aca90636bcaf89be07c7010bb07ec0033907efc3c627f4`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameSession.FlowPositionAt(BounceGarden.GameSession.GridMotion, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/BoardFlowPlanner.cs:39` · `GameSession.FlowRelativeCollision` → `FlowPositionAt(other,a)`
- `Assets/Scripts/Core/BoardFlowPlanner.cs:39` · `GameSession.FlowRelativeCollision` → `FlowPositionAt(other,b)`
- `Assets/Scripts/Core/BoardFlowPlanner.cs:41` · `GameSession.AdvanceBoardFlow` → `FlowPositionAt(move,simulationClock)`

```csharp
static Vector2 FlowPositionAt(GridMotion route,float time){Vector2 from=route.From;for(int i=0;i<route.Path.Count;i++){if(time<route.PathStarts[i])return from;if(time<route.PathEnds[i])return Vector2.Lerp(from,route.Path[i],Mathf.InverseLerp(route.PathStarts[i],route.PathEnds[i],time));from=route.Path[i];}return from;}
```

## 对象层级和素材入口

- L39：`FlowSegmentBox(p,q,Vector2.zero,half)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
const float BoardFlowSpeed=8.8f;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
