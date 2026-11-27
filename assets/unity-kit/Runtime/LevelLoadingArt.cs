using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
public static class LevelLoadingArt {
 [Serializable]sealed class Layout{public Part[] sprites;}
 [Serializable]sealed class Part{public string key,name;public float x,y,width,rotation;}
 static Layout layout;
 static readonly Dictionary<string,Sprite> cache=new Dictionary<string,Sprite>();
 public static Sprite Get(string key){
  if(cache.TryGetValue(key,out var found))return found;
  var texture=Resources.Load<Texture2D>("Art/LevelLoadingC/"+key);
  if(texture==null)throw new InvalidOperationException("Missing level loading art: "+key);
  found=Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),Vector2.one*.5f,100,0,SpriteMeshType.FullRect);found.name="LevelLoadingC/"+key;cache[key]=found;return found;
 }
 public static RectTransform Image(Transform parent,string key,float x,float y,float width){
  var s=Get(key);var image=UICraft.Box(parent,key,x,y,width,width*s.rect.height/s.rect.width,Color.white);image.sprite=s;image.type=UnityEngine.UI.Image.Type.Simple;image.preserveAspect=true;image.raycastTarget=false;return image.rectTransform;
 }
 static Part FindPart(string key){if(layout==null)layout=JsonUtility.FromJson<Layout>(Resources.Load<TextAsset>("Art/LevelLoadingC/complete-layout").text);foreach(var part in layout.sprites)if(part.key==key)return part;throw new InvalidOperationException(key);}
 public static RectTransform Complete(Transform parent,string name){var p=FindPart("whole-"+name);var rect=Image(parent,p.key,p.x,p.y,p.width);rect.name=name;rect.localRotation=Quaternion.Euler(0,0,p.rotation);return rect;}
}
}
