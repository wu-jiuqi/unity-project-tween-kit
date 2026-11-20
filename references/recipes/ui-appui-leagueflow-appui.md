# AppUI · AppUI.LeagueFlow

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.LeagueFlow.cs`
文件 SHA-256：`106308cafc8688a90747c9e37e93639ea46f6ab35ff19b5065d2ea48ded7051d`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.LeagueFlow.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### TickLeagueFlow · L13

方法 SHA-256：`83aacfa4b752f68cdd6e3a647cefbbe1e594673f1e035e9e593adf4179ce55a3`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.TickLeagueFlow()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:89` · `AppUI.Update` → `TickLeagueFlow()`
- `Assets/Scripts/UI/AppUI.LeagueFlow.cs:11` · `AppUI.ShowPendingLeagueBeforeContinue` → `TickLeagueFlow()`

```csharp
void TickLeagueFlow(){
  if(!leagueFocused||Profile==null||!Profile.LeagueUnlocked||PlayerStrategyTestBuild.Enabled||LeagueFlowActive||busy||NavigationActive||modal!=null||overlayRoot==null)return;
  bool result=playing&&RewardReady&&baseRewardSaved&&!qaResult;if(playing&&!result)return;
  if(!result&&overlayRoot.childCount>0)return;
  if(!result&&CurrentPage!="home"&&CurrentPage!="chapters"&&CurrentPage!="ranking"&&CurrentPage!="vip")return;
  if(Time.unscaledTime<leagueFlowPoll)return;leagueFlowPoll=Time.unscaledTime+.4f;
  var s=LeagueSource.Read();if(s==null)return;
  if(s.reward!=null){ShowLeagueAutomaticReward(s);return;}
  if(!Profile.LeagueMatchSeen(s.periodId)){
   leagueFlowRoot=UICraft.Root(safeRoot,"Automatic league season flow");leagueFlowRoot.anchorMin=Vector2.zero;leagueFlowRoot.anchorMax=Vector2.one;leagueFlowRoot.sizeDelta=Vector2.zero;
   LeagueMatchPresentation.Show(leagueFlowRoot,Profile,s,()=>{if(leagueFlowRoot!=null){leagueFlowRoot.gameObject.SetActive(false);Destroy(leagueFlowRoot.gameObject);}leagueFlowRoot=null;leagueFlowPoll=Time.unscaledTime+.25f;});return;
  }
  if(result&&leagueResultShown!=resultKey){leagueResultShown=resultKey;if(Profile.LastLeagueMedalGain>0)LeagueMedalPresentation.Show(results,Profile.LastLeagueRankBefore,Profile.LastLeagueRankAfter,Profile.LastLeagueMedalGain);}
 }
```

## 对象层级和素材入口

- L22：`UICraft.Root(safeRoot,"Automatic league season flow")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
RectTransform leagueFlowRoot;
```
```csharp
float leagueFlowPoll;
```
```csharp
string leagueResultShown;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
