# FusionQA · FusionQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LevelEditor/FusionQA.cs`
文件 SHA-256：`5dbfda4362ec5d980163574c33f79a5480ed958d24d4ec798cd61b04350a8853`
[原始源码快照](../source/Assets/Scripts/LevelEditor/FusionQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Start · L65

方法 SHA-256：`17f23fa97c9be79b8d7db279206f6f0a86682baf3502347015b3d953a558324c`
时间来源：Time.UtcNow, Time.timeScale
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.FusionQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start() {
            started = DateTime.UtcNow.ToString("o");
            output = Path.GetFullPath(LevelEditorRuntime.Arg("--fusion-qa-out", Path.Combine(Application.persistentDataPath, "FusionQA")));
            Directory.CreateDirectory(output); Directory.CreateDirectory(Path.Combine(output, "runs")); Directory.CreateDirectory(Path.Combine(output, "captures"));
            if (!int.TryParse(LevelEditorRuntime.Arg("--fusion-seeds", "10"), out seeds)) seeds = 10;
            seeds = Mathf.Clamp(seeds, 1, 100);
            Application.logMessageReceived += OnLog;
            profileBefore = PlayerPrefs.GetString("BounceGarden.LocalProfile.v1", "<missing>");
            Check("QA process is isolated", RuntimeQA.IsIsolated);
            foreach (string chapter in new[] {"Opening"}) {
                var asset = Resources.Load<TextAsset>(chapter + "/levels");
                Check("bundled campaign exists " + chapter, asset != null);
                if (asset != null) campaignHashes[chapter] = Hash(asset.text);
            }
            externalMode = LevelEditorRuntime.HasArg("--fusion-samples");
            superRangeRegression = LevelEditorRuntime.HasArg("--fusion-super-range-qa");
            flowRegression = LevelEditorRuntime.HasArg("--fusion-flow-qa");
            FusionSampleDefinition[] samples = Array.Empty<FusionSampleDefinition>(); string[] superSampleIds = Array.Empty<string>();
            if (externalMode) {
                bool inputReady = false;
                try {
                    string argument = LevelEditorRuntime.Arg("--fusion-samples");
                    if (string.IsNullOrWhiteSpace(argument) || argument.StartsWith("--", StringComparison.Ordinal)) throw new ArgumentException("--fusion-samples后必须提供JSON路径。");
                    sourceSamplesPath = Path.GetFullPath(argument);
                    var pack = FusionExternalSamples.Load(sourceSamplesPath);
                    samples = pack.samples; superSampleIds = pack.superSampleIds;
                    using (var sha = SHA256.Create()) sourceSamplesSha256 = BitConverter.ToString(sha.ComputeHash(File.ReadAllBytes(sourceSamplesPath))).Replace("-", "").ToLowerInvariant();
                    File.Copy(sourceSamplesPath, Path.Combine(output, "input-samples.json"), false);
                    ExternalDataChecks(samples);
                    inputReady = true;
                } catch (Exception ex) { Check("external samples load and validate", false, ex.ToString()); }
                if (!inputReady) { Finish(0); yield break; }
            } else {
                samples = FusionSamples.CreateDefinitions(); superSampleIds = new[] { "P-03", "P-07" };
                Check("seven independent samples", samples.Length == 7 && samples.Select(s => s.id).Distinct().Count() == 7);
                try { DataChecks(samples); } catch (Exception ex) { Check("sample authoring regression", false, ex.ToString()); }
                try { FusionAuthoringQA.DataChecks(Check); } catch (Exception ex) { Check("fusion authoring constraints execute", false, ex.ToString()); }
            }
            yield return null;
            oldMode = Physics2D.simulationMode; oldTimeScale = Time.timeScale; capturedSimulation = true;
            Physics2D.simulationMode = SimulationMode2D.Script; Time.timeScale = 0;
            session = new GameObject("Fusion QA production GameSession").AddComponent<GameSession>();
            session.enabled = false; session.QAIgnoreFocusLoss = true;
            if (superRangeRegression) yield return FusionSuperRangeQA.Run(session, output, Check);
            if (flowRegression) yield return FusionFlowQA.Run(session, Check);
            if (!externalMode) {
                yield return FusionRearQA.Run(session, Check);
                yield return RuleFixtures(samples);
                yield return FusionDeviceQA.Run(session, Check);
                yield return FusionCoreQA.Run(session, Check);
                yield return LifecycleChecks(samples[0]);
                yield return BoosterFixtures(samples[0]);
                yield return EditorBoosterScenarioChecks(samples[0]);
            }
            foreach (var sample in samples) {
                for (int policy = 0; policy < 3; policy++) for (int seedIndex = 0; seedIndex < seeds; seedIndex++) {
                    yield return NaturalRun(sample, policy, seedIndex);
                    session.StopLevel();
                    yield return null; // Destroy must complete before another board simulates.
                }
                var sampleRuns = runs.Where(r => r.sample == sample.id).ToArray();
                Check(sample.id + " has a naturally completed route", sampleRuns.Any(r => r.won),
                    "natural wins=" + sampleRuns.Count(r => r.won) + "/" + sampleRuns.Length + "; bot exploration, not human win rate");
                Debug.Log("FUSION_QA_PROGRESS " + sample.id + " runs=" + sampleRuns.Length + " naturalWins=" + sampleRuns.Count(r => r.won));
            }
            foreach (var sample in samples.Where(s => superSampleIds.Contains(s.id)))
                for (int seedIndex = 0; seedIndex < seeds; seedIndex++) foreach (string scenario in new[] { "finite_super", "unlimited_super" }) {
                    yield return NaturalRun(sample, 0, seedIndex, scenario);
                    session.StopLevel(); session.BoosterProfile = null; yield return null;
                }
            session.StopLevel(); Destroy(session.gameObject); session = null;
            RestoreSimulation();
            yield return null;
            Finish(samples.Length);
        }
```

### RuleFixtures · L234

方法 SHA-256：`e987e6d9c1f8801cb35025ccd8564d2acd4904891fc7c29e6d0ff488f6d80d21`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.FusionQA.RuleFixtures(BounceGarden.FusionSampleDefinition[])`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/FusionQA.cs:112` · `FusionQA.Start` → `RuleFixtures(samples)`

```csharp
IEnumerator RuleFixtures(FusionSampleDefinition[] samples) {
            // Explicit event injection checks identity/stage rules only; never contributes to natural-run outcomes.
            try {
                session.StartFusionSample(Clone(samples[1].definition), samples[1].moves, "QA rule_fixture protection", 171);
                session.enabled = false;
                var piece = session.FusionPieces.First(p => p.Definition.armor);
                int hp = piece.Brick.CurrentHealth, total = session.FusionGoalsTotal;
                session.FusionRunAttack("qa:compound", 1, () => {
                    session.FusionHit(piece, 99, Vector2.up, piece.Position, true);
                    session.FusionHit(piece, 99, Vector2.up, piece.Position);
                });
                Check("rule_fixture compound attack removes only protection", !piece.Brick.ShellIntact && piece.Alive && piece.Brick.CurrentHealth == hp && session.FusionGoalsCredited == 0);
                session.FusionRunAttack("qa:independent", 1, () => session.FusionHit(piece, 1, Vector2.up, piece.Position));
                Check("rule_fixture independent attack advances exposed body", !piece.Alive && session.FusionGoalsCredited == 1 && session.FusionGoalsTotal == total);
                session.FusionRunAttack("qa:duplicate_deleted", 1, () => session.FusionHit(piece, 99, Vector2.up, piece.Position));
                Check("rule_fixture credited identity cannot be collected twice", session.FusionGoalsCredited == 1);
            } catch (Exception ex) { Check("rule_fixture protection executes", false, ex.ToString()); }
            session.StopLevel(); yield return null;
            try {
                session.StartFusionSample(Clone(samples[2].definition), samples[2].moves, "QA rule_fixture multi-cell", 172);
                session.enabled = false;
                var chest = session.FusionPieces.First(p => p.Definition.kind == "chest");
                int before = chest.Brick.CurrentHealth, total = session.FusionGoalsTotal;
                session.FusionRunAttack("qa:four_cells", 1, () => { for (int i = 0; i < 4; i++) session.FusionHit(chest, 1, Vector2.up, chest.Position); });
                Check("rule_fixture four occupied cells receive one attack", chest.Alive && chest.Brick.CurrentHealth == before - 1);
                session.FusionRunAttack("qa:open_chest", 1, () => session.FusionHit(chest, 99, Vector2.up, chest.Position));
                for (int i = 0; i < 8; i++) session.QAAdvanceOneStep();
                var ids = chest.Definition.contents.Select(c => c.id).ToArray();
                Check("rule_fixture chest activates four reserved identities", ids.All(id => session.FusionPieces.Any(p => p.Definition.id == id && p.Alive)) && session.FusionGoalsTotal == total);
                Check("rule_fixture opening chest does not credit its contents", session.FusionGoalsCredited == 0);
            } catch (Exception ex) { Check("rule_fixture chest executes", false, ex.ToString()); }
            session.StopLevel(); yield return null;
            try {
                session.StartFusionSample(Clone(samples[5].definition), samples[5].moves, "QA rule_fixture spawn snapshot", 173);
                session.enabled = false;
                var producer = session.FusionPieces.First(p => p.Definition.kind == "producer");
                FusionRuntimePiece child = null; int total = session.FusionGoalsTotal;
                session.FusionRunAttack("qa:spawn_then_cover", 1, () => {
                    child = session.FusionSpawnContent(producer.Definition.contents[0], 4, 4);
                    session.FusionHit(child, 99, Vector2.up, child != null ? child.Position : Vector2.zero);
                });
                Check("rule_fixture parent snapshot excludes newly activated content", child != null && child.Alive && child.Brick.CurrentHealth == 1 && session.FusionGoalsCredited == 0 && session.FusionGoalsTotal == total);
            } catch (Exception ex) { Check("rule_fixture spawn snapshot executes", false, ex.ToString()); }
            session.StopLevel(); yield return null;
        }
```

### LifecycleChecks · L280

方法 SHA-256：`a606f045092e4ab261c7166caa332656b0fa4dfba9831e9621aa2e3b06a3a9b7`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.FusionQA.LifecycleChecks(BounceGarden.FusionSampleDefinition)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/FusionQA.cs:115` · `FusionQA.Start` → `LifecycleChecks(samples[0])`

```csharp
IEnumerator LifecycleChecks(FusionSampleDefinition source) {
            // A labelled stress fixture makes every collectible durable. It is never a natural run or difficulty sample.
            var definition = Clone(source.definition);
            foreach (var p in definition.pieces) { if (FusionRules.IsTool(p.kind)) p.kind = "normal"; p.hp = 99; }
            bool startedFixture = false;
            try {
                session.StartFusionSample(definition, 8, "QA lifecycle fixture (not a player sample)", 731);
                session.enabled = false; session.QAIgnoreFocusLoss = true; startedFixture = true;
                for (int i = 0; i < 80 && !session.ChainCanLaunch; i++) session.QAAdvanceOneStep();
                bool first = session.TryShoot(new Vector2(1, .28f));
                for (int i = 0; i < 8; i++) session.QAAdvanceOneStep();
                int shots = session.Shots, pending = session.PendingBurstBalls, step = session.PhysicsStep;
                session.SetPaused(true);
                bool pauseRejected = !session.TryShoot(Vector2.up);
                for (int i = 0; i < 20; i++) session.QAAdvanceOneStep();
                Check("lifecycle fixture pause preserves steps and pending balls", first && pauseRejected && session.Shots == shots && session.PendingBurstBalls == pending && session.PhysicsStep == step);
                session.SetPaused(false);
                for (int i = 0; i < 40 && !session.ChainCanLaunch; i++) session.QAAdvanceOneStep();
                bool hadOldBalls = session.ActiveBalls > 0;
                bool second = session.TryShoot(new Vector2(.15f, 1));
                for (int i = 0; i < 32; i++) session.QAAdvanceOneStep();
                Check("lifecycle fixture two five-ball groups cost exactly two steps", second && session.Shots == 6 && session.SpawnCount == 10, "second=" + second + " shots=" + session.Shots + " spawned=" + session.SpawnCount);
                Check("lifecycle fixture accepts second launch while first balls live", hadOldBalls && second, "oldBalls=" + hadOldBalls + " second=" + second);
                Check("lifecycle fixture records two causal volleys", session.FusionMetrics.Volleys.Count == 2, "groups=" + session.FusionMetrics.Volleys.Count);
            } catch (Exception ex) { Check("lifecycle fixture executes", false, ex.ToString()); }
            if (startedFixture) {
                int oldGeneration = session.Generation;
                session.StopLevel(); yield return null;
                try {
                    session.StartFusionSample(Clone(source.definition), source.moves, source.name, 732);
                    session.enabled = false; session.QAIgnoreFocusLoss = true;
                    Check("restart clears prior session resources and events", session.Generation != oldGeneration && session.ActiveBalls == 0 && session.PendingBurstBalls == 0 && session.Shots == source.moves && session.FusionMetrics.Volleys.Count == 0);
                } catch (Exception ex) { Check("restart fixture executes", false, ex.ToString()); }
            }
            session.StopLevel(); yield return null;
        }
```

### BoosterFixtures · L317

方法 SHA-256：`a821894eb08b1f14e2484869dd8bd961a86e874a7bda39f46f7e44b29ec883a3`
时间来源：Time.fixedDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.FusionQA.BoosterFixtures(BounceGarden.FusionSampleDefinition)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/FusionQA.cs:116` · `FusionQA.Start` → `BoosterFixtures(samples[0])`

```csharp
IEnumerator BoosterFixtures(FusionSampleDefinition source) {
            foreach (bool enhanced in new[] { false, true }) {
                try {
                    var definition = Clone(source.definition);
                    foreach (var p in definition.pieces) { if (FusionRules.IsTool(p.kind)) p.kind = "normal"; p.hp = 99; }
                    definition.finiteBoosters = true; definition.superPerBall = 1;
                    session.StartFusionSample(definition, 8, "QA rule_fixture " + (enhanced ? "finite super" : "ordinary"), 811);
                    session.enabled = false; session.QAIgnoreFocusLoss = true;
                    bool accepted = session.TryShoot(new Vector2(1, .28f));
                    var assigned = new HashSet<int>(); int peak = 0;
                    for (int i = 0; i < Mathf.CeilToInt(14f / Time.fixedDeltaTime); i++) {
                        // Explicit fixture injection seeds each spawned ball's ability, not inventory.
                        // Inventory selection/consumption is verified separately through the editor scenario.
                        foreach (var ball in FindObjectsByType<BallBody>(FindObjectsSortMode.None).Where(b => b.Active && b.SessionGeneration == session.Generation))
                            if (assigned.Add(ball.Id) && enhanced) ball.SetStreakBooster(StreakBooster.Super);
                        session.QAAdvanceOneStep(); peak = Math.Max(peak, session.ActiveBalls);
                        if (session.PendingBurstBalls == 0 && session.ActiveBalls == 0 && i > 40) break;
                    }
                    var procs = session.FusionEvents.Where(e => e.Kind == "BoosterProc").ToArray();
                    Check("rule_fixture " + (enhanced ? "finite super" : "ordinary") + " emits five physical balls for one move", accepted && session.SpawnCount == 5 && session.Shots == 7 && assigned.Count == 5, "spawned=" + session.SpawnCount + " assigned=" + assigned.Count + " peak=" + peak);
                    Check("rule_fixture " + (enhanced ? "super has at most one activation per ball" : "ordinary has zero power activations"), enhanced
                        ? procs.Length > 0 && procs.Length <= 5 && procs.GroupBy(e => e.TargetId).All(g => g.Count() <= 1)
                        : procs.Length == 0, "powerActivations=" + procs.Length);
                    Check("rule_fixture booster run keeps initial goal total", session.FusionGoalsTotal == FusionRules.Goals(definition).Count && string.IsNullOrEmpty(session.FusionRuleError));
                    File.WriteAllText(Path.Combine(output, "booster-fixture-" + (enhanced ? "finite-super" : "ordinary") + ".json"), JsonUtility.ToJson(session.FusionMetrics, true), new UTF8Encoding(false));
                } catch (Exception ex) { Check("rule_fixture booster executes", false, ex.ToString()); }
                session.StopLevel(); yield return null;
            }
        }
```

### EditorBoosterScenarioChecks · L347

方法 SHA-256：`5fdd439f6263e334898149e9f207ea57002c046b7a564c61b27a277f1bfd9cf2`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.FusionQA.EditorBoosterScenarioChecks(BounceGarden.FusionSampleDefinition)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/FusionQA.cs:117` · `FusionQA.Start` → `EditorBoosterScenarioChecks(samples[0])`

```csharp
IEnumerator EditorBoosterScenarioChecks(FusionSampleDefinition source) {
            var definition = Clone(source.definition);
            foreach (var p in definition.pieces) { if (FusionRules.IsTool(p.kind)) p.kind = "normal"; p.hp = 99; }
            var workspace = LevelEditorWorkspace.CreateEmpty();
            var level = workspace.CreateFusionLevel("Alphabet", "QA editor inventory fixture", definition, 8);
            var runner = new GameObject("Fusion editor inventory QA").AddComponent<LevelEditorPlaytest>();
            foreach (string scenario in new[] { "bare", "stocked", "streak10" }) {
                try {
                    bool started = runner.StartDraft(level, scenario);
                    Check("editor " + scenario + " fusion scenario starts", started, runner.LastError);
                    if (started) {
                        var game = runner.Session; game.enabled = false; game.QAIgnoreFocusLoss = true;
                        int expected = scenario == "bare" ? 0 : scenario == "stocked" ? 3 : 1;
                        Check("editor " + scenario + " Super inventory is isolated", game.StreakStock(StreakBooster.Super) == expected, "actual=" + game.StreakStock(StreakBooster.Super));
                        bool selected = game.SelectStreakBooster(StreakBooster.Super);
                        if (expected == 0) Check("bare scenario cannot select unavailable Super", !selected);
                        else {
                            bool shot = game.TryShoot(new Vector2(1, .28f)); var observed = new HashSet<int>();
                            for (int i = 0; i < 50; i++) {
                                game.QAAdvanceOneStep();
                                foreach (var ball in FindObjectsByType<BallBody>(FindObjectsSortMode.None).Where(b => b.Active && b.SessionGeneration == game.Generation))
                                    if (ball.StreakPower == StreakBooster.Super && game.FusionBallCharges(ball) == 1) observed.Add(ball.Id);
                            }
                            Check("editor " + scenario + " selection consumes exactly one item and move", selected && shot && game.StreakStock(StreakBooster.Super) == expected - 1 && game.Shots == 7 && game.SelectedStreakBooster == StreakBooster.None);
                            Check("editor " + scenario + " all five real balls receive finite Super", game.SpawnCount == 5 && observed.Count == 5, "spawned=" + game.SpawnCount + " observed=" + observed.Count);
                            Check("editor " + scenario + " per-ball Super usage cannot exceed one", game.FusionEvents.Where(e => e.Kind == "BoosterProc").GroupBy(e => e.TargetId).All(g => g.Count() <= 1));
                        }
                    }
                } catch (Exception ex) { Check("editor booster scenario executes " + scenario, false, ex.ToString()); }
                runner.Stop(); yield return null;
            }
            Destroy(runner.gameObject); yield return null;
        }
```

### NaturalRun · L381

方法 SHA-256：`68184c8ea15323b34321590b25fc395b0541d9d382cbd69cac309bf4fd3a7538`
时间来源：Time.fixedDeltaTime, Time.realtimeSinceStartup
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.LevelEditing.FusionQA.NaturalRun(BounceGarden.FusionSampleDefinition, int, int, string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/FusionQA.cs:121` · `FusionQA.Start` → `NaturalRun(sample, policy, seedIndex)`
- `Assets/Scripts/LevelEditor/FusionQA.cs:132` · `FusionQA.Start` → `NaturalRun(sample, 0, seedIndex, scenario)`

```csharp
IEnumerator NaturalRun(FusionSampleDefinition sample, int policy, int seedIndex, string scenario = "bare") {
            string[] policies = { "visible_tools", "visible_objectives", "wide_lane_rotation" };
            var definition = Clone(sample.definition); if (scenario == "unlimited_super") definition.finiteBoosters = false;
            var run = new Run { sample = sample.id, name = sample.name, policy = policies[policy], scenario = scenario, cadence = policy == 0 ? "observe_settled" : "rapid_0.56s", seed = 20261111 + seedIndex * 1319,
                draftSha256 = Hash(JsonUtility.ToJson(definition)), result = "not_started" };
            activeNaturalRun = run;
            var inputs = new List<InputRecord>();
            float wallStart = Time.realtimeSinceStartup;
            Action<bool> outcome = won => { run.outcomeCount++; run.won = won; run.completed = true; };
            session.Outcome += outcome;
            bool startedRun = false;
            try {
                session.StartFusionSample(definition, sample.moves, sample.name, run.seed);
                session.enabled = false; session.QAIgnoreFocusLoss = true;
                session.BoosterProfile = scenario == "bare" ? null : LocalProfile.CreateLevelEditorProfile(9, "stocked");
                run.goalsTotal = session.FusionGoalsTotal; startedRun = true;
                Check(sample.id + " runtime accepts configured steps " + policy + "/" + seedIndex, session.Shots == sample.moves);
            } catch (Exception ex) { run.error = ex.ToString(); Check(sample.id + " runtime start", false, run.error); }
            int limit = Mathf.CeilToInt(Mathf.Max(90f, sample.moves * 15f + 10) / Time.fixedDeltaTime), iterations = 0;
            float nextAim = 0, settledSince = -1;
            bool goalConserved = true, tookFirstCapture = false, tookProgressCapture = false, tookTailCapture = false;
            while (startedRun && session.IsPlaying && session.PhysicsStep < limit && iterations++ < limit + 512) {
                try {
                    if (!string.IsNullOrEmpty(session.FusionRuleError)) throw new InvalidOperationException(session.FusionRuleError);
                    float time = session.PhysicsStep * Time.fixedDeltaTime;
                    bool settled = session.ActiveBalls == 0 && session.PendingBurstBalls == 0 && !session.FusionActionsPending && session.ChainGridReady;
                    if (settled) { if (settledSince < 0) settledSince = time; } else settledSince = -1;
                    bool decisionReady = policy != 0 || inputs.Count == 0 || settledSince >= 0 && time - settledSince >= .20f;
                    if (session.ChainCanLaunch && time >= nextAim && decisionReady) {
                        if (scenario != "bare" && inputs.Count == 0)
                            Check(sample.id + " " + scenario + " selects actual inventory Super", session.SelectStreakBooster(StreakBooster.Super));
                        float angle = ChooseAngle(policy, inputs.Count, seedIndex);
                        var input = new InputRecord { physicsStep = session.PhysicsStep, simulationTime = time, shotsBefore = session.Shots, angle = angle };
                        float radians = angle * Mathf.Deg2Rad;
                        input.accepted = session.TryShoot(new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)));
                        inputs.Add(input); nextAim = time + .56f;
                    }
                    session.QAAdvanceOneStep();
                    run.peakBalls = Math.Max(run.peakBalls, session.ActiveBalls);
                    goalConserved &= session.FusionGoalsTotal == run.goalsTotal && session.FusionGoalsCredited >= 0 && session.FusionGoalsCredited <= run.goalsTotal;
                    if (seedIndex == 0 && policy == 0) {
                        if (!tookFirstCapture && session.PhysicsStep >= 1) { Capture(sample.id + "-" + scenario + "-01-opening"); tookFirstCapture = true; }
                        if (!tookProgressCapture && session.FusionMetrics.Transitions > 0) { Capture(sample.id + "-" + scenario + "-02-transition"); tookProgressCapture = true; }
                        if (!tookTailCapture && session.FusionGoalsCredited >= Math.Ceiling(run.goalsTotal * .8)) { Capture(sample.id + "-" + scenario + "-03-tail"); tookTailCapture = true; }
                    }
                } catch (Exception ex) { run.error = ex.ToString(); Check(sample.id + " physics execution", false, run.error); break; }
                if (session.PhysicsStep % 150 == 0) yield return null;
            }
            if (startedRun) {
                run.physicsSteps = session.PhysicsStep; run.simulatedSeconds = session.PhysicsStep * Time.fixedDeltaTime;
                run.shotsUsed = sample.moves - session.Shots; run.remainingShots = session.Shots;
                run.goalsCredited = session.FusionGoalsCredited;
                run.timedOut = session.IsPlaying && string.IsNullOrEmpty(run.error);
                run.result = !string.IsNullOrEmpty(run.error) ? "runtime_error" : run.timedOut ? "incomplete_timeout" : run.won ? "won" : "lost";
                run.metrics = JsonUtility.FromJson<FusionMetrics>(JsonUtility.ToJson(session.FusionMetrics));
                run.events = session.FusionEvents.ToArray();
                run.finalPieces = session.FusionPieces.Where(p => p.Alive).Select(p => new PieceState {
                    id=p.Definition.id,kind=p.Definition.kind,goalIds=p.GoalIds.ToArray(),authoredCol=p.Definition.c,authoredRow=p.Definition.r,
                    hp=p.Brick!=null?p.Brick.CurrentHealth:0,ice=p.Ice,stock=p.Stock,charge=p.Charge,x=p.Position.x,y=p.Position.y,
                    alive=p.Alive,enabled=p.Enabled,armor=p.Brick!=null&&p.Brick.ShellIntact }).ToArray();
                run.finalGoals = session.fusionGoals.Values.Select(g => new GoalState { id=g.Definition.id,ownerId=g.Definition.ownerId,kind=g.Definition.kind,state=g.State.ToString() }).ToArray();
                Check(sample.id + " goal conservation " + policy + "/" + seedIndex, goalConserved);
                Check(sample.id + " no fusion rule errors " + policy + "/" + seedIndex, string.IsNullOrEmpty(session.FusionRuleError), session.FusionRuleError);
                Check(sample.id + " finite natural outcome " + policy + "/" + seedIndex, !run.timedOut && run.outcomeCount == 1, run.result);
                Check(sample.id + " victory implies all initial goals " + policy + "/" + seedIndex, !run.won || run.goalsCredited == run.goalsTotal);
                var credits = run.events.Where(e => e.Kind == "GoalCredited").ToArray();
                Check(sample.id + " unique credited goal events " + policy + "/" + seedIndex, credits.Select(e => e.TargetId).Distinct().Count() == credits.Length && credits.Length == run.goalsCredited);
                Check(sample.id + " events retain current session identity " + policy + "/" + seedIndex, run.events.All(e => e.SessionId == session.Generation) && run.events.Select(e => e.EventId).Distinct().Count() == run.events.Length);
                var eventIds = new HashSet<long>(run.events.Select(e => e.EventId));
                Check(sample.id + " event ancestry points to earlier real events " + policy + "/" + seedIndex,
                    run.events.All(e => e.ParentEventId == 0 || e.ParentEventId < e.EventId && eventIds.Contains(e.ParentEventId))
                    && run.events.Zip(run.events.Skip(1), (a, b) => a.EventId < b.EventId).All(increasing => increasing));
                Check(sample.id + " successful launch accounting matches used moves " + policy + "/" + seedIndex, run.metrics.Shots == run.shotsUsed && run.events.Count(e => e.Kind == "VolleyStarted") == run.shotsUsed);
                if (scenario != "bare") Check(sample.id + " " + scenario + " spends one real inventory item", session.StreakStock(StreakBooster.Super) == 2);
                if (policy == 0 && seedIndex == 0) Capture(sample.id + "-" + scenario + "-04-result");
            }
            session.Outcome -= outcome;
            run.wallClockSeconds = Time.realtimeSinceStartup - wallStart; run.inputs = inputs.ToArray(); runs.Add(run);
            string file = sample.id + "-" + scenario + "-" + policies[policy] + "-" + seedIndex.ToString("D2");
            File.WriteAllText(Path.Combine(output, "runs", file + ".json"), JsonUtility.ToJson(run, true), new UTF8Encoding(false));
            activeNaturalRun = null;
        }
```

### ChooseAngle · L464

方法 SHA-256：`6accff3c918ed37c91e4bcf86dd74192ad5c2ad3ff3f8f976823cca58a03b3c9`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.LevelEditing.FusionQA.ChooseAngle(int, int, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/FusionQA.cs:412` · `FusionQA.NaturalRun` → `ChooseAngle(policy, inputs.Count, seedIndex)`

```csharp
float ChooseAngle(int policy, int shot, int seedIndex) {
            float perturb = new[] { 0f, -2f, 2f, -1f, 1f }[seedIndex % 5];
            if (policy == 2) return new[] { 90f, 68f, 112f, 80f, 100f }[shot % 5] + perturb;
            var pieces = session.FusionPieces.Where(p => p.Alive && p.Enabled && p.Transform != null).ToArray();
            var lookup = new Dictionary<Collider2D,FusionRuntimePiece>();
            foreach (var piece in pieces) foreach (var collider in piece.Transform.GetComponentsInChildren<Collider2D>()) lookup[collider] = piece;
            float best = float.NegativeInfinity, chosen = 90;
            for (int angle = 15; angle <= 165; angle += 3) {
                float radians = angle * Mathf.Deg2Rad;
                var direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
                var position = session.SpawnPosition(direction); float remaining = 48, score = 0;
                Collider2D previous = null; var scored = new HashSet<string>();
                for (int bounce = 0; bounce < 22 && remaining > .01f; bounce++) {
                    var hits = Physics2D.CircleCastAll(position, FlightGeometry.Radius, direction, remaining, 1 << 8).OrderBy(h => h.distance);
                    RaycastHit2D found = default; bool any = false;
                    foreach (var hit in hits) { if (hit.collider == null || hit.collider.isTrigger || hit.distance < .005f && hit.collider == previous) continue; found = hit; any = true; break; }
                    float segment = any ? found.distance : remaining;
                    foreach (var tool in pieces.Where(p => p.Tool != null)) {
                        Vector2 offset = tool.Position - position; float along = Vector2.Dot(offset, direction);
                        float radius = CollectionLevels.Pitch * .42f + FlightGeometry.Radius;
                        if (along >= 0 && along <= segment && (offset - direction * along).sqrMagnitude <= radius * radius && scored.Add(tool.Definition.id))
                            score += policy == 0 ? 12 : 5;
                    }
                    if (!any || direction.y < 0 && found.centroid.y < session.ReturnLineY) break;
                    remaining -= Mathf.Max(.02f, found.distance);
                    if (lookup.TryGetValue(found.collider, out var piece) && scored.Add(piece.Definition.id)) {
                        var p = piece.Definition;
                        if (p.kind == "stone" || p.kind == "gate") score -= 1;
                        else if (FusionRules.IsTool(p.kind)) score += policy == 0 ? 12 : 5;
                        else if (!string.IsNullOrEmpty(FusionRules.GoalKind(p))) score += policy == 1 ? 8 : 3;
                        else if (p.kind == "producer") score += piece.Stock > 0 ? 9 : .1f;
                        else if (FusionRules.IsDevice(p.kind) || p.kind == "chest") score += 4;
                        else score += .5f;
                    }
                    previous = found.collider; direction = Vector2.Reflect(direction, found.normal).normalized; position = found.centroid + found.normal * .02f;
                }
                score += Mathf.Sin((angle + shot * 23) * .12f) * .05f;
                if (score > best) { best = score; chosen = angle; }
            }
            return Mathf.Clamp(chosen + perturb, 15, 165);
        }
```

### RestoreSimulation · L511

方法 SHA-256：`e2625b5dca8fd23ac9cd89d7b4fd4df23f5194e1bda16815fd23cc06b2d49ee4`
时间来源：Time.timeScale
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.FusionQA.RestoreSimulation()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/FusionQA.cs:136` · `FusionQA.Start` → `RestoreSimulation()`
- `Assets/Scripts/LevelEditor/FusionQA.cs:538` · `FusionQA.OnDestroy` → `RestoreSimulation()`

```csharp
void RestoreSimulation() { if (!capturedSimulation) return; Physics2D.simulationMode = oldMode; Time.timeScale = oldTimeScale; capturedSimulation = false; }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void RestoreSimulation() { if (!capturedSimulation) return; Physics2D.simulationMode = oldMode; Time.timeScale = oldTimeScale; capturedSimulation = false; }
```

```csharp
void OnDestroy() { Application.logMessageReceived -= OnLog; if(session != null) session.StopLevel(); RestoreSimulation(); }
```

## 源字段与依赖

```csharp
readonly List<Assertion> checks = new List<Assertion>();
```
```csharp
readonly List<Run> runs = new List<Run>();
```
```csharp
readonly List<string> errors = new List<string>();
```
```csharp
readonly Dictionary<string,string> campaignHashes = new Dictionary<string,string>();
```
```csharp
string output, started, profileBefore, sourceSamplesPath, sourceSamplesSha256, sampleProjectPath;
```
```csharp
bool externalMode, superRangeRegression, flowRegression;
```
```csharp
int seeds;
```
```csharp
int outsideNaturalLogErrors;
```
```csharp
Run activeNaturalRun;
```
```csharp
GameSession session;
```
```csharp
SimulationMode2D oldMode;
```
```csharp
float oldTimeScale;
```
```csharp
bool capturedSimulation;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
