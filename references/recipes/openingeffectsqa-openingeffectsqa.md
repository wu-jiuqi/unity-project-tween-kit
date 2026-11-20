# OpeningEffectsQA · OpeningEffectsQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/OpeningEffectsQA.cs`
文件 SHA-256：`dc81803229d39ffd3b2d8dcc7345bf9ff59eab4ed442d00edf78368f6ecdbd92`
[原始源码快照](../source/Assets/Scripts/OpeningEffectsQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L23

方法 SHA-256：`8984e384f2a90bf2f7fc197baff6ccf05e897434f6ec8585fd71dcd6bff5e0fe`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OpeningEffectsQA.Capture(string)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### WaitPage · L24

方法 SHA-256：`f2e6ce89d1be6a41bd7ef1a40bdbc597b8620bedaff4640c25c69396198c33e5`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OpeningEffectsQA.WaitPage(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OpeningEffectsQA.cs:60` · `OpeningEffectsQA.Start` → `WaitPage(501+index)`

```csharp
IEnumerator WaitPage(int id){float until=Time.realtimeSinceStartup+8;while(Time.realtimeSinceStartup<until&&(app.QAAlphabetIntro==null||app.QAAlphabetIntro.PageId!=id))yield return null;}
```

### Dismiss · L25

方法 SHA-256：`8c527b8c93bb9061eddf2e650938d4c7ec24d3d38ce2e3fe8c8324debf05f499`
时间来源：Time.realtimeSinceStartup, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OpeningEffectsQA.Dismiss()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OpeningEffectsQA.cs:58` · `OpeningEffectsQA.Start` → `Dismiss()`
- `Assets/Scripts/OpeningEffectsQA.cs:60` · `OpeningEffectsQA.Start` → `Dismiss()`

```csharp
IEnumerator Dismiss(){float until=Time.realtimeSinceStartup+5;while(Time.realtimeSinceStartup<until&&app.AlphabetIntroActive){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.03f);}}
```

### Start · L26

方法 SHA-256：`750c6b7fd097f25b42a297b3fd5dbb8e7c383ac454ea00c37d92d15ab4bac50c`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OpeningEffectsQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();g=app.Session;Physics2D.simulationMode=SimulationMode2D.Script;
  Reset();var y=Yarn();g.ApplyChainImpact(y.Brick,99,Vector2.up,y.Brick.transform.position,true);Check(y.Layers==2&&y.Brick.Alive,"ordinary ball cannot damage yarn");
  foreach(int kind in new[]{2,6,5,1}){Reset();y=Yarn();Cast(kind,kind==2?3:kind==5?4:0,kind==5?0:4);Check(y.Layers==1,"direct range hits isolated yarn once kind="+kind);}
  Reset();y=Yarn();Brick(3,4);Brick(5,4);Cast(6,0,4);Check(y.Layers==1&&g.OpeningYarnLayersRemoved==1,"laser direct hit and two eliminated neighbors share one attack");
  Reset();y=Yarn();Brick(3,4);Brick(5,4);Brick(4,3);Brick(4,5);Cast(2,3,3);Check(y.Layers==1,"bomb covers multiple orthogonal neighbors and yarn once");
  Reset();y=Yarn();var diagonal=Brick(5,5);g.SevenHit(diagonal);Check(y.Layers==2,"ordinary diagonal removal does not count");var tough=Brick(3,4,1,30);g.SevenHit(tough);Check(y.Layers==2&&tough.Alive,"damage without an elimination outside direct area does not count");
  Reset();y=Yarn();var left=Brick(3,4,1);var right=Brick(5,4,1);Cast(3,0,8,1);Check(!left.Alive&&!right.Alive&&y.Layers==1&&g.OpeningYarnLayersRemoved==1,"single flower uses both original neighbor positions in one cast");Check(g.Collected[0]==0,"partial yarn has no collection credit");
  Brick(4,3,2);Cast(3,8,8,2);Check(!y.Brick.Alive&&g.OpeningYarnLayersRemoved==2,"second independent flower cast completes yarn");Tick(130);Check(g.Collected[0]==1,"unravel and flight credit once after independent flowers");
  Reset();y=Yarn();tough=Brick(3,4,1,30);Cast(3,0,8,1);Check(tough.Alive&&y.Layers==2,"flower nonlethal absorption does not fake an elimination");
  Reset();y=Yarn();var source=Brick(4,3,1);var partner=g.QASevenTool(6,1,8,1);Cast(3,0,8,1);Check(!source.Alive&&g.SevenConversions==1&&y.Layers==1,"flower conversion notifies source cell even when copied row misses yarn");
  Reset();y=Yarn();g.QASevenTool(2,3,4,0);Cast(6,0,4);Check(!y.Brick.Alive&&g.OpeningYarnLayersRemoved==2,"nested bomb and parent laser are distinct attacks in same frame");
  Reset();y=Yarn();g.QASevenTool(2,2,4,0);Cast(6,1,4);Check(y.Layers==1,"laser and bomb combination is a single combined range attack");
  Reset();y=Yarn();var armored=g.QASevenBrick(5,4,20,2,1,2);g.QASevenTool(3,1,8,0);Cast(3,0,8,1);Check(!y.Brick.Alive&&g.OpeningYarnLayersRemoved==2&&!armored.ShellIntact,"double flower strips two layers consistently with armor");
  Reset();y=Yarn();y.Brick.GetComponent<ChainGridPiece>().RefillProtected=true;g.QAEffectsArea(1,4,4);Check(y.Layers==2,"arrival protection prevents premature range damage");
  foreach(var booster in new[]{StreakBooster.Bomb,StreakBooster.Super}){Reset();y=Yarn();Brick(3,4);Brick(5,4);Brick(4,3);Brick(4,5);g.QAEffectsBooster(booster,4,4);Check(y.Layers==1&&g.OpeningYarnLayersRemoved==1,"booster effect direct area and four neighbors share one attack "+booster);}
  Reset();y=Yarn();left=Brick(3,4);right=Brick(5,4);g.ApplyChainImpact(left,1,Vector2.up,left.transform.position,true);g.ApplyChainImpact(right,1,Vector2.up,right.transform.position,true);Check(!y.Brick.Alive&&g.OpeningYarnLayersRemoved==2,"independent direct ball eliminations in one frame remain two attacks");
  Reset();y=Yarn();source=Brick(0,0,1);source.GetComponent<Rigidbody2D>().position=ChainGridRules.Center(3,4);Cast(3,8,8,1);Check(!source.Alive&&y.Layers==1,"flower removal uses live physical source position rather than authored grid cell");
  Reset();y=Yarn();g.QAEffectsArea(3,4,4,2,true);Reset();Tick(160);Check(g.Collected[0]==0&&g.OpeningYarnPending==0,"restart discards pending peel and unravel collection callbacks");
  Check(g.QAEffectsScopeCount==0,"all nested attack scopes restored after resolution");
  for(int level=0;level<78;level++){
   g.StartLevel(level);g.enabled=false;g.SetPaused(false);var nice=g.QASevenRewardPacket(0);var great=g.QASevenRewardPacket(1);var amazing=g.QASevenRewardPacket(2);var incredible=g.QASevenRewardPacket(3);
   Check(nice.Length==1&&new[]{1,5,6}.Contains(nice[0])&&great.SequenceEqual(new[]{2})&&amazing.Length==2&&amazing[1]==2&&incredible.Length==3&&incredible[1]==2&&incredible[2]==3,"standard special reward tiers independent of level "+(level+1));
   if(level<10){Check(new[]{1,2,3,5,6}.All(k=>OpeningCampaign.Allows(level,k)),"all authored special types accepted in opening "+(level+1));}
   if(level%10==0)yield return null;
  }
  foreach(int level in new[]{0,4,5,9}){g.StartLevel(level);g.enabled=false;g.SetPaused(false);int before=g.ChainGeneratedCount;int tier=g.QAAuditSpecialVolley(18);Check(tier==3&&g.ChainGeneratedCount-before==3&&g.ChainTools.Any(t=>t.Kind==3),"actual 18-break volley pays flower in level "+(level+1));}
  // Intro checks use a private in-memory storage and never modify the player's save.
  var store=new Store();var profile=LocalProfile.Load(store);foreach(int i in new[]{0,4,5})profile.MarkOpeningIntro(i);for(int i=1;i<=3;i++)profile.MarkBoosterGuide((StreakBooster)i);app.QAUseIntroProfile(profile);
  app.QAStartLevel(1);yield return app.QAWaitForLevel();g=app.Session;g.QAIgnoreFocusLoss=true;
  yield return Dismiss();foreach(int kind in SevenSpecialRules.Kinds){g.QASevenClear();g.SetPaused(false);g.QASevenTool(kind,4,8,0);yield return new WaitForSecondsRealtime(.1f);Check(!app.AlphabetIntroActive&&!g.IsPaused,"special appears without a teaching page kind="+kind);}
  var openingStore=new Store();var openingProfile=LocalProfile.Load(openingStore);for(int i=1;i<=3;i++)openingProfile.MarkBoosterGuide((StreakBooster)i);app.QAUseIntroProfile(openingProfile);
  foreach(int index in new[]{0,4,5}){app.QAStartLevel(index);yield return app.QAWaitForLevel();yield return WaitPage(501+index);Check(app.QAAlphabetIntro!=null&&app.QAAlphabetIntro.PageId==501+index,"fresh opening lesson still exists for level "+(index+1));yield return Dismiss();}
  Check(new[]{0,4,5}.All(LocalProfile.Load(openingStore).HasOpeningIntro),"opening lesson seen flags persist");app.QAStartLevel(4);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(1);Check(!app.AlphabetIntroActive,"seen yarn lesson skips without resetting player progress");
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(){g.StartLevel(4);g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);g.QASevenClear();g.BoosterProfile=app.Profile;}
```

## 源字段与依赖

```csharp
GameSession g;
```
```csharp
AppUI app;
```
```csharp
string dir;
```
```csharp
int checks,failures,errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
