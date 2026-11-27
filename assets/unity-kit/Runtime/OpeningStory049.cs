using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace ProjectTweenKit {
// Tutorial-only visuals: no writes to the board, goal counters, shots, or profile.
public sealed class OpeningStory049:MonoBehaviour {
 public const float LoopDuration=6.72f;
 
 public static readonly Color Cream=new Color32(255,241,214,255),Gold=new Color32(255,218,140,255),Mint=new Color32(188,233,204,255),Muted=new Color32(210,200,182,255);
 public sealed class Copy {public string Title,Subtitle,Note;public string[] Heads,Bodies;public Copy(string t,string s,string n,string[] h,string[] b){Title=t;Subtitle=s;Note=n;Heads=h;Bodies=b;}}
 public static Copy GetCopy(int id){switch(id){
  case 501:return new Copy("收集指定颜色","打碎目标颜色的砖块，收满即可过关","优先瞄准目标颜色，让每一步更有效。",new[]{"先看目标颜色","瞄准同色砖块","收满目标数量"},new[]{"上方图标告诉你收什么","打碎后计入收集进度","不用清空整个棋盘"});
  case 505:return new Copy("绒绒毛线团","消除旁边的砖块，就能解开毛线","球直接撞击无效；爆炸、激光命中也能解开。",new[]{"消除毛线旁的砖块","毛线随之解开","解开后自动收集"},new[]{"上下左右任一侧都有效","一次有效消除即可解开","毛线目标 +1"});
  case 506:return new Copy("双层毛线团","先脱紫色外圈，再解奶油内芯","球直接撞击无效；双五色花可直接解开两层。",new[]{"认准紫色外圈","第一次：脱掉紫圈","第二次：解开内芯"},new[]{"这是一团双层毛线","剩下奶油色内芯","全部解开，才计入目标"});
  case 518:return new Copy("三级毛线团","每消除一次邻格，就解开一层","双五色花可一次解两层；球直接撞击无效。",new[]{"第一次：脱掉青圈","第二次：脱掉紫圈","第三次：解开内芯"},new[]{"露出紫色外圈","剩下奶油色内芯","全部解开，才计入目标"});
  case 511:return new Copy("小鱼干点心盒","消除盒子旁的砖块，收集小鱼干","直接撞盒子无效；爆炸按覆盖格数收集。",new[]{"在盒子旁消除","每触发一格","激光穿过两格"},new[]{"上下左右都有效","收集 1 条小鱼干","一次收集 2 条小鱼干"});
  case 521:return new Copy("招财猫与元宝","在猫咪旁消除三次，生成五枚元宝","招财猫不会被消除，可以反复蓄能。",new[]{"消除猫咪旁的砖块","第三次消除后挥爪","打碎并收集元宝"},new[]{"每次相邻消除，蓄能一次","生成 5 枚元宝","计入上方元宝目标"});
  case 531:return new Copy("果冻铜钱","两种方式都能收集，邻格消除更省力","每少 5 点血，外观变一档；铜钱与元宝分开计数。",new[]{"铜钱初始 25 点血","方法一：直接撞击","方法二：邻格消除"},new[]{"数字表示剩余血量","打空 25 点血后收集","无视剩余血量，直接收集"});
  default:throw new ArgumentOutOfRangeException(nameof(id));
 }}
 public int PageId{get;private set;}public float Age{get;private set;}
 OpeningStoryStep049[] steps=new OpeningStoryStep049[3];
 public void Build(int id){PageId=id;for(int i=0;i<3;i++){var p=UICraft.Root(transform,"Live teaching step "+(i+1),i==1?264:-264,450-i*395,360,280);steps[i]=p.gameObject.AddComponent<OpeningStoryStep049>();steps[i].Build(id,i);}Render();}
 public void Sample(float seconds){Age=Mathf.Max(0,seconds);Render();}
 public void SampleStep(int index,float phase){steps[index].Render(phase);}
 void Render(){int active=Mathf.Min(2,(int)(Age%LoopDuration/(LoopDuration/3)));float phase=Age%(LoopDuration/3)/(LoopDuration/3);for(int i=0;i<steps.Length;i++)if(steps[i]!=null)steps[i].Render(i==active?phase:Rest(PageId,i));}
 public static float Rest(int id,int step){if(id==505)return step==0?.12f:step==1?.38f:.95f;if(id==506)return step==0?.12f:.95f;if(id==518)return .95f;if(id==521)return step==0?.12f:step==1?.92f:.95f;return step==0?.12f:step==1?.6f:.95f;}
}
public sealed class OpeningStoryStep049:MonoBehaviour {
 int page,step;Image[] images=new Image[12];Image badge;TMP_Text badgeText,number;
 public void Build(int id,int index){page=id;step=index;
  for(int i=0;i<images.Length;i++){images[i]=UICraft.Box(transform,"Tutorial sprite "+i,0,0,1,1,Color.white);images[i].raycastTarget=false;images[i].type=Image.Type.Simple;images[i].preserveAspect=true;}
  badge=UICraft.Box(transform,"Collection result label",0,-99,214,48,new Color32(49,82,76,255));badge.raycastTarget=false;
  badgeText=UICraft.Label(badge.transform,"",0,0,214,48,24,OpeningStory049.Mint);badgeText.color=OpeningStory049.Mint;badgeText.raycastTarget=false;
  number=UICraft.Label(transform,"",0,0,260,62,37,OpeningStory049.Gold);number.raycastTarget=false;
 }
 void Label(string text,float x=180,float y=121,bool dark=false){number.gameObject.SetActive(true);number.text=text;number.rectTransform.anchoredPosition=new Vector2(x-180,140-y);number.color=dark?(page==531?OpeningStory049.Cream:(Color)new Color32(73,37,13,255)):OpeningStory049.Gold;if(dark){number.outlineColor=new Color32(255,245,214,255);number.outlineWidth=.15f;}else number.outlineWidth=0;}
 void Badge(string text,float y=239){badge.gameObject.SetActive(true);badgeText.text=text;badge.rectTransform.anchoredPosition=new Vector2(0,140-y);}
 void Pic(int slot,Sprite sprite,float x,float y,float w,float h,float alpha=1){var i=images[slot];i.gameObject.SetActive(alpha>0);i.sprite=sprite;i.material=OpeningStory049Art.Material(sprite);i.color=new Color(1,1,1,Mathf.Clamp01(alpha));var r=i.rectTransform;r.pivot=Vector2.one*.5f;r.localScale=Vector3.one;r.anchoredPosition=new Vector2(x+w*.5f-180,140-y-h*.5f);r.sizeDelta=new Vector2(w,h);i.preserveAspect=true;}
 void Bricks(float p,int sides=3){if((sides&1)!=0)Pic(0,SevenSpecialArt.Ordinary(0,Mathf.Min(15,(int)(p*16))),-13,68,131,131,1-Mathf.Clamp01((p-.7f)/.3f));if((sides&2)!=0)Pic(1,SevenSpecialArt.Ordinary(0,Mathf.Min(15,(int)(p*16))),239,68,131,131,1-Mathf.Clamp01((p-.7f)/.3f));}
 void Yarn(string key,int frame=0){var s=OpeningYarnArt.Spec(key,frame);Pic(2,OpeningYarnArt.Frame(key,frame),0,0,1,1);var im=images[2];im.preserveAspect=false;im.rectTransform.pivot=new Vector2(s.px,s.py);im.rectTransform.sizeDelta=new Vector2(132*s.w/s.bodyWidth,132*s.h/s.bodyWidth);im.rectTransform.anchoredPosition=new Vector2(0,-60);}
 void Cat(int frame,bool small=false){var s=OpeningThirdArt.Frame("cat",frame);float q=(small?138:156)/245f;float w=s.rect.width*q,h=s.rect.height*q;Pic(2,s,180-w/2,211-h,w,h);}
 void Goal(Sprite sprite,string text){Pic(8,sprite,134,65,92,92);Badge(text);}
 public void Render(float phase){foreach(var i in images)i.gameObject.SetActive(false);badge.gameObject.SetActive(false);number.gameObject.SetActive(false);float p=Mathf.Clamp01((phase-.2f)/.55f);
  if(page==501){if(step==0){Pic(2,Tray(),19,61,322,151);Pic(3,SevenSpecialArt.OrdinaryIcon(1),44,84,97,97);Label("9 / 12",232,135,true);}else if(step==1){for(int j=0;j<3;j++)Pic(j,SevenSpecialArt.Ordinary(1,Mathf.Min(15,(int)(p*16))),23+j*86,59,140,140,1-Mathf.Clamp01((p-.7f)/.3f));if(phase<.45f)Pic(4,YarnArt.Ball(0),166,220-phase*225,33,33);if(phase>.7f)Badge("目标进度 +3");}else{Pic(3,SevenSpecialArt.OrdinaryIcon(1),43,64,103,103);Label("12 / 12",235,116);Badge("目标完成");}return;}
  if(page==505||page==506||page==518){int layers=page==518?3:page==506?2:1,before=layers==3?3-step:layers==2&&step<2?2:1;
   if((layers==1||layers==2)&&step==0){Yarn(OpeningYarnArt.RestKey(before));Bricks(0);}else if(layers==1&&step==2){Goal(OpeningYarnArt.Rest(1),"毛线目标 +1");}else{Bricks(p);if(p<.12f)Yarn(OpeningYarnArt.RestKey(before));else if(p<.85f)Yarn(before==1?"yarn-unravel":before==3?"yarn-layer-loss-l3":"yarn-layer-loss",Mathf.Clamp((int)((p-.12f)/.73f*16),0,15));else if(before>1)Yarn(OpeningYarnArt.RestKey(before-1));if(before>1)Badge(before+" 层 → "+(before-1)+" 层",249);else if(p>=.85f)Goal(OpeningYarnArt.Rest(1),"毛线目标 +1");}return;}
  if(page==511){int count=step==0?8:step==1?(p<.45f?8:7):(p<.45f?8:6);Pic(2,SecondGroupArt.Sprite("box-"+count),100,40,160,170);if(step<2)Bricks(step==0?0:p,1);else{Pic(5,null,15,127,330,5);images[5].color=new Color32(255,246,183,255);}if(step>0&&p>.3f){for(int j=0;j<step;j++)Pic(6+j,SecondGroupArt.Sprite("fish-biscuit"),148+j*52+p*15,65-p*46,63,31);Badge("小鱼干 +"+step);}return;}
  if(page==521){if(step==0){int n=(phase>=.35f?1:0)+(phase>=.78f?1:0);Cat(n);Bricks(Mathf.Clamp01((phase-.12f)/.23f),1);Bricks(Mathf.Clamp01((phase-.55f)/.23f),2);for(int j=0;j<3;j++){Pic(6+j,null,125+j*40,230,25,25);images[6+j].color=j<n?OpeningStory049.Gold:new Color32(83,89,76,255);}}else if(step==1){Bricks(Mathf.Clamp01((phase-.05f)/.3f),1);Cat(phase<.15f?2:Mathf.Clamp(3+(int)((phase-.15f)/.85f*13),3,15),true);float t=Mathf.Clamp01((phase-.35f)/.4f);for(int j=0;j<5;j++){float a=Mathf.PI+j*Mathf.PI/4;Pic(3+j,OpeningThirdArt.Frame("ingot",0),150+Mathf.Cos(a)*126*t,105+Mathf.Sin(a)*82*t,61,58,t);}Badge("生成 5 枚");}else{Pic(2,OpeningThirdArt.Frame("ingot",0),129,63-p*45,103,103,1-p);if(p>.5f)Goal(OpeningThirdArt.Frame("ingot",0),"元宝目标 +1");}return;}
  if(page==531){if(step==2){Bricks(p);if(p<.55f){Pic(2,OpeningCopperArt.State(25),104,46,152,155);Label("25",180,121,true);}else if(p<.9f)Pic(2,OpeningCopperArt.Frame("burst",Mathf.Clamp((int)((p-.55f)/.35f*8),0,7)),63,16,234,211);else Goal(OpeningCopperArt.State(25),"铜钱目标 +1");}else{int hp=step==0?25:Mathf.Max(0,25-(int)(phase/.8f*25));if(hp>0){Pic(2,OpeningCopperArt.State(hp),94,36,172,172);Label(hp.ToString(),180,121,true);}else{Label("0");Badge("铜钱目标 +1");}if(step==1&&hp>0){Pic(4,YarnArt.Ball(0),32+phase*45,169-phase*44,35,35);Badge("扣至 0 后收集");}}}
 }
 static Sprite tray;static Sprite Tray(){if(tray==null){var tex=Resources.Load<Texture2D>("Art/BattleUIV42/goals-tray");tray=Sprite.Create(tex,new Rect(0,0,tex.width,tex.height),Vector2.one*.5f,100);}return tray;}
}
// A small UI mesh keeps flow arrows independent of retired artwork and remote assets.
public sealed class OpeningStoryArrow049:Graphic {
 protected override void OnPopulateMesh(VertexHelper vh){vh.Clear();Vector2 a=new Vector2(-85,55),b=new Vector2(85,65),c=new Vector2(85,-55);const int segments=20;
  for(int i=0;i<=segments;i++){float t=i/(float)segments;Vector2 p=(1-t)*(1-t)*a+2*(1-t)*t*b+t*t*c;Vector2 tangent=(2*(1-t)*(b-a)+2*t*(c-b)).normalized;Vector2 n=new Vector2(-tangent.y,tangent.x)*2.8f;vh.AddVert(p-n,color,Vector2.zero);vh.AddVert(p+n,color,Vector2.zero);if(i>0){int k=i*2;vh.AddTriangle(k-2,k-1,k);vh.AddTriangle(k-1,k+1,k);}}
  Vector2 d=(c-b).normalized,normal=new Vector2(-d.y,d.x);int end=vh.currentVertCount;vh.AddVert(c+d*8,color,Vector2.zero);vh.AddVert(c-d*17+normal*12,color,Vector2.zero);vh.AddVert(c-d*17-normal*12,color,Vector2.zero);vh.AddTriangle(end,end+1,end+2);
 }
}
// Same authored-alpha remapping as OpeningYarn/BakedJelly, with a Canvas vertex path.
// Sharing the world-space shader would incorrectly apply BoardClip to UI coordinates.
public static class OpeningStory049Art {
 static Material yarn,jelly;
 public static Material Material(Sprite sprite){if(sprite==null)return null;string n=sprite.texture.name;bool ordinary=n.StartsWith("ordinary-");bool authored=n.StartsWith("yarn-")||n=="cat"||n=="ingot"||n=="states"||n=="burst";if(!ordinary&&!authored)return null;
  if(ordinary){if(jelly==null)jelly=Create(.4f);return jelly;}if(yarn==null)yarn=Create(.2f);return yarn;
 }
 static Material Create(float cutoff){var shader=Resources.Load<Shader>("OpeningStory049UI");if(shader==null)throw new InvalidOperationException("Missing introduction UI alpha shader");var mat=new Material(shader){name="Introduction authored alpha "+cutoff,hideFlags=HideFlags.DontSave};mat.SetFloat("_Cutoff",cutoff);return mat;}
}
}
