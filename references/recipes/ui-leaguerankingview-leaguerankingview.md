# LeagueRankingView · LeagueRankingView

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/LeagueRankingView.cs`
文件 SHA-256：`04143f33f8a659d50a1c82925fad238eeb83aaeb08bbf1345da6910054f29356`
[原始源码快照](../source/Assets/Scripts/UI/LeagueRankingView.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `RankReorder` / `rankmove`：保持.48秒OutCubic行位移；主机传目标名次；原排行读取、头像工厂、声音与方向箭头不在此组件中

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### Update · L34

方法 SHA-256：`cdd3682069d3777ed74038ec1783627a4f51620bf73ddc62473c96acd3be6ca7`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LeagueRankingView.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(!initialized||!(GetComponentInParent<CanvasGroup>()?.interactable??true))return;if(Mathf.Abs(root.rect.height-lastHeight)>.5f){Build();dirty=true;}if(dirty||Time.unscaledTime>=nextTick)Refresh(false);}
```

### Refresh · L46

方法 SHA-256：`470ddde7a9a28fcc91ac61768734a0c8b862acba00f9c2bb9be60308335ef428`
时间来源：SetUpdate(true), Time.unscaledTime
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.LeagueRankingView.Refresh(bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/LeagueRankingView.cs:32` · `LeagueRankingView.Initialize` → `Refresh(true)`
- `Assets/Scripts/UI/LeagueRankingView.cs:34` · `LeagueRankingView.Update` → `Refresh(false)`
- `Assets/Scripts/UI/LeagueRankingView.cs:35` · `LeagueRankingView.Enter` → `Refresh(true)`

```csharp
public void Refresh(bool focus){if(!initialized)return;var oldPeriod=snapshot?.periodId;snapshot=source.Read();if(snapshot==null)return;if(lastHeight<0||oldPeriod!=snapshot.periodId)Build();dirty=false;nextTick=Time.unscaledTime+snapshot.config.refreshSeconds;rankText.text="第"+snapshot.player.rank+"名";medalsText.text=snapshot.player.medals+" 枚";content.sizeDelta=new Vector2(606,snapshot.entries.Length*Stride);
  foreach(var e in snapshot.entries){if(!rows.TryGetValue(e.id,out var row)){row=Create(e);rows.Add(e.id,row);row.root.anchoredPosition=new Vector2(0,-(e.rank-.5f)*Stride);}
   row.name.text=e.player?"我":e.name;row.rank.text=e.rank.ToString();row.score.text=e.medals.ToString("N0");row.face.sprite=e.player?CommonButtonsA.Get("selected-normal"):DressingShopArt044.Get("inset");row.face.type=Image.Type.Sliced;row.face.pixelsPerUnitMultiplier=e.player?row.face.sprite.rect.height/59:1.5f;row.face.color=Color.white;
   string style=e.player?profile.EquippedAvatarResource+"/"+profile.EquippedFrameResource:e.portrait+"/"+e.frame;if(row.portrait==null||row.style!=style){if(row.portrait!=null){row.portrait.gameObject.SetActive(false);Destroy(row.portrait.gameObject);}row.portrait=LeaguePortrait.Create(row.visual,profile,e.portrait,-198,0,54,e.player,e.frame);row.style=style;}
   var target=new Vector2(0,-(e.rank-.5f)*Stride);if(row.oldRank!=e.rank){row.move?.Kill();if(row.oldRank>0&&FeedbackProfile.Active.enabledFeedback){row.move=DOTween.To(()=>row.root.anchoredPosition,v=>row.root.anchoredPosition=v,target,.48f).SetEase(Ease.OutCubic).SetUpdate(true);if(e.player&&row.oldRank>e.rank)SoundService.Play("rank_up");}else row.root.anchoredPosition=target;}
   if(row.oldScore!=e.medals||row.oldRank!=e.rank)row.visual.GetComponent<LeagueJelly>().Play(Mathf.Min(.3f,e.rank*.025f));row.oldRank=e.rank;row.oldScore=e.medals;
  }if(focus)FocusPlayer();
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
RectTransform root,content,viewport,composition;
```
```csharp
ScrollRect scroll;
```
```csharp
TMP_Text rankText,medalsText;
```
```csharp
readonly Dictionary<string,Row> rows=new Dictionary<string,Row>();
```
```csharp
bool dirty=true,initialized;
```
```csharp
float nextTick,lastHeight=-1;
```
```csharp
const float Stride=66.12f;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
