# GardenBackgroundQA · GardenBackgroundQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/GardenBackgroundQA.cs`
文件 SHA-256：`b77eff657736eaab60e8a420dd5061756f7921ab224828e95eebe1f13a5a32de`
[原始源码快照](../source/Assets/Scripts/GardenBackgroundQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L26

方法 SHA-256：`b00ea857efb5af67d1ade073a53cb2f992d5ac6097144996a5add1f9883f03be`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GardenBackgroundQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start()
        {
            var args=Environment.GetCommandLineArgs();dir=args[Array.IndexOf(args,"--qa-out")+1];
            Directory.CreateDirectory(dir); File.WriteAllText(Path.Combine(dir,"checks.txt"),"");
            int size=Array.IndexOf(args,"--qa-size");if(size>=0){var s=args[size+1].Split('x');width=int.Parse(s[0]);height=int.Parse(s[1]);}
            Application.logMessageReceived+=(m,s,t)=>{if(t==LogType.Error||t==LogType.Exception){errors++;File.AppendAllText(Path.Combine(dir,"errors.txt"),m+"\n"+s+"\n");}};
            yield return new WaitForSecondsRealtime(1);
            app=FindFirstObjectByType<AppUI>();Check(app!=null&&RuntimeQA.IsIsolated,"isolated memory-only profile");
            cam=Camera.main;target=new RenderTexture(width,height,24);target.Create();cam.targetTexture=target;
            var canvas=FindFirstObjectByType<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceCamera;
            canvas.worldCamera=cam;canvas.planeDistance=1;app.QASetViewport(new Vector2Int(width,height));
            app.QAStartLevel(0);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.7f);
            var game=app.Session;game.QAIgnoreFocusLoss=true;var decor=game.GardenDecor;
            Check(decor!=null&&decor.DecorationCount==15,"original reference foliage and cat regions loaded");
            Check(decor.GetComponentsInChildren<Collider2D>().Length==0,"decoration adds no physics colliders");
            Check(game.JellyWalls!=null&&game.GetComponentsInChildren<JellyWallContact>().Length==3,"existing jelly walls and three collision contacts retained");
            Capture("stage-01");int shots=game.Shots;
            int taps=decor.ClickCount;var point=(Vector2)cam.WorldToScreenPoint(decor.FlowerPosition);
            app.QABeginAim(point);app.QAReleaseAim(point+Vector2.left*120);
            Check(decor.ClickCount==taps+1&&!app.QAIsAiming,"real input gate consumes flower gesture");
            yield return new WaitForFixedUpdate();Check(game.Shots==shots&&game.ActiveBalls==0,"decoration drag-release costs no ammunition and fires no balls");
            point=cam.WorldToScreenPoint(decor.SleepCatPosition);taps=decor.ClickCount;
            app.QABeginAim(point);Check(decor.ClickCount==taps+1&&!app.QAIsAiming,"sleeping cat click reacts without aiming");
            for(int i=0;i<18;i++){yield return new WaitForSecondsRealtime(1f/12);Capture("motion-"+i.ToString("D3"));}
            Check(!decor.TryTap(Vector2.zero),"central playfield never consumes aim input");
            app.QABeginAim(cam.WorldToScreenPoint(Vector2.zero));Check(app.QAIsAiming,"central pointer starts normal aim");
            app.QAReleaseAim(cam.WorldToScreenPoint(Vector2.zero));
            game.SetPaused(true);float clock=decor.AnimationClock;taps=decor.ClickCount;
            var paint=decor.GetComponentInChildren<SpriteRenderer>().sharedMaterial;
            float pose=paint.GetFloat("_MotionClock");
            yield return new WaitForSecondsRealtime(.3f);
            Check(decor.AnimationClock==clock&&paint.GetFloat("_MotionClock")==pose,"pause freezes all local motion");
            Check(!decor.TryTap(decor.PeekCatPosition)&&decor.ClickCount==taps,"pause rejects environment taps");
            game.SetPaused(false);yield return null;Check(decor.AnimationClock>clock,"resume advances environment clock");
            app.QAStartLevel(20);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.5f);Capture("stage-21");
            Check(game.GardenDecor!=decor&&FindObjectsByType<GardenSceneArt>(FindObjectsSortMode.None).Length==1,"level transition destroys old decoration set");
            Check(game.GardenDecor.ClickCount==0,"new level resets click state");
            app.QAStartLevel(CollectionLevels.First);yield return app.QAWaitForLevel();yield return new WaitForSecondsRealtime(.5f);Capture("collection");
            Check(game.IsCollection&&game.GardenDecor!=null,"collection scene retains its dedicated mechanics with new background");
            app.QANavigate("home");yield return null;
            Check(FindObjectsByType<GardenSceneArt>(FindObjectsSortMode.None).Length==0,"home clears environment and loops");
            Check(errors==0,"no runtime errors or exceptions");
            File.WriteAllText(Path.Combine(dir,"result.json"),"{\"checks\":"+checks+",\"failed\":"+failed+",\"errors\":"+errors+",\"width\":"+width+",\"height\":"+height+"}");
            cam.targetTexture=null;target.Release();Destroy(target);Application.Quit(failed==0?0:3);
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
int checks, failed, errors, width=540, height=1170;
```
```csharp
Camera cam;
```
```csharp
RenderTexture target;
```
```csharp
AppUI app;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
