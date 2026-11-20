using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ProjectTweenKit;
namespace TweenGallery {
// Player-side evidence, runs against the exact released executable via --verify-kit.
// Pointer dispatch tests exercise handlers; screenshots are independently inspected.
public sealed class GalleryVerification:MonoBehaviour {
 [Serializable]class Report {public string unityVersion,applicationVersion,platform;public List<Check> checks=new List<Check>();public List<string> errors=new List<string>();public int passed,failed;}
 [Serializable]class Check {public string name;public bool passed;}
 Report report=new Report();Gallery gallery;string output;
 void OnEnable(){Application.logMessageReceived+=Log;}
 void OnDisable(){Application.logMessageReceived-=Log;}
 void Log(string condition,string trace,LogType type){if(type==LogType.Exception||type==LogType.Error)report.errors.Add(condition+"\n"+trace);}
 void CheckThat(string name,bool success){report.checks.Add(new Check{name=name,passed=success});if(success)report.passed++;else report.failed++;}
 IEnumerator Start(){gallery=GetComponent<Gallery>();var args=Environment.GetCommandLineArgs();int at=Array.IndexOf(args,"--evidence");output=at>=0&&at+1<args.Length?args[at+1]:Path.Combine(Application.persistentDataPath,"verification");Directory.CreateDirectory(output);report.unityVersion=Application.unityVersion;report.applicationVersion=Application.version;report.platform=Application.platform.ToString();SoundService.Muted=true;
  for(int i=0;i<Gallery.Entries.Length;i++){
   gallery.Select(i);yield return null;yield return null;CheckThat(Gallery.Entries[i].id+": component exists",gallery.CurrentComponent!=null);
   yield return new WaitForSecondsRealtime(Gallery.Entries[i].id=="logo"?2.4f:.55f);
   ScreenCapture.CaptureScreenshot(Path.Combine(output,Gallery.Entries[i].id+".png"));yield return new WaitForEndOfFrame();
   // Pause must freeze local transforms and the presentation clock.
   float before=KitClock.Now;var comp=gallery.CurrentComponent;Vector3 pose=comp.transform.localScale;KitClock.Paused=true;yield return new WaitForSecondsRealtime(.09f);CheckThat(Gallery.Entries[i].id+": pause clock",Mathf.Approximately(before,KitClock.Now));CheckThat(Gallery.Entries[i].id+": pause pose",Vector3.Distance(pose,comp.transform.localScale)<.001f);KitClock.Paused=false;
   // Repeated teardown never leaves a running component from the previous entry.
   gallery.Replay();gallery.Replay();yield return null;
  }
  gallery.Select(0);yield return null;var bounce=(UIBounce)gallery.CurrentComponent;var target=bounce.Target;Vector3 rest=target.localScale;Vector3 rootScale=bounce.transform.localScale;
  var pointer=new PointerEventData(EventSystem.current){pointerId=-1,button=PointerEventData.InputButton.Left,position=RectTransformUtility.WorldToScreenPoint(null,bounce.transform.position),eligibleForClick=true};
  bounce.OnPointerEnter(pointer);bounce.OnPointerDown(pointer);yield return new WaitForSecondsRealtime(.18f);CheckThat("press deforms only visual",Vector3.Distance(target.localScale,rest)>.01f&&bounce.transform.localScale==rootScale);
  bounce.OnBeginDrag(pointer);pointer.position+=new Vector2(400,150);bounce.OnDrag(pointer);bounce.OnPointerUp(pointer);CheckThat("drag cancels click",!pointer.eligibleForClick);bounce.OnPointerExit(pointer);yield return new WaitForSecondsRealtime(.8f);CheckThat("release returns to rest",Vector3.Distance(target.localScale,rest)<.002f);
  bounce.OnPointerDown(pointer);yield return new WaitForSecondsRealtime(.04f);bounce.enabled=false;CheckThat("disable restores press",Vector3.Distance(target.localScale,rest)<.002f);
  bounce.enabled=true;bounce.OnPointerDown(pointer);yield return null;bounce.SendMessage("OnApplicationFocus",false);CheckThat("focus loss restores press",Vector3.Distance(target.localScale,rest)<.002f);
  gallery.Select(2);yield return null;var modal=(ModalMotion)gallery.CurrentComponent;modal.Open();yield return new WaitForSecondsRealtime(.08f);modal.Close();yield return new WaitForSecondsRealtime(.2f);CheckThat("modal close interrupts open",modal.State=="Closed"&&!modal.GetComponent<CanvasGroup>().blocksRaycasts);modal.Open();modal.Close();modal.Open();yield return new WaitForSecondsRealtime(.3f);CheckThat("modal rapid reentry settles open",modal.State=="Open");
  gallery.Select(4);yield return null;var number=(NumberRoll)gallery.CurrentComponent;number.Play(true);CheckThat("instant number ends at requested value",number.Displayed==number.To);number.Play();yield return new WaitForSecondsRealtime(1.15f);CheckThat("animated number same requested result",number.Displayed==number.To);
  number.Duration=.321f;string preset=Path.Combine(output,"number-preset.json");MotionPreset.Capture(number).Save(preset);number.Duration=.9f;MotionPreset.Load(preset).Apply(number);CheckThat("preset round trip same component",Mathf.Approximately(number.Duration,.321f));
  bool rejected=false;try{MotionPreset.Load(preset).Apply(modal);}catch(Exception){rejected=true;}CheckThat("preset rejects different component",rejected);
  gallery.Replay();yield return null;number=(NumberRoll)gallery.CurrentComponent;CheckThat("replay retains tuned duration",Mathf.Approximately(number.Duration,.321f));gallery.Seek(.16f);int sampled=((NumberRoll)gallery.CurrentComponent).Displayed;yield return new WaitForSecondsRealtime(.1f);CheckThat("timeline holds sampled pose",((NumberRoll)gallery.CurrentComponent).Displayed==sampled&&sampled>100&&sampled<180);KitClock.Paused=false;yield return new WaitForSecondsRealtime(.25f);CheckThat("timeline resumes from sampled time",((NumberRoll)gallery.CurrentComponent).Displayed==180);
  gallery.Select(3);yield return null;var flight=(RewardFlight)gallery.CurrentComponent;flight.Play(true);var end=flight.Target.anchoredPosition;flight.Play();yield return new WaitForSecondsRealtime(.65f);CheckThat("reward instant and animated endpoint agree",Vector2.Distance(flight.Target.anchoredPosition,end)<.01f);flight.StopAndRestore();CheckThat("flight returns to start",Vector2.Distance(flight.Target.anchoredPosition,new Vector2(-250,-300))<.01f);
  gallery.Select(12);yield return null;var entrance=(CollectionGoalEntrance)gallery.CurrentComponent;var targets=entrance.transform.parent.Find("Original HUD home");entrance.Restore();CheckThat("target entrance restores original hierarchy",targets!=null&&targets.Find("Real goals")!=null);
  var restored=targets.Find("Real goals") as RectTransform;CheckThat("target entrance restores transform and sibling",restored.GetSiblingIndex()==0&&restored.anchoredPosition==Vector2.zero&&restored.localScale==Vector3.one&&restored.localRotation==Quaternion.identity);
  gallery.Select(1);yield return null;int writes=0;bool on=true;var testSwitch=SettingsSwitch77.Create(gallery.CurrentComponent.transform.parent,"Write guard",0,0,()=>on,v=>{on=v;writes++;});testSwitch.Refresh();testSwitch.Refresh();CheckThat("switch refresh performs no business write",writes==0);testSwitch.GetComponent<Button>().onClick.Invoke();testSwitch.Refresh();CheckThat("switch real click writes exactly once",writes==1&&!on);testSwitch.gameObject.SetActive(false);var knob=testSwitch.transform.Find("ToggleVisual/toggle_knob") as RectTransform;CheckThat("switch disable aligns to host state",knob!=null&&Mathf.Approximately(knob.anchoredPosition.x,-39));
  gallery.Select(2);yield return null;var vectorRoot=UICraft.Root(gallery.CurrentComponent.transform.parent,"Nonuniform parent compensation");vectorRoot.localScale=new Vector3(.7f,.9f,1);var vectorModal=vectorRoot.gameObject.AddComponent<ModalMotion>();vectorModal.Open();yield return new WaitForSecondsRealtime(.3f);CheckThat("modal retains nonuniform target vector",Vector3.Distance(vectorRoot.localScale,new Vector3(.7f,.9f,1))<.001f);vectorModal.Close(true);
  gallery.Select(5);yield return null;var logo=(JellyLogoIntro)gallery.CurrentComponent;logo.Begin();logo.Begin();yield return null;CheckThat("logo reentry leaves one set of four glyphs",logo.GetComponentsInChildren<JellyGlyph>().Length==4);
  CheckThat("victory sequence preserves first nonuniform hold",VictoryCatAnimation.FrameAt(.29f)==0&&VictoryCatAnimation.FrameAt(.31f)==1);CheckThat("failure sequence preserves different first hold",FailureCatAnimation.FrameAt(.29f)==1);
  // No component references a game profile, GameSession or navigation implementation: this is also checked by validate.py.
  CheckThat("no runtime errors",report.errors.Count==0);File.WriteAllText(Path.Combine(output,"runtime-verification.json"),JsonUtility.ToJson(report,true));Application.Quit(report.failed==0&&report.errors.Count==0?0:2);
 }
}
}
