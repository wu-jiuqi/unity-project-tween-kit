# AppUI · AppUI.SettingsC

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.SettingsC.cs`
文件 SHA-256：`bbeaa573447793fb13640d7f6b77ae72492141fad102f203f94de8ba48c5ba1a`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.SettingsC.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### LayoutSettingsSheetC · L30

方法 SHA-256：`4adfff2ec6e7474fba71eee8806d60e9e22773b14ce162973413b1a58726cbe8`
时间来源：SetUpdate(true)
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.AppUI.LayoutSettingsSheetC(BounceGarden.CSettingsSheet, bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.SettingsC.cs:48` · `CSettingsSheet.LateUpdate` → `app.LayoutSettingsSheetC(this,false)`

```csharp
internal void LayoutSettingsSheetC(CSettingsSheet sheet,bool animate){
  if(sheet==null||modal==null||sheet.transform!=modal)return;var group=modal.GetComponent<CanvasGroup>();if(group==null||!group.blocksRaycasts)return;
  var pixels=MenuAvoidanceViewport;var safe=AlphabetSafeRect;if(WeChatLayoutMetrics.TryGetBlockedRect(pixels,out var blocked))safe.yMax=Mathf.Min(safe.yMax,blocked.yMin-12);
  float cw=canvasRoot.rect.width,ch=canvasRoot.rect.height,availableW=safe.width/Mathf.Max(1,pixels.x)*cw,availableH=safe.height/Mathf.Max(1,pixels.y)*ch;
  float fit=Mathf.Min(1,Mathf.Min(Mathf.Max(.1f,availableW-44)/1000,Mathf.Max(.1f,availableH-70)/sheet.Height));
  var parent=(RectTransform)modal.parent;var basis=new Vector3(canvasRoot.lossyScale.x/parent.lossyScale.x,canvasRoot.lossyScale.y/parent.lossyScale.y,1);var target=basis*fit;
  var center=new Vector3((safe.center.x/Mathf.Max(1,pixels.x)-.5f)*cw,(safe.yMin/Mathf.Max(1,pixels.y)-.5f)*ch+30+sheet.Height*fit*.5f,0);
  modal.anchoredPosition=(Vector2)parent.InverseTransformPoint(canvasRoot.TransformPoint(center));sheet.Remember(pixels,AlphabetSafeRect,parent.lossyScale);
  if(animate){modalMotion?.Kill(false);modal.localScale=target*.96f;group.alpha=0;modalMotion=DOTween.Sequence().SetUpdate(true).SetId(modal);modalMotion.Append(modal.DOScale(target,.18f).SetEase(Ease.OutCubic));modalMotion.Join(group.DOFade(1,.18f));}
  else{modalMotion?.Kill(false);modal.localScale=target;group.alpha=1;}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
