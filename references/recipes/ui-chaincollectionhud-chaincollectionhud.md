# ChainCollectionHUD · ChainCollectionHUD

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/ChainCollectionHUD.cs`
文件 SHA-256：`2953ce988acbd87980173e979b2d0ce9f600bd19771161e61af2df2fbbc56e40`
[原始源码快照](../source/Assets/Scripts/UI/ChainCollectionHUD.cs.txt)

用在哪里：AppUI.BuildBattleHUD → ChainCollectionHUD.Initialize → StateChanged → Refresh → Update
触发操作：剩余步数改变时挤压；此交付只提取步数视觉
导入组件：`ProjectTweenKit.MovesFeedback`；展馆 ID：`moves`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L29

方法 SHA-256：`bf6c18d53d518c8ddfe16c2af2dd604eaf1cb8f09994348d6c25abd2107e59f4`
时间来源：Time.deltaTime
曲线证据：Mathf.LerpUnclamped, Mathf.Sin
解析符号：`BounceGarden.ChainCollectionHUD.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(game==null||game.Generation!=generation||game.IsPaused)return;age+=Time.deltaTime;stepAge+=Time.deltaTime;float intro=Mathf.Min(1.025f,Mathf.LerpUnclamped(.9f,1,UICraft.OutBack(Mathf.Clamp01(age/.5f))));float kick=Mathf.Sin(Mathf.Clamp01(stepAge/.3f)*Mathf.PI)*.025f;stepsRoot.localScale=new Vector3(intro*(1+kick),intro*(1-kick),1);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(game!=null){game.StateChanged-=Refresh;if(game.Generation==generation){game.OpeningLootDestination=null;game.ChainLootDestination=null;game.JellyLootDestination=null;}}}
```

## 源字段与依赖

```csharp
GameSession game;
```
```csharp
readonly List<ChainTaskCard> cards=new List<ChainTaskCard>();
```
```csharp
TMP_Text steps;
```
```csharp
RectTransform stepsRoot;
```
```csharp
int seenSteps;
```
```csharp
float stepAge=2,age;
```
```csharp
int generation;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
