using UnityEngine;
using DG.Tweening;
namespace ProjectTweenKit {
// AppUI.LayoutSettingsSheetC + CloseModal, detached from session resume/navigation.
[RequireComponent(typeof(CanvasGroup))]
public sealed class ModalMotion:MonoBehaviour {
 public float OpenDuration=.18f,CloseDuration=.14f,OpenScale=.96f,CloseScale=.94f;
 public RectTransform Target;
 public bool IsOpen{get;private set;}
 public string State{get;private set;}="Closed";
 Tween motion;CanvasGroup group;Vector3 rest;bool captured;
 void Capture(){if(captured)return;Target=Target!=null?Target:(RectTransform)transform;rest=Target.localScale;group=GetComponent<CanvasGroup>();captured=true;}
 public void Open(bool immediate=false){Capture();motion?.Kill(false);gameObject.SetActive(true);group.blocksRaycasts=group.interactable=true;IsOpen=true;State="Opening";Target.localScale=rest*OpenScale;group.alpha=0;if(immediate){Target.localScale=rest;group.alpha=1;State="Open";return;}SoundService.Play("whoosh");motion=DOTween.Sequence().SetUpdate(KitClock.TweenUpdate,true).Append(Target.DOScale(rest,OpenDuration).SetEase(Ease.OutCubic)).Join(group.DOFade(1,OpenDuration)).OnComplete(()=>State="Open");}
 public void Close(bool immediate=false){Capture();motion?.Kill(false);group.blocksRaycasts=group.interactable=false;IsOpen=false;State="Closing";if(immediate){group.alpha=0;Target.localScale=rest;State="Closed";return;}SoundService.Play("ui_back");motion=DOTween.Sequence().SetUpdate(KitClock.TweenUpdate,true).Append(Target.DOScale(Target.localScale*CloseScale,CloseDuration).SetEase(Ease.InCubic)).Join(group.DOFade(0,CloseDuration)).OnComplete(()=>{Target.localScale=rest;State="Closed";});}
 public void StopAndRestore(){motion?.Kill(false);if(!captured)return;Target.localScale=rest;group.alpha=0;group.blocksRaycasts=group.interactable=false;IsOpen=false;State="Closed";}
 void OnDisable(){StopAndRestore();}
}
}
