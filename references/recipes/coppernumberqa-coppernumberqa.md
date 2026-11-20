# CopperNumberQA · CopperNumberQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/CopperNumberQA.cs`
文件 SHA-256：`174f65cc4d04a362ddb49e755584c7749335d5f6a1668c8ab877be35d642cfd5`
[原始源码快照](../source/Assets/Scripts/CopperNumberQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L7

方法 SHA-256：`8984e384f2a90bf2f7fc197baff6ccf05e897434f6ec8585fd71dcd6bff5e0fe`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CopperNumberQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/CopperNumberQA.cs:12` · `CopperNumberQA.Start` → `Capture("level31-readable-copper")`
- `Assets/Scripts/CopperNumberQA.cs:13` · `CopperNumberQA.Start` → `Capture("intro-readable-copper")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### Start · L8

方法 SHA-256：`7c7f526524379b220e83c3091260e1d8fb943930cd492b6c86b6704b0e7bad8d`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CopperNumberQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};
  yield return new WaitForSecondsRealtime(3);var app=FindFirstObjectByType<AppUI>();app.QAStartLevel(30);yield return app.QAWaitForLevel();for(int n=0;n<100;n++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.025f);if(n>8&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}
  var g=app.Session;g.QAIgnoreFocusLoss=true;g.SetPaused(false);var coins=g.QACopperViews();Check(coins.Length>=5,"real level has copper bricks");int index=0;Check(coins.All(c=>c.Brick.CurrentHealth==25),"all authored copper starts at 25 HP");for(int hp=1;hp<=25;hp++)Check(OpeningCopperArt.StateIndex(hp)==(25-hp)/5,"state changes every five damage at HP "+hp);
  foreach(var view in coins.Take(5)){for(int damage=0;damage<index*5;damage++)g.SevenHit(view.Brick,1);view.Tick(.2f);var text=view.GetComponentsInChildren<TMP_Text>().First(t=>t.name=="Remaining copper HP");Check(text.color==OpeningCopperArt.NumberColor&&text.fontSize>=4.8f&&text.fontStyle.HasFlag(FontStyles.Bold),"dark bold HP for state "+view.Brick.CurrentHealth);Check(text.text==(25-index*5).ToString()&&text.outlineColor.r>.9f,"correct live HP and cream outline "+view.Brick.CurrentHealth);index++;}
  yield return new WaitForSecondsRealtime(.25f);yield return Capture("level31-readable-copper");
  app.QAGardenLesson(30);yield return new WaitForSecondsRealtime(1.1f);var intro=app.QAAlphabetIntro;var label=intro.GetComponentInChildren<OpeningRuleDemo>().GetComponentsInChildren<TMP_Text>().First(t=>int.TryParse(t.text,out _));Check(label.color.r<.4f&&label.color.g<.25f&&label.outlineColor.r>.9f,"intro number uses matching high contrast style");yield return Capture("intro-readable-copper");
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
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

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
