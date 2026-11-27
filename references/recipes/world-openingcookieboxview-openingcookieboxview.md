# OpeningCookieBoxView · OpeningCookieBoxView

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/OpeningCookieBoxView.cs`
文件 SHA-256：`aa83c9e4274356a7f657e6c255d4e7f2596c20f98a13e68137c7c0d8fde696f5`
[原始源码快照](../source/Assets/Scripts/World/OpeningCookieBoxView.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Tick · L24

方法 SHA-256：`bafb23b11f0c047e6ef20b0dc9fa6f1287f7fe010e6215ab22cd33b145ad30a0`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.OpeningCookieBoxView.Tick(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/OpeningCampaign.cs:61` · `GameSession.TickOpeningYarn` → `box.Tick(dt)`

```csharp
public void Tick(float dt){
  if(game==null||game.IsPaused||lift==null)return;jumpAge+=dt;float y;
  if(jumpAge<.11f){float t=Mathf.Clamp01(jumpAge/.11f);y=Mathf.Lerp(startHeight,peakHeight,1-(1-t)*(1-t));}
  else if(jumpAge<.34f){float t=(jumpAge-.11f)/.23f;y=peakHeight*(1-t*t);}
  else y=0;
  lift.localPosition=Vector3.up*y;lift.localRotation=Quaternion.Euler(0,0,Mathf.Sin(Mathf.Clamp01(jumpAge/.34f)*Mathf.PI*2)*2.5f);
  if(Remaining==0&&jumpAge>=.34f)game.OpeningRetireCookie(this);
 }
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
CookieBoxArtView art;
```
```csharp
Transform lift;
```
```csharp
float jumpAge=1,startHeight,peakHeight;
```
```csharp
readonly Dictionary<long,int[]> attacks=new Dictionary<long,int[]>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
