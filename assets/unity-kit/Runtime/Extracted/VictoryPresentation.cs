using System;
using UnityEngine;
using UnityEngine.UI;
namespace ProjectTweenKit {
public sealed partial class VictoryPresentation:MonoBehaviour {
 public const float ActionsAt=1.85f,ActionsDuration=.30f,IntroEnd=2.34f;
 const float RewardsAt=1.45f;
 readonly Sprite[] haloFrames=new Sprite[96],burstFrames=new Sprite[16];
 RectTransform rewards,actions;Image halo,burst,cat;CanvasGroup rewardAlpha,actionAlpha,surfaceAlpha;
 Func<RectTransform> createActions;float clock;bool initialized,running;
 public float Elapsed=>clock;
 public bool Ready{get;private set;}
 
 public RectTransform CharacterRoot{get;private set;}
 public RectTransform ContentRoot=>Surface.Content;
 public ResultSurface048 Surface{get;private set;}
 public int CatFrame{get;private set;}
 public Image Background=>halo;
 public void Begin(Func<RectTransform> actionsFactory,int completedLevel=1,bool firstReward=true,int coinReward=20,int leagueMedals=1,int score=0,int completionBonus=0,string rewardNote=""){
  createActions=actionsFactory;Surface=ResultSurface048.Create(transform,"048 victory fitted content");var body=ContentRoot;
  surfaceAlpha=body.gameObject.AddComponent<CanvasGroup>();surfaceAlpha.alpha=0;
  for(int i=0;i<96;i++)haloFrames[i]=VictoryArt.Get("halo_"+i.ToString("D2"));
  for(int i=0;i<16;i++)burstFrames[i]=VictoryArt.Get("burst_"+i.ToString("D2"));
  var clip=UICraft.Root(body,"Victory light clip",0,0,900,1320);clip.gameObject.AddComponent<RectMask2D>();
  halo=ResultSurface048.Image(clip,"Victory halo",haloFrames[0],0,30,1400,1400);
  CSettingsArt.Image(body,"title_mint_wash",0,550,550,100);
  ResultSurface048.Text(body,completedLevel==45?"花园旅程已完成":"闯关成功",0,550,850,95,completedLevel==45?58:66);
  ResultSurface048.Text(body,"第"+completedLevel+"关"+(completedLevel==45?" · 全部通关":""),0,495,800,65,34,true);
  BuildHeaderStars();
  CharacterRoot=UICraft.Root(body,"Victory character slot",0,15,608,608);
  burst=ResultSurface048.Image(CharacterRoot,"Victory burst",burstFrames[0],0,0,820,820);
  cat=ResultSurface048.Image(CharacterRoot,"Victory cat sequence",ResultsC048Art.Frame(true,0),0,0,608,608);
  rewards=UICraft.Root(body,"Victory rewards",0,-340,840,176);rewardAlpha=rewards.gameObject.AddComponent<CanvasGroup>();
  RewardTile(-205,"本关得分",score.ToString("N0"),"score-star","含步数奖励 "+completionBonus.ToString("N0"));
  RewardTile(205,"获得金币","+"+(firstReward?coinReward:0),"coin",firstReward?rewardNote:"本关已领取奖励");
  var medal=UICraft.Root(rewards,"League medal reward",0,-135,680,60);CSettingsArt.Image(medal,"drag_handle_mint",0,0,680,60,true);
  ResultSurface048.Image(medal,"Victory medal icon",ResultsC048Art.Icon("medal"),-230,0,44,44).gameObject.SetActive(leagueMedals>=0);
  ResultSurface048.Text(medal,leagueMedals<0?"第10关解锁花园联赛":"本期奖章 +"+(firstReward?leagueMedals:0),leagueMedals<0?0:25,0,540,50,29);
  gameObject.AddComponent<ResultAudioTimeline>().Initialize(true);initialized=running=true;Render();
 }
 void RewardTile(float x,string label,string value,string icon,string note){
  CSettingsArt.Image(rewards,"card_cream_tile",x,0,390,176,true);ResultSurface048.Text(rewards,label,x,55,360,45,27,true);
  ResultSurface048.Image(rewards,label+" icon",ResultsC048Art.Icon(icon),x-125,0,48,48);ResultSurface048.Text(rewards,value,x+30,0,260,64,46);
  if(!string.IsNullOrEmpty(note))ResultSurface048.Text(rewards,note,x,-58,345,45,20,true);
 }
 void Update(){if(!running)return;clock+=KitClock.Delta;if(!FeedbackProfile.Active.enabledFeedback)clock=Mathf.Max(clock,IntroEnd);Render();}
 void Render(){
  bool motion=FeedbackProfile.Active.enabledFeedback;CatFrame=motion?ResultsC048Art.FrameAt(true,clock-.08f):7;cat.sprite=ResultsC048Art.Frame(true,CatFrame);surfaceAlpha.alpha=Mathf.Clamp01(clock/.28f);
  RenderHeaderStars(motion);
  float age=motion?Mathf.Max(0,clock-.35f)*8:0;int i=Mathf.FloorToInt(age);halo.sprite=haloFrames[i%96];halo.rectTransform.localRotation=Quaternion.Euler(0,0,-(age-i)*3.75f);halo.color=new Color(1,1,1,.32f*(motion?Mathf.Clamp01((clock-.15f)/.45f):1));
  float b=clock-1;burst.enabled=motion&&b>=0&&b<16f/12;if(burst.enabled)burst.sprite=burstFrames[Mathf.Min(15,Mathf.FloorToInt(b*12))];
  float r=Mathf.Clamp01((clock-RewardsAt)/.3f);rewards.anchoredPosition=new Vector2(0,-340-(1-r)*28);rewards.localScale=Vector3.one*(.96f+.04f*UICraft.OutBack(r));rewardAlpha.alpha=r;
  if(clock>=ActionsAt&&actions==null){actions=createActions?.Invoke();if(actions!=null)actionAlpha=actions.gameObject.AddComponent<CanvasGroup>();}
  Ready=false;if(actions!=null){float p=Mathf.Clamp01((clock-ActionsAt)/ActionsDuration);actions.anchoredPosition=Vector2.down*((1-p)*28);actions.localScale=Vector3.one*(.96f+.04f*UICraft.OutBack(p));actionAlpha.alpha=p;Ready=p>=.999f;actionAlpha.interactable=actionAlpha.blocksRaycasts=Ready;}
 }
 public void Finish(){if(!initialized)return;GetComponent<ResultAudioTimeline>()?.Skip();clock=Mathf.Max(clock,IntroEnd);Render();}
 void OnEnable(){if(initialized){running=true;Render();}}
 void OnDisable(){running=false;Ready=false;if(actionAlpha!=null)actionAlpha.interactable=actionAlpha.blocksRaycasts=false;}
}
}
