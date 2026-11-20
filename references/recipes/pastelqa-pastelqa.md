# PastelQA · PastelQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/PastelQA.cs`
文件 SHA-256：`f2e261f204782414cfd9b019b56c36eeb17a12206022ec0578637b14dce49029`
[原始源码快照](../source/Assets/Scripts/PastelQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L9

方法 SHA-256：`517f2f148107ef2a1fc7195d3ead1612531e0b2b6d54bd3800f097baf3d5b6f9`
时间来源：Time.unscaledTime, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.PastelQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);Application.logMessageReceived+=Log;yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();
 int vi=Array.IndexOf(args,"--qa-viewport");if(vi>=0){var s=args[vi+1].Split('x');outerW=int.Parse(s[0]);outerH=int.Parse(s[1]);bool fullViewport=Array.IndexOf(args,"--mobile-layout")>=0||Array.IndexOf(args,"--juice-qa")>=0;int w=fullViewport?outerW:Mathf.Min(outerW,Mathf.RoundToInt(outerH*1080f/2340f)),h=fullViewport?outerH:Mathf.Min(outerH,Mathf.RoundToInt(outerW*2340f/1080f));target=new RenderTexture(w,h,24);target.Create();padX=(outerW-w)/2;padY=(outerH-h)/2;Camera.main.targetTexture=target;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=Camera.main;canvas.planeDistance=1;app.QASetViewport(new Vector2Int(w,h));yield return null;Canvas.ForceUpdateCanvases();app.QASetViewport(new Vector2Int(w,h));}
 File.WriteAllText(Path.Combine(dir,"mode.txt"),"Unity "+Application.unityVersion+" native="+Screen.width+"x"+Screen.height+"; "+(target==null?"native window; GPU camera render capture (hidden-window framebuffer unavailable)":"GPU diagnostic inner="+target.width+"x"+target.height+" outer="+outerW+"x"+outerH+"; NOT a phone test")+"; memory-only profile --qa --qa-manual.\n");
 layout=new StreamWriter(Path.Combine(dir,"layout.csv"));layout.WriteLine("screen,kind,name,xMin,yMin,xMax,yMax,width,height,textHeight");Check(PastelArt.Count==111,"111 imported pastel sprites");Check(GameTypography.Font.name=="Lilita One Dynamic SDF","existing Lilita One TMP font retained");
 if(Array.IndexOf(args,"--juice-qa")>=0){
 app.QANavigate("home");yield return new WaitForSecondsRealtime(.4f);
 var play=GameObject.Find("Play current level").GetComponent<Button>();var feel=play.GetComponent<UIBounce>();var visual=feel.Target;
 var hit=play.GetComponent<RectTransform>();var hitPosition=hit.anchoredPosition;var rest=visual.anchoredPosition;var size=hit.sizeDelta;
 var e=new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current){pointerId=-1,button=UnityEngine.EventSystems.PointerEventData.InputButton.Left,eligibleForClick=true};
 var camera=FindFirstObjectByType<Canvas>().worldCamera;
 e.position=RectTransformUtility.WorldToScreenPoint(camera,hit.position);
 feel.OnPointerEnter(e);yield return new WaitForSecondsRealtime(.25f);Check(visual.localScale.x>1.02f,"hover visibly inflates button");yield return Capture("button-hover");
 feel.OnPointerDown(e);yield return new WaitForSecondsRealtime(.15f);Check(visual.localScale.y<.94f,"pressed face squashes vertically");yield return Capture("button-pressed");
 feel.OnBeginDrag(e);e.position+=new Vector2(80,25);feel.OnDrag(e);yield return null;
 Check(!e.eligibleForClick,"drag cancels click eligibility");Check(visual.anchoredPosition.x>rest.x+1,"drag stretches face toward pointer");Check(hit.anchoredPosition==hitPosition&&hit.sizeDelta==size,"drag preserves fixed hit rectangle");
 yield return Capture("button-drag");feel.OnPointerExit(e);e.position+=new Vector2(10000,10000);feel.OnDrag(e);yield return null;
 Check((visual.anchoredPosition-rest).magnitude<20&&visual.localScale.x<1.15f,"large drag deformation remains bounded outside hit area");
 feel.OnPointerUp(e);yield return new WaitForSecondsRealtime(.8f);Check(Vector3.Distance(visual.localScale,Vector3.one)<.001f&&Vector2.Distance(visual.anchoredPosition,rest)<.001f,"release outside fully restores rest pose");Check(!app.Session.IsPlaying,"drag did not start gameplay");yield return Capture("button-rest");
 play.interactable=false;feel.OnPointerEnter(e);feel.OnPointerDown(e);yield return new WaitForSecondsRealtime(.2f);Check(visual.localScale==Vector3.one,"disabled button ignores hover and press");play.interactable=true;
 feel.OnPointerEnter(e);yield return new WaitForSecondsRealtime(.2f);play.gameObject.SetActive(false);Check(visual.localScale==Vector3.one,"disable clears animated state");play.gameObject.SetActive(true);
 var wordmark=GameObject.Find("Header wordmark").GetComponent<RectTransform>();var first=(RectTransform)wordmark.GetChild(0);var firstRest=first.anchoredPosition;bool shivered=false;
 for(int i=0;i<36;i++){shivered|=Vector2.Distance(first.anchoredPosition,firstRest)>.2f;yield return Capture("juice-frame-"+i.ToString("D3"));yield return new WaitForSecondsRealtime(1f/30f);}
 // Sample a whole loop regardless of the initial phase.
 float until=Time.unscaledTime+6;while(Time.unscaledTime<until){shivered|=Vector2.Distance(first.anchoredPosition,firstRest)>.2f;yield return null;}Check(shivered,"homepage letter shivers during looping animation");
 Check(feel.IdleInterval==30,"play reminder configured to 30 seconds");
 // Restart idle time through the same pointer interaction used by players.
 feel.OnPointerEnter(e);feel.OnPointerExit(e);int pulses=feel.IdlePulseCount;
 yield return new WaitForSecondsRealtime(29);Check(feel.IdlePulseCount==pulses,"no reminder before 30 seconds");
 bool reminderMoved=false;until=Time.unscaledTime+2;while(Time.unscaledTime<until){reminderMoved|=Mathf.Abs(visual.localScale.x-1)>.003f;yield return null;}
 Check(feel.IdlePulseCount==pulses+1&&reminderMoved,"one visible reminder after 30 idle seconds");
 Call("Settings");yield return new WaitForSecondsRealtime(.3f);Check(!app.HomeMotionAllowed,"modal suspends homepage ambience");Call("CloseModal");
 var carousel=FindFirstObjectByType<ActivityCarousel>();carousel.QADrag(new Vector2(-280,0));yield return new WaitForSecondsRealtime(.3f);Check(carousel.SelectedPage==1,"activity horizontal drag retained");
 app.QANavigateAnimated("shop");yield return new WaitForSecondsRealtime(.4f);Check(!app.HomeMotionAllowed,"hidden homepage suspends ambience");app.QANavigateAnimated("home");yield return new WaitForSecondsRealtime(.5f);
 GameObject.Find("Play current level").GetComponent<Button>().onClick.Invoke();yield return new WaitForSecondsRealtime(.9f);Check(app.Session.IsPlaying,"normal play action still starts level");
 layout.Dispose();Check(errors==0,"zero runtime errors/exceptions");File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failed+" errors="+errors+"\n");Application.Quit(failed==0&&errors==0?0:3);yield break;
 }
 if(Array.IndexOf(args,"--mobile-layout")>=0){
 // Render the complete requested phone viewport. No padded or cropped portrait image.
 var scenarios=new[]{new Rect(0,0,outerW,outerH),new Rect(0,outerH*.026f,outerW,outerH*.936f),new Rect(outerW*.025f,outerH*.035f,outerW*.96f,outerH*.91f)};
 for(int scenario=0;scenario<scenarios.Length;scenario++){
  Rect safe=scenarios[scenario];app.QASetSafeArea(safe);
  foreach(string page in new[]{"home","ranking","shop"}){
   app.QANavigateAnimated(page);yield return new WaitForSecondsRealtime(1.1f);Canvas.ForceUpdateCanvases();
   string name=page+"-safe-"+scenario;yield return Capture(name);
   Check(Camera.main.rect==new Rect(0,0,1,1),"menu camera covers whole player viewport "+name);
   var backdrop=GameObject.Find("Full screen cream backdrop").GetComponent<RectTransform>();Rect bg=ScreenBounds(backdrop);
   Check(bg.xMin<=1&&bg.yMin<=1&&bg.xMax>=outerW-1&&bg.yMax>=outerH-1,"unmasked background covers full viewport "+name);
   foreach(var key in new[]{"Avatar medallion","Settings medallion","Nav button shop","Nav button home","Nav button ranking"}){
    Rect bounds=ScreenBounds(GameObject.Find(key).GetComponent<RectTransform>());Check(Contains(safe,bounds),"control respects safe area "+name+" "+key);
   }
   var nav=FindFirstObjectByType<Round6Nav>();var icon=nav.transform.Find("Pastel fixed navigation/Nav button "+page+"/Press visual/Pop icon").GetComponent<RectTransform>();
   Check(Contains(safe,ScreenBounds(icon)),"raised selected icon respects safe area "+name);
   var caption=icon.parent.Find("Selected label strip "+page);Check(caption.GetComponent<CanvasGroup>().alpha>.99f,"selected caption remains visible "+name);
   Rect navHit=ScreenBounds(GameObject.Find("Nav button home").GetComponent<RectTransform>());
   Check(Mathf.Abs(navHit.yMin-(safe.yMin+25*safe.width/1080f))<2,"navigation follows safe bottom "+name);
   if(page=="home"){
    Rect play=ScreenBounds(GameObject.Find("Play current level").GetComponent<RectTransform>());Check(Contains(safe,play),"play button inside safe area "+name);
    Check(ScreenBounds(icon).yMax<=play.yMin+2,"popped navigation does not overlap play button "+name);
    var carousel=FindFirstObjectByType<ActivityCarousel>();carousel.QADrag(new Vector2(-280,0));yield return new WaitForSecondsRealtime(.3f);Check(carousel.SelectedPage==1,"carousel remains swipeable "+name);carousel.QADrag(new Vector2(280,0));
   }
   Camera.main.Render();var previous=RenderTexture.active;RenderTexture.active=target;var pixels=new Texture2D(outerW,outerH,TextureFormat.RGB24,false);pixels.ReadPixels(new Rect(0,0,outerW,outerH),0,0);pixels.Apply();RenderTexture.active=previous;
   foreach(var point in new[]{new Vector2Int(0,0),new Vector2Int(outerW-1,0),new Vector2Int(0,outerH-1),new Vector2Int(outerW-1,outerH-1),new Vector2Int(0,outerH/2),new Vector2Int(outerW-1,outerH/2)}){Color32 color=pixels.GetPixel(point.x,point.y);Check(Mathf.Abs(color.r-248)<=3&&Mathf.Abs(color.g-242)<=3&&Mathf.Abs(color.b-233)<=3,"GPU background edge is cream "+name+" "+point);}
   Destroy(pixels);
  }
 }
 // Resize in place, as when Android changes the system-bar inset after launch.
 app.QASetSafeArea(scenarios[0]);app.QANavigate("home");yield return null;app.QASetSafeArea(scenarios[1]);yield return null;yield return Capture("home-after-inset-change");
 Call("Settings");yield return new WaitForSecondsRealtime(.3f);yield return Capture("settings-safe");Call("CloseModal");
 app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.9f);Check(app.Session.IsPlaying,"level starts after responsive menu");Check(Camera.main.rect.width<=1&&Camera.main.rect.height<=1,"battle retains fitted board viewport");
 app.QANavigate("ranking");yield return new WaitForSecondsRealtime(1);Check(Camera.main.rect==new Rect(0,0,1,1),"return from battle restores full menu camera");yield return Capture("ranking-after-battle");
 app.ShowIntro(false);yield return new WaitForSecondsRealtime(.5f);yield return Capture("intro-safe");
 layout.Dispose();Check(errors==0,"zero runtime errors/exceptions");File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failed+" errors="+errors+"\n");Application.Quit(failed==0&&errors==0?0:3);yield break;
 }
 if(Array.IndexOf(args,"--nav-jelly")>=0){
 typeof(LocalProfile).GetProperty("UnlockedLevel").SetValue(app.Profile,1);Call("ClearPages");app.QANavigate("home");yield return new WaitForSecondsRealtime(1);yield return Capture("page-home");yield return Capture("difficulty-2");
 var times=new List<string>();float started=Time.unscaledTime;int frame=0;
 foreach(var page in new[]{"shop","home","ranking","home"}){app.QANavigateAnimated(page);var nav=FindFirstObjectByType<Round6Nav>();var item=nav.transform.Find("Pastel fixed navigation/Nav button "+page+"/Press visual");var icon=item.Find("Pop icon").GetComponent<RectTransform>();var caption=item.Find("Selected label strip "+page).GetComponent<CanvasGroup>();Check(caption.alpha<.01f,"caption initially hidden while icon begins pop: "+page);bool squashed=false,stretched=false,aboveRail=false;
 for(int f=0;f<27;f++){times.Add((Time.unscaledTime-started).ToString("F6",CultureInfo.InvariantCulture));squashed|=icon.localScale.y<.9f;stretched|=icon.localScale.y>1.28f;aboveRail|=icon.anchoredPosition.y>70;if(target==null||target.width<=540)yield return Capture("nav-frame-"+(frame++).ToString("D3"));else{frame++;yield return null;}yield return new WaitForSecondsRealtime(1f/30f);}
 Check(squashed&&stretched&&aboveRail,"icon squashes, stretches and rises above rail: "+page);Check(Mathf.Abs(icon.anchoredPosition.y-Round6Nav.RaisedY)<.01f,"selected icon settles at raised position: "+page);Check(Mathf.Abs(caption.alpha-1)<.01f,"selected caption visible after pop: "+page);Check(caption.GetComponent<RectTransform>().anchoredPosition.y<0,"caption sits in lower strip: "+page);
 int visible=0;foreach(var group in nav.GetComponentsInChildren<CanvasGroup>())if(group.name.StartsWith("Selected label strip ")&&group.alpha>.99f)visible++;Check(visible==1,"one selected caption visible: "+page);
 foreach(var key in new[]{"shop","home","ranking"})if(key!=page){var other=nav.transform.Find("Pastel fixed navigation/Nav button "+key+"/Press visual/Pop icon").GetComponent<RectTransform>();Check(other.anchoredPosition.sqrMagnitude<.01f,"unselected icon returns to rail: "+key);}
 yield return Capture("page-"+page);yield return Capture("nav-2-"+page);
 }
 File.WriteAllLines(Path.Combine(dir,"nav-times.txt"),times);
 for(int n=0;n<12;n++){app.QANavigateAnimated(n%2==0?"ranking":"shop");yield return new WaitForSecondsRealtime(.05f);}app.QANavigateAnimated("home");yield return new WaitForSecondsRealtime(1);Check(!app.NavigationActive&&app.CurrentPage=="home","rapid reversals settle on home");yield return Capture("rapid-navigation");
 foreach(int level in new[]{0,2,5}){typeof(LocalProfile).GetProperty("UnlockedLevel").SetValue(app.Profile,level);Call("ClearPages");app.QANavigate("home");yield return new WaitForSecondsRealtime(1);yield return Capture("difficulty-"+(level+1));if(level==2)foreach(var page in new[]{"home","shop","ranking"}){app.QANavigateAnimated(page);yield return new WaitForSecondsRealtime(1);yield return Capture("nav-3-"+page);}}
 FeedbackProfile.Active.enabledFeedback=false;app.QANavigate("shop");yield return null;var plain=FindFirstObjectByType<Round6Nav>().transform.Find("Pastel fixed navigation/Nav button shop/Press visual/Pop icon").GetComponent<RectTransform>();Check(Mathf.Abs(plain.anchoredPosition.y-Round6Nav.RaisedY)<.01f,"feedback disabled uses final pose without animation");FeedbackProfile.Active.enabledFeedback=true;
 typeof(LocalProfile).GetProperty("UnlockedLevel").SetValue(app.Profile,0);Call("ClearPages");app.QANavigate("home");yield return new WaitForSecondsRealtime(1);yield return Capture("page-home");
 var carousel=FindFirstObjectByType<ActivityCarousel>();carousel.QADrag(new Vector2(-280,0));yield return new WaitForSecondsRealtime(.3f);yield return Capture("activity-second-page");
 GameObject.Find("Settings medallion").GetComponent<Button>().onClick.Invoke();yield return new WaitForSecondsRealtime(.3f);yield return Capture("modal-settings");Call("CloseModal");GameObject.Find("Play current level").GetComponent<Button>().onClick.Invoke();yield return new WaitForSecondsRealtime(.9f);Check(app.Session.IsPlaying,"play and settings routes retained");yield return Capture("battle-0");
 layout.Dispose();Check(errors==0,"zero runtime errors/exceptions");File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failed+" errors="+errors+"\n");Application.Quit(failed==0&&errors==0?0:3);yield break;
 }
 if(Array.IndexOf(args,"--home-v7")>=0){
 app.QANavigate("home");yield return new WaitForSecondsRealtime(.3f);
 Check(GameObject.Find("Header wordmark")!=null&&GameObject.Find("Header wordmark").transform.childCount==12,"homepage uses the existing twelve-letter wordmark");
 Check(GameObject.Find("Avatar medallion/Cat cosmetic portrait/Frame")!=null&&GameObject.Find("Avatar medallion/Cat cosmetic portrait/Avatar")!=null,"garden portrait and frame are independently layered");
 foreach(int level in new[]{0,1,2,5}){typeof(LocalProfile).GetProperty("UnlockedLevel").SetValue(app.Profile,level);Call("ClearPages");app.QANavigate("home");yield return new WaitForSecondsRealtime(.3f);var progress=GameObject.Find("Level progress");var labels=progress.GetComponentsInChildren<TMP_Text>();int expected=Mathf.Min(5,LevelCatalog.Count-level);Check(labels.Length==expected,"only available forward levels are shown stage="+(level+1));var leftmost=labels[0];foreach(var label in labels)if(label.rectTransform.anchoredPosition.x<leftmost.rectTransform.anchoredPosition.x)leftmost=label;Check(leftmost.text==(level+1).ToString()&&Mathf.Abs(leftmost.rectTransform.anchoredPosition.x+332)<.01f,"current level fixed at leftmost slot stage="+(level+1));for(int i=0;i<labels.Length;i++)Check(labels[i].text==(level+1+i).ToString(),"forward level sequence stage="+(level+1)+" slot="+i);yield return Capture("difficulty-"+(level+1));
 string expectedSprite=level==0?"button_mint":level==1?"button_yellow":level==2?"button_lavender":"button_blush";
 foreach(var page in new[]{"home","shop","ranking"}){app.QANavigateAnimated(page);yield return new WaitForSecondsRealtime(.4f);var nav=FindFirstObjectByType<Round6Nav>();int active=0;foreach(var image in nav.GetComponentsInChildren<Image>())if(image.enabled&&image.name.StartsWith("Selection ")){active++;Check(image.sprite.name==expectedSprite,"nav selection matches difficulty stage="+(level+1)+" page="+page);}Check(active==1,"one selected nav tile "+page);foreach(var rect in nav.GetComponentsInChildren<RectTransform>())if(rect.name=="Centred icon")Check(rect.anchoredPosition.sqrMagnitude<.001f,"nav icon mount stays at tile centre "+page);if(level==1||level==2)yield return Capture("nav-"+(level+1)+"-"+page);}
 }
 typeof(LocalProfile).GetProperty("UnlockedLevel").SetValue(app.Profile,0);Call("ClearPages");app.QANavigate("home");yield return new WaitForSecondsRealtime(.3f);yield return Capture("page-home");
 foreach(var page in new[]{"shop","ranking","home"}){app.QANavigateAnimated(page);yield return new WaitForSecondsRealtime(.4f);yield return Capture("page-"+page);}
 var carousel=FindFirstObjectByType<ActivityCarousel>();carousel.QADrag(new Vector2(-280,0));yield return new WaitForSecondsRealtime(.3f);Check(carousel.SelectedPage==1,"activity drag preserved");yield return Capture("activity-second-page");
 GameObject.Find("Settings medallion").GetComponent<Button>().onClick.Invoke();yield return new WaitForSecondsRealtime(.3f);yield return Capture("modal-settings");Call("CloseModal");GameObject.Find("Play current level").GetComponent<Button>().onClick.Invoke();yield return new WaitForSecondsRealtime(.9f);Check(app.Session.IsPlaying,"play button pointer action starts level");yield return Capture("battle-0");
 layout.Dispose();Check(errors==0,"zero runtime errors/exceptions");File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failed+" errors="+errors+"\n");Application.Quit(failed==0&&errors==0?0:3);yield break;
 }
 if(Array.IndexOf(args,"--final-home")>=0){
 app.ShowIntro(false);while(FindFirstObjectByType<JellyLogoIntro>()==null)yield return null;var times=new List<string>();float started=Time.unscaledTime;bool earlyGlyphVisible=false;bool lateJelly=false,phaseOverlap=false,sustainedObserved=false;
 for(int f=0;f<150;f++){float age=Time.unscaledTime-started;times.Add(age.ToString("F6",CultureInfo.InvariantCulture));var glyphs=FindObjectsByType<JellyGlyph>(FindObjectsSortMode.None);foreach(var glyph in glyphs)if(!glyph.DropStarted&&glyph.GetComponent<CanvasGroup>().alpha>.001f)earlyGlyphVisible=true;bool anyFalling=Array.Exists(glyphs,g=>!g.DropComplete),anyJelly=Array.Exists(glyphs,g=>g.InSustainedPhase);phaseOverlap|=anyFalling&&anyJelly;sustainedObserved|=anyJelly;if(age>2.7f&&age<4.5f)foreach(var glyph in glyphs)if(Mathf.Abs(glyph.transform.localScale.x-1)>.006f)lateJelly=true;yield return Capture("intro-frame-"+f.ToString("D3"));yield return new WaitForSecondsRealtime(1f/30f);}
 Check(!earlyGlyphVisible,"all letters remain invisible until their own drop starts");Check(UnityEngine.Rendering.SplashScreen.isFinished,"custom animation runs after Unity splash is finished");Check(!phaseOverlap&&sustainedObserved,"all original letter drops complete before sustained jelly phase starts");Check(lateJelly,"logo continues damped jelly motion after initial impact");File.WriteAllLines(Path.Combine(dir,"intro-times.txt"),times);yield return Capture("page-intro");
 app.QANavigate("home");yield return new WaitForSecondsRealtime(.35f);yield return Capture("page-home");
 Check(GameObject.Find("Avatar medallion/Cat cosmetic portrait/Frame")!=null&&GameObject.Find("Avatar medallion/Cat cosmetic portrait/Avatar")!=null,"avatar and frame are separate masked layers");
 Check(GameObject.Find("Settings medallion/ButtonVisual/button_circle_cream")!=null&&GameObject.Find("Settings medallion/ButtonVisual/icon_settings")!=null,"settings base and symbol are separate layers");
 foreach(int level in new[]{0,1,2,5}){typeof(LocalProfile).GetProperty("UnlockedLevel").SetValue(app.Profile,level);Call("ClearPages");app.QANavigate("home");yield return new WaitForSecondsRealtime(.1f);var progress=GameObject.Find("Level progress");Check(progress.GetComponentsInChildren<TMP_Text>().Length==Mathf.Min(5,LevelCatalog.Count-level),"progress contains only five numeric labels level="+level);var play=GameObject.Find("Play current level");Check(play.GetComponentInChildren<TMP_Text>().text=="Level "+(level+1),"play label agrees with progress index level="+level);Check(play.GetComponent<RectTransform>().anchoredPosition.x==progress.GetComponent<RectTransform>().anchoredPosition.x,"play and progress share centre line");yield return Capture("difficulty-"+(level+1));}
 typeof(LocalProfile).GetProperty("UnlockedLevel").SetValue(app.Profile,0);Call("ClearPages");app.QANavigate("home");yield return new WaitForSecondsRealtime(.2f);yield return Capture("page-home");
 var carousel=FindFirstObjectByType<ActivityCarousel>();carousel.QADrag(new Vector2(-270,0));yield return new WaitForSecondsRealtime(.3f);Check(carousel.SelectedPage==1,"activity swipe retained");yield return Capture("activity-second-page");carousel.QADrag(new Vector2(270,0));yield return new WaitForSecondsRealtime(.3f);
 foreach(var page in new[]{"shop","ranking","home"}){app.QANavigateAnimated(page);yield return new WaitForSecondsRealtime(.4f);var nav=FindFirstObjectByType<Round6Nav>();var labels=nav.GetComponentsInChildren<TMP_Text>();Check(labels.Length==1,"only selected nav label visible "+page);foreach(var label in labels){var rect=label.rectTransform;Check(rect.anchoredPosition.y-rect.rect.height*.5f>=-115,"nav label within rail lower edge "+page);}yield return Capture("page-"+page);}
 app.ShowIntro();yield return new WaitForSecondsRealtime(JellyLogoIntro.HoldDuration+JellyLogoIntro.FadeDuration+.2f);Check(GameObject.Find("Logo animation page")==null,"startup animation dismisses after shared hold and fade duration");
 Call("Settings");yield return new WaitForSecondsRealtime(.3f);yield return Capture("modal-settings");Call("CloseModal");app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.9f);Check(app.Session.IsPlaying,"large play button level route remains valid");yield return Capture("battle-0");
 layout.Dispose();Check(errors==0,"zero runtime errors/exceptions");File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failed+" errors="+errors+"\n");Application.Quit(failed==0&&errors==0?0:3);yield break;
 }
 if(Array.IndexOf(args,"--activity-qa")>=0){
 app.QANavigate("home");yield return new WaitForSecondsRealtime(.35f);var carousel=FindFirstObjectByType<ActivityCarousel>();Check(carousel!=null,"home activity carousel exists");
 Check(GameObject.Find("Wallet coin")==null,"top wallet removed");Check(GameObject.Find("pastel/icon_heart")==null,"top energy removed");
 yield return Capture("page-home");float first=carousel.Offset;yield return new WaitForSecondsRealtime(1.2f);Check(Mathf.Abs(carousel.Offset-first)<.01f,"idle does not autoplay");
 carousel.QADrag(new Vector2(-270,4));yield return new WaitForSecondsRealtime(.3f);Check(carousel.SelectedPage==1&&Mathf.Abs(carousel.Offset-384)<.01f,"left swipe snaps to second page");yield return Capture("activity-second-page");
 carousel.QADrag(new Vector2(-1800,0));yield return new WaitForSecondsRealtime(.3f);Check(Mathf.Abs(carousel.Offset-384)<.01f,"last card boundary clamps");
 carousel.QADrag(new Vector2(4,180));yield return new WaitForSecondsRealtime(.3f);Check(Mathf.Abs(carousel.Offset-384)<.01f,"vertical gesture does not change carousel");
 carousel.QADrag(new Vector2(280,0));yield return new WaitForSecondsRealtime(.3f);Check(carousel.SelectedPage==0&&carousel.Offset<.01f,"right swipe snaps to first page");
 carousel.QADrag(new Vector2(1800,0));yield return new WaitForSecondsRealtime(.3f);Check(carousel.Offset<.01f,"first card boundary clamps");
 carousel.QADrag(new Vector2(-35,0));yield return new WaitForSecondsRealtime(.3f);Check(carousel.Offset<.01f,"short drag settles back");
 foreach(var page in new[]{"shop","ranking","home"}){app.QANavigateAnimated(page);yield return new WaitForSecondsRealtime(.4f);yield return Capture("page-"+page);}
 carousel=FindFirstObjectByType<ActivityCarousel>();Check(carousel.Offset<.01f,"return home preserves settled carousel");
 Call("Settings");yield return new WaitForSecondsRealtime(.3f);yield return Capture("modal-settings");Call("CloseModal");
 app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.9f);Check(app.Session.IsPlaying,"level start remains functional");yield return Capture("battle-0");
 layout.Dispose();Check(errors==0,"zero runtime errors/exceptions");File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failed+" errors="+errors+"\n");Application.Quit(failed==0&&errors==0?0:3);yield break;
 }
 if(Array.IndexOf(args,"--home-polish")>=0){
 app.ShowIntro(false);var times=new List<string>();float started=Time.unscaledTime;
 for(int f=0;f<100;f++){times.Add((Time.unscaledTime-started).ToString("F6",CultureInfo.InvariantCulture));yield return Capture("intro-frame-"+f.ToString("D3"));yield return new WaitForSecondsRealtime(1f/30f);}
 File.WriteAllLines(Path.Combine(dir,"intro-times.txt"),times);yield return Capture("page-intro");
 Check(GameObject.Find("Logo animation page")!=null,"dedicated logo animation page exists");
 app.QANavigate("home");yield return new WaitForSecondsRealtime(.35f);yield return Capture("page-home");
 Check(GameObject.Find("Hero group")==null,"home decorative group removed");
 Check(GameObject.Find("home/Pastel layered logo")==null,"home logo moved to intro");
 Check(GameObject.Find("Button VIP")==null&&GameObject.Find("Button 6 stages")==null,"VIP and stage shortcuts removed");
 Check(GameObject.Find("Level progress")!=null,"profile-linked level progress exists");
 foreach(var page in new[]{"shop","ranking","home"}){app.QANavigateAnimated(page);yield return new WaitForSecondsRealtime(.4f);var nav=FindFirstObjectByType<Round6Nav>();int visible=nav.GetComponentsInChildren<TMP_Text>().Length;Check(visible==1,"only selected navigation label visible: "+page);yield return Capture("page-"+page);}
 app.QASetWalletText("99999");yield return Capture("wallet-99999");
 app.QANavigate("home");app.ShowIntro();yield return new WaitForSecondsRealtime(.2f);app.QANavigate("shop");yield return new WaitForSecondsRealtime(5);Check(app.CurrentPage=="shop"&&GameObject.Find("Logo animation page")==null,"cancelled intro does not interfere with navigation");
 app.QANavigate("home");Call("Settings");yield return new WaitForSecondsRealtime(.3f);yield return Capture("modal-settings");Call("CloseModal");
 app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.9f);Check(app.Session.IsPlaying,"home starts current level");yield return Capture("battle-0");
 layout.Dispose();Check(errors==0,"zero runtime errors/exceptions");File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failed+" errors="+errors+"\n");Application.Quit(failed==0&&errors==0?0:3);yield break;
 }
 foreach(var page in new[]{"home","shop","ranking","vip"}){app.QANavigate(page);yield return new WaitForSecondsRealtime(.35f);yield return Capture("page-"+page);}
 app.QANavigate("home");Call("Settings");yield return new WaitForSecondsRealtime(.3f);yield return Capture("modal-settings");Call("CloseModal");yield return new WaitForSecondsRealtime(.2f);Call("LevelPicker");yield return new WaitForSecondsRealtime(.3f);yield return Capture("modal-stage-picker");Call("CloseModal");yield return new WaitForSecondsRealtime(.2f);Call("ContentEnd");yield return new WaitForSecondsRealtime(.3f);yield return Capture("modal-content-end");
 app.QANavigate("shop");Call("MockPurchase",1000,"1,000 coins");yield return new WaitForSecondsRealtime(.3f);yield return Capture("modal-purchase");
 app.QAStartLevel(0);yield return new WaitForSecondsRealtime(.10f);yield return Capture("loading");yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.8f);yield return Capture("tutorial");Call("ShowTutorial",0);yield return Capture("battle-0");Call("Pause");yield return new WaitForSecondsRealtime(.3f);yield return Capture("modal-pause");Call("CloseModal");yield return new WaitForSecondsRealtime(.2f);
 foreach(int level in new[]{0,1,2,3,4,5}){app.QAStartLevel(level);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.9f);Call("ShowTutorial",0);app.Session.SetPaused(true);yield return Capture("battle-"+level);Geometry(level);File.WriteAllText(Path.Combine(dir,"level-"+level+"-physics.json"),JsonUtility.ToJson(app.Session.CurrentLevel,true));}
 app.QAResult(true);yield return new WaitForSecondsRealtime(5.5f);yield return Capture("result-victory");var victory=FindFirstObjectByType<VictoryPresentation>();Check(victory!=null&&victory.Ready,"letter-based victory sequence becomes ready");
 // QA preview must not grant a reward. Only display the existing local ad dialog.
 typeof(AppUI).GetField("qaResult",BindingFlags.Instance|BindingFlags.NonPublic).SetValue(app,false);typeof(AppUI).GetField("baseRewardSaved",BindingFlags.Instance|BindingFlags.NonPublic).SetValue(app,true);Call("MockAd");yield return new WaitForSecondsRealtime(.3f);yield return Capture("modal-ad");app.QANavigate("home");app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.8f);app.QAResult(false);yield return Capture("result-defeat");
 app.QANavigate("home");foreach(string amount in new[]{"1","999","99999"}){app.QASetWalletText(amount);yield return Capture("wallet-"+amount);}
 for(int i=0;i<12;i++){app.QANavigateAnimated(i%2==0?"shop":"ranking");yield return new WaitForSecondsRealtime(.06f);}app.QANavigateAnimated("home");yield return new WaitForSecondsRealtime(.4f);Check(!app.NavigationActive&&app.CurrentPage=="home","rapid navigation settles to final request");Check(Mathf.Abs(app.NavigationPosition.x+1080)<.01f,"page slot exact after reversals");
 int vw=target!=null?target.width:Screen.width,vh=target!=null?target.height:Screen.height;app.QASetSafeArea(new Rect(0,55,vw,vh-115));yield return Capture("safe-area");app.QASetSafeArea(null);
 foreach(int level in new[]{0,2}){app.QAStartLevel(level);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.9f);var game=app.Session;var mode=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;using(var w=new StreamWriter(Path.Combine(dir,"trace-"+level+".csv"))){w.WriteLine("step,id,x,y,vx,vy,shots,pending,targets,hits,breaks");for(int step=0;step<180;step++){if(step==0)game.TryShoot(new Vector2(-.35f,.94f));game.QAAdvanceOneStep();foreach(var ball in FindObjectsByType<BallBody>(FindObjectsSortMode.None))if(ball.Active){var p=ball.Body.position;var v=ball.Body.linearVelocity;w.WriteLine(step+","+ball.Id+","+F(p.x)+","+F(p.y)+","+F(v.x)+","+F(v.y)+","+game.Shots+","+game.PendingBurstBalls+","+game.RemainingTargets+","+game.HitCount+","+game.BreakCount);}}}Physics2D.simulationMode=mode;}
 layout.Dispose();Check(errors==0,"zero runtime errors/exceptions");File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failed+" errors="+errors+"\n");Application.Quit(failed==0&&errors==0?0:3);
 }
```

### Capture · L180

方法 SHA-256：`cbc4bed1feec0980e038e1377b5b426c1135553b9b309f148adff0e4b6860c83`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.PastelQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/PastelQA.cs:20` · `PastelQA.Start` → `Capture("button-hover")`
- `Assets/Scripts/PastelQA.cs:21` · `PastelQA.Start` → `Capture("button-pressed")`
- `Assets/Scripts/PastelQA.cs:24` · `PastelQA.Start` → `Capture("button-drag")`
- `Assets/Scripts/PastelQA.cs:26` · `PastelQA.Start` → `Capture("button-rest")`
- `Assets/Scripts/PastelQA.cs:30` · `PastelQA.Start` → `Capture("juice-frame-"+i.ToString("D3"))`
- `Assets/Scripts/PastelQA.cs:52` · `PastelQA.Start` → `Capture(name)`
- `Assets/Scripts/PastelQA.cs:75` · `PastelQA.Start` → `Capture("home-after-inset-change")`
- `Assets/Scripts/PastelQA.cs:76` · `PastelQA.Start` → `Capture("settings-safe")`
- `Assets/Scripts/PastelQA.cs:78` · `PastelQA.Start` → `Capture("ranking-after-battle")`
- `Assets/Scripts/PastelQA.cs:79` · `PastelQA.Start` → `Capture("intro-safe")`
- `Assets/Scripts/PastelQA.cs:83` · `PastelQA.Start` → `Capture("page-home")`
- `Assets/Scripts/PastelQA.cs:83` · `PastelQA.Start` → `Capture("difficulty-2")`
- `Assets/Scripts/PastelQA.cs:86` · `PastelQA.Start` → `Capture("nav-frame-"+(frame++).ToString("D3"))`
- `Assets/Scripts/PastelQA.cs:90` · `PastelQA.Start` → `Capture("page-"+page)`
- `Assets/Scripts/PastelQA.cs:90` · `PastelQA.Start` → `Capture("nav-2-"+page)`
- `Assets/Scripts/PastelQA.cs:93` · `PastelQA.Start` → `Capture("rapid-navigation")`
- `Assets/Scripts/PastelQA.cs:94` · `PastelQA.Start` → `Capture("difficulty-"+(level+1))`
- `Assets/Scripts/PastelQA.cs:94` · `PastelQA.Start` → `Capture("nav-3-"+page)`
- `Assets/Scripts/PastelQA.cs:96` · `PastelQA.Start` → `Capture("page-home")`
- `Assets/Scripts/PastelQA.cs:97` · `PastelQA.Start` → `Capture("activity-second-page")`
- `Assets/Scripts/PastelQA.cs:98` · `PastelQA.Start` → `Capture("modal-settings")`
- `Assets/Scripts/PastelQA.cs:98` · `PastelQA.Start` → `Capture("battle-0")`
- `Assets/Scripts/PastelQA.cs:105` · `PastelQA.Start` → `Capture("difficulty-"+(level+1))`
- `Assets/Scripts/PastelQA.cs:107` · `PastelQA.Start` → `Capture("nav-"+(level+1)+"-"+page)`
- `Assets/Scripts/PastelQA.cs:109` · `PastelQA.Start` → `Capture("page-home")`
- `Assets/Scripts/PastelQA.cs:110` · `PastelQA.Start` → `Capture("page-"+page)`
- `Assets/Scripts/PastelQA.cs:111` · `PastelQA.Start` → `Capture("activity-second-page")`
- `Assets/Scripts/PastelQA.cs:112` · `PastelQA.Start` → `Capture("modal-settings")`
- `Assets/Scripts/PastelQA.cs:112` · `PastelQA.Start` → `Capture("battle-0")`
- `Assets/Scripts/PastelQA.cs:117` · `PastelQA.Start` → `Capture("intro-frame-"+f.ToString("D3"))`
- `Assets/Scripts/PastelQA.cs:118` · `PastelQA.Start` → `Capture("page-intro")`
- `Assets/Scripts/PastelQA.cs:119` · `PastelQA.Start` → `Capture("page-home")`
- `Assets/Scripts/PastelQA.cs:122` · `PastelQA.Start` → `Capture("difficulty-"+(level+1))`
- `Assets/Scripts/PastelQA.cs:123` · `PastelQA.Start` → `Capture("page-home")`
- `Assets/Scripts/PastelQA.cs:124` · `PastelQA.Start` → `Capture("activity-second-page")`
- `Assets/Scripts/PastelQA.cs:125` · `PastelQA.Start` → `Capture("page-"+page)`
- `Assets/Scripts/PastelQA.cs:127` · `PastelQA.Start` → `Capture("modal-settings")`
- `Assets/Scripts/PastelQA.cs:127` · `PastelQA.Start` → `Capture("battle-0")`
- `Assets/Scripts/PastelQA.cs:133` · `PastelQA.Start` → `Capture("page-home")`
- `Assets/Scripts/PastelQA.cs:134` · `PastelQA.Start` → `Capture("activity-second-page")`
- `Assets/Scripts/PastelQA.cs:140` · `PastelQA.Start` → `Capture("page-"+page)`
- `Assets/Scripts/PastelQA.cs:142` · `PastelQA.Start` → `Capture("modal-settings")`
- `Assets/Scripts/PastelQA.cs:143` · `PastelQA.Start` → `Capture("battle-0")`
- `Assets/Scripts/PastelQA.cs:148` · `PastelQA.Start` → `Capture("intro-frame-"+f.ToString("D3"))`
- `Assets/Scripts/PastelQA.cs:149` · `PastelQA.Start` → `Capture("page-intro")`
- `Assets/Scripts/PastelQA.cs:151` · `PastelQA.Start` → `Capture("page-home")`
- `Assets/Scripts/PastelQA.cs:156` · `PastelQA.Start` → `Capture("page-"+page)`
- `Assets/Scripts/PastelQA.cs:157` · `PastelQA.Start` → `Capture("wallet-99999")`
- `Assets/Scripts/PastelQA.cs:159` · `PastelQA.Start` → `Capture("modal-settings")`
- `Assets/Scripts/PastelQA.cs:160` · `PastelQA.Start` → `Capture("battle-0")`
- `Assets/Scripts/PastelQA.cs:163` · `PastelQA.Start` → `Capture("page-"+page)`
- `Assets/Scripts/PastelQA.cs:164` · `PastelQA.Start` → `Capture("modal-settings")`
- `Assets/Scripts/PastelQA.cs:164` · `PastelQA.Start` → `Capture("modal-stage-picker")`
- `Assets/Scripts/PastelQA.cs:164` · `PastelQA.Start` → `Capture("modal-content-end")`
- `Assets/Scripts/PastelQA.cs:165` · `PastelQA.Start` → `Capture("modal-purchase")`
- `Assets/Scripts/PastelQA.cs:166` · `PastelQA.Start` → `Capture("loading")`
- `Assets/Scripts/PastelQA.cs:166` · `PastelQA.Start` → `Capture("tutorial")`
- `Assets/Scripts/PastelQA.cs:166` · `PastelQA.Start` → `Capture("battle-0")`
- `Assets/Scripts/PastelQA.cs:166` · `PastelQA.Start` → `Capture("modal-pause")`
- `Assets/Scripts/PastelQA.cs:167` · `PastelQA.Start` → `Capture("battle-"+level)`
- `Assets/Scripts/PastelQA.cs:168` · `PastelQA.Start` → `Capture("result-victory")`
- `Assets/Scripts/PastelQA.cs:170` · `PastelQA.Start` → `Capture("modal-ad")`
- `Assets/Scripts/PastelQA.cs:170` · `PastelQA.Start` → `Capture("result-defeat")`
- `Assets/Scripts/PastelQA.cs:171` · `PastelQA.Start` → `Capture("wallet-"+amount)`
- `Assets/Scripts/PastelQA.cs:173` · `PastelQA.Start` → `Capture("safe-area")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();Canvas.ForceUpdateCanvases();AuditLayout(name);if(target==null){var captureTarget=new RenderTexture(Screen.width,Screen.height,24);var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,captureTarget,image);File.WriteAllBytes(Path.Combine(dir,name+".png"),image.EncodeToPNG());captureTarget.Release();Destroy(captureTarget);Destroy(image);}else{Camera.main.Render();var previous=RenderTexture.active;RenderTexture.active=target;var image=new Texture2D(target.width,target.height,TextureFormat.RGB24,false);image.ReadPixels(new Rect(0,0,target.width,target.height),0,0);image.Apply();RenderTexture.active=previous;var full=new Texture2D(outerW,outerH,TextureFormat.RGB24,false);var colors=new Color32[outerW*outerH];for(int i=0;i<colors.Length;i++)colors[i]=new Color32(248,242,233,255);full.SetPixels32(colors);full.SetPixels(padX,padY,target.width,target.height,image.GetPixels());full.Apply();File.WriteAllBytes(Path.Combine(dir,name+".png"),full.EncodeToPNG());Destroy(image);Destroy(full);}}
```

## 对象层级和素材入口

- L15：`GameObject.Find("Play current level")`
- L29：`GameObject.Find("Header wordmark")`
- L29：`wordmark.GetChild(0)`
- L42：`GameObject.Find("Play current level")`
- L54：`GameObject.Find("Full screen cream backdrop")`
- L57：`GameObject.Find(key)`
- L59：`nav.transform.Find("Pastel fixed navigation/Nav button "+page+"/Press visual/Pop icon")`
- L61：`icon.parent.Find("Selected label strip "+page)`
- L62：`GameObject.Find("Nav button home")`
- L65：`GameObject.Find("Play current level")`
- L85：`nav.transform.Find("Pastel fixed navigation/Nav button "+page+"/Press visual")`
- L85：`item.Find("Pop icon")`
- L89：`nav.transform.Find("Pastel fixed navigation/Nav button "+key+"/Press visual/Pop icon")`
- L95：`FindFirstObjectByType<Round6Nav>().transform.Find("Pastel fixed navigation/Nav button shop/Press visual/Pop icon")`
- L98：`GameObject.Find("Settings medallion")`
- L103：`GameObject.Find("Header wordmark")`
- L104：`GameObject.Find("Avatar medallion/Cat cosmetic portrait/Frame")`
- L105：`GameObject.Find("Level progress")`
- L112：`GameObject.Find("Settings medallion")`
- L120：`GameObject.Find("Avatar medallion/Cat cosmetic portrait/Frame")`
- L121：`GameObject.Find("Settings medallion/ButtonVisual/button_circle_cream")`
- L122：`GameObject.Find("Level progress")`
- L126：`GameObject.Find("Logo animation page")`
- L132：`GameObject.Find("Wallet coin")`
- L150：`GameObject.Find("Logo animation page")`
- L152：`GameObject.Find("Hero group")`
- L153：`GameObject.Find("home/Pastel layered logo")`
- L154：`GameObject.Find("Button VIP")`
- L155：`GameObject.Find("Level progress")`
- L158：`GameObject.Find("Logo animation page")`

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
int checks,failed,errors;
```
```csharp
RenderTexture target;
```
```csharp
int outerW,outerH,padX,padY;
```
```csharp
StreamWriter layout;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
