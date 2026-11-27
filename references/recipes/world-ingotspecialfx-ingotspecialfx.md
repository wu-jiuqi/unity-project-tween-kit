# IngotSpecialFx · IngotSpecialFx

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/IngotSpecialFx.cs`
文件 SHA-256：`7d966bc57976e68319455719f3db18cbe952fef8a2a46177411d3e979ca0acac`
[原始源码快照](../source/Assets/Scripts/World/IngotSpecialFx.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Play · L15

方法 SHA-256：`31b8f1dc0b9f0e4a28b9ff0a82e542c8f9f0e5ae439e219bcef24ca38d117685`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.IngotSpecialFx.Play(BounceGarden.GameSession, UnityEngine.Transform, string, UnityEngine.Vector2, float, int, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/SevenSpecialSession.cs:101` · `GameSession.SevenRelease` → `IngotSpecialFx.Play(this,world,"merge",p,1.75f,ingot?partnerColor:color)`
- `Assets/Scripts/Core/SevenSpecialSession.cs:143` · `GameSession.SevenComboArea` → `IngotSpecialFx.Play(this,world,"bomb",origin,pitch*square,colorB)`
- `Assets/Scripts/Core/SevenSpecialSession.cs:146` · `GameSession.SevenComboArea` → `IngotSpecialFx.Play(this,world,"merge",origin,pitch*2,colorB)`
- `Assets/Scripts/World/IngotSpecialFx.cs:16` · `IngotSpecialFx.Skill` → `Play(g,root,"flower",p,size*1.8f,accent)`
- `Assets/Scripts/World/IngotSpecialFx.cs:16` · `IngotSpecialFx.Skill` → `Play(g,root,"bomb",p,size*1.7f,accent)`

```csharp
public static IngotSpecialFx Play(GameSession game,Transform root,string key,Vector2 p,float size,int accent=-1,float duration=.76f){var f=new GameObject("Ingot skill sequence · "+key).AddComponent<IngotSpecialFx>();f.transform.SetParent(root,false);f.transform.position=p;f.owner=game;counts[game]=Alive(game)+1;f.generation=game.Generation;f.key=key;f.size=size;f.duration=duration;f.face=f.gameObject.AddComponent<SpriteRenderer>();f.face.sprite=Frame(key,0);f.face.sortingOrder=80;var t=f.face.sprite.texture;if(!materials.TryGetValue(t,out var m)){m=new Material(Resources.Load<Shader>("IngotFx")){mainTexture=t};materials[t]=m;}f.face.sharedMaterial=m;var block=new MaterialPropertyBlock();block.SetColor("_Accent",accent<0?Color.white:SevenSpecialArt.Palette[Mathf.Clamp(accent,0,4)]);block.SetFloat("_Mix",accent<0?0:.24f);f.face.SetPropertyBlock(block);f.Pose();return f;}
```

### Update · L20

方法 SHA-256：`dd48231602816cd00026ee07415370d00e68438dd91d49dc2a49c63082aa2a9d`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.IngotSpecialFx.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(BalanceClock.Active)return;Advance(Time.unscaledDeltaTime);}
```

## 对象层级和素材入口

- L15：`f.transform.SetParent(root,false)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(!ReferenceEquals(owner,null)&&counts.TryGetValue(owner,out int n)){if(n<=1)counts.Remove(owner);else counts[owner]=n-1;}}
```

## 源字段与依赖

```csharp
static readonly Dictionary<string,Sprite[]> sheets=new Dictionary<string,Sprite[]>();
```
```csharp
static readonly Dictionary<Texture,Material> materials=new Dictionary<Texture,Material>();
```
```csharp
public static readonly string[] Keys={"laser","bomb","flower","merge"};
```
```csharp
static readonly Dictionary<GameSession,int> counts=new Dictionary<GameSession,int>();
```
```csharp
GameSession owner;
```
```csharp
int generation;
```
```csharp
SpriteRenderer face;
```
```csharp
string key;
```
```csharp
float age,duration,size,width;
```
```csharp
Vector2 from,to;
```
```csharp
bool beam;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
