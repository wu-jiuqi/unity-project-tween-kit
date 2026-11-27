# BrandGardenHedge · BrandGardenHedge

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/BrandGardenHedge.cs`
文件 SHA-256：`637152018a72fb1175785b8e7beb4b7bf1a68069f7e89bb666d3edd168ae3d4a`
[原始源码快照](../source/Assets/Scripts/UI/BrandGardenHedge.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L43

方法 SHA-256：`34f60cdbf7276ba7430b0284404b901aa05f449e6a75b35c67da39ff3336ce16`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Exp, Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.BrandGardenHedge.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){
  age+=Mathf.Min(Time.unscaledDeltaTime,.05f);GrownCount=0;FlowerCount=0;
  for(int i=0;i<sprouted;i++){
   float elapsed=age-started[i],t=Mathf.Clamp01(elapsed/GrowthDuration);if(t>=1)GrownCount++;
   float reaction=age-poked[i];float wave=reaction<1.3f?Mathf.Sin(reaction*17)*Mathf.Exp(-reaction*4.5f):0;
   float sway=t>=1?Mathf.Sin(age*1.7f+i*.65f)*.8f:0;
   clumps[i].localScale=new Vector3(Mathf.Lerp(.7f,1,1-Mathf.Pow(1-t,3))+wave*.075f,Mathf.Max(.001f,UICraft.OutBack(t)-wave*.055f),1);
   clumps[i].localRotation=Quaternion.Euler(0,0,sway+wave*5);
   clumps[i].anchoredPosition=roots[i];
  }
  for(int i=0;i<flowers.Length;i++){
   float t=Mathf.Clamp01((age-started[FlowerOwners[i]]-.30f)/.34f);bool show=FlowerOwners[i]<sprouted&&t>0;
   flowers[i].gameObject.SetActive(show);if(!show)continue;FlowerCount++;
   flowers[i].localScale=Vector3.one*UICraft.OutBack(t);flowers[i].localRotation=Quaternion.Euler(0,0,Mathf.Sin(age*2+i)*3);
  }
  for(int i=0;i<2;i++){
   float phase=age*(1.0f+i*.17f)+i*3.1f;
   butterflies[i].anchoredPosition=new Vector2((i==0?-205:230)+Mathf.Sin(phase*.8f)*87,132+Mathf.Sin(phase*1.7f)*23+i*13);
   butterflies[i].GetComponent<UnityEngine.UI.Image>().sprite=butterflyFrames[(Mathf.FloorToInt(age*13+i)%3)==0?1:0];
   butterflies[i].localRotation=Quaternion.Euler(0,0,Mathf.Sin(phase)*12);
  }
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public const int ClumpCount=10;
```
```csharp
public const float GrowthDuration=.42f;
```
```csharp
static readonly int[] FlowerOwners={1,3,6,8};
```
```csharp
readonly RectTransform[] clumps=new RectTransform[ClumpCount],flowers=new RectTransform[4],butterflies=new RectTransform[2];
```
```csharp
readonly float[] started=new float[ClumpCount],poked=new float[ClumpCount];
```
```csharp
readonly Vector2[] roots=new Vector2[ClumpCount];
```
```csharp
float age;
```
```csharp
int sprouted;
```
```csharp
Sprite[] butterflyFrames;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
