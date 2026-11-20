using TMPro;
using UnityEngine;
namespace ProjectTweenKit {
// ScoreGainPresentation: debounce gains, then rise/fade; host owns the actual score.
public sealed class ScoreGain:MonoBehaviour {
 public TMP_Text Text;public RectTransform Target;public CanvasGroup Group;
 public float MergeDelay=.07f,Duration=.65f,FadeIn=.05f,Rise=32,Amplitude=.06f;
 int pending;float wait,age;Vector2 rest;Vector3 scale;bool ready,running;
 public int DisplayedGain{get;private set;}public int Pending=>pending;
 void Capture(){if(ready||Target==null)return;rest=Target.anchoredPosition;scale=Target.localScale;ready=true;}
 public void Add(int amount){if(amount<=0)return;Capture();pending=(int)System.Math.Min(int.MaxValue,(long)pending+amount);wait=MergeDelay;}
 void Update(){float dt=KitClock.ScaledDelta;if(dt<=0)return;if(pending>0){wait-=dt;if(wait<=0){DisplayedGain=pending;pending=0;if(Text!=null)Text.text="+"+DisplayedGain.ToString("N0");age=0;running=true;}}if(running){age+=dt;Sample(age);if(age>=Duration)running=false;}}
 public void Sample(float seconds){Capture();if(!ready)return;float t=Mathf.Clamp01(seconds/Mathf.Max(.001f,Duration));if(Group!=null)Group.alpha=(1-t)*Mathf.Clamp01(seconds/Mathf.Max(.001f,FadeIn));Target.anchoredPosition=rest+Vector2.up*(t*Rise);Target.localScale=scale*(1+Mathf.Sin(t*Mathf.PI)*Amplitude);}
 public void StopAndRestore(){pending=0;running=false;if(Group!=null)Group.alpha=0;if(ready&&Target!=null){Target.anchoredPosition=rest;Target.localScale=scale;}}
 void OnDisable(){StopAndRestore();}
}
}
