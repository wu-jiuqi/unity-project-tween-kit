# SettingsCQA · SettingsCQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/SettingsCQA.cs`
文件 SHA-256：`08e2bbdc566267b284e8f07f3e2e5944c581b8587c12769232b5041b0b6ad494`
[原始源码快照](../source/Assets/Scripts/SettingsCQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Stable · L11

方法 SHA-256：`43d2f7baf0095f3b8bcc6a9265834d0a0ed606721d5d1a5d03d0fae5a7b149af`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SettingsCQA.Stable(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SettingsCQA.cs:17` · `SettingsCQA.Start` → `Stable("settings")`
- `Assets/Scripts/SettingsCQA.cs:21` · `SettingsCQA.Start` → `Stable("audio")`
- `Assets/Scripts/SettingsCQA.cs:21` · `SettingsCQA.Start` → `Stable("settings")`
- `Assets/Scripts/SettingsCQA.cs:22` · `SettingsCQA.Start` → `Stable(entry[1])`
- `Assets/Scripts/SettingsCQA.cs:22` · `SettingsCQA.Start` → `Stable("settings")`
- `Assets/Scripts/SettingsCQA.cs:24` · `SettingsCQA.Start` → `Stable("pause")`
- `Assets/Scripts/SettingsCQA.cs:25` · `SettingsCQA.Start` → `Stable("help")`
- `Assets/Scripts/SettingsCQA.cs:25` · `SettingsCQA.Start` → `Stable("pause")`
- `Assets/Scripts/SettingsCQA.cs:26` · `SettingsCQA.Start` → `Stable(pair[1])`
- `Assets/Scripts/SettingsCQA.cs:26` · `SettingsCQA.Start` → `Stable("pause")`
- `Assets/Scripts/SettingsCQA.cs:28` · `SettingsCQA.Start` → `Stable("pause")`
- `Assets/Scripts/SettingsCQA.cs:28` · `SettingsCQA.Start` → `Stable("restart-confirm")`
- `Assets/Scripts/SettingsCQA.cs:29` · `SettingsCQA.Start` → `Stable("pause")`
- `Assets/Scripts/SettingsCQA.cs:29` · `SettingsCQA.Start` → `Stable("home-confirm")`
- `Assets/Scripts/SettingsCQA.cs:31` · `SettingsCQA.Start` → `Stable("settings")`
- `Assets/Scripts/SettingsCQA.cs:31` · `SettingsCQA.Start` → `Stable("settings")`
- `Assets/Scripts/SettingsCQA.cs:31` · `SettingsCQA.Start` → `Stable("settings")`

```csharp
IEnumerator Stable(string page){yield return new WaitForSecondsRealtime(.32f);Canvas.ForceUpdateCanvases();var sheet=app.QASettingsSheet;Check(sheet!=null&&sheet.Page==page,"C-style page: "+page);Check(Button("X")==null,"no upper-right close button: "+page);if(sheet==null)yield break;Check(sheet.GetComponent<CanvasGroup>().alpha>.99f,"entrance fade finishes: "+page);var r=Bounds((RectTransform)sheet.transform);var safe=app.AlphabetSafeRect;Check(r.xMin>=safe.xMin-2&&r.xMax<=safe.xMax+2&&r.yMin>=safe.yMin-2&&r.yMax<=safe.yMax+2,"sheet fits safe area: "+page);var scale=sheet.transform.lossyScale;Check(Mathf.Abs(scale.x-scale.y)<.002f,"sheet keeps uniform aspect in battle/menu: "+page);Check(sheet.GetComponentsInChildren<TMPro.TMP_Text>().All(t=>t.font.sourceFontFile==UICraft.Font),"delivered rounded font: "+page);}
```

### Capture · L12

方法 SHA-256：`64667afcfb140ce291f44aaadb28dc62cfab7e2797ae5e2c33c5badb1849083c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SettingsCQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SettingsCQA.cs:17` · `SettingsCQA.Start` → `Capture("01-settings")`
- `Assets/Scripts/SettingsCQA.cs:21` · `SettingsCQA.Start` → `Capture("07-audio")`
- `Assets/Scripts/SettingsCQA.cs:22` · `SettingsCQA.Start` → `Capture(entry[2])`
- `Assets/Scripts/SettingsCQA.cs:24` · `SettingsCQA.Start` → `Capture("04-pause")`
- `Assets/Scripts/SettingsCQA.cs:26` · `SettingsCQA.Start` → `Capture(pair[2])`
- `Assets/Scripts/SettingsCQA.cs:31` · `SettingsCQA.Start` → `Capture("08-safe-area")`
- `Assets/Scripts/SettingsCQA.cs:31` · `SettingsCQA.Start` → `Capture("09-wechat-capsule")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var native=ScreenCapture.CaptureScreenshotAsTexture();File.WriteAllBytes(dir+"/"+name+"-native.png",native.EncodeToPNG());Destroy(native);var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var tex=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,tex);File.WriteAllBytes(dir+"/"+name+".png",tex.EncodeToPNG());Destroy(tex);rt.Release();Destroy(rt);}
```

### LoadBattle · L13

方法 SHA-256：`590461b8dcc54310a1d78725ac7d93ea63f7c5e7fc584030d100935d40d0efbe`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SettingsCQA.LoadBattle()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SettingsCQA.cs:24` · `SettingsCQA.Start` → `LoadBattle()`

```csharp
IEnumerator LoadBattle(){app.QAStartLevel(20);yield return app.QAWaitForLevel();app.Session.QAIgnoreFocusLoss=true;float until=Time.realtimeSinceStartup+20;while(Time.realtimeSinceStartup<until){app.QAAdvanceAlphabetIntro();app.QABattleTour?.AdvanceForQA();app.QAAdvanceBoosterGuide();if(app.QAGoalEntrance==null&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;yield return null;}Check(!app.Session.IsPaused,"battle ready before pause");}
```

### Start · L14

方法 SHA-256：`c24fca74d8cf6678379eab9234320a90f79df9f76fc42eb0c343b68fcbe97dde`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SettingsCQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.targetFrameRate=60;QualitySettings.vSyncCount=0;Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");Check(!PlayerStrategyTestBuild.Enabled&&GameVersion.Name=="0.4.0","normal 0.4.0 league build");
  Call("Settings");yield return Stable("settings");Check(Button("试玩统计")==null,"normal settings hide test statistics");yield return Capture("01-settings");
  var b=Button("完成");var bounce=b.GetComponent<UIBounce>();Check(bounce!=null&&bounce.Target!=b.transform,"reuse original bounce with stable hit rect");var rect=Bounds((RectTransform)b.transform);var visual=bounce.Target;var initial=visual.localScale;var data=new PointerEventData(EventSystem.current){button=PointerEventData.InputButton.Left,pointerId=-1,position=rect.center};bounce.OnPointerDown(data);yield return new WaitForSecondsRealtime(.13f);Check((visual.localScale-initial).sqrMagnitude>.0001f,"original button press tween deforms artwork");var now=Bounds((RectTransform)b.transform);Check(Vector2.Distance(rect.center,now.center)<.1f&&Vector2.Distance(rect.size,now.size)<.1f,"button input rectangle stays fixed during tween");bounce.OnPointerUp(data);yield return new WaitForSecondsRealtime(.6f);Check((visual.localScale-initial).sqrMagnitude<.001f,"original elastic release settles");
  bool sound=app.Profile.SoundEnabled;Click("声音总开关："+(sound?"开":"关"));yield return new WaitForSecondsRealtime(.2f);Check(app.Profile.SoundEnabled!=sound&&SoundService.IsMuted==sound,"master sound writes actual mute state");Click("声音总开关："+(!sound?"开":"关"));yield return new WaitForSecondsRealtime(.2f);
  bool vibration=PlayerHaptics71.Enabled;Click("震动："+(vibration?"开":"关"));yield return new WaitForSecondsRealtime(.2f);Check(PlayerHaptics71.Enabled!=vibration,"vibration preference changes");Click("震动："+(!vibration?"开":"关"));yield return new WaitForSecondsRealtime(.2f);
  Click("声音设置");yield return Stable("audio");var sliders=app.GetComponentsInChildren<Slider>().OrderByDescending(s=>s.transform.position.y).ToArray();Check(sliders.Length==3,"all three audio buses retained");for(int i=0;i<sliders.Length;i++){var slider=sliders[i];float old=slider.value;var r=Bounds((RectTransform)slider.transform);slider.OnPointerDown(new PointerEventData(EventSystem.current){button=PointerEventData.InputButton.Left,position=new Vector2(r.xMin+r.width*.27f,r.center.y)});Check(Mathf.Abs(slider.value-old)>.05f&&Mathf.Abs(slider.value-SoundService.BusVolume(i))<.01f,"actual slider hit changes audio bus "+i);slider.value=old;}yield return Capture("07-audio");Click("返回设置");yield return Stable("settings");
  foreach(var entry in new[]{new[]{"玩法帮助","help","02-help"},new[]{"关于游戏","about","03-about"}}){Click(entry[0]);yield return Stable(entry[1]);yield return Capture(entry[2]);Click("返回设置");yield return Stable("settings");}
  Click("完成");yield return new WaitForSecondsRealtime(.2f);Check(app.QASettingsSheet==null,"existing close fade removes menu modal");
  yield return LoadBattle();int generation=app.Session.Generation,shots=app.Session.Shots;Call("Pause");yield return Stable("pause");Check(app.Session.IsPaused,"pause page freezes game");yield return Capture("04-pause");
  Click("玩法帮助");yield return Stable("help");Check(app.Session.IsPaused,"help from pause retains pause");Click("返回设置");yield return Stable("pause");
  foreach(var pair in new[]{new[]{"重新开始","restart-confirm","05-restart"},new[]{"返回首页","home-confirm","06-return-home"}}){Click(pair[0]);yield return Stable(pair[1]);yield return Capture(pair[2]);Click("继续本局");yield return new WaitForSecondsRealtime(.25f);Check(!app.Session.IsPaused&&app.Session.Generation==generation&&app.Session.Shots==shots,"continue cancels "+pair[1]+" without restarting or consuming steps");Call("Pause");yield return Stable("pause");}
  Click("继续游戏");yield return new WaitForSecondsRealtime(.25f);Check(!app.Session.IsPaused,"continue game reuses close tween and resumes after fade");
  Call("Pause");yield return Stable("pause");Click("重新开始");yield return Stable("restart-confirm");Click("确认重新开始");yield return app.QAWaitForLevel();float wait=Time.realtimeSinceStartup+15;while(Time.realtimeSinceStartup<wait&&(app.QAGoalEntrance!=null||app.AlphabetIntroActive||app.Session.IsPaused)){app.QAAdvanceAlphabetIntro();app.QABattleTour?.AdvanceForQA();yield return null;}Check(app.Session.Generation!=generation&&app.Session.LevelIndex==20,"confirm restart restarts same actual campaign stage");
  Call("Pause");yield return Stable("pause");Click("返回首页");yield return Stable("home-confirm");Click("确认返回首页");yield return new WaitForSecondsRealtime(.35f);Check(app.CurrentPage=="home"&&!app.Session.IsPlaying&&app.QASettingsSheet==null,"confirm home ends round and restores actual home");
  // Dynamic safe-area change must reflow an already open sheet, without restarting its flow.
  Call("Settings");yield return Stable("settings");app.QASetSafeArea(new Rect(0,45,Screen.width,Screen.height-125));yield return new WaitForSecondsRealtime(.2f);yield return Stable("settings");yield return Capture("08-safe-area");WeChatLayoutMetrics.Set(Screen.width,Screen.height,new Rect(0,45,Screen.width,Screen.height-125),new Rect(Screen.width-180,45,165,60));yield return Stable("settings");Rect blocked;WeChatLayoutMetrics.TryGetBlockedRect(app.MenuAvoidanceViewport,out blocked);Check(!Bounds((RectTransform)app.QASettingsSheet.transform).Overlaps(blocked),"open sheet clears native WeChat system band");yield return Capture("09-wechat-capsule");WeChatLayoutMetrics.Reset();Click("完成");yield return new WaitForSecondsRealtime(.2f);Check(app.QASettingsSheet==null,"finish control closes adapted sheet");
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures+errors==0?0:3);
 }
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
```csharp
BindingFlags flags=BindingFlags.Instance|BindingFlags.NonPublic;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
