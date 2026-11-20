using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace ProjectTweenKit {
// StartupLoadingView86 progress slice. Task completion is always supplied by the host.
public sealed class LoadingProgress:MonoBehaviour {
 public float FillSpeed=.32f,MaxFrameDelta=.05f,CompletionDuration=.42f,CompletionAmplitude=.045f,TravelWidth=636,MarbleLift=14;
 public RectTransform Visual,Marble,Shine;public Image Fill;public TMP_Text Number;
 public RectTransform[] Stars;
 float shown,target,age,completionAge;bool complete,failed;Vector3 rest=Vector3.one;
 public float Value=>shown;public float TargetValue=>target;public bool Failed=>failed;
 public bool IsComplete=>complete&&shown>=1;public int Percent=>IsComplete?100:Mathf.Min(99,Mathf.FloorToInt(shown*100+.0001f));
 public static LoadingProgress Create(Transform parent,float x=0,float y=0){
  var root=UICraft.Root(parent,"Jelly loading progress",x,y,700,150);var c=root.gameObject.AddComponent<LoadingProgress>();c.Visual=root;
  StartupArt.Picture(root,"progress-track",0,0,700);var fill=StartupArt.Picture(root,"progress-fill",0,8,636);fill.sizeDelta=new Vector2(636,82);c.Fill=fill.GetComponent<Image>();c.Fill.type=Image.Type.Filled;c.Fill.fillMethod=Image.FillMethod.Horizontal;c.Fill.fillOrigin=0;
  StartupArt.Picture(root,"progress-paw",-358,2,64).localRotation=Quaternion.Euler(0,0,-12);
  c.Shine=StartupArt.Picture(root,"progress-sparkle",0,14,30);c.Marble=StartupArt.Picture(root,"progress-marble",-318,8,78);c.Stars=new RectTransform[6];for(int i=0;i<6;i++)c.Stars[i]=StartupArt.Picture(root,"progress-sparkle",318,8,24+i%2*8);
  c.Number=UICraft.Label(root,"0%",0,-120,600,58,40);c.ResetProgress();return c;
 }
 public void CapturePose(){if(Visual!=null)rest=Visual.localScale;}
 public void SetProgress(float value){if(float.IsNaN(value)||float.IsInfinity(value))return;target=Mathf.Max(target,Mathf.Clamp(value,0,.99f));}
 public void Complete(){complete=true;target=1;}
 public void Failure(){failed=true;}
 public void Resume(){failed=false;}
 public void ResetProgress(){shown=target=age=completionAge=0;complete=failed=false;Render(0);}
 public void Advance(float delta){if(Visual==null||failed||delta<=0)return;float dt=Mathf.Clamp(delta,0,Mathf.Max(.001f,MaxFrameDelta));shown=Mathf.MoveTowards(shown,Mathf.Max(shown,target),Mathf.Max(0,FillSpeed)*dt);if(!IsComplete)age+=Mathf.Min(.1f,delta);Render(dt);}
 void Update(){Advance(KitClock.Delta);}
 void Render(float dt){if(Fill==null)return;Fill.fillAmount=shown;if(Number!=null)Number.text=Percent+"%";if(IsComplete)completionAge+=dt;
  float phase=Mathf.Clamp01(completionAge/Mathf.Max(.001f,CompletionDuration));float pulse=IsComplete?Mathf.Sin(phase*Mathf.PI)*CompletionAmplitude:Mathf.Sin(age*2.5f)*.004f;
  Visual.localScale=Vector3.Scale(rest,new Vector3(1+pulse,1-pulse*.55f,1));float half=TravelWidth*.5f;
  Marble.anchoredPosition=new Vector2(-half+TravelWidth*shown,8+(!IsComplete?Mathf.Sin(age*4)*2:Mathf.Sin(phase*Mathf.PI)*MarbleLift));Marble.localRotation=Quaternion.Euler(0,0,IsComplete?0:Mathf.Sin(age*2)*5);Marble.localScale=Vector3.one*(1+(IsComplete?pulse:Mathf.Sin(age*3)*.018f));
  Shine.gameObject.SetActive(shown>.04f&&!IsComplete&&!failed);Shine.anchoredPosition=new Vector2(-half+TravelWidth*shown*Mathf.Repeat(age*.32f,1),17);Shine.localScale=Vector3.one*(.7f+.3f*Mathf.Sin(age*3));
  for(int i=0;i<Stars.Length;i++){var star=Stars[i];bool show=IsComplete&&completionAge<.48f;star.gameObject.SetActive(show);if(!show)continue;float p=Mathf.Clamp01(completionAge/.48f),a=(i*60+15)*Mathf.Deg2Rad;star.anchoredPosition=new Vector2(half,8)+new Vector2(Mathf.Cos(a),Mathf.Sin(a))*(25+65*p);star.localScale=Vector3.one*Mathf.Sin(p*Mathf.PI);}
 }
 public void StopAndRestore(){ResetProgress();}
 void OnDisable(){ResetProgress();}
}
public static class StartupArt {
 static readonly Dictionary<string,Sprite> cache=new Dictionary<string,Sprite>();
 public static Sprite Get(string key){if(cache.TryGetValue(key,out var sprite))return sprite;var t=Resources.Load<Texture2D>("Art/StartupLoading/"+key);if(t==null)throw new InvalidOperationException("Missing startup artwork "+key);return cache[key]=Sprite.Create(t,new Rect(0,0,t.width,t.height),Vector2.one*.5f,100);}
 public static RectTransform Picture(Transform parent,string key,float x,float y,float width){var s=Get(key);var image=UICraft.Box(parent,key,x,y,width,width*s.rect.height/s.rect.width,Color.white);image.sprite=s;image.type=Image.Type.Simple;image.raycastTarget=false;return image.rectTransform;}
}
}
