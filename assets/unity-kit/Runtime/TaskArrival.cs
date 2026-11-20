using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
// ChainTaskCard.Update. Host supplies completion, animation never increments a task.
public sealed class TaskArrival:MonoBehaviour {
 public RectTransform Root,Icon,Pad,Stamp;public TMP_Text Count;
 public float IntroDuration=.48f,PulseDuration=.38f,StampDuration=.4f;
 public float RootAmplitude=.025f,IconAmplitude=.1f,PadAmplitude=.11f;
 Vector3 rootScale,iconScale,padScale,stampScale,countScale;float age,pulseAge=2;bool ready,complete;
 void Capture(){if(ready||Root==null)return;rootScale=Root.localScale;iconScale=Icon.localScale;padScale=Pad.localScale;stampScale=Stamp.localScale;countScale=Count.transform.localScale;ready=true;}
 public void Arrive(bool completed){Capture();complete=completed;pulseAge=0;}
 void Update(){Capture();if(!ready)return;age+=KitClock.ScaledDelta;pulseAge+=KitClock.ScaledDelta;Render();}
 void Render(){float intro=Mathf.Min(1.025f,Mathf.LerpUnclamped(.9f,1,UICraft.OutBack(Mathf.Clamp01(age/Mathf.Max(.001f,IntroDuration))))),pulse=Mathf.Sin(Mathf.Clamp01(pulseAge/Mathf.Max(.001f,PulseDuration))*Mathf.PI);Root.localScale=Vector3.Scale(rootScale,new Vector3(intro*(1+pulse*RootAmplitude),intro*(1-pulse*RootAmplitude),1));Icon.localScale=Vector3.Scale(iconScale,new Vector3(1+pulse*IconAmplitude,1-pulse*IconAmplitude*.8f,1));Count.transform.localScale=countScale*(1+pulse*.04f);Count.enabled=!complete;Stamp.gameObject.SetActive(complete);if(complete)Stamp.localScale=stampScale*Mathf.LerpUnclamped(.4f,1,UICraft.OutBack(Mathf.Clamp01(pulseAge/Mathf.Max(.001f,StampDuration))));float wobble=Mathf.Sin(pulseAge*27)*Mathf.Exp(-pulseAge*8)*PadAmplitude;Pad.localScale=Vector3.Scale(padScale,new Vector3(1+wobble,1-wobble,1));}
 public void StopAndRestore(){if(!ready)return;Root.localScale=rootScale;Icon.localScale=iconScale;Pad.localScale=padScale;Stamp.localScale=stampScale;Count.transform.localScale=countScale;Stamp.gameObject.SetActive(complete);Count.enabled=!complete;age=pulseAge=100;}
 void OnDisable(){StopAndRestore();}
}
}
