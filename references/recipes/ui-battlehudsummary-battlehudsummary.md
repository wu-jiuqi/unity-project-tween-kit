# BattleHUDSummary · BattleHUDSummary

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/BattleHUDSummary.cs`
文件 SHA-256：`78845376ec0e3e17a68c33c888fc30085dd481dd06e20ded8428232237076856`
[原始源码快照](../source/Assets/Scripts/UI/BattleHUDSummary.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L8

方法 SHA-256：`1ef4769ded2e0104b28bcfa0d034535bc1d9e34706774d608e35821f689579b4`
时间来源：PresentationDelta, Time.unscaledDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.BattleHUDSummary.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){victoryPulse+=game!=null?game.PresentationDelta(Time.unscaledDeltaTime):Time.unscaledDeltaTime;float p=Mathf.Sin(Mathf.Clamp01(victoryPulse/.25f)*Mathf.PI);if(score!=null){score.transform.localScale=Vector3.one*(1+p*.2f);score.color=Color.Lerp(UICraft.Ink,new Color(.85f,.45f,.06f),p);}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(game!=null)game.StateChanged-=Refresh;}
```

## 源字段与依赖

```csharp
GameSession game;
```
```csharp
int generation;
```
```csharp
TMP_Text level,score;
```
```csharp
int? shownScore;
```
```csharp
float victoryPulse=2;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
