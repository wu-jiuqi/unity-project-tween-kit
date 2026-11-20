# StarChargeView · StarChargeView

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/StarChargeView.cs`
文件 SHA-256：`816d726fcb239a8fa79cb3bf1a13db08bfb741ac239dc70e043e0bab55961f08`
[原始源码快照](../source/Assets/Scripts/UI/StarChargeView.cs.txt)

用在哪里：BattleScoreMeter.Initialize → StarChargeView.Initialize；Place → Charge；Advance 驱动
触发操作：分数星光到达、材质边缘填充和满星脉冲
导入组件：`ProjectTweenKit.StarChargeView`；展馆 ID：`charge`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### SetImmediate · L16

方法 SHA-256：`fa3b9f1b6d3452f7eb55183d9c06cb8c03c4168afa5997b1e393ad38c2eea31f`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.StarChargeView.SetImmediate(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/StarChargeView.cs:14` · `StarChargeView.Initialize` → `SetImmediate(initial)`
- `Assets/Scripts/UI/StarChargeView.cs:17` · `StarChargeView.Charge` → `SetImmediate(value)`

```csharp
public void SetImmediate(float value){pending=target=fill=fromFill=Mathf.Clamp01(value);fillAge=1;flight=-1;if(gold!=null)gold.fillAmount=fill;Render();}
```

### Advance · L20

方法 SHA-256：`e0ee98dcc1bef7b5e757456663da2bd45016953d4ae462410f884b670e9a4957`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin, Mathf.SmoothStep
解析符号：`BounceGarden.StarChargeView.Advance(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SfxV87QA.cs:40` · `SfxV87QA.Start` → `star.Advance(.25f)`
- `Assets/Scripts/SfxV87QA.cs:40` · `SfxV87QA.Start` → `star.Advance(.4f)`
- `Assets/Scripts/SfxV87QA.cs:40` · `SfxV87QA.Start` → `star.Advance(.5f)`
- `Assets/Scripts/UI/BattleScoreMeter.cs:28` · `BattleScoreMeter.Advance` → `charge.Advance(dt)`
- `Assets/Scripts/UI/BattleScoreMeter.cs:29` · `BattleScoreMeter.LateUpdate` → `charge.Advance(game.PresentationDelta(Mathf.Min(.05f,Time.unscaledDeltaTime)))`

```csharp
public void Advance(float dt){clock+=dt;if(flight>=0){flight+=dt;float u=Mathf.Clamp01(flight/.24f);Vector2 end=host.InverseTransformPoint(transform.position);cometRoot.anchoredPosition=Vector2.Lerp(origin,end,u)+Vector2.up*Mathf.Sin(u*Mathf.PI)*35;cometRoot.localScale=Vector3.one*(1+.4f*Mathf.Sin(u*Mathf.PI));comet.color=new Color(1,1,.83f,1-u*.25f);if(u>=1){flight=-1;fromFill=fill;target=pending;fillAge=0;comet.gameObject.SetActive(false);}}
  float before=fill;fillAge+=dt;fill=Mathf.Lerp(fromFill,target,Mathf.SmoothStep(0,1,Mathf.Clamp01(fillAge/.36f)));if(before<.999f&&fill>=.999f){fullAge=0;SoundService.Play("score_star_full");}fullAge+=dt;gold.fillAmount=fill;Render();
 }
```

### Render · L23

方法 SHA-256：`b284c1e00fd0f3eefd5a21d88f1986b4dce6677a27ff99af265181e93602d3b6`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.StarChargeView.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/StarChargeView.cs:10` · `StarChargeView.RefreshGeometry` → `Render()`
- `Assets/Scripts/UI/StarChargeView.cs:16` · `StarChargeView.SetImmediate` → `Render()`
- `Assets/Scripts/UI/StarChargeView.cs:21` · `StarChargeView.Advance` → `Render()`

```csharp
void Render(){if(gold==null)return;chargeMaterial.SetFloat("_Charge",fill);chargeMaterial.SetFloat("_Clock",clock);
  float pulse=Mathf.Sin(Mathf.Clamp01(fullAge/.38f)*Mathf.PI);transform.localScale=Vector3.one*(1+pulse*.32f);
  if(comet!=null&&flight<0)comet.gameObject.SetActive(false);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(comet!=null)Destroy(comet.gameObject);if(chargeMaterial!=null)Destroy(chargeMaterial);}
```

## 源字段与依赖

```csharp
Image gold,comet;
```
```csharp
Material chargeMaterial;
```
```csharp
RectTransform host,cometRoot;
```
```csharp
float pending,target,fill,fromFill,fillAge=1,flight=-1,clock,fullAge=9;
```
```csharp
Vector2 origin;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
