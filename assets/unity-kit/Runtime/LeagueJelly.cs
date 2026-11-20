using UnityEngine;
namespace ProjectTweenKit {
public sealed class LeagueJelly:MonoBehaviour {
 Vector3 rest;Quaternion rotation;float time,delay;bool ready;public float duration=.65f;public int Pulses{get;private set;}
 public void Play(float wait=0){if(!ready){rest=transform.localScale;rotation=transform.localRotation;ready=true;}transform.localScale=rest;transform.localRotation=rotation;time=0;delay=wait;enabled=true;Pulses++;}
 void Update(){if(!ready)return;if(!FeedbackProfile.Active.enabledFeedback){Finish();return;}time+=KitClock.Delta;if(time<delay)return;float t=(time-delay)/Mathf.Max(.05f,duration);if(t>=1){Finish();return;}float wave=Mathf.Sin(t*Mathf.PI*5)*Mathf.Exp(-4*t)*(1-t);transform.localScale=Vector3.Scale(rest,new Vector3(1+wave*.09f,1-wave*.12f,1));transform.localRotation=rotation*Quaternion.Euler(0,0,wave*2.2f);}
 void Finish(){if(ready){transform.localScale=rest;transform.localRotation=rotation;}enabled=false;}
 void OnDisable(){if(ready){transform.localScale=rest;transform.localRotation=rotation;}}
}

}
