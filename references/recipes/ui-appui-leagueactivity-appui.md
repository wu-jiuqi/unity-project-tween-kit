# AppUI · AppUI.LeagueActivity

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.LeagueActivity.cs`
文件 SHA-256：`48bed91160be3ae6fead891eb0929fa7e5a043e13ba8d8c182d5ca6f95b11376`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.LeagueActivity.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### TickLeagueActivity · L6

方法 SHA-256：`6f0b733852a7bc8cff09599b2c0b8f0fc7b32d6b9a08ce89c8ad0de3c887fab5`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.TickLeagueActivity()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:92` · `AppUI.Update` → `TickLeagueActivity()`

```csharp
void TickLeagueActivity(){
  if(Profile==null||Session==null)return;
  float dt=Mathf.Min(Time.unscaledDeltaTime,.25f);
  if(Input.touchCount>0||Input.GetMouseButton(0)||Input.anyKeyDown||aiming)leagueIdle=0;else leagueIdle+=dt;
  bool battle=leagueFocused&&playing&&!busy&&results==null&&modal==null&&Session.IsPlaying&&!Session.IsPaused;
  bool active=battle&&(Session.ActiveBalls>0||leagueIdle<45);
  if(battle)leagueMenuTime=0;else leagueMenuTime+=dt;
  if(leagueFocused&&!playing&&modal==null&&leagueMenuTime<20&&leagueIdle<20)active=true;
  if(active)leaguePending+=dt;
  if(leaguePending>=5)FlushLeagueActivity(false);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnApplicationQuit(){FlushLeagueActivity(true);}
```

## 源字段与依赖

```csharp
bool leagueFocused=true;
```
```csharp
float leagueIdle,leagueMenuTime;
```
```csharp
double leaguePending;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
