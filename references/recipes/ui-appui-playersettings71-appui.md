# AppUI · AppUI.PlayerSettings71

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.PlayerSettings71.cs`
文件 SHA-256：`4777de407e634ec748d4adc4b27c38f77665d29eb0feab786c8cf05f3e8fb66f`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.PlayerSettings71.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### FitSettingsModal77 · L48

方法 SHA-256：`aaed407da01e40dcce7b412951df72c0f87edfa7f50e91e61756df7a62267f98`
时间来源：SetUpdate(true)
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.AppUI.FitSettingsModal77(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.AudioSettings.cs:20` · `AppUI.OpenAudioSettings` → `FitSettingsModal77(height)`
- `Assets/Scripts/UI/AppUI.BalanceStatistics.cs:46` · `AppUI.DrawBalanceStatistics` → `FitSettingsModal77(1540)`
- `Assets/Scripts/UI/AppUI.BalanceStatistics.cs:55` · `AppUI.ShowBalanceRound` → `FitSettingsModal77(1540)`
- `Assets/Scripts/UI/AppUI.BalanceStatistics.cs:64` · `AppUI.ShowBalanceExport` → `FitSettingsModal77(1120)`
- `Assets/Scripts/UI/AppUI.PlayerSettings71.cs:33` · `AppUI.BuildPlayerSettings` → `FitSettingsModal77(height)`
- `Assets/Scripts/UI/AppUI.PlayerSettings71.cs:63` · `AppUI.OpenPlayerHelp71` → `FitSettingsModal77(height)`
- `Assets/Scripts/UI/AppUI.PlayerSettings71.cs:69` · `AppUI.OpenPlayerAbout71` → `FitSettingsModal77(height)`
- `Assets/Scripts/UI/AppUI.PlayerSettings71.cs:77` · `AppUI.ConfirmLeaveRound71` → `FitSettingsModal77(height)`
- `Assets/Scripts/UI/AppUI.PlayerStrategy.cs:29` · `AppUI.ShowStrategyShots` → `FitSettingsModal77(1540)`
- `Assets/Scripts/UI/AppUI.PlayerStrategy.cs:39` · `AppUI.ShowStrategyTestLevels` → `FitSettingsModal77(1540)`

```csharp
void FitSettingsModal77(float height){
  if(modal==null)return;var sheet=modal.GetComponent<CSettingsSheet>();if(sheet!=null){LayoutSettingsSheetC(sheet,true);return;}
  modalMotion?.Kill(false);float scale=Mathf.Min(1,Mathf.Max(.1f,(menuHeight-100)/height));var group=modal.GetComponent<CanvasGroup>();group.alpha=0;modal.localScale=Vector3.one*scale*.96f;
  var dimmer=modal.Find("Dimmer") as RectTransform;if(dimmer!=null)dimmer.sizeDelta=new Vector2(1200/scale,(menuHeight+200)/scale);
  modalMotion=DOTween.Sequence().SetUpdate(true).SetId(modal);modalMotion.Append(modal.DOScale(scale,.18f).SetEase(Ease.OutCubic));modalMotion.Join(DOTween.To(()=>group.alpha,value=>group.alpha=value,1,.18f));
 }
```

## 对象层级和素材入口

- L51：`modal.Find("Dimmer")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
