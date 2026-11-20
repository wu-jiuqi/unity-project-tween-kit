# OpeningRuleDemo · OpeningIntroduction

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/OpeningIntroduction.cs`
文件 SHA-256：`c467bf55eb7c4ad9d5866a4a26dff546585c47bab4b503afe437de0134994ea0`
[原始源码快照](../source/Assets/Scripts/UI/OpeningIntroduction.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Render · L40

方法 SHA-256：`77ad449958fd8f82897caf721e608f8273e0bfc16e88ca8fc9f2edde9ab6476d`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.OpeningRuleDemo.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/OpeningIntroduction.cs:37` · `OpeningRuleDemo.Initialize` → `Render()`
- `Assets/Scripts/UI/OpeningIntroduction.cs:55` · `OpeningRuleDemo.Update` → `Render()`

```csharp
void Render(){float t=age%7;Alpha(left,1);Alpha(right,1);Alpha(target,1);Alpha(ball,0);targetRect.anchoredPosition=Vector2.zero;targetRect.localScale=Vector3.one;
  if(page==531){int hp=Mathf.Max(1,25-Mathf.FloorToInt(Mathf.Min(t,4.99f)*5));target.sprite=OnboardingArt.Get(7,OpeningCopperArt.StateIndex(hp));if(copperHp!=null){copperHp.text=hp.ToString();var ink=OpeningCopperArt.NumberColor;copperHp.color=new Color(ink.r,ink.g,ink.b,t>=5?Mathf.Clamp01((6-t)*2):1);}if(t>=5){Alpha(left,0);Alpha(target,Mathf.Clamp01((6-t)*2));}caption.text=t<5?"逐点扣血，每损失5点改变外观":"邻格消除：无视血量，直接收集";return;}
  if(page==521){int stage=t<1.5f?0:t<3?1:t<4.5f?2:Mathf.Clamp(3+(int)((t-4.5f)*8),3,15);target.sprite=OnboardingArt.Get(6,stage<3?stage:3);right.sprite=t<4.5f?SevenSpecialArt.OrdinaryIcon(0):OnboardingArt.Get(6,4);caption.text=t<1.5f?"收爪 → 初始":t<3?"一次相邻消除 → 单爪":t<4.5f?"两次相邻消除 → 双爪":"第三次挥爪 → 5枚元宝，再回到初始";return;}
  if(page==511){int remaining=t<1.5f?8:t<3.5f?7:5;target.sprite=t<1.5f?OnboardingArt.Get(5,0):SecondGroupArt.Sprite("box-"+remaining);targetRect.sizeDelta=Vector2.one*290;target.preserveAspect=true;float local=t<3.5f?t-1.5f:t-3.5f;targetRect.anchoredPosition=Vector2.up*(Mathf.Sin(Mathf.Clamp01(local/.34f)*Mathf.PI)*38);if(t>=1.5f)Alpha(left,0);if(t>=3.5f)Alpha(right,0);caption.text=t<1.5f?"在完整盒子旁消除":t<3.5f?"相邻消除 → 小鱼干 +1":"激光穿过盒子两格 → 小鱼干 +2";return;}
  if(page==518){string key=t<1.5f?"yarn-obstacle-l3":t<2.18f?"yarn-layer-loss-l3":t<3.5f?"yarn-obstacle-l2":t<4.18f?"yarn-layer-loss":t<5.5f?"yarn-obstacle-l1":"yarn-unravel";float start=key=="yarn-layer-loss-l3"?1.5f:key=="yarn-layer-loss"?3.5f:5.5f;int frame=key.StartsWith("yarn-obstacle")?0:Mathf.Clamp((int)((t-start)/.68f*16),0,15);target.sprite=key.StartsWith("yarn-obstacle")?OnboardingArt.Get(4,t<1.5f?2:t<3.5f?1:0):OpeningYarnArt.Frame(key,frame);if(t>1.5f)Alpha(left,0);if(t>3.5f)Alpha(right,0);if(t>6.18f)Alpha(target,0);caption.text=t<1.5f?"青色编织外圈：三级":t<3.5f?"脱掉青色外圈 → 二级":t<5.5f?"再脱紫色交叉圈 → 一级":"完全解开 → 毛线收集 +1";return;}
  if(page==505||page==506){bool two=page==506;float second=two?3.5f:1.5f;bool unwrap=two&&t>=1.5f&&t<2.18f,clear=t>=second&&t<second+.64f;target.sprite=OnboardingArt.Get(4,two&&t<1.5f?1:0);if(unwrap)target.sprite=OpeningYarnArt.Frame("yarn-layer-loss",Mathf.Clamp((int)((t-1.5f)/.68f*16),0,15));if(clear)target.sprite=OpeningYarnArt.Frame("yarn-unravel",Mathf.Clamp((int)((t-second)/.64f*16),0,15));if(t>=1.5f)Alpha(left,0);if(t>=second)Alpha(right,0);if(t>=second+.64f)Alpha(target,0);caption.text=t<1.5f?"消除上下左右的普通砖":two&&t<second?"紫色外圈脱落 → 一级 · 收集数不变":t<second+.64f?"完全解开，准备收集":"收集 +1";
  }else{Alpha(ball,t<1.5f?1:0);ball.rectTransform.anchoredPosition=new Vector2(0,Mathf.Lerp(-280,-100,Mathf.Clamp01(t/1.5f)));float k=Mathf.Sin(Mathf.Clamp01((t-1.5f)/.24f)*Mathf.PI)*.12f;targetRect.localScale=new Vector3(1+k,1-k,1);if(t>1.5f&&t<4.5f){if(page==512||page==513){Alpha(left,0);Alpha(right,0);}else Alpha(target,0);}caption.text=page==501?"消除目标颜色，增加收集进度":page==512?"一次爆炸，清除周围砖块":"沿这一整排横向消除";}
 }
```

### Update · L55

方法 SHA-256：`547539215c065763dd76823b3a5d6baaa56d0425c45c1912f749009061cb3125`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.OpeningRuleDemo.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){age=Mathf.Repeat(age+Time.unscaledDeltaTime*(7f/2.2f),7f);Render();FitYarnDemo();}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
Image target,left,right,ball;
```
```csharp
RectTransform targetRect;
```
```csharp
TMPro.TMP_Text caption,copperHp;
```
```csharp
int page;
```
```csharp
float age;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
