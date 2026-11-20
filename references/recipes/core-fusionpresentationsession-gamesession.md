# GameSession · FusionPresentationSession

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/FusionPresentationSession.cs`
文件 SHA-256：`310d7adb887305d999cec7df907fdba780e6e4bf49ed4ae7ac2662f38a83563e`
[原始源码快照](../source/Assets/Scripts/Core/FusionPresentationSession.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### TickFusionPresentation · L22

方法 SHA-256：`44870175795553d2c895e8332ca39ec41b9fc16df39c96f1a5b2ef64dc1ba584`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.GameSession.TickFusionPresentation(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/FusionSession.cs:254` · `GameSession.TickFusion` → `TickFusionPresentation(dt)`

```csharp
void TickFusionPresentation(float dt){
  // Fusion dispatch replaces TickChainCollection, so it owns the same beam pool.
  foreach(var beam in chainBeams.ToArray()){
   beam.Life-=dt;if(beam.View==null){chainBeams.Remove(beam);continue;}
   if(beam.Life<=0){Destroy(beam.View.gameObject);chainBeams.Remove(beam);continue;}
   var sprite=beam.View.GetComponent<SpriteRenderer>();if(sprite!=null){var color=sprite.color;color.a=beam.StartAlpha*Mathf.Clamp01(beam.Life/beam.Duration);sprite.color=color;}
  }
  foreach(var flight in fusionGoalFlights.ToArray()){
   if(flight.View==null){fusionGoalFlights.Remove(flight);continue;}
   flight.Age+=dt;float t=Mathf.Clamp01(flight.Age/.55f);int index=flight.Kind=="fish"?1:2;
   Vector2 destination=flight.Kind=="jelly"?(JellyLootDestination!=null?JellyLootDestination(1):new Vector2(-2.4f,7.95f)):(ChainLootDestination!=null?ChainLootDestination(index):new Vector2(index==1?-1.35f:1.6f,7.95f));
   flight.View.position=Vector2.Lerp(flight.Start,destination,t*t)+Vector2.up*(Mathf.Sin(t*Mathf.PI)*.6f);
   flight.View.localScale=Vector3.one*Mathf.Lerp(.48f,.24f,t);
   if(t<1)continue;
   SoundService.Play("collect_arrive");ChainVisualArt.Glint(this,destination,3,.25f);Destroy(flight.View.gameObject);fusionGoalFlights.Remove(flight);
  }
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
readonly List<FusionGoalFlight> fusionGoalFlights=new List<FusionGoalFlight>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
