using System;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectTweenKit {
[Serializable]public sealed class ThirdFrame {public float x,y,w,h;}
[Serializable]public sealed class ThirdClip {public string key;public int sourceWidth,sourceHeight;public float reference;public ThirdFrame[] frames;}
[Serializable]public sealed class ThirdLayout {public ThirdClip[] clips;}
public static class OpeningThirdArt {
 static ThirdLayout data;static readonly Dictionary<string,Sprite[]> cache=new Dictionary<string,Sprite[]>();
 static ThirdClip Clip(string key){if(data==null)data=JsonUtility.FromJson<ThirdLayout>(Resources.Load<TextAsset>("Art/ThirdGroup/layout").text);foreach(var c in data.clips)if(c.key==key)return c;throw new InvalidOperationException("Missing third-group clip "+key);}
 public static Sprite Frame(string key,int index){var c=Clip(key);if(!cache.TryGetValue(key,out var sprites)){var texture=Resources.Load<Texture2D>("Art/ThirdGroup/"+key);sprites=new Sprite[c.frames.Length];float sx=(float)texture.width/c.sourceWidth,sy=(float)texture.height/c.sourceHeight;for(int i=0;i<sprites.Length;i++){var f=c.frames[i];sprites[i]=Sprite.Create(texture,new Rect(f.x*sx,(c.sourceHeight-f.y-f.h)*sy,f.w*sx,f.h*sy),key=="cat"?new Vector2(.5f,0):Vector2.one*.5f,100*sx,0,SpriteMeshType.FullRect);sprites[i].name="third-"+key+"-"+i;}cache[key]=sprites;}return sprites[Mathf.Clamp(index,0,sprites.Length-1)];}
 public static void Fit(SpriteRenderer image,string key,int frame,float size){image.sprite=Frame(key,frame);image.sharedMaterial=OpeningYarnArt.Material;float reference=key=="cat"?Clip(key).reference/100f:Mathf.Max(image.sprite.bounds.size.x,image.sprite.bounds.size.y);image.transform.localScale=Vector3.one*(size/reference);}
 public static SpriteRenderer Draw(Transform parent,string key,int frame,float size,int order=67){var sr=new GameObject(key+" artwork").AddComponent<SpriteRenderer>();sr.transform.SetParent(parent,false);sr.sortingOrder=order;Fit(sr,key,frame,size);return sr;}
 public static int Variant(int kind)=>kind==6?1:kind==5?2:kind==1?3:kind==2?4:kind==3?5:0;
 public static string KindName(int kind)=>kind==6?"horizontal":kind==5?"vertical":kind==1?"cross":kind==2?"bomb":"rainbow";
}
}
