# BrandLoadingProgress · BrandLoadingProgress

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/BrandLoadingProgress.cs`
文件 SHA-256：`cb5017d6042e2ae6e6c2f75c38d9824a5c81e1614efd54ac9dcb4e14744307d3`
[原始源码快照](../source/Assets/Scripts/UI/BrandLoadingProgress.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### AdvanceProgress · L24

方法 SHA-256：`652569af862717d166087b56f22d2a01f4046e1e5b2983fc6fa2225e34caf8d0`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BrandLoadingProgress.AdvanceProgress(float, float, float)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
// Preserve the existing startup bar's paced interpolation and stalled-frame cap.
 public static float AdvanceProgress(float shown,float target,float delta)=>Mathf.MoveTowards(shown,Mathf.Max(shown,target),.32f*Mathf.Clamp(delta,0,.05f));
```

### Update · L25

方法 SHA-256：`39f950e8b605aa830f272d1e09ed1a3934df7c32394bfabeba81f0e91e6dc78a`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BrandLoadingProgress.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){
  if(Hedge==null)return;
  if(!Failed)ShownProgress=AdvanceProgress(ShownProgress,TargetProgress,Time.unscaledDeltaTime);
  Hedge.SetProgress(ShownProgress);
  number.text=Percent+"%";if(IsComplete)status.text="准备好了，喵！";
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
TMP_Text number,status;
```
```csharp
bool complete;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
