using System;
using UnityEngine;
namespace ProjectTweenKit {
// Host owns async requests. A ticket rejects stale completions before touching visuals.
public sealed class PageTransition:MonoBehaviour {
 public RectTransform[] Pages;public float Width=920,Duration=.27f;
 public int Current{get;private set;}public int Pending{get;private set;}=-1;
 public bool Running{get;private set;}
 int ticket,from;float age,direction;Vector2[] rest;CanvasGroup[] groups;
 void Capture(){if(rest!=null)return;rest=new Vector2[Pages.Length];groups=new CanvasGroup[Pages.Length];for(int i=0;i<Pages.Length;i++){rest[i]=Pages[i].anchoredPosition;var group=Pages[i].GetComponent<CanvasGroup>();if(group==null)group=Pages[i].gameObject.AddComponent<CanvasGroup>();groups[i]=group;}Settle();}
 void Validate(int page){if(Pages==null||page<0||page>=Pages.Length)throw new ArgumentOutOfRangeException(nameof(page));}
 public int Request(int destination){Validate(destination);Capture();Cancel();if(destination!=Current)Pending=destination;return ticket;}
 public bool Resolve(int token,bool ready,bool immediate=false){if(token!=ticket||Pending<0)return false;int destination=Pending;Pending=-1;if(!ready)return false;from=Current;Current=destination;direction=Mathf.Sign(destination-from);age=0;Running=!immediate;Render(immediate?1:0);if(immediate)Settle();return true;}
 public void Cancel(){ticket++;Pending=-1;Running=false;if(rest!=null)Settle();}
 public void Go(int destination,bool immediate=false){int token=Request(destination);Resolve(token,true,immediate);}
 void Render(float p){float e=1-Mathf.Pow(1-Mathf.Clamp01(p),3);for(int i=0;i<Pages.Length;i++){bool show=i==Current||Running&&i==from;groups[i].alpha=show?1:0;groups[i].interactable=groups[i].blocksRaycasts=show&&!Running;Pages[i].anchoredPosition=i==Current?Vector2.right*direction*Width*(1-e):i==from?Vector2.left*direction*Width*e:Vector2.zero;}}
 void Settle(){for(int i=0;i<Pages.Length;i++){Pages[i].anchoredPosition=Vector2.zero;groups[i].alpha=i==Current?1:0;groups[i].interactable=groups[i].blocksRaycasts=i==Current;}}
 public void Advance(float dt){if(!Running)return;age+=Mathf.Max(0,dt);Render(age/Mathf.Max(.001f,Duration));if(age>=Duration){Running=false;Settle();}}
 void Update(){Advance(KitClock.Delta);}
 public void StopAndRestore(){Cancel();if(rest==null)return;for(int i=0;i<Pages.Length;i++)Pages[i].anchoredPosition=rest[i];}
 void OnDisable(){StopAndRestore();}void OnApplicationFocus(bool focus){if(!focus)Cancel();}
}
}
