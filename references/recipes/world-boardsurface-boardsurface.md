# BoardSurface · BoardSurface

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/BoardSurface.cs`
文件 SHA-256：`962b9b0a2b93706d6cada8d24ef45541273d676f76eb7c8fd4ef295f2cdfc67a`
[原始源码快照](../source/Assets/Scripts/World/BoardSurface.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Initialize · L6

方法 SHA-256：`e61f4b177e0c796f0a5babedf2b0e9aa26ee4af57f3191bdd570acad228ba869`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BoardSurface.Initialize(BounceGarden.BoardSpace, bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/GameSession.cs:452` · `GameSession.BuildGarden` → `Ground.Initialize(Board,false)`

```csharp
public void Initialize(BoardSpace space,bool renderLegacy=true){Space=space;if(!Enabled||!renderLegacy)return;
  var filter=gameObject.AddComponent<MeshFilter>();var mesh=new Mesh();float x=10.8f*.5f,y=23.4f*.5f;
  mesh.vertices=new[]{new Vector3(-x,-y),new Vector3(x,-y),new Vector3(x,y),new Vector3(-x,y)};mesh.triangles=new[]{0,1,2,0,2,3};mesh.RecalculateBounds();filter.sharedMesh=mesh;
  var renderer=gameObject.AddComponent<MeshRenderer>();renderer.sortingOrder=-29;
  material=new Material(Resources.Load<Shader>("BoardGrass"));material.mainTexture=Resources.Load<Texture2D>("Art/Round5/grass_uniform_albedo");material.SetTexture("_Backdrop",ProductionArt.Get("game_background").texture);
  material.SetVector("_Board",new Vector4(space.Origin.x,space.Origin.y,space.U.x,space.V.y));material.SetFloat("_Phase",space.Phase);renderer.sharedMaterial=material;
 }
```

### QAParity · L14

方法 SHA-256：`4197aa93e2805c17ef9ca36920bb503911c02b1a7cfa9bb50d5eb978d2dbb7d7`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BoardSurface.QAParity(bool)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void QAParity(bool show){if(material!=null)material.SetFloat("_DebugParity",show?1:0);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(material!=null)Destroy(material);var f=GetComponent<MeshFilter>();if(f!=null&&f.sharedMesh!=null)Destroy(f.sharedMesh);}
```

## 源字段与依赖

```csharp
public static bool Enabled=true;
```
```csharp
Material material;
```
```csharp
GameObject debugRoot;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
