using TMPro;
using UnityEngine;
namespace ProjectTweenKit {
public static class GameTypography {
 static TMP_FontAsset font,heavy;static Material outline,heading;
 public static TMP_FontAsset HeadingFont {get{if(heavy==null){heavy=TMP_FontAsset.CreateFontAsset(Resources.Load<Font>("Fonts/MaokenZhuyuanTi"),48,5,UnityEngine.TextCore.LowLevel.GlyphRenderMode.SDFAA,1024,1024);heavy.name="Maoken Zhuyuan Ti Heading SDF";heavy.atlasPopulationMode=AtlasPopulationMode.Dynamic;heavy.isMultiAtlasTexturesEnabled=true;heavy.fallbackFontAssetTable=new System.Collections.Generic.List<TMP_FontAsset>{Font};}return heavy;}}
 public static Material Outline {get{if(outline==null){outline=new Material(Font.material);outline.EnableKeyword("OUTLINE_ON");outline.SetFloat(ShaderUtilities.ID_OutlineWidth,0f);outline.SetColor(ShaderUtilities.ID_OutlineColor,new Color(.18f,.12f,.09f,1));}return outline;}}
 public static Material Heading {get{if(heading==null){heading=new Material(Font.material);heading.EnableKeyword("OUTLINE_ON");heading.SetFloat(ShaderUtilities.ID_OutlineWidth,0f);heading.SetColor(ShaderUtilities.ID_OutlineColor,new Color(.58f,.035f,.18f,1));}return heading;}}

 public static TMP_FontAsset Font {get{if(font==null){
  font=TMP_FontAsset.CreateFontAsset(UICraft.Font,48,5,UnityEngine.TextCore.LowLevel.GlyphRenderMode.SDFAA,2048,2048);
  font.name="Maoken Zhuyuan Ti UI SDF";font.atlasPopulationMode=AtlasPopulationMode.Dynamic;font.isMultiAtlasTexturesEnabled=true;
  // Keep the supplied typeface for every supported glyph; use the prior face only for missing symbols/spaces.
  var symbols=TMP_FontAsset.CreateFontAsset(Resources.Load<Font>("Fonts/MiaoRoundedUI-Bold"),48,5,UnityEngine.TextCore.LowLevel.GlyphRenderMode.SDFAA,512,512);
  symbols.name="UI missing-symbol fallback";symbols.atlasPopulationMode=AtlasPopulationMode.Dynamic;symbols.isMultiAtlasTexturesEnabled=true;
  font.fallbackFontAssetTable=new System.Collections.Generic.List<TMP_FontAsset>{symbols};
  // Only the small first-screen set is prepared at startup; other glyphs are added on demand.
  font.TryAddCharacters("喵弹拆箱花园联赛喵喵小铺每日挑战排行榜逛猫咪头像与框第关解锁轻松闯一起成长天都有美好声音开设置完成欢迎来到见你真心正在准备继续返回收集目标剩余步数拖动瞄准手发射0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz !?.,:;+-=*/_#()[]'%，。！？：、·—…（）");
 }return font;}}
}
}
