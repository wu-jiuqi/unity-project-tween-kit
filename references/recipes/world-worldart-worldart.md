# WorldArt · WorldArt

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/WorldArt.cs`
文件 SHA-256：`f28868835b1d5c89d381e526d38297ffbc75224cb97d79e131b05743f5f962e8`
[原始源码快照](../source/Assets/Scripts/World/WorldArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Sprite · L15

方法 SHA-256：`54e34705d2bc0b421484ad13aaf0d43c8b2387513f74411f7d53a308566ad5f8`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Exp, Mathf.Sin
解析符号：`BounceGarden.WorldArt.Sprite(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/JuiceGardenView.cs:49` · `JuiceGardenView.TrackBall` → `WorldArt.Sprite("ring")`
- `Assets/Scripts/World/WorldArt.cs:76` · `WorldArt.Draw` → `Sprite(sprite)`

```csharp
public static Sprite Sprite(string kind)
        {
            if (cache.TryGetValue(kind, out Sprite known)) return known;
            var production=ProductionArt.Get(kind);
            if(production!=null){cache[kind]=production;return production;}
            var supplied = Resources.Load<Sprite>("Art/" + kind);
            if (supplied != null) { cache[kind] = supplied; return supplied; }
            var texture = Resources.Load<Texture2D>("Art/" + kind);
            if (texture != null) { supplied = UnityEngine.Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f,.5f), 100); cache[kind] = supplied; return supplied; }
            const int n = 96;
            texture = new Texture2D(n,n,TextureFormat.RGBA32,false) { name = "Fallback_" + kind, filterMode = FilterMode.Bilinear, wrapMode = TextureWrapMode.Clamp };
            var pixels = new Color[n*n];
            for (int y=0;y<n;y++) for(int x=0;x<n;x++)
            {
                float u=(x+.5f)/n*2-1, v=(y+.5f)/n*2-1;
                float edge=1-Mathf.Max(Mathf.Abs(u),Mathf.Abs(v));
                Color c=Color.white;
                if(kind=="ring")
                {float r=Mathf.Sqrt(u*u+v*v);c=new Color(1,1,1,Mathf.Clamp01((.12f-Mathf.Abs(r-.72f))*35));}
                else if(kind=="ball" || kind=="circle")
                {
                    float r=Mathf.Sqrt(u*u+v*v); float a=Mathf.Clamp01((1-r)*n*.5f);
                    float shade=kind=="ball" ? .8f+.2f*v : 1;
                    c=new Color(shade,shade,shade,a);
                    if(kind=="ball") { float gleam=Mathf.Exp(-((u+.3f)*(u+.3f)+(v-.35f)*(v-.35f))*30); c=Color.Lerp(c,Color.white,gleam*.8f); c.a=a; }
                }
                else if(kind=="star")
                {
                    float r=Mathf.Sqrt(u*u+v*v); float a=Mathf.Atan2(v,u);
                    float lim=.54f+.25f*Mathf.Cos(a*5-1.57f); c.a=Mathf.Clamp01((lim-r)*40);
                }
                else if(kind=="grass")
                {
                    float noise=Mathf.PerlinNoise(x*.24f,y*.24f); bool check=((x/16+y/16)%2)==0;
                    c=new Color(.32f+noise*.035f,.67f+noise*.05f,.27f+noise*.04f); if(check)c*=1.035f; c.a=1;
                }
                else if(kind=="water")
                {
                    float wave=Mathf.Sin(x*.19f+y*.5f)*.025f;
                    c=new Color(.13f+wave,.63f+v*.04f+wave,.86f+wave,1);
                    if(v>.8f)c=Color.Lerp(c,Color.white,.5f);
                }
                else
                {
                    float qx=Mathf.Max(Mathf.Abs(u)-.79f,0), qy=Mathf.Max(Mathf.Abs(v)-.79f,0);
                    float a=Mathf.Clamp01((.2f-Mathf.Sqrt(qx*qx+qy*qy))*50);
                    float shade=.86f+.14f*(v*.5f+.5f);
                    if(edge<.12f)shade*=.78f;
                    if(v>.72f && edge>.12f)shade=1;
                    c=new Color(shade,shade,shade,a);
                }
                pixels[y*n+x]=c;
            }
            texture.SetPixels(pixels);texture.Apply(false,true);
            supplied=UnityEngine.Sprite.Create(texture,new Rect(0,0,n,n),new Vector2(.5f,.5f),n);
            cache[kind]=supplied;return supplied;
        }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
static readonly Dictionary<string, Sprite> cache = new Dictionary<string, Sprite>();
```
```csharp
public static readonly Color[] Palette = {
            new Color(.58f,.75f,.96f), new Color(1f,.85f,.47f), new Color(.97f,.65f,.67f),
            new Color(.74f,.62f,.90f), new Color(.62f,.88f,.75f), new Color(.96f,.78f,.63f)
        };
```
```csharp
static Material boardMaterial,worldText;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
