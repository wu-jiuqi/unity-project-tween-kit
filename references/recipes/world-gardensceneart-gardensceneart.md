# GardenSceneArt · GardenSceneArt

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/GardenSceneArt.cs`
文件 SHA-256：`e76f7ec60d4b65be18d14246b1cd3b7f7662d1d3e3f06029b69ee3ae96c25ae1`
[原始源码快照](../source/Assets/Scripts/World/GardenSceneArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Upload · L77

方法 SHA-256：`407439cbadb29d4bc590cff56e0f95bed2eae8e2fa7a46b7ae9b9c808c0a4438`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Exp, Mathf.Sin
解析符号：`BounceGarden.GardenSceneArt.Upload()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/GardenSceneArt.cs:62` · `GardenSceneArt.Initialize` → `Upload()`
- `Assets/Scripts/World/GardenSceneArt.cs:72` · `GardenSceneArt.TryTap` → `Upload()`
- `Assets/Scripts/World/GardenSceneArt.cs:94` · `GardenSceneArt.Update` → `Upload()`

```csharp
// These coefficients are constant across the whole painting. Compute them once
        // per frame instead of repeating trigonometry for every shaded phone pixel.
        void Upload()
        {
            float time=Mathf.Floor(clock*12)/12;
            for(int i=0;i<count;i++){
                float age=activity[i].x,cat=activity[i].y,phase=i*.83f;
                float tapped=age<.75f?Mathf.Sin(age*19)*Mathf.Exp(-age*6):0;
                float sway=Mathf.Sin(time*1.5f+phase)-Mathf.Sin(phase);
                motion[i]=new Vector4((1-cat)*(.018f*sway+.045f*tapped),.045f*tapped,
                    cat*.008f*(Mathf.Sin(time*1.7f+phase)-Mathf.Sin(phase)),cat*.0018f*Mathf.Max(0,tapped));
            }
            material.SetFloat("_MotionClock",clock);material.SetVectorArray("_Motion",motion);
        }
```

### Update · L89

方法 SHA-256：`765aba74f46da010e72eff3fbe5800a5c7d6579d9743e0754cb088a476ea778e`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GardenSceneArt.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update()
        {
            if(owner==null||!owner.IsPlaying||owner.IsPaused)return;
            clock+=Time.deltaTime;
            for(int i=0;i<count;i++)activity[i].x=Mathf.Min(2,activity[i].x+Time.deltaTime);
            Upload();
        }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(material!=null)Destroy(material);}
```

## 源字段与依赖

```csharp
const string PlatePath = "Art/GardenBackgroundV2/reference-plate";
```
```csharp
static Sprite plate;
```
```csharp
readonly Vector4[] regions = new Vector4[16];
```
```csharp
readonly Vector4[] activity = new Vector4[16];
```
```csharp
readonly Vector4[] motion = new Vector4[16];
```
```csharp
Material material;
```
```csharp
GameSession owner;
```
```csharp
int count;
```
```csharp
float clock;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
