using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ProjectTweenKit;

namespace TweenGallery {
[DefaultExecutionOrder(-20000)]
public sealed partial class Gallery:MonoBehaviour {
 [Serializable]public class Entry {public string id,title,category,source,trigger,binding,notes;public bool interactive,timeline,tall;}
 public static readonly Entry[] Entries={
  E("press","按钮按压与拖拽","控件交互","UI/UIBounce.cs","悬停、按住、拖出、松开","Button → ButtonVisual；Target 绑定视觉子层","命中根不移动；拖拽取消点击；禁用与失焦归位。",true),
  E("switch","设置开关","控件交互","UI/SettingsSwitch77.cs","点击轨道切换","ToggleVisual → track / knob；read/write 接宿主","±39 设计单位，0.16 秒 OutCubic；滑动重入先 Kill。",true),
  E("modal","C 版弹窗开合","页面与布局","UI/AppUI.SettingsC.cs + AppUI.cs","点击打开／完成关闭","CanvasGroup + 面板；保留目标 scale 向量","开 0.18 秒 OutCubic；关 0.14 秒 InCubic；业务恢复由宿主处理。",true,true,true),
  E("reward","收集物贝塞尔吸入","奖励与目标","UI/BattleJellyArt.cs::CollectionFlightMotion.Apply","点击收集；重播只重播表现","收集物 + 目标 RectTransform；单位换算由宿主给定","0.6秒 SmoothStep 贝塞尔，入场脉冲、缩至0.28、按种类轻摆；不计入目标。",true,true),
  E("number","结算分段数字滚动","文字与数值","UI/VictoryFinale.cs::Advance","分数星光到达后；点击增加展示值","NumberRoll.Target → TMP_Text；From / To 只控制显示","原结算每段0.2秒三次缓出；星光到达时由宿主触发；不写得分。",true,true),
  E("logo","Logo 入场与逐字文案","文字与数值","UI/JellyLogoIntro.cs","进入展示自动运行","PastelLogo 四独立字层；JellyGlyph + 引语 TMP","依次下落、30fps手工挤压、齐落后果冻衰减；文字每字0.035秒。",false,false,true),
  E("wordmark","Logo 逐字波浪","持续表现","UI/HomeWordmarkWiggle.cs","进入后5.5秒一轮","父级下的独立字图；保存各层原位","每字错开0.09秒、震颤0.48秒；允许宿主通过 CanAnimate 控制。"),
  E("loading","加载页与文字波浪","文字与数值","UI/LoadingPresentation.cs","加载过程自动循环","Build → Play；ContentStatus 可接宿主状态","五猫姿势与字顶点位移；猫一轮3.6秒；不启动真实加载。",false,false,true),
  E("float","装饰猫漂浮","持续表现","UI/UICraft.cs::UIFloat","进入后自动循环","UIFloat 挂在插画视觉层","设置页幅度3、角速度1.4；新增禁用归位接口，原正弦方程不变。"),
  E("drag","欢迎语弹簧拖拽","控件交互","UI/EncouragementDrag.cs","按住文字拖动后松开","固定 hit → visual → 标题／副文案","tanh 限位；105 弹性、6.5 阻尼、每帧四次子步。",true),
  E("activity","活动卡果冻弹簧","控件交互","UI/ActivityJellyMotion.cs","悬停、按下与松开","固定卡 → visual → icon；事件桥调用原组件","弹性155、阻尼10，四次子步；图标有持续浮动。",true),
  E("hit","方向受击挤压","场景表现","World/JellyMotion.cs::HitStretch","点击砖块或改变方向","JellyHit.Target 仅绑定视觉；碰撞体留宿主","0.45秒、四倍π正弦、e^-5t衰减、幅度0.28。",true,true),
  E("goal","目标居中后归位","奖励与目标","UI/CollectionGoalEntrance.cs","进入后自动放大并飞回","将真实目标容器传入 Begin；Restore 恢复原父级","2.35秒；1.45秒起飞，0.75秒 SmoothStep；中断恢复层级。",false,false,true),
  E("charge","星星飞光与旋转充能","奖励与目标","UI/StarChargeView.cs","点击增加充能","Image + 容器；Initialize → Charge → Advance","彗星0.24秒，填充0.36秒；原生UI遮罩；水平填充与旋转图案分层。",true),
  E("victorycat","胜利猫非均匀序列帧","配套编排","UI/VictoryCatAnimation.cs","进入播放8帧","FrameSequence.Target → Image；当前4×2图集","各帧时长原样保留，总长1.92秒；可拖动时间轴。",false,true),
  E("failurecat","失败猫非均匀序列帧","配套编排","UI/FailureCatAnimation.cs","进入播放8帧","FrameSequence.Victory=false；Target → Image","各帧时长原样保留，总长2.32秒；可拖动时间轴。",false,true),
  E("victory","胜利结算页面编排","奖励与目标","UI/VictoryPresentation.cs + .Stars.cs","进入自动演出；按钮仅更新本地展示状态","Begin(actionsFactory, level, first, coins, medals)","原光环96帧、爆发16帧、猫、星级与奖励卡；无奖励系统。",false,false,true),
  E("failure","失败结算页面编排","奖励与目标","UI/FailurePresentation.cs","进入自动演出；重试／广告为本地展示回调","Begin(retry, home, watch, canContinue)","原96帧背景、八帧失败猫与按钮入场；不导航、不播广告。",false,false,true),
  E("medal","联赛奖章与升降名次","奖励与目标","UI/LeagueMotionArt.cs","进入展示奖章飞入与箭头","LeagueMedalPresentation.Show(parent,before,after,medals)","0.55秒入场，保留父级非等比补偿；名次只为显示数据。"),
  E("scoreimpact","分数变色放大计分","文字与数值","UI/BattleHUDSummary.cs","得分星光到达时脉冲","ScoreImpact.Target → TMP；宿主单独更新真实分数","0.25秒正弦脉冲；峰值1.2倍、金色，回到原色与原比例。",true,true),
  E("scoregain","加分合并与上浮淡出","文字与数值","UI/ScoreGainPresentation.cs","连续增加演示分值；只改变显示","ScoreGain → 数字、视觉容器、CanvasGroup","0.07秒合并窗口；0.65秒上移32单位，0.05秒淡入，6%正弦放大。",true),
  E("taskarrival","目标到达与完成印章","奖励与目标","UI/ChainCollectionHUD.cs","点击收集，第三次完成","TaskArrival → 固定布局中的卡片视觉、图标、底垫、数字与印章","0.38秒到达脉冲；图标+10%/-8%，底垫指数衰减；0.4秒OutBack盖章。",true),
  E("moves","剩余步数挤压反馈","文字与数值","UI/ChainCollectionHUD.cs","宿主步数改变时触发","MovesFeedback.Target → 独立步数视觉层","0.5秒OutBack入场；0.3秒正弦挤压±2.5%；不改变业务步数。",true),
  E("rating","连击评价与星光爆发","奖励与目标","UI/ChainRatingView.cs","宿主判定评级后触发","CheerBurst → 字图、CanvasGroup与13个星光","1.8秒；0.45→1.12→1倍，按评级散开4/7/10/13颗星。",true),
  E("leaguejelly","排行卡片果冻抖动","控件交互","UI/LeagueRankingView.cs","名次或积分更新时触发","LeagueJelly 挂视觉子层，固定点击根","0.65秒；sin(5πt)×exp(-4t)×(1-t)，横9%/纵12%，旋转2.2度。",true),
  E("themedissolve","主页背景平滑换景","页面与布局","UI/ReferenceChapterView.cs","点击切换两张原背景","ThemeDissolve.Previous → 旧背景CanvasGroup；新背景在下层","0.8秒SmoothStep淡去旧背景；前景和命中层不跟随透明度。",true,true,true),
  E("loadingprogress","Loading 果冻进度条","文字与数值","../../Tools/Platform/WeChat/StartupLoadingView86.cs","模拟任务进度到达后平滑追赶；任务完成后才到100%","LoadingProgress → Fill / Marble / Shine / Stars / Number","每秒追赶32%，单帧最多0.05秒；完成0.42秒脉冲，6星爆发。",true),
  E("startuploading","微信启动 Loading 整页","页面与布局","../../Tools/Platform/WeChat/StartupLoadingView86.cs","原启动页图层与进度；模拟完成/中断/恢复","StartupLoadingPresentation.Build；调参对象为同一个LoadingProgress","原猫咪五姿态、浮岛、字标和文案；进度不随时间虚增。",true,false,true),
  E("pagenav","同级页面横向切换","页面与布局","UI/AppUI.cs","点击切换；连续操作从当前位置接续","PageSlide.Target → 页面轨道；宿主决定目标位置","0.27秒OutCubic；不执行导航业务。",true),
  E("stagger","商店分组错峰入场","页面与布局","UI/AppUI.Shop.cs","点击播放三组商品入场","GroupStagger.Targets → 原布局视觉层","0.98→1，0.20秒OutCubic，每项错开0.018秒。",true),
  E("rankmove","排行榜名次平滑重排","页面与布局","UI/LeagueRankingView.cs","宿主先给新顺序，点击更新显示","RankReorder.Rows → 稳定身份的行对象；Reorder传每行新名次","0.48秒OutCubic；中断从当前位置接续，不计算积分和名次。",true),
  E("guide","教学手势与目标强调","控件交互","UI/PlayableTutorialGuide.cs","循环指示；模拟真实操作时隐藏","TutorialGesture → 手势、触点、目标框、CanvasGroup","4.4秒一轮；延迟0.6秒后1.3秒SmoothStep拖动；不自动瞄准。",true),
  E("hintfade","操作后提示平滑淡出","页面与布局","UI/BattleUIArt.cs","模拟已操作/等待操作切换提示","HintFade.Group → 非交互提示组","alpha每秒变化2；隐藏提示不挡住输入。",true),
  E("homefeedback","欢迎语分层入场与呼吸","文字与数值","UI/HomeFeedbackView.cs","点击播放分层弹入；平时轻微呼吸","HomeLayeredFeedback → Title / Detail / Decoration","副文案延后0.13秒；cos(17t)×exp(-4.7t)衰减；1.35秒收尾。",true),
  E("pendulum","星星挂件受力摆动","持续表现","UI/BattleScoreMeter.cs","点击从两侧交替施加视觉冲量","PendantSwing.Target → 挂点；不绑定碰撞体","弹性30、阻尼3.6、四子步、限角36度、碰边反弹系数0.25。",true),
  E("breath","联赛徽章轻微呼吸","持续表现","UI/LeagueMatchPresentation.cs","进入后持续运行","DecorativeBreath.Target → 徽章视觉","1 + 0.035×sin(3t)，禁用还原原比例。"),
  E("toast","轻提示续时与替换","配套编排","0.5.1表现切片","在舞台操作或重播","ToastMotion","EnterDuration=.18秒 OutCubic，ExitDuration=.14秒 InCubic；HoldDuration=3.2秒，EnterScale=.96，ExitScale=.94。最新待显示消息覆盖旧消息；宿主输入类型与停留时长。",true,false,false),
  E("loadingbox","共享箱体与猫咪循环","配套编排","0.5.1表现切片","在舞台操作或重播","LoadingBoxAnimation","箱体落下.38秒、回弹.24秒；猫与砖块循环4.2秒；Render绝对时间，不驱动真实进度。",false,false,false),
  E("errorpopup","异常弹窗与共享循环","配套编排","0.5.1表现切片","在舞台操作或重播","ErrorPopupMotion","开.18秒/关.14秒；共享LoadingBoxAnimation；关闭按钮意图至多交付一次，取消/禁用丢弃意图。",true,false,true),
  E("pageflow","五页切换与资源等待","配套编排","0.5.1表现切片","在舞台操作或重播","PageTransition","Width=920设计单位，Duration=.27秒 OutCubic；一次只显示出发与目的页，跨标签只移动一屏；请求票据阻止过期完成。",true,false,false),
  E("nativecharge","旋转星星原生遮罩充能","配套编排","0.5.1表现切片","在舞台操作或重播","StarChargeView","原生RectMask2D+星形Mask；136单位方形覆盖旋转对角线；彗星.24秒、填充.36秒；满星隐藏底色。",true,false,false),
  E("celebration","目标达成移至底部循环","配套编排","0.5.1表现切片","在舞台操作或重播","GoalCelebration","标题关键帧0/.14/.26/.40/.56/.78秒；1.65秒预备，1.8–2.4秒弧线下移，2.85秒落位；12fps、12帧持续循环。",false,false,true),
  E("lowmoves","低步数三次缩放警告","配套编排","0.5.1表现切片","在舞台操作或重播","MovesFeedback","WarningCycle=.48秒，WarningLoops=3，WarningAmplitude=.10，整体峰值1.2倍；业务阈值与400ms震动由宿主处理。",true,false,false),
  E("teaching","三步机关教学循环","配套编排","0.5.1表现切片","在舞台操作或重播","ThreeStepLoop","LoopDuration=6.72秒，每步2.24秒；七种现用教学页的原素材、实际轴心和静态相位，点击切换机关；业务进度不提交。",true,false,true),
 };
 static Entry E(string id,string title,string cat,string source,string trigger,string binding,string notes,bool interactive=false,bool timeline=false,bool tall=false)=>new Entry{id=id,title=title,category=cat,source=source,trigger=trigger,binding=binding,notes=notes,interactive=interactive,timeline=timeline,tall=tall};
 public Entry Current=>Entries[index];public MonoBehaviour CurrentComponent=>component;
 RectTransform canvas,stage;GameObject demo;MonoBehaviour component;Action<bool> play;Action<float> sample;
 int index,filter,typeFilter;bool immediate;float elapsed,timeline;string search="",message="",lastCue="";Vector2 scroll,inspectorScroll;Font guiFont;
 readonly string[] categories={"全部","控件交互","页面与布局","文字与数值","奖励与目标","持续表现","场景表现","配套编排"};
 readonly string[] animationTypes={"全部类型","DOTween","协程","数学曲线","弹簧","序列帧","材质"};
 static string TypesFor(string id)=>id=="pendulum"?"弹簧 数学曲线":id=="press"||id=="switch"||id=="modal"?"DOTween":id=="logo"?"DOTween 协程 数学曲线":id=="activity"||id=="drag"?"弹簧 数学曲线":id=="victorycat"||id=="failurecat"?"序列帧":id=="victory"||id=="failure"?"序列帧 数学曲线":id=="charge"?"数学曲线":"数学曲线";
 readonly Dictionary<string,MotionPreset> retained=new Dictionary<string,MotionPreset>();
 void Awake(){Application.targetFrameRate=60;Application.runInBackground=true;KitClock.Manual=true;KitClock.Reset();DOTween.Init(false,true,LogBehaviour.ErrorsOnly);SoundService.Cue+=OnCue;
  if(Camera.main==null){var camera=new GameObject("Main Camera",typeof(Camera));camera.tag="MainCamera";camera.GetComponent<Camera>().clearFlags=CameraClearFlags.SolidColor;camera.GetComponent<Camera>().backgroundColor=new Color(.98f,.96f,.92f);}
  guiFont=Resources.Load<Font>("Fonts/MaokenZhuyuanTi");var go=new GameObject("Gallery canvas",typeof(RectTransform),typeof(Canvas),typeof(CanvasScaler),typeof(GraphicRaycaster));canvas=go.GetComponent<RectTransform>();go.GetComponent<Canvas>().renderMode=RenderMode.ScreenSpaceOverlay;var scaler=go.GetComponent<CanvasScaler>();scaler.uiScaleMode=CanvasScaler.ScaleMode.ConstantPixelSize;
  if(EventSystem.current==null)new GameObject("EventSystem",typeof(EventSystem),typeof(StandaloneInputModule));
  stage=UICraft.Root(canvas,"Stage",0,0,1080,2340);BuildInterface();Select(0);
  if(Environment.GetCommandLineArgs().Contains("--verify-kit"))gameObject.AddComponent<GalleryVerification>();
 }
 void OnCue(string cue){lastCue=cue;}
 public void Report(string value){message=value;}
 void Update(){KitClock.Frame(Time.unscaledDeltaTime);elapsed+=KitClock.Delta;
  UpdateInterface();
  if(component is StarChargeView charge)charge.Advance(KitClock.Delta);
  if(component is LoadingBoxAnimation actor)actor.Render(actor.Elapsed+KitClock.Delta);
 }
 public void Select(int value){KitClock.Paused=false;message="";hasTriggered=false;primaryAction=null;if(component!=null)retained[Current.id]=MotionPreset.Capture(component);if(demo!=null){foreach(var borrowed in demo.GetComponentsInChildren<CollectionGoalEntrance>(true))borrowed.Restore();demo.SetActive(false);Destroy(demo);}SoundService.CancelTransient();index=Mathf.Clamp(value,0,Entries.Length-1);elapsed=timeline=0;lastCue="";component=null;play=null;sample=null;
  demo=UICraft.Root(stage,"Example "+Current.id).gameObject;Build((RectTransform)demo.transform);if(component!=null)defaults[Current.id]=MotionPreset.Capture(component);if(component!=null&&retained.TryGetValue(Current.id,out var preset))preset.Apply(component);if(!Current.interactive){play?.Invoke(immediate);hasTriggered=true;triggeredAt=elapsed;}RefreshInterface();
 }
 public void Replay(){Select(index);}
 Image Art(Transform p,string key,float x,float y,float w,float h)=>UICraft.Art(p,key,x,y,w,h);
 void Build(RectTransform root){switch(Current.id){
  case "press":{var b=CSettingsArt.Button(root,"Fixed hit","按住我，试着拖动",0,0,820,220,()=>Report("点击完成。试着按住并拖动，松开后观察回弹。"));var c=b.GetComponent<UIBounce>();c.IdleInterval=5.5f;b.gameObject.AddComponent<GalleryInputObserver>().Owner=this;component=c;break;}
  case "switch":{bool state=true;var c=SettingsSwitch77.Create(root,"示例开关",0,0,()=>state,v=>{state=v;Report(v?"开关已打开。再次点击可关闭。":"开关已关闭。再次点击可打开。");});c.transform.localScale=Vector3.one*2.5f;component=c;break;}
  case "modal":{var panel=UICraft.Root(root,"C settings panel",0,90,1000,1350);CSettingsArt.Image(panel,"panel_cream",0,0,1000,1350,true);CSettingsArt.Image(panel,"decor_cat_head",0,350,190,115);UICraft.Label(panel,"设置弹窗",0,120,800,140,80,CSettingsArt.Ink);var c=panel.gameObject.AddComponent<ModalMotion>();component=c;c.Close(true);CSettingsArt.Button(panel,"完成","完成",0,-400,720,170,()=>c.Close(immediate));primaryAction=()=>{if(c.IsOpen)c.Close(immediate);else c.Open(immediate);};play=c.Open;sample=t=>{c.Open();DOTween.ManualUpdate(t,t);};break;}
  case "reward":{var coin=UICraft.Box(root,"Actual copper collectible",-250,-300,135,135,Color.white);coin.sprite=OpeningCopperArt.State(25);coin.type=Image.Type.Simple;coin.preserveAspect=true;var target=Art(root,"resource_capsule",210,380,330,140);var c=root.gameObject.AddComponent<RewardFlight>();c.Target=coin.rectTransform;c.Destination=target.rectTransform;c.Kind=RewardFlight.LootKind.copper_coin;c.UnitsPerWorldUnit=100;component=c;play=c.Play;sample=c.Sample;primaryAction=()=>c.Play(immediate);break;}
  case "number":{var t=UICraft.Label(root,"100",0,120,1000,240,145,CSettingsArt.Ink);var c=root.gameObject.AddComponent<NumberRoll>();c.Target=t;component=c;play=c.Play;sample=c.Sample;primaryAction=()=>c.Play(immediate);break;}
  case "logo":{var c=root.gameObject.AddComponent<JellyLogoIntro>();component=c;play=_=>c.Begin();break;}
  case "wordmark":{var r=PastelLogo.Create(root,0,0,950,400);component=r.gameObject.AddComponent<HomeWordmarkWiggle>();break;}
  case "loading":{var c=root.gameObject.AddComponent<LoadingPresentation>();c.Build();component=c;play=_=>c.Play();break;}
  case "float":{var c=CSettingsArt.Image(root,"decor_cat_head",0,0,570,345).gameObject.AddComponent<UIFloat>();c.amplitude=3;c.speed=1.4f;component=c;break;}
  case "drag":{var hit=UICraft.Box(root,"Fixed text hit",0,0,980,500,Color.clear,false,true);var visual=UICraft.Root(hit.transform,"Text visual",0,0,900,320);var a=UICraft.Label(visual,"状态越来越好！",0,15,900,100,78);var b=UICraft.Label(visual,"已经连续赢了2局。",0,-82,860,76,48);var c=hit.gameObject.AddComponent<EncouragementDrag>();c.Initialize(visual,a,b,()=>!KitClock.Paused);hit.gameObject.AddComponent<GalleryInputObserver>().Owner=this;component=c;break;}
  case "activity":{var hit=UICraft.Box(root,"Fixed card hit",0,0,440,558,Color.clear,false,true);var visual=UICraft.Root(hit.transform,"Card visual",0,0,440,558);var face=Art(visual,"BannerJelly/panel",0,0,440,558);face.preserveAspect=false;face.color=new Color32(255,233,174,255);var icon=Art(visual,"BannerJelly/medal",0,-32,238,238);UICraft.Label(visual,"花园联赛",0,210,366,66,45);UICraft.Label(visual,"轻松闯关，一起成长",0,150,366,52,25);var c=hit.gameObject.AddComponent<ActivityJellyMotion>();c.Initialize(visual,icon.rectTransform,0,()=>!KitClock.Paused);hit.gameObject.AddComponent<ActivityEvents>().Motion=c;hit.gameObject.AddComponent<GalleryInputObserver>().Owner=this;component=c;break;}
  case "hit":{var image=UICraft.Box(root,"Current copper visual",0,120,350,350,Color.white);image.sprite=OpeningCopperArt.State(25);image.type=Image.Type.Simple;image.preserveAspect=true;var c=root.gameObject.AddComponent<JellyHit>();c.Target=image.transform;component=c;play=c.Play;sample=c.Sample;primaryAction=()=>c.Play(immediate);break;}
  case "goal":{var home=UICraft.Root(root,"Original HUD home",0,850,900,200);var cards=UICraft.Root(home,"Real goals",0,0,690,150);for(int i=0;i<3;i++){var r=UICraft.Root(cards,"Card "+i,(i-1)*230,0,210,130);Art(r,"resource_capsule",0,0,210,130);Art(r,"icon_coin",-45,0,58,58);UICraft.Label(r,"6",35,0,90,60,36);}var layer=UICraft.Root(root,"Entrance overlay");var c=layer.gameObject.AddComponent<CollectionGoalEntrance>();component=c;play=_=>c.Begin(cards);break;}
  case "charge":{var star=BattleJellyArt.Image(root,"vine-star-off",0,200,350,350);var c=star.gameObject.AddComponent<StarChargeView>();c.Initialize(root,0);component=c;primaryAction=()=>{if(c.Settled&&c.Fill>=.999f)c.SetImmediate(0);if(immediate)c.SetImmediate(Mathf.Min(1,c.Target+.25f));else c.Charge(Mathf.Min(1,c.Target+.25f),root.TransformPoint(new Vector3(-320,-300,0)));};break;}
  case "victorycat":case "failurecat":{var image=UICraft.Box(root,"Sequence",0,0,850,850,Color.white);image.preserveAspect=true;var c=root.gameObject.AddComponent<FrameSequence>();c.Target=image;c.Victory=Current.id=="victorycat";component=c;play=c.Play;sample=c.Sample;break;}
  case "victory":{var c=root.gameObject.AddComponent<VictoryPresentation>();component=c;play=_=>c.Begin(()=>{var actions=UICraft.Root(root,"Mock actions");CSettingsArt.Button(actions,"继续","继续（展馆本地）",0,-850,750,170,()=>Report("已体验继续按钮。点击“重新开始”可再次观看结算演出。"));return actions;},12,true,40,2);break;}
  case "failure":{var c=root.gameObject.AddComponent<FailurePresentation>();component=c;play=_=>c.Begin(()=>Report("已体验重试按钮。可重新开始观看本次演出。"),()=>Report("已体验返回按钮。可从左侧选择其他效果。"),()=>Report("已体验继续按钮。这里只展示交互结果。"),true);break;}
  case "medal":{LeagueMedalPresentation.Show(root,12,8,2);component=root.GetComponentInChildren<LeagueMedalPresentation>();break;}
 default:BuildAdditional(root);break;
 }}
 public void Seek(float seconds){if(!Current.timeline)return;KitClock.Sampling=true;try{Select(index);if(Current.id=="modal"){sample?.Invoke(seconds);}else{play?.Invoke(false);if(component is RewardFlight r)r.Seek(seconds);else if(component is NumberRoll n)n.Seek(seconds);else if(component is JellyHit j)j.Seek(seconds);else if(component is FrameSequence f)f.Seek(seconds);else if(component is ScoreImpact si)si.Seek(seconds);else if(component is ThemeDissolve td){td.Seek(seconds);}}timeline=seconds;hasTriggered=true;triggeredAt=elapsed-seconds;KitClock.Paused=true;RefreshInterface(false);}finally{KitClock.Sampling=false;}}
 float TimelineDuration=>component is ModalMotion m?m.OpenDuration:component is RewardFlight r?r.Duration:component is NumberRoll n?n.Duration:component is JellyHit j?j.Duration:component is FrameSequence f?f.Duration:component is ScoreImpact si?si.Duration:component is ThemeDissolve td?td.Duration:0;
 string PresetPath {get{var args=Environment.GetCommandLineArgs();int at=Array.IndexOf(args,"--evidence");string root=args.Contains("--verify-kit")&&at>=0&&at+1<args.Length?args[at+1]:Application.persistentDataPath;return Path.Combine(root,"presets",Current.id+".json");}}
 public void SavePreset(){if(component==null)return;try{MotionPreset.Capture(component).Save(PresetPath);Report("参数已保存："+PresetPath);}catch(Exception e){Report("保存失败："+e.Message);}}
 public void LoadPreset(){try{MotionPreset.Load(PresetPath).Apply(component);Report("已载入本项参数，再次触发即可观察。");RefreshInterface();}catch(Exception e){Report("载入失败："+e.Message);}}
 void OnDestroy(){SoundService.Cue-=OnCue;KitClock.Manual=false;KitClock.Paused=false;KitClock.Speed=1;}
}
public sealed class ActivityEvents:MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler {
 public ActivityJellyMotion Motion;public void OnPointerEnter(PointerEventData e)=>Motion.SetHover(true);public void OnPointerExit(PointerEventData e)=>Motion.SetHover(false);public void OnPointerDown(PointerEventData e)=>Motion.Press();public void OnPointerUp(PointerEventData e)=>Motion.Release();
}
}
