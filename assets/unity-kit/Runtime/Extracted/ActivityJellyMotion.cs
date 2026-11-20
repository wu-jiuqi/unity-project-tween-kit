using System;
using UnityEngine;
namespace ProjectTweenKit {
// The card's fixed hit region never follows the visual deformation.
public sealed class ActivityJellyMotion:MonoBehaviour {
 RectTransform visual,icon;Vector2 iconRest;Func<bool> allowed;
 bool hovered,held;float spring,velocity,clock,age;int index;
 public void Initialize(RectTransform face,RectTransform reward,int phase,Func<bool> canAnimate){visual=face;icon=reward;iconRest=icon.anchoredPosition;index=phase;allowed=canAnimate;age=-.001f-phase*.12f;}
 public void SetHover(bool value){hovered=value;if(value)Celebrate(.8f);}
 public void Press(){held=true;velocity=-8;}
 public void Release(){if(!held)return;held=false;Celebrate(1.25f);}
 public void Celebrate(float strength=1){velocity=Mathf.Clamp(velocity+strength*9,-14,14);}
 void Update(){if(visual==null)return;if(!FeedbackProfile.Active.enabledFeedback||allowed==null||!allowed()){ResetPose();return;}
  float dt=Mathf.Min(KitClock.Delta,.04f);clock+=dt;float previous=age;age+=dt;if(previous<0&&age>=0)Celebrate();
  // Damped spring, substepped for consistent motion at low frame rates.
  float target=held?-1f:hovered?.45f:0;
  for(int n=0;n<4;n++){float step=dt*.25f;velocity+=((target-spring)*155-velocity*10)*step;spring+=velocity*step;}
  spring=Mathf.Clamp(spring,-1.4f,1.4f);
  float phase=clock*2.4f+index*1.8f;
  float idle=Mathf.Sin(phase)*.018f;
  visual.localScale=new Vector3(1+spring*.018f,1-spring*.025f,1);
  float grow=hovered?.16f:0;
  icon.localScale=new Vector3(1+grow-spring*.15f+idle,Mathf.Clamp(1+grow+spring*.21f-idle,.72f,1.21f),1);
  icon.anchoredPosition=new Vector2(iconRest.x,Mathf.Clamp(iconRest.y+Mathf.Sin(phase)*7+(hovered?18:0)+spring*19,-176+icon.rect.height*icon.localScale.y*.5f,126-icon.rect.height*icon.localScale.y*.5f));
  icon.localRotation=Quaternion.Euler(0,0,Mathf.Sin(phase*.8f)*(hovered?7:3)+velocity*1.1f);
 }
 public void ResetPose(){spring=velocity=0;hovered=held=false;if(visual!=null)visual.localScale=Vector3.one;if(icon!=null){icon.localScale=Vector3.one;icon.anchoredPosition=iconRest;icon.localRotation=Quaternion.identity;}}
 void OnDisable(){ResetPose();}
}
}
