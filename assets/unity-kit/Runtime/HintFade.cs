using UnityEngine;
namespace ProjectTweenKit {
// BattleAimHint.Update: input activity hides a quiet non-interactive instruction.
public sealed class HintFade:MonoBehaviour {
 public CanvasGroup Group;public float Speed=2;public bool Visible=true;
 void Update(){if(Group==null)return;Group.blocksRaycasts=Group.interactable=false;Group.alpha=Mathf.MoveTowards(Group.alpha,Visible?1:0,Mathf.Max(0,Speed)*KitClock.ScaledDelta);}
 public void StopAndRestore(){if(Group!=null)Group.alpha=1;Visible=true;}
 void OnDisable(){StopAndRestore();}
}
}
