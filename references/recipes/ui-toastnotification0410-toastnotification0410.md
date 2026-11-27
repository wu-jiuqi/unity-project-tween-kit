# ToastNotification0410 · ToastNotification0410

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/ToastNotification0410.cs`
文件 SHA-256：`1eee26c884136c7f988023de63285dff7ac69abd6b6e824460497e1dc49f5676`
[原始源码快照](../source/Assets/Scripts/UI/ToastNotification0410.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `ToastMotion` / `toast`：手工抽取纯表现接口；保留时序，业务事件由宿主输入

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### Show · L39

方法 SHA-256：`a786bb58226668dbbc471e6ff127f69ef5180e6bd663c607fe8a68cd710eb749`
时间来源：SetUpdate(true), Time.unscaledTime
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.ToastNotification0410.Show(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:242` · `AppUI.Toast` → `toast.Show(message)`
- `Assets/Scripts/UI/ToastNotification0410.cs:61` · `ToastNotification0410.Exit` → `Show(next)`

```csharp
public void Show(string message){
  if(string.IsNullOrWhiteSpace(message))return;
  if(Phase!="idle"){
   if(message==Message&&Phase!="exiting"){pending=null;holdUntil=Time.unscaledTime+Hold(Kind);return;}
   pending=message;if(Phase!="exiting")Exit();return;
  }
  Message=message;Kind=Classify(message);label.text=message;
  icon.sprite=Kind==ToastKind0410.Info?CSettingsArt.Get("decor_paw"):ToastArt0410.Get(Kind==ToastKind0410.Success?"icon-saved":Kind==ToastKind0410.Coins?"icon-coins":"icon-retry");
  // Keep long existing business messages complete instead of baking/shortening them.
  var preferred=label.GetPreferredValues(message,630,1000);Height=Mathf.Clamp(preferred.y+50,180,400);
  root.sizeDelta=visual.sizeDelta=new Vector2(900,Height);board.rectTransform.sizeDelta=new Vector2(900,Height);
  board.pixelsPerUnitMultiplier=board.sprite.rect.height/Height;label.rectTransform.sizeDelta=new Vector2(630,Height-40);
  owner.LayoutToast0410(this);transform.SetAsLastSibling();Phase="entering";visual.localScale=Vector3.one*.96f;group.alpha=0;
  motion=DOTween.Sequence().SetUpdate(true).SetId(this);
  motion.Append(visual.DOScale(Vector3.one,.18f).SetEase(Ease.OutCubic));motion.Join(group.DOFade(1,.18f).SetEase(Ease.OutCubic));
  motion.OnComplete(()=>{motion=null;Phase="holding";holdUntil=Time.unscaledTime+Hold(Kind);});
 }
```

### Exit · L56

方法 SHA-256：`9c56a392e24793145df3c188bf106879a7726a8c7eb38fed3d2ac147f9ee8030`
时间来源：SetUpdate(true)
曲线证据：Ease.InCubic
解析符号：`BounceGarden.ToastNotification0410.Exit()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/ToastNotification0410.cs:43` · `ToastNotification0410.Show` → `Exit()`
- `Assets/Scripts/UI/ToastNotification0410.cs:64` · `ToastNotification0410.Update` → `Exit()`

```csharp
void Exit(){
  motion?.Kill(false);motion=null;Phase="exiting";
  // Vector multiplication also remains correct when the entry tween is interrupted.
  motion=DOTween.Sequence().SetUpdate(true).SetId(this);
  motion.Append(visual.DOScale(visual.localScale*.94f,.14f).SetEase(Ease.InCubic));motion.Join(group.DOFade(0,.14f).SetEase(Ease.InCubic));
  motion.OnComplete(()=>{motion=null;var next=pending;pending=null;Phase="idle";if(next!=null)Show(next);else Dismiss();});
 }
```

### Update · L64

方法 SHA-256：`957a096776a2eab3b911b80def1464e1ea60e7ebc42af5de94b43836522f70ca`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ToastNotification0410.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(Phase=="holding"&&Time.unscaledTime>=holdUntil)Exit();}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){pending=null;motion?.Kill(false);motion=null;Phase="idle";if(group!=null)group.alpha=0;}
```

```csharp
void OnDestroy(){motion?.Kill(false);}
```

```csharp
void OnApplicationFocus(bool focus){if(!focus&&initialized)Dismiss();}
```

```csharp
void OnApplicationPause(bool paused){if(paused&&initialized)Dismiss();}
```

## 源字段与依赖

```csharp
AppUI owner;
```
```csharp
RectTransform root,visual;
```
```csharp
CanvasGroup group;
```
```csharp
Image board,icon;
```
```csharp
TMP_Text label;
```
```csharp
Sequence motion;
```
```csharp
string pending;
```
```csharp
float holdUntil;
```
```csharp
bool initialized;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
