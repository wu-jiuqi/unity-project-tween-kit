using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ProjectTweenKit;

namespace TweenGallery {
// Native, uniformly scaled developer exhibit, using the project's existing C artwork.
public sealed partial class Gallery {
 static readonly Color Ink=new Color32(108,76,59,255),Muted=new Color32(153,129,108,255),Mint=new Color32(81,149,130,255),Paper=new Color32(255,251,242,255);
 RectTransform chrome,viewport,listContent,parameterContent,detailsContent,archivePanel,examplesPanel,focusRoot,focusViewport;
 Text exampleCount,selectedTitle,selectedCategory,instruction,stepLabel,stateLabel,notice,primaryLabel,pauseText,speedText,timelineLabel,parameterHint,archiveTitle,archiveBody,rightTitle,directHint;
 Button primaryButton,pauseButton,restartButton,saveButton,loadButton,resetButton,examplesTab,archiveTab,parametersTab,integrationTab,relatedExampleButton;
 InputField searchField;Dropdown categoryMenu,typeMenu;Slider timeSlider;
 Toggle instantToggle,muteToggle;RectTransform timelineRow;bool uiSync,integrationVisible,hasTriggered,listArchiveMode,focusedStage;
 Text focusTitle,focusHint,focusActionText,focusPauseText,focusDirectHint;Button focusAction,locateSourceButton;
 Action primaryAction;float triggeredAt;int uiWidth,uiHeight;
 readonly Dictionary<string,MotionPreset> defaults=new Dictionary<string,MotionPreset>();
 readonly Dictionary<int,Text> exampleLabels=new Dictionary<int,Text>();
 readonly Dictionary<int,Image> selectionBars=new Dictionary<int,Image>();
 readonly List<Action> parameterUpdates=new List<Action>();
 int relatedExample=-1;
 public int VisibleExampleCount{get;private set;}
 public bool ArchiveBrowserOpen=>archiveMode;
 public bool HasMainAction=>primaryAction!=null||!Current.interactive;
 public string OperationHint=>Instructions(Current.id);
 public RectTransform StageViewport=>focusedStage?focusViewport:viewport;
 public bool StageFocused=>focusedStage;
 public Button MainActionButton=>primaryButton;
 public InputField SearchInput=>searchField;
 public string FeedbackText=>message;
 public bool StageShown=>demo!=null&&demo.activeInHierarchy&&!archiveMode;
 public Button ParameterResetButton=>resetButton;
 public bool ShortcutsAvailable {get{var go=EventSystem.current!=null?EventSystem.current.currentSelectedGameObject:null;return go==null||(go.GetComponent<InputField>()==null&&go.GetComponentInParent<Dropdown>()==null&&go.GetComponent<Toggle>()==null);}}

 void BuildInterface(){
  chrome=Rect(canvas,"Gallery interface",0,0,1600,1000);chrome.anchorMin=chrome.anchorMax=Vector2.one*.5f;chrome.pivot=Vector2.one*.5f;chrome.anchoredPosition=Vector2.zero;
  Picture(chrome,"decor_cat_head",26,18,68,58);Label(chrome,"补间体验馆",110,14,650,48,34);
  Label(chrome,"喵弹拆箱  /  选一个效果，动手体验，再把参数带回项目",112,65,900,26,17,Muted);
  Label(chrome,"01 选择效果     02 按提示体验     03 调参复用",1000,34,576,40,18,Mint).alignment=TextAnchor.MiddleRight;
  Panel(chrome,22,112,272,862);Panel(chrome,1208,112,370,862);
  examplesTab=Button(chrome,"效果体验",36,126,122,43,()=>SetBrowserMode(false),true,17);
  archiveTab=Button(chrome,"来源档案",162,126,118,43,()=>SetBrowserMode(true),false,17);
  searchField=TextInput(chrome,"搜索效果 / 组件",38,181,240,39,s=>{search=s;RebuildList();},false);
  Label(chrome,"使用场景",40,230,235,22,15,Muted);categoryMenu=Menu(chrome,categories,38,256,240,37,v=>{filter=v;RebuildList();});
  Label(chrome,"动画类型",40,304,235,22,15,Muted);typeMenu=Menu(chrome,animationTypes,38,330,240,37,v=>{typeFilter=v;RebuildList();});
  exampleCount=Label(chrome,"",40,381,235,28,16,Muted);
  listContent=Scroll(chrome,"Effect directory",36,420,244,492);
  Label(chrome,"先选效果，再按舞台提示操作。",40,929,235,28,15,Muted);

  examplesPanel=Rect(chrome,"Experience mode",312,112,878,862);
  selectedCategory=Label(examplesPanel,"",10,0,610,29,16,Mint);
  Button(examplesPanel,"放大舞台",733,0,137,30,()=>SetStageFocused(true),false,15);
  selectedTitle=Label(examplesPanel,"",8,34,630,47,29);
  Button(examplesPanel,"上一项",664,33,98,43,()=>Navigate(-1),false,16);Button(examplesPanel,"下一项",768,33,102,43,()=>Navigate(1),false,16);
  var hint=Flat(examplesPanel,"Operation guide",8,94,862,83,new Color32(235,244,231,255));
  stepLabel=Label(hint.transform,"",17,8,822,25,17,Mint);instruction=Label(hint.transform,"",17,34,822,40,21);
  var stageCard=Panel(examplesPanel,8,191,862,480,"card_cream_wide");
  viewport=Rect(stageCard,"Clipped example stage",10,9,842,462);viewport.gameObject.AddComponent<RectMask2D>();
  stage.SetParent(viewport,false);stage.anchorMin=stage.anchorMax=stage.pivot=Vector2.one*.5f;stage.anchoredPosition=Vector2.zero;
  stateLabel=Label(examplesPanel,"",19,678,840,30,17,Mint);
  primaryButton=Button(examplesPanel,"",8,718,258,55,TriggerPrimary,true,20);primaryLabel=primaryButton.GetComponentInChildren<Text>();
  directHint=Label(examplesPanel,"请直接操作上方舞台\n按提示点击 / 按住 / 拖动",16,716,250,58,18,Mint);
  restartButton=Button(examplesPanel,"重新开始",277,718,132,55,RestartExperience,false,18);
  pauseButton=Button(examplesPanel,"暂停",420,718,105,55,()=>SetPaused(!KitClock.Paused),false,18);pauseText=pauseButton.GetComponentInChildren<Text>();
  speedText=Label(examplesPanel,"观察速度",541,718,108,25,15,Muted);
  Button(examplesPanel,"0.25×",538,747,77,30,()=>SetObservationSpeed(.25f),false,14);Button(examplesPanel,"0.5×",620,747,70,30,()=>SetObservationSpeed(.5f),false,14);Button(examplesPanel,"1×",695,747,57,30,()=>SetObservationSpeed(1),false,14);
  muteToggle=Check(examplesPanel,"静音",772,731,93,v=>SoundService.Muted=v);
  timelineRow=Rect(examplesPanel,"Timeline controls",8,790,862,64);timelineLabel=Label(timelineRow,"时间轴",0,0,862,24,15,Muted);
  timeSlider=Range(timelineRow,0,32,860,24,0,1,0,v=>{if(!uiSync)Seek(v);});

  archivePanel=Rect(chrome,"Source archive",312,112,878,862);Panel(archivePanel,8,0,862,862,"card_cream_help");
  Label(archivePanel,"来源档案",30,22,800,46,30);Label(archivePanel,"用来查实现与核对进度。未交付的来源条目不播放近似效果。",31,80,790,55,19,Muted);
  archiveTitle=Label(archivePanel,"从左侧选择一个来源条目",32,161,790,72,25);
  archiveBody=Label(archivePanel,"可搜索类名、组件名或源文件。\n\n已提供演示的条目可跳回效果体验。",32,252,790,450,20);
  relatedExampleButton=Button(archivePanel,"查看相关效果 →",32,749,289,58,()=>{if(relatedExample>=0){ClearFilters();Select(relatedExample);}},true);

  parametersTab=Button(chrome,"调参数",1224,127,165,43,()=>ShowIntegration(false),true,18);
  integrationTab=Button(chrome,"接入说明",1395,127,165,43,()=>ShowIntegration(true),false,18);
  rightTitle=Label(chrome,"本项参数",1228,186,329,35,24);
  parameterHint=Label(chrome,"拖动滑条或精确输入；更改保留在当前效果。",1228,229,326,54,17,Muted);
  parameterContent=Scroll(chrome,"Parameter scroll",1226,295,329,438);
  detailsContent=Scroll(chrome,"Integration notes",1226,234,329,600);
  locateSourceButton=Button(chrome,"在来源档案中定位",1226,847,331,41,LocateCurrentSource,false,17);
  resetButton=Button(chrome,"恢复本项默认",1226,754,331,48,ResetCurrentParameters,false,18);
  saveButton=Button(chrome,"保存本项",1226,813,161,48,SavePreset,false,18);loadButton=Button(chrome,"载入本项",1395,813,162,48,LoadPreset,false,18);
  instantToggle=Check(chrome,"即时完成对照",1231,876,207,v=>{immediate=v;Report(v?"已切到即时完成。再次触发会直接到达相同结果。":"已恢复原动画。再次触发可观察完整过程。");});
  Button(chrome,"参数目录",1441,876,116,35,()=>{string folder=Path.GetDirectoryName(PresetPath);Directory.CreateDirectory(folder);Application.OpenURL(new Uri(folder).AbsoluteUri);},false,15);
  notice=Label(chrome,"",1230,923,326,39,15,Muted);
  focusRoot=Rect(chrome,"Focused experience",0,104,1600,880);var focusBack=focusRoot.gameObject.AddComponent<Image>();focusBack.color=Paper;focusBack.raycastTarget=true;
  Panel(focusRoot,24,0,1552,856,"card_cream_wide");focusTitle=Label(focusRoot,"",50,14,1000,39,27);
  focusHint=Label(focusRoot,"",50,57,1450,38,19,Mint);Button(focusRoot,"返回调参",1376,13,172,43,()=>SetStageFocused(false),false,18);
  focusViewport=Rect(focusRoot,"Large clipped stage",43,107,1514,658);focusViewport.gameObject.AddComponent<RectMask2D>();
  focusAction=Button(focusRoot,"",407,781,278,53,TriggerPrimary,true,19);focusActionText=focusAction.GetComponentInChildren<Text>();
  focusDirectHint=Label(focusRoot,"请直接操作上方舞台",407,781,278,53,19,Mint);
  var fp=Button(focusRoot,"暂停",702,781,155,53,()=>SetPaused(!KitClock.Paused));focusPauseText=fp.GetComponentInChildren<Text>();Button(focusRoot,"重新开始",874,781,209,53,RestartExperience);
  focusRoot.gameObject.SetActive(false);
  RebuildList();ResizeInterface();
 }

 public void TriggerPrimary(){
  if(archiveMode||!HasMainAction)return;
  KitClock.Paused=false;hasTriggered=true;triggeredAt=elapsed;message="";
  if(primaryAction!=null)primaryAction();else Replay();
 }
 public void RestartExperience(){KitClock.Paused=false;Replay();}
 public void SetPaused(bool paused){KitClock.Paused=paused;}
 public void SetObservationSpeed(float value){KitClock.Speed=value;}
 public void SetStageFocused(bool value){if(archiveMode&&value)return;focusedStage=value;if(value){focusRoot.gameObject.SetActive(true);stage.SetParent(focusViewport,false);}else{stage.SetParent(viewport,false);focusRoot.gameObject.SetActive(false);}stage.anchoredPosition=Vector2.zero;ResizeStage();}
 public void Navigate(int direction){
  var visible=Enumerable.Range(0,Entries.Length).Where(Matches).ToList();if(visible.Count==0)return;int position=visible.IndexOf(index);if(position<0)position=direction>0?-1:0;Select(visible[(position+direction+visible.Count)%visible.Count]);
 }
 bool Matches(int i){var e=Entries[i];return(filter==0||e.category==categories[filter])&&(typeFilter==0||TypesFor(e.id).Contains(animationTypes[typeFilter]))&&(string.IsNullOrWhiteSpace(search)||(e.title+e.source+TypesFor(e.id)).IndexOf(search,StringComparison.OrdinalIgnoreCase)>=0);}
 public void SetBrowserMode(bool source){
  if(source==archiveMode)return;if(source&&focusedStage)SetStageFocused(false);
  if(source){archiveMode=true;foreach(var borrowed in demo.GetComponentsInChildren<CollectionGoalEntrance>(true))borrowed.Restore();demo.SetActive(false);KitClock.Paused=true;SoundService.CancelTransient();message="";relatedExample=-1;archiveTitle.text="从左侧选择一个来源条目";archiveBody.text="来源档案用于定位源码与核对状态。\n\n请选择左侧条目查看；相关效果可通过下方按钮打开。";}
  else{ClearFilters();Select(index);return;}
  RebuildList();RefreshInterface(false);
 }
 void ClearFilters(){search="";filter=typeFilter=0;searchField.SetTextWithoutNotify("");categoryMenu.SetValueWithoutNotify(0);typeMenu.SetValueWithoutNotify(0);}
 void LocateCurrentSource(){var entry=archive?.entries.FirstOrDefault(e=>e.galleryEntry==Current.id);if(entry==null)return;ClearFilters();SetBrowserMode(true);OpenArchive(entry);}
 void OpenArchive(ArchiveEntry entry){
  archiveTitle.text=entry.title;relatedExample=-1;
  if(!string.IsNullOrEmpty(entry.galleryEntry))relatedExample=Array.FindIndex(Entries,e=>e.id==entry.galleryEntry);
  if(relatedExample<0){string path=entry.source.Replace("Assets/Scripts/","");relatedExample=Array.FindIndex(Entries,e=>e.source.Split(new[]{"::"},StringSplitOptions.None)[0]==path);}
  archiveBody.text="核对状态\n"+StatusName(entry.status)+"\n\n源文件\n"+entry.source+"\n\n记录的实现\n"+entry.description+"\n\n完整方法、源码指纹、资源映射与生命周期见 Skill 的 references/catalog.md。";
  relatedExampleButton.gameObject.SetActive(relatedExample>=0);
 }
 void RebuildList(){
  if(listContent==null)return;listArchiveMode=archiveMode;ClearUI(listContent);exampleLabels.Clear();selectionBars.Clear();int count=0;
  if(!archiveMode){for(int i=0;i<Entries.Length;i++){if(!Matches(i))continue;int k=i;var e=Entries[i];var row=Button(listContent,"",0,count*65,240,59,()=>Select(k));var label=Label(row.transform,e.title,14,6,215,28,17);exampleLabels[k]=label;
    Label(row.transform,e.interactive?"动手体验  ·  "+e.category:"自动播放  ·  "+e.category,14,33,215,21,13,Muted);selectionBars[k]=Flat(row.transform,"Selection",0,11,4,37,Mint);count++;}}
  else if(archive!=null)foreach(var entry in archive.entries){if(filter>0&&entry.category!=categories[filter])continue;if(typeFilter>0&&(entry.animationTypes==null||!entry.animationTypes.Contains(animationTypes[typeFilter])))continue;if(!string.IsNullOrWhiteSpace(search)&&(entry.title+entry.source+entry.description).IndexOf(search,StringComparison.OrdinalIgnoreCase)<0)continue;var captured=entry;var row=Button(listContent,"",0,count*65,240,59,()=>OpenArchive(captured));Label(row.transform,entry.title,11,4,218,29,15);Label(row.transform,StatusName(entry.status),11,34,218,20,13,Muted);count++;}
  VisibleExampleCount=count;listContent.sizeDelta=new Vector2(240,Mathf.Max(1,count*65));exampleCount.text=archiveMode?"来源记录  ·  "+count+" 项":"可体验  "+count+" / "+Entries.Length+" 项";
  if(count==0){Label(listContent,"没有匹配的条目\n换个关键词或恢复“全部”筛选。",9,9,222,100,18,Muted);listContent.sizeDelta=new Vector2(240,110);}
  RefreshSelection();
 }
 void RefreshSelection(){foreach(var pair in exampleLabels)pair.Value.color=pair.Key==index?Mint:Ink;foreach(var pair in selectionBars)pair.Value.gameObject.SetActive(pair.Key==index);}
 void RefreshInterface(bool parameters=true){
  if(chrome==null)return;if(listArchiveMode!=archiveMode)RebuildList();examplesPanel.gameObject.SetActive(!archiveMode);archivePanel.gameObject.SetActive(archiveMode);
  Tab(examplesTab,!archiveMode);Tab(archiveTab,archiveMode);relatedExampleButton.gameObject.SetActive(archiveMode&&relatedExample>=0);
  selectedTitle.text=Current.title;selectedCategory.text=Current.category+"  /  "+(index+1).ToString("00")+" · "+Entries.Length;
  instruction.text=Instructions(Current.id);stepLabel.text=Current.interactive?"第 2 步  ·  动手体验":"第 2 步  ·  已按原场景自动播放";
  if(parameters&&!archiveMode){RebuildParameters();RebuildIntegration();}
  ShowIntegration(integrationVisible);RefreshSelection();ResizeStage();
 }
 void ShowIntegration(bool show){
  integrationVisible=show;bool notes=show||archiveMode;parameterContent.GetComponentInParent<ScrollRect>(true).gameObject.SetActive(!notes);detailsContent.GetComponentInParent<ScrollRect>(true).gameObject.SetActive(notes);locateSourceButton.gameObject.SetActive(notes&&!archiveMode);
  parameterHint.gameObject.SetActive(!notes);resetButton.gameObject.SetActive(!notes);saveButton.gameObject.SetActive(!notes);loadButton.gameObject.SetActive(!notes);instantToggle.gameObject.SetActive(!notes&&SupportsInstant);
  rightTitle.text=notes?"如何接回项目":"本项参数";Tab(parametersTab,!notes);Tab(integrationTab,notes);
  if(archiveMode){ClearUI(detailsContent);Label(detailsContent,"来源档案只用于查看实现与核对状态。\n\n点击中间的“查看相关效果”，可回到可操作的交付示例。\n\n未完成来源核对的条目仍会保留明确标记。",2,0,322,300,19,Muted);detailsContent.sizeDelta=new Vector2(329,320);}
 }
 void RebuildIntegration(){
  ClearUI(detailsContent);string text="用在哪里\n"+Current.trigger+"\n\n绑定哪些对象\n"+Current.binding+"\n\n表现与时序\n"+Current.notes+"\n\n组件\nProjectTweenKit."+component.GetType().Name+"\n\n真实源码\nAssets/Scripts/"+Current.source+"\n\n停止与清理\n参见 Skill：references/lifecycle.md。\n\n来源、素材与参数\n参见 references/catalog.md 和 integration.md。";
  var label=Label(detailsContent,text,2,0,322,1000,18);label.verticalOverflow=VerticalWrapMode.Overflow;detailsContent.sizeDelta=new Vector2(329,Mathf.Max(600,label.preferredHeight+30));
 }
 bool SupportsInstant=>new[]{"modal","reward","number","hit","charge"}.Contains(Current.id);
 void UpdateInterface(){
  if(chrome==null)return;if(uiWidth!=Screen.width||uiHeight!=Screen.height)ResizeInterface();
  bool editing=!ShortcutsAvailable;
  if(!editing&&!archiveMode){if(Input.GetKeyDown(KeyCode.Space))TriggerPrimary();if(Input.GetKeyDown(KeyCode.P))SetPaused(!KitClock.Paused);if(Input.GetKeyDown(KeyCode.R))RestartExperience();}
  if(archiveMode)return;
  focusTitle.text=Current.title;focusHint.text=Instructions(Current.id);focusActionText.text=PrimaryCaption();focusAction.interactable=HasMainAction;focusAction.gameObject.SetActive(HasMainAction);focusDirectHint.gameObject.SetActive(!HasMainAction);focusPauseText.text=KitClock.Paused?"继续播放":"暂停";
  primaryButton.gameObject.SetActive(HasMainAction);directHint.gameObject.SetActive(!HasMainAction);primaryButton.interactable=HasMainAction;primaryLabel.text=PrimaryCaption();pauseText.text=KitClock.Paused?"继续播放":"暂停";
  speedText.text="观察 "+KitClock.Speed.ToString("0.##")+"×";uiSync=true;muteToggle.SetIsOnWithoutNotify(SoundService.Muted);instantToggle.SetIsOnWithoutNotify(immediate);
  timelineRow.gameObject.SetActive(Current.timeline);if(Current.timeline){timeSlider.maxValue=Mathf.Max(.001f,TimelineDuration);if(!KitClock.Paused&&hasTriggered)timeline=Mathf.Clamp(elapsed-triggeredAt,0,TimelineDuration);timeSlider.SetValueWithoutNotify(timeline);timelineLabel.text="时间轴  "+timeline.ToString("0.00")+" / "+TimelineDuration.ToString("0.00")+" s   ·   拖动后暂停，点“继续播放”续播";}
  uiSync=false;stateLabel.text=StateDescription();notice.text=integrationVisible?"完整来源与资源映射保留在 Skill 中。":"保存的 JSON 可用于导入后的同一组件。";
  foreach(var refresh in parameterUpdates)refresh();
 }
 string StateDescription(){
  if(KitClock.Paused)return "已暂停  ·  点击“继续播放”继续观察";
  if(!string.IsNullOrEmpty(message)){string value=message.Length>102?message.Substring(0,99)+"…":message;return value;}
  if(component is ModalMotion modal)return modal.IsOpen?"弹窗已打开  ·  点击面板内“完成”关闭":"等待打开  ·  点击下方“打开弹窗”";
  if(component is StarChargeView charge)return "当前充能 "+Mathf.RoundToInt(charge.Fill*100)+"%  ·  "+(charge.Fill>=.999f?"再次点击可从 0% 重新体验":"点击下方按钮增加 25%");
  if(component is RewardFlight flight)return flight.Running?"收集物飞行中…":hasTriggered?"收集完成  ·  可修改参数后再次开始":"等待体验  ·  点击“开始收集”";
  if(component is NumberRoll number&&hasTriggered)return number.Displayed==number.To?"本轮滚分完成  ·  可修改起止数值后再次播放":"数值滚动中…";
  float duration=Current.id=="logo"?6.8f:Current.id=="goal"?2.35f:Current.id=="victory"?2.34f:Current.id=="failure"?2.32f:Current.id=="medal"?1:float.PositiveInfinity;
  if(component is FrameSequence frames&&!frames.Loop)duration=frames.Duration/Mathf.Max(.01f,frames.PlaybackSpeed);
  if(!Current.interactive&&elapsed-triggeredAt>=duration)return "本轮演示已完成  ·  点击“再看一次”重新观看";
  return Current.interactive?"等待你的操作  ·  "+Instructions(Current.id):"自动演示中  ·  可暂停、慢放或重新开始";
 }
 string PrimaryCaption(){if(Current.id=="modal"&&component is ModalMotion modal)return modal.IsOpen?"关闭弹窗":"打开弹窗";switch(Current.id){case "reward":return "开始收集";case "number":return "开始滚分";case "hit":return "触发受击";case "charge":return "充能 +25%";default:return Current.interactive?"请直接操作舞台":"再看一次 / 空格";}}
 static string Instructions(string id){switch(id){
  case "press":return "移入按钮 → 按住拖动 → 松手，看它回弹。";
  case "switch":return "直接点击舞台里的开关，观察滑块与轨道变化。";
  case "modal":return "点击下方“打开弹窗”，再点面板里的“完成”。";
  case "reward":return "点击下方“开始收集”，观察收集物飞向右上方。";
  case "number":return "点击下方“开始滚分”，看数值从起点滚到终点。";
  case "drag":return "按住中间的欢迎语拖动，松手观察弹簧归位。";
  case "activity":return "将鼠标移入卡片，再按住、松开，观察图标弹动。";
  case "hit":return "点击下方“触发受击”；右侧可切换受力方向。";
  case "charge":return "点击下方“充能 +25%”，观察飞光、填充和满星。";
  case "goal":return "自动展示：目标居中放大，随后飞回原来的 HUD 位置。";
  case "logo":return "自动展示：四字依次落下，文案逐字出现。";
  case "wordmark":return "每 5.5 秒一轮字标波浪；可用慢速观察。";
  case "loading":return "加载表现自动循环：观察文字与猫咪的动作配合。";
  case "float":return "轻微漂浮自动循环；右侧调幅度和速度看差异。";
  case "victorycat":case "failurecat":return "自动播放原序列帧；拖动下方时间轴可逐段观察。";
  case "victory":case "failure":return "自动展示结算编排；按钮出现后可真实点击体验。";
  default:return "自动展示奖章飞入与名次变化；可重新开始观看。";
 }}
 void ResizeInterface(){uiWidth=Screen.width;uiHeight=Screen.height;chrome.localScale=Vector3.one*Mathf.Min(uiWidth/1600f,uiHeight/1000f);ResizeStage();}
 void ResizeStage(){if(viewport==null)return;float height=Current.tall?2340:1250;if(new[]{"press","switch","number","float"}.Contains(Current.id))height=650;if(Current.id=="drag"||Current.id=="activity")height=800;if(Current.id=="hit"||Current.id=="wordmark")height=750;if(component is FrameSequence)height=1000;if(Current.id=="modal")height=1700;if(Current.id=="logo"||Current.id=="loading")height=1650;var view=StageViewport;stage.localScale=Vector3.one*Mathf.Min((view.rect.width-35)/1080f,(view.rect.height-28)/height);}

 void ResetCurrentParameters(){if(component==null||!defaults.TryGetValue(Current.id,out var preset))return;preset.Apply(component);retained[Current.id]=preset;Replay();Report("已恢复本项默认参数。");}
 static string StatusName(string value){switch(value){case "source-route-reviewed":return "已核对来源入口";case "requires-route-review":return "待核对当前入口";case "test-fixture":return "测试 / 预览记录";case "non-animation":return "业务 / 静态绘制";case "legacy-route-disabled":return "旧入口已停用";case "mixed-physics-presentation-review":return "物理与表现待拆分";default:return "来源记录 · "+value;}}

 // Parameter controls use curated names/ranges and edit only public component settings.
 void RebuildParameters(){
  ClearUI(parameterContent);parameterUpdates.Clear();float y=0;
  foreach(var field in component.GetType().GetFields(BindingFlags.Public|BindingFlags.Instance)){
   if(field.IsInitOnly)continue;var target=component;
   if(field.FieldType==typeof(float)||field.FieldType==typeof(int)){
    bool integer=field.FieldType==typeof(int);ParameterSpec spec=Spec(field.Name);var row=Rect(parameterContent,"Parameter "+field.Name,0,y,326,83);Label(row,spec.title,0,0,211,29,18);
    var input=TextInput(row,"",215,0,108,32,null,true);var unit=Label(row,spec.unit,265,41,58,26,14,Muted);unit.alignment=TextAnchor.MiddleRight;
    Func<float> get=()=>target==null?0:Convert.ToSingle(field.GetValue(target));Action<float> set=v=>{if(target==null)return;v=Mathf.Clamp(v,spec.min,spec.max);field.SetValue(target,integer?(object)Mathf.RoundToInt(v):(object)v);};
    var slider=Range(row,0,42,252,25,spec.min,spec.max,get(),v=>{set(v);input.SetTextWithoutNotify(get().ToString(integer?"0":"0.###",CultureInfo.InvariantCulture));});
    input.SetTextWithoutNotify(get().ToString(integer?"0":"0.###",CultureInfo.InvariantCulture));input.onEndEdit.AddListener(value=>{if(float.TryParse(value,NumberStyles.Float,CultureInfo.InvariantCulture,out float n)&&!float.IsNaN(n)&&!float.IsInfinity(n))set(n);slider.SetValueWithoutNotify(get());input.SetTextWithoutNotify(get().ToString(integer?"0":"0.###",CultureInfo.InvariantCulture));});
    parameterUpdates.Add(()=>{if(target!=null&&!input.isFocused){slider.SetValueWithoutNotify(get());input.SetTextWithoutNotify(get().ToString(integer?"0":"0.###",CultureInfo.InvariantCulture));}});y+=83;
   }else if(field.FieldType==typeof(bool)){
    var toggle=Check(parameterContent,ParameterName(field.Name),1,y+4,321,v=>{if(target!=null)field.SetValue(target,v);});toggle.SetIsOnWithoutNotify((bool)field.GetValue(target));y+=47;
   }else if(field.FieldType.IsEnum){
    Label(parameterContent,ParameterName(field.Name),0,y,320,29,18);var names=Enum.GetNames(field.FieldType);var readable=names.Select(n=>n=="cookie"?"鱼干":n=="yarn"?"毛线":n=="copper_coin"?"铜钱":n=="ingot"?"元宝":n=="ordinary_color"?"普通颜色":n).ToArray();var menu=Menu(parameterContent,readable,0,y+34,323,36,v=>{if(target!=null)field.SetValue(target,Enum.Parse(field.FieldType,names[v]));});menu.SetValueWithoutNotify(Convert.ToInt32(field.GetValue(target)));y+=86;
   }else if(field.FieldType==typeof(Vector2)&&field.Name=="Incoming"){
    Label(parameterContent,"受力方向",0,y,320,29,18);string[] words={"从上","从下","从左","从右"};Vector2[] directions={Vector2.down,Vector2.up,Vector2.right,Vector2.left};for(int i=0;i<4;i++){int k=i;Button(parameterContent,words[i],i*81,y+33,77,39,()=>{if(target!=null){field.SetValue(target,directions[k]);Report("已切换受力方向，点击“触发受击”观察。");}},false,15);}y+=88;
   }
  }
  bool any=y>0;parameterContent.sizeDelta=new Vector2(329,Mathf.Max(120,y));resetButton.interactable=saveButton.interactable=loadButton.interactable=any;
  parameterHint.text=any?"滑条实时调整，也可输入精确数值。再次触发比较效果。":"此演出保留项目固定编排。可用暂停、慢速和重新开始观察。";
  if(!any)Label(parameterContent,"原始节奏已保留\n\n打开“接入说明”可查看触发方式、对象绑定与源文件。",2,8,319,190,20,Muted);
 }
 struct ParameterSpec{public string title,unit;public float min,max;public ParameterSpec(string t,string u,float a,float b){title=t;unit=u;min=a;max=b;}}
 static string ParameterName(string name){switch(name){case "Loop":return "循环播放";case "Victory":return "使用胜利猫帧";case "Kind":return "轨迹类型";default:return name;}}
 static ParameterSpec Spec(string field){switch(field){
  case "HoverDuration":return new ParameterSpec("悬停过渡","秒",.02f,1);
  case "ExitDuration":return new ParameterSpec("移出回弹","秒",.02f,1.5f);
  case "PressDuration":return new ParameterSpec("按下时长","秒",.02f,1);
  case "ReleaseDuration":return new ParameterSpec("松开回弹","秒",.05f,2);
  case "IdleDuration":return new ParameterSpec("闲置摆动时长","秒",.1f,3);
  case "IdleInterval":return new ParameterSpec("闲置间隔 · 0关闭","秒",0,15);
  case "SlideDuration":return new ParameterSpec("滑块移动时长","秒",.02f,1);
  case "OpenDuration":return new ParameterSpec("打开时长","秒",.02f,2);
  case "CloseDuration":return new ParameterSpec("关闭时长","秒",.02f,2);
  case "OpenScale":return new ParameterSpec("打开起始比例","倍",.4f,1);
  case "CloseScale":return new ParameterSpec("关闭结束比例","倍",.4f,1);
  case "Duration":return new ParameterSpec("播放时长","秒",.05f,3);
  case "Delay":return new ParameterSpec("触发延迟","秒",0,2);
  case "From":return new ParameterSpec("起始数值","分",0,100000);
  case "To":return new ParameterSpec("结束数值","分",0,100000);
  case "Amplitude":case "Magnitude":return new ParameterSpec("变化幅度","倍",0,3);
  case "amplitude":return new ParameterSpec("浮动幅度","单位",0,35);
  case "speed":return new ParameterSpec("浮动角速度","rad/s",.1f,6);
  case "phase":return new ParameterSpec("起始相位","rad",0,6.283f);
  case "LoopInterval":return new ParameterSpec("循环间隔","秒",1,15);
  case "UnitsPerWorldUnit":return new ParameterSpec("轨迹单位换算","比例",1,200);
  case "EndScale":return new ParameterSpec("到达时大小","倍",.05f,1);
  case "PlaybackSpeed":return new ParameterSpec("帧播放速度","倍",.1f,2);
  default:return new ParameterSpec(field,"",0,10);
 }}

 // UI constructors use fixed design coordinates; only the outer shell scales with the window.
 static RectTransform Rect(Transform parent,string name,float x,float y,float width,float height){var go=new GameObject(name,typeof(RectTransform));go.transform.SetParent(parent,false);var r=(RectTransform)go.transform;r.anchorMin=r.anchorMax=new Vector2(0,1);r.pivot=new Vector2(0,1);r.anchoredPosition=new Vector2(x,-y);r.sizeDelta=new Vector2(width,height);return r;}
 Text Label(Transform parent,string value,float x,float y,float w,float h,int size=19,Color? color=null){var r=Rect(parent,value,x,y,w,h);var text=r.gameObject.AddComponent<Text>();text.font=guiFont;text.text=value;text.fontSize=size;text.color=color??Ink;text.alignment=TextAnchor.MiddleLeft;text.supportRichText=false;text.raycastTarget=false;text.horizontalOverflow=HorizontalWrapMode.Wrap;text.verticalOverflow=VerticalWrapMode.Truncate;return text;}
 static Image Flat(Transform parent,string name,float x,float y,float w,float h,Color color){var r=Rect(parent,name,x,y,w,h);var image=r.gameObject.AddComponent<Image>();image.color=color;image.raycastTarget=false;return image;}
 static RectTransform Picture(Transform parent,string key,float x,float y,float w,float h,bool sliced=false){var r=Rect(parent,key,x,y,w,h);var i=r.gameObject.AddComponent<Image>();i.sprite=CSettingsArt.Get(key);i.type=sliced?Image.Type.Sliced:Image.Type.Simple;i.preserveAspect=!sliced;i.raycastTarget=false;if(sliced)i.pixelsPerUnitMultiplier=key.StartsWith("button_")?i.sprite.rect.height/h:i.sprite.rect.width/w;return r;}
 static RectTransform Panel(Transform parent,float x,float y,float w,float h,string key="card_cream_help")=>Picture(parent,key,x,y,w,h,true);
 Button Button(Transform parent,string text,float x,float y,float w,float h,Action action,bool primary=false,int font=19){var r=Rect(parent,text,x,y,w,h);var image=r.gameObject.AddComponent<Image>();image.sprite=CSettingsArt.Get(primary?"button_peach_normal":"button_cream_normal");image.type=Image.Type.Sliced;image.pixelsPerUnitMultiplier=image.sprite.rect.height/h;var label=Label(r,text,7,0,w-14,h,font);label.alignment=TextAnchor.MiddleCenter;var b=r.gameObject.AddComponent<Button>();b.targetGraphic=image;b.transition=Selectable.Transition.SpriteSwap;b.spriteState=new SpriteState{pressedSprite=CSettingsArt.Get(primary?"button_peach_pressed":"button_cream_pressed")};b.onClick.AddListener(()=>{action();ReleaseSelection(r.gameObject);});return b;}
 static void ReleaseSelection(GameObject owner){if(EventSystem.current!=null&&EventSystem.current.currentSelectedGameObject==owner)EventSystem.current.SetSelectedGameObject(null);}
 System.Collections.IEnumerator ReleaseMenuSelection(GameObject owner){yield return null;ReleaseSelection(owner);}
 static void Tab(Button button,bool selected){var text=button.GetComponentInChildren<Text>();text.color=selected?Mint:Ink;}
 InputField TextInput(Transform parent,string placeholder,float x,float y,float w,float h,Action<string> changed,bool numeric){var r=Rect(parent,placeholder,x,y,w,h);var bg=r.gameObject.AddComponent<Image>();bg.sprite=UICraft.Shape();bg.type=Image.Type.Sliced;bg.color=Paper;var text=Label(r,"",9,0,w-18,h,17);var hint=Label(r,placeholder,9,0,w-18,h,16,Muted);var f=r.gameObject.AddComponent<InputField>();f.textComponent=text;f.placeholder=hint;f.targetGraphic=bg;f.contentType=numeric?InputField.ContentType.DecimalNumber:InputField.ContentType.Standard;if(changed!=null)f.onValueChanged.AddListener(v=>changed(v));return f;}
 Toggle Check(Transform parent,string caption,float x,float y,float width,Action<bool> action){var r=Rect(parent,caption,x,y,width,32);var hit=r.gameObject.AddComponent<Image>();hit.color=Color.clear;var square=Flat(r,"Toggle background",0,4,23,23,new Color32(215,227,212,255));square.raycastTarget=true;var check=Flat(square.transform,"Selected",5,5,13,13,Mint);Label(r,caption,32,0,width-32,32,17);var t=r.gameObject.AddComponent<Toggle>();t.targetGraphic=square;t.graphic=check;t.onValueChanged.AddListener(v=>{if(!uiSync){action(v);ReleaseSelection(r.gameObject);}});return t;}
 Slider Range(Transform parent,float x,float y,float w,float h,float min,float max,float value,Action<float> action){var r=Rect(parent,"Slider",x,y,w,h);var slider=r.gameObject.AddComponent<Slider>();slider.minValue=min;slider.maxValue=max;var back=Flat(r,"Track",0,h*.5f-3,w,6,new Color32(216,226,212,255));back.raycastTarget=true;var area=Rect(r,"Handle area",10,0,w-20,h);var handle=Picture(area,"toggle_knob",0,-2,28,28);handle.pivot=Vector2.one*.5f;handle.GetComponent<Image>().raycastTarget=true;slider.handleRect=handle;slider.targetGraphic=handle.GetComponent<Image>();slider.SetValueWithoutNotify(value);slider.onValueChanged.AddListener(v=>action(v));return slider;}
 RectTransform Scroll(Transform parent,string name,float x,float y,float w,float h){var root=Rect(parent,name,x,y,w+12,h);var view=Rect(root,"Viewport",0,0,w,h);var hit=view.gameObject.AddComponent<Image>();hit.color=new Color(1,1,1,.01f);view.gameObject.AddComponent<RectMask2D>();var content=Rect(view,"Content",0,0,w,1);var scroll=root.gameObject.AddComponent<ScrollRect>();scroll.viewport=view;scroll.content=content;scroll.horizontal=false;scroll.vertical=true;scroll.movementType=ScrollRect.MovementType.Clamped;scroll.scrollSensitivity=30;
  var track=Flat(root,"Scrollbar",w+4,0,6,h,new Color32(232,230,214,255));track.raycastTarget=true;var area=Rect(track.transform,"Sliding area",0,0,6,h);var handle=Flat(area,"Handle",0,0,6,h,Mint);handle.raycastTarget=true;handle.rectTransform.sizeDelta=Vector2.zero;var bar=track.gameObject.AddComponent<Scrollbar>();bar.direction=Scrollbar.Direction.BottomToTop;bar.handleRect=handle.rectTransform;bar.targetGraphic=handle;scroll.verticalScrollbar=bar;scroll.verticalScrollbarVisibility=ScrollRect.ScrollbarVisibility.AutoHide;return content;}
 Dropdown Menu(Transform parent,string[] options,float x,float y,float w,float h,Action<int> changed){var r=Rect(parent,"Select option",x,y,w,h);var bg=r.gameObject.AddComponent<Image>();bg.sprite=UICraft.Shape();bg.type=Image.Type.Sliced;bg.color=Paper;var caption=Label(r,"",10,0,w-35,h,17);Label(r,"v",w-25,0,20,h,15,Muted);
  float menuHeight=Mathf.Min(8,options.Length)*35+6;var template=Rect(r,"Template",0,h+3,w,menuHeight);var plate=template.gameObject.AddComponent<Image>();plate.color=Paper;var view=Rect(template,"Viewport",3,3,w-6,menuHeight-6);view.gameObject.AddComponent<RectMask2D>();var content=Rect(view,"Content",0,0,w-6,35);var item=Rect(content,"Item",0,0,w-6,35);var highlight=item.gameObject.AddComponent<Image>();highlight.color=new Color32(236,244,230,255);var mark=Flat(item,"Check",7,11,12,12,Mint);var itemText=Label(item,"",26,0,w-38,35,16);var toggle=item.gameObject.AddComponent<Toggle>();toggle.targetGraphic=highlight;toggle.graphic=mark;var sc=template.gameObject.AddComponent<ScrollRect>();sc.viewport=view;sc.content=content;sc.horizontal=false;sc.movementType=ScrollRect.MovementType.Clamped;sc.scrollSensitivity=24;template.gameObject.SetActive(false);
  var d=r.gameObject.AddComponent<Dropdown>();d.targetGraphic=bg;d.template=template;d.captionText=caption;d.itemText=itemText;d.options=options.Select(t=>new Dropdown.OptionData(t)).ToList();d.RefreshShownValue();d.onValueChanged.AddListener(v=>{changed(v);StartCoroutine(ReleaseMenuSelection(r.gameObject));});return d;
 }
 static void ClearUI(Transform parent){for(int i=parent.childCount-1;i>=0;i--){var child=parent.GetChild(i).gameObject;child.SetActive(false);Destroy(child);}}
}
public sealed class GalleryInputObserver:MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IBeginDragHandler,IEndDragHandler {
 public Gallery Owner;bool dragged;
 public void OnPointerDown(PointerEventData e){dragged=false;Owner.Report("正在按住。可以继续拖动，再松手观察回弹。");}
 public void OnBeginDrag(PointerEventData e){dragged=true;Owner.Report("拖拽中。松手后观察归位。");}
 public void OnPointerUp(PointerEventData e){Owner.Report(dragged?"拖拽已结束，观察视觉回弹。":"已松开，观察视觉回弹。");}
 public void OnEndDrag(PointerEventData e){Owner.Report("拖拽已结束，观察视觉回弹。");}
}
}
