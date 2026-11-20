using UnityEngine;
namespace ProjectTweenKit {
// ChapterHomeView.Transition: smooth fade of the old background above the new one.
public sealed class ThemeDissolve:MonoBehaviour {
 public CanvasGroup Previous;public float Duration=.8f;float age;bool running;
 public void Play(bool immediate=false){age=0;running=!immediate;Sample(immediate?Duration:0);}
 public void Sample(float seconds){float t=Mathf.Clamp01(seconds/Mathf.Max(.001f,Duration));Previous.alpha=1-t*t*(3-2*t);}
 void Update(){if(!running)return;age+=KitClock.Delta;Sample(age);if(age>=Duration)running=false;}
 public void Seek(float seconds){age=Mathf.Max(0,seconds);running=age<Duration;Sample(age);}
 public void StopAndRestore(){running=false;if(Previous!=null)Previous.alpha=1;}
 void OnDisable(){StopAndRestore();}
}
}
