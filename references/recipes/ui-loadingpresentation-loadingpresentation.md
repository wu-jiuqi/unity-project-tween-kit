# LoadingPresentation · LoadingPresentation

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/LoadingPresentation.cs`
文件 SHA-256：`ca26d9a15b2367fe19c7dc105d88cb15d35313ec8cfa2440fd8b0d51647f80cd`
[原始源码快照](../source/Assets/Scripts/UI/LoadingPresentation.cs.txt)

用在哪里：AppUI.BeginLevel → LoadingPresentation.Build / Play → RunLevelLoad
触发操作：进入关卡的加载页自动循环；内容准备另由宿主管理
导入组件：`ProjectTweenKit.LoadingPresentation`；展馆 ID：`loading`。
复用形式：组合预制体。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L40

方法 SHA-256：`c2145dacb787fa24432b251610090e5b2ebe2f2c972200afd6111cee4ed23fd9`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LoadingPresentation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){
  if(!running||content==null)return;
  if(firstTick){firstTick=false;return;}
  float dt=Mathf.Min(Time.unscaledDeltaTime,.05f);Elapsed+=dt;shown=Mathf.MoveTowards(shown,target,dt*.72f);Hedge.SetProgress(shown);number.text=Percent+"%";Render();
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){running=false;}
```

## 源字段与依赖

```csharp
public const float MinimumDuration=2.6f;
```
```csharp
RectTransform content,backdrop;
```
```csharp
LoadingBoxAnimation box;
```
```csharp
TMP_Text label,number;
```
```csharp
bool running,complete,firstTick;
```
```csharp
float target,shown;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
