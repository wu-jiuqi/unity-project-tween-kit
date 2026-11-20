# SpecialGenerationAudit · SpecialGenerationAudit

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/SpecialGenerationAudit.cs`
文件 SHA-256：`e5ee7ce8a28e55ea856dc889acae0774bc240b64c6a12e7a2f191558e1c68db8`
[原始源码快照](../source/Assets/Scripts/SpecialGenerationAudit.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L19

方法 SHA-256：`e19f237d7613faeb9c6c7324746ca44a323a1f19925af7033567b00b07c27eef`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SpecialGenerationAudit.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SpecialGenerationAudit.cs:27` · `SpecialGenerationAudit.Start` → `Capture(dir+"/level05-authored.png")`
- `Assets/Scripts/SpecialGenerationAudit.cs:36` · `SpecialGenerationAudit.Start` → `Capture(dir+"/level05-after-clear.png")`

```csharp
IEnumerator Capture(string path){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var image=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,image);File.WriteAllBytes(path,image.EncodeToPNG());Destroy(image);rt.Release();Destroy(rt);}
```

### Start · L20

方法 SHA-256：`a098284b5e2991e3b4294f5e5f0bb800f10af8c68d4c3be00b2af2ff92392e73`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SpecialGenerationAudit.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  yield return new WaitForSecondsRealtime(3);var args=Environment.GetCommandLineArgs();string dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);
  var app=FindFirstObjectByType<AppUI>();Physics2D.simulationMode=SimulationMode2D.Script;int checks=0,failures=0,errors=0;
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Exception||t==LogType.Error){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  foreach(int index in Enumerable.Range(0,10).Concat(new[]{10,21,26,52})){
   app.QAStartLevel(index);yield return app.QAWaitForLevel();for(int n=0;n<80;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.025f);if(n>15&&!app.AlphabetIntroActive&&!app.Session.IsPaused)break;}
   var g=app.Session;g.QAIgnoreFocusLoss=true;g.SetPaused(false);g.enabled=false;int initial=g.ChainTools.Count,generated=g.ChainGeneratedCount,eligible=g.QAAuditEligibleSpecialCells();
   if(index==4)yield return Capture(dir+"/level05-authored.png");
   if(index<10){checks++;if(initial!=OpeningCampaign.Stage(index).pieces.Count(p=>p.kind!="yarn"))failures++;}
   var packet=g.QASevenRewardPacket(0);int tier=g.QAAuditSpecialVolley(4);
   string line="LEVEL="+(index+1)+" initial="+initial+" nicePacket=["+string.Join(",",packet)+"] eligibleBefore="+eligible+" tier="+tier+" newSpecials="+(g.ChainGeneratedCount-generated)+" waiting="+g.ChainPendingReplacements;
   File.AppendAllText(dir+"/audit.txt",line+"\n");Debug.Log("SPECIAL_GENERATION_AUDIT "+line);
   checks++;if(tier!=0||g.ChainGeneratedCount-generated!=packet.Length||g.ChainPendingReplacements!=0)failures++;
   if(index<10){checks++;if(g.CollectionBricks.Any(b=>b.Alive&&b.Definition.reveal=="opening-N"&&b.Definition.juiceGroup!=0))failures++;}
   if(index==4||index==5){var yarns=g.QAOpeningYarns();g.QAAuditForcedReward();checks++;if(yarns.Any(y=>!y.Brick.Alive))failures++;}
   for(int n=0;n<180;n++)g.QASevenTick(.02f);checks++;if(!g.ChainGridReady||!g.ChainGridValid(false))failures++;
   if(index==4)yield return Capture(dir+"/level05-after-clear.png");
  }
  for(int index=0;index<78;index++){
   var g=app.Session;g.StartLevel(index);g.enabled=false;g.QAIgnoreFocusLoss=true;g.SetPaused(false);g.QASevenClear();
   var normal=g.QASevenBrick(4,4,1,0);var tool=g.QASevenTool(2,4,3,0);var support=g.QASevenBrick(4,8,1,0);var stone=g.QASevenBrick(3,8,1,0,1,0,true);
   var yarn=index<10?g.QAAuditYarn(4,5):null;Vector2 normalStart=normal.transform.position,toolStart=tool.Position,stoneStart=stone.transform.position;
   g.QAAuditRemoveSupport(support);for(int n=0;n<180;n++)g.QASevenTick(.02f);
   bool moved=normal.transform.position.y<normalStart.y-.4f&&tool.Position.y<toolStart.y-.4f;
   bool yarnMoved=yarn==null||yarn.Alive&&yarn.transform.position.y<ChainGridRules.Center(4,5).y-.4f;
   bool valid=g.ChainGridReady&&g.ChainGridValid(false)&&Vector2.Distance(stone.transform.position,stoneStart)<.002f;
   checks+=3;if(!moved)failures++;if(!yarnMoved)failures++;if(!valid)failures++;
   File.AppendAllText(dir+"/gravity.txt","LEVEL="+(index+1)+" ordinaryAndSpecialFall="+moved+" yarnFalls="+yarnMoved+" gridAndStoneValid="+valid+"\n");
   if(index<10){
    checks++;if(!g.RefillActive)failures++;
    g.QARefillFinishGroup(1);int planned=g.RefillPending;
    for(int n=0;n<600;n++)g.QASevenTick(.02f);
    bool refill=planned>0&&g.RefillGenerated==planned&&g.RefillPending==0&&g.ChainGridReady&&g.ChainGridValid(false)&&(yarn==null||yarn.Alive);
    checks++;if(!refill)failures++;File.AppendAllText(dir+"/gravity.txt","REFILL_LEVEL="+(index+1)+" planned="+planned+" generated="+g.RefillGenerated+" passed="+refill+"\n");
   }
   if(index%5==0)yield return null;
  }
  File.WriteAllText(dir+"/result.json","{\"checks\":"+checks+",\"failures\":"+failures+",\"errors\":"+errors+"}");Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖


业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
