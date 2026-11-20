using UnityEngine;
using TMPro;
namespace ProjectTweenKit {
// VictoryFinale.Advance score segment: 0.20 seconds of cubic ease after a starlight arrival.
public sealed class NumberRoll:MonoBehaviour {
 public TMP_Text Target;public float Duration=.2f,Delay;public int From=100,To=180;
 float age;bool running;public int Displayed{get;private set;}
 public void Play(bool immediate=false){age=-Delay;running=!immediate;Sample(immediate?Duration:0);}
 public void Sample(float seconds){float t=Mathf.Clamp01(seconds/Mathf.Max(.001f,Duration));double shown=From;shown+=(To-From)*(1-Mathf.Pow(1-t,3));Displayed=(int)System.Math.Min(To,System.Math.Round(shown));if(Target!=null)Target.text=Displayed.ToString("N0");}
 void Update(){if(!running)return;age+=KitClock.Delta;if(age<0)return;Sample(age);if(age>=Duration)running=false;}
 public void Seek(float seconds){age=Mathf.Max(0,seconds);running=age<Duration;Sample(age);}
 public void StopAndRestore(){running=false;Sample(0);}
 void OnDisable(){StopAndRestore();}
}
}
