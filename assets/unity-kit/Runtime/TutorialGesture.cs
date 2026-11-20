using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
// PlayableTutorialGuide motion slice. This component never sets aim or advances gameplay.
public sealed class TutorialGesture:MonoBehaviour {
 public Image Hand,Ring;public RectTransform Outline;public CanvasGroup Group;public Vector2 From=new Vector2(-200,-110),To=new Vector2(180,120);
 public float Period=4.4f,StartDelay=.6f,MoveDuration=1.3f;
 float clock;bool suppressed;
 public void SetInputActive(bool active){suppressed=active;if(Group!=null)Group.alpha=active?0:1;}
 void Update(){if(Hand==null)return;clock+=KitClock.ScaledDelta;float t=Mathf.Repeat(clock,Mathf.Max(.01f,Period)),u=Mathf.SmoothStep(0,1,Mathf.Clamp01((t-StartDelay)/Mathf.Max(.001f,MoveDuration)));Vector2 tip=Vector2.Lerp(From,To,u);Hand.sprite=OnboardingArt.Get(2,t<.6f?1:t>2.2f?3:2);Ring.sprite=OnboardingArt.Get(2,5);Hand.rectTransform.anchoredPosition=tip+new Vector2(68,-62);Ring.rectTransform.anchoredPosition=tip;Ring.rectTransform.localScale=Vector3.one*(.85f+.12f*Mathf.Sin(t*8));Outline.anchoredPosition=To;Outline.localScale=Vector3.one*(1+.05f*Mathf.Sin(clock*4));Hand.color=new Color(1,1,1,t>3?Mathf.Clamp01(4-t):1);Ring.color=Hand.color;Group.alpha=suppressed?0:1;}
 public void StopAndRestore(){clock=0;suppressed=true;if(Group!=null)Group.alpha=0;}
 void OnDisable(){StopAndRestore();}
}
}
