using UnityEngine;
using DG.Tweening;
namespace ProjectTweenKit {
// Optional presentation clock. Default operation uses normal Unity time.
public static class KitClock {
 public static bool Manual, Paused, Sampling;
 public static float Speed=1;
 static float now,delta;
 public static float Now=>Manual?now:Time.unscaledTime;
 public static float Delta=>Paused?0:Manual?delta:Time.unscaledDeltaTime*Speed;
 public static float ScaledDelta=>Paused?0:Manual?delta:Time.deltaTime*Speed;
 public static UpdateType TweenUpdate=>Manual?UpdateType.Manual:UpdateType.Normal;
 public static void Reset(){now=delta=0;}
 public static void Frame(float dt){delta=Paused?0:Mathf.Max(0,dt)*Speed;now+=delta;DOTween.ManualUpdate(delta,delta);}
}
public sealed class KitWait:CustomYieldInstruction {
 readonly float end;
 public KitWait(float duration){end=KitClock.Now+duration;}
 public override bool keepWaiting=>KitClock.Now<end;
}
// Explicit presentation preferences. No PlayerPrefs or game profile dependency.
public sealed class FeedbackProfile {
 public static readonly FeedbackProfile Active=new FeedbackProfile();
 public bool enabledFeedback=true,screenShake=true;
}
internal static class RuntimeQA {public static bool IsIsolated=>KitClock.Manual;}
}
