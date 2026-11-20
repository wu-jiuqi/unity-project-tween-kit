using UnityEngine;
namespace ProjectTweenKit {
// ChainCollectionHUD.Update: introductory OutBack and a tiny opposing-axis kick.
public sealed class MovesFeedback:MonoBehaviour {
 public RectTransform Target;public float IntroDuration=.5f,PulseDuration=.3f,Amplitude=.025f;
 Vector3 rest;float age,pulseAge=2;bool ready;
 void Capture(){if(ready||Target==null)return;rest=Target.localScale;ready=true;}
 public void Play(){Capture();pulseAge=0;}
 void Update(){Capture();if(!ready)return;age+=KitClock.ScaledDelta;pulseAge+=KitClock.ScaledDelta;float intro=Mathf.Min(1.025f,Mathf.LerpUnclamped(.9f,1,UICraft.OutBack(Mathf.Clamp01(age/Mathf.Max(.001f,IntroDuration))))),kick=Mathf.Sin(Mathf.Clamp01(pulseAge/Mathf.Max(.001f,PulseDuration))*Mathf.PI)*Amplitude;Target.localScale=Vector3.Scale(rest,new Vector3(intro*(1+kick),intro*(1-kick),1));}
 public void StopAndRestore(){age=pulseAge=100;if(ready&&Target!=null)Target.localScale=rest;}
 void OnDisable(){StopAndRestore();}
}
}
