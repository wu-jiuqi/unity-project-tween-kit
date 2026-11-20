# MainlineCanvas · MainlineArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/MainlineArt.cs`
文件 SHA-256：`2ebd3b54eaa82386d127f624a448b55496272a37109321ab3407fd6fc16f91b8`
[原始源码快照](../source/Assets/Scripts/World/MainlineArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L23

方法 SHA-256：`6dec3cdfc540c92cbdc52cc38d7f9fa89e682774569f139c6c2142c99b459977`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.MainlineCanvas.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(owner==null||owner.IsPaused)return;foreach(var m in marks.Values)if(m.revealed&&m.age<.26f){m.age+=Time.deltaTime;Render(m);}}
```

### Render · L24

方法 SHA-256：`8121960d788b7c1b9f7b450f514c07d796c586df0a417c043d461726eac08c8e`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.MainlineCanvas.Render(BounceGarden.MainlineCanvas.Mark)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/MainlineArt.cs:22` · `MainlineCanvas.Reveal` → `Render(m)`
- `Assets/Scripts/World/MainlineArt.cs:23` · `MainlineCanvas.Update` → `Render(m)`

```csharp
static void Render(Mark m){float u=Mathf.Clamp01(m.age/.26f);float s=.4f+.6f*(1-Mathf.Pow(1-u,3))+.06f*Mathf.Sin(u*Mathf.PI);m.renderer.transform.localScale=m.scale*s;m.renderer.color=new Color(1,1,1,.60f*Mathf.Min(1,u*3));}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
readonly Dictionary<int,Mark> marks=new Dictionary<int,Mark>();
```
```csharp
GameSession owner;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
