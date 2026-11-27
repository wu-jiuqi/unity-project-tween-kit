# CollectionFlightMotion · BattleJellyArt

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/BattleJellyArt.cs`
文件 SHA-256：`a164482acef4b29ab5e4801ec76b40e59dc32750ccc6135b7c7e0d43b3338e94`
[原始源码快照](../source/Assets/Scripts/UI/BattleJellyArt.cs.txt)

用在哪里：GameSession.OpeningCollect → chainLoot → TickChainCollection → CollectionFlightMotion.Apply；ChainCollectionHUD 计算真实目标
触发操作：主线收集物飞向目标卡；收集计数留在 GameSession
导入组件：`ProjectTweenKit.RewardFlight`；展馆 ID：`reward`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Apply · L14

方法 SHA-256：`166455be2b0e20589c20094b47fafbcf4911999ed7f18e8bb146f9598d7b08f5`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.CollectionFlightMotion.Apply(BounceGarden.ChainLoot, UnityEngine.Vector2, string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/ChainCollectionSession.cs:157` · `GameSession.TickChainCollection` → `CollectionFlightMotion.Apply(loot,target,IsOpening&&loot.OpeningTarget>=0?OpeningTargets[loot.OpeningTarget].kind:"ordinary_color")`

```csharp
public static void Apply(ChainLoot loot,Vector2 target,string kind){
  float t=Mathf.Clamp01(loot.Age/.6f),u=t*t*(3-2*t);Vector2 delta=target-loot.Start;
  float side=kind=="cookie"?-.18f:kind=="yarn"?.18f:0;Vector2 control=loot.Start+delta*.22f+Vector2.up*(kind=="ingot"?.65f:.4f)+Vector2.right*side;
  loot.View.position=(1-u)*(1-u)*loot.Start+2*(1-u)*u*control+u*u*target;
  float enter=1+Mathf.Sin(Mathf.Min(1,t/.2f)*Mathf.PI)*.10f,shrink=Mathf.Lerp(1,.28f,Mathf.Pow(t,3));
  float ripple=Mathf.Sin(t*Mathf.PI*2)*.035f*(1-t);loot.View.localScale=new Vector3(1+ripple,1-ripple,1)*(enter*shrink);
  float angle=kind=="yarn"?12:kind=="cookie"?7:kind=="copper_coin"?4:2;loot.View.localRotation=Quaternion.Euler(0,0,Mathf.Sin(t*Mathf.PI*2)*angle*(1-t));
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
