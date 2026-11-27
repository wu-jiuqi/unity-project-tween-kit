# AppUI · AppUI.StreakReward

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.StreakReward.cs`
文件 SHA-256：`d5e6d8efa747be565244fcc12b91e1491123f5fa4781e21cc54811b737019246`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.StreakReward.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### ShowCampaignStreakReward · L9

方法 SHA-256：`c4b925e616a6d6b256bf7fd3db4c7b1e8ca9ddcd781ff234da98464cd02684c5`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.ShowCampaignStreakReward(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:183` · `AppUI.StartPreparedLevel` → `ShowCampaignStreakReward(generation)`

```csharp
IEnumerator ShowCampaignStreakReward(int generation){
  var hud=GetComponentInChildren<StreakTrialHUD>();if(hud==null)yield break;
  var targets=new List<RectTransform>();var kinds=new List<StreakBooster>();
  for(int i=1;i<=3;i++){var k=(StreakBooster)i;if(Profile.StreakBonusStock(k,currentLevel)>0&&Session.BoosterAvailable(k)){var slot=hud.transform.Find("Skill slot "+k) as RectTransform;if(slot!=null){targets.Add(slot);kinds.Add(k);}}}
  if(kinds.Count==0)yield break;
  CancelAim();var root=UICraft.Root(overlayRoot,"连胜道具赠送动画");
  var presentation=root.gameObject.AddComponent<StreakRewardPresentation>();
  int tier=kinds.Count;bool repeated=RuntimeQA.IsIsolated?rewardTiersSeen.Contains(tier):PlayerPrefs.GetInt("BounceGarden.RewardSeen.v76."+tier,0)!=0;
  yield return presentation.Play(Profile.CampaignWinStreak,kinds,targets,repeated);
  if(presentation.Phase=="complete"){rewardTiersSeen.Add(tier);if(!RuntimeQA.IsIsolated){PlayerPrefs.SetInt("BounceGarden.RewardSeen.v76."+tier,1);PlayerPrefs.Save();}}
  if(root!=null)Destroy(root.gameObject);
 }
```

## 对象层级和素材入口

- L12：`hud.transform.Find("Skill slot "+k)`
- L14：`UICraft.Root(overlayRoot,"连胜道具赠送动画")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
readonly HashSet<int> rewardTiersSeen=new HashSet<int>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
