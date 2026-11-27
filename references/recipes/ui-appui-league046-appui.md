# AppUI · AppUI.League046

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.League046.cs`
文件 SHA-256：`90a1952e9a3bfb306b036c79773f4f9926be6255642b090abeed710ca62ff107`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.League046.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### NavigateLeague046 · L13

方法 SHA-256：`b8f1d36b997560a4efa2f9a2b8592e87f57d16ea23b3ce569d798f68dffcbb61`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.NavigateLeague046(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.League046.cs:37` · `AppUI.DrawReward046` → `NavigateLeague046(3)`
- `Assets/Scripts/UI/AppUI.League046.cs:41` · `AppUI.DrawReward046` → `NavigateLeague046(3)`
- `Assets/Scripts/UI/AppUI.League046.cs:42` · `AppUI.DrawReward046` → `NavigateLeague046(3)`
- `Assets/Scripts/UI/LeagueMatchPresentation.cs:17` · `LeagueMatchPresentation.ExitTo` → `app.NavigateLeague046(tab)`

```csharp
public void NavigateLeague046(int tab){if(leagueFlowRoot!=null){leagueFlowRoot.gameObject.SetActive(false);Destroy(leagueFlowRoot.gameObject);leagueFlowRoot=null;}leagueFlowPoll=Time.unscaledTime+30;switch(tab){case 0:ShowPage("chapters");break;case 1:OpenShopCategory();break;case 2:ShowPage("home");break;case 3:ShowPage("ranking");break;default:OpenDressing();break;}}
```

### DrawReward046 · L37

方法 SHA-256：`62c3380c3ebc0683e4ee1992c75066be247a2f141f0d06187abbb469f3981cdb`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.DrawReward046(UnityEngine.RectTransform, BounceGarden.LeagueReward, bool, bool, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.League046.cs:24` · `AppUI.DrawLeagueDetail046` → `DrawReward046(r,leagueReward046??s.reward,claimed,false,leagueDuplicate046)`
- `Assets/Scripts/UI/AppUI.League046.cs:44` · `AppUI.ShowLeagueAutomaticReward046` → `DrawReward046(r,s.reward,false,true,Profile.LeagueDuplicateCoins(s.reward))`

```csharp
void DrawReward046(RectTransform r,LeagueReward reward,bool claimed,bool automatic,int duplicate=0){if(reward==null){LeagueDraw046.Text(r,"当前没有待领取的奖励",360,600,620,60,28);LeagueDraw046.Button(r,"046 return ranking","返回联赛",360,1040,528,64,()=>NavigateLeague046(3),"primary",29);return;}
  LeagueDraw046.Card(r,"046 reward ranking",82,294,556,95);LeagueDraw046.Icon(r,"nav-league",150,341,65);LeagueDraw046.Text(r,(automatic?"最终第":"本期排名 · 第")+reward.rank+"名",405,341,400,45,29);
  LeagueDraw046.RewardPair(r,Profile,string.IsNullOrEmpty(reward.avatarId)?null:LeagueExclusiveCosmetics.ById(reward.avatarId)?.avatar??reward.avatarResource,string.IsNullOrEmpty(reward.frameId)?null:LeagueExclusiveCosmetics.ById(reward.frameId)?.frame??reward.frameResource,416);LeagueDraw046.Card(r,"046 coins",135,731,450,110);LeagueDraw046.Text(r,claimed?"已到账":"金币奖励",360,758,440,30,19,LeagueStyle046.Muted);LeagueStyle046.Image(r,"coin",265,LeagueDraw046.Y(804),43,43);LeagueDraw046.Text(r,"+"+(reward.coins+duplicate).ToString("N0")+" 金币",387,804,360,45,31);
  bool cosmetics=!string.IsNullOrEmpty(reward.avatarId)||!string.IsNullOrEmpty(reward.frameId);LeagueDraw046.Text(r,claimed?(cosmetics?"装扮已加入收藏":"金币已到账"):cosmetics?"头像与头像框奖励":"金币奖励",360,903,620,45,29);LeagueDraw046.Text(r,claimed?"随时换上喜欢的新装扮":automatic?"新一期花园伙伴正在等你":duplicate>0?"含重复装扮补偿 "+duplicate+" 金币":"重复装扮按规则转换为金币",360,958,630,36,21,LeagueStyle046.Muted);
  bool committed=false;LeagueDraw046.Button(r,"046 reward primary",claimed?(cosmetics?"立即装扮":"完成"):automatic?"领取并继续":"领取奖励",360,1040,528,64,()=>{if(claimed){if(cosmetics){if(!LeagueSource.Equip(reward.avatarId,reward.frameId)){Toast("装扮保存失败，请重试。");return;}RefreshLeagueViews();OpenDressing();}else NavigateLeague046(3);return;}if(committed)return;int compensation=Profile.LeagueDuplicateCoins(reward);if(!LeagueSource.Claim(reward.periodId)){Toast("奖励保存失败，请重试。");return;}committed=true;SoundService.Play("reward_claim");if(automatic){if(leagueFlowRoot!=null){leagueFlowRoot.gameObject.SetActive(false);Destroy(leagueFlowRoot.gameObject);leagueFlowRoot=null;}leagueFlowPoll=Time.unscaledTime+.5f;}RefreshLeagueViews();OpenLeagueDetail046("claimed",reward,compensation);},"primary",29);
  if(claimed)LeagueDraw046.Button(r,"046 reward done","完成",360,1130,402,40,()=>NavigateLeague046(3),"secondary",22);
 }
```

## 对象层级和素材入口

- L39：`LeagueStyle046.Image(r,"coin",265,LeagueDraw046.Y(804),43,43)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
string leagueDetail046="rules";
```
```csharp
LeagueReward leagueReward046;
```
```csharp
int leagueDuplicate046;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
