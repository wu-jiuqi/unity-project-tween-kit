# JellyRibbonTexture80QA · JellyRibbonTexture80QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/JellyRibbonTexture80QA.cs`
文件 SHA-256：`d0f0010da676914fc9f775542f6fedac8356d82d8b5eee53be39a100eb664556`
[原始源码快照](../source/Assets/Scripts/JellyRibbonTexture80QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### CaptureNative · L9

方法 SHA-256：`3eee3148d99f805aab03f3eb7f80e39a884571642ec79c02a6848032a0be9c78`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyRibbonTexture80QA.CaptureNative(string, bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/JellyRibbonTexture80QA.cs:11` · `JellyRibbonTexture80QA.Start` → `CaptureNative("01-first-open",true)`
- `Assets/Scripts/JellyRibbonTexture80QA.cs:13` · `JellyRibbonTexture80QA.Start` → `CaptureNative("02-injected-stale-white",false)`
- `Assets/Scripts/JellyRibbonTexture80QA.cs:14` · `JellyRibbonTexture80QA.Start` → `CaptureNative("03-configure-rebind",true)`
- `Assets/Scripts/JellyRibbonTexture80QA.cs:15` · `JellyRibbonTexture80QA.Start` → `CaptureNative("04-reenable",true)`
- `Assets/Scripts/JellyRibbonTexture80QA.cs:16` · `JellyRibbonTexture80QA.Start` → `CaptureNative("05-scroll-return",true)`
- `Assets/Scripts/JellyRibbonTexture80QA.cs:17` · `JellyRibbonTexture80QA.Start` → `CaptureNative("06-menu-return",true)`
- `Assets/Scripts/JellyRibbonTexture80QA.cs:18` · `JellyRibbonTexture80QA.Start` → `CaptureNative("07-phone-layout",true)`

```csharp
IEnumerator CaptureNative(string name,bool mint){yield return new WaitForEndOfFrame();var im=ScreenCapture.CaptureScreenshotAsTexture();File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());for(int i=0;i<3;i++){var p=Probe(.38f+i*.12f);Color color=im.GetPixel(Mathf.Clamp(Mathf.RoundToInt(p.x),0,im.width-1),Mathf.Clamp(Mathf.RoundToInt(p.y),0,im.height-1));File.AppendAllText(dir+"/pixels.txt",name+" sample="+i+" screen="+p+" rgb="+color+"\n");Check(mint?color.g-color.r>.08f&&color.g-color.b>.025f:color.r>.96f&&color.g>.96f&&color.b>.96f,(mint?"mint texture visible":"white-renderer fixture reproduced")+" native "+name+" sample="+i);}Destroy(im);}
```

### Start · L10

方法 SHA-256：`a14fc2cbb18048146a2d694928ed272648831dfef051f172cfaf5e32dc4d314f`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyRibbonTexture80QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};yield return new WaitForSecondsRealtime(4);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");Screen.SetResolution(390,844,false);yield return new WaitForSecondsRealtime(.5f);Screen.SetResolution(540,1170,false);yield return new WaitForSecondsRealtime(.6f);app.QASetViewport(new Vector2Int(540,1170));
  for(int i=0;i<78;i++)app.Profile.CommitAlphabetWin("ribbon-native-"+i,i,50000);app.QAOpenChapters();yield return null;map=app.GetComponentInChildren<JellyJourney80>();map.FocusLevel(35);yield return new WaitForSecondsRealtime(1);ribbon=map.GetComponentInChildren<JellyRouteRibbon80>();Check(ribbon!=null&&ribbon.mainTexture==JellyJourneyArt80.Get("route-material").texture,"ribbon exposes authored material texture");yield return CaptureNative("01-first-open",true);
  // Reproduce stale native binding without changing the sprite or rebuilding the canvas.
  ribbon.canvasRenderer.SetTexture(Texture2D.whiteTexture);ribbon.SetVerticesDirty();yield return null;yield return CaptureNative("02-injected-stale-white",false);
  ribbon.Configure(map,32,39);yield return null;yield return CaptureNative("03-configure-rebind",true);
  ribbon.gameObject.SetActive(false);yield return null;ribbon.gameObject.SetActive(true);yield return null;yield return CaptureNative("04-reenable",true);
  for(int i=0;i<78;i+=7){map.FocusLevel(i);yield return null;Check(ribbon.mainTexture==JellyJourneyArt80.Get("route-material").texture&&ribbon.VertexCount>0,"scroll retains texture and geometry at "+(i+1));}map.FocusLevel(35);yield return null;yield return CaptureNative("05-scroll-return",true);
  app.QANavigate("home");yield return null;app.QAOpenChapters();yield return null;map.FocusLevel(35);yield return null;yield return CaptureNative("06-menu-return",true);
  Screen.SetResolution(390,844,false);yield return new WaitForSecondsRealtime(.4f);app.QASetViewport(new Vector2Int(390,844));map.FocusLevel(35);yield return null;yield return CaptureNative("07-phone-layout",true);
  Check(errors==0,"no runtime errors");File.WriteAllText(dir+"/report.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
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
int checks,failures,errors;
```
```csharp
AppUI app;
```
```csharp
JellyJourney80 map;
```
```csharp
JellyRouteRibbon80 ribbon;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
