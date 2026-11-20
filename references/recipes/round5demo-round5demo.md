# Round5Demo · Round5Demo

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Round5Demo.cs`
文件 SHA-256：`f7e541d92d650c469633922781d09af3302fb884d13d7b1f6df231177f606d0d`
[原始源码快照](../source/Assets/Scripts/Round5Demo.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L8

方法 SHA-256：`ec2bdc114baeb850a5aa59efeee8240fb3732ed72869c899570b87d089d30a8c`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Round5Demo.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--demo-out")+1];yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();recorder=FindFirstObjectByType<WindowsDemoRecorder>();recorder.StartEvidence();
  Note("Automated aim and UI-button invocations, not human mouse input. Real-time final framebuffer and AudioListener mix. No forced outcomes. QA memory-only profile.");
  yield return new WaitForSecondsRealtime(1.2f);app.QAStartLevel(0);yield return new WaitForSecondsRealtime(1.2f);app.Session.Ground.SetDebug(true);yield return new WaitForSecondsRealtime(2);app.Session.Ground.SetDebug(false);
  int volleys=0;while(app.Session.IsPlaying&&volleys<20){if(app.Session.Shots>0&&app.Session.PendingBurstBalls==0){var aim=AimAt(Lowest());app.Session.SetAim(aim);yield return new WaitForSecondsRealtime(.35f);app.QADragAim(aim);volleys++;}yield return new WaitForSecondsRealtime(.85f);}
  bool won=app.Session.RemainingTargets==0;Note("Level1 natural win="+won+" volleys="+volleys+" hits="+app.Session.HitCount+" breaks="+app.Session.BreakCount);
  yield return new WaitForSecondsRealtime(6);if(won&&app.RewardReady){app.CollectReward();yield return new WaitForSecondsRealtime(2);}Note("Wallet="+app.Profile.Coins+" badges="+app.Profile.Badges);
  app.QAStartLevel(2);yield return new WaitForSecondsRealtime(1.2f);app.Session.Ground.SetDebug(true);yield return new WaitForSecondsRealtime(2);app.Session.Ground.SetDebug(false);
  for(int i=0;i<5&&app.Session.IsPlaying;i++){var aim=AimAt(Lowest());app.Session.SetAim(aim);yield return new WaitForSecondsRealtime(.7f);app.QADragAim(aim);yield return new WaitForSecondsRealtime(1);}
  yield return new WaitForSecondsRealtime(2);Invoke("Button =");yield return new WaitForSecondsRealtime(1);Invoke("Button Resume");yield return new WaitForSecondsRealtime(1);Invoke("Button =");yield return new WaitForSecondsRealtime(.5f);Invoke("Button Restart");yield return new WaitForSecondsRealtime(1.2f);Note("Round27 restart shots="+app.Session.Shots+" targets="+app.Session.RemainingTargets);
  Invoke("Button =");yield return new WaitForSecondsRealtime(.5f);Invoke("Button Home");yield return new WaitForSecondsRealtime(1.5f);recorder.StopEvidence();Note("Finished");yield return null;Application.Quit(won?0:3);
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
WindowsDemoRecorder recorder;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
