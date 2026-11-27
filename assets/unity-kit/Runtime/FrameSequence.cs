using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
// Original frame order and nonuniform frame durations, no resampling.
public sealed class FrameSequence:MonoBehaviour {
 public Image Target;public bool Victory=true,Loop;public float PlaybackSpeed=1;
 float age;bool running;public int Frame{get;private set;}
 public void Play(bool immediate=false){age=0;running=!immediate;Sample(immediate?Duration:0);}
 public float Duration=>Victory?VictoryCatAnimation.Duration:FailureCatAnimation.Duration;
 public void Sample(float seconds){if(Loop)seconds=Mathf.Repeat(seconds,Duration);Frame=Victory?VictoryCatAnimation.FrameAt(seconds):FailureCatAnimation.FrameAt(seconds);if(Target!=null)Target.sprite=Victory?VictoryCatAnimation.Get(Frame):FailureCatAnimation.Get(Frame);}
 void Update(){if(!running)return;age+=KitClock.Delta*PlaybackSpeed;Sample(age);if(!Loop&&age>=Duration)running=false;}
 public void Seek(float seconds){age=Mathf.Max(0,seconds);running=Loop||age<Duration;Sample(age);}
 public void StopAndRestore(){running=false;Sample(0);}
 void OnDisable(){StopAndRestore();}
}
}
