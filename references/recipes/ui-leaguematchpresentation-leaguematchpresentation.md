# LeagueMatchPresentation · LeagueMatchPresentation

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/LeagueMatchPresentation.cs`
文件 SHA-256：`6d2a42eadf7cf4f89ff9cef546194f4e43e340075f37e9c88fcc163ba8b3f9d1`
[原始源码快照](../source/Assets/Scripts/UI/LeagueMatchPresentation.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `DecorativeBreath` / `breath`：仅提取徽章1+.035sin(3t)；不包含匹配、排行、日期或确认保存

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### Build · L15

方法 SHA-256：`9a9c70134395b842f66df8db260738968f94d05c61d0560ee6c73bae61522e75`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.LeagueMatchPresentation.Build(BounceGarden.LocalProfile, BounceGarden.LeagueSnapshot, System.Action)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
void Build(LocalProfile p,LeagueSnapshot s,Action completed){
  profile=p;snapshot=s;done=completed;var rt=(RectTransform)transform;rt.anchorMin=Vector2.zero;rt.anchorMax=Vector2.one;rt.sizeDelta=Vector2.zero;
  var curtain=gameObject.AddComponent<Image>();curtain.color=new Color32(248,244,235,255);curtain.raycastTarget=true;
  content=UICraft.Root(transform,"League entry composition");Fit();
  UICraft.Label(content,p.AlphabetUnlocked==10&&s.player.medals==1?"花园联赛已开启":"新一期花园联赛",0,635,980,115,62,UICraft.Ink);
  emblem=LeagueMotionArt.Image(content,"season",0,430,270).rectTransform;
  detail=UICraft.Label(content,"本期对手集结",0,240,940,70,38,UICraft.Ink);
  LeaguePortrait.Create(content,p,"",0,-30,200,true);
  var rivals=s.entries.Where(e=>!e.player).OrderBy(e=>Math.Abs(e.rank-s.player.rank)).Take(6).ToArray();portraits=new RectTransform[rivals.Length];
  for(int i=0;i<rivals.Length;i++){float a=i*Mathf.PI*2/rivals.Length;var v=LeaguePortrait.Create(content,p,rivals[i].portrait,Mathf.Cos(a)*330,Mathf.Sin(a)*170-30,120,false,rivals[i].frame);UICraft.Label(v,rivals[i].name,0,-85,220,50,25,UICraft.Ink);portraits[i]=v;v.localScale=Vector3.zero;}
  status=UICraft.Label(content,"虚拟对手正在入场",0,-340,940,85,37,UICraft.Ink);
  var start=DateTimeOffset.FromUnixTimeSeconds(s.config.calendarSeasons?LeagueEngine.CalendarStart(p.LeagueClock.UtcSeconds):p.LeagueClock.UtcSeconds).ToOffset(TimeSpan.FromHours(8));
  var end=DateTimeOffset.FromUnixTimeSeconds(p.LeagueClock.UtcSeconds+s.remaining).ToOffset(TimeSpan.FromHours(8));
  UICraft.Label(content,start.ToString("M月d日")+" — "+end.AddSeconds(-1).ToString("M月d日")+" · 剩余"+Math.Max(1,(int)Math.Ceiling(s.remaining/86400d))+"天",0,-440,960,65,29,UICraft.Ink);
  var button=UICraft.Button(content,"跳过动画",0,-575,740,130,UICraft.Green,Continue,43);button.name="League entry continue";buttonText=button.GetComponentInChildren<TMP_Text>();
  UICraft.Label(content,"主线首通+1奖章 · 每日重玩最多3枚\n虚拟对手联赛 · 离线对手不增长",0,-725,1000,105,27,UICraft.Ink);SoundService.Play("whoosh");
 }
```

### Update · L33

方法 SHA-256：`380300fcc1add32602d9219176caa362ade0cdbd752378ea3953035231c9f432`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.LeagueMatchPresentation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(!focused||suspended)return;Fit();age+=Mathf.Min(Time.unscaledDeltaTime,.05f);bool motion=FeedbackProfile.Active.enabledFeedback;
  emblem.localScale=Vector3.one*(motion?1+.035f*Mathf.Sin(age*3):1);
  for(int i=0;i<portraits.Length;i++){float t=Mathf.Clamp01((age-.15f-i*.23f)/.32f);portraits[i].localScale=Vector3.one*(motion?UICraft.OutBack(t):(t>0?1:0));}
  if(age>=2.1f&&!summary)Summary();
 }
```

## 对象层级和素材入口

- L18：`UICraft.Root(transform,"League entry composition")`
- L19：`UICraft.Label(content,p.AlphabetUnlocked==10&&s.player.medals==1?"花园联赛已开启":"新一期花园联赛",0,635,980,115,62,UICraft.Ink)`
- L20：`LeagueMotionArt.Image(content,"season",0,430,270)`
- L21：`UICraft.Label(content,"本期对手集结",0,240,940,70,38,UICraft.Ink)`
- L24：`UICraft.Label(v,rivals[i].name,0,-85,220,50,25,UICraft.Ink)`
- L25：`UICraft.Label(content,"虚拟对手正在入场",0,-340,940,85,37,UICraft.Ink)`
- L28：`UICraft.Label(content,start.ToString("M月d日")+" — "+end.AddSeconds(-1).ToString("M月d日")+" · 剩余"+Math.Max(1,(int)Math.Ceiling(s.remaining/86400d))+"天",0,-440,960,65,29,UICraft.Ink)`
- L30：`UICraft.Label(content,"主线首通+1奖章 · 每日重玩最多3枚\n虚拟对手联赛 · 离线对手不增长",0,-725,1000,105,27,UICraft.Ink)`

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
RectTransform content,emblem;
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
bool summary,focused=true,suspended;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
