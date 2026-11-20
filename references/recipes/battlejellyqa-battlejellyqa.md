# BattleJellyQA · BattleJellyQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/BattleJellyQA.cs`
文件 SHA-256：`9566a9a3b9d2de9ff658b41c0b4526b9a0ca84b6ea099be0f3ea3cb8537da16d`
[原始源码快照](../source/Assets/Scripts/BattleJellyQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L12

方法 SHA-256：`8984e384f2a90bf2f7fc197baff6ccf05e897434f6ec8585fd71dcd6bff5e0fe`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BattleJellyQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/BattleJellyQA.cs:24` · `BattleJellyQA.Start` → `Capture("top-hud-score-and-stars")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### Start · L14

方法 SHA-256：`f811147f00aaba1568d01307d1ed15e5cc2816024fd78dc5bba8751ce2af2085`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BattleJellyQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};yield return new WaitForSecondsRealtime(3);var app=FindFirstObjectByType<AppUI>();
  for(int i=0;i<45;i++){app.QAStartLevel(i);yield return app.QAWaitForLevel();for(int n=0;n<80;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.015f);if(n>10&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}var g=app.Session;g.QAIgnoreFocusLoss=true;g.SetPaused(false);Check(g.IsOpening,"opening stage "+(i+1));var summary=app.GetComponentInChildren<BattleHUDSummary>();Check(summary!=null&&summary.LevelLabel.text=="第"+(i+1)+"关","correct visible level number "+(i+1));Check(summary.ScoreLabel.text==g.Score.ToString("N0"),"current score visible "+(i+1));Check(app.GetComponentInChildren<ChainCollectionHUD>().StepsRect.GetComponentsInChildren<TMP_Text>().Any(label=>label.text==g.Shots.ToString()),"remaining moves visible "+(i+1));var cards=app.GetComponentsInChildren<ChainTaskCard>();Check(cards.Length==g.OpeningTargets.Length,"all target cards "+(i+1));for(int c=0;c<cards.Length;c++){Check(cards[c].Icon.GetComponent<UnityEngine.UI.Image>().sprite==g.OpeningTargetSprite(c),"identity sprite "+(i+1)+"/"+c);var number=cards[c].NumberRect.GetComponent<TMP_Text>();Check(number.text==g.OpeningTargets[c].count.ToString(),"remaining count "+(i+1)+"/"+c);}if(new[]{0,4,10,20,30}.Contains(i)){
 var target=g.OpeningTargets[0];int collected=g.OpeningCollected[0];typeof(GameSession).GetMethod("OpeningCollect",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance).Invoke(g,new object[]{target.kind,new Vector2(0,2),target.kind=="ordinary_color"?OpeningCampaign.Color(target.color):-1,target.kind=="ingot"});
 yield return new WaitForSecondsRealtime(.22f);yield return Capture("collect-"+target.kind+"-flight");yield return new WaitForSecondsRealtime(.55f);Check(g.OpeningCollected[0]==collected+1,"clean animation credits once "+target.kind);
 }
if(new[]{0,10,20,30,40,44}.Contains(i)){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/level-"+(i+1)+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}}
  var meter=app.GetComponentInChildren<BattleScoreMeter>();var current=app.Session;Check(meter!=null&&!meter.HasNumericLabels,"vine stars contain no numeric UI");Check(meter.GetComponentsInChildren<UnityEngine.UI.Image>().Length==9,"three jelly stars, stems and leaf junctions, no score panel or bar");Check(app.GetComponentInChildren<ChainCollectionHUD>().GoalsRect.Find("panel-goals")==null,"collection background frame removed");
  var goalRoot=app.GetComponentInChildren<ChainCollectionHUD>().GoalsRect;Check(!goalRoot.GetComponentsInChildren<TMP_Text>().Any(label=>label.text=="收集目标"||label.text=="还需收集"),"goal heading removed");var header=goalRoot.parent.parent.Find("Safe battle header");Check(header.GetComponent<BattleHUDSummary>()!=null,"left level/settings/score column restored");
  var corners=new Vector3[4];goalRoot.GetWorldCorners(corners);var canvas=goalRoot.GetComponentInParent<Canvas>();var uiCamera=canvas.renderMode==RenderMode.ScreenSpaceOverlay?null:canvas.worldCamera;float goalBottom=RectTransformUtility.WorldToScreenPoint(uiCamera,corners[0]).y;float vineTop=Camera.main.WorldToScreenPoint(BoardDisplay.Forward(new Vector2(0,JellyWallFrame.TopCenter))).y;Check(goalBottom>vineTop+4,"goal region leaves clear gap above vine");
  var ratingStars=meter.GetComponentsInChildren<UnityEngine.UI.Image>().Where(image=>image.name=="vine-star-off").ToArray();Check(ratingStars.Length==3&&ratingStars.All(image=>image.rectTransform.rect.width>=80&&image.GetComponent<UnityEngine.UI.Shadow>()!=null),"larger stars with restrained edge shadow");Check(ratingStars.All(image=>image.color.g>image.color.r&&image.color.g>image.color.b),"unearned stars have distinct green colour");
  typeof(GameSession).GetMethod("AddScore",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance).Invoke(current,new object[]{36000});yield return null;yield return null;Check(app.GetComponentInChildren<BattleHUDSummary>().ScoreLabel.text==current.Score.ToString("N0"),"score column updates with actual scoring");Check(ratingStars[0].sprite==BattleJellyArt.Get("vine-star-on"),"score lights first golden star");yield return Capture("top-hud-score-and-stars");
  int before=meter.BallResponses;current.JellyWalls.Impact(new Vector2(0,JellyWallFrame.TopCenter),1,12);meter.Advance(1f/60);Check(meter.BallResponses>before&&Mathf.Abs(meter.Swing(1))>.01f,"real top-wall contact path excites pendulum");float pausedAngle=meter.Swing(1);current.SetPaused(true);current.JellyWalls.Impact(new Vector2(0,JellyWallFrame.TopCenter),1,12);meter.Advance(.05f);Check(meter.Swing(1)==pausedAngle,"pause freezes pendulum");current.SetPaused(false);
  int brickBefore=meter.BrickResponses;typeof(GameSession).GetMethod("NotifyVineFalling",System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance).Invoke(current,new object[]{new Vector2(0,8),new Vector2(0,6),1f});meter.Advance(1f/60);Check(meter.BrickResponses>brickBefore,"falling segment touching star produces bounded impulse");
  for(int n=0;n<360;n++)meter.Advance(1f/60);Check(Mathf.Abs(meter.Swing(1))<.1f,"pendulum returns to rest after contact");
  int oldW=Screen.width,oldH=Screen.height;foreach(var size in new[]{new Vector2Int(390,844),new Vector2Int(360,780),new Vector2Int(375,667)}){
   Screen.SetResolution(size.x,size.y,false);yield return new WaitForSecondsRealtime(.2f);app.QASetViewport(null);WeChatLayoutMetrics.Set(Screen.width,Screen.height,new Rect(0,44,Screen.width,Screen.height-44),new Rect(Screen.width-104,44,96,32));yield return null;yield return null;Canvas.ForceUpdateCanvases();var badge=app.GetComponentInChildren<ChainCollectionHUD>().StepsRect;Rect blocked;WeChatLayoutMetrics.TryGetBlockedRect(new Vector2Int(Screen.width,Screen.height),out blocked);var bounds=ScreenRect(badge);Check(!bounds.Overlaps(blocked),"moves avoids WeChat capsule "+size);float vine=Camera.main.WorldToScreenPoint(BoardDisplay.Forward(new Vector2(0,JellyWallFrame.TopCenter))).y;Check(bounds.yMin>vine+2,"moves remains above vine "+size);var digit=badge.GetComponentsInChildren<TMP_Text>().First(label=>int.TryParse(label.text,out _));digit.ForceMeshUpdate();var digitCanvas=digit.GetComponentInParent<Canvas>();var digitCamera=digitCanvas.renderMode==RenderMode.ScreenSpaceOverlay?null:digitCanvas.worldCamera;float glyphHeight=RectTransformUtility.WorldToScreenPoint(digitCamera,digit.rectTransform.TransformPoint(digit.textBounds.max)).y-RectTransformUtility.WorldToScreenPoint(digitCamera,digit.rectTransform.TransformPoint(digit.textBounds.min)).y;Check(glyphHeight>=12,"moves digits stay readable "+size+" height="+glyphHeight);Check(digit.textInfo.lineCount==1,"moves number never wraps "+size);var summary=app.GetComponentInChildren<BattleHUDSummary>();Check(!ScreenRect(summary.ScoreLabel.rectTransform).Overlaps(ScreenRect(goalRoot)),"score and goals separated "+size);yield return Capture("viewport-"+size.x+"x"+size.y);
  }WeChatLayoutMetrics.Reset();Screen.SetResolution(oldW,oldH,false);yield return new WaitForSecondsRealtime(.2f);app.QASetViewport(null);
  app.QAStartFirstBounce();yield return app.QAWaitForLevel();for(int n=0;n<60;n++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.02f);}var tutorial=app.Session;Check(tutorial.IsTutorial&&tutorial.JellyGoal[4]==6&&tutorial.JellyGoal[1]==0,"tutorial six yellow, zero blue targets");Check(tutorial.CollectionBricks.Where(tutorial.IsJellyTarget).All(b=>b.Definition.colorIndex==1),"tutorial target artwork yellow");
  var actor=new GameObject("Flight sample").transform;foreach(var kind in new[]{"ordinary_color","yarn","cookie","ingot","copper_coin"}){var loot=new ChainLoot{View=actor,Start=Vector2.zero,Age=0};CollectionFlightMotion.Apply(loot,Vector2.one,kind);Check(Vector2.Distance(actor.position,Vector2.zero)<.001f,"flight starts at source "+kind);loot.Age=.3f;CollectionFlightMotion.Apply(loot,Vector2.one,kind);Check(!float.IsNaN(actor.position.x)&&actor.localScale.x>0,"flight valid midpoint "+kind);loot.Age=.6f;CollectionFlightMotion.Apply(loot,Vector2.one,kind);Check(Vector2.Distance(actor.position,Vector2.one)<.001f,"flight reaches correct target "+kind);}Destroy(actor.gameObject);
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口

- L20：`app.GetComponentInChildren<ChainCollectionHUD>().GoalsRect.Find("panel-goals")`
- L21：`goalRoot.parent.parent.Find("Safe battle header")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
string dir;
```
```csharp
int checks,failures,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
