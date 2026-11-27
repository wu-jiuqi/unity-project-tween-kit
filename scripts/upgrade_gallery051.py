"""Register current examples/notes and leave older example IDs stable."""
import json,re
from inventory import KIT,write

ENTRIES=[
 ('toast','轻提示续时与替换','ToastMotion','EnterDuration=.18秒 OutCubic，ExitDuration=.14秒 InCubic；HoldDuration=3.2秒，EnterScale=.96，ExitScale=.94。最新待显示消息覆盖旧消息；宿主输入类型与停留时长。',True,False),
 ('loadingbox','共享箱体与猫咪循环','LoadingBoxAnimation','箱体落下.38秒、回弹.24秒；猫与砖块循环4.2秒；Render绝对时间，不驱动真实进度。',False,False),
 ('errorpopup','异常弹窗与共享循环','ErrorPopupMotion','开.18秒/关.14秒；共享LoadingBoxAnimation；关闭按钮意图至多交付一次，取消/禁用丢弃意图。',True,True),
 ('pageflow','五页切换与资源等待','PageTransition','Width=920设计单位，Duration=.27秒 OutCubic；一次只显示出发与目的页，跨标签只移动一屏；请求票据阻止过期完成。',True,False),
 ('nativecharge','旋转星星原生遮罩充能','StarChargeView','原生RectMask2D+星形Mask；136单位方形覆盖旋转对角线；彗星.24秒、填充.36秒；满星隐藏底色。',True,False),
 ('celebration','目标达成移至底部循环','GoalCelebration','标题关键帧0/.14/.26/.40/.56/.78秒；1.65秒预备，1.8–2.4秒弧线下移，2.85秒落位；12fps、12帧持续循环。',False,True),
 ('lowmoves','低步数三次缩放警告','MovesFeedback','WarningCycle=.48秒，WarningLoops=3，WarningAmplitude=.10，整体峰值1.2倍；业务阈值与400ms震动由宿主处理。',True,False),
 ('teaching','三步机关教学循环','ThreeStepLoop','LoopDuration=6.72秒，每步2.24秒；七种现用教学页的原素材、实际轴心和静态相位，点击切换机关；业务进度不提交。',True,True),
]
def main():
 p=KIT/'assets/sample-project/Assets/Gallery/Gallery.cs';s=p.read_text(encoding='utf-8-sig')
 if 'E("toast"' not in s:
  at=s.index(' public static readonly Entry[] Entries={')+len(' public static readonly Entry[] Entries={')
  # Append rather than shift the stable old numeric test indices.
  end=s.index('\n };',at)
  lines=[]
  for ident,title,component,notes,interactive,tall in ENTRIES:
   lines.append('  E('+','.join(json.dumps(x,ensure_ascii=False) for x in [ident,title,'配套编排','0.5.1表现切片','在舞台操作或重播',component,notes])+','+str(interactive).lower()+',false,'+str(tall).lower()+'),')
  s=s[:end]+'\n'+ '\n'.join(lines)+s[end:]
  s=s.replace('if(component is StarChargeView charge)charge.Advance(KitClock.Delta);','if(component is StarChargeView charge)charge.Advance(KitClock.Delta);\n  if(component is LoadingBoxAnimation actor)actor.Render(actor.Elapsed+KitClock.Delta);')
 for ident,title,component,notes,interactive,tall in ENTRIES:
  line='  E('+','.join(json.dumps(x,ensure_ascii=False) for x in [ident,title,'配套编排','0.5.1表现切片','在舞台操作或重播',component,notes])+','+str(interactive).lower()+',false,'+str(tall).lower()+'),'
  s=re.sub(r'^  E\("'+ident+r'"[^\n]*',lambda _:line,s,flags=re.M)
 p.write_text(s,encoding='utf-8')
 p=KIT/'references/parameter-catalog.json';data=json.loads(p.read_text(encoding='utf-8'));data['entries']=[e for e in data['entries'] if e['id'] not in {e[0] for e in ENTRIES}]
 for ident,title,component,notes,interactive,tall in ENTRIES:data['entries'].append(dict(id=ident,notes=notes,timeSource='KitClock.ScaledDelta' if ident=='lowmoves' else 'KitClock.Delta'))
 for e in data['entries']:
  if e['id'] in ['victorycat','failurecat']:e['notes']='新版8帧，4×2图集；胜利1.92秒，失败2.32秒；ResultsC048Art保留各帧非均匀时长。'
  if e['id']=='charge':e['notes']='已同步原生UI旋转充能实现；彗星.24秒，填充.36秒，满星脉冲.38秒；原接口保留。'
 write(p,data)
 p=KIT/'assets/sample-project/Assets/Gallery/GalleryVerification.cs';s=p.read_text(encoding='utf-8-sig').replace('FailureCatAnimation.FrameAt(.29f)==1','FailureCatAnimation.FrameAt(.29f)==0&&FailureCatAnimation.FrameAt(.31f)==1');s=s.replace('  yield return VerifyCurrent();\n','');s=s.replace('  // No component references a game profile,','  yield return VerifyCurrent();\n  // No component references a game profile,');s=s.replace('public sealed class GalleryVerification:', 'public sealed partial class GalleryVerification:');p.write_text(s,encoding='utf-8')
 p=KIT/'assets/sample-project/Assets/Editor/KitBuild.cs';s=p.read_text(encoding='utf-8-sig').replace('bundleVersion="0.4.0"','bundleVersion="0.5.0"').replace('typeof(DecorativeBreath)}','typeof(DecorativeBreath),typeof(ToastMotion),typeof(PageTransition),typeof(ThreeStepLoop),typeof(LoadingBoxAnimation),typeof(GoalCelebration)}');p.write_text(s,encoding='utf-8')
 p=KIT/'assets/unity-kit/Runtime/ExamplePrefab.cs';s=p.read_text(encoding='utf-8-sig').replace('StartupLoading}','StartupLoading,LoadingBox,ErrorPopup,GoalCelebration,ThreeStepLoop,NativeCharge,LowMoves,Toast,PageTransition}')
 marker='\n  }\n }\n}'
 additions='''
   case Kind.LoadingBox:Component=LoadingBoxAnimation.Create(transform);break;
   case Kind.ErrorPopup:var error=gameObject.AddComponent<ErrorPopupMotion>();error.Build();Component=error;error.Open();break;
   case Kind.GoalCelebration:var goal=gameObject.AddComponent<GoalCelebration>();Component=goal;goal.Begin();break;
   case Kind.ThreeStepLoop:Component=gameObject.AddComponent<ThreeStepLoop>();break;
   case Kind.NativeCharge:var star=CurrentKitArt.StarImage(transform,"star-empty-v22",0,0,200,200);var charge=star.gameObject.AddComponent<StarChargeView>();charge.Initialize((RectTransform)transform,.5f);Component=charge;break;
   case Kind.LowMoves:var warning=gameObject.AddComponent<MovesFeedback>();warning.Label=UICraft.Label(transform,"5",0,0,300,200,100);warning.Target=warning.Label.rectTransform;Component=warning;warning.Warn();break;
   case Kind.Toast:var toast=gameObject.AddComponent<ToastMotion>();toast.Visual=UICraft.Root(transform,"Toast visual",0,0,900,180);CSettingsArt.Image(toast.Visual,"card_cream_help",0,0,900,180,true);toast.Group=gameObject.AddComponent<CanvasGroup>();toast.Label=UICraft.Label(toast.Visual,"",0,0,850,140,45);Component=toast;toast.Show("已保存装扮");break;
   case Kind.PageTransition:var nav=gameObject.AddComponent<PageTransition>();nav.Pages=new RectTransform[2];for(int i=0;i<2;i++){nav.Pages[i]=UICraft.Root(transform,"Page "+i);UICraft.Label(nav.Pages[i],"Page "+i,0,0,600,100,60);}nav.Go(0,true);Component=nav;break;
'''
 if 'case Kind.LoadingBox:' not in s:s=s.replace(marker,additions+marker)
 p.write_text(s,encoding='utf-8')
 print('Registered',36+len(ENTRIES),'gallery entries')
if __name__=='__main__':main()
