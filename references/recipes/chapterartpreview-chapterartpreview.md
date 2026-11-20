# ChapterArtPreview · ChapterArtPreview

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChapterArtPreview.cs`
文件 SHA-256：`3066decf6897c59763ebfb2967f7040973292e95c48263fef49e5fd877e7d7eb`
[原始源码快照](../source/Assets/Scripts/ChapterArtPreview.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L7

方法 SHA-256：`7750baf0b96ed49ff614c1afb6f4df5f1ceb8cd2c05cc6f33a7eb9ee305a82d7`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChapterArtPreview.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){yield return new WaitForSecondsRealtime(1);var app=FindFirstObjectByType<AppUI>();var args=Environment.GetCommandLineArgs();int n=Array.IndexOf(args,"--chapter-preview");int progress=n+1<args.Length&&args[n+1]=="picnic"?34:0;for(int i=0;i<progress;i++)app.Profile.CommitAlphabetWin("chapter-art-preview-"+i,i);app.QARefreshChapterHome();Debug.Log("CHAPTER_ART_PREVIEW memoryOnly=true currentLevel="+(progress+1));}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
