# GameSession · ChainCollectionSession

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/ChainCollectionSession.cs`
文件 SHA-256：`3efeca58b1f20057880acc95e8cb37e922c60fde162490edd53a31f07733e3de`
[原始源码快照](../source/Assets/Scripts/Core/ChainCollectionSession.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### TickChainCollection · L150

方法 SHA-256：`60e763e412239f0e55d79840fae3e1631f0dfd364e9fc27ec4d3dd0ce0596359`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.GameSession.TickChainCollection(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/CollectionSession.cs:45` · `GameSession.TickCollection` → `TickChainCollection(dt)`
- `Assets/Scripts/Core/VictoryRoundSettlement.cs:42` · `GameSession.TickVictoryBoard` → `TickChainCollection(dt)`

```csharp
void TickChainCollection(float dt){
  TickOpeningYarn(dt);  TickSevenSpecials();TickRefill80(dt);
  if(!SevenSpecialActive&&ActiveBalls==0&&PendingBurstBalls==0&&ChainMinis.Count==0&&!queuedShot.HasValue&&(Shots==0||CollectionWon||EarnedChainGoal())){CompleteEarnedChainLoot();return;}
  TickChainMinis(dt);
  foreach(var beam in chainBeams.ToArray()){beam.Life-=dt;if(beam.Life<=0){Destroy(beam.View.gameObject);chainBeams.Remove(beam);}else if(beam.View!=null){var sprite=beam.View.GetComponent<SpriteRenderer>();var color=sprite.color;color.a=beam.StartAlpha*Mathf.Clamp01(beam.Life/beam.Duration);sprite.color=color;}}
  foreach(var loot in chainLoot.ToArray()){
   loot.Age+=dt;float t=Mathf.Clamp01(loot.Age/.6f);Vector2 target=loot.OpeningTarget>=0?(OpeningLootDestination!=null?OpeningLootDestination(loot.OpeningTarget):new Vector2(0,7.95f)):loot.JellyColor>=0?(JellyLootDestination!=null?JellyLootDestination(loot.JellyColor):new Vector2(-2.4f,7.95f)):(ChainLootDestination!=null?ChainLootDestination(loot.Kind):new Vector2(loot.Kind==1?-1.35f:1.6f,7.95f));
   if(IsOpening){CollectionFlightMotion.Apply(loot,target,IsOpening&&loot.OpeningTarget>=0?OpeningTargets[loot.OpeningTarget].kind:"ordinary_color");}else loot.View.position=Vector2.Lerp(loot.Start,target,t*t)+Vector2.up*(Mathf.Sin(t*Mathf.PI)*.7f);
   float absorb=Mathf.Sin(Mathf.Clamp01((t-.78f)/.22f)*Mathf.PI)*.28f;if(!IsOpening)loot.View.localScale=new Vector3(1+absorb,1-absorb,1)*Mathf.Lerp(.55f,.25f,t);
   if(t>=1){SoundService.Play("collect_arrive");if(loot.OpeningTarget>=0){if(loot.StrategyCredit!=null)loot.StrategyCredit();else OpeningCreditTarget(loot.OpeningTarget);}else if(loot.JellyColor>=0)CollectJelly(loot.JellyColor);else{if(Collected[loot.Kind-1]<CollectionGoal[loot.Kind-1]&&Collected[loot.Kind-1]+1>=CollectionGoal[loot.Kind-1])SoundService.Play("goal_complete");Collected[loot.Kind-1]=Mathf.Min(CollectionGoal[loot.Kind-1],Collected[loot.Kind-1]+1);}if(!IsOpening){ChainVisualArt.Effect(this,2,3,target,.7f);ChainVisualArt.Glint(this,target,3,.28f);EmitFeedback(target,"break",loot.JellyColor>=0?JellyCollectionArt.Color(loot.JellyColor):loot.Kind==1?new Color(1,.73f,.3f):new Color(1,.58f,.68f));}Destroy(loot.View.gameObject);chainLoot.Remove(loot);changed=true;}
  }
  TickChainGravity(dt);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
int chainRewardTier=-1;
```
```csharp
public readonly List<ChainTool> ChainTools=new List<ChainTool>();
```
```csharp
public readonly List<ChainMini> ChainMinis=new List<ChainMini>();
```
```csharp
readonly List<ChainLoot> chainLoot=new List<ChainLoot>();
```
```csharp
readonly List<Transform> chainPreview=new List<Transform>();
```
```csharp
readonly List<ChainBeam> chainBeams=new List<ChainBeam>();
```
```csharp
public readonly int[] ChainKindActivations=new int[6];
```
```csharp
public const float ChainProtection=.14f;
```
```csharp
readonly List<ChainTool> chainToolHitSnapshot=new List<ChainTool>(16);
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
