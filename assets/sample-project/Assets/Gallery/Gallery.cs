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
public sealed class Gallery:MonoBehaviour {
 [Serializable]public class Entry {public string id,title,category,source,trigger,binding,notes;public bool interactive,timeline,tall;}
 [Serializable]class Archive {public ArchiveEntry[] entries;}[Serializable]class ArchiveEntry {public string id,title,category,status,source,description,animationTypes;}
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
  E("charge","星星飞光与材质充能","奖励与目标","UI/StarChargeView.cs","点击增加充能","Image + 容器；Initialize → Charge → Advance","彗星0.24秒，填充0.36秒；保留 StarCharge 透明Shader及 _Clock。",true),
  E("victorycat","胜利猫非均匀序列帧","配套编排","UI/VictoryCatAnimation.cs","进入播放16帧","FrameSequence.Target → Image；原4×4图集","各帧时长原样保留，总长1.92秒；可拖动时间轴。",false,true),
  E("failurecat","失败猫非均匀序列帧","配套编排","UI/FailureCatAnimation.cs","进入播放16帧","FrameSequence.Victory=false；Target → Image","各帧时长原样保留，总长2.32秒；可拖动时间轴。",false,true),
  E("victory","胜利结算页面编排","奖励与目标","UI/VictoryPresentation.cs + .Stars.cs","进入自动演出；按钮仅更新本地展示状态","Begin(actionsFactory, level, first, coins, medals)","原光环96帧、爆发16帧、猫、星级与奖励卡；无奖励系统。",false,false,true),
  E("failure","失败结算页面编排","奖励与目标","UI/FailurePresentation.cs","进入自动演出；重试／广告为本地展示回调","Begin(retry, home, watch, canContinue)","原96帧背景、失败猫、逐字标题与按钮入场；不导航、不播广告。",false,false,true),
  E("medal","联赛奖章与升降名次","奖励与目标","UI/LeagueMotionArt.cs","进入展示奖章飞入与箭头","LeagueMedalPresentation.Show(parent,before,after,medals)","0.55秒入场，保留父级非等比补偿；名次只为显示数据。")
 };
 static Entry E(string id,string title,string cat,string source,string trigger,string binding,string notes,bool interactive=false,bool timeline=false,bool tall=false)=>new Entry{id=id,title=title,category=cat,source=source,trigger=trigger,binding=binding,notes=notes,interactive=interactive,timeline=timeline,tall=tall};
 public Entry Current=>Entries[index];public MonoBehaviour CurrentComponent=>component;
 RectTransform canvas,stage;GameObject demo;MonoBehaviour component;Action<bool> play;Action<float> sample;
 int index,filter,typeFilter;bool immediate,archiveMode;float elapsed,timeline;string search="",message="",lastCue="";Vector2 scroll,inspectorScroll;Font guiFont;Archive archive;
 readonly string[] categories={"全部","控件交互","页面与布局","文字与数值","奖励与目标","持续表现","场景表现","配套编排"};
 readonly string[] animationTypes={"全部类型","DOTween","协程","数学曲线","弹簧","序列帧","材质"};
 static string TypesFor(string id)=>id=="press"||id=="switch"||id=="modal"?"DOTween":id=="logo"?"DOTween 协程 数学曲线":id=="activity"||id=="drag"?"弹簧 数学曲线":id=="victorycat"||id=="failurecat"?"序列帧":id=="victory"||id=="failure"?"序列帧 数学曲线":id=="charge"?"材质 数学曲线":"数学曲线";
 readonly Dictionary<string,MotionPreset> retained=new Dictionary<string,MotionPreset>();
 void Awake(){Application.targetFrameRate=60;Application.runInBackground=true;KitClock.Manual=true;KitClock.Reset();DOTween.Init(false,true,LogBehaviour.ErrorsOnly);SoundService.Cue+=OnCue;
  if(Camera.main==null){var camera=new GameObject("Main Camera",typeof(Camera));camera.tag="MainCamera";camera.GetComponent<Camera>().clearFlags=CameraClearFlags.SolidColor;camera.GetComponent<Camera>().backgroundColor=new Color(.98f,.96f,.92f);}
  guiFont=Resources.Load<Font>("Fonts/MaokenZhuyuanTi");var go=new GameObject("Gallery canvas",typeof(RectTransform),typeof(Canvas),typeof(CanvasScaler),typeof(GraphicRaycaster));canvas=go.GetComponent<RectTransform>();go.GetComponent<Canvas>().renderMode=RenderMode.ScreenSpaceOverlay;var scaler=go.GetComponent<CanvasScaler>();scaler.uiScaleMode=CanvasScaler.ScaleMode.ConstantPixelSize;
  if(EventSystem.current==null)new GameObject("EventSystem",typeof(EventSystem),typeof(StandaloneInputModule));
  stage=UICraft.Root(canvas,"Stage",0,0,1080,2340);var data=Resources.Load<TextAsset>("ProjectTweenKit/archive");if(data!=null)archive=JsonUtility.FromJson<Archive>(data.text);Select(0);
  if(Environment.GetCommandLineArgs().Contains("--verify-kit"))gameObject.AddComponent<GalleryVerification>();
 }
 void OnCue(string cue){lastCue=cue;}
 void Update(){KitClock.Frame(Time.unscaledDeltaTime);elapsed+=KitClock.Delta;
  float left=285,right=355,top=86,bottom=94;stage.anchoredPosition=new Vector2((left-right)*.5f,(bottom-top)*.5f);float fit=Mathf.Min((Screen.width-left-right-35)/1080f,(Screen.height-top-bottom-30)/(Current.tall?2340f:1250f));stage.localScale=Vector3.one*Mathf.Max(.05f,fit);
  if(component is StarChargeView charge)charge.Advance(KitClock.Delta);
 }
 public void Select(int value){if(component!=null)retained[Current.id]=MotionPreset.Capture(component);if(demo!=null){foreach(var borrowed in demo.GetComponentsInChildren<CollectionGoalEntrance>(true))borrowed.Restore();demo.SetActive(false);Destroy(demo);}SoundService.CancelTransient();index=Mathf.Clamp(value,0,Entries.Length-1);elapsed=timeline=0;lastCue="";component=null;play=null;sample=null;
  demo=UICraft.Root(stage,"Example "+Current.id).gameObject;Build((RectTransform)demo.transform);if(component!=null&&retained.TryGetValue(Current.id,out var preset))preset.Apply(component);if(!Current.interactive)play?.Invoke(immediate);
 }
 public void Replay(){Select(index);}
 Image Art(Transform p,string key,float x,float y,float w,float h)=>UICraft.Art(p,key,x,y,w,h);
 void Build(RectTransform root){switch(Current.id){
  case "press":{var b=CSettingsArt.Button(root,"Fixed hit","按住我，试着拖动",0,0,820,220,()=>message="点击已触发：仅更新展馆文字");var c=b.GetComponent<UIBounce>();c.IdleInterval=5.5f;component=c;break;}
  case "switch":{bool state=true;var c=SettingsSwitch77.Create(root,"示例开关",0,0,()=>state,v=>{state=v;message=v?"展示状态：开":"展示状态：关";});c.transform.localScale=Vector3.one*2.5f;component=c;break;}
  case "modal":{var panel=UICraft.Root(root,"C settings panel",0,90,1000,1350);CSettingsArt.Image(panel,"panel_cream",0,0,1000,1350,true);CSettingsArt.Image(panel,"decor_cat_head",0,350,190,115);UICraft.Label(panel,"设置弹窗",0,120,800,140,80,CSettingsArt.Ink);var c=panel.gameObject.AddComponent<ModalMotion>();component=c;c.Close(true);CSettingsArt.Button(panel,"完成","完成",0,-400,720,170,()=>c.Close(immediate));CSettingsArt.Button(root,"打开","打开弹窗",0,-760,720,170,()=>c.Open(immediate));play=c.Open;sample=t=>{c.Open();DOTween.ManualUpdate(t,t);};break;}
  case "reward":{var coin=UICraft.Box(root,"Actual copper collectible",-250,-300,135,135,Color.white);coin.sprite=OpeningCopperArt.State(25);coin.type=Image.Type.Simple;coin.preserveAspect=true;var target=Art(root,"resource_capsule",210,380,330,140);var c=root.gameObject.AddComponent<RewardFlight>();c.Target=coin.rectTransform;c.Destination=target.rectTransform;c.Kind=RewardFlight.LootKind.copper_coin;c.UnitsPerWorldUnit=100;component=c;play=c.Play;sample=c.Sample;CSettingsArt.Button(root,"收集","收集物吸入（仅表现）",0,-540,850,170,()=>c.Play(immediate));break;}
  case "number":{var t=UICraft.Label(root,"100",0,120,1000,240,145,CSettingsArt.Ink);var c=root.gameObject.AddComponent<NumberRoll>();c.Target=t;component=c;play=c.Play;sample=c.Sample;CSettingsArt.Button(root,"滚分","增加显示值",0,-250,760,170,()=>c.Play(immediate));break;}
  case "logo":{var c=root.gameObject.AddComponent<JellyLogoIntro>();component=c;play=_=>c.Begin();break;}
  case "wordmark":{var r=PastelLogo.Create(root,0,0,950,400);component=r.gameObject.AddComponent<HomeWordmarkWiggle>();break;}
  case "loading":{var c=root.gameObject.AddComponent<LoadingPresentation>();c.Build();component=c;play=_=>c.Play();break;}
  case "float":{var c=CSettingsArt.Image(root,"decor_cat_head",0,0,570,345).gameObject.AddComponent<UIFloat>();c.amplitude=3;c.speed=1.4f;component=c;break;}
  case "drag":{var hit=UICraft.Box(root,"Fixed text hit",0,0,980,500,Color.clear,false,true);var visual=UICraft.Root(hit.transform,"Text visual",0,0,900,320);var a=UICraft.Label(visual,"状态越来越好！",0,15,900,100,78);var b=UICraft.Label(visual,"已经连续赢了2局。",0,-82,860,76,48);var c=hit.gameObject.AddComponent<EncouragementDrag>();c.Initialize(visual,a,b,()=>!KitClock.Paused);component=c;break;}
  case "activity":{var hit=UICraft.Box(root,"Fixed card hit",0,0,440,558,Color.clear,false,true);var visual=UICraft.Root(hit.transform,"Card visual",0,0,440,558);var face=Art(visual,"BannerJelly/panel",0,0,440,558);face.preserveAspect=false;face.color=new Color32(255,233,174,255);var icon=Art(visual,"BannerJelly/medal",0,-32,238,238);UICraft.Label(visual,"花园联赛",0,210,366,66,45);UICraft.Label(visual,"轻松闯关，一起成长",0,150,366,52,25);var c=hit.gameObject.AddComponent<ActivityJellyMotion>();c.Initialize(visual,icon.rectTransform,0,()=>!KitClock.Paused);hit.gameObject.AddComponent<ActivityEvents>().Motion=c;component=c;break;}
  case "hit":{var image=UICraft.Box(root,"Current copper visual",0,120,350,350,Color.white);image.sprite=OpeningCopperArt.State(25);image.type=Image.Type.Simple;image.preserveAspect=true;var c=root.gameObject.AddComponent<JellyHit>();c.Target=image.transform;component=c;play=c.Play;sample=c.Sample;CSettingsArt.Button(root,"打击","从上方受击",0,-330,750,170,()=>c.Play(immediate));break;}
  case "goal":{var home=UICraft.Root(root,"Original HUD home",0,850,900,200);var cards=UICraft.Root(home,"Real goals",0,0,690,150);for(int i=0;i<3;i++){var r=UICraft.Root(cards,"Card "+i,(i-1)*230,0,210,130);Art(r,"resource_capsule",0,0,210,130);Art(r,"icon_coin",-45,0,58,58);UICraft.Label(r,"6",35,0,90,60,36);}var layer=UICraft.Root(root,"Entrance overlay");var c=layer.gameObject.AddComponent<CollectionGoalEntrance>();component=c;play=_=>c.Begin(cards);break;}
  case "charge":{var star=BattleJellyArt.Image(root,"vine-star-off",0,200,350,350);var c=star.gameObject.AddComponent<StarChargeView>();c.Initialize(root,0);component=c;CSettingsArt.Button(root,"充能","充能 +25%",0,-300,760,170,()=>{if(immediate)c.SetImmediate(Mathf.Min(1,c.Target+.25f));else c.Charge(Mathf.Min(1,c.Target+.25f),root.TransformPoint(new Vector3(-320,-300,0)));});break;}
  case "victorycat":case "failurecat":{var image=UICraft.Box(root,"Sequence",0,0,850,850,Color.white);image.preserveAspect=true;var c=root.gameObject.AddComponent<FrameSequence>();c.Target=image;c.Victory=Current.id=="victorycat";component=c;play=c.Play;sample=c.Sample;break;}
  case "victory":{var c=root.gameObject.AddComponent<VictoryPresentation>();component=c;play=_=>c.Begin(()=>{var actions=UICraft.Root(root,"Mock actions");CSettingsArt.Button(actions,"继续","继续（展馆本地）",0,-850,750,170,()=>message="本地展示：继续；没有导航或发奖");return actions;},12,true,40,2);break;}
  case "failure":{var c=root.gameObject.AddComponent<FailurePresentation>();component=c;play=_=>c.Begin(()=>message="本地展示：重试",()=>message="本地展示：返回",()=>message="本地展示：广告入口",true);break;}
  case "medal":{LeagueMedalPresentation.Show(root,12,8,2);component=root.GetComponentInChildren<LeagueMedalPresentation>();break;}
 }}
 public void Seek(float seconds){if(!Current.timeline)return;KitClock.Sampling=true;try{Select(index);if(Current.id=="modal"){sample?.Invoke(seconds);}else{play?.Invoke(false);if(component is RewardFlight r)r.Seek(seconds);else if(component is NumberRoll n)n.Seek(seconds);else if(component is JellyHit j)j.Seek(seconds);else if(component is FrameSequence f)f.Seek(seconds);}timeline=seconds;KitClock.Paused=true;}finally{KitClock.Sampling=false;}}
 float TimelineDuration=>component is ModalMotion m?m.OpenDuration:component is RewardFlight r?r.Duration:component is NumberRoll n?n.Duration:component is JellyHit j?j.Duration:component is FrameSequence f?f.Duration:0;
 string PresetPath=>Path.Combine(Application.persistentDataPath,"presets",Current.id+".json");
 public void SavePreset(){if(component==null)return;MotionPreset.Capture(component).Save(PresetPath);message="参数已保存："+PresetPath;}
 public void LoadPreset(){try{MotionPreset.Load(PresetPath).Apply(component);message="已载入同一组件参数";}catch(Exception e){message=e.Message;}}
 void OnGUI(){GUI.skin.font=guiFont;GUI.skin.label.wordWrap=true;GUI.skin.label.fontSize=15;GUI.skin.button.fontSize=15;GUI.skin.textField.fontSize=15;GUI.skin.box.fontSize=15;
  GUI.color=Color.white;GUI.backgroundColor=new Color(.94f,.92f,.87f);GUI.skin.box.normal.background=Texture2D.whiteTexture;var ink=new Color(.32f,.23f,.18f);GUI.skin.label.normal.textColor=ink;foreach(var state in new[]{GUI.skin.toggle.normal,GUI.skin.toggle.onNormal,GUI.skin.toggle.hover,GUI.skin.toggle.onHover,GUI.skin.toggle.active,GUI.skin.toggle.onActive})state.textColor=ink;
  // IMGUI draws over the canvas on some render paths; stage remains transparent here.
  GUI.Label(new Rect(25,15,1000,36),"喵弹拆箱 · 项目补间展馆   /   原组件 · 原素材 · 来源可追溯");GUI.Label(new Rect(25,48,1000,28),"已交付示例 "+Entries.Length+" 项；来源候选与待核对项见档案。参数文件可用于导入后的同一组件。");
  GUILayout.BeginArea(new Rect(15,90,255,Screen.height-190),GUI.skin.box);search=GUILayout.TextField(search);filter=GUILayout.SelectionGrid(filter,categories,2);typeFilter=GUILayout.SelectionGrid(typeFilter,animationTypes,2);archiveMode=GUILayout.Toggle(archiveMode,"浏览来源档案（含待核对）");scroll=GUILayout.BeginScrollView(scroll);
  if(!archiveMode){for(int i=0;i<Entries.Length;i++){var e=Entries[i];if((filter==0||e.category==categories[filter])&&(typeFilter==0||TypesFor(e.id).Contains(animationTypes[typeFilter]))&&(string.IsNullOrEmpty(search)||(e.title+e.source).IndexOf(search,StringComparison.OrdinalIgnoreCase)>=0))if(GUILayout.Button((index==i?"● ":"")+e.title,GUILayout.Height(39)))Select(i);}}
  else if(archive!=null)foreach(var e in archive.entries){if(filter>0&&e.category!=categories[filter])continue;if(typeFilter>0&&(e.animationTypes==null||!e.animationTypes.Contains(animationTypes[typeFilter])))continue;if(!string.IsNullOrEmpty(search)&&(e.title+e.source+e.description).IndexOf(search,StringComparison.OrdinalIgnoreCase)<0)continue;if(GUILayout.Button(e.title+"\n"+e.status,GUILayout.Height(60)))message=e.source+"\n"+e.description;}
  GUILayout.EndScrollView();GUILayout.EndArea();
  GUILayout.BeginArea(new Rect(Screen.width-340,90,325,Screen.height-190),GUI.skin.box);inspectorScroll=GUILayout.BeginScrollView(inspectorScroll);GUILayout.Label(Current.title);GUILayout.Space(10);GUILayout.Label("原游戏操作："+Current.trigger);GUILayout.Label("绑定："+Current.binding);GUILayout.Label("行为："+Current.notes);GUILayout.Label("来源：Assets/Scripts/"+Current.source);GUILayout.Space(14);
  if(component!=null){GUILayout.Label("当前组件："+component.GetType().Name);foreach(var f in component.GetType().GetFields(BindingFlags.Instance|BindingFlags.Public)){if(f.FieldType==typeof(float)){float value=(float)f.GetValue(component);GUILayout.Label(f.Name+" = "+value.ToString("0.###")+Unit(f.Name));float max=f.Name.ToLower().Contains("duration")?8:f.Name=="Arc"?400:f.Name.ToLower().Contains("scale")?2:f.Name=="amplitude"?40:f.Name=="IdleInterval"?15:10;float next=GUILayout.HorizontalSlider(value,0,max);if(Mathf.Abs(next-value)>.0001f)f.SetValue(component,next);}else if(f.FieldType==typeof(bool)){bool value=(bool)f.GetValue(component),next=GUILayout.Toggle(value,f.Name);if(next!=value)f.SetValue(component,next);}}}
  GUILayout.BeginHorizontal();if(GUILayout.Button("保存参数"))SavePreset();if(GUILayout.Button("载入参数"))LoadPreset();GUILayout.EndHorizontal();GUILayout.Label("状态："+(KitClock.Paused?"暂停":Current.interactive?"等待真实操作 / 可重入":"自动播放")+"\n时间："+elapsed.ToString("0.00")+" s\n声音节点："+lastCue);GUILayout.Label(message);GUILayout.EndScrollView();GUILayout.EndArea();
  GUILayout.BeginArea(new Rect(285,Screen.height-85,Mathf.Max(100,Screen.width-650),78),GUI.skin.box);GUILayout.BeginHorizontal();if(GUILayout.Button("重播表现"))Replay();KitClock.Paused=GUILayout.Toggle(KitClock.Paused,"暂停");GUILayout.Label("速度 "+KitClock.Speed.ToString("0.00")+"×",GUILayout.Width(100));KitClock.Speed=GUILayout.HorizontalSlider(KitClock.Speed,.1f,2,GUILayout.Width(120));SoundService.Muted=GUILayout.Toggle(SoundService.Muted,"静音");if(Current.id=="modal"||Current.id=="reward"||Current.id=="number"||Current.id=="hit"||Current.id=="charge")immediate=GUILayout.Toggle(immediate,"即时完成对照");GUILayout.EndHorizontal();if(Current.timeline){float next=GUILayout.HorizontalSlider(timeline,0,TimelineDuration);if(Mathf.Abs(next-timeline)>.002f)Seek(next);}else GUILayout.Label(Current.interactive?"此条目等待真实操作。重播不会模拟一次点击或提交业务结果。":"按原触发方式自动播放；含协程编排的条目不提供伪时间轴。");GUILayout.EndArea();
 }
 static string Unit(string name)=>name.Contains("Duration")||name.Contains("Delay")||name.Contains("Interval")?" s":name=="Arc"||name=="amplitude"?" 设计单位":name=="speed"?" rad/s":"";
 void OnDestroy(){SoundService.Cue-=OnCue;KitClock.Manual=false;KitClock.Paused=false;KitClock.Speed=1;}
}
public sealed class ActivityEvents:MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler {
 public ActivityJellyMotion Motion;public void OnPointerEnter(PointerEventData e)=>Motion.SetHover(true);public void OnPointerExit(PointerEventData e)=>Motion.SetHover(false);public void OnPointerDown(PointerEventData e)=>Motion.Press();public void OnPointerUp(PointerEventData e)=>Motion.Release();
}
}
