# StarChargeView · StarChargeView

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/StarChargeView.cs`
文件 SHA-256：`024b28acce52159900fec2dc1b703384cc9bda5d4af1b9af097493d7231b2ae8`
[原始源码快照](../source/Assets/Scripts/UI/StarChargeView.cs.txt)

用在哪里：BattleScoreMeter.Initialize → StarChargeView.Initialize；Place → Charge；Advance 驱动
触发操作：分数星光到达、原生UI遮罩填充和满星脉冲
导入组件：`ProjectTweenKit.StarChargeView`；展馆 ID：`charge`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Advance · L19

方法 SHA-256：`ae735b00b2f4f67396058222805f96a2925050d1036237cfa618340e7b8a59ff`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin, Mathf.SmoothStep
解析符号：`BounceGarden.StarChargeView.Advance(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/BattleScoreMeter.cs:31` · `BattleScoreMeter.Advance` → `charge.Advance(dt)`
- `Assets/Scripts/UI/BattleScoreMeter.cs:32` · `BattleScoreMeter.LateUpdate` → `charge.Advance(game.PresentationDelta(Mathf.Min(.05f,Time.unscaledDeltaTime)))`

```csharp
public void Advance(float dt){clock+=dt;if(flight>=0){flight+=dt;float u=Mathf.Clamp01(flight/.24f);Vector2 end=host.InverseTransformPoint(transform.position);cometRoot.anchoredPosition=Vector2.Lerp(origin,end,u)+Vector2.up*Mathf.Sin(u*Mathf.PI)*35;cometRoot.localScale=Vector3.one*(1+.4f*Mathf.Sin(u*Mathf.PI));comet.color=new Color(1,1,.83f,1-u*.25f);if(u>=1){flight=-1;fromFill=fill;target=pending;fillAge=0;comet.gameObject.SetActive(false);}}float before=fill;fillAge+=dt;fill=Mathf.Lerp(fromFill,target,Mathf.SmoothStep(0,1,Mathf.Clamp01(fillAge/.36f)));if(before<.999f&&fill>=.999f){fullAge=0;SoundService.Play("score_star_full");}fullAge+=dt;Render();}
```

### Render · L20

方法 SHA-256：`1f48e531ba0549e73bbe0c26dfcb1d4a2981204b797c0702dfd58ae172fa4cd4`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.StarChargeView.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/StarChargeView.cs:9` · `StarChargeView.PrepareVictoryTransfer` → `Render()`
- `Assets/Scripts/UI/StarChargeView.cs:10` · `StarChargeView.RefreshGeometry` → `Render()`
- `Assets/Scripts/UI/StarChargeView.cs:10` · `StarChargeView.SetRotation` → `Render()`
- `Assets/Scripts/UI/StarChargeView.cs:17` · `StarChargeView.SetImmediate` → `Render()`
- `Assets/Scripts/UI/StarChargeView.cs:19` · `StarChargeView.Advance` → `Render()`

```csharp
void Render(){if(gold==null)return;Quaternion turn=Quaternion.Euler(0,0,rotation);empty.rectTransform.localRotation=gold.rectTransform.localRotation=turn;empty.enabled=fill<.999f;gold.enabled=fill>.001f;clip.sizeDelta=new Vector2(FillExtent,FillExtent*fill);float cut=-HalfExtent+FillExtent*fill;edge.enabled=EdgeVisible;if(edge.enabled){edge.rectTransform.localRotation=Quaternion.Inverse(turn);edge.rectTransform.localPosition=Quaternion.Inverse(turn)*new Vector3(0,cut-1,0);edge.color=new Color(1,1,.88f,.8f+.2f*Mathf.Sin(clock*12));}float pulse=Mathf.Sin(Mathf.Clamp01(fullAge/.38f)*Mathf.PI);if(!victory)transform.localScale=Vector3.one*(1+pulse*.32f);var size=((RectTransform)transform).rect.size;visual.localScale= victory?Vector3.one*(size.x/90f):new Vector3(size.x/90f,size.y/82f,1);visual.anchoredPosition=new Vector2(0,-45*pulse*.32f*(size.y/82f)/(1+pulse*.32f));if(comet!=null&&flight<0)comet.gameObject.SetActive(false);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(comet!=null)Destroy(comet.gameObject);}
```

## 源字段与依赖

```csharp
// 136 covers the full diagonal of the rotating 90 x 82 artwork at every angle.
 const float FillExtent=136,HalfExtent=FillExtent*.5f;
```
```csharp
Image empty,gold,edge,comet;
```
```csharp
RectTransform host,cometRoot,clip,visual;
```
```csharp
bool victory;
```
```csharp
float pending,target,fill,fromFill,fillAge=1,flight=-1,clock,fullAge=9,rotation;
```
```csharp
Vector2 origin;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
