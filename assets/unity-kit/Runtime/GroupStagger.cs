using UnityEngine;
namespace ProjectTweenKit {
// AppUI.ShopEnter: scale .98 -> original, .20s OutCubic, .018s per item.
public sealed class GroupStagger:MonoBehaviour {
 public RectTransform[] Targets;public float Duration=.20f,Interval=.018f,StartScale=.98f;
 Vector3[] rest;float age;bool running;
 public void Play(){if(rest==null){rest=new Vector3[Targets.Length];for(int i=0;i<Targets.Length;i++)rest[i]=Targets[i].localScale;}age=0;running=true;Sample(0);}
 public void Sample(float seconds){for(int i=0;i<Targets.Length;i++){float t=Mathf.Clamp01((seconds-i*Interval)/Mathf.Max(.001f,Duration));Targets[i].localScale=rest[i]*Mathf.LerpUnclamped(StartScale,1,1-Mathf.Pow(1-t,3));}}
 void Update(){if(!running)return;age+=KitClock.Delta;Sample(age);if(age>=Duration+Mathf.Max(0,Targets.Length-1)*Interval)running=false;}
 public void StopAndRestore(){running=false;if(rest==null)return;for(int i=0;i<Targets.Length;i++)if(Targets[i]!=null)Targets[i].localScale=rest[i];}
 void OnDisable(){StopAndRestore();}
}
}
