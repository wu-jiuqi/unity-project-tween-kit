# AppUI · AppUI.Shop

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.Shop.cs`
文件 SHA-256：`524b27ee4444ddff4e0e47f8edaee9e1c4d9d28fbf5513c2db5601d0494b386f`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.Shop.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `GroupStagger` / `stagger`：保持.98起始比例、.20秒OutCubic、.018逐项延迟；初始scale向量由绑定对象捕获

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### ShopEnter · L13

方法 SHA-256：`4dd8f4509b3b5f95b3de53aae7f7ab5cf4fd29c2c36bc661931650c719f820fe`
时间来源：SetUpdate(true)
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.AppUI.ShopEnter(UnityEngine.RectTransform, int)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
void ShopEnter(RectTransform target,int order=0){target.localScale=Vector3.one*.98f;target.DOScale(1,.20f).SetDelay(order*.018f).SetEase(Ease.OutCubic).SetUpdate(true).SetLink(target.gameObject);}
```

### OpenReferenceModal · L43

方法 SHA-256：`8d41bd727674a29c14295b81830b1257113e421f0e7d16aa8ba97e518f89c64d`
时间来源：SetUpdate(true)
曲线证据：Ease.OutBack
解析符号：`BounceGarden.AppUI.OpenReferenceModal(string, float, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.Shop.cs:35` · `AppUI.OpenShopGuide` → `OpenReferenceModal("喵喵小铺指南",408,500)`
- `Assets/Scripts/UI/AppUI.Shop.cs:45` · `AppUI.OpenDressing` → `OpenReferenceModal("挑选你的装扮")`
- `Assets/Scripts/UI/AppUI.Shop.cs:60` · `AppUI.OpenCosmeticOffer` → `OpenReferenceModal(owned?"我的收藏":"解锁装扮",408,540)`
- `Assets/Scripts/UI/AppUI.Systems.cs:14` · `AppUI.OpenScoreRecords` → `OpenReferenceModal("积分纪录",408,700)`
- `Assets/Scripts/UI/AppUI.Systems.cs:24` · `AppUI.OpenEconomyGuide` → `OpenReferenceModal("金币与道具",408,620)`
- `Assets/Scripts/UI/AppUI.Systems.cs:35` · `AppUI.OpenBoosterOffer` → `OpenReferenceModal("补充道具",408,580)`

```csharp
void OpenReferenceModal(string title,float width=408,float height=750){OpenModal("",UICraft.Blue,width*2.18f,height*2.18f);Clear(modal);float fit=Mathf.Min(1,(menuHeight-100)/(height*2.18f));var dim=UICraft.Box(modal,"Dimmer",0,0,1200/fit,(menuHeight+200)/fit,new Color(.20f,.16f,.15f,.43f),false,true);dim.sprite=null;modalMotion?.Kill();modal.localScale=Vector3.one*fit;var group=modal.GetComponent<CanvasGroup>();group.alpha=0;dressBody=UICraft.Root(modal,"Reference dressing",0,0,width,height);dressBody.localScale=Vector3.one*2.18f;R.Panel(dressBody,"panel",width/2,height/2,width,height);R.Text(dressBody,title,width/2,34,width-90,38,23,null,true);R.TextButton(dressBody,"X",width-38,32,45,45,"cream",CloseModal,29);modal.localScale=Vector3.one*fit*.96f;modalMotion=DOTween.Sequence().SetUpdate(true).SetId(modal);modalMotion.Append(modal.DOScale(fit,.24f).SetEase(Ease.OutBack));modalMotion.Join(DOTween.To(()=>group.alpha,v=>group.alpha=v,1,.18f));}
```

### DrawDressing · L46

方法 SHA-256：`c723eefbb1c6aa1b2d4a497857168ef874bfbbefe42d20eeb16723b2fba6161d`
时间来源：SetUpdate(true)
曲线证据：Ease.OutBack
解析符号：`BounceGarden.AppUI.DrawDressing()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.Shop.cs:45` · `AppUI.OpenDressing` → `DrawDressing()`
- `Assets/Scripts/UI/AppUI.Shop.cs:48` · `AppUI.DrawDressing` → `DrawDressing()`
- `Assets/Scripts/UI/AppUI.Shop.cs:48` · `AppUI.DrawDressing` → `DrawDressing()`
- `Assets/Scripts/UI/AppUI.Shop.cs:51` · `AppUI.DrawDressing` → `DrawDressing()`

```csharp
void DrawDressing(){if(dressBody==null)return;var old=dressBody.Find("装扮选项");if(old!=null){old.gameObject.SetActive(false);Destroy(old.gameObject);}var root=UICraft.Root(dressBody,"装扮选项",0,0,408,750);var contentFade=root.gameObject.AddComponent<CanvasGroup>();contentFade.alpha=0;DOTween.To(()=>contentFade.alpha,v=>contentFade.alpha=v,1,.16f).SetUpdate(true).SetLink(root.gameObject);
  var preview=R.Rect(root,"装扮预览",204,115,126,126);R.Portrait(preview,previewAvatar,previewFrame,63,63,126);preview.localScale=Vector3.one;preview.DOPunchScale(Vector3.one*.045f,.25f,1,.4f).SetUpdate(true).SetLink(preview.gameObject);R.Text(root,"预览",204,184,220,27,14);
  R.Panel(root,"cream",204,219,364,44);R.TextButton(root,"头像",114,219,182,43,dressingFrames?null:"lilac",()=>{dressingFrames=false;DrawDressing();},19);R.TextButton(root,"头像框",295,219,182,43,dressingFrames?"lilac":null,()=>{dressingFrames=true;DrawDressing();},19);
  var grid=ReferenceScroll(root,204,441,366,378,Mathf.Ceil(CatCosmeticCatalog.Items.Length/3f)*126);for(int n=0;n<CatCosmeticCatalog.Items.Length;n++){int index=n;string id=CatCosmeticCatalog.Items[index].id;bool f=dressingFrames,owned=Profile.OwnsShopCosmetic(id,f),selected=(string.IsNullOrEmpty(f?previewFrame:previewAvatar)?CatCosmeticCatalog.Default.id:(f?previewFrame:previewAvatar))==id;var card=R.Panel(grid,"card",0,0,116,122).rectTransform;card.anchorMin=card.anchorMax=new Vector2(.5f,1);card.anchoredPosition=new Vector2((n%3-1)*122,-63-n/3*126);
   if(selected){var rim=UICraft.Box(card,"Selected outline",0,0,113,119,R.Mint);var inner=UICraft.Box(rim.transform,"Selected cream",0,0,110,116,new Color32(255,252,244,255));}
   var hit=R.Button(card,"选择 "+(f?"头像框 ":"头像 ")+id,58,61,114,120,null,()=>{if(!owned){SoundService.Play("ui_unavailable");Toast("尚未解锁，去小铺看看吧。");return;}if(f)previewFrame=id==CatCosmeticCatalog.Default.id?"":id;else previewAvatar=id==CatCosmeticCatalog.Default.id?"":id;DrawDressing();});
   if(f)R.Portrait(hit.transform,previewAvatar,id,57,43,88);else if(index<0)R.Portrait(hit.transform,"",previewFrame,57,43,88);else R.Avatar(hit.transform,id,57,43,87);
   if(f)R.Text(hit.transform,index<0?"默认":FrameNames[index],57,89,105,19,13,null,true);
   string status=selected?((f?Profile.EquippedFrameId:Profile.EquippedAvatarId)==(id==CatCosmeticCatalog.Default.id?"":id)?"使用中":"已选择"):owned?"已拥有":LocalProfile.CosmeticPrice(id,f).ToString("N0");float statusY=f?108:104;if(selected||owned)R.Panel(hit.transform,selected?"mint":"cream",57,statusY,91,19);R.Text(hit.transform,status,selected||owned?57:66,statusY,88,20,13,selected?new Color32(28,151,97,255):R.Ink,true);
   if(!owned&&!f)foreach(var image in hit.GetComponentsInChildren<Image>())if(image.name.StartsWith("Catalog avatar",StringComparison.Ordinal))image.color=new Color(1,1,1,.65f);
   if(selected){var check=UICraft.Box(hit.transform,"Check",43,44,24,24,R.Mint,true);check.rectTransform.localScale=Vector3.one*.65f;check.rectTransform.DOScale(1,.22f).SetEase(Ease.OutBack).SetUpdate(true).SetLink(check.gameObject);var shortArm=UICraft.Box(check.transform,"Check short",-4,-1,7,3,Color.white);shortArm.rectTransform.localRotation=Quaternion.Euler(0,0,-45);var longArm=UICraft.Box(check.transform,"Check long",2,1,12,3,Color.white);longArm.rectTransform.localRotation=Quaternion.Euler(0,0,45);}else if(!owned){R.Art(hit.transform,"lock",95,79,17,19);R.Coin(hit.transform,30,statusY,16);}
  }
  R.TextButton(root,"到小铺发现更多装扮",204,656,340,34,null,()=>OpenShopCategory(dressingFrames?2:1),15);R.TextButton(root,"保存装扮",204,704,310,56,"mint",()=>{if(!Profile.EquipShopCosmetics(previewAvatar,previewFrame)){SoundService.Play("ui_unavailable");Toast("装扮保存失败，请重试。");return;}SoundService.Play("cosmetic_equip");if(UnityEngine.Random.value<.25f)SoundService.Play("cat_dress_pleased");RefreshLeagueViews();if(CurrentPage=="shop")DrawShop();CloseModal();Toast("装扮已保存");},23);
 }
```

### CosmeticSpark · L61

方法 SHA-256：`4debcdc90ce3fcbb0c14fe2d534505a7f11727843950688ca55fe920ac11b721`
时间来源：SetUpdate(true)
曲线证据：Ease.OutCubic, Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.AppUI.CosmeticSpark(UnityEngine.Transform)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
void CosmeticSpark(Transform parent){for(int n=0;n<8;n++){float angle=n*Mathf.PI/4;var spark=UICraft.Art(parent,"pastel/fx_spark",0,180,70,70);if(spark==null)continue;var rt=spark.rectTransform;DOTween.To(()=>rt.anchoredPosition,v=>rt.anchoredPosition=v,new Vector2(Mathf.Cos(angle)*220,180+Mathf.Sin(angle)*200),.5f).SetEase(Ease.OutCubic).SetUpdate(true).SetLink(rt.gameObject);DOTween.To(()=>spark.color.a,v=>{var c=spark.color;c.a=v;spark.color=c;},0,.25f).SetDelay(.25f).SetUpdate(true).SetLink(rt.gameObject);Destroy(rt.gameObject,.65f);}}
```

## 对象层级和素材入口

- L43：`UICraft.Box(modal,"Dimmer",0,0,1200/fit,(menuHeight+200)/fit,new Color(.20f,.16f,.15f,.43f),false,true)`
- L43：`UICraft.Root(modal,"Reference dressing",0,0,width,height)`
- L46：`dressBody.Find("装扮选项")`
- L46：`UICraft.Root(dressBody,"装扮选项",0,0,408,750)`
- L50：`UICraft.Box(card,"Selected outline",0,0,113,119,R.Mint)`
- L56：`UICraft.Box(hit.transform,"Check",43,44,24,24,R.Mint,true)`
- L56：`R.Art(hit.transform,"lock",95,79,17,19)`
- L61：`UICraft.Art(parent,"pastel/fx_spark",0,180,70,70)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
int shopCategory;
```
```csharp
bool dressingFrames;
```
```csharp
string previewAvatar,previewFrame;
```
```csharp
RectTransform shopBody,dressBody;
```
```csharp
float referenceShopHeight;
```
```csharp
RectTransform shopListing;
```
```csharp
TMP_Text referenceWallet;
```
```csharp
RectTransform referenceWalletTarget;
```
```csharp
static readonly string[] CatNames={"英短猫","布偶猫","暹罗猫","缅因猫","波斯猫","孟加拉猫","无毛猫","折耳猫","异短猫","安哥拉猫"};
```
```csharp
static readonly string[] FrameNames={"蓝色爪印","丁香蝴蝶结","海洋珍珠","森林绿叶","玫瑰花园","金色星光","月光","薄荷缎带","蜜桃爱心","皇家皇冠"};
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
