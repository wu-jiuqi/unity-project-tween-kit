# ChainBondView · ChainCubeArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/ChainCubeArt.cs`
文件 SHA-256：`ad5006d51aac98c515c5957af427ccbe1e50c4e37299633038dc1b0c928abc76`
[原始源码快照](../source/Assets/Scripts/World/ChainCubeArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L46

方法 SHA-256：`0bfd87079148fee4e79ac2f1888510bbdcb395ef819d9aeb5928172ebe5a2a72`
时间来源：Time.deltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.ChainBondView.Update()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/ChainCubeArt.cs:45` · `ChainBondView.Initialize` → `Update()`

```csharp
void Update(){if(owner==null||!owner.IsPlaying||owner.IsPaused)return;age+=Time.deltaTime;for(int i=0;i<marks.Count;i++){if(bond.Members[i]==null)continue;Vector2 p=bond.Members[i].transform.position;marks[i].position=p;marks[i].localScale=Vector3.one*JellyPowerArt.MarkerScale*(1+Mathf.Sin(age*5+i)*.015f);if(i==0)continue;Vector2 q=bond.Members[i-1].transform.position,delta=p-q;var line=lines[i-1];JellyPowerArt.BeamPose(line,p,q,.045f);line.GetComponent<SpriteRenderer>().color=new Color(1,1,1,.62f+Mathf.Sin(age*4+i)*.12f);}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
GameSession owner;
```
```csharp
ChainBond bond;
```
```csharp
readonly List<Transform> lines=new List<Transform>();
```
```csharp
readonly List<Transform> marks=new List<Transform>();
```
```csharp
float age;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
