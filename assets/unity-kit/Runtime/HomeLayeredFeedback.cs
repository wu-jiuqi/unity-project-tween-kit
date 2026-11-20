using UnityEngine;
namespace ProjectTweenKit {
// HomeFeedbackView.Update/Pop, bound to the host's own visual layers.
public sealed class HomeLayeredFeedback:MonoBehaviour {
 public RectTransform Title,Detail,Decoration;public float Strength=1,IdleAmplitude=1,PopFrequency=17,Damping=4.7f,Duration=1.35f;
 Vector2 a,b,c;Vector3 sa,sb,sc;Quaternion rotation;float idle,entrance=-1;bool ready,running=true;
 void Capture(){if(ready||Title==null)return;a=Title.anchoredPosition;b=Detail.anchoredPosition;c=Decoration.anchoredPosition;sa=Title.localScale;sb=Detail.localScale;sc=Decoration.localScale;rotation=Decoration.localRotation;ready=true;}
 public void Play(){Capture();entrance=0;running=true;}
 void Update(){Capture();if(!ready||!running)return;float dt=Mathf.Min(KitClock.Delta,.05f);idle+=dt;if(entrance>=0){entrance+=dt;if(entrance>Duration)entrance=-1;}float breath=Mathf.Sin(idle*1.8f)*IdleAmplitude,wave=Pop(entrance,0)*Strength,follow=Pop(entrance,.13f)*Strength;
  Title.localScale=Vector3.Scale(sa,new Vector3(1+breath*.018f-wave*.065f,1+breath*.018f+wave*.12f,1));Title.anchoredPosition=a+Vector2.up*(breath*3+wave*13);
  Detail.localScale=sb*(1+Mathf.Sin(idle*1.8f-.6f)*.009f*IdleAmplitude+follow*.045f);Detail.anchoredPosition=b+Vector2.up*(Mathf.Sin(idle*1.8f-.6f)*2*IdleAmplitude+follow*9);
  Decoration.localScale=Vector3.Scale(sc,new Vector3(1-wave*.09f,1+wave*.17f,1));Decoration.anchoredPosition=c+Vector2.up*(Mathf.Sin(idle*1.8f+.4f)*4*IdleAmplitude+wave*9);Decoration.localRotation=rotation*Quaternion.Euler(0,0,Mathf.Sin(idle*1.5f)*3*IdleAmplitude+wave*5);
 }
 float Pop(float time,float delay){if(time<0)return 0;float t=time-delay;if(t<0)return -.75f;return -Mathf.Cos(t*PopFrequency)*Mathf.Exp(-t*Damping)*.85f;}
 public void StopAndRestore(){running=false;entrance=-1;if(!ready)return;Title.anchoredPosition=a;Detail.anchoredPosition=b;Decoration.anchoredPosition=c;Title.localScale=sa;Detail.localScale=sb;Decoration.localScale=sc;Decoration.localRotation=rotation;}
 void OnDisable(){StopAndRestore();}
}
}
