# UICraft · UICraft

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/UICraft.cs`
文件 SHA-256：`1ffef55e3a848a9b068d062396f727d5fc6f739d1ca3e59ec342a3dd9b0db0c6`
[原始源码快照](../source/Assets/Scripts/UI/UICraft.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Flower · L42

方法 SHA-256：`271d638afe276a01736394d0ef2f44d2d5cad4ff0b6ca6a86a0409227e4f3289`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.UICraft.Flower(UnityEngine.Transform, float, float, float, UnityEngine.Color)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public static RectTransform Flower(Transform p,float x,float y,float s,Color c){var rt=Root(p,"Flower",x,y,s,s);Box(rt,"Stem",0,-s*.38f,s*.1f,s*.5f,new Color32(50,129,65,255));for(int a=0;a<5;a++){float f=a*Mathf.PI*2/5;Box(rt,"Petal",Mathf.Cos(f)*s*.24f,Mathf.Sin(f)*s*.24f,s*.38f,s*.38f,c,true);}Box(rt,"Pollen",0,0,s*.32f,s*.32f,Gold,true);return rt;}
```

## 对象层级和素材入口

- L42：`Root(p,"Flower",x,y,s,s)`
- L42：`Box(rt,"Stem",0,-s*.38f,s*.1f,s*.5f,new Color32(50,129,65,255))`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public static readonly Color Ink = new Color32(108,76,74,255);
```
```csharp
public static readonly Color Green = new Color32(160,226,195,255);
```
```csharp
public static readonly Color Blue = new Color32(193,169,235,255);
```
```csharp
public static readonly Color Gold = new Color32(255,199,42,255);
```
```csharp
static Sprite rounded, circle;
```
```csharp
static Font font;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
