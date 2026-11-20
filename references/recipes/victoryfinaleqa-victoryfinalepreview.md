# VictoryFinalePreview · VictoryFinaleQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/VictoryFinaleQA.cs`
文件 SHA-256：`73e3cc29e67ccbd1045018477ebcd57dc6096f11103bc9cfb276457bd9d8243c`
[原始源码快照](../source/Assets/Scripts/VictoryFinaleQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L50

方法 SHA-256：`4cb77232c5754c8830942a11c8bf1b8b30770b7d13844ca516d860d653146a41`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryFinalePreview.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();int n=Array.IndexOf(args,"--victory-preview");int moves=n>=0&&n+1<args.Length?Mathf.Max(0,int.Parse(args[n+1])):5;yield return new WaitForSecondsRealtime(3);var app=FindFirstObjectByType<AppUI>();app.QAStartLevel(0);yield return app.QAWaitForLevel();for(int i=0;i<90;i++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.025f);if(i>8&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}app.Session.QAIgnoreFocusLoss=true;app.Session.SetPaused(false);yield return new WaitForSecondsRealtime(1);app.Session.TryShoot(Vector2.up);yield return new WaitForSecondsRealtime(.55f);app.Session.QAFinaleSpecialRound();app.Session.QAFinaleGoal(moves,1800);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
