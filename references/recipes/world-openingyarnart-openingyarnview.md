# OpeningYarnView · OpeningYarnArt

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/OpeningYarnArt.cs`
文件 SHA-256：`830334db086b32c4f88c06658d4a866fccb34ffbb203cc9279cfe907383ce7b4`
[原始源码快照](../source/Assets/Scripts/World/OpeningYarnArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Tick · L32

方法 SHA-256：`058bc33b64e15a7deec28056db31c18519c14f141eb0e3d3daffab539241e89e`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.OpeningYarnView.Tick(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/OpeningCampaign.cs:61` · `GameSession.TickOpeningYarn` → `view.Tick(dt)`

```csharp
public void Tick(float dt){if(game==null||game.IsPaused)return;bounceAge+=dt;layerAge+=dt;if(Layers>0&&LayerRemaining<=0){image.enabled=true;image.sprite=OpeningYarnArt.Rest(Layers);rest=OpeningYarnArt.Fit(OpeningYarnArt.RestKey(Layers),0);}float t=Mathf.Clamp01(bounceAge/.22f),impact=Mathf.Sin(t*Mathf.PI)*.13f;image.transform.localScale=Vector3.Scale(rest,new Vector3(1+impact,1-impact*.8f,1));}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
GameSession game;
```
```csharp
SpriteRenderer image;
```
```csharp
float bounceAge=10,layerAge=10,layerWait=OpeningYarnEffect.LayerDuration;
```
```csharp
Vector3 rest;
```
```csharp
readonly HashSet<long> hits=new HashSet<long>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
