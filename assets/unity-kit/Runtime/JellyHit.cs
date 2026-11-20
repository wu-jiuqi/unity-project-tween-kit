using UnityEngine;
namespace ProjectTweenKit {
// Exact JellyMotion.HitStretch solver applied only to the bound visual object.
public sealed class JellyHit:MonoBehaviour {
 public Transform Target;public float Duration=.45f,Amplitude=1;public Vector2 Incoming=Vector2.down;
 Vector3 rest;float age;bool captured,running;
 public void Play(bool immediate=false){if(Target==null)return;if(!captured){rest=Target.localScale;captured=true;}age=immediate?Duration:0;running=!immediate;Sample(age);}
 public void Sample(float seconds){if(!captured)return;var v=JellyMotion.HitStretch(seconds/Mathf.Max(.001f,Duration)*JellyMotion.HitDuration,Incoming);Target.localScale=Vector3.Scale(rest,new Vector3(1+(v.x-1)*Amplitude,1+(v.y-1)*Amplitude,1));}
 void Update(){if(!running)return;age+=KitClock.ScaledDelta;Sample(age);if(age>=Duration)running=false;}
 public void Seek(float seconds){age=Mathf.Max(0,seconds);running=age<Duration;Sample(age);}
 public void StopAndRestore(){running=false;if(captured&&Target!=null)Target.localScale=rest;}
 void OnDisable(){StopAndRestore();}
}
}
