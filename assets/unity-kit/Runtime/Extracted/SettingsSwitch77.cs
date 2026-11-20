using System;using UnityEngine;using UnityEngine.UI;using DG.Tweening;
namespace ProjectTweenKit {
public sealed class SettingsSwitch77:MonoBehaviour {
 public float SlideDuration=.16f;
 Func<bool> read;Action<bool> write;string key;Image track;RectTransform knob;Tween slide;
 public static SettingsSwitch77 Create(Transform parent,string key,float x,float y,Func<bool> read,Action<bool> write){
  var root=UICraft.Root(parent,key,x,y,180,156);var hit=root.gameObject.AddComponent<Image>();hit.color=Color.clear;hit.raycastTarget=true;
  var control=root.gameObject.AddComponent<SettingsSwitch77>();control.read=read;control.write=write;control.key=key;
  var visual=UICraft.Root(root,"ToggleVisual",0,0,180,156);
  control.track=CSettingsArt.Image(visual,"toggle_track_on",0,0,164,82);control.track.preserveAspect=false;
  control.knob=CSettingsArt.Image(visual,"toggle_knob",0,0,68,68).rectTransform;
  var button=root.gameObject.AddComponent<Button>();button.targetGraphic=control.track;button.transition=Selectable.Transition.None;
  button.onClick.AddListener(()=>{SoundService.Play("click");write(!read());control.Refresh();});root.gameObject.AddComponent<UIBounce>().Target=visual;control.Refresh(false);return control;
 }
 public void Refresh(){Refresh(true);}
 void Refresh(bool animate){
  bool value=read();track.sprite=CSettingsArt.Get(value?"toggle_track_on":"toggle_track_off");float x=value?39:-39;slide?.Kill(false);
  if(animate)slide=knob.DOAnchorPos(new Vector2(x,0),SlideDuration).SetEase(Ease.OutCubic).SetUpdate(KitClock.TweenUpdate, true);else knob.anchoredPosition=new Vector2(x,0);
  gameObject.name=key+(value?"：开":"：关");
 }
 void OnDisable(){slide?.Kill(false);if(read!=null&&knob!=null)Refresh(false);}
}
}

