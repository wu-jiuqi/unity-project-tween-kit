# IngotFxQA · IngotFxQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/IngotFxQA.cs`
文件 SHA-256：`cafa99c673fed7bb197ced962c3cbf9e8e24770e07d2935df600a789754ef519`
[原始源码快照](../source/Assets/Scripts/IngotFxQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L14

方法 SHA-256：`13a27882af4529ccf9111b3d1d0c0388662c34c2ebeda044bfe9d3f57d8fcc63`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.IngotFxQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/IngotFxQA.cs:22` · `IngotFxQA.Start` → `Capture("ingot-"+a+"-"+b)`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,image);File.WriteAllBytes(dir+"/"+name+".png",image.EncodeToPNG());Destroy(image);rt.Release();Destroy(rt);}
```

### Start · L15

方法 SHA-256：`2378a227007ed66403a041322e44f73a6e0fb74c0050eafc052283298e5c2d0a`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.IngotFxQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();bool preview=args.Contains("--ingot-fx-preview");dir=args.Contains("--qa-out")?args[Array.IndexOf(args,"--qa-out")+1]:Application.temporaryCachePath+"/IngotFX";Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Exception||t==LogType.Error){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();g=app.Session;Physics2D.simulationMode=SimulationMode2D.Script;
 app.QAStartLevel(20);yield return app.QAWaitForLevel();for(int i=0;i<100;i++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return null;if(i>10&&!app.AlphabetIntroActive&&!g.IsPaused)break;}
 foreach(var key in IngotSpecialFx.Keys)for(int f=0;f<16;f++)Check(IngotSpecialFx.Frame(key,f)!=null,"dedicated frame "+key+"/"+f);
 if(preview){while(true)foreach(int a in kinds)foreach(int b in new[]{0,6,5,1,2,3}){Fixture(a,b,1);for(int n=0;n<105;n++){g.QAThirdStep(1f/60);yield return new WaitForSecondsRealtime(1f/60);}}}
 foreach(int a in kinds)foreach(int b in new[]{0,6,5,1,2,3}){
  Fixture(a,b,0);string expected=Snapshot();yield return null;
  foreach(int flags in b==0?new[]{1}:new[]{1,2,3}){Fixture(a,b,flags);Check(Snapshot()==expected,"unchanged damage score and conversions "+a+"+"+b+" flags="+flags);var gold=g.GetComponentsInChildren<IngotSpecialFx>();Check(gold.Length>0,"dedicated effect routed "+a+"+"+b+" flags="+flags);if(b!=0)Check(gold.Any(f=>f.Key=="merge"),"gold combination fusion "+a+"+"+b+" flags="+flags);if(a==3&&b!=0&&b!=3||b==3&&a!=3)Check(g.SevenConversions>0,"flower copies retain gold release "+a+"+"+b);
   if(flags==1&&(b==0||a==2&&b==2||a==3&&b==2||a==1&&b==2)){yield return new WaitForSecondsRealtime(.22f);yield return Capture("ingot-"+a+"-"+b);}yield return null;
  }
 }
 g.QAThirdFixture();SevenWindup.Play(g,g.transform,2,1,Vector2.zero,6,3,Vector2.right,()=>{},true,true);Check(g.GetComponentsInChildren<SevenWindup>().SelectMany(w=>w.GetComponentsInChildren<SpriteRenderer>()).All(r=>r.sprite.texture==OpeningThirdArt.Frame("ingot",0).texture),"windup portraits preserve both ingot types");yield return new WaitForSecondsRealtime(.85f);Check(IngotSpecialFx.Alive(g)==0,"dedicated effects complete without blocking refill");File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
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
GameSession g;
```
```csharp
AppUI app;
```
```csharp
static readonly int[] kinds={6,5,1,2,3};
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
