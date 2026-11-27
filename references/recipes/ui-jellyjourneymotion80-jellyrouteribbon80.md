# JellyRouteRibbon80 · JellyJourneyMotion80

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/JellyJourneyMotion80.cs`
文件 SHA-256：`03c5ac68c7c3f15a1d9602399a3b064b6ae514ff746384240e55b113f6f2162b`
[原始源码快照](../source/Assets/Scripts/UI/JellyJourneyMotion80.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Configure · L61

方法 SHA-256：`67c925b62fbfd2d0965d042f2e9dd40120bd7868c523e00f4f4da3fc132120bf`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyRouteRibbon80.Configure(BounceGarden.JellyJourney80, int, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyJourney80.cs:65` · `JellyJourney80.RefreshVisible` → `route.Configure(this,min,max)`

```csharp
public void Configure(JellyJourney80 map,int min,int max){owner=map;if(textureSprite==null)textureSprite=JourneyGardenArt043.Get("route-material");first=Mathf.Max(-1,min-1);last=Mathf.Min(map.LevelCount,max+1);raycastTarget=false;SetMaterialDirty();SetVerticesDirty();}
```

### RefreshMotion · L62

方法 SHA-256：`21b93ffdea3cd4a054742a46591edc6579730290acbf1890358b16b9c007a424`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyRouteRibbon80.RefreshMotion()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyJourney80.cs:74` · `JellyJourney80.LateUpdate` → `route.RefreshMotion()`
- `Assets/Scripts/UI/JellyJourney80.cs:74` · `JellyJourney80.LateUpdate` → `route.RefreshMotion()`
- `Assets/Scripts/UI/JellyJourney80.cs:77` · `JellyJourney80.CancelMotion` → `route.RefreshMotion()`

```csharp
public void RefreshMotion(){SetVerticesDirty();}
```

### OnPopulateMesh · L63

方法 SHA-256：`5fd4ae379b30ff783bd0f6d7dc6e94934096a165d6ebef9d5a15594fdbbeceb4`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.JellyRouteRibbon80.OnPopulateMesh(UnityEngine.UI.VertexHelper)`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
protected override void OnPopulateMesh(VertexHelper vh){vh.Clear();VertexCount=0;if(owner==null||textureSprite==null)return;Rect uv=textureSprite.textureRect;float tw=textureSprite.texture.width,th=textureSprite.texture.height;float u0=uv.xMin/tw,u1=uv.xMax/tw,v=(uv.y+uv.height*.5f)/th;const int samples=36;int row=0;
  for(int leg=first;leg<last;leg++){Vector2 a=owner.RoutePoint(leg),d=owner.RoutePoint(leg+1);float dy=d.y-a.y;Vector2 b=a+Vector2.up*(dy*.46f),c=d-Vector2.up*(dy*.46f);for(int j=0;j<=samples;j++){if(leg>first&&j==0)continue;float t=j/(float)samples,s=1-t;Vector2 p=s*s*s*a+3*s*s*t*b+3*s*t*t*c+t*t*t*d;Vector2 tangent=3*s*s*(b-a)+6*s*t*(c-b)+3*t*t*(d-c);var normal=new Vector2(tangent.y,-tangent.x).normalized;float width=72*(1+Mathf.Sin(owner.MotionClock*2.3f+p.y*.008f)*.055f+Mathf.Abs(owner.DragPull)*.12f);var left=p-normal*width*.5f;var right=p+normal*width*.5f;vh.AddVert(left,color,new Vector2(u0,v));vh.AddVert(right,color,new Vector2(u1,v));if(row>0){int k=row*2;vh.AddTriangle(k-2,k-1,k);vh.AddTriangle(k,k-1,k+1);}row++;}}
  VertexCount=row*2;
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
JellyJourney80 owner;
```
```csharp
Sprite textureSprite;
```
```csharp
int first,last;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
