# UIFloat · UICraft

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/UICraft.cs`
文件 SHA-256：`51c3aa8b4c0bb5203257961084c4e10cce0345821c8f7dcade4eefd73e44b69a`
[原始源码快照](../source/Assets/Scripts/UI/UICraft.cs.txt)

用在哪里：OpenSettingsSheetC 猫头 amplitude=3/speed=1.4；About 插画 amplitude=4/speed=1.3
触发操作：设置、帮助、关于等插画轻浮动
导入组件：`ProjectTweenKit.UIFloat`；展馆 ID：`float`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L46

方法 SHA-256：`b4a4beb7e6d721b9f22b58461644a8cf67ddbd0e306103969ad56cbe607f26cc`
时间来源：Time.unscaledTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.UIFloat.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){transform.localPosition=initial+Vector3.up*Mathf.Sin(Time.unscaledTime*speed+phase)*amplitude;}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public float amplitude=12,speed=1.4f,phase;
```
```csharp
Vector3 initial;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
