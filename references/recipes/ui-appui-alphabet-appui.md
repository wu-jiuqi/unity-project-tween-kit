# AppUI · AppUI.Alphabet

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.Alphabet.cs`
文件 SHA-256：`8dff872e69e4ee1fd758aad88c3551cbba0060dc0437d8f5c4b5015b9e792187`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.Alphabet.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### ShowAlphabetIntroductions · L12

方法 SHA-256：`6b26a4f5bad6af6d30507594aadbb59b08e1197d8e954ff786cd76936fec3300`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.ShowAlphabetIntroductions(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:183` · `AppUI.StartPreparedLevel` → `ShowAlphabetIntroductions(generation)`

```csharp
IEnumerator ShowAlphabetIntroductions(int generation){yield return ShowOpeningIntroduction(generation);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
AlphabetIntroductionView alphabetIntroduction;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
