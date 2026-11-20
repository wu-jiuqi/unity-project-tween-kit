# VictoryFinale · VictoryFinale

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/VictoryFinale.cs`
文件 SHA-256：`74c40b616fc14db063840223dd1d7fd7a8af46ffc840345c2738a994f3998977`
[原始源码快照](../source/Assets/Scripts/UI/VictoryFinale.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Begin · L10

方法 SHA-256：`c1753b6c44b2d472835a0f5c760c2fb9b9391f7c41fb5b74b80e8f0e7e32743c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.VictoryFinale.Begin(BounceGarden.GameSession, BounceGarden.ChainCollectionHUD, BounceGarden.BattleHUDSummary, BounceGarden.BattleScoreMeter, int, System.Action<UnityEngine.RectTransform[]>)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Begin(GameSession session,ChainCollectionHUD collection,BattleHUDSummary score,BattleScoreMeter scoreMeter,int before,Action<RectTransform[]> done){
  game=session;hud=collection;summary=score;meter=scoreMeter;complete=done;root=(RectTransform)transform;remaining=game.Shots;initialScore=before;finalScore=game.Score;DisplayScore=before;
  var blocker=gameObject.AddComponent<Image>();blocker.color=Color.clear;blocker.raycastTarget=true;gameObject.AddComponent<VictoryPlaybackControl>().Initialize(game);
  int count=Mathf.Clamp(remaining,0,5);bombs=new Image[count];points=new Image[count];gains=new TMP_Text[count];weights=new int[count];amounts=new int[count];colors=new int[count];slots=new Vector2[count];thrown=new bool[count];landed=new bool[count];
  from=Local(hud.StepsRect.position);scoreTarget=summary!=null?Local(summary.ScoreLabel.transform.position):new Vector2(-405,835);
  var order=new[]{0,1,2,3,4};for(int i=4;i>0;i--){int j=UnityEngine.Random.Range(0,i+1);int swap=order[i];order[i]=order[j];order[j]=swap;}
  int assigned=0,previousBonus=0;
  for(int i=0;i<count;i++){colors[i]=order[i];weights[i]=remaining/count+(i<remaining%count?1:0);assigned+=weights[i];int bonus=(int)((long)(finalScore-before)*assigned/Mathf.Max(1,remaining));amounts[i]=bonus-previousBonus;previousBonus=bonus;
   float a=(90+i*360f/Mathf.Max(1,count))*Mathf.Deg2Rad;slots[i]=new Vector2(Mathf.Cos(a),Mathf.Sin(a))*(count==1?0:115)+new Vector2(0,80);
   bombs[i]=UICraft.Box(root,"Victory thrown bomb "+i,from.x,from.y,145,145,Color.white);bombs[i].sprite=SevenSpecialArt.SpecialIcon(2,colors[i]);bombs[i].type=Image.Type.Simple;bombs[i].preserveAspect=true;bombs[i].raycastTarget=false;bombs[i].gameObject.SetActive(false);
   points[i]=BattleJellyArt.Image(root,"vine-star-on",0,0,30,30);points[i].name="Bonus starlight "+i;points[i].gameObject.SetActive(false);
   gains[i]=UICraft.Label(root,"+"+amounts[i].ToString("N0"),0,0,360,90,46,new Color(.7f,.38f,.04f));gains[i].color=new Color(1,.87f,.28f);gains[i].outlineColor=new Color(.4f,.23f,.06f);gains[i].outlineWidth=.18f;gains[i].raycastTarget=false;gains[i].gameObject.SetActive(false);
  }
  scoreEnd=count>0?.16f+(count-1)*.13f+.48f+.38f+.20f:.1f;blastAt=scoreEnd+.85f;
  blast=UICraft.Box(root,"Victory shared bomb explosion",0,80,1100,1100,Color.white);blast.type=Image.Type.Simple;blast.preserveAspect=true;blast.raycastTarget=false;blast.gameObject.SetActive(false);
  merge=UICraft.Box(root,"Original combination windup",0,80,380,380,Color.white);merge.type=Image.Type.Simple;merge.preserveAspect=true;merge.raycastTarget=false;merge.gameObject.SetActive(false);
  pigment=new Material(Resources.Load<Shader>("VictoryPigment"));pigment.SetFloat("_Count",Mathf.Max(1,count));for(int i=0;i<5;i++)pigment.SetColor("_Color"+i,SevenSpecialArt.Palette[order[i]]);blast.material=pigment;mergePigment=new Material(pigment);merge.material=mergePigment;
  white=UICraft.Box(root,"Short victory white flash",0,0,4000,5000,Color.clear);white.sprite=null;white.raycastTarget=false;
  hud.ShowVictorySteps(remaining);summary?.ShowVictoryScore(before);meter?.CountVictoryScore(before);
 }
```

### Update · L32

方法 SHA-256：`13e5839b2739196ef1b34e4d87af79d61bf8b94ffb983c812c8a0e1cb9c36a9a`
时间来源：PresentationDelta, Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryFinale.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(handed||game==null)return;Advance(game.PresentationDelta(Mathf.Min(.05f,Time.unscaledDeltaTime)));}
```

### Advance · L33

方法 SHA-256：`d34c69cb3827a23f43ffab8c467e17dae16f130597188c5e8f0badb5be30bf79`
时间来源：Time.unscaledTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.VictoryFinale.Advance(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/VictoryFinale.cs:32` · `VictoryFinale.Update` → `Advance(game.PresentationDelta(Mathf.Min(.05f,Time.unscaledDeltaTime)))`

```csharp
public void Advance(float dt){
  if(handed)return;game.TickVictoryBoard(dt);age+=Mathf.Max(0,dt);int spent=0;double shown=initialScore;
  for(int i=0;i<bombs.Length;i++){float launch=.16f+i*.13f,t=age-launch,u=Mathf.Clamp01(t/.48f);if(t>=0){spent+=weights[i];if(!thrown[i]){thrown[i]=true;ThrownCount++;game.ConvertVictorySteps(weights[i]);SoundService.Play("victory_bomb_throw");}}
   var b=bombs[i];b.gameObject.SetActive(t>=0&&age<blastAt);if(t<.48f){b.rectTransform.anchoredPosition=Vector2.Lerp(from,slots[i],u)+new Vector2(-Mathf.Sin(u*Mathf.PI)*170,Mathf.Sin(u*Mathf.PI)*110);b.rectTransform.localScale=Vector3.one*Mathf.Lerp(.55f,1,u);b.rectTransform.localRotation=Quaternion.Euler(0,0,(1-u)*-100);}
   else{float hold=t-.48f;b.rectTransform.anchoredPosition=slots[i]+JellyMotion.FloatOffset(hold,i);b.rectTransform.localScale=JellyMotion.FloatScale(hold,i);b.rectTransform.localRotation=Quaternion.Euler(0,0,JellyMotion.FloatAngle(hold,i));}
   float fly=(t-.54f)/.32f;bool flying=fly>=0&&fly<1&&amounts[i]>0;points[i].gameObject.SetActive(flying);gains[i].gameObject.SetActive(fly>=-.15f&&fly<1.5f&&amounts[i]>0);
   points[i].rectTransform.anchoredPosition=Vector2.Lerp(slots[i],scoreTarget,Mathf.Clamp01(fly))+Vector2.up*Mathf.Sin(Mathf.Clamp01(fly)*Mathf.PI)*140;
   gains[i].rectTransform.anchoredPosition=slots[i]+new Vector2(0,135+Mathf.Clamp01(fly)*80);gains[i].color=new Color(1,.84f,.19f,Mathf.Clamp01((1.5f-fly)*2));gains[i].transform.localScale=Vector3.one*(.75f+.25f*Ease((fly+.15f)/.2f));
   float arrive=launch+.86f;if(age>=arrive&&!landed[i]){landed[i]=true;summary?.ShowVictoryScore(DisplayScore,true);SoundService.Play("victory_score_arrive");}shown+=amounts[i]*Ease((age-arrive)/.2f);
  }
  int previousDisplay=DisplayScore;DisplayScore=(int)Math.Min(finalScore,Math.Round(shown));if(DisplayScore>previousDisplay&&age>=nextCountTick){nextCountTick=age+.04f;SoundService.Play("score_count_tick");}hud.ShowVictorySteps(Mathf.Max(0,remaining-spent));summary?.ShowVictoryScore(DisplayScore);if(!detached)meter?.CountVictoryScore(DisplayScore);
  float m=(age-(blastAt-.28f))/.28f;merge.gameObject.SetActive(bombs.Length>1&&m>=0&&m<1);if(merge.gameObject.activeSelf){merge.sprite=SevenSpecialArt.Frame("combo-merge",Mathf.Clamp((int)(m*7),0,6));VictoryBombArt.FrameMaterial(merge.material,merge.sprite);merge.color=new Color(1,1,1,m*.8f);}
  float explosion=age-blastAt;blast.gameObject.SetActive(explosion>=0&&explosion<.8f);if(blast.gameObject.activeSelf){blast.sprite=VictoryBombArt.Frame(Mathf.Max(1,bombs.Length),Mathf.Clamp((int)(explosion/.8f*16),0,15));VictoryBombArt.FrameMaterial(pigment,blast.sprite);blast.rectTransform.sizeDelta=Vector2.one*Mathf.Lerp(950,1650,Mathf.Clamp01(explosion/.8f));}
  if(explosion>=0&&!sounded){sounded=true;SoundService.Play("victory_finale_blast");PlayerHaptics71.Pulse(PlayerHaptics71.Strength.Heavy);}
  if(explosion>=.25f&&!cleared){cleared=true;game.SweepVictoryBoard();}
  float flash=explosion-.29f;white.color=new Color(1,1,1,FeedbackProfile.Active.enabledFeedback&&flash>=0&&flash<.16f?.72f*(1-flash/.16f):0);
  float starAge=explosion-.84f;if(starAge>=0){if(!detached){detached=true;starSoundAt=Time.unscaledTime;SoundService.Play("victory_stars_transfer");stars=meter!=null?meter.DetachVictoryStars(root):new RectTransform[0];starStarts=new Vector2[stars.Length];starSizes=new Vector2[stars.Length];for(int i=0;i<stars.Length;i++){starStarts[i]=stars[i].anchoredPosition;starSizes[i]=stars[i].rect.size;stars[i].SetAsLastSibling();}}
   float u=Ease(starAge/.48f);for(int i=0;i<stars.Length;i++){stars[i].anchoredPosition=Vector2.Lerp(starStarts[i],new Vector2((i-1)*270,i==1?365:230),u);stars[i].sizeDelta=Vector2.Lerp(starSizes[i],Vector2.one*(i==1?360:310),u);stars[i].GetComponent<StarChargeView>()?.RefreshGeometry(dt);stars[i].localScale=Vector3.one;stars[i].localRotation=Quaternion.Euler(0,0,Mathf.Lerp(0,(i-1)*-12,u));}
   if(starAge>=.65f&&Time.unscaledTime-starSoundAt>=.45f){handed=true;var done=complete;complete=null;done?.Invoke(stars);}
  }
 }
```

## 对象层级和素材入口

- L19：`UICraft.Box(root,"Victory thrown bomb "+i,from.x,from.y,145,145,Color.white)`
- L20：`BattleJellyArt.Image(root,"vine-star-on",0,0,30,30)`
- L21：`UICraft.Label(root,"+"+amounts[i].ToString("N0"),0,0,360,90,46,new Color(.7f,.38f,.04f))`
- L24：`UICraft.Box(root,"Victory shared bomb explosion",0,80,1100,1100,Color.white)`
- L25：`UICraft.Box(root,"Original combination windup",0,80,380,380,Color.white)`
- L27：`UICraft.Box(root,"Short victory white flash",0,0,4000,5000,Color.clear)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){if(pigment!=null)Destroy(pigment);if(mergePigment!=null)Destroy(mergePigment);}
```

## 源字段与依赖

```csharp
GameSession game;
```
```csharp
ChainCollectionHUD hud;
```
```csharp
BattleHUDSummary summary;
```
```csharp
BattleScoreMeter meter;
```
```csharp
RectTransform root;
```
```csharp
Action<RectTransform[]> complete;
```
```csharp
Image[] bombs,points;
```
```csharp
TMP_Text[] gains;
```
```csharp
bool[] thrown,landed;
```
```csharp
int[] weights,colors,amounts;
```
```csharp
Vector2[] slots;
```
```csharp
Vector2 from,scoreTarget;
```
```csharp
int remaining,initialScore,finalScore;
```
```csharp
float age,blastAt,scoreEnd,nextCountTick,starSoundAt=-1;
```
```csharp
bool cleared,handed,detached,sounded;
```
```csharp
Image blast,merge,white;
```
```csharp
Material pigment,mergePigment;
```
```csharp
RectTransform[] stars;
```
```csharp
Vector2[] starStarts;
```
```csharp
Vector2[] starSizes;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
