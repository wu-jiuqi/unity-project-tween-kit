using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
// One shared component atlas for every C-style settings surface.
public static class CSettingsArt {
 [Serializable] sealed class Catalog {public Entry[] sprites;}
 [Serializable] sealed class Entry {public string name,texture;public int[] rectUnityBottomLeft,borderUnityLTRB;}
 static Catalog catalog;static readonly Dictionary<string,Sprite> sprites=new Dictionary<string,Sprite>();
 public static readonly Color Ink=new Color32(127,83,49,255),Muted=new Color32(164,128,89,255);
 public static Sprite Get(string key){
  if(sprites.TryGetValue(key,out var found))return found;
  if(catalog==null){var json=Resources.Load<TextAsset>("Art/SettingsC/atlas");if(json==null)throw new InvalidOperationException("Missing C settings atlas catalog");catalog=JsonUtility.FromJson<Catalog>(json.text);}
  foreach(var e in catalog.sprites){if(e.name!=key)continue;var texture=Resources.Load<Texture2D>("Art/SettingsC/"+System.IO.Path.GetFileNameWithoutExtension(e.texture));if(texture==null)throw new InvalidOperationException("Missing C settings texture: "+e.texture);
   var r=e.rectUnityBottomLeft;var b=e.borderUnityLTRB;found=Sprite.Create(texture,new Rect(r[0],r[1],r[2],r[3]),Vector2.one*.5f,100,0,SpriteMeshType.FullRect,new Vector4(b[0],b[1],b[2],b[3]));found.name="Settings C / "+key;sprites[key]=found;return found;
  }throw new InvalidOperationException("Missing C settings component: "+key);
 }
 public static Image Image(Transform parent,string key,float x,float y,float w,float h,bool sliced=false){
  var image=UICraft.Box(parent,key,x,y,w,h,Color.white);image.sprite=Get(key);image.raycastTarget=false;image.type=sliced?UnityEngine.UI.Image.Type.Sliced:UnityEngine.UI.Image.Type.Simple;image.preserveAspect=!sliced;
  if(sliced)image.pixelsPerUnitMultiplier=key.StartsWith("button_")?image.sprite.rect.height/h:image.sprite.rect.width/w;
  return image;
 }
 public static Button Button(Transform parent,string name,string caption,float x,float y,float w,float h,Action action,string art="button_peach_normal",int font=49){
  var root=UICraft.Root(parent,name,x,y,w,h);var hit=root.gameObject.AddComponent<Image>();hit.color=Color.clear;hit.raycastTarget=true;
  var visual=UICraft.Root(root,"ButtonVisual",0,0,w,h);Image face=null;
  if(!string.IsNullOrEmpty(art))face=Image(visual,art,0,0,w,h,art!="button_round_cream");
  if(!string.IsNullOrEmpty(caption)){var t=UICraft.Label(visual,caption,0,0,w*.84f,h*.65f,font,Ink);t.color=Ink;t.fontSizeMin=font*.75f;t.fontSizeMax=font;}
  var button=root.gameObject.AddComponent<Button>();button.targetGraphic=face!=null?face:hit;
  if(art=="button_peach_normal"||art=="button_cream_normal"){button.transition=Selectable.Transition.SpriteSwap;button.spriteState=new SpriteState{pressedSprite=Get(art.Replace("_normal","_pressed"))};}else button.transition=Selectable.Transition.None;
  button.onClick.AddListener(()=>{SoundService.Play("click");action?.Invoke();});root.gameObject.AddComponent<UIBounce>().Target=visual;return button;
 }
}
}
