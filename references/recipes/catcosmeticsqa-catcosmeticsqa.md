# CatCosmeticsQA · CatCosmeticsQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/CatCosmeticsQA.cs`
文件 SHA-256：`aec6445586cc4042a620927e1ff9dff19ded7d42af1c374be4928ff6fbc615b9`
[原始源码快照](../source/Assets/Scripts/CatCosmeticsQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Capture · L13

方法 SHA-256：`37b4e0cefc45be40b32c86a4839f1a87b69131ec504d6ff41768b17a43c74af9`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CatCosmeticsQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/CatCosmeticsQA.cs:21` · `CatCosmeticsQA.Start` → `Capture("01-home")`
- `Assets/Scripts/CatCosmeticsQA.cs:33` · `CatCosmeticsQA.Start` → `Capture("02-ranking")`
- `Assets/Scripts/CatCosmeticsQA.cs:34` · `CatCosmeticsQA.Start` → `Capture("03-reward-preview")`
- `Assets/Scripts/CatCosmeticsQA.cs:36` · `CatCosmeticsQA.Start` → `Capture("04-customize")`
- `Assets/Scripts/CatCosmeticsQA.cs:37` · `CatCosmeticsQA.Start` → `Capture("05-equipped-home")`
- `Assets/Scripts/CatCosmeticsQA.cs:41` · `CatCosmeticsQA.Start` → `Capture("06-battle")`

```csharp
IEnumerator Capture(string name){yield return new WaitForEndOfFrame();Canvas.ForceUpdateCanvases();cam.Render();var old=RenderTexture.active;RenderTexture.active=target;var image=new Texture2D(width,height,TextureFormat.RGB24,false);image.ReadPixels(new Rect(0,0,width,height),0,0);image.Apply();RenderTexture.active=old;File.WriteAllBytes(Path.Combine(dir,name+".png"),image.EncodeToPNG());Destroy(image);}
```

### Start · L15

方法 SHA-256：`2314ccbed0df493e9c352774cff82830b457625e7ccb8abb36dac81068c60300`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CatCosmeticsQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start(){
  var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];Directory.CreateDirectory(dir);File.WriteAllText(Path.Combine(dir,"checks.txt"),"");int index=Array.IndexOf(args,"--qa-size");if(index>=0){var size=args[index+1].Split('x');width=int.Parse(size[0]);height=int.Parse(size[1]);}
  Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s+"\n");}};
  yield return new WaitForSecondsRealtime(1);app=FindFirstObjectByType<AppUI>();cam=Camera.main;target=new RenderTexture(width,height,24);target.Create();cam.targetTexture=target;var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;canvas.worldCamera=cam;canvas.planeDistance=1;app.QASetViewport(new Vector2Int(width,height));app.QAStaticNavigation("home");yield return new WaitForSecondsRealtime(.7f);
  Check(CatCosmeticCatalog.Validate().StartsWith("PASS"),"20 sprites and 100 covered pair layouts load in built player");
  Check(Header().AvatarId=="01_british_shorthair"&&Header().FrameId=="01_british_shorthair","home header uses default British pair");
  Check(GameObject.Find("Avatar medallion/garden_frame")==null,"old header placeholder removed");yield return Capture("01-home");
  var audit=UICraft.Root(canvas.transform,"Hidden pair audit",0,0,256,256);audit.gameObject.SetActive(false);
  foreach(var avatar in CatCosmeticCatalog.Items)foreach(var frame in CatCosmeticCatalog.Items){
   var pair=CatCosmeticCatalog.Create(audit,avatar.avatar.resource,frame.frame.resource,0,0,256);var a=pair.Find("Avatar").GetComponent<Image>();var f=pair.Find("Frame").GetComponent<Image>();var layout=CatCosmeticCatalog.Pair(avatar,frame);var r=a.rectTransform;
   Check(a.sprite==CatCosmeticCatalog.Sprite(avatar.avatar.resource)&&f.sprite==CatCosmeticCatalog.Sprite(frame.frame.resource)&&!a.raycastTarget&&!f.raycastTarget&&Mathf.Abs((r.anchorMax.x-r.anchorMin.x)-layout.scale)<.00001f&&Mathf.Abs((r.anchorMax.y+r.anchorMin.y)*.5f-(.5f-layout.offsetY))<.00001f&&f.transform.GetSiblingIndex()>a.transform.GetSiblingIndex(),"runtime pair "+avatar.id+" / "+frame.id);
  }
  Destroy(audit.gameObject);
  foreach(var item in CatCosmeticCatalog.Items)Check(Resources.Load<GameObject>("Art/CatCosmeticsV3/prefabs/"+item.id)!=null,"imported prefab "+item.id);
  Check(CatCosmeticCatalog.FindAvatar("LeagueV1/reward_avatar").id=="02_ragdoll"&&CatCosmeticCatalog.FindFrame("LeagueV1/reward_frame").id=="02_ragdoll","legacy reward paths resolve to new pair");
  Check(CatCosmeticCatalog.FindAvatar("LeagueV1/avatar_luna")!=null&&CatCosmeticCatalog.FindAvatar("avatar_mochi")!=null&&CatCosmeticCatalog.FindAvatar("LeagueV1/avatar_sunny")!=null&&CatCosmeticCatalog.FindAvatar("avatar_clover")!=null&&CatCosmeticCatalog.FindAvatar("avatar_poppy")!=null,"all five saved rival aliases resolve");
  app.QAStaticNavigation("ranking");yield return new WaitForSecondsRealtime(.9f);var league=FindFirstObjectByType<LeagueRankingView>();
  Check(league.RowCount==21,"all leaderboard rows present");Check(league.ListContent.GetComponentsInChildren<CatCosmeticPortrait>().Select(p=>p.AvatarId).Distinct().Count()==10,"all ten breeds present in rival roster");
  Check(league.ListContent.GetComponentsInChildren<CatCosmeticPortrait>().All(p=>p.transform.Find("Frame")!=null),"rival and player rows include frames");yield return Capture("02-ranking");
  app.OpenLeagueRewards();yield return new WaitForSecondsRealtime(.35f);Check(FindObjectsByType<CatCosmeticPortrait>(FindObjectsSortMode.None).Any(p=>p.AvatarId=="02_ragdoll"&&((RectTransform)p.transform).rect.width==290),"reward panel shows combined new artwork");yield return Capture("03-reward-preview");
  var clock=new LeagueSelfTests.Clock{Now=DateTimeOffset.UtcNow.ToUnixTimeSeconds()};app.Profile.ConfigureLeague(new ResourceLeagueConfigProvider(),clock);for(int i=0;i<60;i++)app.LeagueSource.AddTestMedals();clock.Now+=8*86400L;var pending=app.LeagueSource.Read().reward;
  Check(pending!=null,"eligible pending cosmetic reward");if(pending!=null){int coins=app.Profile.Coins;Check(app.LeagueSource.Claim(pending.periodId),"cosmetic reward still claims");Check(app.Profile.Coins==coins+pending.coins&&!app.LeagueSource.Claim(pending.periodId),"claim remains idempotent");Check(app.LeagueSource.Equip(pending.avatarId,pending.frameId),"equip owned avatar and frame");app.OpenLeagueCustomization();yield return new WaitForSecondsRealtime(.3f);yield return Capture("04-customize");
   app.QAStaticNavigation("home");yield return new WaitForSecondsRealtime(.5f);Check(Header().AvatarId=="02_ragdoll"&&Header().FrameId=="02_ragdoll","equipped pair shown in home header");yield return Capture("05-equipped-home");
   Check(app.LeagueSource.Equip(pending.avatarId,""),"avatar can be equipped independently");app.QAStaticNavigation("home");yield return null;Check(Header().AvatarId=="02_ragdoll"&&Header().FrameId=="01_british_shorthair","independent selection uses measured mixed pair");
   var saved=app.Profile.ExportForQA().Replace("CatCosmeticsV3/avatars/02_ragdoll","LeagueV1/reward_avatar").Replace("CatCosmeticsV3/frames/02_ragdoll","LeagueV1/reward_frame");var old=LocalProfile.ImportForQA(saved);var root=LeaguePortrait.Create(canvas.transform,old,"",0,0,256,true);Check(root.GetComponent<CatCosmeticPortrait>().AvatarId=="02_ragdoll","old saved equipped resource renders new avatar");Destroy(root.gameObject);
  }
  app.QAStaticNavigation("home");app.QAStartLevel(6);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.3f);var hud=GameObject.Find("Battle HUD");Check(hud.GetComponentsInChildren<CatCosmeticPortrait>().Any(p=>p.AvatarId=="02_ragdoll"),"battle HUD uses equipped portrait");yield return Capture("06-battle");
  File.AppendAllText(Path.Combine(dir,"checks.txt"),$"RESULT checks={checks} failed={failures} errors={errors}\n");Application.Quit(failures+errors==0?0:1);
 }
```

## 对象层级和素材入口

- L21：`GameObject.Find("Avatar medallion/garden_frame")`
- L22：`UICraft.Root(canvas.transform,"Hidden pair audit",0,0,256,256)`
- L24：`pair.Find("Avatar")`
- L33：`p.transform.Find("Frame")`
- L41：`GameObject.Find("Battle HUD")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
string dir;
```
```csharp
int checks,failures,errors,width=540,height=1170;
```
```csharp
AppUI app;
```
```csharp
Camera cam;
```
```csharp
RenderTexture target;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
