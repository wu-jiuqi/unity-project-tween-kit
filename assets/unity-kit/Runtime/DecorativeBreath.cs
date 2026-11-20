using UnityEngine;
namespace ProjectTweenKit {
// LeagueMatchPresentation emblem: 1 + .035*sin(age*3).
public sealed class DecorativeBreath:MonoBehaviour {
 public Transform Target;public float Amplitude=.035f,AngularSpeed=3;
 Vector3 rest;float age;bool ready;
 void Update(){if(Target==null)return;if(!ready){rest=Target.localScale;ready=true;}age+=Mathf.Min(KitClock.Delta,.05f);Target.localScale=rest*(1+Amplitude*Mathf.Sin(age*AngularSpeed));}
 public void StopAndRestore(){age=0;if(ready&&Target!=null)Target.localScale=rest;}
 void OnDisable(){StopAndRestore();}
}
}
