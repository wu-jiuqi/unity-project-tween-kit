# StreakRewardGlow · AppUI.StreakReward

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.StreakReward.cs`
文件 SHA-256：`d5e6d8efa747be565244fcc12b91e1491123f5fa4781e21cc54811b737019246`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.StreakReward.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### OnPopulateMesh · L70

方法 SHA-256：`1c7540805c74e1e5fa8baf235bbc630727bfb49fa29f9300aa333e5d0267b561`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.StreakRewardGlow.OnPopulateMesh(UnityEngine.UI.VertexHelper)`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
protected override void OnPopulateMesh(VertexHelper vh){vh.Clear();const int segments=64;float radius=rectTransform.rect.width*.5f;
  for(int i=0;i<segments;i++){float a=i*Mathf.PI*2/segments,b=(i+1)*Mathf.PI*2/segments;int n=vh.currentVertCount;var edge=color;edge.a=0;vh.AddVert(Vector3.zero,color,Vector2.zero);vh.AddVert(new Vector3(Mathf.Cos(a),Mathf.Sin(a))*radius,edge,Vector2.zero);vh.AddVert(new Vector3(Mathf.Cos(b),Mathf.Sin(b))*radius,edge,Vector2.zero);vh.AddTriangle(n,n+1,n+2);}
  for(int i=0;i<12;i++){float a=i*Mathf.PI/6;int n=vh.currentVertCount;var edge=color;edge.a=0;vh.AddVert(new Vector3(Mathf.Cos(a-.08f),Mathf.Sin(a-.08f))*radius*.25f,color,Vector2.zero);vh.AddVert(new Vector3(Mathf.Cos(a),Mathf.Sin(a))*radius,edge,Vector2.zero);vh.AddVert(new Vector3(Mathf.Cos(a+.08f),Mathf.Sin(a+.08f))*radius*.25f,color,Vector2.zero);vh.AddTriangle(n,n+1,n+2);}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
