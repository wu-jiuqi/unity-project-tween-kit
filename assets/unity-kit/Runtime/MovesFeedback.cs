using UnityEngine;
namespace ProjectTweenKit {
// ChainCollectionHUD.Update: introductory OutBack and a tiny opposing-axis kick.
public sealed class MovesFeedback:MonoBehaviour {
 public RectTransform Target;public float IntroDuration=.5f,PulseDuration=.3f,Amplitude=.025f;
 public float WarningCycle=.48f,WarningAmplitude=.10f;public int WarningLoops=3;public Color WarningColor=new Color32(202,53,45,255);public TMPro.TMP_Text Label;
 float warningAge=100;Color ink;public int CompletedWarningLoops=>Mathf.Min(WarningLoops,Mathf.FloorToInt(warningAge/Mathf.Max(.001f,WarningCycle)));
 public void Warn(){Capture();warningAge=0;if(Label!=null)Label.color=WarningColor;}
 Vector3 rest;float age,pulseAge=2;bool ready;
 void Capture(){if(ready||Target==null)return;rest=Target.localScale;if(Label!=null)ink=Label.color;ready=true;}
 public void Play(){Capture();pulseAge=0;}
 void Update(){Capture();if(!ready)return;age+=KitClock.ScaledDelta;pulseAge+=KitClock.ScaledDelta;float intro=Mathf.Min(1.025f,Mathf.LerpUnclamped(.9f,1,UICraft.OutBack(Mathf.Clamp01(age/Mathf.Max(.001f,IntroDuration))))),kick=Mathf.Sin(Mathf.Clamp01(pulseAge/Mathf.Max(.001f,PulseDuration))*Mathf.PI)*Amplitude;warningAge+=KitClock.ScaledDelta;float warning=warningAge<WarningCycle*WarningLoops?(1-Mathf.Cos(warningAge/Mathf.Max(.001f,WarningCycle)*Mathf.PI*2))*WarningAmplitude:0;Target.localScale=Vector3.Scale(rest,new Vector3(intro*(1+kick)*(1+warning),intro*(1-kick)*(1+warning),1));}
 public void StopAndRestore(){age=pulseAge=warningAge=100;if(Label!=null&&ready)Label.color=ink;if(ready&&Target!=null)Target.localScale=rest;}
 void OnDisable(){StopAndRestore();}
}
}
