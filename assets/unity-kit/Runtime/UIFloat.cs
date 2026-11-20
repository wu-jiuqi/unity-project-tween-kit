using UnityEngine;
namespace ProjectTweenKit {
// UICraft.UIFloat sine equation, with explicit interrupt/reset semantics added.
public sealed class UIFloat:MonoBehaviour {
 public float amplitude=12,speed=1.4f,phase;
 Vector3 initial;bool captured;
 void OnEnable(){if(!captured){initial=transform.localPosition;captured=true;}}
 void Update(){transform.localPosition=initial+Vector3.up*Mathf.Sin(KitClock.Now*speed+phase)*amplitude;}
 public void StopAndRestore(){enabled=false;}
 void OnDisable(){if(captured)transform.localPosition=initial;}
}
}
