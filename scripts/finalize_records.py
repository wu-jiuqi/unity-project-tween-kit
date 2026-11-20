"""Refresh review metadata without altering any tested runtime input."""
import json
from pathlib import Path
from inventory import KIT,write,sha
def main():
    path=KIT/'references/resource-map.json';data=json.loads(path.read_text(encoding='utf-8'));unique={}
    for item in data:
        key=item['target']
        if key in unique:
            if unique[key]['sha256']!=item['sha256']:raise RuntimeError('Conflicting source mapping: '+key)
            continue
        p=KIT/key;meta=p.with_name(p.name+'.meta')
        if meta.exists():item['packagedMetaSha256']=sha(meta)
        unique[key]=item
    write(path,list(unique.values()))
    path=KIT/'references/source-map.json';source=json.loads(path.read_text(encoding='utf-8'));source['evidencePolicy']='Conservative syntax candidates plus partial semantic call resolution. Explicit source-route reviews are applied by catalog.py; unknown is never promoted to active.';write(path,source)
    runtime=json.loads((KIT/'evidence/runtime-verification.json').read_text(encoding='utf-8-sig'));clean=json.loads((KIT/'evidence/clean-import-result.json').read_text(encoding='utf-8-sig'));summary=json.loads((KIT/'references/review-summary.json').read_text(encoding='utf-8'))
    acceptance=json.loads((KIT/'references/acceptance.json').read_text(encoding='utf-8'));acceptance['runtimeChecks']={'passed':runtime['passed'],'failed':runtime['failed'],'errors':len(runtime['errors'])};acceptance['independentImport']=clean;acceptance['sourceReviewCounts']=summary['counts'];write(KIT/'references/acceptance.json',acceptance)
    text=f'''# 阶段验证记录

此包尚未完成全量覆盖验收。19个展馆条目有可运行交付；来源清单的待审阅项不计作已完成动画。

- 玩家程序：Unity {runtime['unityVersion']}，{runtime['platform']}；{runtime['passed']}项通过、{runtime['failed']}项失败、{len(runtime['errors'])}个运行错误。
- 独立空工程：只导入导出的unitypackage，{clean['scripts']}个脚本、{clean['prefabs']}个组合预制体，{clean['missingScripts']}个缺失脚本；结果{clean['result']}。
- 自动验证覆盖：指针按压/拖拽取消点击、禁用/失焦归位、全部示例时钟暂停、弹窗开合重入及非等比scale、即时与正常结果一致、保存/载入及类型拒绝、重播保留调参、时间轴冻结与续播、开关write恰好一次、借入容器归还、Logo重入及非均匀帧时长。
- 独立导入的预制体构建在Editor中执行；未把它写成完整的独立玩家流程测试，也未执行真人5分钟首次接入计时。
- 真实被测文件集、退出码和运行报告哈希见[player-run.json](player-run.json)；来源与展馆代码身份见[build-inputs.json](build-inputs.json)；导入包身份见[clean-import-run.json](clean-import-run.json)。
- 隐藏窗口ScreenCapture全黑，未作为视觉通过或发布预览图。可见原生窗口的检查证据另放visual目录；工具调用中的可见窗口观察与自动事件派发是两种不同证据。

当前还有{summary['counts'].get('requires-route-review',0)}个源分组待核对入口，另有混合物理/表现、旧导航和预览类单列。完整后续工作见[验收状态](../references/acceptance.json)。
'''
    (KIT/'evidence/VERIFICATION.md').write_text(text,encoding='utf-8')
    print('Finalized metadata; full acceptance remains false')
if __name__=='__main__':main()
