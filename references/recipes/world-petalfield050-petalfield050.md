# PetalField050 · PetalField050

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/PetalField050.cs`
文件 SHA-256：`287a2fbc1f667f622380c6ee90d5cfd033f0c4bebb1cfa5de645ad392bfee129`
[原始源码快照](../source/Assets/Scripts/World/PetalField050.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Initialize · L13

方法 SHA-256：`59f10556b699aff5f01e0945212467211b078201b93f26f348f4b8f57a5c8dea`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.PetalField050.Initialize(BounceGarden.GameSession)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Initialize(GameSession owner){game=owner;material=new Material(Shader.Find("Sprites/Default"));var rng=new System.Random(4418);
  for(int row=0;row<Rows;row++)for(int col=0;col<Columns;col++){int i=row*Columns+col;var p=new Petal();petals[i]=p;p.home=new Vector2(Left+(col+.5f)*DX+((float)rng.NextDouble()-.5f)*DX*.5f,Bottom+(row+.5f)*DY+((float)rng.NextDouble()-.5f)*DY*.5f);p.width=Mathf.Lerp(.19f,.34f,(float)rng.NextDouble());p.angle=(float)rng.NextDouble()*360;p.life=Mathf.Lerp(.8f,1.4f,(float)rng.NextDouble());p.height=Mathf.Lerp(.24f,.5f,(float)rng.NextDouble());p.spin=Mathf.Lerp(-120,120,(float)rng.NextDouble());
   var sprite=BattleGardenArt050.Get("petal-"+(i%3));p.shadow=Draw("Petal ground shadow "+i,sprite,-25);p.face=Draw("Reusable petal "+i,sprite,-24);Render(p);}
 }
```

### Render · L26

方法 SHA-256：`3c78503b2fea28ca5c118f886022844dbcad1436eede7e341b965a2cb17619a8`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.PetalField050.Render(BounceGarden.PetalField050.Petal)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/PetalField050.cs:15` · `PetalField050.Initialize` → `Render(p)`
- `Assets/Scripts/World/PetalField050.cs:31` · `PetalField050.Update` → `Render(p)`

```csharp
void Render(Petal p){float u=p.flying?Mathf.Clamp01(p.age/p.life):0;float arc=p.flying?Mathf.Sin(u*Mathf.PI):0;Vector2 ground=p.home+p.drift*arc;float height=arc*p.height;float flutter=p.flying?Mathf.Sin(u*15+p.angle)*arc:0;
  p.face.transform.localPosition=ground+Vector2.up*height+Vector2.right*flutter*.035f;p.face.transform.localRotation=Quaternion.Euler(0,0,p.angle+(p.flying?p.spin*u+flutter*14:0));float scale=p.width/p.face.sprite.bounds.size.x;p.face.transform.localScale=new Vector3(scale*(p.flying?Mathf.Lerp(.35f,1,Mathf.Abs(Mathf.Cos(u*9))):1),scale*(1+arc*.12f),1);p.face.color=new Color(1,1,1,p.flying?.86f:.22f);
  p.shadow.transform.localPosition=ground-Vector2.up*.015f;p.shadow.transform.localRotation=Quaternion.Euler(0,0,p.angle);p.shadow.transform.localScale=new Vector3(scale*(1+arc*.3f),scale*.45f,1);p.shadow.color=new Color(.46f,.36f,.49f,p.flying?Mathf.Lerp(.13f,.045f,arc):.015f);
 }
```

### Update · L30

方法 SHA-256：`40a01afb5a9bddebbea57db45046b2662fe510e620b31307e13d1cd89f37f5c0`
时间来源：PresentationDelta, Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.PetalField050.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(game==null||game.IsPaused)return;float dt=game.PresentationDelta(Time.deltaTime);if(dt<=0)return;clock+=dt;frame++;budget=8;SampledBalls=0;if(game.IsPlaying&&FeedbackProfile.Active.enabledFeedback)game.VisitPetalBalls(this);
  for(int i=0;i<petals.Length;i++){var p=petals[i];if(!p.flying)continue;p.age+=dt;if(p.age>=p.life){p.flying=false;p.cooldown=clock+.75f;ActivePetals--;LandCount++;}Render(p);}
 }
```

## 对象层级和素材入口

- L13：`Shader.Find("Sprites/Default")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(material!=null)Destroy(material);}
```

## 源字段与依赖

```csharp
const int Columns=12,Rows=20;
```
```csharp
public const int Capacity=Columns*Rows,AirborneLimit=32;
```
```csharp
const float Left=-4.2f,Bottom=-9.5f,DX=8.4f/Columns,DY=16.6f/Rows;
```
```csharp
readonly Petal[] petals=new Petal[Capacity];
```
```csharp
readonly Dictionary<int,Track> tracks=new Dictionary<int,Track>(128);
```
```csharp
GameSession game;
```
```csharp
int frame,budget;
```
```csharp
float clock;
```
```csharp
Material material;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
