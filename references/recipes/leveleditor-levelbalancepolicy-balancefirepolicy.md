# BalanceFirePolicy · LevelBalancePolicy

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LevelEditor/LevelBalancePolicy.cs`
文件 SHA-256：`00a1037baefedbdfa866b1e5c4163c242e94d2a887fb0ab3c3e9a3a042dfafc0`
[原始源码快照](../source/Assets/Scripts/LevelEditor/LevelBalancePolicy.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Choose · L37

方法 SHA-256：`1a9935ddfc2373067184b1b66080bf41ec1e8c20b620092d0bcb8e91008c3b50`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.LevelEditing.BalanceFirePolicy.Choose(BounceGarden.GameSession, BounceGarden.LevelEditing.BalanceWeights, System.Random)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/LevelBalanceRunner.cs:66` · `LevelBalanceRunner.Run` → `BalanceFirePolicy.Choose(game,request.weights,new System.Random(unchecked(seed^(used+1)*1640531527)))`
- `Assets/Scripts/LevelEditor/LevelBalanceRunner.cs:95` · `LevelBalanceRunner.Choose` → `BalanceFirePolicy.Choose(game,new BalanceWeights(),random)`

```csharp
// All three firing modes call this same visible-board heuristic with identical weights.
 public static BalanceAim Choose(GameSession g,BalanceWeights w,System.Random random){
  var bricks=g.CollectionBricks.Where(b=>b!=null&&b.Alive&&b.gameObject.activeInHierarchy&&b.GetComponent<BoxCollider2D>()?.enabled!=false&&b.GetComponent<ChainTileVisibility>()?.Hidden!=true).ToArray();
  var positions=new Vector2[bricks.Length];var halves=new Vector2[bricks.Length];var gains=new Vector2[bricks.Length];
  for(int n=0;n<bricks.Length;n++){var b=bricks[n];var body=b.GetComponent<Rigidbody2D>();positions[n]=body!=null?body.position:(Vector2)b.transform.position;var box=b.GetComponent<BoxCollider2D>();halves[n]=box!=null?box.size*.5f:Vector2.one*.38f;gains[n]=g.BalanceVisibleGains(b);}
  var tools=g.ChainTools.Where(t=>!t.Consumed&&!t.Waiting&&t.View!=null&&t.View.gameObject.activeInHierarchy&&t.View.GetComponent<ChainTileVisibility>()?.Hidden!=true).ToArray();
  var toolValues=new float[tools.Length];
  for(int t=0;t<tools.Length;t++){
   var tool=tools[t];float value=0;
   for(int n=0;n<bricks.Length;n++){
    var d=positions[n]-tool.Position;bool covered=tool.Kind==SevenSpecialRules.Horizontal?Mathf.Abs(d.y)<.5f:tool.Kind==SevenSpecialRules.Vertical?Mathf.Abs(d.x)<.5f:tool.Kind==SevenSpecialRules.Cross?Mathf.Abs(d.x)<.5f||Mathf.Abs(d.y)<.5f:tool.Kind==SevenSpecialRules.Bomb?Mathf.Abs(d.x)<1.3f&&Mathf.Abs(d.y)<1.3f:bricks[n].Definition.colorIndex==tool.ColorIndex;
    if(covered)value+=gains[n].x+gains[n].y;
   }
   int partners=tools.Count(other=>other!=tool&&SevenSpecialRules.Nearby(other.Position,tool.Position));
   toolValues[t]=w.special*(1+Mathf.Min(3,value/8))+w.combo*Mathf.Min(2,partners);
  }
  var best=new BalanceAim{Direction=Vector2.up,Utility=float.NegativeInfinity};float bestTool=0;
  for(int i=0;i<w.directions;i++){
   float angle=Mathf.Lerp(15,165,(i+.2f+(float)random.NextDouble()*.6f)/w.directions)*Mathf.Deg2Rad;var direction=new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));var origin=g.SpawnPosition(direction);
   int hit=-1;float nearest=100;
   for(int n=0;n<bricks.Length;n++)if(RayBox(origin,direction,positions[n],halves[n],out float distance)&&distance<nearest){nearest=distance;hit=n;}
   float utility=0,specialValue=0;int kind=0;
   if(hit>=0){var first=bricks[hit];bool immune=g.OpeningYarnBrick(first)||g.OpeningCookieBrick(first)||g.OpeningCatBrick(first);float clear=immune?0:1f/Mathf.Max(1,first.CurrentHealth);
    utility=w.direct*clear+w.target*gains[hit].x*clear;
    if(clear>0)for(int n=0;n<bricks.Length;n++)if(n!=hit&&Adjacent(positions[hit],positions[n],halves[n]))utility+=w.adjacent*gains[n].y*clear;
   }
   for(int t=0;t<tools.Length;t++){var d=tools[t].Position-origin;float along=Vector2.Dot(d,direction);if(along>0&&along<nearest+.4f&&Mathf.Abs(d.x*direction.y-d.y*direction.x)<.45f){specialValue=Mathf.Max(specialValue,toolValues[t]);kind=tools[t].Kind;}}
   bestTool=Mathf.Max(bestTool,specialValue);utility+=specialValue+(float)random.NextDouble()*w.noise;
   if(utility>best.Utility)best=new BalanceAim{Direction=direction,Utility=utility,Kind=kind};
  }
  best.SpecialOpportunity=Mathf.Clamp01(bestTool/Mathf.Max(.01f,w.special*3));return best;
 }
```

## 对象层级和素材入口

- L56：`RayBox(origin,direction,positions[n],halves[n],out float distance)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
