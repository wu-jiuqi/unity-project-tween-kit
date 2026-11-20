# PixelPieceArt · PixelPieceArt

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/PixelPieceArt.cs`
文件 SHA-256：`69a1eb886f89cc6c67172e351929a52e2602617f5da232407fbe15cad163abb1`
[原始源码快照](../source/Assets/Scripts/World/PixelPieceArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Points · L7

方法 SHA-256：`84e357bf2f95086a49583d9a93660ee74d0e3b5c03647cb8f2f41083f03d8efb`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.PixelPieceArt.Points(int, UnityEngine.Vector2)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/PixelDropSession.cs:24` · `PixelDropBody.Initialize` → `PixelPieceArt.Points(shape,size)`

```csharp
public static Vector2[] Points(int shape,Vector2 size){var p=new List<Vector2>();if(shape==4){p.Add(new Vector2(-.46f,-.45f));p.Add(new Vector2(.46f,0));p.Add(new Vector2(-.46f,.45f));}
 else if(shape==3){p.Add(new Vector2(-.46f,-.45f));p.Add(new Vector2(.46f,-.45f));p.Add(new Vector2(0,.48f));}
 else if(shape==2){for(int i=0;i<16;i++){float a=i*Mathf.PI*2/16;p.Add(new Vector2(Mathf.Cos(a)*.48f,Mathf.Sin(a)*.43f*(.6f+.4f*Mathf.Abs(Mathf.Sin(a)))));}}
 else if(shape==1){for(int i=0;i<20;i++){float a=i*Mathf.PI*2/20;p.Add(new Vector2(Mathf.Cos(a)*.47f,Mathf.Sin(a)*.47f));}}
 else{for(int i=0;i<4;i++)for(int k=0;k<5;k++){float a=(i*90+k*22.5f)*Mathf.Deg2Rad;Vector2 c=new Vector2(i==0||i==3?.33f:-.33f,i<2?.33f:-.33f);p.Add(c+new Vector2(Mathf.Cos(a),Mathf.Sin(a))*.14f);}}
 for(int i=0;i<p.Count;i++)p[i]=Vector2.Scale(p[i],size);return p.ToArray();}
```

### Sprite · L14

方法 SHA-256：`222094ca231d2441e21fd9abb22d9e27453bdcdf27192b2636082d8b11e30802`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Exp
解析符号：`BounceGarden.PixelPieceArt.Sprite(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/PixelDropSession.cs:29` · `PixelDropBody.Initialize` → `PixelPieceArt.Sprite(shape)`

```csharp
public static Sprite Sprite(int shape){if(sprites.TryGetValue(shape,out var s))return s;var texture=new Texture2D(128,128,TextureFormat.RGBA32,false);texture.filterMode=FilterMode.Bilinear;var outline=Points(shape,Vector2.one);
 for(int y=0;y<128;y++)for(int x=0;x<128;x++){Vector2 p=new Vector2((x+.5f)/128-.5f,(y+.5f)/128-.5f);bool hit=Inside(p,outline);float rim=Inside(p/ .88f,outline)?1:.72f;float shine=Mathf.Exp(-((p.x+.19f)*(p.x+.19f)*100+(p.y-.23f)*(p.y-.23f)*220));float light=Mathf.Clamp01((.63f+p.y*.28f)*rim+shine*.38f);texture.SetPixel(x,y,new Color(light*.8f,light,1,hit?1:0));}
 texture.Apply();s=UnityEngine.Sprite.Create(texture,new Rect(0,0,128,128),Vector2.one*.5f,128);sprites[shape]=s;return s;}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
static readonly Dictionary<int,Sprite> sprites=new Dictionary<int,Sprite>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
