# LifecycleQA · LifecycleQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LifecycleQA.cs`
文件 SHA-256：`5eb5f51c622ca3fd851568af960dadfcf9e5e26583c75e3258bf61bf4ef7c9b0`
[原始源码快照](../source/Assets/Scripts/LifecycleQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L12

方法 SHA-256：`6d27ac852a286fad1645f6fbed586f908e36d2199428ecee9f66c9859b9f66c7`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LifecycleQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  string directory=Arg("--qa-out"),stage=Arg("--lifecycle"),scenario=Arg("--case");Directory.CreateDirectory(directory);
  if(LocalProfile.PersistentTestKey==null)throw new Exception("Refusing lifecycle test without isolated persistent key");
  yield return new WaitForSecondsRealtime(.5f);var app=FindFirstObjectByType<AppUI>();var profile=app.Profile;
  if(stage=="verify"){
   var before=JsonUtility.FromJson<Snapshot>(File.ReadAllText(Path.Combine(directory,"checkpoint.json")));
   int expected=scenario=="before"?1580:scenario=="complete"?1620:1600;
   bool pass=before.passed&&profile.StorageHealthy&&profile.Coins==expected&&profile.Coins==before.coins&&profile.Badges==(scenario=="before"?0:1)&&profile.UnlockedLevel==(scenario=="before"?0:1)&&profile.CommittedTransactionCount==(scenario=="before"?0:scenario=="complete"?2:1);
   if(scenario!="before")pass&=!profile.CommitBase(before.key,0);
   int coins=profile.Coins;LocalMockServices.ResolveAd(profile,before.key,MockOutcome.Cancelled);LocalMockServices.ResolveAd(profile,before.key,MockOutcome.Failed);pass&=profile.Coins==coins;
   if(scenario=="complete")pass&=LocalMockServices.ResolveAd(profile,before.key,MockOutcome.Completed)==MockOutcome.Completed&&profile.Coins==coins;
   yield return Capture(Path.Combine(directory,"restart.png"));Write(directory,"verified.json",profile,before.key,pass);Application.Quit(pass?0:4);yield break;
  }
  if(profile.Coins!=1580||profile.CommittedTransactionCount!=0)throw new Exception("Lifecycle test key is not fresh");
  app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.8f);
  float deadline=Time.realtimeSinceStartup+80;
  while(app.Session.IsPlaying&&Time.realtimeSinceStartup<deadline){if(app.Session.PendingBurstBalls==0&&app.Session.Shots>0){BrickBody lowest=null;foreach(var brick in FindObjectsByType<BrickBody>(FindObjectsSortMode.None))if(brick.Alive&&(lowest==null||brick.transform.position.y<lowest.transform.position.y))lowest=brick;if(lowest!=null)app.QADragAim(((Vector2)lowest.transform.position-app.Session.LauncherPosition).normalized);}yield return new WaitForSecondsRealtime(.7f);}
  if(app.Session.RemainingTargets!=0)throw new Exception("Lifecycle scenario did not naturally win");
  while(!app.RewardReady)yield return null;string key=app.QAResultKey;
  if(scenario!="before"){
   if(scenario=="cancel"||scenario=="fail"||scenario=="no-callback"||scenario=="complete"){
    Find("Button x2 Coins").onClick.Invoke();
    if(scenario=="cancel")Find("Button Cancel").onClick.Invoke();
    if(scenario=="fail")Find("Button Fail").onClick.Invoke();
    if(scenario=="complete"){var button=Find("Button Complete  +20");button.onClick.Invoke();button.onClick.Invoke();}
   }else app.CollectReward();
   if(scenario=="flight")yield return new WaitForSecondsRealtime(.7f);
   if(scenario=="settled")yield return new WaitForSecondsRealtime(2);
  }
  int expectedCoins=scenario=="before"?1580:scenario=="complete"?1620:1600;
  bool valid=profile.StorageHealthy&&profile.Coins==expectedCoins&&profile.Badges==(scenario=="before"?0:1)&&profile.CommittedTransactionCount==(scenario=="before"?0:scenario=="complete"?2:1);
  yield return Capture(Path.Combine(directory,"checkpoint.png"));Write(directory,"checkpoint.json",profile,key,valid);
  if(!valid){Debug.LogError("Lifecycle checkpoint failed expected wallet/storage-health assertions");Application.Quit(5);yield break;}
  // Host terminates this process after the checkpoint; no graceful quit callback.
  while(true)yield return new WaitForSecondsRealtime(.25f);
 }
```

### Capture · L50

方法 SHA-256：`5e3b87bc1e215b10ca80e02668fc2c4234a61e7c4848956d1e0e2d3e1b72b436`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LifecycleQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LifecycleQA.cs:23` · `LifecycleQA.Start` → `Capture(Path.Combine(directory,"restart.png"))`
- `Assets/Scripts/LifecycleQA.cs:43` · `LifecycleQA.Start` → `Capture(Path.Combine(directory,"checkpoint.png"))`

```csharp
static IEnumerator Capture(string path){yield return new WaitForEndOfFrame();var target=new RenderTexture(Screen.width,Screen.height,24);var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,target,image);File.WriteAllBytes(path,image.EncodeToPNG());target.Release();Destroy(target);Destroy(image);}
```

## 对象层级和素材入口

- L33：`Find("Button x2 Coins")`
- L34：`Find("Button Cancel")`
- L35：`Find("Button Fail")`
- L36：`Find("Button Complete  +20")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
