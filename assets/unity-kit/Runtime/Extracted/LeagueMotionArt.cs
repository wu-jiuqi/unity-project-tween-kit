using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
public static class LeagueMotionArt {
 static readonly Dictionary<string,Sprite> cache=new Dictionary<string,Sprite>();
 public static Sprite Get(string key){if(cache.TryGetValue(key,out var s))return s;var t=Resources.Load<Texture2D>("Art/LeagueCalendarV1/league-motion-atlas");if(t==null)throw new InvalidOperationException("League motion atlas missing");int x=key=="up"||key=="season"?1:0,y=key=="medal"||key=="up"?1:0;s=Sprite.Create(t,new Rect(x*t.width/2,y*t.height/2,t.width/2,t.height/2),Vector2.one*.5f,100,0,SpriteMeshType.FullRect);cache.Add(key,s);return s;}
 public static Image Image(Transform parent,string key,float x,float y,float size){var v=UICraft.Box(parent,"League generated "+key,x,y,size,size,Color.white);v.sprite=Get(key);v.type=UnityEngine.UI.Image.Type.Simple;v.preserveAspect=true;v.raycastTarget=false;return v;}
}
public sealed class LeagueMedalPresentation:MonoBehaviour {
 RectTransform medal,arrow;CanvasGroup group;float age;bool moving;int delta;
 public static void Show(RectTransform parent,int before,int after,int medals){Transform target=parent;foreach(var t in parent.GetComponentsInChildren<RectTransform>())if(t.name=="Victory rewards"){target=t;break;}var root=UICraft.Root(target,"League medal and rank feedback",0,0,832,270);root.gameObject.AddComponent<LeagueMedalPresentation>().Build(before,after,medals);}
 void Build(int before,int after,int medals){delta=before-after;moving=FeedbackProfile.Active.enabledFeedback;group=gameObject.AddComponent<CanvasGroup>();group.blocksRaycasts=false;
  var old=transform.parent.Find("Victory medal icon");if(old!=null)old.gameObject.SetActive(false);
  medal=LeagueMotionArt.Image(transform,"medal",130,-35,130).rectTransform;
  
  UICraft.Label(transform,delta>0?"升至第"+after+"名":delta<0?"暂列第"+after+"名":"本期第"+after+"名",0,-111,720,44,26,UICraft.Ink);
  if(delta!=0){arrow=LeagueMotionArt.Image(transform,delta>0?"up":"down",310,-110,48).rectTransform;UICraft.Label(transform,Math.Abs(delta).ToString(),365,-110,55,44,25,UICraft.Ink);}
  SoundService.Play(delta>0?"rank_up":"medal_arrive");
 }
 void Update(){age+=Mathf.Min(KitClock.Delta,.05f);group.alpha=Mathf.Clamp01(age/.15f);float aspect=Mathf.Abs(transform.lossyScale.x/Mathf.Max(.001f,transform.lossyScale.y));if(arrow!=null)arrow.localScale=new Vector3(1,aspect,1);if(!moving){medal.localScale=new Vector3(1,aspect,1);return;}
  float enter=Mathf.Clamp01(age/.55f);medal.anchoredPosition=new Vector2(Mathf.Lerp(0,130,enter*enter),-35+Mathf.Sin(enter*Mathf.PI)*100);float size=Mathf.Lerp(1.8f,1,enter);medal.localScale=new Vector3(size,size*aspect,1);
  if(arrow!=null)arrow.anchoredPosition=new Vector2(310,-110+(delta>0?1:-1)*Mathf.Sin(Mathf.Clamp01((age-.3f)/.65f)*Mathf.PI)*45);
 }
}
}
