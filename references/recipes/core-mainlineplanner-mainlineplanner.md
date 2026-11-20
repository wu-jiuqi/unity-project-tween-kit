# MainlinePlanner · MainlinePlanner

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/MainlinePlanner.cs`
文件 SHA-256：`a0f199df5a8d766c778fe42b34b60c3d8a7a36839aa903adbf46e9b362000a13`
[原始源码快照](../source/Assets/Scripts/Core/MainlinePlanner.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Evaluate · L17

方法 SHA-256：`22580c235d5765a9cdbb5213a3798f202319f17bcb6f98563d4e751aadd846f0`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.MainlinePlanner.Evaluate(BounceGarden.GameSession, BounceGarden.BrickBody[], float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/MainlinePlanner.cs:14` · `MainlinePlanner.Choose` → `Evaluate(game,bricks,a)`

```csharp
static float Evaluate(GameSession game,BrickBody[] bricks,float angle){
  var targets=bricks.Select(b=>new Target{d=b.Definition,p=b.transform.position,hp=b.CurrentHealth,charge=game.CoreCharge(b),shell=b.ShellIntact}).ToArray();float score=0;
  var direction=new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad),Mathf.Sin(angle*Mathf.Deg2Rad));
  for(int ball=0;ball<5;ball++){
   Vector2 p=game.SpawnPosition(direction),v=direction;float distance=game.CurrentLevel.ballSpeed*9;int heat=0;
   for(int bounce=0;bounce<80&&distance>0;bounce++){
    float near=distance;Vector2 normal=Vector2.zero;Target hit=null;
    float wall=v.x>0?(3.938f-p.x)/v.x:v.x<0?(-3.938f-p.x)/v.x:float.MaxValue;if(wall>.0001f&&wall<near){near=wall;normal=v.x>0?Vector2.left:Vector2.right;}
    float top=v.y>0?(7.328f-p.y)/v.y:v.y<0?(-6.1f-p.y)/v.y:float.MaxValue;if(top>.0001f&&top<near){near=top;normal=v.y>0?Vector2.down:Vector2.up;}
    foreach(var t in targets)if(!t.dead){
     // Conservative expanded square; guide geometry is checked by real physics later.
     float half=t.d.kind==BrickKind.Guide?.269f:.3f;float h=half+FlightGeometry.Radius;Vector2 lo=t.p-Vector2.one*h,hi=t.p+Vector2.one*h;
     float tx1=v.x==0?float.NegativeInfinity:(lo.x-p.x)/v.x,tx2=v.x==0?float.PositiveInfinity:(hi.x-p.x)/v.x;
     float ty1=v.y==0?float.NegativeInfinity:(lo.y-p.y)/v.y,ty2=v.y==0?float.PositiveInfinity:(hi.y-p.y)/v.y;
     if(v.x==0&&(p.x<lo.x||p.x>hi.x)||v.y==0&&(p.y<lo.y||p.y>hi.y))continue;
     float tx=Mathf.Min(tx1,tx2),ty=Mathf.Min(ty1,ty2),entry=Mathf.Max(tx,ty),exit=Mathf.Min(Mathf.Max(tx1,tx2),Mathf.Max(ty1,ty2));
     if(entry>.0001f&&entry<=exit&&entry<near){near=entry;hit=t;normal=tx>ty?(v.x>0?Vector2.left:Vector2.right):(v.y>0?Vector2.down:Vector2.up);}
    }
    p+=v*near;distance-=near;if(normal==Vector2.zero||p.y<=-6.09f&&v.y<0)break;
    v=Vector2.Reflect(v,normal).normalized;p+=normal*.002f;
    if(hit==null)continue;heat++;int damage=heat>=8?3:heat>=4?2:1;
    if(hit.d.kind==BrickKind.Core){hit.charge++;score+=.35f;if(hit.charge<3)continue;damage=99;}
    var queue=new Queue<Target>();score+=Damage(hit,damage,queue);
    if(hit.d.kind==BrickKind.Resonance)foreach(var t in targets)if(t!=hit&&!t.dead&&t.d.kind==BrickKind.Resonance&&t.d.juiceGroup==hit.d.juiceGroup)score+=Damage(t,1,queue);
    while(queue.Count>0){var source=queue.Dequeue();var d=source.d;
     foreach(var t in targets)if(!t.dead){int dr=Mathf.Abs(d.referenceRow-t.d.referenceRow),dc=Mathf.Abs(d.referenceCol-t.d.referenceCol);bool affect=d.kind==BrickKind.Switch?t.d.kind==BrickKind.Seal&&t.d.juiceGroup==d.juiceGroup:d.kind==BrickKind.Rainbow?(d.axis==0?dr==0:dc==0):dr<=(d.kind==BrickKind.Core?2:1)&&dc<=(d.kind==BrickKind.Core?2:1);if(affect)score+=Damage(t,d.kind==BrickKind.Switch?99:2,queue);}
    }
   }
  }
  return score;
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
