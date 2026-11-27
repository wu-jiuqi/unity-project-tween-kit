using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace ProjectTweenKit {
public static class SevenSpecialArt {
 public const int ColorCount=5;
 public static readonly string[] ColorKeys={"pink","yellow","mint","blue","lilac"};
 public static readonly Color[] Palette={new Color32(246,138,170,255),new Color32(245,211,108,255),new Color32(140,221,165,255),new Color32(125,206,235,255),new Color32(197,160,237,255)};
 static readonly string[] Names={"粉红","明黄","浅薄荷绿","天蓝","浅丁香紫"};
 [Serializable]sealed class Footprints{public Footprint[] entries;}
 [Serializable]sealed class Footprint{public string key;public float fraction;}
 static readonly Dictionary<string,float> footprints=new Dictionary<string,float>();
 static bool footprintsLoaded;
 static readonly Dictionary<string,Sprite[]> baked=new Dictionary<string,Sprite[]>();
 [Serializable]sealed class PhaseLayout{public PhaseColumn[] entries;public PhaseRows[] rows;}
 [Serializable]sealed class PhaseRows{public string key;public float[] cuts;}
 [Serializable]sealed class PhaseColumn{public string key;public int column;public float left,width,pivotX;}
 static PhaseLayout phaseLayout;
 static readonly Dictionary<Sprite,float> phaseWidths=new Dictionary<Sprite,float>();
 static Sprite FiveColorFrame(Texture2D texture,string kind,int column,int row){
  if(phaseLayout==null){var text=Resources.Load<TextAsset>("Art/FiveBakedV80/sequence-layout");if(text==null)throw new InvalidOperationException("Missing reviewed five-color slice layout");phaseLayout=JsonUtility.FromJson<PhaseLayout>(text.text);}
  foreach(var entry in phaseLayout.entries)if(entry.key==kind&&entry.column==column)foreach(var bands in phaseLayout.rows)if(bands.key==kind){float top=bands.cuts[row],bottom=bands.cuts[row+1],height=bottom-top,pivotY=(bottom-(row+.5f)/5f)/height;var sprite=Sprite.Create(texture,new Rect(entry.left*texture.width,(1-bottom)*texture.height,entry.width*texture.width,height*texture.height),new Vector2(entry.pivotX,pivotY),256,0,SpriteMeshType.FullRect);phaseWidths[sprite]=texture.width/5f/256f;return sprite;}
  throw new InvalidOperationException("Missing five-color phase column: "+kind+"/"+column);
 }

 [Serializable]sealed class SquareLayout{public SquareShape[] entries;}
 [Serializable]public sealed class SquareShape{public string key;public float x,y,rectWidth,rectHeight,width,height,centerX,centerY;}
 static readonly Dictionary<string,SquareShape> squareShapes=new Dictionary<string,SquareShape>();
 static readonly Dictionary<string,Sprite> squareSprites=new Dictionary<string,Sprite>();
 static readonly Dictionary<Sprite,SquareShape> spriteShapes=new Dictionary<Sprite,SquareShape>();
 static readonly Dictionary<string,Sprite> specialIcons=new Dictionary<string,Sprite>();
 static SquareShape Shape(string key){if(squareShapes.Count==0){var data=Resources.Load<TextAsset>("Art/FiveBakedV80/square-layout-v2");if(data==null)throw new InvalidOperationException("Missing square special layout");foreach(var e in JsonUtility.FromJson<SquareLayout>(data.text).entries)squareShapes[e.key]=e;}return squareShapes[key];}
 static Sprite Square(string kind,int color){string key=BakedKey(kind,color);if(squareSprites.TryGetValue(key,out var sprite))return sprite;var texture=Resources.Load<Texture2D>("Art/FiveBakedV80/square-specials-v2");if(texture==null)throw new InvalidOperationException("Missing square special illustration");var shape=Shape(key);sprite=Sprite.Create(texture,new Rect(shape.x*texture.width,shape.y*texture.height,shape.rectWidth*texture.width,shape.rectHeight*texture.height),Vector2.one*.5f,256,0,SpriteMeshType.FullRect);sprite.name="square-"+key;squareSprites[key]=sprite;spriteShapes[sprite]=Shape(key);return sprite;}
 public static string BakedKey(string kind,int color)=>kind+"-"+ColorKeys[Mathf.Clamp(color,0,4)];
 static Sprite[] BakedFrames(string kind,int color){color=Mathf.Clamp(color,0,4);string key=BakedKey(kind,color);if(baked.TryGetValue(key,out var result))return result;var texture=Resources.Load<Texture2D>("Art/FiveBakedV80/"+(kind=="ordinary"?key:kind+"-five-colors"));if(texture==null)throw new InvalidOperationException("Missing baked five-color jelly "+key);if(kind=="ordinary")result=Slice(texture,"baked-"+key);else{var phases=new Sprite[5];float w=texture.width/5f,h=texture.height/5f;for(int row=0;row<5;row++){phases[row]=FiveColorFrame(texture,kind,color,row);phases[row].name="baked-"+key+"-phase-"+row;}result=new Sprite[16];for(int f=0;f<16;f++)result[f]=phases[f<4?0:f<7?1:f<10?2:f<13?3:4];}baked[key]=result;return result;}
 static Sprite[] Slice(Texture2D t,string label){var a=new Sprite[16];float w=t.width/4f,h=t.height/4f;for(int i=0;i<16;i++){a[i]=Sprite.Create(t,new Rect(i%4*w,(3-i/4)*h,w,h),Vector2.one*.5f,256,0,SpriteMeshType.FullRect);a[i].name=label+"-"+i;}return a;}
 public static Sprite[] OrdinaryFrames(int color)=>BakedFrames("ordinary",color);
 public static Sprite Ordinary(int color,int frame=0)=>OrdinaryFrames(color)[Mathf.Clamp(frame,0,15)];
 public static Sprite Special(int kind,int color,int frame=0)=>frame<7?Square(Key(kind),Mathf.Clamp(color,0,4)):BakedFrames(Key(kind),color)[Mathf.Clamp(frame,0,15)];
 public static Sprite SpecialIcon(int kind,int color){string key=BakedKey(Key(kind),color);if(specialIcons.TryGetValue(key,out var icon))return icon;var s=Special(kind,color);var shape=Shape(key);var r=s.rect;var crop=new Rect(r.x+(shape.centerX-shape.width*.5f)*r.width,r.y+(shape.centerY-shape.height*.5f)*r.height,r.width*shape.width,r.height*shape.height);icon=Sprite.Create(s.texture,crop,Vector2.one*.5f,256,0,SpriteMeshType.FullRect);specialIcons[key]=icon;return icon;}
 public static float FootprintFraction(string key){if(!footprintsLoaded){footprintsLoaded=true;var data=Resources.Load<TextAsset>("Art/FiveBakedV80/footprints");if(data!=null)foreach(var e in JsonUtility.FromJson<Footprints>(data.text).entries)footprints[e.key]=e.fraction;}return footprints.TryGetValue(key,out float f)?f:key.StartsWith("ordinary-")?.5f:.65f;}
 public static void FitBaked(SpriteRenderer image,float width){image.sharedMaterial=Material(image.sprite);if(phaseWidths.TryGetValue(image.sprite,out float nominalWidth)){image.transform.localPosition=Vector3.zero;image.transform.localScale=Vector3.one*(width/(nominalWidth*FootprintFraction(image.sprite.texture.name)));return;}if(spriteShapes.TryGetValue(image.sprite,out var shape)){var b=image.sprite.bounds.size;image.transform.localScale=new Vector3(width/(b.x*shape.width),width/(b.y*shape.height),1);image.transform.localPosition=new Vector3((.5f-shape.centerX)*width/shape.width,(.5f-shape.centerY)*width/shape.height,0);return;}image.transform.localPosition=Vector3.zero;image.transform.localScale=Vector3.one*(width/(image.sprite.bounds.size.x*FootprintFraction(image.sprite.texture.name)));}
 
 public static void FitOrdinary(SpriteRenderer image,float width)=>FitBaked(image,width);
 static readonly Sprite[] ordinaryIcons=new Sprite[5];
 public static Sprite OrdinaryIcon(int color){color=Mathf.Clamp(color,0,4);if(ordinaryIcons[color]!=null)return ordinaryIcons[color];var source=Ordinary(color);var r=source.rect;float p=r.width*60f/256f;return ordinaryIcons[color]=Sprite.Create(source.texture,new Rect(r.x+p,r.y+p,r.width-p*2,r.height-p*2),Vector2.one*.5f,source.pixelsPerUnit,0,SpriteMeshType.FullRect);}
 static readonly Dictionary<string,Sprite[]> frames=new Dictionary<string,Sprite[]>();
 static readonly Dictionary<Texture,Material> materials=new Dictionary<Texture,Material>(),effectMaterials=new Dictionary<Texture,Material>();
 
 public static string Key(int kind)=>kind==1?"cross":kind==2?"bomb":kind==3?"flower":kind==5?"vertical":kind==6?"horizontal":"jelly";
 public static string Name(int kind)=>kind==1?"十字激光":kind==2?"爆炸":kind==3?"五色花":kind==5?"纵向激光":"横向激光";
 public static string ColorName(int color)=>Names[Mathf.Clamp(color,0,ColorCount-1)];
 public static Sprite Frame(string key,int f){
  if(!frames.TryGetValue(key,out var a)){var t=Resources.Load<Texture2D>("Art/SevenSpecialV80/"+key);if(t==null)throw new InvalidOperationException("Missing generated seven-color artwork: "+key);a=new Sprite[16];float w=t.width/4f,h=t.height/4f;for(int i=0;i<16;i++){a[i]=Sprite.Create(t,new Rect(i%4*w,(3-i/4)*h,w,h),Vector2.one*.5f,256,0,SpriteMeshType.FullRect);a[i].name="seven80-"+key+"-"+i;}frames[key]=a;}return a[Mathf.Clamp(f,0,15)];
 }
 public static Material Material(Sprite s){if(!materials.TryGetValue(s.texture,out var m)){m=new Material(Resources.Load<Shader>("BakedJelly")){mainTexture=s.texture,name="Seven jelly "+s.texture.name};materials[s.texture]=m;}return m;}
 public static void Fit(SpriteRenderer sr,float width){sr.sharedMaterial=Material(sr.sprite);sr.transform.localScale=Vector3.one*(width/(sr.sprite.bounds.size.x*.68f));}
 public static Material EffectMaterial(Sprite sprite){if(!effectMaterials.TryGetValue(sprite.texture,out var m)){m=new Material(Resources.Load<Shader>("JellyPairFx")){mainTexture=sprite.texture};effectMaterials[sprite.texture]=m;}return m;}
 public static void EffectColors(SpriteRenderer image,int a,int b,MaterialPropertyBlock block){image.sharedMaterial=EffectMaterial(image.sprite);var r=image.sprite.rect;var t=image.sprite.texture;block.Clear();block.SetColor("_ColorA",Palette[Mathf.Clamp(a,0,4)]);block.SetColor("_ColorB",Palette[Mathf.Clamp(b,0,4)]);block.SetVector("_UvRect",new Vector4(r.x/t.width,r.y/t.height,r.width/t.width,r.height/t.height));image.SetPropertyBlock(block);}
}
}
