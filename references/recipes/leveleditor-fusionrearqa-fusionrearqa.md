# FusionRearQA · FusionRearQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LevelEditor/FusionRearQA.cs`
文件 SHA-256：`c427790d0ec8531489216dc8f266b30e1868a0c261dddf8fb6b3dd3b8a6068a6`
[原始源码快照](../source/Assets/Scripts/LevelEditor/FusionRearQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Run · L9

方法 SHA-256：`6fd448492a34489b64e13e1fbc926af67ace6a74a3eb33ac44d717a2a5666c24`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.FusionRearQA.Run(BounceGarden.GameSession, System.Action<string, bool, string>)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/FusionQA.cs:111` · `FusionQA.Start` → `FusionRearQA.Run(session, Check)`

```csharp
public static IEnumerator Run(GameSession game,Action<string,bool,string> check){
#if BOUNCE_LEVEL_EDITOR
  for(int variant=0;variant<3;variant++){
   try{Scenario(game,check,variant);}catch(Exception e){check("rear fixture executes",false,e.ToString());}
   game.StopLevel();yield return null;
  }
#endif
  yield break;
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
