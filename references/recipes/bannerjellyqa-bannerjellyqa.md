# BannerJellyQA · BannerJellyQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/BannerJellyQA.cs`
文件 SHA-256：`491a14dbd7a3735e1ed143431282611eeeec8fc95f0a4f926a2eca37f8443805`
[原始源码快照](../source/Assets/Scripts/BannerJellyQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L7

方法 SHA-256：`f56177c7ca479812c0338b5e6e7b483a4fcfa62e629f07f09e6e7425a8602eea`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BannerJellyQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/BannerJellyQA.cs:12` · `BannerJellyQA.Start` → `Capture("home-rest")`
- `Assets/Scripts/BannerJellyQA.cs:16` · `BannerJellyQA.Start` → `Capture("home-hover")`
- `Assets/Scripts/BannerJellyQA.cs:17` · `BannerJellyQA.Start` → `Capture("home-press")`
- `Assets/Scripts/BannerJellyQA.cs:18` · `BannerJellyQA.Start` → `Capture("motion-"+f.ToString("D3"))`
- `Assets/Scripts/BannerJellyQA.cs:19` · `BannerJellyQA.Start` → `Capture("home-second-page")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();Camera.main.Render();var old=RenderTexture.active;RenderTexture.active=target;var t=new Texture2D(target.width,target.height,TextureFormat.RGB24,false);t.ReadPixels(new Rect(0,0,target.width,target.height),0,0);t.Apply();RenderTexture.active=old;File.WriteAllBytes(Path.Combine(dir,name+".png"),t.EncodeToPNG());Destroy(t);}
```

### Start · L8

方法 SHA-256：`d65a0559e4dddef6e953fce0da334195b02ee336726a9166d05010fd86dd8490`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BannerJellyQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s+"\n");}};
 yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();var size=args[Array.IndexOf(args,"--qa-viewport")+1].Split('x');int w=int.Parse(size[0]),h=int.Parse(size[1]);target=new RenderTexture(w,h,24);target.Create();Camera.main.targetTexture=target;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=Camera.main;canvas.planeDistance=1;app.QASetViewport(new Vector2Int(w,h));app.QANavigate("home");yield return new WaitForSecondsRealtime(1);Canvas.ForceUpdateCanvases();app.QASetViewport(new Vector2Int(w,h));
 var c=FindFirstObjectByType<ActivityCarousel>();var motions=c.GetComponentsInChildren<ActivityJellyMotion>();Check(motions.Length==3,"three independently animated cards");foreach(string key in new[]{"panel","medal","trophy","chest"})Check(Resources.Load<Sprite>("Art/BannerJelly/"+key)!=null,"sprite imported "+key);
 foreach(var text in c.GetComponentsInChildren<TMP_Text>()){text.ForceMeshUpdate();Check(text.GetPreferredValues(text.text,text.rectTransform.rect.width,Mathf.Infinity).y<=text.rectTransform.rect.height+6,"text fits "+text.text);}
 yield return Capture("home-rest");
 var card=(RectTransform)motions[0].transform;var hitPosition=card.anchoredPosition;RectTransform icon=null;foreach(var image in card.GetComponentsInChildren<Image>())if(image.sprite!=null&&image.sprite.name=="medal")icon=image.rectTransform;var visual=card.Find("Jelly visual").GetComponent<RectTransform>();
 var e=new PointerEventData(EventSystem.current){pointerId=-1,button=PointerEventData.InputButton.Left};e.position=RectTransformUtility.WorldToScreenPoint(Camera.main,card.position); // Direct callbacks need an explicit raycast camera, just like EventSystem.
 e.pointerCurrentRaycast=new RaycastResult{module=canvas.GetComponent<GraphicRaycaster>(),gameObject=c.gameObject};e.pointerPressRaycast=e.pointerCurrentRaycast;
 c.OnPointerEnter(e);yield return new WaitForSecondsRealtime(.5f);Check(icon.localScale.x>1.04f&&icon.anchoredPosition.y>-32,"hover grows and lifts reward");yield return Capture("home-hover");
 c.OnPointerDown(e);yield return new WaitForSecondsRealtime(.18f);Check(icon.localScale.x>1.1f&&icon.localScale.y<1,"press visibly squashes reward");yield return Capture("home-press");c.OnPointerUp(e);
 for(int f=0;f<42;f++){yield return Capture("motion-"+f.ToString("D3"));yield return new WaitForSecondsRealtime(1f/30f);}Check(card.anchoredPosition==hitPosition&&card.localScale==Vector3.one,"animation preserves fixed hit region");c.OnPointerExit(e);
 c.QADrag(new Vector2(-310,0));yield return new WaitForSecondsRealtime(.8f);Check(c.SelectedPage==1&&Mathf.Abs(c.Offset-384)<.05f,"drag snaps to second page");yield return Capture("home-second-page");c.QADrag(new Vector2(900,0));yield return new WaitForSecondsRealtime(.8f);Check(c.SelectedPage==0&&Mathf.Abs(c.Offset)<.05f,"drag clamps and returns to first page");c.QADrag(new Vector2(0,100));Check(c.SelectedPage==0,"vertical drag does not change page");
 FeedbackProfile.Active.enabledFeedback=false;yield return null;Check(visual.localScale==Vector3.one&&icon.localScale==Vector3.one,"feedback toggle restores rest pose");FeedbackProfile.Active.enabledFeedback=true;
 GameObject.Find("Settings medallion").GetComponent<Button>().onClick.Invoke();yield return new WaitForSecondsRealtime(.3f);Check(!app.HomeMotionAllowed&&icon.localScale==Vector3.one,"modal suspends banner motion");typeof(AppUI).GetMethod("CloseModal",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic).Invoke(app,null);
 app.QANavigate("shop");yield return null;app.QANavigate("home");yield return new WaitForSecondsRealtime(.6f);Check(FindFirstObjectByType<ActivityCarousel>()!=null,"navigation restores carousel");Check(errors==0,"zero runtime errors");File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failures+" errors="+errors+"\n");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口

- L13：`card.Find("Jelly visual")`
- L21：`GameObject.Find("Settings medallion")`

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
```csharp
RenderTexture target;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
