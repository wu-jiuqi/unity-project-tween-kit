using UnityEngine;
using UnityEngine.Events;
namespace ProjectTweenKit {
// Composed prefabs reconstruct the original runtime-created sprites and delegates.
// They intentionally do not serialize temporary Sprite.Create or TMP font instances.
public sealed class ExamplePrefab:MonoBehaviour {
 public enum Kind {Press,Switch,Modal,Number,Float,Hit,Sequence}
 public Kind Example;
 public bool InitialValue=true;
 public UnityEvent Clicked=new UnityEvent();
 public UnityEvent<bool> ValueChanged=new UnityEvent<bool>();
 public MonoBehaviour Component{get;private set;}
 bool built,value;
 void Start(){Build();}
 public void Build(){if(built)return;built=true;value=InitialValue;
  switch(Example){
   case Kind.Press:Component=CSettingsArt.Button(transform,"JellyButton","开始",0,0,760,190,()=>Clicked.Invoke()).GetComponent<UIBounce>();break;
   case Kind.Switch:Component=SettingsSwitch77.Create(transform,"SettingsSwitch",0,0,()=>value,v=>{value=v;ValueChanged.Invoke(v);});break;
   case Kind.Modal:var modal=UICraft.Root(transform,"CModal",0,0,1000,1350);CSettingsArt.Image(modal,"panel_cream",0,0,1000,1350,true);var m=modal.gameObject.AddComponent<ModalMotion>();Component=m;CSettingsArt.Button(modal,"完成","完成",0,-400,720,170,()=>m.Close());m.Open(!Application.isPlaying);break;
   case Kind.Number:var n=gameObject.AddComponent<NumberRoll>();n.Target=UICraft.Label(transform,"100",0,0,800,180,100);Component=n;n.Play();break;
   case Kind.Float:var cat=UICraft.Art(transform,"cat_idle",0,0,400,400);var f=cat.gameObject.AddComponent<UIFloat>();f.amplitude=3;Component=f;break;
   case Kind.Hit:var brick=UICraft.Art(transform,"brick_mint",0,0,200,170);var h=brick.gameObject.AddComponent<JellyHit>();h.Target=brick.transform;Component=h;h.Play();break;
   case Kind.Sequence:var image=UICraft.Box(transform,"CatSequence",0,0,720,720,Color.white);var s=gameObject.AddComponent<FrameSequence>();s.Target=image;Component=s;s.Play();break;
  }
 }
}
}
