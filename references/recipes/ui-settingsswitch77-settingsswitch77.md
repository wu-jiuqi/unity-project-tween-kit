# SettingsSwitch77 · SettingsSwitch77

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/SettingsSwitch77.cs`
文件 SHA-256：`b53bfd125ba388bab8e762309de63c8a029a1fc6097515c96ab308fa4bd96574`
[原始源码快照](../source/Assets/Scripts/UI/SettingsSwitch77.cs.txt)

用在哪里：AppUI.Settings → OpenSettingsSheetC → SettingsSwitch77.Create；AudioSettings 复用
触发操作：声音与震动开关；点击读写宿主一次
导入组件：`ProjectTweenKit.SettingsSwitch77`；展馆 ID：`switch`。
复用形式：组合预制体。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Refresh · L15

方法 SHA-256：`1584a54acd4d946e503508bec86d99b16405014330907cf4aef1cb91106cd258`
时间来源：SetUpdate(true)
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.SettingsSwitch77.Refresh(bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/SettingsSwitch77.cs:14` · `SettingsSwitch77.Refresh` → `Refresh(true)`

```csharp
void Refresh(bool animate){
  bool value=read();track.sprite=CSettingsArt.Get(value?"toggle_track_on":"toggle_track_off");float x=value?39:-39;slide?.Kill(false);
  if(animate)slide=knob.DOAnchorPos(new Vector2(x,0),.16f).SetEase(Ease.OutCubic).SetUpdate(true);else knob.anchoredPosition=new Vector2(x,0);
  gameObject.name=key+(value?"：开":"：关");
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){slide?.Kill(false);}
```

## 源字段与依赖

```csharp
Func<bool> read;
```
```csharp
Action<bool> write;
```
```csharp
string key;
```
```csharp
Image track;
```
```csharp
RectTransform knob;
```
```csharp
Tween slide;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
