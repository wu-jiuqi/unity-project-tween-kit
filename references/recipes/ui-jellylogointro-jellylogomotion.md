# JellyLogoMotion · JellyLogoIntro

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/JellyLogoIntro.cs`
文件 SHA-256：`b95d3a90f696aace042f7b35ded905e0101bdc962f77faf0baf9f095c0db3470`
[原始源码快照](../source/Assets/Scripts/UI/JellyLogoIntro.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Sample · L115

方法 SHA-256：`21be4dfe5441c8400d9239825369a9dff3eb0925bec84d2aaa0ba055167bf379`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Exp, Mathf.Sin
解析符号：`BounceGarden.JellyLogoMotion.Sample(float, float, float, float, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyLogoIntro.cs:164` · `JellyGlyph.Impact` → `JellyLogoMotion.Sample(t,remaining)`

```csharp
public static Pose Sample(float elapsed,float remaining,float strength=1,float phase=0,float fadeIn=1){
  float envelope=Mathf.Exp(-.42f*elapsed)*Mathf.Clamp01(remaining/.65f)*strength*fadeIn;
  float wave=Mathf.Sin(elapsed*11.5f+phase);
  return new Pose{Scale=new Vector3(1+wave*.17f*envelope,1-wave*.14f*envelope,1),
   Lift=Mathf.Abs(Mathf.Sin(elapsed*5.75f+phase*.5f))*21*envelope,
   Rotation=Mathf.Sin(elapsed*8+phase)*3.8f*envelope,
   Deformation=Mathf.Sin(Mathf.Floor(elapsed*30)/30*11.5f+phase)*.42f*envelope};
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
