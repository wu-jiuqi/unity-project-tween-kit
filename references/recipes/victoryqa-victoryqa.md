# VictoryQA · VictoryQA

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/VictoryQA.cs`
文件 SHA-256：`5a5ed43a67b5169f6d3a875ccda455727a6249c97864b956b3a4014af333cf78`
[原始源码快照](../source/Assets/Scripts/VictoryQA.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Level · L33

方法 SHA-256：`235168479d753c2583fbcfef0448d0387b09dbab90a3a30bcd5baaaae971a695`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryQA.Level(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/VictoryQA.cs:63` · `VictoryQA.Start` → `Level(0)`
- `Assets/Scripts/VictoryQA.cs:97` · `VictoryQA.Start` → `Level(0)`
- `Assets/Scripts/VictoryQA.cs:116` · `VictoryQA.Start` → `Level(0)`
- `Assets/Scripts/VictoryQA.cs:119` · `VictoryQA.Start` → `Level(0)`

```csharp
IEnumerator Level(int index) { app.QAStartLevel(index); yield return app.QAWaitForLevel(); yield return new WaitForSecondsRealtime(.2f); }
```

### Capture · L34

方法 SHA-256：`f149f73a7cb88d384673969091e8027d546c5a6c833f8f4b9580f567e5e047ab`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryQA.Capture(string)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/VictoryQA.cs:82` · `VictoryQA.Start` → `Capture("victory-" + time.ToString("F2", System.Globalization.CultureInfo.InvariantCulture))`
- `Assets/Scripts/VictoryQA.cs:94` · `VictoryQA.Start` → `Capture("safe-area")`
- `Assets/Scripts/VictoryQA.cs:124` · `VictoryQA.Start` → `Capture("feedback-disabled")`

```csharp
IEnumerator Capture(string name) {
        yield return new WaitForEndOfFrame(); Canvas.ForceUpdateCanvases(); camera.Render();
        var old = RenderTexture.active; RenderTexture.active = target;
        var image = new Texture2D(width, height, TextureFormat.RGB24, false);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0); image.Apply(); RenderTexture.active = old;
        File.WriteAllBytes(Path.Combine(directory, name + ".png"), image.EncodeToPNG()); Destroy(image);
    }
```

### Start · L52

方法 SHA-256：`f346aec652c11987cb116b89415545ba480e3e78a968d262c10f535d31eca685`
时间来源：Time.timeScale, WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.VictoryQA.Start()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
IEnumerator Start() {
        var args = Environment.GetCommandLineArgs(); directory = args[Array.IndexOf(args, "--qa-out") + 1]; Directory.CreateDirectory(directory);
        Application.logMessageReceived += (message, stack, type) => { if (type == LogType.Error || type == LogType.Exception) { errors++; File.AppendAllText(Path.Combine(directory, "errors.txt"), message + "\n" + stack + "\n"); } };
        var size = args[Array.IndexOf(args, "--qa-viewport") + 1].Split('x'); width = int.Parse(size[0]); height = int.Parse(size[1]);
        yield return new WaitForSecondsRealtime(1);
        app = FindFirstObjectByType<AppUI>(); camera = Camera.main;
        target = new RenderTexture(width, height, 24); target.Create(); camera.targetTexture = target; camera.aspect = (float)width / height;
        var canvas = FindFirstObjectByType<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceCamera; canvas.worldCamera = camera; canvas.planeDistance = 1;
        app.QASetViewport(new Vector2Int(width, height)); app.QANavigate("home"); yield return null;
        Check(RuntimeQA.IsIsolated, "player profile and feedback use isolated test storage");
        Check(LocalProfile.RunSelfTests().StartsWith("PASS"), "reward ledger self-tests pass");
        yield return Level(0); int coins = app.Profile.Coins, badges = app.Profile.Badges;
        Win(); yield return null;
        Check(Victory != null && !Victory.Ready, "victory enters with actions gated");
        var cat = Victory.CharacterRoot.GetComponentInChildren<Image>();
        Check(cat != null && cat.sprite != null && !cat.raycastTarget, "cat sequence is visible and cannot intercept input");
        var catCorners = new Vector3[4]; cat.rectTransform.GetWorldCorners(catCorners);
        var rewardCorners = new Vector3[4]; ((RectTransform)Victory.transform.Find("Victory rewards")).GetWorldCorners(rewardCorners);
        Check(catCorners[0].y > rewardCorners[1].y, "entire cat canvas stays above reward card");
        var subtitle = Victory.GetComponentInChildren<TMP_Text>();
        var subtitleCorners = new Vector3[4]; subtitle.rectTransform.GetWorldCorners(subtitleCorners);
        Check(catCorners[1].y < subtitleCorners[0].y, "jump apex stays below subtitle");
        float age = 0; var durations = new[] { .30f,.10f,.10f,.18f,.08f,.08f,.08f,.12f,.10f,.08f,.08f,.08f,.10f,.10f,.14f,.20f };
        for (int i = 0; i < durations.Length; i++) {
            Check(VictoryCatAnimation.FrameAt(age + durations[i] / 2) == i && VictoryCatAnimation.Get(i) != null, "approved cat frame and duration " + i);
            age += durations[i];
        }
        app.CollectReward(); Check(app.Profile.Coins == coins, "early collection cannot grant rewards");
        foreach (float time in new[] { .2f, .7f, 1.1f, 1.95f, 2.3f }) {
            while (Victory.Elapsed < time) yield return null;
            yield return Capture("victory-" + time.ToString("F2", System.Globalization.CultureInfo.InvariantCulture));
        }
        Check(Victory.Ready && app.RewardReady, "buttons become usable after their entrance completes");
        Check(Victory.CatFrame == 15, "cat holds final seated frame after landing");
        Check(app.Profile.Coins == coins && app.Profile.Badges == badges, "animation never automatically grants rewards");
        int letters = 0; foreach (Transform child in Victory.transform) if (child.name.StartsWith("Victory letter ")) letters++;
        Check(letters == 14, "all 14 title letter instances are separate jelly images");
        var halo = Victory.transform.Find("Victory light clip/Victory halo").GetComponent<Image>();
        var haloSprite = halo.sprite; var haloRotation = halo.rectTransform.localRotation;
        yield return new WaitForSecondsRealtime(.24f);
        Check(halo.sprite != haloSprite || halo.rectTransform.localRotation != haloRotation, "sunburst keeps rotating after the intro");
        Check(!Field<RectTransform>("hudRoot").gameObject.activeSelf, "battle controls and aim hint are hidden during victory");
        ButtonBounds(); app.QASetSafeArea(new Rect(0, 55, width, height - 110)); yield return null; ButtonBounds(); yield return Capture("safe-area"); app.QASetSafeArea(null);
        Click(ButtonNamed("Button ")); app.CollectReward(); yield return null;
        Check(app.CurrentPage == "home" && app.Profile.Coins == coins + 20 && app.Profile.Badges == badges + 1, "Home commits exactly one base reward and returns home");
        yield return Level(0); Win();
        Check(Victory.CatFrame == 0, "new victory restarts cat from prone pose");
        Time.timeScale = 0; yield return new WaitForSecondsRealtime(.9f);
        Check(Victory.CatFrame > 3, "cat sequence advances while gameplay time is paused");
        Time.timeScale = 1; Victory.Finish(); yield return null;
        Check(Victory.CatFrame == 15, "skip intro settles cat immediately");
        var next = Victory.transform.Find("Result actions").GetComponentsInChildren<Button>()[0]; Click(next); yield return app.QAWaitForLevel();
        Check(Field<int>("currentLevel") == 1 && app.Profile.Coins == coins + 40, "Next saves base reward once and starts the actual next level");
        Check(Field<RectTransform>("hudRoot").gameObject.activeSelf, "battle HUD is restored for the next level");
        Win(); Victory.Finish(); yield return null; int beforeAd = app.Profile.Coins;
        Click(ButtonNamed("Button x2 Coins")); yield return null;
        Check(app.Profile.Coins == beforeAd + 20, "video entry saves base reward once");
        Click(ButtonNamed("Button Cancel")); yield return null;
        Check(app.Profile.Coins == beforeAd + 20 && Victory.Ready, "cancelling video retains base reward and usable actions");
        Click(ButtonNamed("Button x2 Coins")); yield return null; Click(ButtonNamed("Button Complete  +20")); yield return null;
        Check(app.Profile.Coins == beforeAd + 40, "completed local video adds only the extra 20 coins");
        Click(ButtonNamed("Button x2 Coins")); yield return null;
        Check(app.Profile.Coins == beforeAd + 40, "duplicate video entry grants nothing");
        app.CollectReward(); yield return null;
        yield return Level(0); Win(); yield return new WaitForSecondsRealtime(.2f); int interruptedCoins = app.Profile.Coins, interruptedLedger = app.Profile.CommittedTransactionCount;
        app.QANavigate("home"); yield return null;
        Check(Victory == null && app.Profile.Coins == interruptedCoins && app.Profile.CommittedTransactionCount == interruptedLedger, "interrupting intro destroys effects without granting rewards");
        yield return Level(0); FeedbackProfile.Active.enabledFeedback = false; Win(); yield return new WaitForSecondsRealtime(.1f);
        Check(Victory.Ready, "disabled feedback immediately exposes usable actions");
        Check(Victory.CatFrame == 15, "disabled feedback shows the final seated cat");
        halo = Victory.transform.Find("Victory light clip/Victory halo").GetComponent<Image>(); haloSprite = halo.sprite; haloRotation = halo.rectTransform.localRotation;
        yield return new WaitForSecondsRealtime(.25f); Check(halo.sprite == haloSprite && halo.rectTransform.localRotation == haloRotation, "disabled feedback stops idle motion");
        yield return Capture("feedback-disabled"); FeedbackProfile.Active.enabledFeedback = true; app.QANavigate("home"); yield return null;
        string result = "RESULT checks=" + checks + " failed=" + failures + " errors=" + errors;
        File.AppendAllText(Path.Combine(directory, "checks.txt"), result + "\n"); Debug.Log(result); Application.Quit(failures == 0 && errors == 0 ? 0 : 1);
    }
```

## 对象层级和素材入口

- L69：`Victory.transform.Find("Victory rewards")`
- L89：`Victory.transform.Find("Victory light clip/Victory halo")`
- L103：`Victory.transform.Find("Result actions")`
- L122：`Victory.transform.Find("Victory light clip/Victory halo")`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
string directory;
```
```csharp
int checks, failures, errors, width, height;
```
```csharp
AppUI app;
```
```csharp
Camera camera;
```
```csharp
RenderTexture target;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
