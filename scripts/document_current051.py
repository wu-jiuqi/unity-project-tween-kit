"""Update maintained release docs and historical/current scope records."""
import json,subprocess
from inventory import KIT,write,sha

def main():
    for name in ['README.md','SKILL.md']:
        p=KIT/name;s=p.read_text(encoding='utf-8-sig')
        s=s.replace('当前36个展馆条目','当前44个展馆条目').replace('已提供36个可体验条目','已提供44个可体验条目')
        if 'current-051.md' not in s:
            heading='\n## 0.5.0-preview · 游戏0.5.1同步\n\n新增轻提示、共享箱体Loading、异常弹窗、五页资源门控切换、旋转遮罩充星、目标庆祝、低步数警告及七页三步教学；同步新版八帧结算猫与C版结果页。共44个展馆条目，参数导出、独立预制体与生命周期验证同步更新。旧版示例保留为兼容参考。\n\n[新版接线与提取边界](references/current-051.md) · [当前来源映射](references/current-051-deliveries.json) · [验收记录](evidence/VERIFICATION.md)\n'
            s+=heading
        p.write_text(s,encoding='utf-8')
    for name in ['integration.md','lifecycle.md','design-reuse.md','loading-progress.md','gallery-ui.md','adaptations.md','validation.md','licenses.md']:
        p=KIT/'references'/name;s=p.read_text(encoding='utf-8-sig')
        if 'current-051.md' not in s:s+='\n\n## 游戏0.5.1同步\n\n新版接口、时间驱动、共享Loading、五页资源等待、八帧结果页、原生遮罩充星及新增清理合同见[当前补充说明](current-051.md)。旧章节的3.6秒加载猫与旧材质充星说明属于0.4.0历史实现；当前charge条目和结果页以本轮代码为准，独立旧LoadingPresentation仍保留。\n'
        p.write_text(s,encoding='utf-8')
    # Source removal is evidence of absence, not a blanket claim that every old family is dead.
    prior=json.loads(subprocess.check_output(['git','show','244e492:references/source-map.json'],cwd=KIT,text=True,encoding='utf-8'))
    current=json.loads((KIT/'references/source-map.json').read_text(encoding='utf-8'));ids={f['id'] for f in current['families']}
    removed=[{'id':f['id'],'source':f['source'],'class':f['class'],'previousStatus':f['usageStatus'],'status':'absent-from-current-source-inventory; historical snapshot retained'} for f in prior['families'] if f['id'] not in ids]
    write(KIT/'references/source-transition-051.json',dict(date='2026-11-27',previousCommit='244e492d4666b757169a62f9090035b0ff723702',previousGroups=len(prior['families']),currentGroups=len(current['families']),removedGroups=removed,policy='Only current inventory counts toward current coverage. Historical snapshots and reusable old presets remain explicitly versioned.'))
    p=KIT/'references/acceptance.json';acceptance=json.loads(p.read_text(encoding='utf-8'));acceptance['workingVersion']='0.5.1';acceptance['kitVersion']='0.5.0-preview';acceptance['deliveredGalleryEntries']=44
    acceptance['stageStatus']['4']='44-entry gallery with current 0.5.1 slices, compatible parameter export and versioned legacy references; project-wide coverage remains partial'
    acceptance['currentScope']={'date':'2026-11-27','sourceWorkingTreeFingerprintsSha256':sha(KIT/'references/source-fingerprints.json'),'sourceMappings':'current-051-deliveries.json','runtimeEvidence':'../evidence/runtime-verification.json','independentImportEvidence':'../evidence/clean-import-result.json','fullProjectCoverage':False}
    write(p,acceptance)
    print('Updated 0.5.1 scope docs; removed historical groups:',len(removed))

if __name__=='__main__':main()
