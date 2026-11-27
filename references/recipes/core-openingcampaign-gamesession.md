# GameSession · OpeningCampaign

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/OpeningCampaign.cs`
文件 SHA-256：`ec6c56f66fa068fa076f3d3694423c25ad1b929775f06c290c3103e6f25b4471`
[原始源码快照](../source/Assets/Scripts/Core/OpeningCampaign.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### InitializeOpeningGridSteps · L63

方法 SHA-256：`a449cf3b0df472ea68d594c85b07b74a772e119749a2f9fa10ca6d605afc0f35`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameSession.InitializeOpeningGridSteps(bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/GameSession.cs:148` · `GameSession.BuildSession` → `InitializeOpeningGridSteps(true)`
- `Assets/Scripts/Core/OpeningCampaign.cs:62` · `GameSession.InitializeOpeningGrid` → `InitializeOpeningGridSteps(false)`

```csharp
System.Collections.IEnumerator InitializeOpeningGridSteps(bool incremental){
  var s=OpeningStage;InitializeOpeningTargets();InitializeOpeningThird();
  var occupiedByBoxes=new HashSet<int>();foreach(var box in s.pieces.Where(p=>p.kind=="cookie_box")){SpawnOpeningCookieBox(box);for(int y=box.r;y<box.r+2;y++)for(int x=box.c;x<box.c+2;x++)occupiedByBoxes.Add(y*9+x);}
  for(int r=0;r<9;r++){if(incremental)yield return null;for(int c=0;c<9;c++){char code=s.rows[r][c];if(code=='.')continue;var p=s.pieces.FirstOrDefault(v=>v.c==c&&v.r==r);
   if(occupiedByBoxes.Contains(r*9+c))continue;
   if(p!=null&&p.kind=="copper_coin"){SpawnOpeningCopper(p);continue;}
   if(p!=null&&p.kind=="lucky_cat"){SpawnOpeningCat(p);continue;}
   if(p!=null&&p.kind!="yarn"&&p.kind!="ingot"){var tool=SpawnChainTool(OpeningCampaign.ToolKind(p.kind),ChainGridRules.Center(c,r),OpeningCampaign.Color(code.ToString()));tool.Ingot=p.appearance=="ingot";continue;}
   CreateBrick(new BrickSpawn{position=ChainGridRules.Center(c,r),size=Vector2.one*.76f,hp=p==null||p.kind=="ingot"?OpeningCampaign.InitialHp(s,c,r):1,colorIndex=p!=null?0:OpeningCampaign.Color(code.ToString()),referenceCol=c,referenceRow=r,juiceGroup=0,reveal=p!=null?"opening-yarn":"opening-N",kind=BrickKind.Normal},0);
   if(p!=null&&p.kind=="ingot"){OpeningSetIngot(CollectionBricks.Last());continue;}
   if(p!=null){var brick=CollectionBricks.Last();var view=brick.gameObject.AddComponent<OpeningYarnView>();view.Initialize(this,brick,p.layers);openingYarn[brick.Id]=view;}
  }}
  ChainMessage=s.name+" · "+s.focus;PlanChainRow();
 }
```

## 对象层级和素材入口

- L65：`SpawnOpeningCookieBox(box)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
int openingSupplySerial;
```
```csharp
long openingEliminationSerial,openingEliminationBatch;
```
```csharp
readonly Dictionary<long,int> openingEliminationStrength=new Dictionary<long,int>();
```
```csharp
readonly Dictionary<int,OpeningYarnView> openingYarn=new Dictionary<int,OpeningYarnView>();
```
```csharp
readonly List<OpeningYarnEffect> openingYarnEffects=new List<OpeningYarnEffect>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
