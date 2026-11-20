# LeagueRankingView · LeagueRankingView

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/LeagueRankingView.cs`
文件 SHA-256：`e02ec449c21cb64354b47245aee1f660e2a05b161062dc9e25a30388c720d753`
[原始源码快照](../source/Assets/Scripts/UI/LeagueRankingView.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L38

方法 SHA-256：`92613a32baecbcbea037e6f6321d1bdcb916839b7fbb6b808324bd49e3c61ae1`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LeagueRankingView.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(!initialized)return;bool visible=GetComponentInParent<CanvasGroup>()?.interactable??true;if(!visible)return;if(Mathf.Abs(root.rect.height-lastHeight)>.5f){Build();dirty=true;}if(dirty||Time.unscaledTime>=nextTick)Refresh(false);else if(Time.unscaledTime>=nextClockTick)UpdateClock();}
```

### UpdateClock · L68

方法 SHA-256：`2687c4cb44dbf1fdb769540c26a1e3407b77c2bee3d98ddbc3be5dbf368817cf`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LeagueRankingView.UpdateClock()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/LeagueRankingView.cs:38` · `LeagueRankingView.Update` → `UpdateClock()`
- `Assets/Scripts/UI/LeagueRankingView.cs:74` · `LeagueRankingView.Refresh` → `UpdateClock()`

```csharp
void UpdateClock(){
  long seconds=Math.Max(0,snapshot.remaining-(long)(Time.unscaledTime-readTime));var remaining=TimeSpan.FromSeconds(seconds);
  countdown.text=(remaining.TotalDays>=1?((int)remaining.TotalDays)+"天":"")+remaining.Hours.ToString("00")+":"+remaining.Minutes.ToString("00")+":"+remaining.Seconds.ToString("00");nextClockTick=Time.unscaledTime+1;
 }
```

### Refresh · L73

方法 SHA-256：`17ad109e93c48934c7126ef547191fc1022a851107449ede0500f75e2d5bc63d`
时间来源：SetUpdate(true), Time.unscaledTime
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.LeagueRankingView.Refresh(bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/LeagueRankingView.cs:35` · `LeagueRankingView.Initialize` → `Refresh(true)`
- `Assets/Scripts/UI/LeagueRankingView.cs:38` · `LeagueRankingView.Update` → `Refresh(false)`
- `Assets/Scripts/UI/LeagueRankingView.cs:39` · `LeagueRankingView.Enter` → `Refresh(true)`
- `Assets/Scripts/UI/LeagueRankingView.cs:40` · `LeagueRankingView.SetSource` → `Refresh(true)`

```csharp
public void Refresh(bool focus){
  string previousPeriod=snapshot?.periodId;snapshot=source.Read();if(snapshot==null)return;if(lastHeight<0||previousPeriod!=snapshot.periodId)Build();dirty=false;readTime=Time.unscaledTime;nextTick=readTime+snapshot.config.refreshSeconds;var c=snapshot.config;UpdateClock();podium.Bind(snapshot,profile);
  rankText.text=string.Format(c.copy.rank,snapshot.player.rank)+"  ·  "+snapshot.player.medals.ToString("N0")+"枚奖章";rewardLink.text=snapshot.reward!=null?"领取奖励 >":"前"+c.topRewardCount+"名奖励 >";
  float stride=c.layout.rowHeight+c.layout.rowGap;content.sizeDelta=new Vector2(990,snapshot.entries.Length*stride);
  var ids=new HashSet<string>(snapshot.entries.Select(e=>e.id));foreach(var id in rows.Keys.Where(id=>!ids.Contains(id)).ToArray()){rows[id].move?.Kill();rows[id].root.gameObject.SetActive(false);Destroy(rows[id].root.gameObject);rows.Remove(id);}
  foreach(var e in snapshot.entries){if(!rows.TryGetValue(e.id,out var row)){row=Create(e);rows.Add(e.id,row);row.root.anchoredPosition=new Vector2(0,-(e.rank-.5f)*stride);}
   row.name.text=e.name;row.rank.text=e.rank.ToString();row.rank.color=e.rank<=3?Color.white:UICraft.Ink;
   row.rankPlate.gameObject.SetActive(e.rank<=3);row.crown.gameObject.SetActive(e.rank<=3);if(e.rank<=3){row.rankPlate.color=e.rank==1?new Color32(246,187,64,255):e.rank==2?new Color32(117,173,226,255):new Color32(224,145,93,255);row.crown.color=row.rankPlate.color;}
   row.hint.text=e.player?c.copy.you:snapshot.next?.id==e.id?c.copy.next:"";row.score.text=e.medals.ToString("N0");row.face.color=e.player?new Color32(217,247,224,255):e.rank==1?new Color32(255,236,194,255):e.rank==2?new Color32(235,238,254,255):e.rank==3?new Color32(255,232,218,255):new Color32(250,245,239,255);
   row.gift.gameObject.SetActive(e.rank<=3);if(e.rank<=3)row.gift.sprite=PastelArt.Get(e.rank==1?"chest_blush":e.rank==2?"chest_blue":"chest_lavender");
   string style=profile.EquippedAvatarResource+"/"+profile.EquippedFrameResource;if(e.player&&row.style!=style){row.style=style;row.portrait.gameObject.SetActive(false);Destroy(row.portrait.gameObject);row.portrait=LeaguePortrait.Create(row.visual,profile,"",-315,0,130,true);}
   Vector2 target=new Vector2(0,-(e.rank-.5f)*stride);if(row.oldRank!=e.rank){if(row.oldRank>0){row.direction.sprite=LeagueMotionArt.Get(row.oldRank>e.rank?"up":"down");row.direction.gameObject.SetActive(true);row.direction.rectTransform.DOKill();row.direction.rectTransform.anchoredPosition=new Vector2(95,-37+(row.oldRank>e.rank?18:-18));DOTween.To(()=>row.direction.rectTransform.anchoredPosition,v=>row.direction.rectTransform.anchoredPosition=v,new Vector2(95,-37),.6f).SetTarget(row.direction.rectTransform).SetUpdate(true);}
   if(e.player&&row.oldRank>e.rank&&row.oldRank>0)SoundService.Play("rank_up");row.move?.Kill();if(row.oldRank>0&&FeedbackProfile.Active.enabledFeedback)row.move=DOTween.To(()=>row.root.anchoredPosition,v=>row.root.anchoredPosition=v,target,.48f).SetEase(Ease.OutCubic).SetUpdate(true);else row.root.anchoredPosition=target;}
   if(row.oldScore>=0&&(row.oldScore!=e.medals||row.oldRank!=e.rank))row.visual.GetComponent<LeagueJelly>().Play();else if(row.oldScore<0)row.visual.GetComponent<LeagueJelly>().Play(Mathf.Min(.30f,e.rank*.025f));row.oldRank=e.rank;row.oldScore=e.medals;
  }
  if(pressureText!=null)pressureText.text=LeagueEngine.Pressure(snapshot);
  if(focus)FocusPlayer();
  // Season entry is scheduled by AppUI, independently of this page.
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(profile!=null)profile.Changed-=MarkDirty;foreach(var row in rows.Values)row.move?.Kill();}
```

## 源字段与依赖

```csharp
LocalProfile profile;
```
```csharp
ILeagueSource source;
```
```csharp
Action play,rewards,back;
```
```csharp
LeagueSnapshot snapshot;
```
```csharp
RectTransform root,content,viewport;
```
```csharp
ScrollRect scroll;
```
```csharp
LeaguePodiumView podium;
```
```csharp
TMP_Text countdown,rankText,rewardLink,pressureText;
```
```csharp
readonly Dictionary<string,Row> rows=new Dictionary<string,Row>();
```
```csharp
float lastHeight=-1,nextTick,nextClockTick,readTime;
```
```csharp
bool dirty=true,initialized;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
