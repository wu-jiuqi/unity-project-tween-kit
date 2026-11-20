using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
// ChainRatingView.Update: tiered word-art pop and golden-angle spark distribution.
public sealed class CheerBurst:MonoBehaviour {
 public Image Art;public CanvasGroup Group;public RectTransform[] Sparks;public Image[] SparkImages;
 public float Duration=1.8f;public int Tier;
 float age;bool running;
 public void Play(){age=0;running=true;Art.sprite=CheerSprite(Tier);Art.rectTransform.sizeDelta=new Vector2(420+Mathf.Clamp(Tier,0,3)*40,160);Sample(0);}
 public static Sprite CheerSprite(int tier){string key=new[]{"nice","great","amazing","incredible"}[Mathf.Clamp(tier,0,3)];var texture=Resources.Load<Texture2D>("Art/ChineseV59/Cheers/"+key);if(!cache.TryGetValue(key,out var sprite)){sprite=Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),Vector2.one*.5f,100,0,SpriteMeshType.FullRect);cache[key]=sprite;}return sprite;}
 static readonly System.Collections.Generic.Dictionary<string,Sprite> cache=new System.Collections.Generic.Dictionary<string,Sprite>();
 public void Sample(float seconds){int tier=Mathf.Clamp(Tier,0,3);float t=Mathf.Clamp01(seconds/Mathf.Max(.001f,Duration));float pop=t<.12f?Mathf.Lerp(.45f,1.12f,t/.12f):Mathf.Lerp(1.12f,1,Mathf.Clamp01((t-.12f)/.17f));Art.transform.localScale=Vector3.one*pop;Group.alpha=Mathf.Min(Mathf.Clamp01(t/.06f),Mathf.Clamp01((1-t)/.22f));for(int i=0;i<Sparks.Length;i++){bool visible=i<4+tier*3;Sparks[i].gameObject.SetActive(visible);if(!visible)continue;float angle=i*2.399f,distance=(110+tier*14)*Mathf.Clamp01(t/.7f);Sparks[i].anchoredPosition=new Vector2(Mathf.Cos(angle)*(Art.rectTransform.rect.width*.43f+distance*.35f),Mathf.Sin(angle)*(Art.rectTransform.rect.height*.43f+distance*.22f));Sparks[i].localScale=Vector3.one*Mathf.Lerp(1,.3f,t);SparkImages[i].color=new Color(1,1,1,Mathf.Clamp01((1-t)/.45f));}}
 void Update(){if(!running)return;age+=KitClock.ScaledDelta;Sample(age);if(age>=Duration)running=false;}
 public void StopAndRestore(){running=false;if(Group!=null)Group.alpha=0;if(Art!=null)Art.transform.localScale=Vector3.one;}
 void OnDisable(){StopAndRestore();}
}
}
