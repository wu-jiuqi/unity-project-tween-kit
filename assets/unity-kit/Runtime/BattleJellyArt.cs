using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
namespace ProjectTweenKit {
public static class ChineseArt {
 public static Image Image(Transform parent,string name,Sprite sprite,float x,float y,float w,float h){var image=UICraft.Box(parent,name,x,y,w,h,Color.white);image.sprite=sprite;image.type=UnityEngine.UI.Image.Type.Simple;image.preserveAspect=true;image.raycastTarget=false;return image;}
}
public static class BattleJellyArt {
 static readonly Dictionary<string,Sprite> cache=new Dictionary<string,Sprite>();
 public static Sprite Get(string key){if(cache.TryGetValue(key,out var s))return s;var t=Resources.Load<Texture2D>("Art/BattleJellyV5/"+key);if(t==null)throw new System.InvalidOperationException("Missing battle art "+key);return cache[key]=Sprite.Create(t,new Rect(0,0,t.width,t.height),Vector2.one*.5f,100,0,SpriteMeshType.FullRect);}
 public static Image Image(Transform p,string key,float x,float y,float w,float h){var i=UICraft.Box(p,key,x,y,w,h,Color.white);i.type=UnityEngine.UI.Image.Type.Simple;i.sprite=Get(key);i.preserveAspect=false;i.raycastTarget=false;return i;}
}
public static class GoalCelebrationArt {
 static readonly Dictionary<string,Sprite> cache=new Dictionary<string,Sprite>();
 public static Sprite Get(string key){if(cache.TryGetValue(key,out var s))return s;var t=Resources.Load<Texture2D>("Art/GoalCelebrationV1/"+key);if(t==null)throw new System.InvalidOperationException("Missing celebration art "+key);return cache[key]=Sprite.Create(t,new Rect(0,0,t.width,t.height),Vector2.one*.5f,100,0,SpriteMeshType.FullRect);}
}
}
