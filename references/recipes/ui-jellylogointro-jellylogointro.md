# JellyLogoIntro · JellyLogoIntro

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/JellyLogoIntro.cs`
文件 SHA-256：`b95d3a90f696aace042f7b35ded905e0101bdc962f77faf0baf9f095c0db3470`
[原始源码快照](../source/Assets/Scripts/UI/JellyLogoIntro.cs.txt)

用在哪里：AppUI.Awake → ShowIntro → StartIntroWhenVisible → JellyLogoIntro.Begin
触发操作：启动画面可见后自动演出
导入组件：`ProjectTweenKit.JellyLogoIntro`；展馆 ID：`logo`。
复用形式：组合预制体。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Elapsed · L10

方法 SHA-256：`0cdaefc3a1a0b4c682b684a0658894cac702cc50d2a4c75d5f76220dfa4caf24`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyLogoIntro.Elapsed`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public float Elapsed=>Time.unscaledTime-startedAt;
```

### Begin · L20

方法 SHA-256：`f7eccdbc1be5b519defa93010476889682764560c2d18dd5e371c36fd2f2734f`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyLogoIntro.Begin(bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/BrandStartupHost.cs:19` · `BrandStartupHost.BeginWhenVisible` → `intro.Begin(false)`

```csharp
public void Begin(bool prepareHome=true){
  startedAt=Time.unscaledTime;
  logo=UICraft.Root(transform,"Brand logo - independent sprites",0,0,1040,450);
  decorations=UICraft.Root(logo,"Behind letters - cat and jellies",0,0,1040,450);
  BrandSplashArt.Image(decorations,"jelly-pink",-325,65,215);
  BrandSplashArt.Image(decorations,"jelly-blue",-195,90,185);
  BrandSplashArt.Image(decorations,"jelly-yellow",295,75,210);
  BrandSplashArt.Image(decorations,"jelly-mint",-410,-15,175);
  BrandSplashArt.Image(decorations,"jelly-purple",415,-10,175);
  BrandSplashArt.Image(decorations,"cat",0,123,332);
  // Put the scale origins inside the lettering, preserving the resting artwork
  // positions. Small silhouettes are occluded, then grow out from behind the word.
  foreach(RectTransform decoration in decorations){
   decoration.pivot=new Vector2(.5f,.2f);
   decoration.anchoredPosition-=Vector2.up*decoration.rect.height*.3f;
  }
  SetDecorationScale(0);
  var letters=UICraft.Root(logo,"Four falling letters",0,0,1040,450);
  string[] keys={"letter-miao","letter-dan","letter-chai","letter-xiang"};
  string[] names={"喵","弹","拆","箱"};
  for(int i=0;i<4;i++){
   var letter=BrandSplashArt.Image(letters,keys[i],-334+i*223,-58,246);letter.name=names[i];
   var glyph=letter.gameObject.AddComponent<JellyGlyph>();glyphs.Add(glyph);glyph.Begin(i);
  }
  quote=UICraft.Label(transform,"—— 玩上五分钟，\n快乐一整天！",0,0,960,210,72);
  quote.name="Brand encouragement";quote.color=new Color32(104,63,30,255);quote.enableAutoSizing=false;
  quote.textWrappingMode=TextWrappingModes.NoWrap;quote.characterSpacing=1;quote.lineSpacing=12;quote.fontStyle=FontStyles.Bold;
  quoteMaterial=new Material(quote.fontSharedMaterial);quoteMaterial.EnableKeyword("OUTLINE_ON");
  quoteMaterial.SetFloat("_FaceDilate",.12f);
  quoteMaterial.SetColor("_OutlineColor",new Color32(255,246,218,255));quoteMaterial.SetFloat("_OutlineWidth",.34f);quoteMaterial.SetFloat("_OutlineSoftness",0);
  quoteMaterial.EnableKeyword("UNDERLAY_ON");quoteMaterial.SetColor("_UnderlayColor",new Color(0.35f,.19f,.08f,.25f));
  quoteMaterial.SetFloat("_UnderlayOffsetY",-.5f);quoteMaterial.SetFloat("_UnderlaySoftness",0);quoteMaterial.SetFloat("_UnderlayDilate",.04f);quote.fontSharedMaterial=quoteMaterial;
  quoteGroup=quote.gameObject.AddComponent<CanvasGroup>();quoteGroup.alpha=0;
  version=UICraft.Label(transform,"版本 "+GameVersion.Name,0,0,600,50,26);version.color=new Color32(113,83,54,255);
  var progress=UICraft.Root(transform,"Brand loading progress",0,0,940,300);Loading=progress.gameObject.AddComponent<BrandLoadingProgress>();Loading.Build();
  var plaza=UICraft.Root(transform,"Brand living plaza",0,0,1080,((RectTransform)transform).rect.height);plaza.SetSiblingIndex(1);
  GardenScene=plaza.gameObject.AddComponent<BrandGardenScene>();GardenScene.Build(Loading.Hedge);
  Layout();StartCoroutine(ReleaseAfterLanding());
  if(prepareHome)StartCoroutine(PrepareHome());
 }
```

### ReleaseAfterLanding · L70

方法 SHA-256：`0756a5e2b7171097082f474c6ae18383080c47a5950aa9c80f28f3a144e63fea`
时间来源：SetUpdate(true), Time.unscaledTime
曲线证据：Ease.OutCubic, Ease.OutSine
解析符号：`BounceGarden.JellyLogoIntro.ReleaseAfterLanding()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyLogoIntro.cs:57` · `JellyLogoIntro.Begin` → `ReleaseAfterLanding()`

```csharp
IEnumerator ReleaseAfterLanding(){
  while(glyphs.Exists(g=>!g.DropComplete))yield return null;
  AllLettersLanded=true;float release=Time.unscaledTime;foreach(var glyph in glyphs)glyph.ReleaseJelly(release);
  DecorationsStarted=true;SoundService.Play("whoosh");
  reveal=DOTween.Sequence().SetUpdate(true);
  // Scale each decoration about its own center; scaling their parent would throw
  // the outer jellies offscreen and change their authored positions behind the letters.
  reveal.Append(DOTween.To(()=>DecorationScale,SetDecorationScale,1.5f,.24f).SetEase(Ease.OutCubic));
  reveal.Append(DOTween.To(()=>DecorationScale,SetDecorationScale,1f,.34f).SetEase(Ease.OutSine));
  reveal.AppendCallback(()=>{
   DecorationsSettled=true;int index=0;
   foreach(RectTransform decoration in decorations){
    var jelly=decoration.gameObject.AddComponent<JellyLogoDecoration>();
    jelly.Play(index++,startedAt+HoldDuration,decoration.name=="cat" ? .45f : .62f);
   }
  });reveal.AppendInterval(.08f);
  reveal.AppendCallback(()=>StartCoroutine(RevealQuote()));
 }
```

### RevealQuote · L89

方法 SHA-256：`8fae978ba9bf707df6890c76597f44d069a8480f2bcf5b91ffe5e4c27ac285e9`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Pow
解析符号：`BounceGarden.JellyLogoIntro.RevealQuote()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyLogoIntro.cs:86` · `JellyLogoIntro.ReleaseAfterLanding` → `RevealQuote()`

```csharp
IEnumerator RevealQuote(){
  float elapsed=0;while(elapsed<.4f){elapsed+=Time.unscaledDeltaTime;float t=Mathf.Clamp01(elapsed/.4f),e=1-Mathf.Pow(1-t,3);
   quoteGroup.alpha=t;float height=((RectTransform)transform).rect.height;
   quote.rectTransform.anchoredPosition=new Vector2(0,-height*.005f-18*(1-e));
   quote.rectTransform.localScale=Vector3.one*Mathf.Lerp(.96f,1,e);yield return null;
  }
  quoteGroup.alpha=1;quote.rectTransform.localScale=Vector3.one;Layout();PresentationReady=true;
 }
```

### PrepareHome · L97

方法 SHA-256：`d2a123edb20568dbd1ddedbad988af73a47eab40a10b5bda1f1784ba2072cc69`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.JellyLogoIntro.PrepareHome()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyLogoIntro.cs:58` · `JellyLogoIntro.Begin` → `PrepareHome()`
- `Assets/Scripts/UI/JellyLogoIntro.cs:102` · `JellyLogoIntro.PrepareHome` → `PrepareHome()`

```csharp
IEnumerator PrepareHome(){
  if(preparing)yield break;preparing=true;if(retry!=null)retry.SetActive(false);Loading.Resume();Loading.SetProgress(.15f);
  preparation=new ContentLoadOperation();yield return preparation.Run(GameContent.PreparePage("home"));
  if(!preparation.Succeeded){
   preparing=false;Loading.Failure("花园准备未完成，请重试。");
   if(retry==null)retry=UICraft.Button(Loading.transform,"重试",0,-253,300,85,UICraft.Gold,()=>StartCoroutine(PrepareHome()),34).gameObject;
   retry.SetActive(true);yield break;
  }
  Loading.SetProgress(.8f);Canvas.ForceUpdateCanvases();yield return null;yield return null;
  Loading.SetProgress(.95f);while(!PresentationReady)yield return null;Loading.Complete();preparing=false;
 }
```

## 对象层级和素材入口

- L22：`UICraft.Root(transform,"Brand logo - independent sprites",0,0,1040,450)`
- L23：`UICraft.Root(logo,"Behind letters - cat and jellies",0,0,1040,450)`
- L24：`BrandSplashArt.Image(decorations,"jelly-pink",-325,65,215)`
- L25：`BrandSplashArt.Image(decorations,"jelly-blue",-195,90,185)`
- L26：`BrandSplashArt.Image(decorations,"jelly-yellow",295,75,210)`
- L27：`BrandSplashArt.Image(decorations,"jelly-mint",-410,-15,175)`
- L28：`BrandSplashArt.Image(decorations,"jelly-purple",415,-10,175)`
- L29：`BrandSplashArt.Image(decorations,"cat",0,123,332)`
- L37：`UICraft.Root(logo,"Four falling letters",0,0,1040,450)`
- L41：`BrandSplashArt.Image(letters,keys[i],-334+i*223,-58,246)`
- L44：`UICraft.Label(transform,"—— 玩上五分钟，\n快乐一整天！",0,0,960,210,72)`
- L53：`UICraft.Label(transform,"版本 "+GameVersion.Name,0,0,600,50,26)`
- L54：`UICraft.Root(transform,"Brand loading progress",0,0,940,300)`
- L55：`UICraft.Root(transform,"Brand living plaza",0,0,1080,((RectTransform)transform).rect.height)`
- L55：`plaza.SetSiblingIndex(1)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){preparation?.Cancel();reveal?.Kill();StopAllCoroutines();}
```

```csharp
void OnDestroy(){if(quoteMaterial!=null)Destroy(quoteMaterial);}
```

## 源字段与依赖

```csharp
public const float HoldDuration=6.8f,FadeDuration=.4f;
```
```csharp
public static bool ExternalStartupOwnsIntro;
```
```csharp
float startedAt;
```
```csharp
ContentLoadOperation preparation;
```
```csharp
bool preparing;
```
```csharp
GameObject retry;
```
```csharp
float decorationPopScale;
```
```csharp
RectTransform logo,decorations;
```
```csharp
TMP_Text quote,version;
```
```csharp
CanvasGroup quoteGroup;
```
```csharp
Sequence reveal;
```
```csharp
Material quoteMaterial;
```
```csharp
readonly List<JellyGlyph> glyphs=new List<JellyGlyph>();
```
```csharp
float previousHeight;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
