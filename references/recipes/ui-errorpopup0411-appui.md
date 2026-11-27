# AppUI · ErrorPopup0411

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/ErrorPopup0411.cs`
文件 SHA-256：`ed26afddece5e3058f53df71deb155e865f9387408d65c60ecced6926da340e9`
[原始源码快照](../source/Assets/Scripts/UI/ErrorPopup0411.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### OpenErrorPopup0411 · L5

方法 SHA-256：`56e379e31b16d6d63dfcfd27457a8df9eec5104629b685da4fa5ae5311be627d`
时间来源：SetUpdate(true)
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.AppUI.OpenErrorPopup0411(string, string, System.Action, System.Action)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.ContentLoading.cs:47` · `AppUI.PrepareOutcomeRoutine` → `OpenErrorPopup0411("网络连接中断","本局仍可继续结算，请重试连接。",()=>{RemoveModal(false);PrepareOutcomeContent(win);},()=>{RemoveModal(false);CancelLoadingToHome();})`
- `Assets/Scripts/UI/AppUI.ContentLoading.cs:59` · `AppUI.RunLevelLoad` → `OpenErrorPopup0411("暂时未能进入关卡",operation.TimedOut?"等待时间较长，请检查网络。\n已准备的资源会保留，可重试或返回花园。":"本关准备未完成。\n请重试，或返回花园。",()=>BeginLevel(index,tutorialMode,skipOnboarding,trial),CancelLoadingToHome)`

```csharp
void OpenErrorPopup0411(string title,string message,Action retry,Action home){
  RemoveModal(false);modal=UICraft.Root(overlayRoot,"Modal",0,0,1080,1700);
  var view=modal.gameObject.AddComponent<ErrorPopup0411>();view.Build(this,title,message,()=>CloseErrorPopup0411(retry),()=>CloseErrorPopup0411(home));
  modalMotion=DOTween.Sequence().SetUpdate(true).SetId(modal);view.Visual.localScale=Vector3.one*.96f;view.Group.alpha=0;
  modalMotion.Append(view.Visual.DOScale(Vector3.one,.18f).SetEase(Ease.OutCubic));modalMotion.Join(view.Group.DOFade(1,.18f).SetEase(Ease.OutCubic));SoundService.Play("whoosh");
 }
```

### CloseErrorPopup0411 · L11

方法 SHA-256：`7d4a6e8d8d7a69cc7c49fe9d28ede25a599c64d2eb9239d230869b0da616be37`
时间来源：SetUpdate(true)
曲线证据：Ease.InCubic
解析符号：`BounceGarden.AppUI.CloseErrorPopup0411(System.Action)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/ErrorPopup0411.cs:7` · `AppUI.OpenErrorPopup0411` → `CloseErrorPopup0411(retry)`
- `Assets/Scripts/UI/ErrorPopup0411.cs:7` · `AppUI.OpenErrorPopup0411` → `CloseErrorPopup0411(home)`

```csharp
void CloseErrorPopup0411(Action action){var view=QAErrorPopup0411;if(view==null||!view.Group.blocksRaycasts)return;view.Group.blocksRaycasts=false;var old=modal;modalMotion?.Kill(false);modalMotion=DOTween.Sequence().SetUpdate(true).SetId(old);modalMotion.Append(view.Visual.DOScale(view.Visual.localScale*.94f,.14f).SetEase(Ease.InCubic));modalMotion.Join(view.Group.DOFade(0,.14f).SetEase(Ease.InCubic));modalMotion.OnComplete(()=>{if(modal!=old)return;modalMotion=null;action?.Invoke();});}
```

## 对象层级和素材入口

- L6：`UICraft.Root(overlayRoot,"Modal",0,0,1080,1700)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
