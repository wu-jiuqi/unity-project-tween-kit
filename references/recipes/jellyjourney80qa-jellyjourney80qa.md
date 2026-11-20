# JellyJourney80QA · JellyJourney80QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/JellyJourney80QA.cs`
文件 SHA-256：`0e5f5876cb847692f627924045e28d9c95cdca786d73b70c2f637fb5314e63a7`
[原始源码快照](../source/Assets/Scripts/JellyJourney80QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L11

方法 SHA-256：`a9b8e2e99b6e8de94287999399e8febb81a77c66d9118010bfd03d3598ba5f83`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyJourney80QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/JellyJourney80QA.cs:16` · `JellyJourney80QA.Start` → `Capture("01-new-profile")`
- `Assets/Scripts/JellyJourney80QA.cs:17` · `JellyJourney80QA.Start` → `Capture("02-current36")`
- `Assets/Scripts/JellyJourney80QA.cs:18` · `JellyJourney80QA.Start` → `Capture("03-elastic-release")`
- `Assets/Scripts/JellyJourney80QA.cs:21` · `JellyJourney80QA.Start` → `Capture("motion-frames/frame-"+frame.ToString("D3"))`
- `Assets/Scripts/JellyJourney80QA.cs:22` · `JellyJourney80QA.Start` → `Capture("levels1-8-spacing")`
- `Assets/Scripts/JellyJourney80QA.cs:22` · `JellyJourney80QA.Start` → `Capture("levels29-36-spacing")`
- `Assets/Scripts/JellyJourney80QA.cs:25` · `JellyJourney80QA.Start` → `Capture("wechat-safe-area")`
- `Assets/Scripts/JellyJourney80QA.cs:26` · `JellyJourney80QA.Start` → `Capture("03-replay-battle")`
- `Assets/Scripts/JellyJourney80QA.cs:28` · `JellyJourney80QA.Start` → `Capture("04-win-return")`
- `Assets/Scripts/JellyJourney80QA.cs:30` · `JellyJourney80QA.Start` → `Capture("05-all-clear")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### OpenMap · L12

方法 SHA-256：`8c1babc9eb913f4e000a1e79baff67cef7645597bef09be7abfef10da45140e0`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyJourney80QA.OpenMap()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/JellyJourney80QA.cs:16` · `JellyJourney80QA.Start` → `OpenMap()`
- `Assets/Scripts/JellyJourney80QA.cs:29` · `JellyJourney80QA.Start` → `OpenMap()`
- `Assets/Scripts/JellyJourney80QA.cs:30` · `JellyJourney80QA.Start` → `OpenMap()`

```csharp
IEnumerator OpenMap(){app.QAOpenChapters();yield return null;yield return null;}
```

### Battle · L13

方法 SHA-256：`797b5d067ea6b88041d3a8a31775c48850cb2ebfed0cabdbf67c8181160b2c13`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyJourney80QA.Battle()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/JellyJourney80QA.cs:26` · `JellyJourney80QA.Start` → `Battle()`
- `Assets/Scripts/JellyJourney80QA.cs:28` · `JellyJourney80QA.Start` → `Battle()`
- `Assets/Scripts/JellyJourney80QA.cs:29` · `JellyJourney80QA.Start` → `Battle()`

```csharp
IEnumerator Battle(){yield return app.QAWaitForLevel();for(int n=0;n<180;n++){if(app.AlphabetIntroActive)app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();if(!app.AlphabetIntroActive&&app.GetComponentInChildren<StreakRewardPresentation>()==null&&!app.Session.IsPaused)break;yield return new WaitForSecondsRealtime(.035f);}app.Session.QAIgnoreFocusLoss=true;app.Session.SetPaused(false);}
```

### Start · L15

方法 SHA-256：`ee8093f60fac39e0e5a635b047f0e21bc883397cb23bbf88177bc4522b206268`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyJourney80QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];int f=Array.IndexOf(args,"--qa-fps");if(f>=0)fps=int.Parse(args[f+1]);Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.targetFrameRate=fps;QualitySettings.vSyncCount=0;Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};yield return new WaitForSecondsRealtime(4);app=FindFirstObjectByType<AppUI>();ProfileChecks();
  app.Profile.ResetForTesting();yield return OpenMap();Check(Map!=null&&Map.LevelCount==AlphabetCampaign.Count&&Map.FocusIndex==0,"new profile opens 78-level continuous tree at level 1");Check(!app.GetComponentsInChildren<MonoBehaviour>().Any(x=>x.GetType().Name=="GardenRoute70")&&!app.GetComponentsInChildren<TMP_Text>().Any(t=>t.text.Contains("第1章")),"chapter cards and old rail are not instantiated");Check(!app.StartTreeLevel(1),"locked level rejected even in isolated audit through public map entry");Check(Map.LevelButton(0)!=null&&Map.LevelButton(0).interactable,"new profile current level is actionable");yield return Capture("01-new-profile");
  for(int i=0;i<35;i++)app.Profile.CommitAlphabetWin("tree-ui-"+i,i,i%3==0?65000:i%3==1?35000:10000);yield return null;Map.Recenter();yield return null;Check(Map.FocusIndex==35&&Map.LevelButton(35).interactable,"current level36 follows real profile progress");Check(!Map.LevelButton(36).interactable,"future level37 stays locked");Check(ExecuteEvents.GetEventHandler<IDragHandler>(Map.LevelButton(35).gameObject)==Map.Scroll.gameObject,"drag beginning on a level is owned by ScrollRect, not a button effect");yield return new WaitForSecondsRealtime(.95f);yield return Capture("02-current36");
  var fixedButton=Map.LevelButton(35);var fixedRect=(RectTransform)fixedButton.transform;Rect fixedBefore=Bounds(fixedRect);var pressEvent=new PointerEventData(EventSystem.current){position=fixedBefore.center,button=PointerEventData.InputButton.Left};ExecuteEvents.Execute(fixedButton.gameObject,pressEvent,ExecuteEvents.pointerDownHandler);yield return new WaitForSecondsRealtime(.15f);Check(Vector2.Distance(Map.LevelMotion(35).BodyScale,Vector2.one)>.15f,"pointer down visibly squashes and stretches jelly body");Check(Bounds(fixedRect)==fixedBefore,"large squash leaves logical hit rectangle fixed");var edgeProbe=new PointerEventData(EventSystem.current){position=fixedBefore.center+Vector2.right*(Screen.width/1080f*145)};var edgeHits=new List<RaycastResult>();EventSystem.current.RaycastAll(edgeProbe,edgeHits);Check(edgeHits.Count>0&&ExecuteEvents.GetEventHandler<IPointerClickHandler>(edgeHits[0].gameObject)==fixedButton.gameObject,"expanded stable touch target covers the bulging jelly edge");ExecuteEvents.Execute(fixedButton.gameObject,pressEvent,ExecuteEvents.pointerUpHandler);yield return new WaitForSecondsRealtime(.18f);yield return Capture("03-elastic-release");Map.BeginJellyDrag();Map.PullJelly(90);yield return null;Check(Mathf.Abs(Map.DragPull)>.3f&&Map.RibbonVertices<1600,"drag deforms a bounded continuous ribbon mesh");Map.EndJellyDrag();yield return new WaitForSecondsRealtime(1.25f);Check(Mathf.Abs(Map.DragPull)<.01f,"drag release springs back without permanent path offset");

  for(int i=0;i<AlphabetCampaign.Count;i++){Map.FocusLevel(i);yield return null;Check(Map.LevelButton(i)!=null&&Map.LevelButton(i).name=="Jelly level "+(i+1),"independent entry exists for level "+(i+1));Check(Map.LiveNodeCount<=20&&Map.AllocatedNodeCount<=24&&Map.LiveSegmentCount<=10,"virtualized objects bounded at level "+(i+1));bool clear=Map.AdjacentOverlapCount()==0;Check(clear,"all visible adjacent graphics and branches are separated at level "+(i+1));for(int pose=0;pose<4;pose++){Map.AuditMotion(i*.37f+pose*.19f,pose==0?1:0,pose*.07f);yield return null;Check(Map.AdjacentOverlapCount()==0,"exaggerated animation envelope stays clear level="+(i+1)+" pose="+pose);Check(Map.RibbonVertices<1600,"animated route vertex count bounded at level "+(i+1));}Map.EndAudit();}
  Map.Recenter();yield return new WaitForSecondsRealtime(.9f);var audition=Map.LevelMotion(35);Map.BeginJellyDrag();Map.PullJelly(90);Map.SendMessage("OnApplicationFocus",false);yield return null;Check(!Map.MotionAllowed&&!audition.TweenAlive&&Map.DragPull==0,"focus loss cancels held tweens and route pull");Map.SendMessage("OnApplicationFocus",true);Map.SendMessage("OnApplicationPause",true);yield return null;Check(!Map.MotionAllowed&&!audition.TweenAlive,"background pause keeps menu animation stopped");Map.SendMessage("OnApplicationPause",false);yield return new WaitForSecondsRealtime(.9f);Check(Map.MotionAllowed,"resume restores visible menu motion");Directory.CreateDirectory(dir+"/motion-frames");var demoEvent=new PointerEventData(EventSystem.current){position=Bounds((RectTransform)Map.LevelButton(35).transform).center,button=PointerEventData.InputButton.Left};var durations=new List<float>();float lastFrame=Time.realtimeSinceStartup;for(int frame=0;frame<60;frame++){if(frame==0)ExecuteEvents.Execute(Map.LevelButton(35).gameObject,demoEvent,ExecuteEvents.pointerDownHandler);if(frame==10)ExecuteEvents.Execute(Map.LevelButton(35).gameObject,demoEvent,ExecuteEvents.pointerUpHandler);yield return Capture("motion-frames/frame-"+frame.ToString("D3"));float now=Time.realtimeSinceStartup;durations.Add(Mathf.Clamp(now-lastFrame,.015f,.2f));lastFrame=now;yield return new WaitForSecondsRealtime(.025f);}File.WriteAllLines(dir+"/motion-frames/durations.txt",durations.Select(t=>t.ToString("F6",System.Globalization.CultureInfo.InvariantCulture)));
  Map.FocusLevel(2);yield return null;yield return Capture("levels1-8-spacing");Map.FocusLevel(31);yield return null;yield return Capture("levels29-36-spacing");for(int i=77;i>=0;i--){Map.FocusLevel(i);yield return null;bool clear=Map.AdjacentOverlapCount()==0;Check(clear,"reverse-scroll pooled graphics remain separated at level "+(i+1));}
  Map.FocusLevel(0);yield return null;float before=Map.Scroll.content.anchoredPosition.y;var eventData=new PointerEventData(EventSystem.current){position=Bounds(Map.Viewport).center,pressPosition=Bounds(Map.Viewport).center,button=PointerEventData.InputButton.Left};Map.Scroll.OnBeginDrag(eventData);eventData.position+=Vector2.down*230;Map.Scroll.OnDrag(eventData);Map.Scroll.OnEndDrag(eventData);yield return null;Check(Mathf.Abs(Map.Scroll.content.anchoredPosition.y-before)>50,"production ScrollRect accepts pointer drag");Check(Click(Map.LocateButton),"real raycast clicks locate button");yield return null;Check(Map.LevelButton(35)!=null&&Bounds((RectTransform)Map.LevelButton(35).transform).Overlaps(Bounds(Map.Viewport)),"locate restores visible current entrance");
  foreach(var size in new[]{new Vector2Int(320,568),new Vector2Int(390,844),new Vector2Int(540,960),new Vector2Int(540,1170)}){Screen.SetResolution(size.x,size.y,false);yield return new WaitForSecondsRealtime(.3f);app.QASetViewport(size);Map.Recenter();yield return null;var view=Bounds(Map.Viewport);var head=Bounds(Map.Header);var foot=Bounds(Map.Footer);Check(view.yMax<=head.yMin+2&&view.yMin>=foot.yMax-2,"map clipped between fixed bars "+size);Check(Bounds((RectTransform)Map.PlayButton.transform).yMin>=-1&&Bounds((RectTransform)Map.PlayButton.transform).xMax<=size.x+1,"CTA remains onscreen "+size);Check(Click(Map.LocateButton),"location hit target works "+size);yield return Capture("layout-"+size.x+"x"+size.y);}
  Screen.SetResolution(390,844,false);yield return new WaitForSecondsRealtime(.3f);app.QASetViewport(new Vector2Int(390,844));app.QASetSafeArea(new Rect(0,30,390,770));WeChatLayoutMetrics.Set(390,844,new Rect(0,44,390,770),new Rect(286,48,96,32));yield return null;yield return null;Rect blocked;WeChatLayoutMetrics.TryGetBlockedRect(new Vector2Int(390,844),out blocked);Check(!Bounds(Map.Header).Overlaps(blocked),"full header and wallet avoid native WeChat capsule");yield return Capture("wechat-safe-area");WeChatLayoutMetrics.Reset();app.QASetSafeArea(null);yield return null;
  Map.FocusLevel(33);yield return null;int highest=app.Profile.AlphabetUnlocked;Check(Click(Map.LevelButton(33)),"real raycast dispatch replays completed level34");yield return Battle();Check(app.Session.LevelIndex==33&&app.Profile.AlphabetUnlocked==highest,"selected replay enters correct production level without progress change");yield return Capture("03-replay-battle");app.QANavigate("home");yield return null;
  // Win by clearing actual objectives through the real damage/outcome path.
  app.Profile.ResetForTesting();app.Profile.CompleteFirstBounce();app.QAOpenChapters();yield return null;Check(app.StartTreeLevel(0),"new current map entrance starts progression run");yield return Battle();var game=app.Session;for(int pass=0;pass<6;pass++)foreach(var brick in game.CollectionBricks.Where(b=>b.Alive&&(b.Definition.juiceGroup>0||game.IsJellyTarget(b))).ToArray())game.ApplyChainImpact(brick,int.MaxValue,Vector2.up,brick.transform.position,false);yield return new WaitForSecondsRealtime(4);Check(app.RewardClaimed&&app.Profile.AlphabetUnlocked==1&&app.Profile.BestScore(0)==game.Score,"actual target win saves score and unlocks exactly next map node");yield return new WaitForSecondsRealtime(2);var back=app.GetComponentsInChildren<Button>().FirstOrDefault(b=>b.GetComponentsInChildren<TMP_Text>().Any(t=>t.text=="返回关卡"));Check(back!=null,"tree-launched win exposes return to map action");if(back!=null)back.onClick.Invoke();yield return null;yield return null;Check(app.CurrentPage=="chapters"&&Map!=null&&Map.FocusIndex==1&&Map.LevelButton(0).interactable&&Map.LevelButton(1).interactable,"win return rebuilds completed replay node and next current entrance");yield return Capture("04-win-return");
  foreach(int boundary in new[]{26}){app.Profile.ResetForTesting();for(int i=0;i<boundary;i++)app.Profile.CommitAlphabetWin("tree-boundary-"+boundary+"-"+i,i,100);app.QANavigate("home");yield return null;Check(!app.ChapterOpeningActive&&app.Profile.HomeLevelIndex==boundary,"home does not show chapter gate at "+boundary);yield return OpenMap();Check(app.StartTreeLevel(boundary),"map crosses former chapter boundary "+boundary);yield return Battle();Check(app.Session.LevelIndex==boundary,"production battle loads level "+(boundary+1));app.QANavigate("home");yield return null;}
  app.Profile.ResetForTesting();for(int i=0;i<AlphabetCampaign.Count;i++)app.Profile.CommitAlphabetWin("tree-all-"+i,i,65000);app.QANavigate("home");yield return null;yield return OpenMap();Map.Recenter();yield return null;Check(Map.FocusIndex==AlphabetCampaign.Count-1&&Map.LevelButton(AlphabetCampaign.Count-1).interactable&&Map.PlayButton.GetComponentInChildren<TMP_Text>().text=="重玩第"+AlphabetCampaign.Count+"关","all-clear state remains replayable without phantom level79");Check(!app.StartTreeLevel(AlphabetCampaign.Count),"campaign end cannot launch nonexistent level79");yield return Capture("05-all-clear");Check(errors==0,"no runtime errors or exceptions");File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+",\"fps\":"+fps+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
string dir;
```
```csharp
int checks,failures,errors,fps=60;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
