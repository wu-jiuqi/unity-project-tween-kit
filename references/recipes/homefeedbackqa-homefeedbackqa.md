# HomeFeedbackQA · HomeFeedbackQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/HomeFeedbackQA.cs`
文件 SHA-256：`57ff14b4fef010808df4dac1c0ea199af47a31a065e7d126aa843a8995df30e3`
[原始源码快照](../source/Assets/Scripts/HomeFeedbackQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L19

方法 SHA-256：`55899212230dc9929e01dcb6491a7715e0d8e7ec4eb15a00418f3a20f895f31d`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.HomeFeedbackQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);Application.logMessageReceived+=Log;
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();Check(RuntimeQA.IsIsolated&&LocalTestTools.Available,"QA and test tools isolated from normal player profile");
  if(Array.IndexOf(args,"--feedback-seed")>=0||Array.IndexOf(args,"--feedback-reload")>=0){
   var store=new LocalHomeFeedbackStorage("HomeFeedbackV13Lifecycle");var p=new LocalHomeFeedbackSource(store);
   if(Array.IndexOf(args,"--feedback-seed")>=0){Check(p.Reset()&&p.Visit()&&p.StartLevel(),"seed isolated persistent slot");for(int i=0;i<3;i++)p.RecordResult("persistent-"+i,i,true,true);Check(p.Read().WinStreak==3&&p.ConsumeCelebration(p.Read().Key),"save streak and consumed milestone to disk");}
   else{Check(p.Read().WinStreak==3&&p.Read().LoginDays==1&&p.Read().PlayedToday,"new process restores login, streak and daily display mode");Check(!p.ConsumeCelebration(p.Read().Key),"new process does not replay consumed milestone");Check(p.Reset(),"clean only isolated lifecycle slot");}
   Finish();yield break;
  }
  Check(LocalHomeFeedbackSource.RunSelfTests().StartsWith("PASS:"),"local date, copy, deduplication and persistence rules pass");
  int index=Array.IndexOf(args,"--qa-viewport");var size=(index>=0?args[index+1]:"540x1170").Split('x');width=int.Parse(size[0]);height=int.Parse(size[1]);
  target=new RenderTexture(width,height,24);target.Create();Camera.main.targetTexture=target;Camera.main.rect=new Rect(0,0,1,1);
  var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=Camera.main;canvas.planeDistance=1;
  app.QASetViewport(new Vector2Int(width,height));yield return null;app.QASetViewport(new Vector2Int(width,height));app.QANavigate("home");yield return new WaitForSecondsRealtime(1);
  Check(GameObject.Find("Header wordmark")!=null&&GameObject.Find("Header wordmark").transform.childCount==12,"twelve-letter text logo moved to header");
  Check(GameObject.Find("Home wordmark")==null&&GameObject.Find("Home achievement feedback")!=null,"central logo replaced by achievement feedback");
  var saved=Source("savedFeedback");Check(saved.Read().LoginDays==1&&!saved.Read().PlayedToday,"first home shows day-one visit feedback");
  yield return Capture("home-first-visit");Call("Settings");yield return new WaitForSecondsRealtime(.3f);yield return Capture("settings");
  Check(GameObject.Find("Button Test Tools")!=null,"settings exposes Test Tools in test build");Call("OpenTestTools");yield return new WaitForSecondsRealtime(.3f);yield return Capture("test-tools");
  Call("PreviewFeedback",7,0,false);yield return new WaitForSecondsRealtime(.9f);Check(Source("feedback").Read().Title=="A whole week!"&&saved.Read().LoginDays==1,"7-day preview is temporary");yield return Capture("home-7-days");
  Call("PreviewFeedback",1,5,true);yield return new WaitForSecondsRealtime(.9f);Check(Source("feedback").Read().WinStreak==5&&saved.Read().WinStreak==0,"5-win preview does not change saved streak");yield return Capture("home-5-wins");
  Call("PreviewFeedback",8,5,false);yield return new WaitForSecondsRealtime(.5f);Check(!Source("feedback").Read().PlayedToday&&Source("feedback").Read().LoginDays==8,"next-day preview uses daily feedback");yield return Capture("home-next-day");
  Call("UseSavedFeedback");yield return new WaitForSecondsRealtime(.4f);Check(ReferenceEquals(Source("feedback"),saved)&&saved.Read().WinStreak==0,"Use saved data exits preview");
  saved.StartLevel();for(int i=0;i<3;i++)saved.RecordResult("ui-streak-"+i,i,true,true);app.Profile.CommitBase("ui-profile",0);app.Profile.SoundEnabled=false;
  app.QANavigate("home");yield return new WaitForSecondsRealtime(.9f);Check(saved.Read().WinStreak==3&&saved.Read().Milestone,"actual saved source drives milestone feedback");yield return Capture("home-saved-3-wins");
  Call("ConfirmLocalReset",false);yield return new WaitForSecondsRealtime(.3f);Check(saved.Read().WinStreak==3,"opening reset confirmation does not erase anything");yield return Capture("confirm-reset-streaks");
  GameObject.Find("Button Cancel").GetComponent<Button>().onClick.Invoke();yield return new WaitForSecondsRealtime(.3f);Check(saved.Read().WinStreak==3&&app.Profile.UnlockedLevel==1,"cancel preserves streak and progression");
  Call("ConfirmLocalReset",false);yield return new WaitForSecondsRealtime(.3f);GameObject.Find("Button Reset").GetComponent<Button>().onClick.Invoke();yield return new WaitForSecondsRealtime(.6f);
  Check(saved.Read().WinStreak==0&&saved.Read().LoginDays==1&&!saved.Read().PlayedToday,"partial reset returns to day one and zero wins");
  Check(app.Profile.UnlockedLevel==1&&app.Profile.Coins==1600&&!app.Profile.SoundEnabled&&app.Profile.HasBaseReward("ui-profile"),"partial reset preserves progress, wallet, sound and transaction ledger");yield return Capture("home-after-streak-reset");
  Call("ConfirmLocalReset",true);yield return new WaitForSecondsRealtime(.3f);yield return Capture("confirm-reset-all");GameObject.Find("Button Reset").GetComponent<Button>().onClick.Invoke();yield return new WaitForSecondsRealtime(.6f);
  Check(app.Profile.UnlockedLevel==0&&app.Profile.Coins==1580&&app.Profile.Badges==0&&app.Profile.SoundEnabled&&app.Profile.CommittedTransactionCount==0,"full reset clears only game's owned profile state");
  Check(saved.Read().LoginDays==1&&saved.Read().WinStreak==0&&ReferenceEquals(Source("feedback"),saved),"full reset clears feedback and preview mode");yield return Capture("home-after-full-reset");
  // Starting a real level while previewing must resume writing the genuine save source.
  Call("PreviewFeedback",30,20,true);app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.9f);Check(ReferenceEquals(Source("feedback"),saved)&&saved.Read().PlayedToday&&saved.Read().WinStreak==0,"starting gameplay exits preview and records real daily phase");
  Call("OnOutcome",true);Check(saved.Read().WinStreak==1,"actual AppUI outcome hook records victory immediately");app.QANavigate("home");yield return new WaitForSecondsRealtime(.7f);Call("OnOutcome",true);Check(saved.Read().WinStreak==1,"late duplicate outcome cannot add a win");yield return Capture("home-after-real-outcome");
  app.QAStartLevel(1);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.9f);Call("OnOutcome",false);Check(saved.Read().WinStreak==0&&saved.Read().AfterLoss,"failure callback clears genuine streak");app.QANavigate("home");yield return new WaitForSecondsRealtime(.6f);yield return Capture("home-after-loss");
  app.QASetSafeArea(new Rect(width*.025f,height*.035f,width*.96f,height*.91f));yield return null;yield return Capture("home-safe-area");
  Call("Settings");yield return new WaitForSecondsRealtime(.3f);Call("OpenTestTools");yield return new WaitForSecondsRealtime(.3f);yield return Capture("test-tools-safe-area");
  File.WriteAllText(Path.Combine(dir,"mode.txt"),"Complete GPU viewport "+width+"x"+height+"; real player profile never read or cleared. UI callbacks simulated explicitly, not physical device input.\n");Finish();
 }
```

### Capture · L61

方法 SHA-256：`fa1d993879a0d2a798228537291ffc0d870bc9a68d26bbf20693dbad325ee931`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.HomeFeedbackQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/HomeFeedbackQA.cs:36` · `HomeFeedbackQA.Start` → `Capture("home-first-visit")`
- `Assets/Scripts/HomeFeedbackQA.cs:36` · `HomeFeedbackQA.Start` → `Capture("settings")`
- `Assets/Scripts/HomeFeedbackQA.cs:37` · `HomeFeedbackQA.Start` → `Capture("test-tools")`
- `Assets/Scripts/HomeFeedbackQA.cs:38` · `HomeFeedbackQA.Start` → `Capture("home-7-days")`
- `Assets/Scripts/HomeFeedbackQA.cs:39` · `HomeFeedbackQA.Start` → `Capture("home-5-wins")`
- `Assets/Scripts/HomeFeedbackQA.cs:40` · `HomeFeedbackQA.Start` → `Capture("home-next-day")`
- `Assets/Scripts/HomeFeedbackQA.cs:43` · `HomeFeedbackQA.Start` → `Capture("home-saved-3-wins")`
- `Assets/Scripts/HomeFeedbackQA.cs:44` · `HomeFeedbackQA.Start` → `Capture("confirm-reset-streaks")`
- `Assets/Scripts/HomeFeedbackQA.cs:48` · `HomeFeedbackQA.Start` → `Capture("home-after-streak-reset")`
- `Assets/Scripts/HomeFeedbackQA.cs:49` · `HomeFeedbackQA.Start` → `Capture("confirm-reset-all")`
- `Assets/Scripts/HomeFeedbackQA.cs:51` · `HomeFeedbackQA.Start` → `Capture("home-after-full-reset")`
- `Assets/Scripts/HomeFeedbackQA.cs:54` · `HomeFeedbackQA.Start` → `Capture("home-after-real-outcome")`
- `Assets/Scripts/HomeFeedbackQA.cs:55` · `HomeFeedbackQA.Start` → `Capture("home-after-loss")`
- `Assets/Scripts/HomeFeedbackQA.cs:56` · `HomeFeedbackQA.Start` → `Capture("home-safe-area")`
- `Assets/Scripts/HomeFeedbackQA.cs:57` · `HomeFeedbackQA.Start` → `Capture("test-tools-safe-area")`

```csharp
IEnumerator Capture(string name){
  yield return new WaitForEndOfFrame();Canvas.ForceUpdateCanvases();
  int overflow=0;foreach(var b in FindObjectsByType<Button>(FindObjectsSortMode.None)){var group=b.GetComponentInParent<CanvasGroup>();if(group!=null&&!group.blocksRaycasts)continue;var bounds=Bounds(b.GetComponent<RectTransform>());if(bounds.xMin< -1||bounds.yMin< -1||bounds.xMax>width+1||bounds.yMax>height+1)overflow++;}
  Check(overflow==0,"active buttons within full viewport "+name+" overflow="+overflow);
  var logo=GameObject.Find("Header wordmark");if(logo!=null){var bounds=Bounds(logo.GetComponent<RectTransform>());var avatar=Bounds(GameObject.Find("Avatar medallion").GetComponent<RectTransform>());var settings=Bounds(GameObject.Find("Settings medallion").GetComponent<RectTransform>());Check(bounds.xMin>avatar.xMax&&bounds.xMax<settings.xMin,"header wordmark between avatar and settings "+name);}
  foreach(var view in FindObjectsByType<HomeFeedbackView>(FindObjectsSortMode.None))foreach(var text in view.GetComponentsInChildren<TMP_Text>()){text.ForceMeshUpdate();Check(text.GetPreferredValues(text.text,text.rectTransform.rect.width,Mathf.Infinity).y<=text.rectTransform.rect.height+6,"feedback text fits "+name+" "+text.text);}
  Camera.main.Render();var previous=RenderTexture.active;RenderTexture.active=target;var image=new Texture2D(width,height,TextureFormat.RGB24,false);image.ReadPixels(new Rect(0,0,width,height),0,0);image.Apply();RenderTexture.active=previous;File.WriteAllBytes(Path.Combine(dir,name+".png"),image.EncodeToPNG());Destroy(image);
 }
```

## 对象层级和素材入口

- L33：`GameObject.Find("Header wordmark")`
- L34：`GameObject.Find("Home wordmark")`
- L37：`GameObject.Find("Button Test Tools")`
- L45：`GameObject.Find("Button Cancel")`
- L46：`GameObject.Find("Button Reset")`
- L49：`GameObject.Find("Button Reset")`
- L65：`GameObject.Find("Header wordmark")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
AppUI app;
```
```csharp
string dir;
```
```csharp
int checks,failed,errors,width,height;
```
```csharp
RenderTexture target;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
