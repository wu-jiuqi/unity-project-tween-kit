# TutorialMotionBoard · TutorialMotionBoard

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/TutorialMotionBoard.cs`
文件 SHA-256：`456e83689291a2820029ddac56a124662a46ad9437bc39d977da485f7e357d5d`
[原始源码快照](../source/Assets/Scripts/UI/TutorialMotionBoard.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Configure · L28

方法 SHA-256：`2282db9a5e9894a7e709c0f19d85b986a272a6759aded420272aa023a82dea2e`
时间来源：SetUpdate(true)
曲线证据：Ease.Linear
解析符号：`BounceGarden.TutorialMotionBoard.Configure(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AlphabetIntroductionView.Motion.cs:18` · `AlphabetIntroductionView.SetMotionLesson` → `motion.Configure(mode)`

```csharp
public void Configure(int next){mode=next;clock=0;cycle=0;timeline?.Kill();timeline=DOTween.To(()=>clock,t=>clock=t,Duration,Duration).SetEase(Ease.Linear).SetLoops(-1,LoopType.Restart).SetUpdate(true).OnStepComplete(()=>cycle++);Render();}
```

### Render · L32

方法 SHA-256：`15c73986034796ecf846a3d8db7825302d237849216c81f61552d26a9bea821c`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin, Mathf.SmoothStep
解析符号：`BounceGarden.TutorialMotionBoard.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/TutorialMotionBoard.cs:28` · `TutorialMotionBoard.Configure` → `Render()`
- `Assets/Scripts/UI/TutorialMotionBoard.cs:30` · `TutorialMotionBoard.Sample` → `Render()`
- `Assets/Scripts/UI/TutorialMotionBoard.cs:53` · `TutorialMotionBoard.Update` → `Render()`

```csharp
void Render(){
  if(hand==null)return;float t=clock;bool hit=t>2.4f&&t<3.6f;
  for(int i=0;i<25;i++){tiles[i].sprite=JellyCollectionArt.Sprite(0);tiles[i].color=Color.white;tiles[i].rectTransform.localScale=Vector3.one;numbers[i].text="1";numbers[i].color=UICraft.Ink;}
  task.text=mode<2?"黄色砖块 · "+(mode==1&&t>=1.6f?"5":"6"):mode==2?"一组连发 · "+Mathf.Clamp(Mathf.CeilToInt((t-.4f)/.3f),0,4)+" / 4 BREAKS":"";
  foreach(var link in links)Alpha(link,0);
  Alpha(specialEmblem,0);Alpha(hbeam,0);Alpha(vbeam,0);Alpha(loot,0);Alpha(spark,0);Alpha(nice,0);
  float u=Mathf.SmoothStep(0,1,Mathf.Clamp01((t-.6f)/1.3f));Vector2 tip=Vector2.Lerp(new Vector2(100,-470),new Vector2(0,-355),u);
  hand.rectTransform.anchoredPosition=tip+new Vector2(88,-80);hand.sprite=TutorialArt.Get(t<.6f?"hand-press":t>2.2f?"hand-release":"hand-drag");ring.rectTransform.anchoredPosition=tip;ring.rectTransform.localScale=Vector3.one*(.9f+.1f*Mathf.Sin(t*8));
  float gesture=t>.2f&&t<2.5f?1:0;Alpha(hand,gesture);Alpha(ring,gesture*.8f);
  for(int i=0;i<dots.Count;i++){dots[i].rectTransform.anchoredPosition=Vector2.Lerp(new Vector2(0,-485),new Vector2(0,-240),i/7f);Alpha(dots[i],gesture);}
  if(mode==5){
   foreach(int i in new[]{5,9,17,21,23}){tiles[i].sprite=SevenSpecialArt.OrdinaryIcon(0);tiles[i].color=Color.white;if(hit){Alpha(tiles[i],0);Alpha(numbers[i],0);}}
   Tool(12,3);hand.rectTransform.anchoredPosition=Cell(12)+new Vector2(88,-80);ring.rectTransform.anchoredPosition=Cell(12);foreach(var d in dots)Alpha(d,0);if(hit){Alpha(tiles[12],0);Alpha(specialEmblem,0);spark.rectTransform.anchoredPosition=Cell(12);Alpha(spark,1);}return;
  }
  if(mode<2){foreach(int i in new[]{11,12,13,16,17,18}){tiles[i].sprite=SevenSpecialArt.OrdinaryIcon(1);numbers[i].color=Color.white;}
   if(mode==1){Alpha(hand,0);Alpha(ring,0);foreach(var d in dots)Alpha(d,0);if(t>.7f){Alpha(tiles[17],0);Alpha(numbers[17],0);}float f=Mathf.SmoothStep(0,1,Mathf.Clamp01((t-.7f)/.9f));loot.rectTransform.anchoredPosition=Vector2.Lerp(Cell(17),new Vector2(0,555),f)+Vector2.right*(Mathf.Sin(f*Mathf.PI)*120);loot.rectTransform.localScale=Vector3.one*(1-.5f*f);Alpha(loot,t>.7f&&t<1.6f?1:0);task.rectTransform.localScale=Vector3.one*(1+(t>=1.6f&&t<1.95f?Mathf.Sin((t-1.6f)/.35f*Mathf.PI)*.1f:0));}
  }else if(mode==2){Alpha(hand,0);Alpha(ring,0);foreach(var d in dots)Alpha(d,0);int broken=0;foreach(int i in new[]{20,21,23,24}){if(t>.4f+broken*.3f){Alpha(tiles[i],0);Alpha(numbers[i],0);}broken++;}Alpha(nice,t>1.6f&&t<4?1:0);if(t>2.2f&&t<3.1f)for(int i=0;i<5;i++){float v=Mathf.Clamp01((t-2.2f)/.9f);dots[i].rectTransform.anchoredPosition=Vector2.Lerp(new Vector2((i-2)*90,-220),new Vector2(0,-500),v);Alpha(dots[i],1);}if(t>3.1f){int target=new[]{6,8,12,16}[cycle%4];Tool(target,1);spark.rectTransform.anchoredPosition=Cell(target);Alpha(spark,t<3.9f?1:0);}}
  else{int kind=mode-2;Tool(12,kind);if(mode==3&&hit){Alpha(hbeam,1);Alpha(vbeam,1);}if(mode==4&&hit){foreach(int i in new[]{6,7,8,11,12,13,16,17,18}){tiles[i].color=new Color(1,.82f,.35f);tiles[i].rectTransform.localScale=Vector3.one*1.04f;}}if(mode==5){int[] members={5,9,17,21,23};if(t>1.1f){Alpha(tiles[12],0);foreach(int i in members){tiles[i].color=new Color(.8f,.65f,1);tiles[i].rectTransform.localScale=Vector3.one*(hit?.78f:1);if(hit){Alpha(tiles[i],.25f);Alpha(numbers[i],.25f);}}}if(hit){spark.rectTransform.anchoredPosition=Cell(17);Alpha(spark,1);}}}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnApplicationFocus(bool value){focus=value||RuntimeQA.IsIsolated;if(focus)timeline?.Play();else timeline?.Pause();}
```

```csharp
void OnDestroy(){timeline?.Kill();}
```

## 源字段与依赖

```csharp
readonly List<Image> tiles=new List<Image>();
```
```csharp
readonly List<TMP_Text> numbers=new List<TMP_Text>();
```
```csharp
readonly List<Image> dots=new List<Image>();
```
```csharp
readonly List<Image> links=new List<Image>();
```
```csharp
Image hand,ring,spark,loot,hbeam,vbeam,specialEmblem;
```
```csharp
TMP_Text nice,task;
```
```csharp
Tween timeline;
```
```csharp
float clock;
```
```csharp
int mode,cycle;
```
```csharp
bool focus=true;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
