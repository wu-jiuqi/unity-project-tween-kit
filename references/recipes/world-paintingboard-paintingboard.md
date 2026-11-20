# PaintingBoard · PaintingBoard

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/PaintingBoard.cs`
文件 SHA-256：`f9b9d501d8d95fee86fa408c51af92c133769cf4f749a9f6c828f9c9d655af2e`
[原始源码快照](../source/Assets/Scripts/World/PaintingBoard.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L25

方法 SHA-256：`d26278477b669c78bfe6e1bf9cac3f17c9cbacd7dbf45bdc8cc3a617f6b3876c`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.PaintingBoard.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(owner==null||owner.IsPaused)return;foreach(var m in marks.Values)if(m.revealed&&m.age<.26f){m.age+=Time.deltaTime;Render(m);}}
```

### Render · L26

方法 SHA-256：`44ceaf54f9245a88862796488c30feda89550bfaf17866468e7edb0719937db5`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.PaintingBoard.Render(BounceGarden.PaintingBoard.Mark)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/PaintingBoard.cs:23` · `PaintingBoard.Reveal` → `Render(mark)`
- `Assets/Scripts/World/PaintingBoard.cs:25` · `PaintingBoard.Update` → `Render(m)`

```csharp
void Render(Mark mark){float u=Mathf.Clamp01(mark.age/.26f);float scale=.4f+.6f*(1-Mathf.Pow(1-u,3))+.06f*Mathf.Sin(u*Mathf.PI);mark.renderer.transform.localScale=mark.scale*scale;mark.renderer.color=new Color(1,1,1,PaintOpacity*Mathf.Min(1,u*3));}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public const float PaintOpacity=.45f;
```
```csharp
readonly Dictionary<int,Mark> marks=new Dictionary<int,Mark>();
```
```csharp
GameSession owner;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
