# Mobile73QA · Mobile73QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Mobile73QA.cs`
文件 SHA-256：`a0934912236b43214287981031388d98d2952430885a5e32f93ffd201645bb49`
[原始源码快照](../source/Assets/Scripts/Mobile73QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L16

方法 SHA-256：`1c18075fca85c9cf5833a0cffaf2c0f438ab58e2961325727eefef3885682ef7`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Mobile73QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();
  // Logical phone sizes and status/safe-area insets, independent of rendering DPR.
  foreach(var phone in new[]{new Vector3(320,568,20),new Vector3(360,800,24),new Vector3(375,667,20),new Vector3(390,844,47),new Vector3(393,852,59),new Vector3(430,932,59),new Vector3(412,915,32),new Vector3(768,1024,24)}){
   Screen.SetResolution((int)phone.x,(int)phone.y,false);yield return new WaitForSecondsRealtime(.25f);
   var pixels=new Vector2Int(Screen.width,Screen.height);float w=phone.x,h=phone.y,top=phone.z;
   app.QASetViewport(pixels);app.QASetSafeArea(new Rect(0,34/h*pixels.y,pixels.x,(h-top-34)/h*pixels.y));
   WeChatLayoutMetrics.Set(w,h,new Rect(0,top,w,h-top-34),new Rect(w-95,top+4,87,32));
   foreach(var page in new[]{"chapters","ranking","shop"}){
    app.QANavigate(page);yield return new WaitForSecondsRealtime(.35f);Canvas.ForceUpdateCanvases();
    var nav=FindObjectsByType<MobileNavigation73>(FindObjectsSortMode.None).First(n=>n.gameObject.activeInHierarchy&&n.GetComponentInParent<CanvasGroup>().interactable);Rect blocked;WeChatLayoutMetrics.TryGetBlockedRect(pixels,out blocked);
    var title=Bounds(nav.Title);var back=Bounds(nav.Back);float unit=WeChatLayoutMetrics.LogicalPixelScale(pixels);
    Check(!title.Overlaps(blocked)&&!back.Overlaps(blocked),page+" clears native capsule "+w+"x"+h);
    Check(title.xMin>=0&&title.xMax<=pixels.x+1&&title.yMax<=app.MenuSafeRect.yMax+1,page+" title inside safe area "+w);
    Check(Mathf.Abs(title.center.x-pixels.x*.5f)<1,page+" title stays horizontally centered "+w);
    Check(back.width>=43*unit&&back.height>=43*unit,page+" return hit area >=44 logical px "+w);
    Check(Button("Shop information")==null&&Button("League rewards preview")==null&&Button("i")==null,page+" no right information button");
    if(w==390||w==320)yield return Capture(page+"-"+w);
   }
  }
  WeChatLayoutMetrics.Set(390,844,new Rect(0,47,390,763),Rect.zero);app.QANavigate("shop");yield return new WaitForSecondsRealtime(.25f);Check(FindObjectsByType<MobileNavigation73>(FindObjectsSortMode.None).Any(n=>n.gameObject.activeInHierarchy),"invalid capsule uses fallback");
  Screen.SetResolution(390,844,false);yield return new WaitForSecondsRealtime(.25f);app.QASetViewport(new Vector2Int(Screen.width,Screen.height));app.QASetSafeArea(new Rect(0,34,Screen.width,Screen.height-81));app.QANavigate("home");yield return new WaitForSecondsRealtime(.3f);yield return Capture("home");
  Click("Settings medallion");yield return new WaitForSecondsRealtime(.3f);Click("声音设置");yield return new WaitForSecondsRealtime(.3f);
  var sliders=FindObjectsByType<Slider>(FindObjectsSortMode.None).Where(s=>s.gameObject.activeInHierarchy).OrderByDescending(s=>s.transform.position.y).ToArray();Check(sliders.Length==3,"three volume bus controls");
  for(int i=0;i<sliders.Length;i++){
   var slider=sliders[i];var rect=slider.GetComponent<RectTransform>();var b=Bounds(rect);float previous=slider.value;slider.OnPointerDown(new PointerEventData(EventSystem.current){button=PointerEventData.InputButton.Left,position=new Vector2(b.xMin+b.width*.25f,b.center.y)});
   Check(Mathf.Abs(slider.value-previous)>.05f&&Mathf.Abs(SoundService.BusVolume(i)-slider.value)<.01f,"volume track touch updates bus "+i);
  }
  Click("声音总开关：开");yield return new WaitForSecondsRealtime(.25f);Check(SoundService.IsMuted,"master sound actually muted");Click("声音总开关：关");yield return new WaitForSecondsRealtime(.25f);Check(!SoundService.IsMuted,"master sound actually resumed");
  yield return Capture("audio-settings");
  int pulses=0;PlayerHaptics71.MobilePulse=(strength,done)=>{pulses++;done?.Invoke(true);};PlayerHaptics71.Enabled=true;Check(Button("测试震动")==null,"audio settings excludes vibration test");Click("返回设置");yield return new WaitForSecondsRealtime(.25f);
  Click("震动：开");Check(!PlayerHaptics71.Enabled,"vibration switch disabled");PlayerHaptics71.TryPulse(PlayerHaptics71.Strength.Medium,_=>{});Check(pulses==0,"disabled vibration never calls device");
  Click("震动：关");Check(PlayerHaptics71.Enabled&&pulses==0,"enabling vibration has no preview");
  bool? result=null;PlayerHaptics71.MobilePulse=(strength,done)=>done?.Invoke(false);PlayerHaptics71.TryPulse(PlayerHaptics71.Strength.Medium,ok=>result=ok);Check(result==false,"failed device API propagates failure");
  Click("玩法帮助");yield return new WaitForSecondsRealtime(.25f);Check(Button("返回设置")!=null,"help opens concrete instructions");Click("返回设置");yield return new WaitForSecondsRealtime(.25f);Click("关于游戏");yield return new WaitForSecondsRealtime(.25f);Check(Button("返回设置")!=null,"about opens version screen");Click("返回设置");yield return new WaitForSecondsRealtime(.25f);yield return Capture("settings");
  PlayerHaptics71.MobilePulse=null;Check(errors==0,"zero runtime errors");File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures+errors==0?0:1);
 }
```

### Capture · L55

方法 SHA-256：`f094ecae679c3fd372b8397e6fb9d326d92300dcc8bd502ccdcd5bf4df74d0c9`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Mobile73QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Mobile73QA.cs:35` · `Mobile73QA.Start` → `Capture(page+"-"+w)`
- `Assets/Scripts/Mobile73QA.cs:39` · `Mobile73QA.Start` → `Capture("home")`
- `Assets/Scripts/Mobile73QA.cs:47` · `Mobile73QA.Start` → `Capture("audio-settings")`
- `Assets/Scripts/Mobile73QA.cs:52` · `Mobile73QA.Start` → `Capture("settings")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var tex=ScreenCapture.CaptureScreenshotAsTexture();File.WriteAllBytes(dir+"/"+name+".png",tex.EncodeToPNG());Destroy(tex);}
```

## 对象层级和素材入口


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
int checks,failures,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
