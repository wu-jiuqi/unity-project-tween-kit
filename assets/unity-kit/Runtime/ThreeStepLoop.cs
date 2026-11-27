using System;
using UnityEngine;
namespace ProjectTweenKit {
// Presentation-only sampler. The host binds each demonstration; it never submits progress.
public sealed class ThreeStepLoop:MonoBehaviour {
 public float LoopDuration=6.72f;
 public Vector3 RestPhases=new Vector3(.12f,.6f,.95f);
 public Action<int,float> RenderStep;
 public float Elapsed{get;private set;}public int ActiveStep{get;private set;}
 public void Sample(float seconds){Elapsed=Mathf.Max(0,seconds);float part=Mathf.Max(.003f,LoopDuration)/3;ActiveStep=Mathf.Min(2,(int)(Mathf.Repeat(Elapsed,part*3)/part));float phase=Mathf.Repeat(Elapsed,part)/part;for(int i=0;i<3;i++)RenderStep?.Invoke(i,i==ActiveStep?phase:RestPhases[i]);}
 void Update(){Sample(Elapsed+KitClock.Delta);}
 public void StopAndRestore(){Sample(0);}
 void OnDisable(){StopAndRestore();}void OnDestroy(){RenderStep=null;}
}
}
