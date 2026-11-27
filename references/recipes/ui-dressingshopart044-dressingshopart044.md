# DressingShopArt044 · DressingShopArt044

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/DressingShopArt044.cs`
文件 SHA-256：`2d48b24be5fbd96a10002dc816fb94b10a679110c2dc85bd2ddefb429289f447`
[原始源码快照](../source/Assets/Scripts/UI/DressingShopArt044.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### BannerText · L22

方法 SHA-256：`3e82fca74ef8cf0faf6cf36c12f7e3800e4f6d39108e2f865c4e29f5fe8a5688`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.DressingShopArt044.BannerText(UnityEngine.Transform, string, float, float, float, float, int, bool)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public static TMP_Text BannerText(Transform parent,string value,float x,float y,float w,float h,int size,bool title){
  var t=Text(parent,value,x,y,w,h,size,new Color32(255,246,218,255));t.fontStyle=FontStyles.Bold;
  var material=title?goldTitle:goldSubtitle;if(material==null){material=new Material(GameTypography.Font.material);material.name=title?"044 gold outline banner title":"044 gold outline banner subtitle";material.EnableKeyword("OUTLINE_ON");material.SetColor(ShaderUtilities.ID_OutlineColor,new Color32(221,161,52,255));material.SetFloat(ShaderUtilities.ID_OutlineWidth,title?.16f:.11f);material.SetFloat("_OutlineSoftness",0);material.SetFloat("_FaceDilate",title?.025f:.012f);if(title)goldTitle=material;else goldSubtitle=material;}
  t.fontSharedMaterial=material;t.UpdateMeshPadding();return t;
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
static Layouts layouts;
```
```csharp
static readonly Dictionary<string,Sprite> sprites=new Dictionary<string,Sprite>();
```
```csharp
static Material goldTitle,goldSubtitle;
```
```csharp
public static readonly string[] Names={"元气橘橘","俏皮蓝蓝","好奇布偶","机灵三花","晚安云朵","开心虎斑"};
```
```csharp
public static readonly string[] FrameNames={"薄荷新芽","奶油猫耳","丁香蝴蝶结","雏菊花园","星月晴空","金爪桂冠"};
```
```csharp
public static readonly string[] RewardNames={"金爪桂冠","繁花花匠","星露伙伴"};
```
```csharp
public static readonly Color Ink=new Color32(121,80,46,255),Muted=new Color32(163,123,85,255);
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
