using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
public static class OnboardingArt {
 [Serializable]sealed class Layout {public Entry[] entries;}
 [Serializable]sealed class Entry {public string key,atlas;public float x,y,w,h;}
 static readonly Dictionary<string,Sprite> cache=new Dictionary<string,Sprite>();
 static Layout layout;
 public static Sprite Get(int atlas,int cell){
  string key=atlas.ToString("D2")+"-"+cell;if(cache.TryGetValue(key,out var found))return found;
  if(layout==null){var json=Resources.Load<TextAsset>("Art/OnboardingV1/layout");if(json==null)throw new InvalidOperationException("Onboarding layout missing");layout=JsonUtility.FromJson<Layout>(json.text);}
  foreach(var e in layout.entries){if(e.key!=key)continue;var t=Resources.Load<Texture2D>("Art/OnboardingV1/"+e.atlas);if(t==null)throw new InvalidOperationException("Onboarding atlas missing: "+e.atlas);
   var border=atlas==1&&cell==0?new Vector4(175,155,175,205):atlas==1&&cell==1?new Vector4(90,90,90,90):Vector4.zero;
   found=Sprite.Create(t,new Rect(e.x,e.y,e.w,e.h),Vector2.one*.5f,100,0,SpriteMeshType.FullRect,border);found.name="Onboarding "+key;cache[key]=found;return found;
  }throw new InvalidOperationException("Onboarding sprite missing: "+key);
 }
 public static Image Draw(Transform parent,string name,int atlas,int cell,float x,float y,float w,float h){var i=UICraft.Box(parent,name,x,y,w,h,Color.white);i.sprite=Get(atlas,cell);i.type=Image.Type.Simple;i.preserveAspect=true;i.raycastTarget=false;return i;}
 public static Image Panel(Transform parent,float w,float h){var i=Draw(parent,"Painted garden lesson card",1,0,0,0,w,h);i.type=Image.Type.Sliced;i.preserveAspect=false;return i;}
}
}
