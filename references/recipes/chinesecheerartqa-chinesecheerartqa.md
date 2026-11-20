# ChineseCheerArtQA · ChineseCheerArtQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/ChineseCheerArtQA.cs`
文件 SHA-256：`0dbd13bafa25e416e504c5a0a7a66d44b37f8431eb44e1ff3cffb6e2764c662c`
[原始源码快照](../source/Assets/Scripts/ChineseCheerArtQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L13

方法 SHA-256：`af37e35f4d3090d2e22a1865c4da6334252ffddf8d8504f89174b4da82240907`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChineseCheerArtQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/ChineseCheerArtQA.cs:23` · `ChineseCheerArtQA.Start` → `Capture("chain-tier-"+tier)`
- `Assets/Scripts/ChineseCheerArtQA.cs:24` · `ChineseCheerArtQA.Start` → `Capture("motion-"+tier+"-"+frame.ToString("D2"))`
- `Assets/Scripts/ChineseCheerArtQA.cs:28` · `ChineseCheerArtQA.Start` → `Capture("feedback-tier-"+tier)`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(540,1170,24);rt.Create();var tex=new Texture2D(540,1170,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,tex);File.WriteAllBytes(dir+"/"+name+".png",tex.EncodeToPNG());Destroy(tex);rt.Release();Destroy(rt);}
```

### Start · L14

方法 SHA-256：`1c2afb1bd8086e37913c83629320709594f6e9e9689cef36d359f6b24f4b4f1f`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChineseCheerArtQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(8);app=FindFirstObjectByType<AppUI>();app.QASetViewport(new Vector2Int(540,1170));app.QAStartLevel(0);yield return app.QAWaitForLevel();for(int i=0;i<40&&app.AlphabetIntroActive;i++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.15f);}app.Session.QAIgnoreFocusLoss=true;
  var view=FindFirstObjectByType<ChainRatingView>();var record=typeof(GameSession).GetMethod("RecordChainRatingBreak",BindingFlags.Instance|BindingFlags.NonPublic);int breaks=0;
  for(int tier=0;tier<4;tier++){
   var sprite=ChineseArt.CheerArt(tier);Check(Mathf.Abs(sprite.rect.width/sprite.rect.height-3f)<.01f,"imported artwork retains source 3:1 aspect "+tier);Check(sprite!=null&&sprite.texture.width>=1000,"full resolution Chinese artwork "+tier);Check(sprite.texture.GetPixel(0,0).a<.01f&&sprite.texture.GetPixel(sprite.texture.width-1,sprite.texture.height-1).a<.01f,"transparent sprite corners "+tier);
   while(breaks<InGameFeedbackState.Thresholds[tier]){record.Invoke(app.Session,null);breaks++;}yield return new WaitForSecondsRealtime(.08f);
   Check(view.VisibleTier==tier&&view.IsVisible&&view.VisibleArt==sprite,"actual chain threshold displays PNG sprite "+tier);
   var image=view.GetComponentsInChildren<Image>(true).First(i=>i.sprite==sprite);Check(image.GetComponent<TMPro.TMP_Text>()==null&&!image.raycastTarget&&image.preserveAspect,"PNG graphic preserves aspect and aim input "+tier);
   Check(Mathf.Approximately(image.transform.lossyScale.x,image.transform.lossyScale.y),"main rating uses uniform world scale "+tier);float scale=image.transform.localScale.x;yield return new WaitForSecondsRealtime(.18f);Check(Mathf.Abs(scale-image.transform.localScale.x)>.01f,"jelly pop animation changes scale "+tier);
   app.Session.SetPaused(true);float age=view.Age;yield return Capture("chain-tier-"+tier);yield return new WaitForSecondsRealtime(.12f);Check(view.Age==age,"pause freezes artwork animation "+tier);app.Session.SetPaused(false);
   for(int frame=0;frame<10;frame++){yield return Capture("motion-"+tier+"-"+frame.ToString("D2"));yield return new WaitForSecondsRealtime(.08f);}yield return new WaitForSecondsRealtime(1);Check(!view.IsVisible,"artwork fades and hides "+tier);
  }
  var feedback=FindFirstObjectByType<InGameFeedbackView>();
  for(int tier=0;tier<4;tier++){
   feedback.State.Advance(3);for(int i=0;i<InGameFeedbackState.Thresholds[tier];i++)feedback.State.Break();yield return new WaitForSecondsRealtime(.3f);Check(feedback.VisibleTier==tier&&feedback.VisibleArt==ChineseArt.CheerArt(tier),"companion feedback path displays PNG "+tier);var cheer=feedback.GetComponentsInChildren<Image>(true).First(i=>i.sprite==ChineseArt.CheerArt(tier));Check(Mathf.Approximately(cheer.transform.lossyScale.x,cheer.transform.lossyScale.y),"companion cheer keeps uniform scale during pop "+tier);yield return Capture("feedback-tier-"+tier);yield return new WaitForSecondsRealtime(1.8f);
  }
  Check(errors==0,"zero runtime errors");File.WriteAllText(dir+"/result.json",$"{{\"checks\":{checks},\"failures\":{failures},\"errors\":{errors}}}");Application.Quit(failures+errors==0?0:1);
 }
```

## 对象层级和素材入口

- L18：`ChineseArt.CheerArt(tier)`
- L28：`ChineseArt.CheerArt(tier)`

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
