# JellyWallQA · JellyWallQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/JellyWallQA.cs`
文件 SHA-256：`ec673b5bcf001ac23ad42ba81cf0bfd3fb71e58591d18d9258c15cafd8c50fda`
[原始源码快照](../source/Assets/Scripts/JellyWallQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L36

方法 SHA-256：`ac02f75ab886fc8c629f5d030b6c4cb972743c35aa661e0dcb750cfd808ebea5`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyWallQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start()
        {
            var args = Environment.GetCommandLineArgs(); dir = args[Array.IndexOf(args, "--qa-out") + 1];
            Directory.CreateDirectory(dir); File.WriteAllText(Path.Combine(dir, "checks.txt"), "");
            int size = Array.IndexOf(args, "--qa-size");
            if (size >= 0) { var v = args[size + 1].Split('x'); width = int.Parse(v[0]); height = int.Parse(v[1]); }
            Application.logMessageReceived += (m, stack, type) => {
                if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert) {
                    errors++; File.AppendAllText(Path.Combine(dir, "errors.txt"), m + "\n" + stack + "\n");
                }
            };
            yield return new WaitForSecondsRealtime(1);
            app = FindFirstObjectByType<AppUI>();
            Check(RuntimeQA.IsIsolated && app != null, "memory-only profile and application");
            cam = Camera.main; target = new RenderTexture(width, height, 24); target.Create(); cam.targetTexture = target;
            var canvas = FindFirstObjectByType<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = cam; canvas.planeDistance = 1; app.QASetViewport(new Vector2Int(width, height));
            app.QAStartLevel(1); yield return app.QAWaitForLevel(); yield return new WaitForSecondsRealtime(.8f);
            var game = app.Session; var walls = game.JellyWalls;
            Check(walls != null && walls.GetComponent<MeshRenderer>().sharedMaterial.shader.isSupported,
                "cream vine wall material supported by player GPU");
            Check(game.GetComponentsInChildren<JellyWallFrame>().Length == 1 &&
                game.GetComponentsInChildren<JellyWallContact>().Length == 3, "one seamless frame and three physical contacts");
            Check(game.transform.Find("WorldRoot/LeftWall/Boundary guide") == null, "placeholder wall sprites removed");
            Check(Mathf.Abs(JellyWallFrame.SideCenter - JellyWallFrame.HalfWidth - FlightGeometry.Right) < .00001f &&
                Mathf.Abs(JellyWallFrame.TopCenter - JellyWallFrame.HalfWidth - FlightGeometry.Top) < .00001f,
                "resting inner surfaces align with original collider faces");
            Check(JellyWallFrame.Envelope(0) == 0 && JellyWallFrame.Envelope(.04f) > 0 &&
                JellyWallFrame.Envelope(.19f) < 0 && Mathf.Abs(JellyWallFrame.Envelope(JellyWallFrame.Duration)) < .000001f,
                "compression, smaller rebound and exact 500ms rest");
            game.SetPaused(true); yield return new WaitForEndOfFrame(); Capture("01-rest"); game.SetPaused(false);
            var oldMode = Physics2D.simulationMode; Physics2D.simulationMode = SimulationMode2D.Script;
            for (int side = 0; side < 3; side++)
            {
                if (side > 0) { game.StartLevel(1); yield return new WaitForSecondsRealtime(.35f); walls = game.JellyWalls; }
                walls.Clear(); Check(game.TryShoot(Vector2.up), "fixture launch " + side); game.QAAdvanceOneStep();
                foreach (var b in game.GetComponentsInChildren<BallBody>()) if (b.Active) b.Body.simulated = false;
                var ball = game.GetComponentInChildren<BallBody>();
                Vector2 p = side == 0 ? new Vector2(-3.5f, -1) : side == 1 ? new Vector2(0, 6.9f) : new Vector2(3.5f, -1);
                Vector2 d = side == 0 ? Vector2.left : side == 1 ? Vector2.up : Vector2.right;
                ball.Initialize(ball.Id, game.Generation, p, d, 0);
                foreach (var brick in game.GetComponentsInChildren<BrickBody>()) brick.GetComponent<Collider2D>().enabled = false;
                Physics2D.SyncTransforms(); int before = walls.TotalImpacts;
                for (int step = 0; step < 6 && walls.TotalImpacts == before; step++) game.QAAdvanceOneStep();
                Check(walls.TotalImpacts > before && walls.ActiveImpacts > 0, "real ball collision triggers wall " + side);
                Check(Vector2.Dot(ball.Body.linearVelocity, d) < 0 &&
                    Mathf.Abs(ball.Body.linearVelocity.magnitude - game.CurrentLevel.ballSpeed) < .002f,
                    "solver reflection and configured speed retained " + side);
                walls.Advance(.04f);
                Vector2 contact = side == 0 ? new Vector2(FlightGeometry.Left, -1) :
                    side == 1 ? new Vector2(0, FlightGeometry.Top) : new Vector2(FlightGeometry.Right, -1);
                Check(walls.Deformation(contact, side) > .025f, "contact has visible local dent " + side);
                Vector2 far = contact + (side == 1 ? Vector2.right : Vector2.up) * 2;
                Check(Mathf.Abs(walls.Deformation(far, side)) < .00001f, "distant wall stays still " + side);
                game.SetPaused(true); float frozen = walls.Deformation(contact, side); walls.Advance(.2f);
                int frozenCount = walls.TotalImpacts; walls.Impact(contact, side, 10);
                Check(walls.Deformation(contact, side) == frozen && walls.TotalImpacts == frozenCount,
                    "pause freezes clocks and rejects new impact " + side);
                if (side == 2) { yield return new WaitForEndOfFrame(); Capture("02-impact"); }
                game.SetPaused(false); walls.Advance(JellyWallFrame.Duration);
                Check(walls.ActiveImpacts == 0 && walls.Deformation(contact, side) == 0, "all deformation settles " + side);
            }
            walls.Clear();
            for (int i = 0; i < 48; i++) walls.Impact(new Vector2(FlightGeometry.Right, -5 + i * .23f), 2, 30);
            walls.Advance(.04f);
            Check(walls.ActiveImpacts == JellyWallFrame.Capacity, "rapid contacts use bounded 16-slot storage");
            Check(Mathf.Abs(walls.Deformation(new Vector2(FlightGeometry.Right, 0), 2)) <= JellyWallFrame.MaximumDent,
                "overlapping impacts have a safe amplitude limit");
            walls.Clear(); walls.Impact(new Vector2(FlightGeometry.Right, -1), 2, 10);
            walls.Impact(new Vector2(FlightGeometry.Right, -1), 2, 10);
            Check(walls.ActiveImpacts == 1, "same-place rapid volley coalesces");
            walls.Clear(); walls.Impact(new Vector2(FlightGeometry.Right, -1), 2, 10);
            walls.Impact(new Vector2(FlightGeometry.Left, 1), 0, 10);
            Check(walls.ActiveImpacts == 2, "separate contacts animate independently");
            // Deterministic 60fps animation frames of the live shader. Keep the
            // fixture clock explicit so PNG readback time does not shorten motion.
            walls.enabled = false; game.enabled = false; walls.Clear();
            foreach (var b in game.GetComponentsInChildren<BallBody>()) if (b.Active) b.Body.simulated = false;
            var demoBall = game.GetComponentInChildren<BallBody>();
            demoBall.Initialize(demoBall.Id, game.Generation, new Vector2(3.5f, -1), Vector2.right, 0);
            Physics2D.SyncTransforms(); int demoBefore = walls.TotalImpacts;
            for (int i = 0; i < 22; i++)
            {
                Physics2D.Simulate(1f / 60); walls.Advance(1f / 60);
                yield return new WaitForEndOfFrame(); Capture("motion-" + i.ToString("D2"));
            }
            Check(walls.TotalImpacts > demoBefore, "animation evidence uses real ball contact");
            game.enabled = true; walls.enabled = true;
            for (int level = 0; level < LevelCatalog.Count; level++)
            {
                game.StartLevel(level); yield return new WaitForSecondsRealtime(.30f);
                Check(game.JellyWalls != null && game.JellyWalls.ActiveImpacts == 0 &&
                    game.GetComponentsInChildren<JellyWallContact>().Length == 3, "fresh frame in level " + (level + 1));
                if (level == 6) { game.SetPaused(true); yield return new WaitForEndOfFrame(); Capture("03-ability-stage"); game.SetPaused(false); }
            }
            var oldWalls = game.JellyWalls; oldWalls.Impact(new Vector2(4.1f, 0), 2, 10);
            game.StopLevel(); Check(game.JellyWalls == null && oldWalls.ActiveImpacts == 0, "exit clears and detaches wall state");
            yield return null; Check(oldWalls == null, "exit destroys old surface and owned render resources");
            Physics2D.simulationMode = oldMode; cam.targetTexture = null; target.Release(); Destroy(target);
            File.AppendAllText(Path.Combine(dir, "checks.txt"), "RESULT checks=" + checks + " failed=" + failed + " errors=" + errors + "\n");
            Application.Quit(failed == 0 && errors == 0 ? 0 : 4);
        }
```

## 对象层级和素材入口

- L59：`game.transform.Find("WorldRoot/LeftWall/Boundary guide")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
string dir;
```
```csharp
int checks, failed, errors, width = 540, height = 1170;
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
