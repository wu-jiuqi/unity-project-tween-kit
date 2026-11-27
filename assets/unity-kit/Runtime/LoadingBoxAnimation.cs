using UnityEngine;using UnityEngine.UI;
namespace ProjectTweenKit {
// Shared, unchanged page-02 choreography. Callers own the unscaled presentation clock.
public sealed class LoadingBoxAnimation:MonoBehaviour {
 public float CycleDuration=4.2f; public const float CatCycleDuration=4.2f;
 public float Elapsed{get;private set;}public string CurrentPose{get;private set;}
 public int CycleIndex=>Mathf.FloorToInt(Mathf.Max(0,Elapsed-.62f)/Mathf.Max(.01f,CycleDuration));
 
 RectTransform hero,cat,catPaws,sprout,shadow;RectTransform[] bricks=new RectTransform[5];
 CanvasGroup[] brickAlpha=new CanvasGroup[5];CanvasGroup catAlpha,pawsAlpha;Vector2[] destinations;Vector2 catDestination,pawsDestination;
 public static LoadingBoxAnimation Create(Transform parent,float y=0,float scale=1){var rt=UICraft.Root(parent,"Shared loading box actor",0,y,620,550);rt.localScale=Vector3.one*scale;var actor=rt.gameObject.AddComponent<LoadingBoxAnimation>();actor.Build();return actor;}
 void Build(){
  shadow=LevelLoadingArt.Complete(transform,"shadow");shadow.GetComponent<Image>().color=new Color(1,1,1,.25f);
  hero=UICraft.Root(transform,"Animated box assembly",0,0,620,550);
  // One complete sprite per object, unchanged throughout the animation.
  // Both box planes share their full source canvas and the exact same transform.
  LevelLoadingArt.Complete(hero,"box-rear");
  destinations=new Vector2[5];
  string[] keys={"coral","yellow","green","blue","purple"};
  for(int i=0;i<2;i++){bricks[i]=LevelLoadingArt.Complete(hero,"brick-"+keys[i]);destinations[i]=bricks[i].anchoredPosition;brickAlpha[i]=bricks[i].gameObject.AddComponent<CanvasGroup>();}
  cat=LevelLoadingArt.Complete(hero,"cat-body");catDestination=cat.anchoredPosition;catAlpha=cat.gameObject.AddComponent<CanvasGroup>();
  for(int i=2;i<5;i++){bricks[i]=LevelLoadingArt.Complete(hero,"brick-"+keys[i]);destinations[i]=bricks[i].anchoredPosition;brickAlpha[i]=bricks[i].gameObject.AddComponent<CanvasGroup>();}
  catPaws=UICraft.Root(hero,"cat-paws",0,48,240,120);pawsDestination=catPaws.anchoredPosition;pawsAlpha=catPaws.gameObject.AddComponent<CanvasGroup>();
  LevelLoadingArt.Complete(catPaws,"cat-paw-left");LevelLoadingArt.Complete(catPaws,"cat-paw-right");
  LevelLoadingArt.Complete(hero,"box-front");
  sprout=LevelLoadingArt.Complete(hero,"sprout");sprout.pivot=new Vector2(.5f,0);
 Render(0);
 }
 static float Ease(float t)=>1-Mathf.Pow(1-Mathf.Clamp01(t),3);
 public void Render(float elapsed){Elapsed=elapsed;
  if(hero==null)return;
  float drop=Mathf.Clamp01(Elapsed/.38f);float bounce=Mathf.Clamp01((Elapsed-.38f)/.24f);
  hero.anchoredPosition=new Vector2(0,430*(1-drop*drop));
  hero.localScale=Vector3.one*(1+.16f*Mathf.Sin(bounce*Mathf.PI));
  shadow.localScale=Vector3.one*Mathf.Lerp(.55f,1,drop);
  float t=Mathf.Repeat(Mathf.Max(0,Elapsed-.62f),Mathf.Max(.01f,CycleDuration));
  CurrentPose=Elapsed<.38f?"BoxDrop":Elapsed<.62f?"BoxBounce":t<1.2f?"BricksPop":t<1.65f?"CatPop":t<2.9f?"Hold":t<3.85f?"Disappear":"Empty";
  for(int i=0;i<5;i++)AnimatePart(bricks[i],brickAlpha[i],destinations[i],t,i*.18f,3.15f+i*.075f,1.18f);
  AnimatePart(cat,catAlpha,catDestination,t,1.12f,2.94f,1.15f);
  catPaws.localScale=cat.localScale;catPaws.anchoredPosition=cat.anchoredPosition+(pawsDestination-catDestination)*cat.localScale.x;pawsAlpha.alpha=catAlpha.alpha;
  float grow=Mathf.Clamp01((Elapsed-.42f)/.65f);sprout.localScale=new Vector3(Ease(grow),Mathf.Max(.001f,UICraft.OutBack(grow)),1);
  sprout.localRotation=Quaternion.Euler(0,0,Mathf.Sin(Mathf.Max(0,Elapsed-1.07f)*2.2f)*9*grow);
 }
 public bool AutoPlay;
 void Update(){if(AutoPlay)Render(Elapsed+KitClock.Delta);}
 public void StopAndRestore(){Render(0);}
 void OnDisable(){StopAndRestore();}
 void AnimatePart(RectTransform part,CanvasGroup alpha,Vector2 destination,float t,float delay,float hide,float overshoot){
  if(Elapsed<.62f||t<delay){part.localScale=Vector3.one*.001f;alpha.alpha=0;return;}
  float age=t-delay,scale;float y=0,a=1;
  if(age<.19f){float p=Ease(age/.19f);scale=Mathf.Lerp(.08f,overshoot,p);y=Mathf.Lerp(-130,18,p);}
  else if(age<.43f){float p=Mathf.SmoothStep(0,1,(age-.19f)/.24f);scale=Mathf.Lerp(overshoot,1,p);y=Mathf.Lerp(18,0,p);}
  else scale=1;
  if(t>=hide){float p=Mathf.Clamp01((t-hide)/.42f);scale=p<.18f?Mathf.Lerp(1,1.07f,p/.18f):Mathf.Lerp(1.07f,.001f,Ease((p-.18f)/.82f));y=-150*Ease(p);a=1-Mathf.SmoothStep(0,1,p);}
  part.anchoredPosition=destination+Vector2.up*y;part.localScale=Vector3.one*scale;alpha.alpha=a;
 }
}
}
