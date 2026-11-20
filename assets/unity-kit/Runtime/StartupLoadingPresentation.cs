using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace ProjectTweenKit {
// Project WeChat startup composition, independent of network/SDK startup.
public sealed class StartupLoadingPresentation:MonoBehaviour {
 static readonly string[] Tips={"猫咪正在伸懒腰…","给果冻方块充点弹力…","把小纸箱摆摆整齐…","擦亮今天的幸运弹珠…","让薄荷浮岛轻轻飘起来…","藏好一份拆箱小惊喜…","给快乐加一点软乎乎…","猫爪准备，瞄准快乐…","下一次弹跳，会有惊喜…","带上好心情，一起出发…"};
 readonly System.Collections.Generic.List<RectTransform> letters=new System.Collections.Generic.List<RectTransform>(),jellies=new System.Collections.Generic.List<RectTransform>();
 RectTransform island,cat,box,ball,background,content;Image catImage;TMP_Text tip;float age;string failure;bool built;
 public LoadingProgress Progress{get;private set;}
 static Sprite Art(string key)=>StartupArt.Get(key);
 static RectTransform Picture(Transform p,string key,float x,float y,float w)=>StartupArt.Picture(p,key,x,y,w);
 static float Y(float top)=>800-top;
 public void Build(){if(built)return;built=true;
  UICraft.Box(transform,"Startup mint backdrop",0,0,6000,6000,new Color32(229,245,223,255),false,true);
  background=Picture(transform,"background",0,0,900);
  content=UICraft.Root(transform,"Safe startup content",0,0,900,1600);
  for(int i=0;i<4;i++)letters.Add(Picture(content,new[]{"logo-miao","logo-tan","logo-chai","logo-xiang"}[i],-283+i*182,Y(440)+Art(new[]{"logo-miao","logo-tan","logo-chai","logo-xiang"}[i]).rect.height/Art(new[]{"logo-miao","logo-tan","logo-chai","logo-xiang"}[i]).rect.width*91,182));
  island=UICraft.Root(content,"Floating startup scene",0,0,900,1600);
  Bottom(island,"island",0,1168,840);
  jellies.Add(Bottom(island,"jelly-blush",-271,1015,120));jellies.Add(Bottom(island,"jelly-blue",-165,1018,121));jellies.Add(Bottom(island,"jelly-yellow",-225,915,114));
  cat=Bottom(island,"cat-idle",6,1000,490);catImage=cat.GetComponent<Image>();box=Bottom(island,"box",260,1035,262);
  ball=UICraft.Box(island,"Golden startup ball",-145,Y(1060),64,64,new Color32(248,207,52,255),true).rectTransform;UICraft.Box(ball,"Ball highlight",-10,12,17,17,new Color32(255,249,205,255),true);
  Flower(island,-276,Y(1055),68,new Color32(255,251,234,255));Flower(island,295,Y(1075),54,new Color32(255,251,234,255));
  Progress=LoadingProgress.Create(content,0,Y(1270));Progress.Number.transform.SetParent(content,false);Progress.Number.rectTransform.anchoredPosition=new Vector2(0,Y(1438));
  tip=UICraft.Label(content,"",0,Y(1378),840,52,29,UICraft.Ink);Render();
 }
 RectTransform Bottom(Transform p,string key,float x,float bottom,float w){var r=Picture(p,key,x,Y(bottom),w);r.pivot=new Vector2(.5f,0);return r;}
 static RectTransform Flower(Transform p,float x,float y,float s,Color c){var rt=UICraft.Root(p,"Flower",x,y,s,s);UICraft.Box(rt,"Stem",0,-s*.38f,s*.1f,s*.5f,new Color32(50,129,65,255));for(int a=0;a<5;a++){float f=a*Mathf.PI*2/5;UICraft.Box(rt,"Petal",Mathf.Cos(f)*s*.24f,Mathf.Sin(f)*s*.24f,s*.38f,s*.38f,c,true);}UICraft.Box(rt,"Pollen",0,0,s*.32f,s*.32f,UICraft.Gold,true);return rt;}
 public void Failure(string message){failure=message;Progress.Failure();}
 public void Resume(){failure=null;Progress.Resume();}
 void LateUpdate(){if(!built)return;var root=(RectTransform)transform;var art=Art("background");float cover=Mathf.Max(root.rect.width/art.rect.width,root.rect.height/art.rect.height);background.sizeDelta=art.rect.size*cover;content.localScale=Vector3.one*Mathf.Min(root.rect.width/900,root.rect.height/1600);}
 static float Hop(float t,float delay,float period){float p=Mathf.Repeat(t-delay,period);return p<.65f?Mathf.Sin(p/.65f*Mathf.PI):0;}
 void Update(){if(!built)return;if(!Progress.Failed&&!Progress.IsComplete)age+=Mathf.Min(.1f,KitClock.Delta);Render();}
 void Render(){tip.text=Progress.Failed?(failure??"资源准备中断，可重试"):Progress.IsComplete?"准备好了，喵！":Tips[(int)(age/2.8f)%Tips.Length];
  island.anchoredPosition=Vector2.up*Mathf.Sin(age*Mathf.PI*2/4.8f)*9;
  for(int i=0;i<letters.Count;i++){var p=letters[i].anchoredPosition;p.y=Y(440)+letters[i].sizeDelta.y*.5f+Hop(age,i*.17f,5.8f)*14;letters[i].anchoredPosition=p;}
  float phase=age%9,bounce=Hop(age,1.8f,9);string pose=phase<1.8f?"idle":phase<2.3f?"anticipate":phase<2.9f?"celebrate":phase<4?"settle":phase<5.8f?"idle":phase<7.5f?"wave":"idle";
  catImage.sprite=Art("cat-"+pose);cat.anchoredPosition=new Vector2(6,Y(1000)+bounce*27);float breath=Mathf.Sin(age*Mathf.PI*2/3.1f);cat.localScale=new Vector3(1-breath*.008f-bounce*.025f,1+breath*.012f+bounce*.045f,1);cat.localRotation=Quaternion.Euler(0,0,phase>5.8f&&phase<7.5f?Mathf.Sin(age*Mathf.PI*2)*1.4f:0);
  ball.anchoredPosition=new Vector2(-145,Y(1060)+Hop(age,.8f,3.2f)*32);
  box.anchoredPosition=new Vector2(260,Y(1035)+Hop(age,3.5f,6)*9);box.localRotation=Quaternion.Euler(0,0,Mathf.Sin(age*Mathf.PI*2/5.1f)*1.1f);
  for(int i=0;i<jellies.Count;i++){var p=jellies[i].anchoredPosition;p.y=Y(new[]{1015,1018,915}[i])+Hop(age,2.6f+i*.15f,4.8f)*13;jellies[i].anchoredPosition=p;}
 }
 public void StopAndRestore(){age=0;if(Progress!=null){Progress.ResetProgress();Render();}}
 void OnDisable(){StopAndRestore();}
}
}
