using System;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
public sealed class FailurePresentation:MonoBehaviour {
 readonly Sprite[] frames=new Sprite[96];Image background,cat;RectTransform actions;CanvasGroup actionAlpha;float clock;bool firstTick=true;
 public int CatFrame{get;private set;}
 public bool Ready=>clock>=1.35f;
 
 public ResultSurface048 Surface{get;private set;}
 
 public RectTransform Character=>cat.rectTransform;
 public void Begin(Action retry,Action home,Action watch,bool canContinue,int level=1,string homeCaption="返回花园"){
  gameObject.AddComponent<ResultAudioTimeline>().Initialize(false);Surface=ResultSurface048.Create(transform,"048 failure fitted content");var body=Surface.Content;
  for(int i=0;i<96;i++)frames[i]=FailureArt.Get("background_"+i.ToString("D3"));
  var clip=UICraft.Root(body,"Failure cloud clip",0,0,900,1320);clip.gameObject.AddComponent<RectMask2D>();
  background=ResultSurface048.Image(clip,"Failure looping clouds",frames[0],0,110,1080,1080);background.color=new Color(1,1,1,.65f);
  CSettingsArt.Image(body,"title_mint_wash",0,550,550,100);
  ResultSurface048.Text(body,"再试一次吧",0,550,850,95,66);ResultSurface048.Text(body,"第"+level+"关",0,495,800,65,34,true);
  cat=ResultSurface048.Image(body,"Failure cat sequence",ResultsC048Art.Frame(false,0),0,80,640,640);
  cat.rectTransform.pivot=new Vector2(.5f,42f/512);cat.rectTransform.anchoredPosition=new Vector2(0,80-(470f/512-.5f)*640);
  ResultSurface048.Text(body,"就差一点点",0,-275,850,80,47);ResultSurface048.Text(body,canContinue?"保留棋盘，增加5次机会":"再试一次，就有新希望",0,-345,850,65,32,true);
  if(canContinue)ResultSurface048.Text(body,"每关限续关一次",0,-400,850,50,25,true);
  actions=UICraft.Root(body,"Failure actions",0,0,1080,1920);actionAlpha=actions.gameObject.AddComponent<CanvasGroup>();
  if(canContinue)CurrentKitArt.Button(actions,"Button Watch ad continue","看广告继续 · +5次",0,-475,820,112,watch,"primary",43);
  CurrentKitArt.Button(actions,"Button Try again","重新开始",0,canContinue?-600:-550,820,100,retry,canContinue?"secondary":"primary",49);
  CSettingsArt.Button(actions,"Button Home",homeCaption,0,canContinue?-700:-670,560,60,home,null,31);Render();
 }
 void Update(){if(firstTick)firstTick=false;else clock+=Mathf.Min(KitClock.Delta,.1f);if(!FeedbackProfile.Active.enabledFeedback)clock=Mathf.Max(clock,2.32f);Render();}
 void Render(){
  bool motion=FeedbackProfile.Active.enabledFeedback;background.sprite=frames[motion?Mathf.FloorToInt(clock*24)%96:0];CatFrame=motion?ResultsC048Art.FrameAt(false,clock):7;cat.sprite=ResultsC048Art.Frame(false,CatFrame);
  float breath=motion&&clock>2.32f?.003f*(1-Mathf.Cos((clock-2.32f)*Mathf.PI)):0;cat.rectTransform.localScale=new Vector3(1,1+breath,1);
  float a=Mathf.Clamp01((clock-1.05f)/.3f);actions.anchoredPosition=Vector2.down*(1-UICraft.OutBack(a))*30;actionAlpha.alpha=a;actionAlpha.interactable=actionAlpha.blocksRaycasts=Ready;
 }
 void OnDisable(){if(actionAlpha!=null)actionAlpha.interactable=actionAlpha.blocksRaycasts=false;}
}
}
