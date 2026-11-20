# PlayableTutorialGuide · PlayableTutorialGuide

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/PlayableTutorialGuide.cs`
文件 SHA-256：`2f3bea325bad77b03e38784bbd30b2772a48c4524b04dac3efb74db847d72747`
[原始源码快照](../source/Assets/Scripts/UI/PlayableTutorialGuide.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L24

方法 SHA-256：`aeda375b30ca35b37af05c2d6c340dd160edf720ee32a001b997af7a9da725ed`
时间来源：Time.deltaTime
曲线证据：Mathf.Sin, Mathf.SmoothStep
解析符号：`BounceGarden.PlayableTutorialGuide.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){
  if(closed||game==null||game.Generation!=generation||!game.IsPlaying){if(visual!=null)visual.alpha=0;return;}
  if(game.IsPaused){visual.alpha=0;ReleaseAim();return;}clock+=Time.deltaTime;
  if(kind>0&&game.ChainKindActivations[kind-1]>initialActivations){app.Profile.MarkPowerPractice(kind);Close();return;}
  Vector2 target=Vector2.zero;bool hasTarget=false;
  if(kind==0){var b=game.CollectionBricks.Where(b=>b.Alive&&game.IsJellyTarget(b)).OrderBy(b=>b.transform.position.y).ThenBy(b=>Mathf.Abs(b.transform.position.x)).FirstOrDefault();if(b!=null){target=b.transform.position;hasTarget=true;}}
  else{var tool=game.ChainTools.FirstOrDefault(t=>t.Kind==kind);if(tool!=null){target=tool.Position;hasTarget=true;}}
  if(kind==0){if(game.JellyCollected[4]>seenCollected){seenCollected=game.JellyCollected[4];notice="黄色砖块飞进目标卡啦！";noticeUntil=clock+3;}else if(game.BreakCount>breaks&&clock>noticeUntil){notice=game.Shots==19?"每步连发五颗球。\n弹跳期间也能再次发射！":"砖块在2秒后下落。\n利用空隙，弹向更高处！";noticeUntil=clock+3;}breaks=game.BreakCount;text.text=clock<noticeUntil?notice:game.Shots==20?"拖动瞄准，松手发射。\n收集全部6块黄色砖块。":clock%12<6?"击碎剩余黄色砖块。\n补入的白砖不会增加收集目标。":"利用墙壁反弹，命中更多果冻。\n无需清空所有砖块。";}
  else text.text=kind==1?"命中十字激光即可激活。\n一组连发直接击碎4块砖，可获得更多！":kind==2?"命中炸弹，爆破周围3×3区域。\n击碎越多，奖励越丰富！":"命中五色花，按绑定色发动效果。\n附近有特殊砖时优先组合！";
  bool show=hasTarget&&!pointer&&clock-lastInput>5&&game.ActiveBalls==0&&game.PendingBurstBalls==0;visual.alpha=show?1:0;hand.gameObject.SetActive(show);
  if(!show){ReleaseAim();return;}
  float t=(clock-lastInput)%4.4f;Vector2 direction=GameSession.NormalizeAimDirection(target-game.LauncherPosition);Vector2 origin=Local(game.LauncherPosition),end=Local(game.LauncherPosition+direction*2.1f);float u=Mathf.SmoothStep(0,1,Mathf.Clamp01((t-.6f)/1.3f));Vector2 tip=Vector2.Lerp(origin,end,u);
  hand.sprite=OnboardingArt.Get(2,t<.6f?1:t>2.2f?3:2);ring.sprite=OnboardingArt.Get(2,5);hand.rectTransform.anchoredPosition=tip+new Vector2(68,-62);ring.rectTransform.anchoredPosition=tip;ring.rectTransform.localScale=Vector3.one*(.85f+.12f*Mathf.Sin(t*8));outline.rectTransform.anchoredPosition=Local(target);outline.rectTransform.localScale=Vector3.one*(1+.05f*Mathf.Sin(clock*4));
  hand.color=new Color(1,1,1,t>3?Mathf.Clamp01(4-t):1);ring.color=hand.color;
  if(t>.6f&&t<2.4f){game.SetAim(direction);ownsAim=true;}else ReleaseAim();
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){ReleaseAim();}
```

## 源字段与依赖

```csharp
AppUI app;
```
```csharp
GameSession game;
```
```csharp
int kind,generation,breaks,initialActivations,seenCollected;
```
```csharp
float clock,lastInput=-10,noticeUntil;
```
```csharp
bool pointer,ownsAim,closed;
```
```csharp
Image hand,ring,outline;
```
```csharp
TMP_Text text;
```
```csharp
CanvasGroup visual;
```
```csharp
RectTransform root;
```
```csharp
string notice;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
