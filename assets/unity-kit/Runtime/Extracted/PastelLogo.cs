using System;
using UnityEngine;
namespace ProjectTweenKit {
public static class PastelLogo {
 [Serializable]sealed class Glyph {public string key,character;public int[] size,visible;}
 [Serializable]sealed class Layout {public float gap;public Glyph[] glyphs;}
 static Layout layout;static readonly Sprite[] glyphs=new Sprite[4];
 public static RectTransform Create(Transform parent,float x,float y,float width,float height){
  if(layout==null)layout=JsonUtility.FromJson<Layout>(Resources.Load<TextAsset>("Art/ChineseV60/Logo/layout").text);
  const float designHeight=1000;float total=layout.gap*3;foreach(var g in layout.glyphs)total+=g.visible[2]*designHeight/g.visible[3];
  float fit=Mathf.Min(width/total,height/designHeight);var root=UICraft.Root(parent,"Pastel layered logo",x,y,total*fit,designHeight*fit);float cursor=-total*.5f;
  for(int i=0;i<4;i++){
   var g=layout.glyphs[i];if(glyphs[i]==null){var t=Resources.Load<Texture2D>("Art/ChineseV60/Logo/"+g.key);if(t==null)throw new InvalidOperationException("Missing independently generated logo glyph "+g.key);glyphs[i]=Sprite.Create(t,new Rect(0,0,t.width,t.height),Vector2.one*.5f,100,0,SpriteMeshType.FullRect);}
   float scale=designHeight/g.visible[3],visibleWidth=g.visible[2]*scale;
   var image=ChineseArt.Image(root,g.character,glyphs[i],(cursor+visibleWidth*.5f)*fit,0,g.size[0]*scale*fit,g.size[1]*scale*fit);
   image.preserveAspect=false;image.rectTransform.pivot=new Vector2((g.visible[0]+g.visible[2]*.5f)/g.size[0],1-(g.visible[1]+g.visible[3]*.5f)/g.size[1]);
   cursor+=visibleWidth+layout.gap;
  }
  return root;
 }
}
}
