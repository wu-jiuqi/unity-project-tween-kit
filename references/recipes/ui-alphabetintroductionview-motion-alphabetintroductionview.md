# AlphabetIntroductionView · AlphabetIntroductionView.Motion

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AlphabetIntroductionView.Motion.cs`
文件 SHA-256：`3539dc570f10b715ec08fbabe8789cb64571564ec665f5197029b2ac810c5c81`
[原始源码快照](../source/Assets/Scripts/UI/AlphabetIntroductionView.Motion.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### AdvanceMotion · L24

方法 SHA-256：`e3415184f453f91a74cc9e7d6417d291f4cb5d2f50b2625c55abd1aa238d19de`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AlphabetIntroductionView.AdvanceMotion()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AlphabetIntroductionView.cs:42` · `AlphabetIntroductionView.Advance` → `AdvanceMotion()`

```csharp
bool AdvanceMotion(){if(motion==null||motionSecond||page.id!=1&&page.id!=4)return false;motionSecond=true;started=Time.unscaledTime;SetMotionLesson();return true;}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
TutorialMotionBoard motion;
```
```csharp
bool motionSecond;
```
```csharp
TMPro.TMP_Text motionHeading,motionCaption,motionRule,motionNext;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
