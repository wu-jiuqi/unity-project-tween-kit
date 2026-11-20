# StreakRewardV74QA · StreakRewardV74QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/StreakRewardV74QA.cs`
文件 SHA-256：`dd94ebedb6b09333e74e3b119828f15999911c511e27154d51d48f9ca8cf304f`
[原始源码快照](../source/Assets/Scripts/StreakRewardV74QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L8

方法 SHA-256：`90d22d36c347347a860ba4485a297f9877507c711fb722ef03b0183de4be4a5c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.StreakRewardV74QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/StreakRewardV74QA.cs:30` · `StreakRewardV74QA.Start` → `Capture("tier"+wins+"-game")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var t=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,t);File.WriteAllBytes(dir+"/"+name+".png",t.EncodeToPNG());Destroy(t);rt.Release();Destroy(rt);}
```

### Start · L9

方法 SHA-256：`9ab91804402828bf7db1e536fbdaacbada80fc43af705295dac83bba1bf3beb0`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.StreakRewardV74QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(4);app=FindFirstObjectByType<AppUI>();app.QANavigate("home");SoundService.SetMuted(true);
  foreach(int wins in new[]{0,3,5,10}){
   app.QANavigate("home");app.Profile.ResetForTesting();app.Profile.InitializeChapterGates();
   for(int n=0;n<wins;n++)app.Profile.CommitAlphabetWin("reward74-"+wins+"-"+n,n,100);
   // Avoid unrelated first-time introductions so the actual start sequence can be observed.
   for(int i=1;i<=3;i++){var k=(StreakBooster)i;app.Profile.MarkBoosterIntro(k);app.Profile.MarkBoosterGuide(k);}
   app.QAStartLevel(wins);float deadline=Time.realtimeSinceStartup+30;bool saw=false;var phases=new System.Collections.Generic.HashSet<string>();
   while(Time.realtimeSinceStartup<deadline){
    if(app.AlphabetIntroActive)app.QAAdvanceAlphabetIntro();
    var p=app.GetComponentInChildren<StreakRewardPresentation>();
    if(p!=null){saw=true;Check(app.Session.IsPaused,"animation pauses session "+wins+" "+p.Phase);Check(app.Session.SelectedStreakBooster==StreakBooster.None,"animation never selects tool "+wins);Check(!app.Session.SelectStreakBooster(StreakBooster.Pierce)&&!app.Session.TryShoot(Vector2.up),"selection and shooting blocked during reward "+wins);
     if(phases.Add(p.Phase)){if(p.Phase=="reveal")yield return new WaitForSecondsRealtime(.45f);yield return Capture("tier"+wins+"-"+p.Phase);}
    }
    if(app.Session.IsPlaying&&!app.Session.IsPaused&&!app.StreakRewardActive)break;
    yield return new WaitForSecondsRealtime(.07f);
   }
   var g=app.Session;g.QAIgnoreFocusLoss=true;Check(app.Session.IsPlaying&&!g.IsPaused,"game resumes after presentation "+wins);Check(saw==(wins>=3),"reward only shown when eligible "+wins);Check(g.SelectedStreakBooster==StreakBooster.None,"nothing selected after reward "+wins);
   if(wins>=3){Check(phases.Contains("reveal")&&phases.Contains("fly")&&phases.Contains("restore"),"all animation phases observed "+wins);int tier=wins>=10?3:wins>=5?2:1;for(int i=1;i<=tier;i++){var k=(StreakBooster)i;Check(app.Profile.StreakBonusStock(k,wins)==1,"gift retained until manual use "+k+" "+wins);}
    var kind=(StreakBooster)tier;int own=app.Profile.BoosterStock(kind);Check(g.SelectStreakBooster(kind),"manual selection works "+wins);Check(g.TryShoot(Vector2.up),"manual gift volley fires "+wins);yield return new WaitForSecondsRealtime(.3f);Check(app.Profile.StreakBonusStock(kind,wins)==0&&app.Profile.BoosterStock(kind)==own,"manual shot consumes gift first "+wins);
   }
   yield return Capture("tier"+wins+"-game");
  }
  Check(errors==0,"zero runtime errors");File.WriteAllText(dir+"/result.json",$"{{\"checks\":{checks},\"failures\":{failures},\"errors\":{errors}}}");Application.Quit(failures+errors==0?0:1);
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

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
