# LevelEditorApp · LevelEditorApp

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/LevelEditor/LevelEditorApp.cs`
文件 SHA-256：`7ebe073546dbf34bbc3089360685b8f887f1202e7a749518bf042258f429f1e1`
[原始源码快照](../source/Assets/Scripts/LevelEditor/LevelEditorApp.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Awake · L64

方法 SHA-256：`2d5abbe516adf61a146d14b868cf199be3831737b86c2c428a83690d6a3a8519`
时间来源：Time.Now, Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.LevelEditorApp.Awake()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Awake()
        {
            Application.targetFrameRate = 60;
            Application.runInBackground = true;
            qaMode = HasArgument("--level-editor-qa");
            rootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BounceBustersLevelEditor");
            if (qaMode) rootDirectory = Argument("--level-editor-qa-out") ?? Path.Combine(Path.GetTempPath(), "BounceBustersLevelEditorQA");
            workspaceDirectory = Argument("--level-editor-workspace") ?? Path.Combine(rootDirectory, "Projects", "MyLevels");
            if (!qaMode && Argument("--level-editor-workspace") == null)
            {
                try { var path = Path.Combine(rootDirectory, "authoring-settings.json"); if (File.Exists(path)) { var settings = JsonUtility.FromJson<WorkspaceSettings>(File.ReadAllText(path)); if (!string.IsNullOrWhiteSpace(settings.workspace)) workspaceDirectory = settings.workspace; } }
                catch (Exception ex) { Debug.LogWarning("Level editor workspace preference: " + ex.Message); }
            }
            exportParent = Path.Combine(rootDirectory, "Exports");
            Playtest = gameObject.AddComponent<LevelEditorPlaytest>();
            Playtest.Completed += OnCompleted;
            try
            {
                Directory.CreateDirectory(rootDirectory);
                bool anyCampaign = Directory.Exists(workspaceDirectory) && Directory.GetFiles(workspaceDirectory, "campaign.json", SearchOption.AllDirectories).Length > 0;
                Workspace = anyCampaign ? LevelEditorWorkspace.LoadDirectory(workspaceDirectory) : LevelEditorWorkspace.CreateEmpty();
                if (anyCampaign && string.IsNullOrEmpty(Workspace.LastSaveDirectory)) workspaceDirectory = Path.Combine(rootDirectory, "Imports", DateTime.Now.ToString("yyyyMMdd-HHmmss-fff"));
                if (!anyCampaign || string.IsNullOrEmpty(Workspace.LastSaveDirectory))
                {
                    var saved = Workspace.SaveWorkspace(workspaceDirectory);
                    if (!saved.Success) throw new IOException(saved.Message);
                }
                loaded = true; SelectLevel(0);
                RememberWorkspace();
                SetStatus(Workspace.Levels.Count == 0 ? "新项目已就绪 · 选择规则模板，点击「＋ 新建关卡」从空棋盘开始" : "已载入 " + Workspace.Levels.Count + " 关 · 草稿每 15 秒自动保存 · " + workspaceDirectory);
            }
            catch (Exception ex) { SetStatus("工作区载入失败：" + ex.Message, true); }
            nextSave = Time.unscaledTime + 15;
            Application.wantsToQuit += CanQuit;
        }
```

### Update · L118

方法 SHA-256：`ab18f68a0a250524b63e9f16f0a0b407e2d9d96dedc67e95d41e863baea0d6a2`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.LevelEditorApp.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update()
        {
            // A release outside the native window may not deliver a GUI MouseUp.
            if (gestureLevel != null && !Input.GetMouseButton(0) && !Input.GetMouseButtonUp(0)) FinishBoardGesture();
            if (loaded && !qaMode && !IsPlaytesting && gestureLevel == null && Time.unscaledTime >= nextSave)
            {
                nextSave = Time.unscaledTime + 15;
                if (Workspace.IsDirty) SaveWorkspace();
            }
            if (IsPlaytesting && Camera.main != null)
            {
                var rect = previewRect;
                Camera.main.rect = new Rect(rect.x * scale / Screen.width, 1 - (rect.y + rect.height) * scale / Screen.height,
                    rect.width * scale / Screen.width, rect.height * scale / Screen.height);
            }
        }
```

### BeginPlaytest · L209

方法 SHA-256：`b8ddd6f2459ce2aa89656ae0a81b89de7784cccff751e3da894699ef9dc124e2`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.LevelEditorApp.BeginPlaytest()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/LevelEditor/LevelEditorApp.cs:230` · `LevelEditorApp.RestartPlaytest` → `BeginPlaytest()`
- `Assets/Scripts/LevelEditor/LevelEditorApp.cs:349` · `LevelEditorApp.DrawHeader` → `BeginPlaytest()`

```csharp
public bool BeginPlaytest()
        {
            if (CurrentLevel == null || IsPlaytesting) return false;
            FinishBoardGesture();
            RefreshValidation();
            if (issues.Any(p => p.IsError)) { SetStatus("请先修复当前关卡的校验错误，再开始试玩。", true); return false; }
            if (!SaveWorkspace()) return false;
            string snapshot = CurrentLevel.SnapshotJson();
            if (!Playtest.StartDraft(CurrentLevel, Scenarios[scenario])) { SetStatus(Playtest.LastError, true); return false; }
            IsPlaytesting = true; aiming = false; runStart = Time.unscaledTime; recordWritten = false;
            fusionPlayScroll = Vector2.zero;
            shotRecords.Clear(); runScenario = Scenarios[scenario];
            using (var hash = SHA256.Create()) runHash = BitConverter.ToString(hash.ComputeHash(Encoding.UTF8.GetBytes(snapshot))).Replace("-", "").ToLowerInvariant();
            Playtest.Session.VolleyStarted -= OnVolley; Playtest.Session.VolleyStarted += OnVolley;
            if (Camera.main != null) Camera.main.enabled = true;
            SetStatus("真实试玩 · 按住棋盘瞄准，松开发射 · 道具需手动选择 · Esc 返回编辑");
            return true;
        }
```

### OnVolley · L243

方法 SHA-256：`dae665711d2ed7e07d92954f470e573ac63c91f51f3d9d9dbb37b32b09ab3d63`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LevelEditing.LevelEditorApp.OnVolley(BounceGarden.StreakBooster)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
void OnVolley(StreakBooster booster)
        {
            shotRecords.Add(new ShotRecord { elapsedSeconds = Time.unscaledTime - runStart, booster = booster.ToString(),
                remainingShots = Playtest.Session.Shots, hits = Playtest.Session.HitCount, breaks = Playtest.Session.BreakCount,
                collected = (int[])Playtest.Session.Collected.Clone(), jellyCollected = (int[])Playtest.Session.JellyCollected.Clone() });
        }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy()
        {
            FinishBoardGesture();
            Application.wantsToQuit -= CanQuit;
            foreach (var texture in styleTextures) if (texture != null) Destroy(texture);
        }
```

```csharp
void OnApplicationFocus(bool focused) { if (!focused) FinishBoardGesture(); }
```

```csharp
void OnApplicationPause(bool paused) { if (paused) FinishBoardGesture(); }
```

```csharp
void OnDisable() { FinishBoardGesture(); }
```

## 源字段与依赖

```csharp
const float Sidebar = 222, InspectorWidth = 326;
```
```csharp
static readonly Color Background = Hex(0x101A28), Panel = Hex(0x192638), Line = Hex(0x304157);
```
```csharp
static readonly Color Ink = Hex(0xECF1F6), Muted = Hex(0xA9B9CA), Accent = Hex(0x7EE2BD), Amber = Hex(0xFFD18C);
```
```csharp
static readonly string[] Chapters = LevelEditorRuleCatalog.ChapterIds.ToArray();
```
```csharp
static readonly string[] ChapterNames = Chapters.Select(LevelEditorRuleCatalog.ChapterName).ToArray();
```
```csharp
static readonly string[] Scenarios = { "bare", "streak3", "streak5", "streak10", "stocked" };
```
```csharp
static readonly string[] ScenarioNames = { "无道具", "3 连胜", "5 连胜", "10 连胜", "测试库存" };
```
```csharp
GUIStyle body, muted, heading, title, button, selectedButton, primaryButton, input, small, center, cellLabel;
```
```csharp
readonly List<Texture2D> styleTextures = new List<Texture2D>();
```
```csharp
readonly Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();
```
```csharp
readonly List<ShotRecord> shotRecords = new List<ShotRecord>();
```
```csharp
int selectedLevel = -1, selectedChapter, scenario, toolMode;
```
```csharp
string selectedPiece, paintKind = "N", search = "", status = "正在载入关卡…", workspaceDirectory, rootDirectory;
```
```csharp
string editName = "", movesText = "20", hpText = "1", colText = "0", rowText = "0", sizeText = "1", iceText = "0", layersText = "1", ringText = "2", ripenText = "2";
```
```csharp
string contentsText = "1,0,2,0", direction = "up", chestMode = "plain";
```
```csharp
bool armor, statusError, aiming, closeRequested, qaMode, loaded, recordWritten;
```
```csharp
bool showIssues;
```
```csharp
float scale = 1, width = 1440, height = 900, nextSave, runStart, inspectorContentHeight = 1200;
```
```csharp
Rect boardRect, previewRect;
```
```csharp
Vector2 levelScroll, paletteScroll, inspectorScroll, issueScroll, folderScroll;
```
```csharp
Vector2 pointerStart;
```
```csharp
IReadOnlyList<ValidationIssue> issues = Array.Empty<ValidationIssue>();
```
```csharp
string folderMode, folderPath, folderError = "", exportParent, confirmAction;
```
```csharp
string[] folderChildren = Array.Empty<string>();
```
```csharp
LevelEditorPiece copiedPiece;
```
```csharp
string copiedChapter;
```
```csharp
string runHash, runScenario;
```
```csharp
LevelEditorLevel gestureLevel;
```
```csharp
int boardControlId, gestureControlId, gestureMode, gestureScreenWidth, gestureScreenHeight;
```
```csharp
Vector2? previousStrokePoint;
```
```csharp
Vector2 gestureStart;
```
```csharp
Vector2Int moveOffset;
```
```csharp
string movingPiece;
```
```csharp
readonly HashSet<Vector2Int> strokeVisited = new HashSet<Vector2Int>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
