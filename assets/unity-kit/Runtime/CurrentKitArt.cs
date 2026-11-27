using System;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
public static class CurrentKitArt {
 static readonly System.Collections.Generic.Dictionary<string,Sprite> stars=new System.Collections.Generic.Dictionary<string,Sprite>();
 public static Sprite Star(string key){if(stars.TryGetValue(key,out var sprite))return sprite;var t=Resources.Load<Texture2D>("Art/BattleGarden050/"+key);if(t==null)throw new InvalidOperationException(key);return stars[key]=Sprite.Create(t,new Rect(0,0,t.width,t.height),Vector2.one*.5f,100,0,SpriteMeshType.FullRect);}
 public static Image StarImage(Transform parent,string key,float x,float y,float w,float h){var i=UICraft.Box(parent,key,x,y,w,h,Color.white);i.sprite=Star(key);i.type=Image.Type.Simple;i.preserveAspect=true;i.raycastTarget=false;return i;}
 public static Button Button(Transform parent,string name,string text,float x,float y,float w,float h,Action action,string style,int size)=>CommonButtonsA.Button(parent,name,text,x,y,w,h,action,style,size);
}
}
