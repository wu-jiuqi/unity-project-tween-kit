using UnityEngine;
namespace ProjectTweenKit {
// Current CollectionFlightMotion.Apply equations; no collection counters or reward writes.
public sealed class RewardFlight:MonoBehaviour {
 public enum LootKind {cookie,yarn,copper_coin,ingot,ordinary_color}
 public RectTransform Target,Destination;
 public LootKind Kind=LootKind.cookie;
 public float Duration=.6f,Delay,UnitsPerWorldUnit=1,EndScale=.28f;
 Vector2 start,end;Vector3 scale;Quaternion rotation;float age;bool running,captured;
 public bool Running=>running;public float Elapsed=>age;
 public void Play(bool immediate=false){StopAndRestore();if(Target==null||Destination==null)return;start=Target.anchoredPosition;scale=Target.localScale;rotation=Target.localRotation;captured=true;end=((RectTransform)Target.parent).InverseTransformPoint(Destination.position);age=-Delay;running=!immediate;Target.gameObject.SetActive(true);Sample(immediate?Duration:0);}
 public void Sample(float seconds){if(!captured)return;float t=Mathf.Clamp01(seconds/Mathf.Max(.001f,Duration)),u=t*t*(3-2*t);Vector2 delta=end-start;
  float side=Kind==LootKind.cookie?-.18f:Kind==LootKind.yarn?.18f:0;Vector2 control=start+delta*.22f+Vector2.up*(Kind==LootKind.ingot?.65f:.4f)*UnitsPerWorldUnit+Vector2.right*side*UnitsPerWorldUnit;
  Target.anchoredPosition=(1-u)*(1-u)*start+2*(1-u)*u*control+u*u*end;
  float enter=1+Mathf.Sin(Mathf.Min(1,t/.2f)*Mathf.PI)*.10f,shrink=Mathf.Lerp(1,EndScale,Mathf.Pow(t,3));
  float ripple=Mathf.Sin(t*Mathf.PI*2)*.035f*(1-t);Target.localScale=Vector3.Scale(scale,new Vector3(1+ripple,1-ripple,1)*(enter*shrink));
  float angle=Kind==LootKind.yarn?12:Kind==LootKind.cookie?7:Kind==LootKind.copper_coin?4:2;Target.localRotation=rotation*Quaternion.Euler(0,0,Mathf.Sin(t*Mathf.PI*2)*angle*(1-t));
 }
 void Update(){if(!running)return;age+=KitClock.Delta;if(age<0)return;Sample(age);if(age>=Duration)running=false;}
 public void Seek(float seconds){age=Mathf.Max(0,seconds);running=age<Duration;Sample(age);}
 public void StopAndRestore(){running=false;if(captured&&Target!=null){Target.anchoredPosition=start;Target.localScale=scale;Target.localRotation=rotation;}captured=false;}
 void OnDisable(){StopAndRestore();}
}
}
