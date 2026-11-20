using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace ProjectTweenKit {
[Serializable]public sealed class CopperFrame {public float x,y,w,h;}
[Serializable]public sealed class CopperClip {public string key;public int sourceWidth,sourceHeight;public CopperFrame[] frames;}
[Serializable]public sealed class CopperLayout {public CopperClip[] clips;}
public static class OpeningCopperArt {
 public const int MaxHealth=25,HealthPerState=5;
 public static readonly Color NumberColor=new Color32(73,37,13,255);
 public static void StyleNumber(TMP_Text text){text.color=NumberColor;text.fontStyle=FontStyles.Bold;text.outlineColor=new Color32(255,245,214,255);text.outlineWidth=.14f;}
 static CopperLayout data;static readonly Dictionary<string,Sprite[]> cache=new Dictionary<string,Sprite[]>();
 static CopperClip Clip(string key){if(data==null)data=JsonUtility.FromJson<CopperLayout>(Resources.Load<TextAsset>("Art/FourthGroup/layout").text);foreach(var c in data.clips)if(c.key==key)return c;throw new InvalidOperationException("Missing copper clip "+key);}
 public static Sprite Frame(string key,int index){var c=Clip(key);if(!cache.TryGetValue(key,out var frames)){var tex=Resources.Load<Texture2D>("Art/FourthGroup/"+key);if(tex==null)throw new InvalidOperationException("Missing copper texture "+key);float sx=(float)tex.width/c.sourceWidth,sy=(float)tex.height/c.sourceHeight;frames=new Sprite[c.frames.Length];for(int i=0;i<frames.Length;i++){var f=c.frames[i];frames[i]=Sprite.Create(tex,new Rect(f.x*sx,(c.sourceHeight-f.y-f.h)*sy,f.w*sx,f.h*sy),Vector2.one*.5f,100*sx,0,SpriteMeshType.FullRect);frames[i].name="copper-"+key+"-"+i;}cache[key]=frames;}return frames[Mathf.Clamp(index,0,frames.Length-1)];}
 public static int StateIndex(int hp)=>Mathf.Clamp((MaxHealth-Mathf.Clamp(hp,1,MaxHealth))/HealthPerState,0,4);
 public static Sprite State(int hp)=>Frame("states",StateIndex(hp));
}
}
