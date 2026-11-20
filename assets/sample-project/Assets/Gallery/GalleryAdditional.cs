using UnityEngine;
using UnityEngine.UI;
using ProjectTweenKit;
namespace TweenGallery {
public sealed partial class Gallery {
 static readonly System.Collections.Generic.Dictionary<string,Sprite> extraSprites=new System.Collections.Generic.Dictionary<string,Sprite>();
 static Sprite ExtraSprite(string key){if(extraSprites.TryGetValue(key,out var sprite))return sprite;sprite=Resources.Load<Sprite>("Art/"+key);if(sprite==null){var t=Resources.Load<Texture2D>("Art/"+key);if(t==null)throw new System.InvalidOperationException("Missing example artwork "+key);sprite=Sprite.Create(t,new Rect(0,0,t.width,t.height),Vector2.one*.5f,100,0,SpriteMeshType.FullRect);}extraSprites[key]=sprite;return sprite;}
 Image ExtraArt(Transform root,string key,float x,float y,float w,float h){var image=UICraft.Box(root,key,x,y,w,h,Color.white);image.type=Image.Type.Simple;image.sprite=ExtraSprite(key);image.preserveAspect=true;image.raycastTarget=false;return image;}
 void BuildAdditional(RectTransform root){switch(Current.id){
  case "scoreimpact":{
   UICraft.Label(root,"当前分数",0,180,800,80,48);var text=UICraft.Label(root,"12,000",0,0,950,200,145,CSettingsArt.Ink);
   var c=root.gameObject.AddComponent<ScoreImpact>();c.Target=text;component=c;int value=12000;
   primaryAction=()=>{value+=120;text.text=value.ToString("N0");c.Play(immediate);};play=c.Play;sample=c.Sample;break;
  }
  case "scoregain":{
   UICraft.Label(root,"连续点击：合并短时间内的加分",0,210,950,80,45);var visual=UICraft.Root(root,"Gain visual",0,0,420,80);var group=visual.gameObject.AddComponent<CanvasGroup>();group.alpha=0;group.blocksRaycasts=false;
   ExtraArt(visual,"ScoreV65/gain_spark",-133,0,64,64);ExtraArt(visual,"ScoreV65/score_star",-92,0,35,35);var text=UICraft.Label(visual,"",40,0,250,54,30);text.color=new Color32(175,118,27,255);
   visual.localScale=Vector3.one*2;var c=root.gameObject.AddComponent<ScoreGain>();c.Target=visual;c.Group=group;c.Text=text;component=c;primaryAction=()=>c.Add(120);break;
  }
  case "taskarrival":{
   var card=UICraft.Root(root,"Task visual",0,0,196,164);card.localScale=Vector3.one*2.5f;var pad=BattleJellyArt.Image(card,"goal-pad-honey",0,0,204,164);var icon=UICraft.Box(card,"Copper target",0,21,96,86,Color.white);icon.sprite=OpeningCopperArt.State(25);icon.type=Image.Type.Simple;icon.preserveAspect=true;
   var count=UICraft.Label(card,"3",0,-35,176,52,46);var stamp=BattleJellyArt.Image(card,"goal-complete",0,-35,42,42);stamp.gameObject.SetActive(false);
   var c=root.gameObject.AddComponent<TaskArrival>();c.Root=card;c.Pad=pad.rectTransform;c.Icon=icon.rectTransform;c.Count=count;c.Stamp=stamp.rectTransform;component=c;int remaining=3;
   primaryAction=()=>{if(remaining==0)remaining=3;remaining--;count.text=remaining.ToString();c.Arrive(remaining==0);};break;
  }
  case "moves":{
   var visual=UICraft.Root(root,"Moves visual",0,0,170,128);visual.localScale=Vector3.one*3;BattleJellyArt.Image(visual,"moves-badge",0,0,170,128);UICraft.Label(visual,"剩余步数",0,35,160,34,26);var number=UICraft.Label(visual,"12",0,-13,150,76,64);var c=root.gameObject.AddComponent<MovesFeedback>();c.Target=visual;component=c;int remaining=12;primaryAction=()=>{remaining=remaining<=0?12:remaining-1;number.text=remaining.ToString();c.Play();};break;
  }
  case "rating":{
   var visual=UICraft.Root(root,"Rating",0,0,600,190);visual.localScale=Vector3.one*1.5f;var group=visual.gameObject.AddComponent<CanvasGroup>();group.alpha=0;group.blocksRaycasts=false;var image=UICraft.Box(visual,"Rating art",0,0,540,160,Color.white);image.type=Image.Type.Simple;image.preserveAspect=true;image.sprite=CheerBurst.CheerSprite(0);
   var c=root.gameObject.AddComponent<CheerBurst>();c.Art=image;c.Group=group;c.Sparks=new RectTransform[13];c.SparkImages=new Image[13];for(int i=0;i<13;i++){var spark=Art(visual,"fx_spark",0,0,30,30);c.Sparks[i]=spark.rectTransform;c.SparkImages[i]=spark;}component=c;primaryAction=c.Play;break;
  }
  case "leaguejelly":{
   var visual=UICraft.Root(root,"Rank visual",0,0,900,230);CSettingsArt.Image(visual,"card_cream_help",0,0,900,230,true);Art(visual,"icon_medal",-340,0,110,110);UICraft.Label(visual,"第 8 名    ·    积分 +120",65,0,680,120,60);var c=visual.gameObject.AddComponent<LeagueJelly>();component=c;primaryAction=()=>c.Play();break;
  }
  case "themedissolve":{
   var next=ExtraArt(root,"NativeV50/background-picnic",0,0,853,1844);var previous=ExtraArt(root,"NativeV50/background-alphabet",0,0,853,1843);var group=previous.gameObject.AddComponent<CanvasGroup>();group.blocksRaycasts=false;var c=root.gameObject.AddComponent<ThemeDissolve>();c.Previous=group;component=c;bool picnic=true;
   primaryAction=()=>{next.sprite=ExtraSprite("NativeV50/background-"+(picnic?"picnic":"alphabet"));previous.sprite=ExtraSprite("NativeV50/background-"+(picnic?"alphabet":"picnic"));picnic=!picnic;c.Play(immediate);};play=c.Play;sample=c.Sample;break;
  }
  default:throw new System.InvalidOperationException("No demonstration for "+Current.id);
 }}
}
}
