# SfxV87QA · SfxV87QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/SfxV87QA.cs`
文件 SHA-256：`066727ec7759c9ea789e5748c5a00b0d037ec48ed388de6404ea0688b2da2e21`
[原始源码快照](../source/Assets/Scripts/SfxV87QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Heard · L17

方法 SHA-256：`126f8489e945aa74636b1a14e4f4645b09fb4c1d9fb3a0731c86083e35f63eee`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SfxV87QA.Heard(string)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
void Heard(string cue){cues.Add(cue);File.AppendAllText(dir+"/events.csv",Time.unscaledTime.ToString("F3",System.Globalization.CultureInfo.InvariantCulture)+","+cue+"\n");}
```

### Level · L18

方法 SHA-256：`9558c4e729d59547da26fd7ffadb49e322444ce52c180724acf3bc89be0c5204`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SfxV87QA.Level()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SfxV87QA.cs:42` · `SfxV87QA.Start` → `Level()`

```csharp
IEnumerator Level(){game.enabled=true;app.QAStartLevel(0);yield return app.QAWaitForLevel();for(int i=0;i<100;i++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();if(i>8&&!app.AlphabetIntroActive&&!game.IsPaused)break;yield return new WaitForSecondsRealtime(.03f);}game.QAIgnoreFocusLoss=true;}
```

### Start · L19

方法 SHA-256：`a7acbbcd6ea52adc6137aafa4cdbc9928ae35f03c2b99dd839b51aa110e28adc`
时间来源：Time.unscaledTime, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SfxV87QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");File.WriteAllText(dir+"/events.csv","time,cue\n");Application.targetFrameRate=60;QualitySettings.vSyncCount=0;
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();game=app.Session;SoundService.Played+=Heard;
  var data=JsonUtility.FromJson<Manifest>(Resources.Load<TextAsset>("Audio/DeliveryV87/manifest").text);
  foreach(var entry in data.clips){var clip=SoundService.QAClip(entry.cue);Check(clip!=null&&clip.channels==1&&clip.frequency==40000,"delivered clip decodes "+entry.cue);Check(clip==Resources.Load<AudioClip>(entry.path),"manifest resolves exact delivered path "+entry.cue);}
  foreach(string cue in new[]{"yarn_layer_remove","yarn_final_unravel","ingot_transform_land","copper_hit","copper_collect","flower_suction","victory_bomb_throw","victory_score_arrive"})Check(SoundService.HasCue(cue)&&SoundService.QAClip(cue+"_a")!=SoundService.QAClip(cue+"_b"),"distinct random variants "+cue);
  cues.Clear();for(int i=0;i<20;i++)SoundService.Play("ingot_transform_land");Check(Count("ingot_transform_land")==1,"same-frame landing burst is coalesced");yield return new WaitForSecondsRealtime(.07f);SoundService.Play("ingot_transform_land");Check(Count("ingot_transform_land")==2,"landing limiter admits next staggered ingot");
  SoundService.SetBusVolume(1,0);int silent=cues.Count;SoundService.Play("copper_hit");Check(cues.Count==silent,"SFX zero volume suppresses playback");SoundService.SetBusVolume(1,.8f);
  SoundService.CancelTransient();game.QAThirdFixture();cues.Clear();
  var layer=OpeningYarnEffect.Play(game,game.transform,Vector2.zero,false,.2f);layer.Tick(.1f);Check(Count("yarn_layer_remove")==0,"queued yarn layer is silent before animation");layer.Tick(.11f);Check(Count("yarn_layer_remove")==1,"outer layer starts its own sound");layer.Tick(.1f);Check(Count("yarn_layer_remove")==1,"layer sound is not repeated each frame");Destroy(layer.gameObject);
  var final=OpeningYarnEffect.Play(game,game.transform,Vector2.zero,true);final.Tick(.01f);Check(Count("yarn_final_unravel")==1,"final unravel has a separate cue");Destroy(final.gameObject);
  var cat=game.QAThirdCat(4,4);cat.Add(1);cat.Add(1);cat.Add(2);Check(Count("lucky_cat_charge_ready")==0,"duplicate and partial cat charge stay silent");cat.Add(3);Check(Count("lucky_cat_charge_ready")==1,"cat readiness plays once on queued cast");
  for(int i=0;i<100;i++){game.TickOpeningThird(.02f);if(i%4==0)yield return new WaitForSecondsRealtime(.02f);}
  Check(Count("lucky_cat_cast")==1&&game.OpeningIngotConversions==5,"cat cast is one sound for five conversions");Check(Count("ingot_transform_land")>0&&Count("ingot_transform_land")<=5,"ingot landing density is bounded");Check(Count("rainbow_sweep")==0&&Count("grid_settle")==0,"cat no longer reuses flower or generic landing cues");
  game.QACopperFixture();var copper=game.QACopper(hp:2);cues.Clear();game.SevenHit(copper.Brick,1);Check(Count("copper_hit")==1&&Count("copper_collect")==0,"living copper plays damage only");game.SevenHit(copper.Brick,1);Check(Count("copper_collect")==1,"zero-health copper plays retirement once");game.SevenHit(copper.Brick,1);Check(Count("copper_collect")==1,"dead copper cannot repeat retirement");
  game.QAThirdFixture();var box=game.QASecondBox();cues.Clear();game.QASecondCell(3,3);Check(Count("cookie_box_release")==1,"real biscuit release uses paper-box cue");for(int i=0;i<7;i++)game.QASecondCell(3,3);box.Tick(.5f);Check(Count("cookie_box_empty")==1,"box retirement cue is emitted once");
  foreach(int kind in new[]{1,2,3,5,6}){yield return new WaitForSecondsRealtime(.22f);game.QAThirdFixture();cues.Clear();game.QASfxRelease(kind,0,true);Check(Count(kind==3?"ingot_vortex_release":kind==2?"ingot_bomb_release":"ingot_laser_release")==1,"ingot skill selects specific sound kind="+kind);Check(Count("bomb")==0&&Count("cross_laser")==0&&Count("rainbow_sweep")==0,"ingot main sound replaces generic kind="+kind);}
  yield return new WaitForSecondsRealtime(.36f);game.QAThirdFixture();cues.Clear();game.QASfxRelease(3);Check(Count("flower_suction")==1&&Count("ingot_vortex_release")==0,"ordinary flower uses one suction cue");
  game.QAThirdFixture();cues.Clear();game.QASfxRelease(1,2);Check(Count("special_combo_release")==1&&Count("bomb")==0&&Count("cross_laser")==0,"combo core replaces stacked single-skill sounds");
  yield return new WaitForSecondsRealtime(.22f);cues.Clear();SevenWindup.Play(game,game.transform,2,0,Vector2.zero,0,0,Vector2.zero,()=>{});Check(Count("special_windup")==1&&Count("special_combo_windup")==0,"single windup chooses one cue");SevenWindup.Play(game,game.transform,2,0,Vector2.zero,1,1,Vector2.right,()=>{});Check(Count("special_combo_windup")==1,"combo windup chooses its own cue");
  var canvas=FindFirstObjectByType<Canvas>().transform;var root=UICraft.Root(canvas,"SFX star fixture",0,0,400,400);var image=BattleJellyArt.Image(root,"vine-star-off",0,0,80,80);var star=image.gameObject.AddComponent<StarChargeView>();star.Initialize(root,0);cues.Clear();star.Charge(1,image.transform.position);star.Advance(.25f);star.Advance(.4f);Check(Count("score_star_full")==1,"visible star fill crossing full emits once");star.Advance(.5f);Check(Count("score_star_full")==1,"full star idle is silent");Destroy(root.gameObject);
  foreach(int moves in new[]{0,1,5}){
   yield return Level();cues.Clear();game.QAFinaleGoal(moves,45000);float deadline=Time.unscaledTime+20;bool accelerated=false;while(app.QAVictoryPage==null&&Time.unscaledTime<deadline){if(app.QAVictoryFinale!=null&&moves==5&&!accelerated){game.AccelerateVictoryPlayback();accelerated=true;}yield return null;}
   Check(app.QAVictoryPage!=null,"result arrives after SFX finale moves="+moves);Check(Count("victory_finale_blast")==1,"one finale blast including zero bombs moves="+moves);Check(Count("victory_stars_transfer")==1,"one grouped star transfer moves="+moves);Check(moves==0?Count("victory_bomb_throw")==0:Count("victory_bomb_throw")>0&&Count("victory_bomb_throw")<=moves,"bomb throws are event bound and limited moves="+moves);Check(moves==0?Count("victory_score_arrive")==0:Count("victory_score_arrive")>0,"score arrival has its own cue moves="+moves);
  }
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");SoundService.Played-=Heard;Application.Quit(failures+errors==0?0:3);
 }
```

## 对象层级和素材入口

- L35：`game.QASecondBox()`
- L40：`UICraft.Root(canvas,"SFX star fixture",0,0,400,400)`
- L40：`BattleJellyArt.Image(root,"vine-star-off",0,0,80,80)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
AppUI app;
```
```csharp
GameSession game;
```
```csharp
string dir;
```
```csharp
int checks,failures,errors;
```
```csharp
readonly List<string> cues=new List<string>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
