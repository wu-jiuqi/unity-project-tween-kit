# GameSession · CollectionSession

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/CollectionSession.cs`
文件 SHA-256：`7e842fe5ad1036d8cff1652bc6762e789b54aa4d73207c5ab22701b4c86b9bc9`
[原始源码快照](../source/Assets/Scripts/Core/CollectionSession.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### FallCollectionTreats · L69

方法 SHA-256：`6e3cae7d154f51f28d6a376191ac364dc2afb338f744a31dfe26239cc0d5f7f2`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameSession.FallCollectionTreats(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/CollectionSession.cs:82` · `GameSession.TickCollection` → `FallCollectionTreats(dt)`
- `Assets/Scripts/Core/CollectionSession.cs:87` · `GameSession.TickCollection` → `FallCollectionTreats(dt)`

```csharp
bool FallCollectionTreats(float dt){bool moving=false;foreach(var item in CollectionTreats.OrderBy(t=>t.View.position.y).ToArray()){
  float y=item.View.position.y,target=CollectionLevels.Basket;
  foreach(var b in CollectionBricks)if(b.Alive&&b.Definition.referenceCol==item.Column&&b.transform.position.y<y-.05f)target=Mathf.Max(target,b.transform.position.y+.65f);
  foreach(var lower in CollectionTreats)if(lower!=item&&lower.Column==item.Column&&lower.View.position.y<y-.05f)target=Mathf.Max(target,lower.View.position.y+.50f);
  target=Mathf.Min(y,target);item.Speed+=dt*20;float next=Mathf.MoveTowards(y,target,item.Speed*dt);item.View.position=new Vector3(CollectionLevels.Cell(item.Column,0).x,next,0);
  if(next<=CollectionLevels.Basket+.001f){CollectTreat(item);continue;}if(next>target+.001f)moving=true;else item.Speed=0;
 }return moving;}
```

### TickCollection · L76

方法 SHA-256：`cc825d71fb62b3d566ace0ccf4ccbee0a60dc0f2a7afc72de7f13196c0983d0e`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.GameSession.TickCollection(float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
void TickCollection(float dt){
  if(!IsCollection)return;if(IsChainCollection){TickChainCollection(dt);return;}
  harvestLife=Mathf.Max(0,harvestLife-dt);if(harvestText!=null){harvestText.alpha=Mathf.Clamp01(harvestLife*3);harvestText.transform.localPosition=new Vector3(0,CollectionLevels.Basket+.75f+(1.3f-harvestLife)*.3f,-.1f);harvestText.transform.localScale=Vector3.one*(1+Mathf.Sin(Mathf.Clamp01((1.3f-harvestLife)*4)*Mathf.PI)*.16f);}
  bool danger=CollectionBricks.Any(b=>b.Alive&&b.transform.position.y<CollectionLevels.Danger+2);
  if(collectionDanger!=null)collectionDanger.color=new Color(1,.27f,.31f,danger?.55f+.4f*Mathf.Sin(simulationClock*8):.25f);
  if(collectionPhase==0&&collectionRoundPending&&ActiveBalls==0&&PendingBurstBalls==0&&rules.PendingDamageChains==0){collectionPhase=1;changed=true;}
  if(collectionPhase==1){if(FallCollectionTreats(dt)||CollectionWon)return;if(!collectionRoundPending){collectionPhase=0;PlanCollectionRow();changed=true;return;}collectionPhase=2;collectionClock=0;}
  if(collectionPhase==2){float step=Mathf.Min(dt,.32f-collectionClock);collectionClock+=step;foreach(var b in CollectionBricks)if(b.Alive)b.transform.position+=Vector3.down*(CollectionLevels.Pitch*step/.32f);Physics2D.SyncTransforms();if(collectionClock<.32f)return;
   collectionFailed=CollectionBricks.Any(b=>b.Alive&&b.transform.position.y-.38f<=CollectionLevels.Danger);
   if(collectionFailed)return;foreach(var b in CollectionBricks.Where(b=>!b.Alive).ToArray()){CollectionBricks.Remove(b);Destroy(b.gameObject);}SpawnCollectionRow(NextCollectionRow,0);CollectionRound++;PlanCollectionRow();collectionRoundPending=false;collectionPhase=3;collectionClock=0;changed=true;
  }
  if(collectionPhase==3){bool moving=FallCollectionTreats(dt);collectionClock+=dt;if(!moving&&collectionClock>.30f){collectionPhase=0;changed=true;}}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public readonly List<BrickBody> CollectionBricks=new List<BrickBody>();
```
```csharp
public readonly List<CollectionTreat> CollectionTreats=new List<CollectionTreat>();
```
```csharp
public readonly int[] Collected=new int[2],CollectionGoal=new int[2];
```
```csharp
public const float CollectionVictoryDelay=1f;
```
```csharp
float collectionVictoryAt;
```
```csharp
int collectionPhase;
```
```csharp
float collectionClock;
```
```csharp
bool collectionFailed,collectionRoundPending;
```
```csharp
SpriteRenderer collectionDanger;
```
```csharp
TMPro.TMP_Text harvestText;
```
```csharp
int harvestCount;
```
```csharp
float harvestLife;
```
```csharp
readonly List<Transform> collectionPreview=new List<Transform>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
