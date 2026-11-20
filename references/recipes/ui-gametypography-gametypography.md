# GameTypography · GameTypography

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/GameTypography.cs`
文件 SHA-256：`e8888f5956b8619398dbc6f887ea0fe199857103e9733b6a704c84df31abcbbc`
[原始源码快照](../source/Assets/Scripts/UI/GameTypography.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Outline · L7

方法 SHA-256：`15126e098bb281ec4326557bed8874975912df2affdf5192aa68e729164d0688`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameTypography.Outline`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public static Material Outline {get{if(outline==null){outline=new Material(Font.material);outline.EnableKeyword("OUTLINE_ON");outline.SetFloat(ShaderUtilities.ID_OutlineWidth,0f);outline.SetColor(ShaderUtilities.ID_OutlineColor,new Color(.18f,.12f,.09f,1));}return outline;}}
```

### Heading · L8

方法 SHA-256：`f54af42187c123bf19d8cd7029d36ab65c3e4bfd133e1621c866f6be6d639bcd`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameTypography.Heading`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public static Material Heading {get{if(heading==null){heading=new Material(Font.material);heading.EnableKeyword("OUTLINE_ON");heading.SetFloat(ShaderUtilities.ID_OutlineWidth,0f);heading.SetColor(ShaderUtilities.ID_OutlineColor,new Color(.58f,.035f,.18f,1));}return heading;}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
static TMP_FontAsset font,heavy;
```
```csharp
static Material outline,heading;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
