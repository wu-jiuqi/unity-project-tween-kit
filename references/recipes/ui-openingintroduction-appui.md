# AppUI · OpeningIntroduction

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/OpeningIntroduction.cs`
文件 SHA-256：`f8d81f47ee83376de12872c067c516ee3c72d2a68c788c572af0f6530db7800b`
[原始源码快照](../source/Assets/Scripts/UI/OpeningIntroduction.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### ShowOpeningIntroduction · L27

方法 SHA-256：`48dd18ddb1cd36cf2d08090e3fec9dbdfb27ef167e0493d725f7bc813ddae06f`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.ShowOpeningIntroduction(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.Alphabet.cs:12` · `AppUI.ShowAlphabetIntroductions` → `ShowOpeningIntroduction(generation)`

```csharp
IEnumerator ShowOpeningIntroduction(int generation){
  foreach(int index in OpeningIntroduction.LessonKeys(currentLevel)){
  if(Profile.HasOpeningIntro(index))continue;
  bool complete=false;var root=UICraft.Root(canvasRoot,"Opening campaign introduction "+index);alphabetIntroduction=root.gameObject.AddComponent<AlphabetIntroductionView>();alphabetIntroduction.Begin(this,OpeningIntroduction.Page(index),()=>{Profile.MarkOpeningIntro(index);complete=true;});
  while(!complete&&generation==viewGeneration&&alphabetIntroduction!=null)yield return null;
  if(generation!=viewGeneration){CloseAlphabetIntroduction();yield break;}alphabetIntroduction=null;}
 }
```

## 对象层级和素材入口

- L30：`UICraft.Root(canvasRoot,"Opening campaign introduction "+index)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
