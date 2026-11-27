# OpeningStoryStep049 · OpeningStory049

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/OpeningStory049.cs`
文件 SHA-256：`52543b2c315cd7de5ff960db59eb45811dcf5c8ce34d3b98772b2befd2ceca55`
[原始源码快照](../source/Assets/Scripts/UI/OpeningStory049.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Render · L44

方法 SHA-256：`6e60e31afe1e525ddec2f75b6fa03d191013e72b6d685291d7e3ef1fa7397d1e`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.OpeningStoryStep049.Render(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/OpeningStory049.cs:26` · `OpeningStory049.Render` → `steps[i].Render(i==active?phase:Rest(PageId,i))`

```csharp
public void Render(float phase){foreach(var i in images)i.gameObject.SetActive(false);badge.gameObject.SetActive(false);number.gameObject.SetActive(false);float p=Mathf.Clamp01((phase-.2f)/.55f);
  if(page==501){if(step==0){Pic(2,Tray(),19,61,322,151);Pic(3,SevenSpecialArt.OrdinaryIcon(1),44,84,97,97);Label("9 / 12",232,135,true);}else if(step==1){for(int j=0;j<3;j++)Pic(j,SevenSpecialArt.Ordinary(1,Mathf.Min(15,(int)(p*16))),23+j*86,59,140,140,1-Mathf.Clamp01((p-.7f)/.3f));if(phase<.45f)Pic(4,YarnArt.Ball(0),166,220-phase*225,33,33);if(phase>.7f)Badge("目标进度 +3");}else{Pic(3,SevenSpecialArt.OrdinaryIcon(1),43,64,103,103);Label("12 / 12",235,116);Badge("目标完成");}return;}
  if(page==505||page==506||page==518){int layers=page==518?3:page==506?2:1,before=layers==3?3-step:layers==2&&step<2?2:1;
   if((layers==1||layers==2)&&step==0){Yarn(OpeningYarnArt.RestKey(before));Bricks(0);}else if(layers==1&&step==2){Goal(OpeningYarnArt.Rest(1),"毛线目标 +1");}else{Bricks(p);if(p<.12f)Yarn(OpeningYarnArt.RestKey(before));else if(p<.85f)Yarn(before==1?"yarn-unravel":before==3?"yarn-layer-loss-l3":"yarn-layer-loss",Mathf.Clamp((int)((p-.12f)/.73f*16),0,15));else if(before>1)Yarn(OpeningYarnArt.RestKey(before-1));if(before>1)Badge(before+" 层 → "+(before-1)+" 层",249);else if(p>=.85f)Goal(OpeningYarnArt.Rest(1),"毛线目标 +1");}return;}
  if(page==511){int count=step==0?8:step==1?(p<.45f?8:7):(p<.45f?8:6);Pic(2,SecondGroupArt.Sprite("box-"+count),100,40,160,170);if(step<2)Bricks(step==0?0:p,1);else{Pic(5,null,15,127,330,5);images[5].color=new Color32(255,246,183,255);}if(step>0&&p>.3f){for(int j=0;j<step;j++)Pic(6+j,SecondGroupArt.Sprite("fish-biscuit"),148+j*52+p*15,65-p*46,63,31);Badge("小鱼干 +"+step);}return;}
  if(page==521){if(step==0){int n=(phase>=.35f?1:0)+(phase>=.78f?1:0);Cat(n);Bricks(Mathf.Clamp01((phase-.12f)/.23f),1);Bricks(Mathf.Clamp01((phase-.55f)/.23f),2);for(int j=0;j<3;j++){Pic(6+j,null,125+j*40,230,25,25);images[6+j].color=j<n?OpeningStory049.Gold:new Color32(83,89,76,255);}}else if(step==1){Bricks(Mathf.Clamp01((phase-.05f)/.3f),1);Cat(phase<.15f?2:Mathf.Clamp(3+(int)((phase-.15f)/.85f*13),3,15),true);float t=Mathf.Clamp01((phase-.35f)/.4f);for(int j=0;j<5;j++){float a=Mathf.PI+j*Mathf.PI/4;Pic(3+j,OpeningThirdArt.Frame("ingot",0),150+Mathf.Cos(a)*126*t,105+Mathf.Sin(a)*82*t,61,58,t);}Badge("生成 5 枚");}else{Pic(2,OpeningThirdArt.Frame("ingot",0),129,63-p*45,103,103,1-p);if(p>.5f)Goal(OpeningThirdArt.Frame("ingot",0),"元宝目标 +1");}return;}
  if(page==531){if(step==2){Bricks(p);if(p<.55f){Pic(2,OpeningCopperArt.State(25),104,46,152,155);Label("25",180,121,true);}else if(p<.9f)Pic(2,OpeningCopperArt.Frame("burst",Mathf.Clamp((int)((p-.55f)/.35f*8),0,7)),63,16,234,211);else Goal(OpeningCopperArt.State(25),"铜钱目标 +1");}else{int hp=step==0?25:Mathf.Max(0,25-(int)(phase/.8f*25));if(hp>0){Pic(2,OpeningCopperArt.State(hp),94,36,172,172);Label(hp.ToString(),180,121,true);}else{Label("0");Badge("铜钱目标 +1");}if(step==1&&hp>0){Pic(4,YarnArt.Ball(0),32+phase*45,169-phase*44,35,35);Badge("扣至 0 后收集");}}}
 }
```

## 对象层级和素材入口

- L45：`Label("9 / 12",232,135,true)`
- L50：`Label("25",180,121,true)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
int page,step;
```
```csharp
Image[] images=new Image[12];
```
```csharp
Image badge;
```
```csharp
TMP_Text badgeText,number;
```
```csharp
static Sprite tray;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
