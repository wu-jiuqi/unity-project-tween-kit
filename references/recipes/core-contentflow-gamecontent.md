# GameContent · ContentFlow

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/ContentFlow.cs`
文件 SHA-256：`fb8d6f405ac79fbbae515606b3280471121ce937faff4f001b2ed22af88d5bd2`
[原始源码快照](../source/Assets/Scripts/Core/ContentFlow.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Level · L9

方法 SHA-256：`a11b412112e64606c8d8a2887073bcc18b6acb12615162db51e3d84b8b9a767f`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameContent.Level`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public static Func<int,IEnumerator> Level;
```

### Prefetch · L10

方法 SHA-256：`ad021a9117a7e4eeedcbc9aa1964ec86c9cb500052fe3e0d0af2c91dabacb438`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameContent.Prefetch`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public static Func<int,IEnumerator> Prefetch;
```

### Page · L11

方法 SHA-256：`73854a4a7e6578f5f5136e0343e260d212558c42810617796a4e3b9688099295`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameContent.Page`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public static Func<string,IEnumerator> Page;
```

### PrepareLevel · L16

方法 SHA-256：`3536774145f4c8bc386cae1c50f34cf93d070cfc1b7b78073128e8b3d070e017`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameContent.PrepareLevel(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:166` · `AppUI.StartPreparedLevel` → `GameContent.PrepareLevel(currentLevel)`

```csharp
public static IEnumerator PrepareLevel(int level){if(Level!=null)yield return Level(level);if(!string.IsNullOrEmpty(Error?.Invoke()))throw new InvalidOperationException(Error());}
```

### PrefetchLevel · L17

方法 SHA-256：`9eead87aed673bd9098b444bee9c1b58f8f8a0ed75d7c2c504b0ef14d7acb57c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameContent.PrefetchLevel(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.ContentLoading.cs:21` · `AppUI.PrefetchFollowingLevel` → `GameContent.PrefetchLevel(index+1)`

```csharp
public static IEnumerator PrefetchLevel(int level){if(Prefetch!=null)yield return Prefetch(level);}
```

### PreparePage · L18

方法 SHA-256：`d1f8431191b2636f0fa180dda089a9a5f66a12ed8dd4d41fcf556f4fa31ac20a`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameContent.PreparePage(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.ContentLoading.cs:24` · `AppUI.PrefetchFollowingLevel` → `GameContent.PreparePage("chapters")`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:26` · `AppUI.PrefetchFollowingLevel` → `GameContent.PreparePage("result-lose")`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:42` · `AppUI.PrepareOutcomeRoutine` → `GameContent.PreparePage(page)`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:66` · `AppUI.PrepareMenuContentRoutine` → `GameContent.PreparePage(page)`
- `Assets/Scripts/UI/AppUI.LeagueFlow.cs:30` · `AppUI.PrepareLeagueFlowContent` → `GameContent.PreparePage("ranking")`
- `Assets/Scripts/UI/JellyLogoIntro.cs:99` · `JellyLogoIntro.PrepareHome` → `GameContent.PreparePage("home")`

```csharp
public static IEnumerator PreparePage(string page){if(Page!=null)yield return Page(page);if(!string.IsNullOrEmpty(Error?.Invoke()))throw new InvalidOperationException(Error());}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public static Func<int,IEnumerator> Level;
```
```csharp
public static Func<int,IEnumerator> Prefetch;
```
```csharp
public static Func<string,IEnumerator> Page;
```
```csharp
public static Func<string,bool> Ready;
```
```csharp
public static Func<string> Error;
```
```csharp
public static Func<long> Progress;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
