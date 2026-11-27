using UnityEngine;
using UnityEngine.UI;
using ProjectTweenKit;
namespace TweenGallery {
public sealed partial class Gallery {
 void BuildCurrent(RectTransform root){switch(Current.id){
  case "toast":{
   var c=root.gameObject.AddComponent<ToastMotion>();c.Visual=UICraft.Root(root,"Toast visual",0,0,900,180);c.Group=root.gameObject.AddComponent<CanvasGroup>();c.Group.alpha=0;
   var panel=UICraft.Box(c.Visual,"Toast panel",0,0,900,180,Color.white);var texture=Resources.Load<Texture2D>("Art/Toasts0410/toast-panel");panel.sprite=Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),Vector2.one*.5f,100,0,SpriteMeshType.FullRect,new Vector4(220,150,220,150));panel.type=Image.Type.Sliced;
   c.Label=UICraft.Label(c.Visual,"",0,0,760,130,44,CSettingsArt.Ink);component=c;int n=0;primaryAction=()=>c.Show(++n%2==0?"已保存装扮":"资源准备中，请稍候");CSettingsArt.Button(root,"Repeat toast","同文案续时",0,-240,600,110,()=>c.Show(c.Message??"已保存装扮"));break;
  }
  case "loadingbox":{var actor=LoadingBoxAnimation.Create(root);component=actor;break;}
  case "errorpopup":{
   var c=root.gameObject.AddComponent<ErrorPopupMotion>();c.Build();component=c;primaryAction=c.Open;
   CurrentKitArt.Button(c.Motion.Target,"Retry request","重试",0,-465,800,128,()=>c.CloseWithIntent(()=>Report("重试意图已交给宿主。")),"primary",53);
   CurrentKitArt.Button(c.Motion.Target,"Return request","返回",0,-620,800,120,()=>c.CloseWithIntent(()=>Report("返回意图已交给宿主。")),"secondary",49);c.Open();break;
  }
  case "pageflow":{
   var viewport=UICraft.Root(root,"Five-page viewport",0,70,920,510);viewport.gameObject.AddComponent<RectMask2D>();var c=root.gameObject.AddComponent<PageTransition>();c.Pages=new RectTransform[5];
   for(int i=0;i<5;i++){var p=UICraft.Root(viewport,"Page "+i,0,0,900,500);CSettingsArt.Image(p,"card_cream_help",0,0,900,500,true);UICraft.Label(p,new[]{"地图","小铺","首页","联赛","装扮"}[i],0,0,800,100,65);c.Pages[i]=p;}
   component=c;int page=0;primaryAction=()=>c.Go(page=(page+3)%5,immediate);
   for(int i=0;i<5;i++){int destination=i;CSettingsArt.Button(root,"Destination "+i,new[]{"地图","小铺","首页","联赛","装扮"}[i],(i-2)*185,-270,170,85,()=>{c.Go(destination,immediate);});}
   int pending=0;CSettingsArt.Button(root,"Wait resources","等待联赛资源",-290,-400,520,85,()=>{pending=c.Request(3);Report("仍显示原页；可切换目的地取消等待。");});CSettingsArt.Button(root,"Resolve resources","资源就绪",290,-400,520,85,()=>Report(c.Resolve(pending,true)?"准备完成，开始切换。":"旧请求已取消。"));c.Go(0,true);break;
  }
  case "nativecharge":{
   var star=CurrentKitArt.StarImage(root,"star-empty-v22",0,100,350,350);var c=star.gameObject.AddComponent<StarChargeView>();c.Initialize(root,.4f);component=c;primaryAction=()=>{c.SetRotation(c.RotorAngle+45);c.SetImmediate(c.Fill>=.99f?0:Mathf.Min(1,c.Fill+.2f));};break;
  }
  case "celebration":{var layer=UICraft.Root(root,"Celebration bounds",0,0,1080,1920);var c=layer.gameObject.AddComponent<GoalCelebration>();component=c;play=_=>c.Begin();break;}
  case "lowmoves":{
   var text=UICraft.Label(root,"5",0,50,500,240,180,CSettingsArt.Ink);var c=root.gameObject.AddComponent<MovesFeedback>();c.Target=text.rectTransform;c.Label=text;component=c;primaryAction=c.Warn;UICraft.Label(root,"剩余步数 · 三次整体缩放",0,-170,1000,90,42);break;
  }
  case "teaching":{
   var c=root.gameObject.AddComponent<ThreeStepLoop>();component=c;var ids=new[]{501,505,506,518,511,521,531};int current=0;RectTransform layer=null;
   System.Action rebuild=()=>{if(layer!=null){layer.gameObject.SetActive(false);Destroy(layer.gameObject);}layer=UICraft.Root(root,"Three-step teaching");var story=layer.gameObject.AddComponent<OpeningStory049>();story.Build(ids[current]);var copy=OpeningStory049.GetCopy(ids[current]);UICraft.Label(layer,copy.Title,0,800,1000,100,60,CSettingsArt.Ink);for(int i=0;i<3;i++)UICraft.Label(layer,copy.Heads[i],i==1?-270:270,450-i*395,480,100,36,CSettingsArt.Ink);c.RestPhases=new Vector3(OpeningStory049.Rest(ids[current],0),OpeningStory049.Rest(ids[current],1),OpeningStory049.Rest(ids[current],2));c.RenderStep=(i,p)=>story.SampleStep(i,p);c.Sample(0);};
   rebuild();primaryAction=()=>{current=(current+1)%ids.Length;rebuild();};break;
  }
  default:throw new System.InvalidOperationException("Missing current example: "+Current.id);
 }}
}
}
