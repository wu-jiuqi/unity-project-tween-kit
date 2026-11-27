using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace ProjectTweenKit {
// Layout and status classification belong to the host. This owns one visual slot.
public sealed class ToastMotion:MonoBehaviour {
 public RectTransform Visual;public CanvasGroup Group;public TMP_Text Label;
 public float EnterDuration=.18f,ExitDuration=.14f,HoldDuration=3.2f,EnterScale=.96f,ExitScale=.94f;
 public string Message{get;private set;}public string Phase{get;private set;}="idle";
 public string Pending=>pending;
 string pending;float age,hold;Vector3 rest,start;float startAlpha;bool captured;
 void Capture(){if(captured)return;rest=Visual.localScale;captured=true;Group.interactable=Group.blocksRaycasts=false;}
 public void Show(string message){if(string.IsNullOrWhiteSpace(message))return;Capture();
  if(Phase!="idle"){if(message==Message&&Phase!="exiting"){pending=null;hold=HoldDuration;return;}pending=message;if(Phase!="exiting")Exit();return;}
  Message=message;if(Label!=null)Label.text=message;Visual.localScale=rest*EnterScale;Group.alpha=0;age=0;Phase="entering";
 }
 void Exit(){age=0;start=Visual.localScale;startAlpha=Group.alpha;Phase="exiting";}
 public void Advance(float dt){if(!captured)return;dt=Mathf.Max(0,dt);age+=dt;
  if(Phase=="entering"){float p=Mathf.Clamp01(age/Mathf.Max(.001f,EnterDuration));float e=1-Mathf.Pow(1-p,3);Visual.localScale=Vector3.LerpUnclamped(rest*EnterScale,rest,e);Group.alpha=e;if(p>=1){Phase="holding";hold=HoldDuration;}}
  else if(Phase=="holding"){hold-=dt;if(hold<=0)Exit();}
  else if(Phase=="exiting"){float p=Mathf.Clamp01(age/Mathf.Max(.001f,ExitDuration)),e=p*p*p;Visual.localScale=Vector3.LerpUnclamped(start,start*ExitScale,e);Group.alpha=startAlpha*(1-e);if(p>=1){string next=pending;StopAndRestore();if(next!=null)Show(next);}}
 }
 void Update(){Advance(KitClock.Delta);}
 public void StopAndRestore(){pending=null;Phase="idle";age=hold=0;if(!captured)return;Visual.localScale=rest;Group.alpha=0;Group.interactable=Group.blocksRaycasts=false;}
 void OnDisable(){StopAndRestore();}void OnApplicationFocus(bool focus){if(!focus)StopAndRestore();}void OnApplicationPause(bool pause){if(pause)StopAndRestore();}
}
}
