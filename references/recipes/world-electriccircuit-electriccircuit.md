# ElectricCircuit · ElectricCircuit

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/ElectricCircuit.cs`
文件 SHA-256：`308a24277e831596e71dca97677e9fb84924a133b875d43483e8d90b2a3812c2`
[原始源码快照](../source/Assets/Scripts/World/ElectricCircuit.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L25

方法 SHA-256：`bfed52586bbf425bf85f29dbcf82cdb1fa1597516a8c9190d28491d809a70abc`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ElectricCircuit.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(Owner==null||Owner.IsPaused)return;cooldown=Mathf.Max(0,cooldown-Time.deltaTime);clock+=Time.deltaTime;remaining=Mathf.Max(0,remaining-Time.deltaTime);UpdateLines();}
```

### UpdateLines · L26

方法 SHA-256：`6cb7bdc261904a57224ea20a101075402e63b414078ba63044a167673e1670b1`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.ElectricCircuit.UpdateLines()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/ElectricCircuit.cs:18` · `ElectricCircuit.ClearVisuals` → `UpdateLines()`
- `Assets/Scripts/World/ElectricCircuit.cs:22` · `ElectricCircuit.PreviewKnownConnections` → `UpdateLines()`
- `Assets/Scripts/World/ElectricCircuit.cs:25` · `ElectricCircuit.Update` → `UpdateLines()`

```csharp
void UpdateLines(){bool show=Active&&FeedbackProfile.Active.enabledFeedback;
  foreach(var p in pillars)p.glow.color=new Color(.25f,1,1,show?.55f:0);
  foreach(var link in links){link.glow.enabled=link.halo.enabled=link.core.enabled=show;if(!show)continue;Vector3 a=pillars[link.from].tip.position,b=pillars[link.to].tip.position;Vector3 side=Vector3.Cross((b-a).normalized,Vector3.forward);
   for(int j=0;j<33;j++){float t=j/32f;float noise=j==0||j==32?0:(Mathf.Sin(j*12.41f+Mathf.Floor(clock*30)*2.3f+link.from)*.12f+(j%2==0?.055f:-.055f));Vector3 point=Vector3.Lerp(a,b,t)+side*noise;link.glow.SetPosition(j,point);link.halo.SetPosition(j,point);link.core.SetPosition(j,point);}}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
readonly List<Pillar> pillars=new List<Pillar>();
```
```csharp
readonly List<Link> links=new List<Link>();
```
```csharp
static Material lineMaterial;
```
```csharp
float remaining,clock,cooldown;
```
```csharp
public GameSession Owner;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
