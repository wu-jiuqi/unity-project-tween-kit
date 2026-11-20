# LoadingPresentation · LoadingPresentation

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/LoadingPresentation.cs`
文件 SHA-256：`be73feae8f643713af3ce16b5ecdd6c7f888b5ff28b4b851a085c7af0cea22df`
[原始源码快照](../source/Assets/Scripts/UI/LoadingPresentation.cs.txt)

用在哪里：AppUI.BeginLevel → LoadingPresentation.Build / Play → RunLevelLoad
触发操作：进入关卡的加载页自动循环；内容准备另由宿主管理
导入组件：`ProjectTweenKit.LoadingPresentation`；展馆 ID：`loading`。
复用形式：组合预制体。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L43

方法 SHA-256：`38f786bb3663c733bdcbb4fb953b86d8c614c176a53f1c5b09fab12737625d7a`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.LoadingPresentation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update() {
  if (!running) return;
  if(trackContentStatus&&GameContent.Status!=null&&label.text!=GameContent.Status())label.text=GameContent.Status();
  // Preparation stalls must not count as already-seen animation or skip a whole pose.
  if (firstTick) { firstTick = false; return; }
  Elapsed += Mathf.Min(Time.unscaledDeltaTime, .1f);
  Render();
 }
```

### Hop · L51

方法 SHA-256：`758ac5179cfaf629b9d6669d09c8bf2151d10f4bdeae214bfdc4773fcfe4bbfa`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.LoadingPresentation.Hop(float, float, float, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/LoadingPresentation.cs:59` · `LoadingPresentation.Render` → `Hop(animationTime, i * .18f, 3.7f, .60f)`
- `Assets/Scripts/UI/LoadingPresentation.cs:70` · `LoadingPresentation.Render` → `Hop(animationTime, i * .13f, 2.9f, .58f)`

```csharp
static float Hop(float time, float delay, float period, float duration) {
  float age = Mathf.Repeat(time - delay, period);
  return age < duration ? Mathf.Sin(Mathf.PI * age / duration) : 0;
 }
```

### Render · L55

方法 SHA-256：`05bca3b8ed2b4d26ad08550edb1d8c25565e1ad11e614b536f388c5afd7b4ba4`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.LoadingPresentation.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/LoadingPresentation.cs:38` · `LoadingPresentation.Build` → `Render()`
- `Assets/Scripts/UI/LoadingPresentation.cs:42` · `LoadingPresentation.Play` → `Render()`
- `Assets/Scripts/UI/LoadingPresentation.cs:49` · `LoadingPresentation.Update` → `Render()`

```csharp
void Render() {
  // Preserve the original full choreography at the faster loading tempo.
  float animationTime = Elapsed * (6.7f / CatCycleDuration);
  for (int i = 0; i < letters.Length; i++) {
   float h = Hop(animationTime, i * .18f, 3.7f, .60f);
   letters[i].anchoredPosition = letterPositions[i] + Vector2.up * (30 * h);
   letters[i].localScale = new Vector3(1 - .025f * h, 1 + .035f * h, 1);
   letters[i].localRotation = Quaternion.Euler(0, 0, (i % 2 == 0 ? 1 : -1) * h * 1.43f);
  }
  label.ForceMeshUpdate();
  var info = label.textInfo;
  for (int i = 0; i < info.characterCount; i++) {
   var ch = info.characterInfo[i];
   if (!ch.isVisible) continue;
   var vertices = info.meshInfo[ch.materialReferenceIndex].vertices;
   Vector3 shift = Vector3.up * (16 * Hop(animationTime, i * .13f, 2.9f, .58f));
   for (int j = 0; j < 4; j++) vertices[ch.vertexIndex + j] += shift;
  }
  label.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
  float age = Mathf.Repeat(animationTime, 6.7f), stretch = 1, lift = 0, angle = 0;
  int pose = 0;
  if (age >= 1.8f && age < 2.35f) { pose = 1; stretch -= .025f * Mathf.Sin((age - 1.8f) / .55f * Mathf.PI); }
  else if (age >= 2.35f && age < 3.05f) { pose = 2; lift = 30 * Mathf.Sin((age - 2.35f) / .7f * Mathf.PI); }
  else if (age >= 3.05f && age < 4.2f) pose = 3;
  else if (age >= 4.2f && age < 5.65f) { pose = 4; angle = Mathf.Sin((age - 4.2f) * 7) * 2; }
  CurrentPose = PoseNames[pose];
  if (catImage != null && poses[pose] != null) catImage.sprite = poses[pose];
  // Stretch and tilt around the feet rather than the centre of the sprite.
  stretch += Mathf.Sin(animationTime * 2.2f) * .008f;
  cat.localScale = new Vector3(catScale.x, catScale.y * stretch, catScale.z);
  cat.anchoredPosition = catPosition + Vector2.up * (lift + 150 * catScale.y * (stretch - 1));
  cat.localRotation = Quaternion.Euler(0, 0, angle);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public const float CatCycleDuration = 3.6f;
```
```csharp
public const float MinimumDuration = .6f;
```
```csharp
RectTransform[] letters;
```
```csharp
Vector2[] letterPositions;
```
```csharp
RectTransform cat;
```
```csharp
Vector2 catPosition;
```
```csharp
Vector3 catScale;
```
```csharp
Image catImage;
```
```csharp
TMP_Text label;
```
```csharp
Sprite[] poses;
```
```csharp
bool running, firstTick,trackContentStatus;
```
```csharp
static readonly string[] PoseNames = { "cat_idle", "cat_anticipate", "cat_celebrate", "cat_settle", "cat_wave" };
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
