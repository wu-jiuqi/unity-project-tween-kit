# ContentLoadOperation · ContentFlow

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/ContentFlow.cs`
文件 SHA-256：`fb8d6f405ac79fbbae515606b3280471121ce937faff4f001b2ed22af88d5bd2`
[原始源码快照](../source/Assets/Scripts/Core/ContentFlow.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Run · L31

方法 SHA-256：`f15d008608626815f21726447a23ca22fa8e236cfd310feea1756288c6865100`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ContentLoadOperation.Run(System.Collections.IEnumerator, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.ContentLoading.cs:21` · `AppUI.PrefetchFollowingLevel` → `operation.Run(GameContent.PrefetchLevel(index+1))`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:24` · `AppUI.PrefetchFollowingLevel` → `operation.Run(GameContent.PreparePage("chapters"))`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:26` · `AppUI.PrefetchFollowingLevel` → `operation.Run(GameContent.PreparePage("result-lose"))`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:42` · `AppUI.PrepareOutcomeRoutine` → `operation.Run(GameContent.PreparePage(page))`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:53` · `AppUI.RunLevelLoad` → `operation.Run(StartPreparedLevel(loading,generation))`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:66` · `AppUI.PrepareMenuContentRoutine` → `operation.Run(GameContent.PreparePage(page))`
- `Assets/Scripts/UI/AppUI.LeagueFlow.cs:30` · `AppUI.PrepareLeagueFlowContent` → `operation.Run(GameContent.PreparePage("ranking"))`
- `Assets/Scripts/UI/JellyLogoIntro.cs:99` · `JellyLogoIntro.PrepareHome` → `preparation.Run(GameContent.PreparePage("home"))`

```csharp
public IEnumerator Run(IEnumerator routine,float timeout=DefaultTimeout){
  var deadline=new ContentLoadDeadline(Time.realtimeSinceStartup,timeout,GameContent.Progress!=null,GameContent.Progress?.Invoke()??0);
  Func<bool> expired=()=>!PreparationComplete&&deadline.Expired(Time.realtimeSinceStartup,GameContent.Progress?.Invoke()??0);
  var stack=new Stack<IEnumerator>();stack.Push(routine);
  try {
   while(stack.Count>0&&!Cancelled){
    if(expired()){TimedOut=true;break;}
    var top=stack.Peek();bool more=false;object next=null;
    try{more=top.MoveNext();if(more)next=top.Current;}catch(Exception error){Failure=error;break;}
    if(!more){stack.Pop();(top as IDisposable)?.Dispose();continue;}
    if(next is IEnumerator nested){stack.Push(nested);continue;}
    if(next is AsyncOperation async){
     while(!async.isDone&&!Cancelled){if(expired()){TimedOut=true;break;}yield return null;}
     if(TimedOut)break;
    }else yield return next;
   }
  }finally{while(stack.Count>0){try{(stack.Pop() as IDisposable)?.Dispose();}catch(Exception error){if(Failure==null)Failure=error;}}}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public bool Cancelled {get;private set;}
```

```csharp
public void Cancel(){Cancelled=true;}
```

## 源字段与依赖

```csharp
public const float DefaultTimeout=45;
```
```csharp
public bool PreparationComplete;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
