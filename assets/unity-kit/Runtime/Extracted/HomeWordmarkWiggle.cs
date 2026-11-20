using System.Collections.Generic;
using UnityEngine;
namespace ProjectTweenKit {
// A short shiver travels left to right across both rows, followed by a quiet hold.
public sealed class HomeWordmarkWiggle : MonoBehaviour {
 public float LoopInterval=5.5f;
 readonly List<RectTransform> letters=new List<RectTransform>();
 Vector2[] positions;Vector3[] scales;Quaternion[] rotations;public System.Func<bool> CanAnimate;float elapsed;bool focused=true;
 void Start(){foreach(Transform child in transform)letters.Add((RectTransform)child);
  letters.Sort((a,b)=>a.anchoredPosition.x.CompareTo(b.anchoredPosition.x));
  positions=new Vector2[letters.Count];scales=new Vector3[letters.Count];rotations=new Quaternion[letters.Count];
  for(int i=0;i<letters.Count;i++){positions[i]=letters[i].anchoredPosition;scales[i]=letters[i].localScale;rotations[i]=letters[i].localRotation;}
 }
 void Update(){if(positions==null)return;if(!focused||!FeedbackProfile.Active.enabledFeedback||(CanAnimate!=null&&!CanAnimate())){Restore();elapsed=0;return;}
  elapsed+=KitClock.Delta;float cycle=elapsed%LoopInterval;
  for(int i=0;i<letters.Count;i++){float t=(cycle-.7f-i*.09f)/.48f;float wave=t>0&&t<1?Mathf.Sin(t*Mathf.PI*8)*Mathf.Sin(t*Mathf.PI):0;
   letters[i].anchoredPosition=positions[i]+new Vector2(wave*1.2f,wave*3.2f);
   letters[i].localRotation=rotations[i]*Quaternion.Euler(0,0,wave*2.2f);
   letters[i].localScale=Vector3.Scale(scales[i],new Vector3(1+wave*.018f,1-wave*.025f,1));
  }
 }
 void Restore(){if(positions==null)return;for(int i=0;i<letters.Count;i++){letters[i].anchoredPosition=positions[i];letters[i].localScale=scales[i];letters[i].localRotation=rotations[i];}}
 void OnDisable(){Restore();elapsed=0;}
 void OnApplicationFocus(bool value){focused=value;if(!value){Restore();elapsed=0;}}
}
}
