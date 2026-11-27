# WorldFx · WorldArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/WorldArt.cs`
文件 SHA-256：`b920bb3d7d1fcb01e8f3a036033a05004ca2c1b7e51c2fe1d0c59fdb195e4954`
[原始源码快照](../source/Assets/Scripts/World/WorldArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Emit · L102

方法 SHA-256：`cebaf1a11de723856114b2531026c30240d00325d218f544137cff411e46a686`
时间来源：Time.time
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.WorldFx.Emit(UnityEngine.Vector2, string, UnityEngine.Color, string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/GameSession.cs:307` · `GameSession.EmitFeedback` → `fx.Emit(position,cue,color,appearance)`

```csharp
public void Emit(Vector2 position,string cue,Color color,string appearance=null){
            if(!FeedbackProfile.Active.enabledFeedback)return;float cell=Owner!=null?Owner.Board.CellWidth:.73f;
            if(cue=="hit"){var index=Owner.Board.Nearest(position);if(pulse.TryGetValue(index,out float prior)&&Time.time-prior<.05f)return;pulse[index]=Time.time;Add("impact_star",position,.10f,cell*.34f,Color.white,Vector2.zero,0);return;}
            if(cue=="break"&&appearance==JellyBrickArt.SpriteName)return;
            if(cue=="break"){
                Add("impact_star",position,.10f,cell*.38f,Color.white,Vector2.zero,0);Add("shockwave_ring",position,.16f,cell*.68f,new Color(1,.94f,.72f,.65f),Vector2.zero,0);RingCount++;
                string debris=appearance=="blue_star"?"debris_blue":appearance=="orange_star"?"debris_orange":"debris_neutral";Color tint=debris=="debris_neutral"?color:Color.white;string pastelColor=appearance==null?"":appearance.Replace("brick_blank_","").Replace("brick_","");if(pastelColor=="blue"||pastelColor=="mint"||pastelColor=="blush"||pastelColor=="yellow"||pastelColor=="lavender"){debris="pastel/fx_shard_"+pastelColor;tint=Color.white;}
                for(int i=0;i<4;i++){float angle=(i+.25f)*Mathf.PI*.5f;Add(debris,position,.32f,cell*R(.08f,.13f),tint,new Vector2(Mathf.Cos(angle),Mathf.Sin(angle))*cell*R(2.2f,3.2f),cell*8);}
                Add("four_point_spark",position,.17f,cell*.08f,new Color(1,.94f,.7f,.75f),Vector2.up*cell,0);return;
            }
            if(cue=="shoot"){Add("impact_star",position,.07f,cell*.18f,new Color(1,.93f,.6f,.85f),Vector2.zero,0);Add("short_streak",position,.08f,cell*.24f,new Color(1,.95f,.7f,.5f),Vector2.down*cell,0);return;}
            if(cue=="splash")for(int i=0;i<4;i++)Add("four_point_spark",position,.2f,cell*.09f,new Color(.6f,.9f,1,.6f),new Vector2(R(-1,1),R(1,2))*cell,cell*9);
        }
```

### Update · L120

方法 SHA-256：`ad60b153d186f1aefd32b89a9ad5a058fad09ea73971395e21f55e99091ae14d`
时间来源：PresentationDelta, Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.WorldFx.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(Owner!=null&&Owner.IsPaused)return;float dt=Owner!=null?Owner.PresentationDelta(Time.deltaTime):Time.deltaTime;for(int i=live.Count-1;i>=0;i--){var p=live[i];p.left-=dt;if(p.left<=0){Recycle(i);continue;}p.velocity.y-=p.gravity*dt;p.renderer.transform.position+=(Vector3)(p.velocity*dt);p.renderer.transform.Rotate(0,0,p.spin*dt);float age=1-p.left/p.life;float alpha=p.key.StartsWith("debris")?Mathf.Clamp01((1-age)/.4f):1-age;p.renderer.color=new Color(p.color.r,p.color.g,p.color.b,p.color.a*alpha);SetSize(p,p.size*(p.key=="shockwave_ring"?Mathf.Lerp(.2f/.68f,1,age):p.key.StartsWith("debris")?Mathf.Lerp(1,.6f,age):1));}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
readonly List<Particle> live=new List<Particle>();
```
```csharp
readonly Stack<Particle> pool=new Stack<Particle>();
```
```csharp
readonly Dictionary<Vector2Int,float> pulse=new Dictionary<Vector2Int,float>();
```
```csharp
readonly System.Random random=new System.Random(6006);
```
```csharp
JellyPopPool jelly;
```
```csharp
public GameSession Owner;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
