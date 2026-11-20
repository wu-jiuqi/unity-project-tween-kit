using UnityEngine;
using UnityEngine.UI;
using ProjectTweenKit;
namespace TweenGallery {
public sealed partial class Gallery {
 void BuildSupplemental(RectTransform root){switch(Current.id){
  case "loadingprogress":case "startuploading":{
   LoadingProgress progress;StartupLoadingPresentation page=null;
   if(Current.id=="startuploading"){page=root.gameObject.AddComponent<StartupLoadingPresentation>();page.Build();progress=page.Progress;}
   else {progress=LoadingProgress.Create(root,0,70);UICraft.Label(root,"真实进度输入 · 手动模拟任务事件",0,270,1000,80,42);}
   component=progress;primaryAction=()=>{progress.SetProgress(progress.TargetValue+.2f);Report("已输入下一批模拟任务进度；全部完成前最多显示 99%。");};
   float y=page!=null?-1000:-230;float h=page!=null?80:90;
   CSettingsArt.Button(root,"Required tasks completed","任务完成",-325,y,300,h,()=>progress.Complete());
   var retry=CSettingsArt.Button(root,"Fail or retry","模拟中断",0,y,300,h,()=>{if(progress.Failed){progress.Resume();page?.Resume();}else{progress.Failure();page?.Failure("资源准备中断，可重试");}Report(progress.Failed?"进度已保持，点击中间按钮恢复。":"已恢复，继续等待宿主输入进度。");});
   retry.onClick.AddListener(()=>retry.GetComponentInChildren<TMPro.TMP_Text>().text=progress.Failed?"恢复加载":"模拟中断");
   CSettingsArt.Button(root,"Reset loading example","重置进度",325,y,300,h,()=>{progress.ResetProgress();page?.Resume();retry.GetComponentInChildren<TMPro.TMP_Text>().text="模拟中断";});break;
  }
  case "pagenav":{
   var view=UICraft.Root(root,"Page viewport",0,0,920,520);view.gameObject.AddComponent<RectMask2D>();var track=UICraft.Root(view,"Page track",0,0,920,520);
   for(int i=0;i<2;i++){var page=UICraft.Root(track,"Page "+i,i*920,0,900,500);CSettingsArt.Image(page,"card_cream_help",0,0,900,500,true);CSettingsArt.Image(page,"decor_cat_head",0,70,220,150);UICraft.Label(page,i==0?"花园首页":"花园排行",0,-100,800,90,65);}
   var c=root.gameObject.AddComponent<PageSlide>();c.Target=track;c.Go(Vector2.zero,true);component=c;bool second=false;primaryAction=()=>{second=!second;c.Go(new Vector2(second?-920:0,0),immediate);};break;
  }
  case "stagger":{
   var c=root.gameObject.AddComponent<GroupStagger>();c.Targets=new RectTransform[3];for(int i=0;i<3;i++){var card=UICraft.Root(root,"Product visual "+i,(i-1)*300,0,270,380);CSettingsArt.Image(card,"card_cream_help",0,0,270,380,true);Art(card,"icon_medal",0,40,130,130);UICraft.Label(card,new[]{"道具","头像","装扮"}[i],0,-110,240,70,40);c.Targets[i]=card;}component=c;primaryAction=c.Play;break;
  }
  case "rankmove":{
   var c=root.gameObject.AddComponent<RankReorder>();c.Rows=new RectTransform[4];c.Stride=135;for(int i=0;i<4;i++){var row=UICraft.Root(root,"Stable row "+i,0,(1.5f-i)*135,880,112);CSettingsArt.Image(row,"card_cream_help",0,0,880,112,true);Art(row,"icon_medal",-350,0,65,65);UICraft.Label(row,new[]{"花园伙伴 A","我 · 分数已由宿主更新","花园伙伴 C","花园伙伴 D"}[i],55,0,650,80,38);c.Rows[i]=row;}component=c;bool updated=false;primaryAction=()=>{updated=!updated;c.Reorder(updated?new[]{1,0,3,2}:new[]{0,1,2,3});};break;
  }
  case "guide":{
   var c=root.gameObject.AddComponent<TutorialGesture>();var layer=UICraft.Root(root,"Noninteractive guide");c.Group=layer.gameObject.AddComponent<CanvasGroup>();c.Group.blocksRaycasts=false;c.Group.interactable=false;c.Hand=OnboardingArt.Draw(layer,"Drag glove",2,1,0,0,210,210);c.Ring=OnboardingArt.Draw(layer,"Touch ripple",2,5,0,0,120,120);c.Outline=TutorialArt.Picture(layer,"Target outline","target-outline",Vector2.zero,new Vector2(120,120)).rectTransform;component=c;bool active=false;primaryAction=()=>{active=!active;c.SetInputActive(active);Report(active?"模拟玩家正在操作，教学手势已隐藏。":"玩家停止操作，教学手势恢复。");};break;
  }
  case "hintfade":{
   var visual=UICraft.Root(root,"Quiet instruction",0,0,950,150);ExtraArt(visual,"BattleUIV42/swipe-gesture",-310,0,70,85);UICraft.Label(visual,"拖动瞄准，松手发射",70,0,700,100,55);var c=root.gameObject.AddComponent<HintFade>();c.Group=visual.gameObject.AddComponent<CanvasGroup>();component=c;primaryAction=()=>c.Visible=!c.Visible;break;
  }
  case "homefeedback":{
   var c=root.gameObject.AddComponent<HomeLayeredFeedback>();c.Title=UICraft.Label(root,"状态越来越好！",0,15,950,100,78).rectTransform;c.Detail=UICraft.Label(root,"已经连续赢了 2 局。",0,-82,900,76,48).rectTransform;c.Decoration=Art(root,"icon_medal",0,150,96,96).rectTransform;component=c;primaryAction=c.Play;break;
  }
  case "pendulum":{
   var hinge=UICraft.Root(root,"Visual star hinge",0,220,2,2);hinge.localScale=Vector3.one*3;BattleJellyArt.Image(hinge,"vine-star-junction",0,0,24,16);BattleJellyArt.Image(hinge,"vine-star-stem",0,-36,18,72);BattleJellyArt.Image(hinge,"vine-star-on",0,-102,80,80);var c=root.gameObject.AddComponent<PendantSwing>();c.Target=hinge;component=c;float side=1;primaryAction=()=>{c.Impulse(side);side=-side;};break;
  }
  case "breath":{
   var icon=LeagueMotionArt.Image(root,"season",0,0,360);var c=root.gameObject.AddComponent<DecorativeBreath>();c.Target=icon.transform;component=c;break;
  }
  default:throw new System.InvalidOperationException("No example for "+Current.id);
 }}
}
}
