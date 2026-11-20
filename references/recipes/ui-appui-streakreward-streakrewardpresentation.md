# StreakRewardPresentation · AppUI.StreakReward

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.StreakReward.cs`
文件 SHA-256：`4c5255d2dfd52a6300896f190541055ae204aa716487b88b4f59b914c4dfe53c`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.StreakReward.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Play · L27

方法 SHA-256：`300f1c40150dee5a7ad2609d48ab08d943be53766b9c205259057975492b58f2`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Exp, Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.StreakRewardPresentation.Play(int, System.Collections.Generic.List<BounceGarden.StreakBooster>, System.Collections.Generic.List<UnityEngine.RectTransform>, bool)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public IEnumerator Play(int wins,List<StreakBooster> kinds,List<RectTransform> targets,bool repeated=false){
  canAccelerate=repeated;
  var root=(RectTransform)transform;
  var shade=UICraft.Box(root,"全屏压暗",0,0,10000,10000,Color.clear,false,true);shade.sprite=null;
  var title=UICraft.Label(root,wins+"连胜奖励",0,440,950,100,62);title.color=new Color(1,.94f,.7f,0);
  var note=UICraft.Label(root,"赠送本关道具 · 点击选择后再发射",0,-260,1000,70,32);note.color=new Color(1,1,1,0);
  if(repeated){var hint=UICraft.Label(root,"点击加速",0,-360,600,70,34);hint.color=Color.white;}
  var cards=new RectTransform[kinds.Count];var halos=new RectTransform[kinds.Count];var labels=new CanvasGroup[kinds.Count];var starts=new Vector2[kinds.Count];var ends=new Vector2[kinds.Count];
  for(int i=0;i<kinds.Count;i++){
   var group=targets[i].gameObject.AddComponent<CanvasGroup>();group.alpha=0;hidden.Add(group);
   starts[i]=new Vector2((i-(kinds.Count-1)*.5f)*310,70);
   // Resolve the real HUD position through the hierarchy, including safe-area transforms.
   ends[i]=root.InverseTransformPoint(targets[i].position);
   cards[i]=UICraft.Root(root,"赠送 "+kinds[i],starts[i].x,starts[i].y,300,300);
   halos[i]=UICraft.Root(cards[i],"旋转闪光",0,0,440,440);
   var glow=halos[i].gameObject.AddComponent<StreakRewardGlow>();glow.raycastTarget=false;glow.color=new Color(1,.86f,.4f,.8f);
   var icon=UICraft.Box(cards[i],"奖励道具",0,0,250,250,Color.white);GardenArt70.Orb(icon,kinds[i],240,0,8);
   var label=UICraft.Label(cards[i],StreakTrials.Name(kinds[i])+" ×1",0,-175,310,74,30);label.color=Color.white;labels[i]=label.gameObject.AddComponent<CanvasGroup>();
   cards[i].localScale=Vector3.zero;
  }
  SoundService.Play("streak_reward");
  const float duration=2.35f;float elapsed=0;
  while(elapsed<duration){
   if(this==null||root==null)yield break;
   elapsed+=Time.unscaledDeltaTime*(accelerated?4:1);float reveal=Mathf.Clamp01((elapsed-.18f)/.42f),fly=Mathf.Clamp01((elapsed-1.22f)/.72f),fade=Mathf.Clamp01((elapsed-2.05f)/.30f);
   Phase=elapsed<.18f?"dim":elapsed<1.22f?"reveal":elapsed<1.94f?"fly":"restore";
   shade.color=new Color(.035f,.025f,.09f,.76f*Mathf.Clamp01(elapsed/.18f)*(1-fade));
   title.color=new Color(1,.94f,.7f,reveal*(1-Mathf.Clamp01(fly*2)));note.color=new Color(1,1,1,reveal*(1-Mathf.Clamp01(fly*2)));
   for(int i=0;i<cards.Length;i++){
    float ease=fly*fly*(3-2*fly);cards[i].anchoredPosition=Vector2.Lerp(starts[i],ends[i],ease)+Vector2.up*(Mathf.Sin(fly*Mathf.PI)*120);
    cards[i].localScale=Vector3.one*Mathf.Lerp(UICraft.OutBack(reveal),112f/240f,ease);
    halos[i].localRotation=Quaternion.Euler(0,0,elapsed*38);halos[i].localScale=Vector3.one*(1+.07f*Mathf.Sin(elapsed*9));
    var glow=halos[i].GetComponent<Graphic>();glow.color=new Color(1,.88f,.5f,(.6f+.25f*Mathf.Exp(-Mathf.Pow((elapsed-.35f)*8,2)))*(1-fly));labels[i].alpha=1-fly;
    if(fly>=1){hidden[i].alpha=1;cards[i].gameObject.SetActive(false);}
   }
   yield return null;
  }
  Phase="complete";
 }
```

## 对象层级和素材入口

- L30：`UICraft.Box(root,"全屏压暗",0,0,10000,10000,Color.clear,false,true)`
- L31：`UICraft.Label(root,wins+"连胜奖励",0,440,950,100,62)`
- L32：`UICraft.Label(root,"赠送本关道具 · 点击选择后再发射",0,-260,1000,70,32)`
- L33：`UICraft.Label(root,"点击加速",0,-360,600,70,34)`
- L40：`UICraft.Root(root,"赠送 "+kinds[i],starts[i].x,starts[i].y,300,300)`
- L41：`UICraft.Root(cards[i],"旋转闪光",0,0,440,440)`
- L43：`UICraft.Box(cards[i],"奖励道具",0,0,250,250,Color.white)`
- L44：`UICraft.Label(cards[i],StreakTrials.Name(kinds[i])+" ×1",0,-175,310,74,30)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDestroy(){foreach(var group in hidden)if(group!=null){group.alpha=1;Destroy(group);}}
```

## 源字段与依赖

```csharp
readonly List<CanvasGroup> hidden=new List<CanvasGroup>();
```
```csharp
bool canAccelerate,accelerated;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
