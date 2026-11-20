# BattleInterfaceTour · BattleInterfaceTour

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/BattleInterfaceTour.cs`
文件 SHA-256：`f7de6a846c4a7f68c8cd1b8d064ae97ccdd02e19f4b611ce08a0b2baac33a15a`
[原始源码快照](../source/Assets/Scripts/UI/BattleInterfaceTour.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Begin · L28

方法 SHA-256：`5362800d581a65899374f07842decc7bd73bbb2a3888d6b83a30aa6ec3a593b5`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BattleInterfaceTour.Begin(BounceGarden.AppUI, BounceGarden.ChainCollectionHUD, System.Action)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Begin(AppUI owner,ChainCollectionHUD collection,Action done){
  app=owner;hud=collection;completed=done;root=(RectTransform)transform;
  var blocker=gameObject.AddComponent<Image>();blocker.color=Color.clear;blocker.raycastTarget=true;
  var tap=gameObject.AddComponent<Button>();tap.transition=Selectable.Transition.None;tap.onClick.AddListener(Advance);
  for(int i=0;i<4;i++){dim[i]=UICraft.Box(root,"Tour dim "+i,0,0,1,1,new Color(.08f,.1f,.06f,.7f));dim[i].sprite=null;dim[i].raycastTarget=false;}
  outline=TutorialArt.Picture(root,"Live interface focus","target-outline",Vector2.zero,Vector2.one*150);outline.raycastTarget=false;
  bubble=UICraft.Root(root,"Garden tour speech",0,0,850,440);
  var plate=OnboardingArt.Draw(bubble,"Speech background",1,1,0,0,850,440);plate.type=Image.Type.Sliced;plate.preserveAspect=false;
  heading=UICraft.Label(bubble,"",0,125,740,70,43,UICraft.Ink);body=UICraft.Label(bubble,"",0,10,730,180,36,UICraft.Ink);body.textWrappingMode=TextWrappingModes.Normal;
  progress=UICraft.Label(bubble,"",0,-135,740,60,28,new Color(.35f,.52f,.30f));
  hand=OnboardingArt.Draw(root,"Tour pointer",2,0,0,0,145,185).rectTransform;
  lastAdvance=Time.unscaledTime;Place();
 }
```

### Place · L44

方法 SHA-256：`6cd3d8914f9457acbccf801fa83473fa696ffabb57a95561968ed7764ddbe3b4`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.BattleInterfaceTour.Place()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/BattleInterfaceTour.cs:39` · `BattleInterfaceTour.Begin` → `Place()`
- `Assets/Scripts/UI/BattleInterfaceTour.cs:61` · `BattleInterfaceTour.Advance` → `Place()`
- `Assets/Scripts/UI/BattleInterfaceTour.cs:63` · `BattleInterfaceTour.Update` → `Place()`

```csharp
void Place(){
  if(hud==null||Camera.main==null)return;Rect focus;
  if(step==0)focus=RectOf(hud.GoalsRect);
  else if(step==1)focus=RectOf(hud.Cards[0].ContentRect);
  else if(step==2)focus=RectOf(hud.StepsRect);
  else {Vector2 p=World(step==3?FindTarget():app.Session.LauncherPosition);focus=new Rect(p-Vector2.one*(step==3?65:115),Vector2.one*(step==3?130:230));}
  var r=root.rect;focus=Rect.MinMaxRect(Mathf.Clamp(focus.xMin,r.xMin,r.xMax),Mathf.Clamp(focus.yMin,r.yMin,r.yMax),Mathf.Clamp(focus.xMax,r.xMin,r.xMax),Mathf.Clamp(focus.yMax,r.yMin,r.yMax));FocusRect=focus;
  Block(0,(r.xMin+focus.xMin)*.5f,r.center.y,focus.xMin-r.xMin,r.height);Block(1,(focus.xMax+r.xMax)*.5f,r.center.y,r.xMax-focus.xMax,r.height);Block(2,focus.center.x,(r.yMin+focus.yMin)*.5f,focus.width,focus.yMin-r.yMin);Block(3,focus.center.x,(focus.yMax+r.yMax)*.5f,focus.width,r.yMax-focus.yMax);
  outline.rectTransform.anchoredPosition=focus.center;outline.rectTransform.sizeDelta=focus.size+Vector2.one*10;outline.rectTransform.localScale=Vector3.one*(1+.018f*Mathf.Sin(age*5));
  float safeTop=r.yMax-110,safeBottom=r.yMin+130;float y=focus.center.y>0?focus.yMin-280:focus.yMax+280;y=Mathf.Clamp(y,safeBottom+220,safeTop-220);
  float width=Mathf.Min(850,r.width-70);bubble.sizeDelta=new Vector2(width,440);bubble.localScale=Vector3.one*Mathf.Min(1,width/850);bubble.anchoredPosition=new Vector2(0,y);
  hand.anchoredPosition=new Vector2(Mathf.Clamp(focus.xMax+20,r.xMin+90,r.xMax-90),focus.yMin-80+Mathf.Sin(age*4)*10);
  string[] titles={"这是本关的收集目标","看看图标和数字","这是剩余步数","在棋盘上找到它","准备发射！"};
  string[] copy={"把上方的目标全部收集齐，\n就能过关。","图标告诉你要收集什么。\n数字表示还需要收集多少个。","每次发射消耗1步，\n一步会连续发出5颗球。","打碎对应的目标砖，\n收集物就会飞进上方目标区。","拖动瞄准，松手发射。\n试试利用墙壁反弹！"};
  heading.text=titles[step];body.text=copy[step];progress.text=(step+1)+" / 5　·　"+(step==4?"点击开始":"点击继续");
 }
```

### Advance · L61

方法 SHA-256：`47120d83a1241f6210025210f722a0eeff447d82fa53097d8186e3784f28a91d`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BattleInterfaceTour.Advance()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/BattleInterfaceTour.cs:62` · `BattleInterfaceTour.AdvanceForQA` → `Advance()`

```csharp
public void Advance(){if(Time.unscaledTime-lastAdvance<.25f)return;lastAdvance=Time.unscaledTime;if(step<4){step++;age=0;Place();SoundService.Play("click");return;}var done=completed;completed=null;gameObject.SetActive(false);done?.Invoke();}
```

### Update · L63

方法 SHA-256：`2570a018e250807fd74bcf8140eb483cb076a5bea4501080471f89720437ff01`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BattleInterfaceTour.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){age+=Time.unscaledDeltaTime;Place();}
```

## 对象层级和素材入口

- L32：`UICraft.Box(root,"Tour dim "+i,0,0,1,1,new Color(.08f,.1f,.06f,.7f))`
- L34：`UICraft.Root(root,"Garden tour speech",0,0,850,440)`
- L36：`UICraft.Label(bubble,"",0,125,740,70,43,UICraft.Ink)`
- L37：`UICraft.Label(bubble,"",0,-135,740,60,28,new Color(.35f,.52f,.30f))`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
AppUI app;
```
```csharp
ChainCollectionHUD hud;
```
```csharp
RectTransform root,bubble,hand;
```
```csharp
readonly Image[] dim=new Image[4];
```
```csharp
Image outline;
```
```csharp
TMP_Text heading,body,progress;
```
```csharp
Action completed;
```
```csharp
int step;
```
```csharp
float age,lastAdvance;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
