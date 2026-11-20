# Settlement82QA · Settlement82QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Settlement82QA.cs`
文件 SHA-256：`32f013ce24c519897e923113f58ad905bb205a1d0b6ee9d78f7ea5ba9e13556d`
[原始源码快照](../source/Assets/Scripts/Settlement82QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Resolve · L25

方法 SHA-256：`a659f5326b77425706df1e8cd06da75e84bab7ef8eed411028b6dabc2fe3a226`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Settlement82QA.Resolve(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Settlement82QA.cs:30` · `Settlement82QA.Start` → `Resolve()`
- `Assets/Scripts/Settlement82QA.cs:33` · `Settlement82QA.Start` → `Resolve()`
- `Assets/Scripts/Settlement82QA.cs:34` · `Settlement82QA.Start` → `Resolve()`
- `Assets/Scripts/Settlement82QA.cs:35` · `Settlement82QA.Start` → `Resolve()`
- `Assets/Scripts/Settlement82QA.cs:36` · `Settlement82QA.Start` → `Resolve()`
- `Assets/Scripts/Settlement82QA.cs:37` · `Settlement82QA.Start` → `Resolve()`
- `Assets/Scripts/Settlement82QA.cs:38` · `Settlement82QA.Start` → `Resolve()`
- `Assets/Scripts/Settlement82QA.cs:39` · `Settlement82QA.Start` → `Resolve()`
- `Assets/Scripts/Settlement82QA.cs:40` · `Settlement82QA.Start` → `Resolve(400)`
- `Assets/Scripts/Settlement82QA.cs:41` · `Settlement82QA.Start` → `Resolve(1400)`

```csharp
IEnumerator Resolve(int limit=700){resolved=won=false;for(int i=0;i<limit&&!resolved;i++){g.QASevenTick(.02f);resolved=g.QA82Resolve(out won);if(i%4==0)yield return new WaitForSecondsRealtime(.02f);}}
```

### Start · L26

方法 SHA-256：`eac954f20ac443b1c2d393bf46bd027009a14584f6101203997b41ca99ae5a18`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.Settlement82QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();int at=Array.IndexOf(args,"--qa-out");dir=at>=0?args[at+1]:Application.persistentDataPath+"/Settlement82";Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");
  Application.logMessageReceived+=(message,stack,type)=>{if(type==LogType.Error||type==LogType.Exception||type==LogType.Assert){errors++;File.AppendAllText(dir+"/errors.txt",message+"\n"+stack+"\n");}};
  yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();g=app.Session;Physics2D.simulationMode=SimulationMode2D.Script;
  var final=g.QA82Fixture();g.ApplyChainImpact(final,1,Vector2.up,final.transform.position,true);yield return Resolve();
  bool repro=args.Contains("--settlement82-repro");Check(repro?!resolved&&g.CollectionWon:resolved&&won,repro?"reproduced victory deadlock at even refill boundary with undeliverable reward":"victory resolves at even refill boundary with undeliverable reward");File.AppendAllText(dir+"/checks.txt",g.QA82Status()+"\n");
  if(!repro){
   final=g.QA82Fixture(2,false);g.ApplyChainImpact(final,1,Vector2.up,final.transform.position,true);int generated=g.RefillGenerated;yield return Resolve();Check(resolved&&won&&g.RefillGenerated==generated,"earned victory does not generate a closing refill");
   final=g.QA82Fixture(1,true);g.ApplyChainImpact(final,1,Vector2.up,final.transform.position,true);yield return Resolve();Check(resolved&&won,"odd-boundary victory also ignores an impossible replacement");
   final=g.QA82Fixture(2,true,0);g.ApplyChainImpact(final,1,Vector2.up,final.transform.position,true);yield return Resolve();Check(resolved&&won,"last-move victory closes the refill boundary without another input");
   final=g.QA82Fixture(2,true);g.QA82BufferInputs();g.ApplyChainImpact(final,1,Vector2.up,final.transform.position,true);int moves=g.Shots;g.QA82DriveInput();Check(!g.QA82HasQueuedInput&&!g.ChainCanLaunch&&!g.TryShoot(Vector2.up)&&g.Shots==moves&&g.SpawnCount==0,"winning hit discards next-shot input without spending a move or spawning another volley");yield return Resolve();Check(resolved&&won,"buffered-input victory settles without another shot");
   final=g.QA82Fixture(2,false);foreach(var b in g.CollectionBricks.Where(b=>b.Alive).Take(9).ToArray())g.QARefillRemove(b);g.QARefillFull();g.QASevenTick(.02f);generated=g.RefillGenerated;Check(generated==1&&g.RefillPending>0,"fixture has one committed arrival and queued future supply");g.ApplyChainImpact(final,1,Vector2.up,final.transform.position,true);yield return Resolve();Check(resolved&&won&&g.RefillGenerated==generated&&!g.RefillPendingSettlement,"committed arrival lands, uncommitted supply is cancelled");
   final=g.QA82Fixture(2,false);foreach(var b in g.CollectionBricks.Where(b=>b.Alive&&b!=final).ToArray())g.QARefillRemove(b);g.ApplyChainImpact(final,1,Vector2.up,final.transform.position,true);yield return Resolve();Check(resolved&&won&&g.RefillGenerated==0&&g.RefillFullBatches==0,"empty-board victory does not start a full refill");
   final=g.QA82Fixture(2,true);g.ApplyChainImpact(final,1,Vector2.up,final.transform.position,true);g.SetPaused(true);yield return new WaitForSecondsRealtime(.2f);Check(!g.CollectionVictoryPending,"pause does not start the result timer");g.SetPaused(false);yield return Resolve();Check(resolved&&won,"paused winning volley settles normally after resume");
   final=g.QA82Fixture(2,false);g.JellyGoal[0]=999;foreach(var b in g.CollectionBricks.Where(b=>b.Alive).Take(8).ToArray())g.QARefillRemove(b);g.QARefillFinishGroup(1);Check(g.RefillPending==4,"unfinished level still snapshots half of eight gaps");yield return Resolve(400);Check(!resolved&&!won&&g.RefillGenerated==4,"unfinished collection still refills and stays playable");
   final=g.QA82Fixture(2,false);foreach(var b in g.CollectionBricks.Where(b=>b.Alive&&b.GetComponent<ChainGridPiece>().Row==8&&b.GetComponent<ChainGridPiece>().Col>=4&&b.GetComponent<ChainGridPiece>().Col<=6).ToArray())g.QARefillRemove(b);var flower=g.QASevenTool(3,4,8,0);g.QASevenTool(3,5,8,0);g.QASevenTool(3,6,8,0);g.ActivateChainTool(flower,Vector2.up);yield return Resolve(1400);Check(resolved&&won&&g.SevenWaitingCount==0&&g.RefillGenerated==0,"double-flower winning chain releases waiting flowers without demanding a new board");
   app.QAStartLevel(5);yield return app.QAWaitForLevel();app.Session.enabled=true;for(int n=0;n<100;n++){app.QAAdvanceAlphabetIntro();yield return new WaitForSecondsRealtime(.025f);if(n>12&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}g=app.Session;g.SetPaused(false);g.enabled=false;
   foreach(var yarn in g.QAOpeningYarns()){var visual=yarn.GetComponentsInChildren<SpriteRenderer>().First(s=>s.sharedMaterial==OpeningYarnArt.Material);var box=yarn.Brick.GetComponent<BoxCollider2D>();Check(Mathf.Abs(visual.bounds.size.x-box.size.x)<.002f&&Mathf.Abs(visual.bounds.size.y-box.size.y)<.002f,"square yarn matches ordinary brick width AND height");Check(Vector2.Distance(visual.bounds.center,yarn.Brick.transform.position)<.002f,"square wool brick is centred in its collider");}
   yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,image);File.WriteAllBytes(dir+"/square-yarn-level06.png",image.EncodeToPNG());Destroy(image);rt.Release();Destroy(rt);
  }
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+",\"reproduction\":"+(repro?"true":"false")+"}");Debug.Log("SETTLEMENT82_QA "+g.QA82Status()+" checks="+checks+" failures="+failures+" errors="+errors);Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

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
```csharp
bool resolved,won;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
