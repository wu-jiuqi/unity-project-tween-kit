# OptimizationV76QA · OptimizationV76QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/OptimizationV76QA.cs`
文件 SHA-256：`7d0926799ff1d8516da2cba4ee520a592505c72b4006b4954c2f30228c2acc2d`
[原始源码快照](../source/Assets/Scripts/OptimizationV76QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Endless · L13

方法 SHA-256：`e6323a3ac013241abf27fc6ebfebf2680384acc7f8ae265b03882779519cfcf1`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OptimizationV76QA.Endless()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OptimizationV76QA.cs:25` · `OptimizationV76QA.Start` → `Endless()`
- `Assets/Scripts/OptimizationV76QA.cs:26` · `OptimizationV76QA.Start` → `Endless()`
- `Assets/Scripts/OptimizationV76QA.cs:30` · `OptimizationV76QA.Start` → `Endless()`

```csharp
IEnumerator Endless(){try{while(true)yield return null;}finally{disposed++;}}
```

### Broken · L14

方法 SHA-256：`1b4a5918746326309fcd2a204ad551193b8f4ecea1bda0b3b330027cd2829aa4`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OptimizationV76QA.Broken()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OptimizationV76QA.cs:15` · `OptimizationV76QA.Nested` → `Broken()`
- `Assets/Scripts/OptimizationV76QA.cs:29` · `OptimizationV76QA.Start` → `Broken()`

```csharp
IEnumerator Broken(){yield return null;throw new InvalidOperationException("injected missing content");}
```

### Nested · L15

方法 SHA-256：`e8afb8c4026bbb8041c30a20326c58c993444494388e1f9f26f2f5ebd95f9ca4`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OptimizationV76QA.Nested()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OptimizationV76QA.cs:24` · `OptimizationV76QA.Start` → `Nested()`

```csharp
IEnumerator Nested(){yield return Broken();}
```

### FaultAtInitialization · L16

方法 SHA-256：`8c063b4a93a9bf0d4a713a68e8a991501915195de72c4b41c837e42d95a06fee`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OptimizationV76QA.FaultAtInitialization()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OptimizationV76QA.cs:31` · `OptimizationV76QA.Start` → `FaultAtInitialization()`

```csharp
IEnumerator FaultAtInitialization(){Action handler=null;handler=()=>{app.Session.StateChanged-=handler;throw new InvalidOperationException("injected board initialization failure");};app.Session.StateChanged+=handler;yield break;}
```

### CancelNext · L17

方法 SHA-256：`0d755e9f8e75280e24122ba965ded4cb00225fe4ce81f2e2e3fa358b293536f4`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OptimizationV76QA.CancelNext(BounceGarden.ContentLoadOperation)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OptimizationV76QA.cs:26` · `OptimizationV76QA.Start` → `CancelNext(op)`

```csharp
IEnumerator CancelNext(ContentLoadOperation operation){yield return null;operation.Cancel();}
```

### Capture · L19

方法 SHA-256：`84c0765f5adb16cd3f0e2501dd4867c0e0b428902261a29e51aa2919b2efdfb7`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OptimizationV76QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OptimizationV76QA.cs:29` · `OptimizationV76QA.Start` → `Capture("loading-failure")`
- `Assets/Scripts/OptimizationV76QA.cs:34` · `OptimizationV76QA.Start` → `Capture("home")`
- `Assets/Scripts/OptimizationV76QA.cs:40` · `OptimizationV76QA.Start` → `Capture("battle")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,image);File.WriteAllBytes(dir+"/"+name+".png",image.EncodeToPNG());Destroy(image);rt.Release();Destroy(rt);}
```

### WaitBattle · L20

方法 SHA-256：`ab9922f3de28f87ddeae96d30a99ca619e75a3e704040b51708931485a9597fc`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OptimizationV76QA.WaitBattle()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OptimizationV76QA.cs:39` · `OptimizationV76QA.Start` → `WaitBattle()`

```csharp
IEnumerator WaitBattle(){float deadline=Time.realtimeSinceStartup+25;while(Time.realtimeSinceStartup<deadline){if(app.AlphabetIntroActive)app.QAAdvanceAlphabetIntro();if(app.Session.IsPlaying&&!app.Session.IsPaused&&!app.StreakRewardActive)yield break;yield return null;}Check(false,"battle entered before deadline");}
```

### Start · L21

方法 SHA-256：`8720abb89c09d1d7596f204aad9c967c64d5a906276bc2f2734ac80349927b30`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OptimizationV76QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(4);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");SoundService.SetMuted(true);
  var op=new ContentLoadOperation();yield return op.Run(Nested());Check(op.Failure!=null&&!op.Succeeded,"nested initialization exception caught");
  op=new ContentLoadOperation();yield return op.Run(Endless(),.12f);Check(op.TimedOut&&disposed==1,"deadline disposes nested request");
  op=new ContentLoadOperation();StartCoroutine(CancelNext(op));yield return op.Run(Endless());Check(op.Cancelled&&disposed==2,"cancel disposes request and releases lock");
  op=new ContentLoadOperation();yield return op.Run(Once());Check(op.Succeeded,"fresh operation succeeds after failures");
  int writes=SoundService.MixSaveCount;for(int n=0;n<100;n++)SoundService.SetBusVolume(0,n/100f);Check(SoundService.MixSaveCount==writes&&SoundService.MixPending,"100 volume changes apply without disk flush");Check(Mathf.Abs(SoundService.BusVolume(0)-.99f)<.001f,"live volume follows final slider value");SoundService.SaveMix();SoundService.SaveMix();Check(SoundService.MixSaveCount==writes+1&&!SoundService.MixPending,"release and close flush only once");SoundService.SetBusVolume(0,.42f);yield return new WaitForSecondsRealtime(.45f);Check(SoundService.MixSaveCount==writes+2,"idle debounce commits pending value");
  var original=GameContent.Level;GameContent.Level=index=>Broken();app.QAStartLevel(0);yield return new WaitForSecondsRealtime(1);Check(app.GetComponentsInChildren<TMP_Text>(true).Any(t=>t.text=="暂时未能进入关卡"),"real level entry shows recoverable failure");Check(!app.Session.IsPlaying,"failed initialization leaves no running world");yield return Capture("loading-failure");GameContent.Level=original;app.QANavigate("home");
  GameContent.Level=index=>Endless();app.QAStartLevel(0);yield return new WaitForSecondsRealtime(.2f);var back=app.GetComponentsInChildren<Button>().FirstOrDefault(b=>b.GetComponentsInChildren<TMP_Text>().Any(t=>t.text=="返回花园"));Check(back!=null,"loading exposes return action before timeout");if(back!=null)back.onClick.Invoke();yield return null;yield return null;Check(app.CurrentPage=="home"&&!app.Session.IsPlaying,"return cancels active loading safely");GameContent.Level=original;
  GameContent.Level=index=>FaultAtInitialization();app.QAStartLevel(0);yield return new WaitForSecondsRealtime(1);GameContent.Level=original;Check(app.GetComponentsInChildren<TMP_Text>(true).Any(t=>t.text=="暂时未能进入关卡"),"board initialization exception reaches recovery UI");app.QANavigate("home");
  foreach(var size in new[]{new Vector2Int(320,568),new Vector2Int(360,800),new Vector2Int(375,667),new Vector2Int(390,844),new Vector2Int(393,852),new Vector2Int(430,932),new Vector2Int(412,915),new Vector2Int(768,1024)}){
   app.QASetViewport(size);app.QANavigate("home");yield return null;Check(app.GetComponentsInChildren<TMP_Text>().Any(t=>t.text=="开始第1关"),"explicit home action "+size);Check(app.GetComponentsInChildren<TMP_Text>().Any(t=>t.text=="章节地图"),"chapter orb has label "+size);app.QANavigate("chapters");yield return new WaitForSecondsRealtime(.25f);Check(Button("当前关快捷继续")!=null,"chapter current-level shortcut "+size);if(size.x==390||size.x==320)yield return Capture("chapters-"+size.x);app.QANavigate("home");}
  app.QASetViewport(null);yield return Capture("home");
  app.Profile.ResetForTesting();app.Profile.InitializeChapterGates();for(int n=0;n<3;n++)app.Profile.CommitAlphabetWin("v76-win-"+n,n,100);for(int i=1;i<=3;i++){app.Profile.MarkBoosterIntro((StreakBooster)i);app.Profile.MarkBoosterGuide((StreakBooster)i);}
  for(int run=0;run<2;run++){
   app.QAStartLevel(3);float limit=Time.realtimeSinceStartup+20;StreakRewardPresentation reward=null;while(Time.realtimeSinceStartup<limit){if(app.AlphabetIntroActive)app.QAAdvanceAlphabetIntro();reward=app.GetComponentInChildren<StreakRewardPresentation>();if(reward!=null)break;yield return null;}
   Check(reward!=null,"reward appears run "+run);if(reward!=null){reward.OnPointerClick(null);Check(reward.Accelerated==(run==1),"first full / repeat accelerated "+run);Check(app.Session.IsPaused,"reward blocks gameplay "+run);}
   yield return WaitBattle();Check(app.Session.SelectedStreakBooster==StreakBooster.None,"reward leaves manual choice "+run);Check(app.Metrics.Current!=null,"round metrics attached "+run);
   if(run==0){yield return Capture("battle");app.QADragAim(Vector2.up);yield return new WaitForSecondsRealtime(.7f);Check(app.Session.SpawnCount==5,"five physical balls remain unchanged");Check(app.Metrics.Current.shots==1&&app.Metrics.Current.latencySamples==1,"metrics count one volley and first spawn latency");}
   app.QANavigate("home");yield return null;
  }
  Check(app.Metrics.CompletedCount>=2,"abandoned rounds retained");File.WriteAllText(dir+"/playtest.json",app.Metrics.ExportJson());
  foreach(var cue in new[]{"snow_ice_break","snow_block_shrink","snowdrift_layer","snow_clear","snow_cocoa_activate"})foreach(string suffix in new[]{"_a","_b"}){var clip=SoundService.QAClip(cue+suffix);Check(clip!=null&&clip.channels==1&&clip.length>.1f&&clip.length<.9f,"winter sound "+cue+suffix);}
  Check(errors==0,"zero runtime errors");File.WriteAllText(dir+"/result.json",$"{{\"checks\":{checks},\"failures\":{failures},\"errors\":{errors}}}");Application.Quit(failures+errors==0?0:1);
 }
```

### Once · L47

方法 SHA-256：`b9f80037c56f3f1d1f7962dc132d9a04a9596cd5087ca597fa22d961fabb0095`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OptimizationV76QA.Once()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OptimizationV76QA.cs:27` · `OptimizationV76QA.Start` → `Once()`

```csharp
IEnumerator Once(){yield return null;}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
IEnumerator CancelNext(ContentLoadOperation operation){yield return null;operation.Cancel();}
```

## 源字段与依赖

```csharp
string dir;
```
```csharp
int checks,failures,errors,disposed;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
