# ErrorPopup0411 · ErrorPopup0411

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/ErrorPopup0411.cs`
文件 SHA-256：`ed26afddece5e3058f53df71deb155e865f9387408d65c60ecced6926da340e9`
[原始源码快照](../source/Assets/Scripts/UI/ErrorPopup0411.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `ErrorPopupMotion` / `errorpopup`：手工抽取纯表现接口；保留时序，业务事件由宿主输入

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### Update · L34

方法 SHA-256：`3795171fab03323e963f05233c8de60d32a237f8c71139404a39621a46a70b19`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ErrorPopup0411.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){Age+=Mathf.Min(Time.unscaledDeltaTime,.05f);Box?.Render(Age);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public const float Width=1080,Height=1700;
```
```csharp
RectTransform layout;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
