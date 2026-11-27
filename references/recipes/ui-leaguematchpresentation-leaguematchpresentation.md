# LeagueMatchPresentation · LeagueMatchPresentation

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/LeagueMatchPresentation.cs`
文件 SHA-256：`8f190d6f0afc9eb44da7ada484fac1995192782bef0bef50bba6cffe1d46af45`
[原始源码快照](../source/Assets/Scripts/UI/LeagueMatchPresentation.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `DecorativeBreath` / `breath`：仅提取徽章1+.035sin(3t)；不包含匹配、排行、日期或确认保存

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### Update · L13

方法 SHA-256：`6bb2a9fbf12c6ef826b96640699194dc7c2f41685670406c6d606650b0f041aa`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LeagueMatchPresentation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(closed||!focused||suspended)return;var layout=page.GetComponent<DressingShopLayout044>();layout.Layout(((RectTransform)transform).rect.height,app.LeagueTopReserve046,app.LeagueNavigationHeight046);age+=Mathf.Min(Time.unscaledDeltaTime,.05f);if(!summary){bool motion=FeedbackProfile.Active.enabledFeedback;for(int i=0;i<portraits.Length;i++){float t=Mathf.Clamp01((age-.15f-i*.23f)/.32f);portraits[i].localScale=Vector3.one*(motion?UICraft.OutBack(t):(t>0?1:0));}if(age>=2.1f)Summary();}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnApplicationFocus(bool f){focused=f;}
```

```csharp
void OnApplicationPause(bool p){suspended=p;}
```

## 源字段与依赖

```csharp
LocalProfile profile;
```
```csharp
LeagueSnapshot snapshot;
```
```csharp
Action done;
```
```csharp
RectTransform content,page;
```
```csharp
RectTransform[] portraits;
```
```csharp
TMP_Text detail,status,buttonText;
```
```csharp
float age;
```
```csharp
bool summary,focused=true,suspended,closed;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
