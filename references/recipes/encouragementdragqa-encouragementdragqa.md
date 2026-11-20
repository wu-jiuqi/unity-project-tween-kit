# EncouragementDragQA · EncouragementDragQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/EncouragementDragQA.cs`
文件 SHA-256：`770e5fa6a43944876d8c5258b6444e17c477a0077e16ac87e0d8e37df222dd01`
[原始源码快照](../source/Assets/Scripts/EncouragementDragQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L8

方法 SHA-256：`f56177c7ca479812c0338b5e6e7b483a4fcfa62e629f07f09e6e7425a8602eea`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.EncouragementDragQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/EncouragementDragQA.cs:14` · `EncouragementDragQA.Start` → `Capture("motion-"+f.ToString("D3"))`
- `Assets/Scripts/EncouragementDragQA.cs:14` · `EncouragementDragQA.Start` → `Capture("home-one-win")`
- `Assets/Scripts/EncouragementDragQA.cs:21` · `EncouragementDragQA.Start` → `Capture("drag-held")`
- `Assets/Scripts/EncouragementDragQA.cs:26` · `EncouragementDragQA.Start` → `Capture("drag-motion-"+f.ToString("D3"))`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();Camera.main.Render();var old=RenderTexture.active;RenderTexture.active=target;var t=new Texture2D(target.width,target.height,TextureFormat.RGB24,false);t.ReadPixels(new Rect(0,0,target.width,target.height),0,0);t.Apply();RenderTexture.active=old;File.WriteAllBytes(Path.Combine(dir,name+".png"),t.EncodeToPNG());Destroy(t);}
```

### Start · L9

方法 SHA-256：`27000c948fe7bf98675dd7bfb071427526d9de82615918db43b44d2d00896321`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.EncouragementDragQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s+"\n");}};
 yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();var size=args[Array.IndexOf(args,"--qa-viewport")+1].Split('x');int w=int.Parse(size[0]),h=int.Parse(size[1]);target=new RenderTexture(w,h,24);target.Create();Camera.main.targetTexture=target;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=Camera.main;canvas.planeDistance=1;app.QASetViewport(new Vector2Int(w,h));app.QANavigate("home");yield return new WaitForSecondsRealtime(1);app.QASetViewport(new Vector2Int(w,h));
 Check(RuntimeQA.IsIsolated,"isolated player data");Check(LocalHomeFeedbackSource.RunSelfTests().StartsWith("PASS:"),"feedback storage and copy rules retained");
 var saved=(IHomeFeedbackSource)typeof(AppUI).GetField("savedFeedback",BindingFlags.Instance|BindingFlags.NonPublic).GetValue(app);saved.StartLevel();saved.RecordResult("encouragement-test",0,true,true);app.QANavigate("home");yield return null;
 var view=FindFirstObjectByType<HomeFeedbackView>();TMP_Text title=null,detail=null;foreach(var text in view.GetComponentsInChildren<TMP_Text>()){if(text.text=="Great start!")title=text;if(text.text=="One win in the bag.")detail=text;}Check(title!=null&&detail!=null,"actual one-win copy displayed");Check(title.fontSizeMax==78&&detail.fontSizeMax==48,"headline and detail enlarged about nine percent");
 bool changed=false,stretched=false;float min=10,max=0;for(int f=0;f<54;f++){float y=title.rectTransform.localScale.y;min=Mathf.Min(min,y);max=Mathf.Max(max,y);stretched|=y>1.03f;changed|=Mathf.Abs(title.rectTransform.anchoredPosition.y-15)>.5f;yield return Capture("motion-"+f.ToString("D3"));yield return new WaitForSecondsRealtime(1f/30f);}Check(max-min>.07f&&stretched&&changed,"visible title squash, spring and lift");yield return Capture("home-one-win");
 float first=title.rectTransform.anchoredPosition.y;yield return new WaitForSecondsRealtime(.6f);Check(Mathf.Abs(title.rectTransform.anchoredPosition.y-first)>.1f,"idle text gently floats");
 var drag=view.GetComponent<EncouragementDrag>();var root=(RectTransform)view.transform;var visual=root.Find("Encouragement drag visual").GetComponent<RectTransform>();var homePosition=root.anchoredPosition;var homeScale=root.localScale;
 var e=new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current){pointerId=-1,button=UnityEngine.EventSystems.PointerEventData.InputButton.Left,eligibleForClick=true};
 e.position=RectTransformUtility.WorldToScreenPoint(Camera.main,root.position);e.pointerCurrentRaycast=new UnityEngine.EventSystems.RaycastResult{module=canvas.GetComponent<GraphicRaycaster>(),gameObject=root.gameObject};e.pointerPressRaycast=e.pointerCurrentRaycast;
 var hits=new System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult>();UnityEngine.EventSystems.EventSystem.current.RaycastAll(e,hits);Check(hits.Count>0&&hits[0].gameObject==root.gameObject,"encouragement is reachable through actual UI raycast");
 drag.OnPointerDown(e);drag.OnBeginDrag(e);e.position+=new Vector2(150,55)*(w/540f);drag.OnDrag(e);yield return null;
 Check(drag.Held&&drag.Pull.x>.5f,"mouse drag follows pointer");Check(visual.localScale.x>1.15f&&Mathf.Abs(visual.localEulerAngles.z)>5,"drag visibly stretches and tilts group");Check(!e.eligibleForClick,"drag cancels click eligibility");Check(root.anchoredPosition==homePosition&&root.localScale==homeScale,"hit rectangle stays fixed");yield return Capture("drag-held");
 var other=new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current){pointerId=2,button=UnityEngine.EventSystems.PointerEventData.InputButton.Left,position=e.position};drag.OnPointerUp(other);Check(drag.Held,"second pointer cannot release active drag");
 e.position+=Vector2.one*100000;drag.OnDrag(e);yield return null;Check(Mathf.Abs(drag.Pull.x)<=1.001f&&Mathf.Abs(drag.Pull.y)<=1.001f&&visual.anchoredPosition.magnitude<135,"off-window drag remains bounded");
 // Return to a predominantly horizontal pull to show a clear sideways recoil.
 e.position=RectTransformUtility.WorldToScreenPoint(Camera.main,root.position)+new Vector2(150,35)*(w/540f);drag.OnDrag(e);drag.OnPointerUp(e);drag.OnEndDrag(e);bool crossed=false;
 for(int f=0;f<72;f++){crossed|=drag.Pull.x<-.03f;yield return Capture("drag-motion-"+f.ToString("D3"));yield return new WaitForSecondsRealtime(1f/30f);}
 Check(crossed,"release overshoots past rest position");yield return new WaitForSecondsRealtime(.8f);Check(drag.Pull.magnitude<.002f&&Vector2.Distance(visual.anchoredPosition,Vector2.zero)<.2f,"release settles back to rest");
 e.pointerId=0;e.position=RectTransformUtility.WorldToScreenPoint(Camera.main,root.position);drag.OnPointerDown(e);drag.OnBeginDrag(e);e.position+=new Vector2(0,100);drag.OnDrag(e);Check(drag.Held&&drag.Pull.y>.3f,"touch drag uses the same spring feedback");FeedbackProfile.Active.enabledFeedback=false;yield return null;Check(!drag.Held&&visual.localScale==Vector3.one&&drag.Pull==Vector2.zero,"disabled feedback clears drag state");FeedbackProfile.Active.enabledFeedback=true;
 drag.OnPointerDown(e);e.position+=new Vector2(40,0);drag.OnDrag(e);GameObject.Find("Settings medallion").GetComponent<Button>().onClick.Invoke();yield return new WaitForSecondsRealtime(.2f);Check(!drag.Held&&visual.anchoredPosition==Vector2.zero,"modal cancels active drag");Call("CloseModal");
 drag.OnPointerDown(e);e.position+=new Vector2(40,0);drag.OnDrag(e);root.gameObject.SetActive(false);Check(!drag.Held&&visual.localScale==Vector3.one,"disable cancels drag and resets visual");root.gameObject.SetActive(true);
 FeedbackProfile.Active.enabledFeedback=false;yield return null;Check(title.rectTransform.localScale==Vector3.one&&detail.rectTransform.localScale==Vector3.one,"feedback toggle restores text pose");FeedbackProfile.Active.enabledFeedback=true;
 GameObject.Find("Settings medallion").GetComponent<Button>().onClick.Invoke();yield return new WaitForSecondsRealtime(.2f);Check(title.rectTransform.localScale==Vector3.one,"modal suspends encouragement animation");Call("CloseModal");
 foreach(var scenario in new[]{new[]{1,0,0},new[]{7,0,0},new[]{1,3,1},new[]{1,5,1}}){Call("PreviewFeedback",scenario[0],scenario[1],scenario[2]==1);yield return new WaitForSecondsRealtime(.4f);view=FindFirstObjectByType<HomeFeedbackView>();foreach(var text in view.GetComponentsInChildren<TMP_Text>()){text.ForceMeshUpdate();Check(text.GetPreferredValues(text.text,text.rectTransform.rect.width,Mathf.Infinity).y<=text.rectTransform.rect.height+6,"copy fits: "+text.text);}}
 Call("UseSavedFeedback");app.QANavigate("shop");yield return null;app.QANavigate("home");yield return new WaitForSecondsRealtime(.5f);Check(FindFirstObjectByType<HomeFeedbackView>()!=null,"return home restores encouragement");Check(saved.Read().WinStreak==1,"animation and previews preserve one-win state");Check(errors==0,"zero runtime errors");File.AppendAllText(Path.Combine(dir,"checks.txt"),"RESULT checks="+checks+" failed="+failures+" errors="+errors+"\n");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口

- L16：`root.Find("Encouragement drag visual")`
- L29：`GameObject.Find("Settings medallion")`
- L32：`GameObject.Find("Settings medallion")`

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
