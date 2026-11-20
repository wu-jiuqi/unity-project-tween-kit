using UnityEngine;
namespace ProjectTweenKit {
// AppUI.ShowPage normalized slide; the host still owns routing and layout.
public sealed class PageSlide:MonoBehaviour {
 public RectTransform Target;public Vector2 Destination;public float Duration=.27f;
 Vector2 start,rest;float age;bool ready,running;
 public void Go(Vector2 destination,bool immediate=false){if(!ready){rest=Target.anchoredPosition;ready=true;}start=Target.anchoredPosition;Destination=destination;age=0;running=!immediate;Sample(immediate?Duration:0);}
 public void Sample(float seconds){float t=Mathf.Clamp01(seconds/Mathf.Max(.001f,Duration));Target.anchoredPosition=Vector2.LerpUnclamped(start,Destination,1-Mathf.Pow(1-t,3));}
 void Update(){if(!running)return;age+=KitClock.Delta;Sample(age);if(age>=Duration)running=false;}
 public void StopAndRestore(){running=false;if(ready&&Target!=null)Target.anchoredPosition=rest;}
 void OnDisable(){StopAndRestore();}
}
}
