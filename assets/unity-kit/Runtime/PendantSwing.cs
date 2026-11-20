using UnityEngine;
namespace ProjectTweenKit {
// BattleScoreMeter.Advance: a visual spring, no rigidbody or collision ownership.
public sealed class PendantSwing:MonoBehaviour {
 public RectTransform Target;public float Spring=30,Damping=3.6f,MaxAngle=36,ImpulseStrength=120;
 Quaternion rest;float angle,velocity;bool ready;
 public float Angle=>angle;
 void Capture(){if(ready||Target==null)return;rest=Target.localRotation;ready=true;}
 public void Impulse(float direction){Capture();velocity=Mathf.Clamp(velocity+Mathf.Clamp(direction,-1,1)*ImpulseStrength,-240,240);}
 void Update(){Capture();if(!ready)return;float dt=Mathf.Clamp(KitClock.ScaledDelta,0,.05f);for(int n=0;n<4;n++){float step=dt*.25f;velocity+=(-angle*Spring-velocity*Damping)*step;float next=angle+velocity*step;angle=Mathf.Clamp(next,-MaxAngle,MaxAngle);if(next!=angle&&Mathf.Sign(velocity)==Mathf.Sign(angle))velocity*=-.25f;}Target.localRotation=rest*Quaternion.Euler(0,0,angle);}
 public void StopAndRestore(){angle=velocity=0;if(ready&&Target!=null)Target.localRotation=rest;}
 void OnDisable(){StopAndRestore();}
}
}
