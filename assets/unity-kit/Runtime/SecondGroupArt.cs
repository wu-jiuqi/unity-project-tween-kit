using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTweenKit {
[Serializable] public sealed class SecondGroupSpriteSpec {
 public string key,asset;public int sourceWidth,sourceHeight;public float x,y,w,h;
}
[Serializable] public sealed class SecondGroupArtData {
 public int schema,boxCells,maxCount;public float pitch,ordinaryBody,boxBody;public SecondGroupSpriteSpec[] sprites;
}
/// <summary>Whole-object sprites. Regions calibrate transparent margins, never divide a box into cells.</summary>
public static class SecondGroupArt {
 const string Root="Art/SecondGroupProduction/";
 static SecondGroupArtData data;static Material material;
 static readonly Dictionary<string,Sprite> cache=new Dictionary<string,Sprite>();
 public static SecondGroupArtData Data {get {if(data==null){var json=Resources.Load<TextAsset>(Root+"manifest");if(json==null)throw new InvalidOperationException("Missing second-group production manifest");data=JsonUtility.FromJson<SecondGroupArtData>(json.text);}return data;}}
 public static Material Material {get {if(material==null){var shader=Resources.Load<Shader>("SecondGroupSprite");if(shader==null)throw new InvalidOperationException("Missing second-group sprite shader");material=new Material(shader);}return material;}}
 public static SecondGroupSpriteSpec Spec(string key){foreach(var s in Data.sprites)if(s.key==key)return s;throw new ArgumentException("Unknown second-group sprite: "+key);}
 public static Sprite Sprite(string key){
  if(cache.TryGetValue(key,out var sprite))return sprite;
  var s=Spec(key);var texture=Resources.Load<Texture2D>(Root+s.asset);
  if(texture==null)throw new InvalidOperationException("Missing production sprite: "+s.asset);
  float sx=texture.width/(float)s.sourceWidth,sy=texture.height/(float)s.sourceHeight;
  sprite=UnityEngine.Sprite.Create(texture,new Rect(s.x*sx,s.y*sy,s.w*sx,s.h*sy),Vector2.one*.5f,100*sx,0,SpriteMeshType.FullRect);
  sprite.name=key;cache[key]=sprite;return sprite;
 }
 public static string BoxKey(int remaining,bool open=true){if(remaining<0||remaining>Data.maxCount)throw new ArgumentOutOfRangeException(nameof(remaining),"Production art contains exact counts 0–8");return open?"box-"+remaining:"box-closed";}
 public static void Apply(SpriteRenderer image,string key,float side){
  image.sprite=Sprite(key);image.sharedMaterial=Material;image.color=Color.white;
  var size=image.sprite.bounds.size;float uniform=side/Mathf.Max(size.x,size.y);
  image.transform.localScale=Vector3.one*uniform;
 }
}
}
