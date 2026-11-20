using TMPro;
using UnityEngine;
namespace ProjectTweenKit {
// BattleHUDSummary.Update: arrival-triggered gold/scale pulse, independent of counting.
public sealed class ScoreImpact:MonoBehaviour {
 public TMP_Text Target;public float Duration=.25f,Amplitude=.2f;
 public Color Highlight=new Color(.85f,.45f,.06f);
 Vector3 rest;Color ink;float age;bool ready,running;
 void Capture(){if(ready||Target==null)return;rest=Target.transform.localScale;ink=Target.color;ready=true;}
 public void Play(bool immediate=false){Capture();age=0;running=!immediate;Sample(immediate?Duration:0);}
 public void Sample(float seconds){Capture();if(!ready)return;float p=Mathf.Sin(Mathf.Clamp01(seconds/Mathf.Max(.001f,Duration))*Mathf.PI);if(seconds>=Duration)p=0;Target.transform.localScale=rest*(1+p*Amplitude);Target.color=Color.Lerp(ink,Highlight,p);}
 void Update(){if(!running)return;age+=KitClock.Delta;Sample(age);if(age>=Duration)running=false;}
 public void Seek(float seconds){age=Mathf.Max(0,seconds);running=age<Duration;Sample(age);}
 public void StopAndRestore(){running=false;if(ready&&Target!=null){Target.transform.localScale=rest;Target.color=ink;}}
 void OnDisable(){StopAndRestore();}
}
}
