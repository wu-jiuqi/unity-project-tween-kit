"""Compare all 96 reference recipes without importing its art or adding unrelated UI."""
import argparse,json,subprocess,hashlib
from collections import Counter
from pathlib import Path
from inventory import KIT,write

# Matching is by interaction purpose. The note records differences; a family match
# is not a claim of frame-for-frame parity or completion of the original game audit.
DECISIONS={
 '01.01':('covered','victory,failure,logo','已有页面入场编排；保持项目既有节奏'),
 '01.02':('partial','modal,pagenav','已有弹窗退出和页面横移；未另做通用整页退出'),
 '01.03':('added','pagenav','补项目实际0.27秒横移，保留项目设计而非照抄参考交叉淡化'),
 '01.04':('partial','modal','子页面通过宿主弹窗合同接入，没有另建导航历史栈'),
 '01.05':('excluded','','没有卡片详情展开场景；真实容器迁移已由目标开场覆盖'),
 '01.06':('partial','hintfade','本轮只补非交互提示组；完整HUD与按钮输入门控未提取'),
 '01.07':('added','stagger','补原商店0.20秒入场和每项0.018秒错峰'),
 '01.08':('pending','','安全区布局以即时正确为先，未添加布局迁移动画'),
 '02.01':('covered','modal','项目实际弹窗入场'),
 '02.02':('covered','modal','项目实际关闭与输入门控合同'),
 '02.03':('excluded','','当前玩家界面没有侧边抽屉'),
 '02.04':('partial','modal','设置面板已有开合；不额外改成底部抽屉'),
 '02.05':('excluded','','当前玩家界面无下拉展开场景；展馆筛选控件不算游戏效果'),
 '02.06':('excluded','','不为触屏游戏新增悬停提示玩法'),
 '02.07':('partial','modal','复用当前子页开合，内容替换与页面历史由宿主处理'),
 '02.08':('excluded','','当前没有拒绝关闭的交互规则'),
 '03.01':('excluded','','没有需提取的通用展开容器'),
 '03.02':('excluded','','没有需提取的通用折叠容器'),
 '03.03':('excluded','','内容布局由宿主负责，未新增内容驱动尺寸效果'),
 '03.04':('excluded','','没有可拖动分隔条'),
 '03.05':('covered','taskarrival','目标卡的到达脉冲与完成印章'),
 '03.06':('added','pagenav','页面内容随实际轨道横移替换；背景另有themedissolve'),
 '03.07':('excluded','','不为展示新增空背包/列表填充业务'),
 '03.08':('excluded','','用户明确排除卡片翻牌'),
 '04.01':('excluded','','当前页签采用原有底板，不新增下划线视觉'),
 '04.02':('excluded','','没有玩家键盘焦点框需求'),
 '04.03':('partial','pagenav','页面切换可复用横移；商店栏目重建不冒充已做交叉切换'),
 '04.04':('excluded','','无步骤连接线控件'),
 '04.05':('excluded','','无面包屑导航'),
 '04.06':('pending','','原活动栏Settle强制page=0，不能据此承诺完整分页'),
 '04.07':('pending','','商店筛选当前是即时重建，未另加标签过渡'),
 '04.08':('partial','breath','已补视觉呼吸；地图节点选中整套仍待提取'),
 '05.01':('covered','press','原UIBounce悬停进入'),
 '05.02':('covered','press','原UIBounce移出归位'),
 '05.03':('covered','press','原UIBounce按下压缩'),
 '05.04':('covered','press','原UIBounce松开回弹'),
 '05.05':('covered','press','真实按下释放驱动反馈，不叠加第二份按钮缩放'),
 '05.06':('covered','press','拖出取消点击'),
 '05.07':('partial','press','禁用清理归位已验证；没有新加禁用形变曲线'),
 '05.08':('excluded','','没有长按蓄力UI，不把游戏瞄准改成该交互'),
 '06.01':('covered','switch','项目开关滑动与read/write合同'),
 '06.02':('excluded','','当前没有独立复选框动画需求'),
 '06.03':('excluded','','当前没有单选表单动画需求'),
 '06.04':('excluded','','没有玩家输入表单聚焦动效需求'),
 '06.05':('excluded','','没有玩家输入表单失焦动效需求'),
 '06.06':('excluded','','不引入表单校验失败流程'),
 '06.07':('excluded','','不引入表单校验成功流程'),
 '06.08':('excluded','','设置音量滑块沿用原控件，不新增滑块气泡'),
 '07.01':('added','stagger','复用实际商品入场；排行已有leaguejelly错峰'),
 '07.02':('covered','leaguejelly','排行新行有原果冻入场；不新增背包插入业务'),
 '07.03':('excluded','','当前排行删行直接移除，不虚构原有删行补间'),
 '07.04':('added','rankmove','按稳定身份平滑移动到宿主新名次'),
 '07.05':('pending','','筛选后重建列表仍由宿主负责，尚未提取变化编排'),
 '07.06':('pending','','暂不把原单页活动栏包装成完整滚动吸附'),
 '07.07':('pending','','地图边界回弹是实际候选，尚未拆出独立示例'),
 '07.08':('excluded','','FocusPlayer当前即时定位，保持原行为'),
 '08.01':('covered','drag,activity','已有按住时视觉反馈；不提供物品拾取业务'),
 '08.02':('covered','drag','欢迎语真实指针拖动'),
 '08.03':('excluded','','没有背包/物品有效放置槽'),
 '08.04':('excluded','','没有物品无效放置槽'),
 '08.05':('excluded','','没有合法落点吸附玩法'),
 '08.06':('covered','drag','原弹簧释放归位；非背包落点校验'),
 '08.07':('excluded','','不增加拖拽排序业务'),
 '08.08':('covered','drag,press','取消、禁用和失焦归位'),
 '09.01':('covered','number','单段滚分'),
 '09.02':('covered','scoregain,number','增量合并与数值显示；钱包业务由宿主负责'),
 '09.03':('pending','','BossHUD只有原血量追赶，机制HUD不直接计入当前主线交付'),
 '09.04':('excluded','','当前主线没有治疗填充UI'),
 '09.05':('excluded','','没有跨级经验系统'),
 '09.06':('pending','','联赛倒计时存在，但临界强调不是现有已交付效果'),
 '09.07':('covered','loading,startuploading','未知等待期间只播装饰，进度不按时间虚增'),
 '09.08':('added','loadingprogress,startuploading','补微信启动层原进度与完成信号门控'),
 '10.01':('pending','','当前Toast是静态定时提示，未把参考动效冒充项目原实现'),
 '10.02':('covered','taskarrival,rating,medal','目标完成、评级与奖励表现'),
 '10.03':('partial','loadingprogress','中断保持和错误文字有示例；未新增错误抖动'),
 '10.04':('partial','moves','步数变化有反馈；低步数声音不冒充视觉警告'),
 '10.05':('pending','','主页角标跳动尚未单独提取'),
 '10.06':('added','loadingprogress,startuploading','模拟中断/恢复与明确完成；网络重试由宿主执行'),
 '10.07':('added','guide','原手势、触点与目标强调；移除自动瞄准和存档'),
 '10.08':('partial','goal,guide','目标坐标由宿主更新；不另建悬停提示跟随系统'),
 '11.01':('covered','victory','原奖励卡分层出现，不使用翻牌'),
 '11.02':('covered','victory,charge','原星级与星星充能'),
 '11.03':('pending','','鱼干箱开盖是项目场景候选；不导入参考宝箱皮肤'),
 '11.04':('covered','reward','实际收集物飞向HUD'),
 '11.05':('covered','goal','真实目标容器归位；不新增成就业务'),
 '11.06':('excluded','','没有经验升级强调场景'),
 '11.07':('added','rankmove','原排行行位移，最终名次为输入'),
 '11.08':('partial','victory','组件已有Finish，展馆未单列完整跳过结算流程'),
 '12.01':('added','breath,homefeedback','补原徽章呼吸与欢迎语分层呼吸'),
 '12.02':('covered','float','原装饰猫正弦漂浮'),
 '12.03':('partial','victory','组合有旋转/光环编排，未新增孤立恒速转圈图标'),
 '12.04':('added','pendulum','原星星挂件四子步视觉弹簧'),
 '12.05':('covered','breath','同一呼吸组件可绑定项目现有光环；不另造素材'),
 '12.06':('added','loadingprogress','原进度填充内的循环扫光与端点弹珠'),
 '12.07':('excluded','','无闭合路径巡游场景'),
 '12.08':('excluded','','触屏项目未使用指针视差，不新增该交互'),
}

def main():
    ap=argparse.ArgumentParser();ap.add_argument('--reference',type=Path,required=True);a=ap.parse_args()
    ref='origin/codex/recipe-gallery-96';path='Assets/TweenGallery/Resources/recipe-catalog.json'
    commit=subprocess.check_output(['git','rev-parse',ref],cwd=a.reference,text=True).strip()
    raw=subprocess.check_output(['git','show',commit+':'+path],cwd=a.reference)
    recipes=json.loads(raw.decode('utf-8-sig'))['recipes'];ids={r['id'] for r in recipes}
    if len(recipes)!=96 or ids!=set(DECISIONS):raise RuntimeError('Reference recipes changed; review the matrix')
    entries=[{'id':r['id'],'name':r['name'],'status':DECISIONS[r['id']][0],'galleryIds':DECISIONS[r['id']][1].split(',') if DECISIONS[r['id']][1] else [],'reason':DECISIONS[r['id']][2]} for r in recipes]
    counts=dict(Counter(e['status'] for e in entries))
    report={'referenceRepository':'https://github.com/wu-jiuqi/unity-ui-tween','referenceBranch':ref,'referenceCommit':commit,'catalogPath':path,'catalogSha256':hashlib.sha256(raw).hexdigest(),'recipesReviewed':96,'counts':counts,'artPolicy':'Only existing BounceBusters project art; no art imported from the reference repository and no new art generated.','scope':'By-purpose comparison, not a claim to reproduce all 96 reference demonstrations.','entries':entries}
    write(KIT/'references/reference96-comparison.json',report)
    labels={'covered':'已有对应家族','added':'本轮补入','partial':'部分对应','excluded':'不纳入','pending':'相关待整理'}
    lines=['# 96项配方对照与取舍','',f"参考[96项配方目录](https://github.com/wu-jiuqi/unity-ui-tween/blob/{commit}/{path})，固定提交`{commit}`。96项位于`codex/recipe-gallery-96`分支；没有用默认分支的旧目录替代。",'',
           '按用途比对，不照搬参考素材或把96个场景当成96个独立算法。保留项目原曲线、时序和交互。', '', '本轮新增10个展馆条目，共36项。全部使用现有项目素材；没有导入参考仓库美术，也没有新增AI美术。微信启动进度原来位于Tools/Platform/WeChat，因此与主Assets扫描分开保留来源证据。','',
           '分类：'+ '；'.join(labels[k]+str(v)+'项' for k,v in counts.items())+'。其中“已有”与“补入”表示用途/家族对应，具体差异见逐行说明。','',
           '| 编号 | 参考效果 | 处理 | 对应条目 | 原因与边界 |','|---|---|---|---|---|']
    for e in entries:lines.append(f"| {e['id']} | {e['name']} | {labels[e['status']]} | {', '.join(e['galleryIds']) or '—'} | {e['reason']} |")
    (KIT/'references/reference96-comparison.md').write_text('\n'.join(lines)+'\n',encoding='utf-8')
    print(json.dumps({'reviewed':96,'counts':counts,'referenceCommit':commit},ensure_ascii=False))

if __name__=='__main__':main()
