# WeChatAdaptation87QA · WeChatAdaptation87QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/WeChatAdaptation87QA.cs`
文件 SHA-256：`f81b2944703d8d7282c952e56b6d7eed351301dff6026f647a4afc68880e80a1`
[原始源码快照](../source/Assets/Scripts/WeChatAdaptation87QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L13

方法 SHA-256：`23c22b743e3a6eb52a2f10fbbdc92143f8b6e65d3228d71f7d12bfdf9513c318`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`WeChatAdaptation87QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/WeChatAdaptation87QA.cs:25` · `WeChatAdaptation87QA.CaptureGoldFrames` → `Capture("tutorial-gold-frames")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);var texture=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,texture);File.WriteAllBytes(output+"/"+name+".png",texture.EncodeToPNG());Destroy(texture);rt.Release();Destroy(rt);}
```

### CaptureGoldFrames · L14

方法 SHA-256：`6c887470904b3603ea7edb424e73aed6be67a2b967c624ea2cfcd02dc8257662`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`WeChatAdaptation87QA.CaptureGoldFrames()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/WeChatAdaptation87QA.cs:37` · `WeChatAdaptation87QA.Start` → `CaptureGoldFrames()`

```csharp
IEnumerator CaptureGoldFrames(){
  var root=UICraft.Root(FindFirstObjectByType<Canvas>().transform,"Gold focus frame visual QA");
  UICraft.Box(root,"Cream sample background",0,0,1200,2600,new Color32(242,246,226,255));
  UICraft.Label(root,"新手教程 · 金色高亮框",0,860,980,110,58,UICraft.Ink);
  var sizes=new[]{new Vector2(720,170),new Vector2(190,500),new Vector2(180,180),new Vector2(440,90)};
  var positions=new[]{new Vector2(0,580),new Vector2(-250,-60),new Vector2(200,100),new Vector2(190,-310)};
  for(int i=0;i<sizes.Length;i++){
   var p=positions[i];var size=sizes[i];UICraft.Box(root,"Target sample "+i,p.x,p.y,size.x-40,size.y-40,new Color32(213,234,202,255));
   TutorialArt.Picture(root,"Gold frame sample "+i,"target-outline",p,size);
  }
  UICraft.Label(root,"宽、窄、方形目标\n圆角与星光保持比例，只有边线延展",0,-670,960,180,38,UICraft.Ink);
  yield return Capture("tutorial-gold-frames");Destroy(root.gameObject);yield return null;
 }
```

### Start · L27

方法 SHA-256：`557de5b3d9ed2a9db7ec04a0d5fc423ec238697818ced1ae8a1d16b66a623b99`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`WeChatAdaptation87QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();output=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(output);File.WriteAllText(output+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(output+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);var app=FindFirstObjectByType<AppUI>();
  foreach(var size in new[]{new Vector2Int(320,568),new Vector2Int(390,844),new Vector2Int(430,932)}){
   Screen.SetResolution(size.x,size.y,false);yield return new WaitForSecondsRealtime(.4f);
   float top=size.y>700?47:20,bottom=size.y>700?34:0;
   WeChatLayoutMetrics.Set(Screen.width,Screen.height,new Rect(0,top,Screen.width,Screen.height-top-bottom),new Rect(Screen.width-100,top+8,92,32));app.QASetViewport(null);app.QANavigate("home");yield return null;yield return null;Canvas.ForceUpdateCanvases();
   var home=app.GetComponentInChildren<ChapterHomeView>();var setting=home.GetComponentsInChildren<RectTransform>().First(r=>r.name=="Settings medallion");Rect blocked;WeChatLayoutMetrics.TryGetBlockedRect(new Vector2Int(Screen.width,Screen.height),out blocked);
   Check(Bounds(setting).height*148/176>32,"home settings visibly larger than native button "+size);
   Check(Bounds(setting).center.y<=blocked.yMin+8&&Bounds(setting).yMin<blocked.yMin-16,"home settings lower half below capsule "+size);
   var bg=app.GetComponentsInChildren<RectTransform>().First(r=>r.name=="Live theme background");Check(Bounds(bg).yMin<1&&Bounds(bg).yMax>=Screen.height-1,"home full bleed "+size);yield return Capture("home-"+size.x);if(size.x==390)yield return CaptureGoldFrames();
   app.QAOpenChapters();yield return null;yield return null;Canvas.ForceUpdateCanvases();
   var map=app.GetComponentInChildren<JellyJourney80>();var headArt=Bounds(map.Header.Find("panel-header") as RectTransform);var footArt=Bounds(map.Footer.Find("panel-footer") as RectTransform);
   Check(headArt.yMax>=Screen.height-1&&headArt.xMin<=0&&headArt.xMax>=Screen.width,"chapter header art reaches physical edges "+size);
   Check(footArt.yMin<=1&&footArt.xMin<=0&&footArt.xMax>=Screen.width,"chapter footer art reaches physical edges "+size);
   Check(!map.GetComponentsInChildren<RectTransform>().Any(r=>r.name=="pill-currency"||r.name=="icon-coin"),"chapter currency removed "+size);
   var title=map.GetComponentsInChildren<TMP_Text>().First(t=>t.text=="关卡之旅");var back=map.Header.Find("Jelly back") as RectTransform;
   Check(!Bounds(title.rectTransform).Overlaps(blocked)&&!Bounds(back).Overlaps(blocked),"chapter title and back avoid WeChat capsule "+size);
   Check(Bounds((RectTransform)map.PlayButton.transform).yMin>=app.MenuSafeRect.yMin,"chapter CTA avoids home gesture "+size);
   Check(Bounds(map.Viewport).yMax<=Bounds(map.Header).yMin+2&&Bounds(map.Viewport).yMin>=Bounds(map.Footer).yMax-2,"chapter map clipped clear of fixed controls "+size);
   yield return Capture("chapters-"+size.x);
   foreach(int level in new[]{0,30,44}){
    app.QAStartLevel(level);yield return app.QAWaitForLevel();for(int i=0;i<100;i++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.015f);if(i>10&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}
    var hud=app.GetComponentInChildren<ChainCollectionHUD>();var summary=app.GetComponentInChildren<BattleHUDSummary>();var settings=app.GetComponentsInChildren<RectTransform>().First(r=>r.name=="Battle settings");var score=Bounds(summary.ScoreLabel.rectTransform);var goals=Bounds(hud.GoalsRect);var steps=Bounds(hud.StepsRect);float wall=Camera.main.WorldToScreenPoint(BoardDisplay.Forward(new Vector2(0,JellyWallFrame.TopCenter))).y;
    Check(Camera.main.rect==new Rect(0,0,1,1)&&Mathf.Abs(Camera.main.aspect-1080f/2340f)<.0001f,"battle projection stretches to full screen "+size+"/"+level);
    var corners=app.QALayoutCorners();Check(corners.All(p=>p.x>=-1&&p.y>=-1&&p.x<=Screen.width+1&&p.y<=Screen.height+1)&&corners.Max(p=>p.x)-corners.Min(p=>p.x)>=Screen.width-1&&corners.Max(p=>p.y)-corners.Min(p=>p.y)>=Screen.height-1,"battle UI covers both screen axes "+size+"/"+level);
    Check(GameObject.Find("Full screen original garden fill")==null,"no edge extension around board "+size+"/"+level);
    Check(Bounds(summary.LevelLabel.rectTransform).yMax<=Screen.height-top+1,"left header clears status bar "+size+"/"+level);
    Check(Bounds(summary.LevelLabel.rectTransform).center.y>Bounds(settings).center.y,"left level then settings "+size+"/"+level);
    Check(Bounds(settings).yMin>score.yMax,"left settings then score "+size+"/"+level);
    Check(score.yMin>wall+4&&goals.yMin>wall+4&&steps.yMin>wall+4,"information clears top wall "+size+"/"+level);
    Check(!score.Overlaps(goals)&&!steps.Overlaps(goals),"three columns separated "+size+"/"+level);
    Check(steps.yMax<=blocked.yMin+17,"capsule overlap bounded "+size+"/"+level);
    Check(summary.LevelLabel.text=="第"+(level+1)+"关"&&hud.Cards.Count==app.Session.OpeningTargets.Length,"actual level and targets "+size+"/"+level);yield return Capture("battle-"+size.x+"-"+(level+1));
    if(level==0){app.Session.QAIgnoreFocusLoss=true;int before=app.Session.Shots;app.QADragAim(new Vector2(.3f,1));yield return null;yield return null;Check(app.Session.Shots==before-1,"stretched view preserves drag input "+size);}
   }
  }
  File.WriteAllText(output+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口

- L15：`UICraft.Root(FindFirstObjectByType<Canvas>().transform,"Gold focus frame visual QA")`
- L16：`UICraft.Box(root,"Cream sample background",0,0,1200,2600,new Color32(242,246,226,255))`
- L17：`UICraft.Label(root,"新手教程 · 金色高亮框",0,860,980,110,58,UICraft.Ink)`
- L21：`UICraft.Box(root,"Target sample "+i,p.x,p.y,size.x-40,size.y-40,new Color32(213,234,202,255))`
- L24：`UICraft.Label(root,"宽、窄、方形目标\n圆角与星光保持比例，只有边线延展",0,-670,960,180,38,UICraft.Ink)`
- L39：`map.Header.Find("panel-header")`
- L39：`map.Footer.Find("panel-footer")`
- L43：`map.Header.Find("Jelly back")`
- L53：`GameObject.Find("Full screen original garden fill")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
string output;
```
```csharp
int checks,failures,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
