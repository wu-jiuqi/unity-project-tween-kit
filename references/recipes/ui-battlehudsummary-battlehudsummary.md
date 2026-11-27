# BattleHUDSummary · BattleHUDSummary

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/BattleHUDSummary.cs`
文件 SHA-256：`df20e5475c21c7efa50e668f62e3ea8f5e80259dbe1279d124fbc536e20c40a5`
[原始源码快照](../source/Assets/Scripts/UI/BattleHUDSummary.cs.txt)

用在哪里：AppUI.BuildBattleHUD → BattleHUDSummary.Initialize；VictoryFinale.Advance / VictoryRoundPreparation.Update → ShowVictoryScore(impact=true)
触发操作：结算星光到达时分数变金色并放大回落
导入组件：`ProjectTweenKit.ScoreImpact`；展馆 ID：`scoreimpact`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L8

方法 SHA-256：`5105901156533bdc6d3b6c63356eb3e94811ae57a1d312951d92a8b4ab0ddd83`
时间来源：PresentationDelta, Time.unscaledDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.BattleHUDSummary.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){victoryPulse+=game!=null?game.PresentationDelta(Time.unscaledDeltaTime):Time.unscaledDeltaTime;float p=Mathf.Sin(Mathf.Clamp01(victoryPulse/.25f)*Mathf.PI);if(score!=null){score.transform.localScale=Vector3.one*(1+p*.2f);score.color=Color.Lerp(ScoreInk,new Color(.85f,.45f,.06f),p);}}
```

### Initialize · L10

方法 SHA-256：`225d9269821364b40fc4e7cd4bbe9f33c3b361a5e7dc80e3c9d6239709f0131f`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BattleHUDSummary.Initialize(BounceGarden.GameSession)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Initialize(GameSession g){game=g;generation=g.Generation;
  var title=UICraft.Root(transform,"Garden level plaque",-397,1118,218,58);title.gameObject.AddComponent<BattleHudAspect050>();BattleGardenArt050.Image(title,"level-plaque-v19",0,0,218,58);
  level=UICraft.Label(title,"第"+(g.LevelIndex+1)+"关",0,3,200,48,31,BattleGardenArt050.Ink);level.name="Battle level number";BattleRoundTypography.Style(level,true);level.gameObject.AddComponent<CurvedBattleLevel050>();
  var scoreRow=UICraft.Root(transform,"Score below settings",-356,980,290,60);scoreRow.gameObject.AddComponent<BattleHudAspect050>();scoreRow.gameObject.AddComponent<BattleScorePlacement050>();var caption=UICraft.Label(scoreRow,"得分",-85,0,80,34,25,BattleGardenArt050.Ink);BattleRoundTypography.Style(caption,false);caption.gameObject.SetActive(false);
  score=UICraft.Label(scoreRow,"",0,0,290,60,36,BattleGardenArt050.Ink);score.name="Battle current score";BattleRoundTypography.Style(score,true);score.enableAutoSizing=true;score.fontSizeMin=34;score.fontSizeMax=40;score.fontStyle=FontStyles.Bold;score.alignment=TextAlignmentOptions.Left;score.textWrappingMode=TextWrappingModes.NoWrap;scoreMaterial=new Material(score.fontSharedMaterial){name="Private battle score outlined material"};scoreMaterial.EnableKeyword("OUTLINE_ON");scoreMaterial.SetFloat("_OutlineWidth",.3f);scoreMaterial.SetColor("_OutlineColor",new Color32(255,253,242,255));scoreMaterial.SetFloat("_FaceDilate",.045f);score.fontSharedMaterial=scoreMaterial;score.extraPadding=true;score.UpdateMeshPadding();
  game.StateChanged+=Refresh;Refresh();
 }
```

## 对象层级和素材入口

- L11：`UICraft.Root(transform,"Garden level plaque",-397,1118,218,58)`
- L11：`BattleGardenArt050.Image(title,"level-plaque-v19",0,0,218,58)`
- L12：`UICraft.Label(title,"第"+(g.LevelIndex+1)+"关",0,3,200,48,31,BattleGardenArt050.Ink)`
- L13：`UICraft.Root(transform,"Score below settings",-356,980,290,60)`
- L13：`UICraft.Label(scoreRow,"得分",-85,0,80,34,25,BattleGardenArt050.Ink)`
- L14：`UICraft.Label(scoreRow,"",0,0,290,60,36,BattleGardenArt050.Ink)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(game!=null)game.StateChanged-=Refresh;if(scoreMaterial!=null)Destroy(scoreMaterial);}
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
Material scoreMaterial;
```
```csharp
static readonly Color ScoreInk=new Color32(85,47,27,255);
```
```csharp
int? shownScore;
```
```csharp
float victoryPulse=2;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
