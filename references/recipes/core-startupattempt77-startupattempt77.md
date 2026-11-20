# StartupAttempt77 · StartupAttempt77

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/StartupAttempt77.cs`
文件 SHA-256：`d3526b777bd630bc3c0616eda1437c2656646d3789eea8e2036718ab09a7e42a`
[原始源码快照](../source/Assets/Scripts/Core/StartupAttempt77.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Run · L10

方法 SHA-256：`a1490d39e47c1c72da4ba980ae179f9861fbc2026f8c394925f91f7902ccc6bd`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.StartupAttempt77.Run(System.Action<System.Action, System.Action<string>>, float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public IEnumerator Run(Action<Action,Action<string>> begin,float timeout=15){
  float deadline=Time.realtimeSinceStartup+timeout;
  try{
   try{begin(()=>{if(!resolved){resolved=true;Succeeded=true;}},message=>{if(!resolved){resolved=true;Error=message;}});}catch(Exception error){resolved=true;Error=error.Message;}
   while(!resolved&&Time.realtimeSinceStartup<deadline)yield return null;
   if(!resolved){resolved=true;Error="初始化等待超时，请重试。";}
  }finally{resolved=true;}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
bool resolved;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
