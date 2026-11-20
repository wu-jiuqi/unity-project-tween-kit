# JellyBalanceR2QA · JellyBalanceR2QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/JellyBalanceR2QA.cs`
文件 SHA-256：`7ec6b54fe7e6e8893ac6eae22aff13b0c1a67f0b9ec5874a68d1b5c07357b34e`
[原始源码快照](../source/Assets/Scripts/JellyBalanceR2QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L8

方法 SHA-256：`8984e384f2a90bf2f7fc197baff6ccf05e897434f6ec8585fd71dcd6bff5e0fe`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyBalanceR2QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/JellyBalanceR2QA.cs:13` · `JellyBalanceR2QA.Start` → `Capture("copper-hit-squash")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### Start · L9

方法 SHA-256：`862c2c9554303d16f9ca68399ba00a13c67f2ee7ac5b5cd72c78c7d1c9699a5f`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyBalanceR2QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s);}};yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();g=app.Session;
  Check(OpeningCampaign.Validate()>1000,"all stage definitions validate");for(int i=0;i<45;i++)Check(OpeningCampaign.Stage(i).moves==moves[i],"agreed test moves level "+(i+1));
  Check(ScoreRules.SpareShot==3000,"remaining step reward is 3000");foreach(int n in new[]{0,1,5,17}){g.StartLevel(0);g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);g.QAFinaleGoal(n,1800);int score=g.FinalizeScore(true);Check(score==1800+n*3000&&g.CompletionBonus==n*3000,"full remaining step reward "+n);Check(g.FinalizeScore(true)==score,"remaining bonus credits once "+n);}yield return null;
  app.QAStartLevel(30);yield return app.QAWaitForLevel();for(int i=0;i<100;i++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return null;if(i>10&&!app.AlphabetIntroActive&&!g.IsPaused)break;}g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);var coin=g.QACopperViews().First();Vector3 anchor=coin.transform.position;var collider=coin.GetComponent<BoxCollider2D>();Vector2 box=collider.size;
  g.ApplyChainImpact(coin.Brick,1,Vector2.up,anchor,true);coin.Tick(.04f);Check(coin.ImpactAnimating&&coin.ImpactStretch.x>1&&coin.ImpactStretch.y<1,"coin impact squashes along vertical entry");Check((coin.ImpactStretch-JellyMotion.HitStretch(.04f,Vector2.up)).sqrMagnitude<.00001f,"coin shares exact cat impact curve");yield return Capture("copper-hit-squash");g.ApplyChainImpact(coin.Brick,1,Vector2.right,anchor,true);coin.Tick(.04f);Check(coin.ImpactStretch.x<1&&coin.ImpactStretch.y>1,"repeat side hit restarts with horizontal squash");Vector2 frozen=coin.ImpactStretch;g.SetPaused(true);g.QAThirdStep(.2f);Check(coin.ImpactStretch==frozen,"pause freezes copper tween");g.SetPaused(false);coin.Tick(.5f);Check(!coin.ImpactAnimating&&coin.ImpactStretch==Vector2.one,"coin returns exactly to rest");Check(coin.transform.position==anchor&&collider.size==box&&coin.transform.localScale==Vector3.one,"coin collider and grid anchor remain fixed");
  var camera=Camera.main;var juice=camera.GetComponent<CameraJuice>();juice.ResetImpact();Check(FeedbackProfile.Active.specialShakeHeight==.0032f&&FeedbackProfile.Active.burstShakeHeight==.0022f,"serialized shake strength reduced");juice.ImpactScreen(FeedbackProfile.Active.specialShakeHeight,FeedbackProfile.Active.specialShakeDuration);yield return new WaitForEndOfFrame();Check(juice.WorldOffset.magnitude<.0001f,"jelly starts smoothly without instant kick");float start=Time.realtimeSinceStartup,peak=0;int signChanges=0,last=0;while(Time.realtimeSinceStartup-start<.42f){yield return new WaitForEndOfFrame();peak=Mathf.Max(peak,juice.WorldOffset.magnitude);if(Mathf.Abs(juice.WorldOffset.y)>.0001f){int sign=juice.WorldOffset.y>0?1:-1;if(last!=0&&last!=sign)signChanges++;last=sign;}}float fraction=peak/(camera.orthographicSize*2);Check(fraction>.0003f&&fraction<.002f,"soft viewport scaled jelly peak "+fraction);Check(signChanges<=2,"low frequency rebound avoids rapid shake");Check(!juice.Active&&juice.WorldOffset.magnitude<.0001f,"camera returns exactly to rest");juice.ImpactScreen(1,1);for(int i=0;i<100;i++)juice.ImpactScreen(1,1);yield return new WaitForSecondsRealtime(.05f);Check(juice.WorldOffset.magnitude/(camera.orthographicSize*2)<.0042f,"extreme simultaneous impacts remain capped");g.SetPaused(true);yield return new WaitForEndOfFrame();Check(!juice.Active&&juice.WorldOffset.magnitude<.0001f,"pause clears camera recoil");g.SetPaused(false);
  for(int i=0;i<3;i++)Check((JellyMotion.FloatScale(.2f,i)-JellyMotion.FloatScale(.35f,i)).magnitude>.01f,"shared bomb star jelly phase "+i);
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
```csharp
GameSession g;
```
```csharp
AppUI app;
```
```csharp
static readonly int[] moves={8,10,10,12,10,14,12,12,12,12,10,12,14,14,16,16,12,12,14,16,16,14,16,12,14,14,14,14,18,14,12,12,14,12,12,14,16,14,14,14,16,14,14,16,18};
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
