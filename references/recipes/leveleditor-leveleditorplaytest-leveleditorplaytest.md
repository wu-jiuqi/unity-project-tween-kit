# LevelEditorPlaytest · LevelEditorPlaytest

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LevelEditor/LevelEditorPlaytest.cs`
文件 SHA-256：`7e8c5720ca2f69fcfc8b2f8e609f9b4136eb08ef2de4a59e69c50d8c1202e788`
[原始源码快照](../source/Assets/Scripts/LevelEditor/LevelEditorPlaytest.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L142

方法 SHA-256：`1b3c6e3a522e42ea20c9c0096563ccb2216638623c12bf5f87d40c5d5c9fac04`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.LevelEditorPlaytest.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(Session!=null&&Session.IsPlaying&&!Session.IsPaused)ElapsedSeconds+=Time.deltaTime;}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public void Stop(){
  if(Session!=null){Session.SetAim(null);Session.StopLevel();Session.BoosterProfile=null;}
  Profile=null;SoundService.CancelTransient();
 }
```

```csharp
public void SetPaused(bool paused){if(Session!=null)Session.SetPaused(paused);}
```

```csharp
void OnDestroy(){if(Session!=null){Session.Outcome-=OnOutcome;Session.StopLevel();Session.BoosterProfile=null;}Profile=null;}
```

## 源字段与依赖

```csharp
int lastIndex,lastRuntimeIndex;
```
```csharp
string lastJson,lastEditorLevelId,lastDisplayName;
```
```csharp
bool completed;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
