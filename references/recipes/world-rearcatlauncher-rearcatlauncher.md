# RearCatLauncher · RearCatLauncher

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/RearCatLauncher.cs`
文件 SHA-256：`2c8cee4084b1844acd21cd833e27a08c8b6aa66f65aa72f30425a70b44421a72`
[原始源码快照](../source/Assets/Scripts/World/RearCatLauncher.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L25

方法 SHA-256：`51c7dfb041e926a77b85e67f7754759354d6bf6ec9e5eb213d28b9f7f04220a8`
时间来源：Time.deltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.RearCatLauncher.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){
  if(owner==null||!owner.IsPlaying||owner.IsPaused)return;
  float dt=Time.deltaTime;afterShot+=dt;
  if(firing){clockMs+=dt*1000;if(clockMs>=layout.duration_ms){firing=false;clockMs=0;}}
  else if(aiming){clockMs=Mathf.Min(layout.contact_ms-.01f,clockMs+dt*1000);}
  else clockMs=0;
  ApplyFrame();
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
SpriteRenderer face;
```
```csharp
YarnArt.Layout layout;
```
```csharp
float clockMs,afterShot=10;
```
```csharp
bool aiming,firing;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
