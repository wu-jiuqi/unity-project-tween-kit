# BrickBody · PhysicsBodies

状态：`mixed-physics-presentation-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/PhysicsBodies.cs`
文件 SHA-256：`4a7c2a6cede5908904f677f27deef0120740c077dad28921b906f4eedea61d21`
[原始源码快照](../source/Assets/Scripts/World/PhysicsBodies.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L294

方法 SHA-256：`ed5a0dfa64559efe9eb9dbaae24c6b0641ff48478b09180a8adf0e1fc31f3a72`
时间来源：Time.deltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.BrickBody.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update()
        {
            if(!Alive||owner==null||owner.IsPaused||owner.IsPixelDrop)return;
            // ChainTileVisibility owns chain colliders. Never briefly re-enable
            // hidden supply tiles or rebuild unchanged broadphase proxies here.
            if(!owner.IsChainCollection){bool enabled=age>=delay+.28f;if(hitCollider.enabled!=enabled)hitCollider.enabled=enabled;}
            if(visualSettled&&pulse<=0)return;
            age+=Time.deltaTime;
            float t=owner.IsChainCollection?1:Mathf.Clamp01((age-delay)/.28f),s=t-1;
            float entrance=1+2.70158f*s*s*s+1.70158f*s*s;
            pulse=Mathf.Max(0,pulse-Time.deltaTime);
            float hit=pulse>0&&FeedbackProfile.Active.enabledFeedback?Mathf.Sin((1-pulse/FeedbackProfile.Active.hitDuration)*Mathf.PI)*FeedbackProfile.Active.hitScale:0;
            if(!owner.IsChainCollection&&hitCollider.enabled!=(t>=1))hitCollider.enabled=t>=1;
            float scale=Mathf.Max(0,entrance);
            visualRoot.localScale=new Vector3(scale*(1+hit),scale*(1-hit),1);
            if(round5)visualRoot.localPosition=new Vector3(0,(visualRoot.localScale.y-1)*owner.Board.CellHeight*.5f,0);
            var profile=FeedbackProfile.Active;
            float hitAge=profile.hitDuration-pulse;
            if(flash==null)flash=new MaterialPropertyBlock();
            flash.SetFloat("_Flash",pulse>0&&profile.enabledFeedback?.48f*(1-Mathf.Clamp01(hitAge/profile.flashDuration)):0);
            face.SetPropertyBlock(flash);
            foreach(var image in hitFaces)if(image!=null&&image!=face&&image.sharedMaterial!=null&&image.sharedMaterial.HasProperty("_Flash"))image.SetPropertyBlock(flash);
            if(number!=null)number.transform.localScale=Vector3.one*(1+hit*3);
            visualSettled=t>=1&&pulse<=0;
            if(visualSettled){face.SetPropertyBlock(null);enabled=false;}
        }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
GameSession owner;
```
```csharp
Transform visualRoot;
```
```csharp
SpriteRenderer face;
```
```csharp
TMP_Text number,numberShadow;
```
```csharp
Collider2D hitCollider;
```
```csharp
static readonly System.Collections.Generic.Dictionary<Texture,Material> flashMaterials=new System.Collections.Generic.Dictionary<Texture,Material>();
```
```csharp
MaterialPropertyBlock flash;
```
```csharp
readonly System.Collections.Generic.List<SpriteRenderer> hitFaces=new System.Collections.Generic.List<SpriteRenderer>(8);
```
```csharp
float age,delay,pulse;
```
```csharp
bool round5;
```
```csharp
float footprintWidth;
```
```csharp
SpriteRenderer groundShadow;
```
```csharp
// Optional supplied static state sprites; never use destruction animation frames as damage skins.
        Sprite damagedSkin,criticalSkin;
```
```csharp
bool visualSettled;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
