# PaintingCollectionPresentation · PaintingCollectionPresentation

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/PaintingCollectionPresentation.cs`
文件 SHA-256：`602e4293fd5ac8f6966ff9061231e3d792694ced545f0338a26297b0aba7c4f9`
[原始源码快照](../source/Assets/Scripts/UI/PaintingCollectionPresentation.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Begin · L17

方法 SHA-256：`00faf1847175668d7cfe9a2d79dc2cfb3847172ae77495c7fdf04b2a30022849`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.PaintingCollectionPresentation.Begin(bool, System.Action)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.Painting.cs:13` · `AppUI.BeginPaintingCollection` → `paintCollection.Begin(saved,()=>{if(generation!=viewGeneration||!playing)return;ClearPaintingCollection();ShowResult(true);if(!saved)Toast("作品保存失败，请重试。");})`

```csharp
public void Begin(bool saved,Action done){
  completed=done;var root=(RectTransform)transform;root.anchorMin=Vector2.zero;root.anchorMax=Vector2.one;root.sizeDelta=Vector2.zero;root.anchoredPosition=Vector2.zero;
  whole=gameObject.AddComponent<CanvasGroup>();whole.blocksRaycasts=true;
  var back=UICraft.Root(transform,"Full screen black collection dimmer");back.anchorMin=Vector2.zero;back.anchorMax=Vector2.one;back.sizeDelta=Vector2.zero;back.anchoredPosition=Vector2.zero;
  dimmer=back.gameObject.AddComponent<Image>();dimmer.color=Color.clear;dimmer.raycastTarget=true;
  content=UICraft.Root(transform,"Collection content",0,0,1080,1800);
  art=UICraft.Root(content,"Framed fox artwork",0,60,960,960);
  UICraft.Box(art,"Artwork paper",0,0,658,658,new Color32(248,242,233,255));
  rough=MakeImage(art,"Original irregular paint",PaintingArt.Image("fox_splatter"),650);
  pixel=MakeImage(art,"Collected regular pixel artwork",PaintingArt.Image("fox_pixel"),650*720/768);
  frame=MakeImage(art,"Generated honey wood picture frame",PaintingArt.Image("fox_frame"),960);
  var text=UICraft.Root(content,"Collection captions");captions=text.gameObject.AddComponent<CanvasGroup>();
  UICraft.Label(text,"小狐狸",0,-520,900,100,62).color=new Color32(255,240,207,255);
  UICraft.Label(text,saved?"小狐狸001 · 已收入画册":"小狐狸001 · 创作完成",0,-615,940,70,29).color=new Color32(244,214,167,255);
  for(int i=0;i<28;i++){
   float angle=i*Mathf.PI*2/28;var image=UICraft.Box(content,"Collection golden paint spark",0,0,i%3==0?13:7,i%3==0?13:7,Color.white,true);
   image.raycastTarget=false;sparks.Add(new Spark{image=image,start=new Vector2(Mathf.Cos(angle)*390,60+Mathf.Sin(angle)*390),velocity=new Vector2(Mathf.Cos(angle)*48,60+Mathf.Sin(angle)*38),phase=i*.618f});
  }
  if(!string.IsNullOrEmpty(artworkId)){
   rough.sprite=MainlineArt.Art(MainlineCatalog.ArtIndex(artworkId),true);pixel.sprite=MainlineCatalog.Artwork(artworkId);frame.enabled=false;
   foreach(var label in text.GetComponentsInChildren<TMPro.TMP_Text>())label.text=label.text=="小狐狸"?MainlineCatalog.All[MainlineCatalog.ArtIndex(artworkId)].artName:"已收入画册";
  }
  UICraft.Button(content,"SKIP / COLLECT",0,-790,680,110,UICraft.Green,Skip,32);
  running=true;Render();
 }
```

### Update · L45

方法 SHA-256：`bbd7ea43bf27655ada4dcbc24661296582739af95bcafba59e8cbcb569c99514`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.PaintingCollectionPresentation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(!running)return;Elapsed+=Time.unscaledDeltaTime;Render();if(Elapsed>=Duration&&!called){called=true;running=false;var callback=completed;completed=null;callback?.Invoke();}}
```

### Render · L46

方法 SHA-256：`6d2b0fb3e1c51392ae27d9d46f5366f06c68ad9ee7f088f149bf020a74a802b3`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.PaintingCollectionPresentation.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/PaintingCollectionPresentation.cs:40` · `PaintingCollectionPresentation.Begin` → `Render()`
- `Assets/Scripts/UI/PaintingCollectionPresentation.cs:45` · `PaintingCollectionPresentation.Update` → `Render()`

```csharp
void Render(){
  var parent=transform.parent as RectTransform;if(parent!=null)content.localScale=Vector3.one*Mathf.Min(parent.rect.width/1080,parent.rect.height/1850);
  float intro=Ease(Mathf.Clamp01(Elapsed/.45f)),blend=Ease(Mathf.Clamp01((Elapsed-1.35f)/1.35f));
  dimmer.color=new Color(0,0,0,.68f*intro);
  art.localScale=Vector3.one*Mathf.Lerp(.88f,1,UICraft.OutBack(intro));
  Alpha(rough,1-blend);Alpha(pixel,blend);Alpha(frame,Ease(Mathf.Clamp01((Elapsed-2.05f)/.8f)));
  captions.alpha=Ease(Mathf.Clamp01((Elapsed-2.65f)/.45f));
  foreach(var s in sparks){float age=Elapsed-1.65f-s.phase%1*.4f;float p=Mathf.Clamp01(age/2.1f);s.image.rectTransform.anchoredPosition=s.start+s.velocity*Mathf.Max(0,age);s.image.rectTransform.localRotation=Quaternion.Euler(0,0,s.phase*57+age*35);float a=age<0?0:Mathf.Sin(p*Mathf.PI)*.8f;s.image.color=new Color(1,.85f,.49f,a);s.image.rectTransform.localScale=Vector3.one*(.55f+Mathf.Sin(p*Mathf.PI)*.75f);}
  whole.alpha=1-Ease(Mathf.Clamp01((Elapsed-4.1f)/.5f));
 }
```

## 对象层级和素材入口

- L20：`UICraft.Root(transform,"Full screen black collection dimmer")`
- L22：`UICraft.Root(transform,"Collection content",0,0,1080,1800)`
- L23：`UICraft.Root(content,"Framed fox artwork",0,60,960,960)`
- L24：`UICraft.Box(art,"Artwork paper",0,0,658,658,new Color32(248,242,233,255))`
- L25：`MakeImage(art,"Original irregular paint",PaintingArt.Image("fox_splatter"),650)`
- L25：`PaintingArt.Image("fox_splatter")`
- L26：`MakeImage(art,"Collected regular pixel artwork",PaintingArt.Image("fox_pixel"),650*720/768)`
- L26：`PaintingArt.Image("fox_pixel")`
- L27：`MakeImage(art,"Generated honey wood picture frame",PaintingArt.Image("fox_frame"),960)`
- L27：`PaintingArt.Image("fox_frame")`
- L28：`UICraft.Root(content,"Collection captions")`
- L29：`UICraft.Label(text,"小狐狸",0,-520,900,100,62)`
- L30：`UICraft.Label(text,saved?"小狐狸001 · 已收入画册":"小狐狸001 · 创作完成",0,-615,940,70,29)`
- L32：`UICraft.Box(content,"Collection golden paint spark",0,0,i%3==0?13:7,i%3==0?13:7,Color.white,true)`
- L36：`MainlineArt.Art(MainlineCatalog.ArtIndex(artworkId),true)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){running=false;completed=null;}
```

## 源字段与依赖

```csharp
public const float Duration=4.6f;
```
```csharp
readonly List<Spark> sparks=new List<Spark>();
```
```csharp
RectTransform content,art;
```
```csharp
CanvasGroup whole;
```
```csharp
Image dimmer,rough,pixel,frame;
```
```csharp
CanvasGroup captions;
```
```csharp
Action completed;
```
```csharp
bool running,called;
```
```csharp
string artworkId;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
