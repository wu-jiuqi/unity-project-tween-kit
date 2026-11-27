# OpeningStory049Art · OpeningStory049

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/OpeningStory049.cs`
文件 SHA-256：`52543b2c315cd7de5ff960db59eb45811dcf5c8ce34d3b98772b2befd2ceca55`
[原始源码快照](../source/Assets/Scripts/UI/OpeningStory049.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Create · L68

方法 SHA-256：`ea52d5ef7f59a9706bdf1e50495df685ea50ed6b810fb46e15ae79252a167f7a`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OpeningStory049Art.Create(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/OpeningStory049.cs:66` · `OpeningStory049Art.Material` → `Create(.4f)`
- `Assets/Scripts/UI/OpeningStory049.cs:66` · `OpeningStory049Art.Material` → `Create(.2f)`

```csharp
static Material Create(float cutoff){var shader=Resources.Load<Shader>("OpeningStory049UI");if(shader==null)throw new InvalidOperationException("Missing introduction UI alpha shader");var mat=new Material(shader){name="Introduction authored alpha "+cutoff,hideFlags=HideFlags.DontSave};mat.SetFloat("_Cutoff",cutoff);return mat;}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
static Material yarn,jelly;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
