# FailureQA · FailureQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/FailureQA.cs`
文件 SHA-256：`933aadfab2260026480883d4c7aaf2ea8bebc06dc895da66b513f945b4930188`
[原始源码快照](../source/Assets/Scripts/FailureQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Level · L16

方法 SHA-256：`82bc722eacc61c7d580118882f32358aa7f6b1f33240eb0dbaf0ddc2ec11390b`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FailureQA.Level()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/FailureQA.cs:31` · `FailureQA.Start` → `Level()`
- `Assets/Scripts/FailureQA.cs:57` · `FailureQA.Start` → `Level()`
- `Assets/Scripts/FailureQA.cs:57` · `FailureQA.Start` → `Level()`

```csharp
IEnumerator Level(){app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.25f);}
```

### Lose · L17

方法 SHA-256：`7ddb8023fb15e5ffb928177e76b859c31441e9e07f4c2587f5946ae6b557a229`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FailureQA.Lose()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/FailureQA.cs:32` · `FailureQA.Start` → `Lose()`
- `Assets/Scripts/FailureQA.cs:57` · `FailureQA.Start` → `Lose()`
- `Assets/Scripts/FailureQA.cs:58` · `FailureQA.Start` → `Lose()`
- `Assets/Scripts/FailureQA.cs:58` · `FailureQA.Start` → `Lose()`
- `Assets/Scripts/FailureQA.cs:59` · `FailureQA.Start` → `Lose()`

```csharp
IEnumerator Lose(){Exhaust();yield return new WaitForSecondsRealtime(1.5f);}
```

### Capture · L18

方法 SHA-256：`18299981e7ef96dd544257ecc62ccca644b08622cf1b45bef3b329ce1db857a1`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FailureQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/FailureQA.cs:40` · `FailureQA.Start` → `Capture("failure-"+age.ToString("F1",System.Globalization.CultureInfo.InvariantCulture))`
- `Assets/Scripts/FailureQA.cs:50` · `FailureQA.Start` → `Capture("failure-final")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();cam.rect=new Rect(0,0,1,1);cam.aspect=(float)width/height;Canvas.ForceUpdateCanvases();
        // Match the production ScreenSpaceOverlay layout after switching the QA
        // camera from the letterboxed board to a full-viewport render target.
        var design=(RectTransform)typeof(AppUI).GetField("designRoot",BindingFlags.Instance|BindingFlags.NonPublic).GetValue(app);
        var safe=(RectTransform)design.parent;design.localScale=Vector3.one*Mathf.Min(safe.rect.width/1080f,safe.rect.height/2340f);
        Canvas.ForceUpdateCanvases();cam.Render();var old=RenderTexture.active;RenderTexture.active=target;var im=new Texture2D(width,height,TextureFormat.RGB24,false);im.ReadPixels(new Rect(0,0,width,height),0,0);im.Apply();RenderTexture.active=old;File.WriteAllBytes(Path.Combine(dir,name+".png"),im.EncodeToPNG());Destroy(im);}
```

### Start · L24

方法 SHA-256：`725da634a1617521caf9ab4950f727dddb69b833f5ba5db74f41d730e87e5a5c`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.FailureQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
        var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");
        int sizeIndex=Array.IndexOf(args,"--qa-size");if(sizeIndex>=0){var size=args[sizeIndex+1].Split('x');width=int.Parse(size[0]);height=int.Parse(size[1]);}
        Application.logMessageReceived+=(message,stack,type)=>{if(type==LogType.Error||type==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),message+"\n"+stack+"\n");}};
        yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();cam=Camera.main;target=new RenderTexture(width,height,24);target.Create();cam.targetTexture=target;
        var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;app.QASetViewport(new Vector2Int(width,height));
        Check(RuntimeQA.IsIsolated,"isolated test profile");Check(RuleSelfTests.RunAll().StartsWith("PASS"),"rule regression including once-only continue and win rejection");
        yield return Level();int generation=app.Session.Generation,targets=app.Session.RemainingTargets,coins=app.Profile.Coins;
        yield return Lose();var presentation=FindFirstObjectByType<FailurePresentation>();Check(presentation!=null&&presentation.Ready,"failure presentation appears after actual exhausted-shot outcome");
        Check(!app.Session.IsPlaying&&app.Session.CanContinue,"continue offered only after terminal loss");int letters=0;foreach(Transform t in presentation.transform){if(t.name.StartsWith("Failure letter "))letters++;}Check(letters==8,"eight independent jelly title letters");
        var cat=presentation.transform.Find("Failure cat sequence").GetComponent<Image>();
        Check(cat!=null&&!cat.raycastTarget,"approved v26 cat present without blocking buttons");
        Check(presentation.CatFrame>0&&presentation.CatFrame<15,"cat naturally advances while board is stopped");
        Check(FailureCatAnimation.FrameAt(0)==0&&FailureCatAnimation.FrameAt(.7f)==3&&FailureCatAnimation.FrameAt(1.3f)==8&&FailureCatAnimation.FrameAt(8)==15,"HTML timing matches Unity including final hold");
        for(int i=0;i<16;i++)Check(FailureCatAnimation.Get(i).rect.size==new Vector2(512,512),"frame "+i+" uses fixed registered canvas");
        var clock=typeof(FailurePresentation).GetField("clock",BindingFlags.Instance|BindingFlags.NonPublic);
        foreach(float age in new[]{.7f,1.3f,2.5f}){clock.SetValue(presentation,age);yield return null;yield return Capture("failure-"+age.ToString("F1",System.Globalization.CultureInfo.InvariantCulture));}
        yield return new WaitForSecondsRealtime(.3f);Check(presentation.CatFrame==15,"prone frame holds after completion");
        var corners=new Vector3[4];cat.rectTransform.GetWorldCorners(corners);float bottom=presentation.transform.InverseTransformPoint(corners[0]).y,top=presentation.transform.InverseTransformPoint(corners[1]).y;
        Check(bottom>-421&&top<450,"full cat canvas clears subtitle and ad button at "+width+"x"+height);
        Check(Mathf.Abs(cat.rectTransform.anchoredPosition.x)<.01f&&cat.rectTransform.sizeDelta==new Vector2(760,760),"cat centered with constant square dimensions");
        FeedbackProfile.Active.enabledFeedback=false;yield return null;Check(presentation.CatFrame==15&&cat.rectTransform.localScale==Vector3.one,"reduced motion holds prone pose without breathing");FeedbackProfile.Active.enabledFeedback=true;
        foreach(string name in new[]{"Button Watch ad continue","Button Try again","Button Home"}){
            var b=Find(name);var group=(RectTransform)b.transform.Find("ButtonVisual/Centered content");Check(Mathf.Abs(group.anchoredPosition.x)<.01f,"centered icon/text group: "+name);
            foreach(var image in group.GetComponentsInChildren<Image>())Check(Mathf.Abs(image.rectTransform.anchoredPosition.y)<.01f,"icon aligned to content center: "+name);
        }
        yield return Capture("failure-final");var bg=presentation.transform.Find("Failure looping clouds").GetComponent<Image>();var initial=bg.sprite;yield return new WaitForSecondsRealtime(.15f);Check(bg.sprite!=initial,"background loop continues behind cat");
        foreach(string outcome in new[]{"Cancel","Fail","X"}){Find("Button Watch ad continue").onClick.Invoke();yield return null;Find("Button "+outcome).onClick.Invoke();yield return null;Check(!app.Session.IsPlaying&&app.Session.Shots==0&&app.Session.Generation==generation,"ad "+outcome+" keeps failed board and grants nothing");}
        Find("Button Watch ad continue").onClick.Invoke();yield return null;var complete=Find("Button Complete  +5 shots");complete.onClick.Invoke();complete.onClick.Invoke();yield return null;
        Check(app.Session.IsPlaying&&app.Session.Shots==5&&app.Session.Generation==generation,"successful ad resumes same session; duplicate callback grants once");
        Check(app.Session.RemainingTargets==targets&&app.Profile.Coins==coins,"continue preserves board and currency");Check(FindFirstObjectByType<FailurePresentation>()==null,"failure overlay removed on resume");
        Check(app.Session.TryShoot(Vector2.up),"resumed level accepts real shot");yield return new WaitForSecondsRealtime(.3f);Check(app.Session.Shots==4&&app.Session.ActiveBalls+app.Session.PendingBurstBalls>0,"real volley consumes one extra shot");
        // Separate session to test expiry of SDK completion callbacks after navigation.
        yield return Level();yield return Lose();Action<MockOutcome> callback=null;RewardedContinueAds.Show=cb=>callback=cb;Find("Button Watch ad continue").onClick.Invoke();Check(callback!=null,"SDK provider receives callback");app.QANavigate("home");yield return Level();int newGeneration=app.Session.Generation;callback(MockOutcome.Completed);RewardedContinueAds.Show=null;yield return null;Check(app.Session.Generation==newGeneration&&app.Session.Shots==30,"stale ad completion cannot affect a new level");
        yield return Lose();Find("Button Watch ad continue").onClick.Invoke();yield return null;Find("Button Complete  +5 shots").onClick.Invoke();yield return null;yield return Lose();Check(!Find("Button Watch ad continue").interactable&&!app.Session.TryContinueAfterAd(),"second failure disables ad continue");
        Find("Button Try again").onClick.Invoke();yield return app.QAWaitForLevel();Check(app.Session.IsPlaying&&app.Session.Shots==30,"retry button restarts full level");yield return Lose();Find("Button Home").onClick.Invoke();yield return null;Check(app.CurrentPage=="home"&&!app.Session.IsPlaying,"home exits failed session");
        string result="RESULT checks="+checks+" failed="+failed+" errors="+errors;File.AppendAllText(Path.Combine(dir,"checks.txt"),result+"\n");Debug.Log(result);Application.Quit(failed==0&&errors==0?0:1);
    }
```

## 对象层级和素材入口

- L34：`presentation.transform.Find("Failure cat sequence")`
- L47：`Find(name)`
- L47：`b.transform.Find("ButtonVisual/Centered content")`
- L50：`presentation.transform.Find("Failure looping clouds")`
- L51：`Find("Button Watch ad continue")`
- L52：`Find("Button Watch ad continue")`
- L57：`Find("Button Watch ad continue")`
- L58：`Find("Button Watch ad continue")`
- L59：`Find("Button Try again")`

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
Camera cam;
```
```csharp
RenderTexture target;
```
```csharp
int width=540,height=1170;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
