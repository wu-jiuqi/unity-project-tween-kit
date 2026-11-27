using UnityEngine;
using UnityEngine.Events;
namespace ProjectTweenKit {
// Composed prefabs reconstruct the original runtime-created sprites and delegates.
// They intentionally do not serialize temporary Sprite.Create or TMP font instances.
public sealed class ExamplePrefab:MonoBehaviour {
 public enum Kind {Press,Switch,Modal,Number,Float,Hit,Sequence,ScoreImpact,ScoreGain,LoadingProgress,StartupLoading,LoadingBox,ErrorPopup,GoalCelebration,ThreeStepLoop,NativeCharge,LowMoves,Toast,PageTransition}
 public Kind Example;
 public bool InitialValue=true;
 public UnityEvent Clicked=new UnityEvent();
 public UnityEvent<bool> ValueChanged=new UnityEvent<bool>();
 public MonoBehaviour Component{get;private set;}
 bool built,value;
 void Start(){Build();}
 public void Build(){if(built)return;built=true;value=InitialValue;
  switch(Example){
   case Kind.Press:Component=CSettingsArt.Button(transform,"JellyButton","开始",0,0,760,190,()=>Clicked.Invoke()).GetComponent<UIBounce>();break;
   case Kind.Switch:Component=SettingsSwitch77.Create(transform,"SettingsSwitch",0,0,()=>value,v=>{value=v;ValueChanged.Invoke(v);});break;
   case Kind.Modal:var modal=UICraft.Root(transform,"CModal",0,0,1000,1350);CSettingsArt.Image(modal,"panel_cream",0,0,1000,1350,true);var m=modal.gameObject.AddComponent<ModalMotion>();Component=m;CSettingsArt.Button(modal,"完成","完成",0,-400,720,170,()=>m.Close());m.Open(!Application.isPlaying);break;
   case Kind.Number:var n=gameObject.AddComponent<NumberRoll>();n.Target=UICraft.Label(transform,"100",0,0,800,180,100);Component=n;n.Play();break;
   case Kind.Float:var cat=UICraft.Art(transform,"cat_idle",0,0,400,400);var f=cat.gameObject.AddComponent<UIFloat>();f.amplitude=3;Component=f;break;
   case Kind.Hit:var brick=UICraft.Art(transform,"brick_mint",0,0,200,170);var h=brick.gameObject.AddComponent<JellyHit>();h.Target=brick.transform;Component=h;h.Play();break;
   case Kind.Sequence:var image=UICraft.Box(transform,"CatSequence",0,0,720,720,Color.white);var s=gameObject.AddComponent<FrameSequence>();s.Target=image;Component=s;s.Play();break;
   case Kind.ScoreImpact:{var label=UICraft.Label(transform,"12,000",0,100,900,180,120);var pulse=gameObject.AddComponent<ScoreImpact>();pulse.Target=label;Component=pulse;int score=12000;CSettingsArt.Button(transform,"Demonstrate score arrival","加分并变色放大",0,-150,760,150,()=>{score+=120;label.text=score.ToString("N0");pulse.Play();});break;}
   case Kind.ScoreGain:{var visual=UICraft.Root(transform,"Gain visual",0,100,420,80);var group=visual.gameObject.AddComponent<CanvasGroup>();group.alpha=0;group.blocksRaycasts=false;var text=UICraft.Label(visual,"",40,0,250,54,30);text.color=new Color32(175,118,27,255);foreach(var key in new[]{"gain_spark","score_star"}){var icon=UICraft.Box(visual,key,key=="gain_spark"?-133:-92,0,key=="gain_spark"?64:35,key=="gain_spark"?64:35,Color.white);icon.sprite=Resources.Load<Sprite>("Art/ScoreV65/"+key);icon.type=UnityEngine.UI.Image.Type.Simple;icon.preserveAspect=true;}var gain=gameObject.AddComponent<ScoreGain>();gain.Target=visual;gain.Text=text;gain.Group=group;Component=gain;CSettingsArt.Button(transform,"Demonstrate merged gain","增加 +120",0,-150,760,150,()=>gain.Add(120));break;}
   case Kind.LoadingProgress:{var progress=ProjectTweenKit.LoadingProgress.Create(transform);Component=progress;CSettingsArt.Button(transform,"Advance demo progress","模拟任务进度 +20%",-220,-250,410,110,()=>progress.SetProgress(progress.TargetValue+.2f));CSettingsArt.Button(transform,"Required tasks completed","任务完成",250,-250,340,110,()=>progress.Complete());break;}
   case Kind.StartupLoading:{var page=gameObject.AddComponent<StartupLoadingPresentation>();page.Build();Component=page.Progress;CSettingsArt.Button(transform,"Advance startup progress","模拟任务进度 +20%",-220,-1050,410,110,()=>page.Progress.SetProgress(page.Progress.TargetValue+.2f));CSettingsArt.Button(transform,"Startup completed","任务完成",250,-1050,340,110,()=>page.Progress.Complete());break;}

   case Kind.LoadingBox:Component=LoadingBoxAnimation.Create(transform);break;
   case Kind.ErrorPopup:var error=gameObject.AddComponent<ErrorPopupMotion>();error.Build();Component=error;CurrentKitArt.Button(error.Motion.Target,"Retry","重试",0,-465,800,128,()=>error.CloseWithIntent(()=>Clicked.Invoke()),"primary",53);error.Open();break;
   case Kind.GoalCelebration:var goal=gameObject.AddComponent<GoalCelebration>();Component=goal;goal.Begin();break;
   case Kind.ThreeStepLoop:var loop=gameObject.AddComponent<ThreeStepLoop>();var storyRoot=UICraft.Root(transform,"Three teaching steps");var story=storyRoot.gameObject.AddComponent<OpeningStory049>();story.Build(501);loop.RenderStep=(i,p)=>story.SampleStep(i,p);Component=loop;break;
   case Kind.NativeCharge:var star=CurrentKitArt.StarImage(transform,"star-empty-v22",0,0,200,200);var charge=star.gameObject.AddComponent<StarChargeView>();charge.Initialize((RectTransform)transform,.5f);Component=charge;break;
   case Kind.LowMoves:var warning=gameObject.AddComponent<MovesFeedback>();warning.Label=UICraft.Label(transform,"5",0,0,300,200,100);warning.Target=warning.Label.rectTransform;Component=warning;warning.Warn();break;
   case Kind.Toast:var toast=gameObject.AddComponent<ToastMotion>();toast.Visual=UICraft.Root(transform,"Toast visual",0,0,900,180);CSettingsArt.Image(toast.Visual,"card_cream_help",0,0,900,180,true);toast.Group=gameObject.AddComponent<CanvasGroup>();toast.Label=UICraft.Label(toast.Visual,"",0,0,850,140,45);Component=toast;toast.Show("已保存装扮");break;
   case Kind.PageTransition:var nav=gameObject.AddComponent<PageTransition>();nav.Pages=new RectTransform[2];for(int i=0;i<2;i++){nav.Pages[i]=UICraft.Root(transform,"Page "+i);UICraft.Label(nav.Pages[i],"Page "+i,0,0,600,100,60);}nav.Go(0,true);Component=nav;break;

  }
 }
 void Update(){if(Component is LoadingBoxAnimation actor)actor.Render(actor.Elapsed+KitClock.Delta);if(Component is StarChargeView charge)charge.Advance(KitClock.Delta);}
}
}
