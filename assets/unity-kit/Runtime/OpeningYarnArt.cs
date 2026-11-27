using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTweenKit {
[Serializable] public sealed class YarnFrameSpec {public float x,y,w,h,px,py,bodyWidth,bodyHeight;}
[Serializable] public sealed class YarnClipSpec {public string key;public int sourceWidth,sourceHeight;public YarnFrameSpec[] frames;}
[Serializable] public sealed class YarnAnimationData {public YarnClipSpec[] clips;}
public static class OpeningYarnArt {
 static readonly Dictionary<string,Sprite[]> sprites=new Dictionary<string,Sprite[]>();
 static YarnAnimationData data;static Material material;static Sprite icon;
 public static Material Material {get {if(material==null){var shader=Resources.Load<Shader>("OpeningYarn");if(shader==null)throw new InvalidOperationException("Missing yarn alpha shader");material=new Material(shader);}return material;}}
 static YarnClipSpec Clip(string key){if(data==null)data=JsonUtility.FromJson<YarnAnimationData>(Resources.Load<TextAsset>("Art/YarnObstacleV3/animation-layout").text);foreach(var clip in data.clips)if(clip.key==key)return clip;throw new InvalidOperationException("Missing yarn animation "+key);}
 public static YarnFrameSpec Spec(string key,int frame)=>Clip(key).frames[Mathf.Clamp(frame,0,Clip(key).frames.Length-1)];
 public static Sprite Frame(string key,int frame){if(!sprites.TryGetValue(key,out var frames)){var texture=Resources.Load<Texture2D>("Art/YarnObstacleV3/"+key);if(texture==null)throw new InvalidOperationException("Missing yarn image "+key);var clip=Clip(key);var specs=clip.frames;frames=new Sprite[specs.Length];for(int i=0;i<frames.Length;i++){var s=specs[i];float sx=texture.width/(float)clip.sourceWidth,sy=texture.height/(float)clip.sourceHeight;frames[i]=Sprite.Create(texture,new Rect(s.x*sx,s.y*sy,s.w*sx,s.h*sy),new Vector2(s.px,s.py),100*sx,0,SpriteMeshType.FullRect);frames[i].name=key+"-"+i;}sprites[key]=frames;}return frames[Mathf.Clamp(frame,0,frames.Length-1)];}
 public static string RestKey(int layers)=>layers>=3?"yarn-obstacle-l3":layers==2?"yarn-obstacle-l2":"yarn-obstacle-l1";
 public static Sprite Rest(int layers)=>Frame(RestKey(layers),0);
 public static Vector3 Fit(string key,int frame,float width=.76f,float height=.76f){var s=Spec(key,frame);return new Vector3(width/(s.bodyWidth/100f),height/(s.bodyHeight/100f),1);}
 public static Sprite Icon {get{if(icon==null){var s=Rest(1);icon=Sprite.Create(s.texture,s.rect,Vector2.one*.5f,s.pixelsPerUnit,0,SpriteMeshType.FullRect);}return icon;}}
 public static SpriteRenderer Draw(Transform parent,string name,int layers,int order=62){var view=new GameObject(name).AddComponent<SpriteRenderer>();view.transform.SetParent(parent,false);view.sprite=Rest(layers);view.sharedMaterial=Material;view.sortingOrder=order;view.transform.localPosition=Vector2.down*(.76f*.5f);view.transform.localScale=Fit(RestKey(layers),0);return view;}
}

}
