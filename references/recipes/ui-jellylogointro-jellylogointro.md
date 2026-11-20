# JellyLogoIntro · JellyLogoIntro

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/JellyLogoIntro.cs`
文件 SHA-256：`b555af8ce8910e5c3c35bfb6b6a3abedf91be7e191cb3ab482036f9aeaeac469`
[原始源码快照](../source/Assets/Scripts/UI/JellyLogoIntro.cs.txt)

用在哪里：AppUI.Awake → ShowIntro → StartIntroWhenVisible → JellyLogoIntro.Begin
触发操作：启动画面可见后自动演出
导入组件：`ProjectTweenKit.JellyLogoIntro`；展馆 ID：`logo`。
复用形式：组合预制体。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Begin · L6

方法 SHA-256：`07757f91c52adbaa1d51a4b396c910536b6f54e0e96c122d85a3adf93f2c6509`
时间来源：SetUpdate(true)
曲线证据：Ease.Linear
解析符号：`BounceGarden.JellyLogoIntro.Begin()`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Begin(){logo=PastelLogo.Create(transform,0,190,850,370);
 quote=UICraft.Label(transform,"玩上五分钟，\n快乐一整天。",0,-190,900,160,48,UICraft.Ink);quote.ForceMeshUpdate();int characters=quote.textInfo.characterCount;quote.maxVisibleCharacters=0;
 int index=0;foreach(Transform item in logo){var glyph=item.gameObject.AddComponent<JellyGlyph>();glyphs.Add(glyph);glyph.Begin(index++);}StartCoroutine(ReleaseAfterLanding());
 reveal=DOTween.Sequence().SetUpdate(true);reveal.AppendInterval(2.05f);float visible=0;reveal.Append(DOTween.To(()=>visible,v=>{visible=v;quote.maxVisibleCharacters=Mathf.FloorToInt(v);},characters,characters*.035f).SetEase(Ease.Linear));
 }
```

### ReleaseAfterLanding · L11

方法 SHA-256：`925952969e8b7407031ca6353a6e71ad54f56d2efc668724a38c67e30ce4a1f6`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyLogoIntro.ReleaseAfterLanding()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyLogoIntro.cs:8` · `JellyLogoIntro.Begin` → `ReleaseAfterLanding()`

```csharp
IEnumerator ReleaseAfterLanding(){while(glyphs.Exists(g=>!g.DropComplete))yield return null;float release=Time.unscaledTime;foreach(var glyph in glyphs)glyph.ReleaseJelly(release);}
```

## 对象层级和素材入口

- L7：`UICraft.Label(transform,"玩上五分钟，\n快乐一整天。",0,-190,900,160,48,UICraft.Ink)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){reveal?.Kill();}
```

## 源字段与依赖

```csharp
public const float HoldDuration=6.8f,FadeDuration=.4f;
```
```csharp
RectTransform logo;
```
```csharp
TMP_Text quote;
```
```csharp
Sequence reveal;
```
```csharp
readonly List<JellyGlyph> glyphs=new List<JellyGlyph>();
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
