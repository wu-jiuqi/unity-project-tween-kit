using System;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
// Closing is presentation only. The host handles a real button intent after closure.
public sealed class ErrorPopupMotion:MonoBehaviour {
 public ModalMotion Motion{get;private set;}public LoadingBoxAnimation Actor{get;private set;}
 public bool Closed=>Motion!=null&&Motion.State=="Closed";
 Action intent;bool closing;float age;
 public void Build(){var panel=UICraft.Root(transform,"Error visual",0,0,1080,1700);LevelLoadingArt.Image(panel,"panel",0,0,940);UICraft.Label(panel,"暂时未能完成",0,510,850,100,64,CSettingsArt.Ink);Actor=LoadingBoxAnimation.Create(panel,110,.86f);UICraft.Label(panel,"请重试，或返回上一页",0,-260,820,130,39,CSettingsArt.Ink);Motion=gameObject.AddComponent<ModalMotion>();Motion.Target=panel;Motion.Close(true);}
 public void Open(){intent=null;closing=false;age=0;Actor.Render(0);Motion.Open();}
 public bool CloseWithIntent(Action action){if(closing||Closed)return false;intent=action;closing=true;Motion.Close();return true;}
 void Update(){if(Motion==null)return;age+=Mathf.Min(KitClock.Delta,.05f);Actor.Render(age);if(closing&&Closed){closing=false;var action=intent;intent=null;action?.Invoke();}}
 public void StopAndRestore(){intent=null;closing=false;age=0;Motion?.StopAndRestore();Actor?.StopAndRestore();}
 void OnDisable(){StopAndRestore();}
}
}
