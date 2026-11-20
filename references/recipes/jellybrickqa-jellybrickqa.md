# JellyBrickQA · JellyBrickQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/JellyBrickQA.cs`
文件 SHA-256：`7fae7b8585623abb1c2fde07ef37517c6ccb444e1347d2553fe0fea102ad7fa3`
[原始源码快照](../source/Assets/Scripts/JellyBrickQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L30

方法 SHA-256：`0e41c3e7ee2b02c54a77cf476488b972ea17d471658547fc555b000776a7a87a`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyBrickQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start()
        {
            var args = Environment.GetCommandLineArgs();
            dir = args[Array.IndexOf(args, "--qa-out") + 1]; Directory.CreateDirectory(dir);
            Application.logMessageReceived += Log;
            yield return new WaitForSecondsRealtime(1);
            var app = FindFirstObjectByType<AppUI>();
            Check(RuntimeQA.IsIsolated, "test uses isolated memory-only profile");
            var atlas = JellyBrickArt.Rest.texture;
            Check(atlas.width == 1024 && atlas.height == 1024, "single 1024-square runtime atlas");
            Check(!atlas.isReadable && atlas.mipmapCount == 1, "no CPU pixel copy and no mipmaps");
            Check(JellyBrickArt.Frames.Length == 16, "sixteen cached sprite views");
            foreach (var frame in JellyBrickArt.Frames) Check(frame.texture == atlas, "all frames share one texture " + frame.name);
            File.WriteAllText(Path.Combine(dir, "memory.txt"), "Atlas format=" + atlas.format + " runtimeBytes=" + Profiler.GetRuntimeMemorySizeLong(atlas) + "\n");
            for (int level = 0; level < 6; level++)
            {
                app.QAStartLevel(level);
                // Wait for the loading overlay to finish even on a slow first frame.
                do { yield return null; }
                while (GameObject.Find("Loading") != null || app.Session.LevelIndex != level || !app.Session.IsPlaying);
                yield return new WaitForSecondsRealtime(.65f);
                var game = app.Session; game.SetPaused(false);
                var bricks = game.GetComponentsInChildren<BrickBody>();
                var pool = game.GetComponentInChildren<JellyPopPool>();
                pool.enabled = false;
                Check(game.CurrentLevel.obstacles.Length == 0, "no obstacle spawn data level=" + (level + 1));
                Check(game.GetComponentsInChildren<MovingObstacle>(true).Length == 0, "no rotors paddles or internal walls level=" + (level + 1));
                Check(game.GetComponentsInChildren<ElectricPillarContact>(true).Length == 0 && game.Circuit.PillarCount == 0, "no electric pillars level=" + (level + 1));
                foreach(var collider in game.GetComponentsInChildren<Collider2D>(true))
                    Check(collider.GetComponentInParent<BrickBody>(true) != null || collider.GetComponentInParent<BallBody>(true) != null || collider.name == "LeftWall" || collider.name == "RightWall" || collider.name == "TopWall" || collider.name == "WaterVolume", "only bricks balls and playfield boundaries have colliders level=" + (level + 1) + " object=" + collider.name);

                Check(bricks.Length == game.CurrentLevel.bricks.Length, "all authored targets spawn level=" + (level + 1));
                Check(pool.Capacity >= bricks.Length, "pool prewarmed for whole level=" + (level + 1));
                foreach (var brick in bricks)
                {
                    Check(brick.UsesJelly && brick.Face.sprite == JellyBrickArt.Rest, "jelly face level=" + (level + 1) + " target=" + brick.Id);
                    Check(brick.Face.sharedMaterial == bricks[0].Face.sharedMaterial, "shared brick material level=" + (level + 1) + " target=" + brick.Id);
                    Check(brick.GetComponent<Collider2D>().enabled, "original collider remains level=" + (level + 1) + " target=" + brick.Id);
                    if (brick.Definition.shutter) Check(brick.transform.Find("VisualRoot/Pastel shutter frame") == null, "shutter face also replaced");
                }
                Capture("level-" + (level + 1));

                // Exercise the actual damage/removal/feedback path with a valid session ball.
                var ballObj = new GameObject("Jelly QA contact ball"); ballObj.transform.SetParent(game.transform, false);
                var ball = ballObj.AddComponent<BallBody>(); ball.Create(game, null);
                ball.Initialize(9000 + level, game.Generation, new Vector2(0, -4), Vector2.zero, 0);
                ball.Body.simulated = false;
                var target = bricks[0];
                int expectedHp = target.Definition.hp;
                int played = pool.Played;
                for (int hp = expectedHp; hp > 0; hp--)
                {
                    game.HitBrick(target, ball, target.transform.position);
                    if (hp > 1) Check(target.Alive && pool.Played == played, "nonfatal hit retains jelly without pop level=" + (level + 1));
                    if (hp > 1) yield return new WaitForFixedUpdate();
                }
                Check(!target.Alive && !target.gameObject.activeSelf && !target.GetComponent<Collider2D>().enabled, "fatal hit removes brick and collider level=" + (level + 1));
                Check(pool.Played == played + 1 && pool.ActiveCount == 1, "fatal hit starts exactly one flipbook level=" + (level + 1));
                var fx = game.GetComponentInChildren<WorldFx>();
                Check(fx.ActiveCount <= expectedHp - 1, "no legacy debris emitted on jelly break level=" + (level + 1));
                game.SetPaused(true); pool.Advance(1);
                Check(pool.ActiveCount == 1, "pause freezes pop level=" + (level + 1));
                game.SetPaused(false);
                if (level == 0)
                {
                    for (int frame = 0; frame < 16; frame++)
                    {
                        Capture("pop-" + frame.ToString("D2"));
                        pool.Advance(.04001f);
                    }
                }
                else pool.Advance(1);
                Check(pool.ActiveCount == 0, "finished pop returned to pool level=" + (level + 1));
                Destroy(ballObj);

                // Full-board simultaneous clear, then repeated steady-state reuse.
                var point = bricks[0].JellyPosition; var scale = bricks[0].RestFaceScale;
                for (int i = 0; i < bricks.Length; i++) pool.Play(bricks[i].JellyPosition, bricks[i].RestFaceScale, 35);
                Check(pool.ActiveCount == bricks.Length && pool.Replaced == 0, "full-board burst fits pool level=" + (level + 1));
                if (level == 2) { pool.Advance(.24f); Capture("full-board-burst"); }
                pool.Advance(1);
                for (int n = 0; n < 32; n++) { pool.Play(point, scale, 35); pool.Advance(1); }
                long before = GC.GetAllocatedBytesForCurrentThread();
                for (int n = 0; n < 10000; n++) { pool.Play(point, scale, 35); pool.Advance(1); }
                long allocated = GC.GetAllocatedBytesForCurrentThread() - before;
                Check(allocated == 0, "10000 reused play/update cycles allocate zero managed bytes level=" + (level + 1) + " actual=" + allocated);
                Check(pool.ActiveCount == 0 && pool.Replaced == 0, "no retained live effects or overflow level=" + (level + 1));
                File.AppendAllText(Path.Combine(dir, "memory.txt"), "level=" + (level + 1) + " targets=" + bricks.Length + " pool=" + pool.Capacity + " peak=" + pool.Peak + " reusedCycles=10000 managedBytes=" + allocated + "\n");
                // A completed shutter must retain its reveal behavior after the art swap.
                BrickBody shutter = null;
                foreach (var brick in bricks) if (brick.Definition.shutter && brick.Definition.reveal == "target") shutter = brick;
                if (shutter != null)
                {
                    ballObj = new GameObject("Jelly QA shutter ball"); ballObj.transform.SetParent(game.transform, false);
                    ball = ballObj.AddComponent<BallBody>(); ball.Create(game, null);
                    ball.Initialize(19000 + level, game.Generation, new Vector2(0, -4), Vector2.zero, 0); ball.Body.simulated = false;
                    int originalTargets = game.RemainingTargets;
                    for (int hp = shutter.Definition.hp; hp > 0; hp--) { game.HitBrick(shutter, ball, shutter.transform.position); yield return new WaitForFixedUpdate(); }
                    Check(!shutter.Alive && game.RemainingTargets == originalTargets, "shutter reveals replacement target level=" + (level + 1));
                    bool revealed = false;
                    foreach (var brick in game.GetComponentsInChildren<BrickBody>()) if (brick.Id > bricks.Length) revealed |= brick.UsesJelly && brick.Face.sprite == JellyBrickArt.Rest;
                    Check(revealed, "revealed target also uses jelly level=" + (level + 1));
                    Destroy(ballObj);
                }

                foreach(var formerMechanism in bricks) if(formerMechanism.Alive && formerMechanism.Definition.shutter && string.IsNullOrEmpty(formerMechanism.Definition.reveal))
                {
                    ballObj = new GameObject("Bricks-only QA ball"); ballObj.transform.SetParent(game.transform, false);
                    ball = ballObj.AddComponent<BallBody>(); ball.Create(game, null);
                    ball.Initialize(29000 + level, game.Generation, new Vector2(0, -4), Vector2.zero, 0); ball.Body.simulated = false;
                    int targetsBefore = game.RemainingTargets;
                    for(int hp=formerMechanism.Definition.hp;hp>0;hp--){game.HitBrick(formerMechanism,ball,formerMechanism.transform.position);yield return new WaitForFixedUpdate();}
                    Check(!formerMechanism.Alive && game.RemainingTargets == targetsBefore - 1, "former rotor brick clears without replacement level=" + (level + 1));
                    Check(game.GetComponentsInChildren<MovingObstacle>(true).Length == 0, "destroying former mechanism cannot spawn obstacles level=" + (level + 1));
                    Destroy(ballObj);
                }
                game.StopLevel(); yield return null;
                Check(game.GetComponentInChildren<JellyPopPool>() == null, "level stop releases pool level=" + (level + 1));
            }
            Check(errors == 0, "zero runtime errors");
            File.AppendAllText(Path.Combine(dir, "checks.txt"), "RESULT checks=" + checks + " failed=" + failed + " errors=" + errors + "\n");
            Application.Quit(failed == 0 && errors == 0 ? 0 : 3);
        }
```

## 对象层级和素材入口

- L49：`GameObject.Find("Loading")`
- L68：`brick.transform.Find("VisualRoot/Pastel shutter frame")`
- L73：`ballObj.transform.SetParent(game.transform, false)`
- L123：`ballObj.transform.SetParent(game.transform, false)`
- L137：`ballObj.transform.SetParent(game.transform, false)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
string dir;
```
```csharp
int checks, failed, errors;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
