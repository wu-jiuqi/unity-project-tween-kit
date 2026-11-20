# SevenSpecial80QA · SevenSpecial80QA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/SevenSpecial80QA.cs`
文件 SHA-256：`36e794a059d2a7a295bb1f49ab448b651fce2e25ec60010697c68e164e6b5632`
[原始源码快照](../source/Assets/Scripts/SevenSpecial80QA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L30

方法 SHA-256：`8984e384f2a90bf2f7fc197baff6ccf05e897434f6ec8585fd71dcd6bff5e0fe`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SevenSpecial80QA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SevenSpecial80QA.cs:36` · `SevenSpecial80QA.Start` → `Capture("00-home")`
- `Assets/Scripts/SevenSpecial80QA.cs:38` · `SevenSpecial80QA.Start` → `Capture("01-mainline-five-colors")`
- `Assets/Scripts/SevenSpecial80QA.cs:44` · `SevenSpecial80QA.Start` → `Capture("00b-level-one-default-rewards")`
- `Assets/Scripts/SevenSpecial80QA.cs:67` · `SevenSpecial80QA.Start` → `Capture("02-double-rainbow-wait")`
- `Assets/Scripts/SevenSpecial80QA.cs:69` · `SevenSpecial80QA.Start` → `Capture("03-after-refill")`
- `Assets/Scripts/SevenSpecial80QA.cs:73` · `SevenSpecial80QA.Start` → `Capture("04-real-volley")`
- `Assets/Scripts/SevenSpecial80QA.cs:74` · `SevenSpecial80QA.Start` → `Capture("05-double-bomb-combination")`
- `Assets/Scripts/SevenSpecial80QA.cs:77` · `SevenSpecial80QA.Start` → `Capture("06-all-25-special-variants")`
- `Assets/Scripts/SevenSpecial80QA.cs:80` · `SevenSpecial80QA.Start` → `Capture("08-combo-slow-motion")`
- `Assets/Scripts/SevenSpecial80QA.cs:81` · `SevenSpecial80QA.Start` → `Capture("09-flower-black-hole-suction")`
- `Assets/Scripts/SevenSpecial80QA.cs:84` · `SevenSpecial80QA.Start` → `Capture("07-level-77-mainline")`
- `Assets/Scripts/SevenSpecial80QA.cs:86` · `SevenSpecial80QA.Start` → `Capture("07b-level-77-special-showcase")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();var rt=new RenderTexture(Screen.width,Screen.height,24);rt.Create();var im=new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);QARecorder.Capture(Camera.main,rt,im);File.WriteAllBytes(dir+"/"+name+".png",im.EncodeToPNG());Destroy(im);rt.Release();Destroy(rt);}
```

### Start · L33

方法 SHA-256：`a97e5c9cd3aa92ea03ccc871ace9a72cc9c337f0722e8dbb087816133534047c`
时间来源：Time.realtimeSinceStartup, Time.timeScale, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SevenSpecial80QA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(dir+"/checks.txt","");Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Exception||t==LogType.Error){errors++;File.AppendAllText(dir+"/errors.txt",m+"\n"+s+"\n");}};
  Application.targetFrameRate=60;yield return new WaitForSecondsRealtime(3);app=FindFirstObjectByType<AppUI>();g=app.Session;
  Check(Application.version=="0.1.80"&&GameVersion.Code==80,"v80-r2 product version retained");yield return Capture("00-home");
  app.QAStartLevel(4);yield return app.QAWaitForLevel();while(app.AlphabetIntroActive){app.QAAdvanceAlphabetIntro();yield return null;}for(int n=0;n<160;n++){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return new WaitForSecondsRealtime(.04f);}g=app.Session;g.QAIgnoreFocusLoss=true;g.SetPaused(false);if(g.SelectedStreakBooster!=StreakBooster.None)g.SelectStreakBooster(g.SelectedStreakBooster);g.enabled=false;
  Check(g.SevenSpecialActive,"new rules enabled in the ordinary campaign");Check(app.QATutorialGuide==null,"legacy fixed-level special hand guide is absent");yield return Capture("01-mainline-five-colors");
  foreach(var key in new[]{"combo-cross","combo-bands","combo-bomb","combo-merge"}){Check(Enumerable.Range(0,16).All(i=>SevenSpecialArt.Frame(key,i)!=null),"16 generated combination frames: "+key);}
  for(int c=0;c<5;c++){Check(Enumerable.Range(0,16).All(f=>SevenSpecialArt.Ordinary(c,f).texture.name=="ordinary-"+SevenSpecialArt.ColorKeys[c]),"complete ordinary jelly pigment and destruction sequence "+c);foreach(int k in new[]{5,6,1,2,3})Check(Enumerable.Range(0,16).All(f=>SevenSpecialArt.Special(k,c,f)!=null)&&SevenSpecialArt.Special(k,c).texture.name=="square-specials-v2","complete square special variant "+k+" color "+c);}
  var colors=Enumerable.Range(0,100).Select(_=>g.NextSevenColor()).Distinct().Count();Check(colors==5,"five-color source covers all colors");
  // Cold-start every shipped chapter layout; goals and terrain remain attached.
  for(int level=0;level<78;level++){g.StartLevel(level);g.enabled=false;Check(g.SevenSpecialActive&&g.ChainGridValid()&&g.CollectionBricks.Where(b=>g.SevenColorTarget(b)).All(b=>b.Definition.colorIndex>=0&&b.Definition.colorIndex<5),"mainline level "+(level+1)+" occupancy and color IDs");var nice=g.QASevenRewardPacket(0);var great=g.QASevenRewardPacket(1);var amazing=g.QASevenRewardPacket(2);var incredible=g.QASevenRewardPacket(3);Check(g.QASevenRewardPacket(-1).Length==0&&nice.Length==1&&(nice[0]==1||nice[0]==5||nice[0]==6)&&great.SequenceEqual(new[]{2})&&amazing.Length==2&&amazing[1]==2&&incredible.Length==3&&incredible[1]==2&&incredible[2]==3,"level "+(level+1)+" has full default reward tiers without level locks");if(level%10==0)yield return null;}
  Reset(0);for(int row=0;row<9;row++)for(int col=0;col<9;col++)g.QASevenBrick(col,row,4,col%5);g.QASevenCompleteReward(3);Check(g.ChainTools.Count==0,"level one awards no special below four direct breaks");g.QASevenCompleteReward(4);Check(g.ChainTools.Count==1&&new[]{1,5,6}.Contains(g.ChainTools[0].Kind),"level one Nice award creates a laser through production replacement");g.QASevenCompleteReward(8);Check(g.ChainTools.Count(t=>t.Kind==2)==1,"level one Great award creates a bomb without progression lock");g.QASevenCompleteReward(18);g.QASevenCompleteReward(4);Check(SevenSpecialRules.Kinds.All(kind=>g.ChainTools.Any(t=>t.Kind==kind)),"all five special types can be generated in level one by the configured reward mechanism");yield return new WaitForSecondsRealtime(.3f);yield return Capture("00b-level-one-default-rewards");
  g.StartTutorial();g.enabled=false;g.QASevenCompleteReward(18);Check(g.IsTutorial&&g.SevenSpecialActive&&g.ChainTools.Count==3&&g.ChainTools.Any(t=>t.Kind==3),"first-bounce tutorial uses full default special generation too");
  var oldSimulation=Physics2D.simulationMode;Physics2D.simulationMode=SimulationMode2D.Script;
  Reset();var flowerStyle=g.SpawnChainTool(3,ChainGridRules.Center(4,4));var flowerBody=flowerStyle.View.Find("Complete baked special jelly").GetComponent<SpriteRenderer>();Check(flowerBody.sprite==SevenSpecialArt.Special(3,flowerStyle.ColorIndex)&&flowerBody.color==Color.white,"flower body and five petals are painted together without runtime tint");Check(flowerStyle.View.GetComponentsInChildren<SpriteRenderer>().Length==1,"special brick uses one complete illustration with no pasted icon");
  var fxBlock=new MaterialPropertyBlock();
  for(int c=0;c<5;c++){Reset();yield return null;var colorLaser=g.QASevenTool(6,4,4,c);g.ActivateChainTool(colorLaser,Vector2.up);var actualFx=FindObjectsByType<SevenSpecialFx>(FindObjectsSortMode.None).Where(x=>x.gameObject.activeInHierarchy).ToArray();Check(actualFx.Length>0&&actualFx.All(x=>{var r=x.GetComponentInChildren<SpriteRenderer>();r.GetPropertyBlock(fxBlock);return r.sharedMaterial.shader.name=="Garden/JellyPairFx"&&fxBlock.GetColor("_ColorA")==SevenSpecialArt.Palette[c]&&fxBlock.GetColor("_ColorB")==SevenSpecialArt.Palette[c];}),"single activation particles use source color "+c);Check(g.QASevenBeamColors(c,c),"full-length laser uses source color "+c);yield return null;}
  Reset();var pinkPair=g.QASevenTool(6,4,4,0);g.QASevenTool(2,5,5,3);g.ActivateChainTool(pinkPair,Vector2.up);Check(g.QASevenBeamColors(0,3),"combination beams preserve both participating colors");yield return null;
  var pairs=new[]{new[]{6,6,16},new[]{5,5,16},new[]{6,5,16},new[]{6,2,25},new[]{5,2,25},new[]{2,2,23},new[]{1,6,43},new[]{1,5,43},new[]{1,2,43},new[]{1,1,63}};
  foreach(var pair in pairs){Reset();var fixture=new System.Collections.Generic.List<BrickBody>();for(int row=0;row<9;row++)for(int col=0;col<9;col++)if(!(col==4&&row==4)&&!(col==5&&row==5))fixture.Add(g.QASevenBrick(col,row,25,0));var first=g.QASevenTool(pair[0],4,4,0);var second=g.QASevenTool(pair[1],5,5,2);g.ActivateChainTool(first,Vector2.up);Check(first.Consumed&&second.Consumed&&g.SevenCombos==1&&fixture.Count(x=>x.CurrentHealth==15)==pair[2]&&fixture.All(x=>x.CurrentHealth==15||x.CurrentHealth==25),"combination "+pair[0]+"+"+pair[1]+" has exact footprint and one ten-damage hit per brick");if(pair[0]==6&&pair[1]==2)Check(fixture.Single(x=>x.GetComponent<ChainGridPiece>().Col==0&&x.GetComponent<ChainGridPiece>().Row==5).CurrentHealth==15&&fixture.Single(x=>x.GetComponent<ChainGridPiece>().Col==5&&x.GetComponent<ChainGridPiece>().Row==0).CurrentHealth==25,"horizontal plus bomb selects three rows rather than columns");if(pair[0]==5&&pair[1]==2)Check(fixture.Single(x=>x.GetComponent<ChainGridPiece>().Col==5&&x.GetComponent<ChainGridPiece>().Row==0).CurrentHealth==15&&fixture.Single(x=>x.GetComponent<ChainGridPiece>().Col==0&&x.GetComponent<ChainGridPiece>().Row==5).CurrentHealth==25,"vertical plus bomb selects three columns rather than rows");yield return null;}
  Reset();var comboArmor=g.QASevenBrick(4,4,50,0,1,3);var comboFirst=g.QASevenTool(1,4,3,0);var comboPartner=g.QASevenTool(2,5,3,2);g.ActivateChainTool(comboFirst,Vector2.up);Check(comboArmor.ArmorLayers==2&&comboArmor.CurrentHealth==50&&g.SevenCombos==1,"one combination removes one armor layer without counting its two sources twice");
  Reset();var partnerTarget=g.QASevenBrick(8,0,25,3);var lineSource=g.QASevenTool(6,4,4,3);var flowerPartner=g.QASevenTool(3,5,5,0);g.ActivateChainTool(lineSource,Vector2.up);Check(lineSource.Consumed&&flowerPartner.Consumed&&g.SevenDoubles==0&&g.SevenConversions==1&&!partnerTarget.Alive,"non-flower trigger combines with a nearby flower using the non-flower partner type/color, not a false double-flower");
  Reset();var large=g.QASevenBrick(3,3,60,0,2);var cross=g.QASevenTool(1,2,3,0);g.ActivateChainTool(cross,Vector2.up);Check(large.CurrentHealth==50,"cross hits a multi-cell large brick once for ten");
  Reset();var armor=g.QASevenBrick(4,4,50,0,1,3);var h=g.QASevenTool(6,1,4,0);var bomb=g.QASevenTool(2,3,4,1);g.ActivateChainTool(h,Vector2.up);Check(armor.ArmorLayers==1&&armor.CurrentHealth==50,"laser and triggered bomb independently remove two of three armor layers");
  Reset();armor=g.QASevenBrick(4,4,50,0,1,1);g.SevenHit(armor,10,1);Check(!armor.ShellIntact&&armor.CurrentHealth==50,"last armor layer absorbs full effect without HP overflow");g.SevenHit(armor,10,1);Check(armor.CurrentHealth==40,"later effect damages exposed body");
  Reset();var terrain=g.QASevenBrick(4,4,1,0,1,0,true);var behind=g.QASevenBrick(6,4,25,1);h=g.QASevenTool(6,2,4,2);g.ActivateChainTool(h,Vector2.up);Check(terrain.Alive&&behind.CurrentHealth==15,"terrain is immune and does not occlude laser");
  Reset();var red=g.QASevenBrick(8,0,25,0);var blue=g.QASevenBrick(7,0,25,3);var rainbow=g.QASevenTool(3,0,8,0);g.ActivateChainTool(rainbow,Vector2.up);Check(red.CurrentHealth==15&&blue.CurrentHealth==25,"bound-color attack is global and independent of local ordinary neighbors");
  Reset();red=g.QASevenBrick(8,0,25,0);rainbow=g.QASevenTool(3,0,8,4);g.ActivateChainTool(rainbow,Vector2.up);Check(g.SevenEmptyCasts==1&&red.CurrentHealth==25,"missing bound color is a true empty cast with no pulse");
  Check(SevenSpecialRules.Priority(3,3,0)<SevenSpecialRules.Priority(1,0,0),"rainbow priority beats any matching-color special");Check(SevenSpecialRules.Priority(2,0,0)<SevenSpecialRules.Priority(1,3,0),"matching bomb priority beats off-color cross");Check(SevenSpecialRules.Priority(2,0,0)==SevenSpecialRules.Priority(1,0,0),"matching-color special types tie");Check(SevenSpecialRules.Priority(2,1,0)==SevenSpecialRules.Priority(5,2,0),"off-color bomb and direction laser tie");
  Reset();rainbow=g.QASevenTool(3,4,4,0);bomb=g.QASevenTool(2,5,3,0);cross=g.QASevenTool(1,3,4,3);g.ActivateChainTool(rainbow,Vector2.up);Check(bomb.Consumed&&!cross.Consumed&&g.SevenDoubles==0,"diagonal matching-color bomb selected before off-color cross");
  Reset();red=g.QASevenBrick(8,0,25,0);blue=g.QASevenBrick(7,0,25,3);rainbow=g.QASevenTool(3,0,8,0);h=g.QASevenTool(6,1,8,3);g.ActivateChainTool(rainbow,Vector2.up);Check(!blue.Alive&&red.CurrentHealth==15&&g.SevenConversions==1,"off-color partner chooses partner color; copies fire immediately for ten");
  Reset();red=g.QASevenBrick(8,0,25,0,1,2);var big=g.QASevenBrick(2,2,60,0,2);rainbow=g.QASevenTool(3,0,8,0);bomb=g.QASevenTool(2,1,8,0);g.ActivateChainTool(rainbow,Vector2.up);Check(red.ArmorLayers==1&&red.CurrentHealth==25&&big.CurrentHealth==60&&g.SevenConversions==0,"conversion strips one armor layer and excludes colorless large bricks");
  Reset();var p=g.QASevenBrick(7,0,1,0);var q=g.QASevenBrick(7,1,1,0);big=g.QASevenBrick(6,3,80,3,2);rainbow=g.QASevenTool(3,0,8,0);bomb=g.QASevenTool(2,1,8,0);g.ActivateChainTool(rainbow,Vector2.up);Check(g.SevenConversions==2&&g.SevenDoubles==0,"two copied neighboring bombs emit two single effects and never combine");
  Reset();large=g.QASevenBrick(2,2,50,0,2);armor=g.QASevenBrick(6,3,50,0,1,3);terrain=g.QASevenBrick(8,0,1,0,1,0,true);rainbow=g.QASevenTool(3,4,7,0);var r2=g.QASevenTool(3,5,7,4);var r3=g.QASevenTool(3,0,0,2);var nearbyOldLaser=g.QASevenTool(6,0,1,3);g.ActivateChainTool(rainbow,Vector2.up);
  Check(g.SevenDoubles==1&&large.CurrentHealth==20&&armor.ArmorLayers==1&&armor.CurrentHealth==50&&terrain.Alive,"double rainbow deals thirty or removes two armor; terrain survives");Check(r3.Waiting&&g.SevenWaitingCount==1&&!r3.Consumed&&nearbyOldLaser.Consumed,"third flower waits before refill and cannot be consumed by an adjacent old laser during screen-wide resolution");yield return new WaitForSecondsRealtime(.2f);yield return Capture("02-double-rainbow-wait");Check(r3.View.localScale.x>1.1f,"waiting rainbow grows slightly and stays anchored");Vector2 anchor=r3.Position;int refills=g.ChainRefills;
  float limit=Time.realtimeSinceStartup+15;while(!r3.Consumed&&Time.realtimeSinceStartup<limit){g.QASevenTick(.02f);yield return null;}
  Check(r3.Consumed&&g.ChainRefills>refills,"after old animations finish board refills and remaining rainbow triggers");Check(terrain.Alive,"terrain survives full double-rainbow settlement");yield return Capture("03-after-refill");
  Reset();red=g.QASevenBrick(4,4,25,0);rainbow=g.QASevenTool(3,0,8,0);g.CollectionGoal[0]=0;g.ActivateChainTool(rainbow,Vector2.up);Check(!g.QASevenOutcome(),"earned victory waits for pending effects and refill score settlement");
  int beforeVictoryRefills=g.ChainRefills;bool resolved=false;float victoryLimit=Time.realtimeSinceStartup+12;while(!resolved&&Time.realtimeSinceStartup<victoryLimit){g.QASevenTick(.02f);resolved=g.QASevenOutcome();yield return null;}Check(resolved&&g.ChainRefills>beforeVictoryRefills,"earned victory completes after normal refill and all score effects settle");
  Physics2D.simulationMode=oldSimulation;
  g.StartLevel(4);g.enabled=true;g.QAIgnoreFocusLoss=true;g.SetPaused(false);int physicalHits=g.HitCount;Check(g.TryShoot(new Vector2(.15f,1)),"real production volley accepted");Check(g.IsPlaying&&!g.IsPaused,"first-appearance pages can immediately pause an active or queued volley");yield return new WaitForSecondsRealtime(2.2f);Check(g.HitCount>physicalHits&&g.SpawnCount>=5,"real ball contacts damage five-color campaign bricks");yield return Capture("04-real-volley");g.enabled=false;
  Reset();for(int row=1;row<8;row++)for(int col=1;col<8;col++)if(!(col==4&&row==4)&&!(col==5&&row==5))g.QASevenBrick(col,row,25,(col+row)%5);var bombA=g.QASevenTool(2,4,4,0);g.QASevenTool(2,5,5,1);g.ActivateChainTool(bombA,Vector2.up);yield return new WaitForSecondsRealtime(.23f);yield return Capture("05-double-bomb-combination");
  // Inspect all 25 complete variants together in the production board renderer.
  Reset(18);var kinds=new[]{5,6,1,2,3};for(int row=0;row<9;row++)for(int col=0;col<9;col++){if(col>=2&&col<=6&&row>=3&&row<=7)g.QASevenTool(kinds[row-3],col,row,col-2);else g.QASevenBrick(col,row,4,col%5);}
  yield return new WaitForSecondsRealtime(.3f);yield return Capture("06-all-25-special-variants");
  foreach(var t in g.ChainTools){var image=t.View.GetComponentInChildren<SpriteRenderer>();var painted=SevenSpecialArt.PaintedBounds(image);Check(Mathf.Abs(painted.size.x-g.Board.CellWidth)<.025f&&Mathf.Abs(painted.size.y-g.Board.CellWidth)<.025f&&Vector2.Distance(painted.center,t.Position)<.015f,"square silhouette matches ordinary cell and is centered "+t.Kind+" color "+t.ColorIndex);}
  Reset();foreach(int kind in SevenSpecialRules.Kinds)g.QASevenTool(kind,kind,8,kind%5);yield return new WaitForSecondsRealtime(.1f);Check(!app.AlphabetIntroActive&&!g.IsPaused,"all five specials appear without teaching or pause");
  Reset();var cinematicTarget=g.QASevenBrick(4,3,25,1);var cinematicA=g.QASevenTool(6,4,4,0);g.QASevenTool(2,5,5,3);g.ActivateChainTool(cinematicA,Vector2.up);Check(cinematicTarget.CurrentHealth==25&&g.SevenSettlementPending,"combination reserves both sources and defers damage until jelly windup finishes");yield return new WaitForSecondsRealtime(.16f);Check(Time.timeScale>=.45f&&Time.timeScale<=1f,"combination retains light feedback without deep slow motion");yield return Capture("08-combo-slow-motion");yield return new WaitForSecondsRealtime(.35f);Check(cinematicTarget.CurrentHealth==15&&g.SevenCombos==1,"both bouncing source bricks release one combined ten-damage effect");yield return new WaitForSecondsRealtime(.2f);Check(Mathf.Approximately(Time.timeScale,1),"special slow motion restores normal time automatically");
  Reset();var pulled=new System.Collections.Generic.List<BrickBody>();for(int i=0;i<7;i++)pulled.Add(g.QASevenBrick(i+1,1+i%3,4,0));var suctionArmor=g.QASevenBrick(7,6,25,0,1,2);var suctionFlower=g.QASevenTool(3,4,6,0);g.ActivateChainTool(suctionFlower,Vector2.up);yield return new WaitForSecondsRealtime(.46f);Check(pulled.All(x=>!x.Alive)&&SevenSuctionFx.Alive(g)>=7&&suctionArmor.ArmorLayers==1&&suctionArmor.CurrentHealth==25,"flower targets spiral inward while armor only loses one layer");yield return Capture("09-flower-black-hole-suction");yield return new WaitForSecondsRealtime(.6f);Check(SevenSuctionFx.Alive(g)==0,"all suction ghosts finish before normal board settlement");
  Reset();var pausedTarget=g.QASevenBrick(4,3,25,0);var pausedSource=g.QASevenTool(2,4,4,1);g.ActivateChainTool(pausedSource,Vector2.up);yield return new WaitForSecondsRealtime(.08f);g.SetPaused(true);yield return new WaitForSecondsRealtime(.2f);Check(Mathf.Approximately(Time.timeScale,1)&&pausedTarget.CurrentHealth==25,"pause cancels cinematic clock and holds pending special damage");g.SetPaused(false);yield return new WaitForSecondsRealtime(.45f);Check(pausedTarget.CurrentHealth==15&&Mathf.Approximately(Time.timeScale,1),"resume finishes pending special once and restores time");
  Reset();var oldWindup=g.QASevenTool(2,4,4,0);g.ActivateChainTool(oldWindup,Vector2.up);yield return new WaitForSecondsRealtime(.08f);Reset();yield return new WaitForSecondsRealtime(.4f);Check(Mathf.Approximately(Time.timeScale,1)&&!g.SevenSettlementPending,"restarting during slow motion cancels old callback without affecting new board");
  app.QAStartLevel(76);yield return app.QAWaitForLevel();float enterLimit=Time.realtimeSinceStartup+8;while(Time.realtimeSinceStartup<enterLimit){app.QAAdvanceAlphabetIntro();app.QAAdvanceBoosterGuide();yield return null;}g=app.Session;g.enabled=false;g.SetPaused(false);yield return new WaitForSecondsRealtime(.3f);yield return Capture("07-level-77-mainline");
  var showcase=new[]{new[]{5,0,8,4},new[]{6,4,7,1},new[]{1,4,0,0},new[]{2,4,1,1},new[]{3,8,7,3}};foreach(var spec in showcase){var position=ChainGridRules.Center(spec[1],spec[2]);foreach(var b in g.CollectionBricks.Where(b=>b.Alive&&Vector2.Distance(b.transform.position,position)<.05f&&b.GetComponent<ChainGridPiece>().Size==1).ToArray())b.Remove();var present=g.ChainTools.FirstOrDefault(t=>Vector2.Distance(t.Position,position)<.05f);if(present==null)g.QASevenTool(spec[0],spec[1],spec[2],spec[3]);else {present.Kind=spec[0];present.ColorIndex=spec[3];}}
  yield return new WaitForSecondsRealtime(.35f);yield return Capture("07b-level-77-special-showcase");
  Physics2D.simulationMode=oldSimulation;
  File.WriteAllText(dir+"/report.json",JsonUtility.ToJson(new Report{checks=checks,failures=failures,errors=errors},true));Debug.Log("SEVEN_SPECIAL_QA checks="+checks+" failures="+failures+" errors="+errors);Application.Quit(failures==0&&errors==0?0:3);
 }
```

## 对象层级和素材入口

- L47：`flowerStyle.View.Find("Complete baked special jelly")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Reset(int level=4){g.StartLevel(level);g.enabled=false;g.QAIgnoreFocusLoss=true;g.QASevenClear();}
```

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

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
