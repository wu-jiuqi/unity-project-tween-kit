# GameSession · OpeningTargets

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/OpeningTargets.cs`
文件 SHA-256：`8b6126ce9c76d27cc0138828b6950db5dd301285b9889823ee6dd3fc9736f778`
[原始源码快照](../source/Assets/Scripts/Core/OpeningTargets.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### OpeningCollect · L17

方法 SHA-256：`88773666f1114b4e546cb0b8af8930587a55e06c8542d3e61b6bcdef5295ee46`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameSession.OpeningCollect(string, UnityEngine.Vector2, int, bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/OpeningCampaign.cs:107` · `GameSession.OpeningYarnArrived` → `OpeningCollect("yarn",position)`
- `Assets/Scripts/Core/OpeningCookieSession.cs:36` · `GameSession.OpeningCookieApply` → `OpeningCollect("cookie",box.Cell(slot))`
- `Assets/Scripts/Core/OpeningCopperSession.cs:46` · `GameSession.OpeningCopperArrived` → `OpeningCollect("copper_coin",p)`
- `Assets/Scripts/Core/OpeningTargets.cs:30` · `GameSession.OpeningCreditTool` → `OpeningCollect(OpeningThirdArt.KindName(tool.Kind),tool.Position,tool.ColorIndex,tool.Ingot)`

```csharp
void OpeningCollect(string kind,Vector2 position,int color=-1,bool ingot=false){
  if(!IsOpening)return;
  for(int i=0;i<OpeningTargets.Length;i++){var t=OpeningTargets[i];bool match=t.kind=="ingot"?ingot:t.kind=="ordinary_color"?kind=="ordinary_color"&&!ingot&&OpeningCampaign.Color(t.color)==color:t.kind==kind;
   if(!match||OpeningCollected[i]+openingTargetPending[i]>=t.count)continue;
   openingTargetPending[i]++;var root=new GameObject("Opening collected "+t.kind).transform;root.SetParent(world,false);root.position=position;
   var sprite=OpeningTargetSprite(i);var image=ChainVisualArt.Draw(root,"Independent target collectible",sprite,.48f/Mathf.Max(.01f,sprite.bounds.size.x),75);image.sharedMaterial=OpeningYarnArt.Material;
   // A thin cream silhouette keeps the flying object readable over same-colour bricks.
   var edge=new GameObject("Soft collectible rim").AddComponent<SpriteRenderer>();edge.transform.SetParent(root,false);edge.sprite=sprite;edge.sharedMaterial=SevenSpecialArt.Material(sprite);edge.sortingOrder=image.sortingOrder-1;edge.transform.localScale=image.transform.localScale*1.12f;edge.color=new Color(1,1,1,.82f);var block=new MaterialPropertyBlock();block.SetFloat("_Flash",1);edge.SetPropertyBlock(block);
   int creditedTarget=i;chainLoot.Add(new ChainLoot{OpeningTarget=i,Start=position,View=root,StrategyCredit=StrategyObserver==null?null:StrategyWrap(()=>OpeningCreditTarget(creditedTarget))});changed=true;
  }
 }
```

## 对象层级和素材入口

- L21：`root.SetParent(world,false)`
- L24：`edge.transform.SetParent(root,false)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
int[] openingTargetPending=Array.Empty<int>();
```
```csharp
public Func<int,Vector2> OpeningLootDestination;
```
```csharp
readonly HashSet<int> openingCreditedBricks=new HashSet<int>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
