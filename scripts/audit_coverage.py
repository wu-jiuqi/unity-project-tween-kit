"""Rescan source/serialized motion surfaces and report uncovered candidates honestly.

Run inventory.py and catalog.py first. This report distinguishes scan coverage,
reviewed source routes and delivered components; static matches are not live proof.
"""
import argparse,json,re
from collections import Counter
from pathlib import Path
from inventory import KIT,write,sha

NEW={'scoreimpact','scoregain','taskarrival','moves','rating','leaguejelly','themedissolve'}
PATTERNS={
    'DOTween':r'\bDOTween\b|\bDO(?:Scale|Fade|Move|LocalMove|Rotate|LocalRotate|AnchorPos|Color|Punch|Shake|Jump|Path|Blendable)\w*\s*\(',
    'coroutine':r'\bIEnumerator\b|\byield\s+return\b',
    'frame-clock':r'\bTime\.(?:deltaTime|unscaledDeltaTime|time|unscaledTime)\b|PresentationDelta\s*\(',
    'curve-spring':r'AnimationCurve|SmoothDamp|Mathf\.(?:Sin|Cos|Exp|Pow|Lerp|SmoothStep|MoveTowards)|Vector[234]\.Lerp',
    'text-material-frame':r'maxVisibleCharacters|\.SetFloat\s*\(|\.SetColor\s*\(|fillAmount\s*=|ForceMeshUpdate|FrameAt\s*\(',
    'other-animation-api':r'\b(?:LeanTween|iTween|Animator|Animation|PlayableDirector)\b',
}

def main():
    ap=argparse.ArgumentParser();ap.add_argument('--source',type=Path,required=True);a=ap.parse_args();src=a.source.resolve()
    data=json.loads((KIT/'references/source-map.json').read_text(encoding='utf-8'))
    fingerprints=json.loads((KIT/'references/source-fingerprints.json').read_text(encoding='utf-8'))
    scanned=[]
    for rel,h in fingerprints.items():
        p=src/rel
        if not p.exists() or sha(p)!=h:raise RuntimeError('Source inventory is stale: '+rel)
        code=p.read_text(encoding='utf-8-sig')
        scanned.append({'path':rel,'sha256':h,'signals':[k for k,v in PATTERNS.items() if re.search(v,code)]})
    serialized=[]
    for p in sorted((src/'Assets').rglob('*')):
        if not p.is_file() or p.suffix not in {'.unity','.prefab','.anim','.controller','.overrideController','.playable'}:continue
        text=p.read_text(encoding='utf-8-sig',errors='replace')
        serialized.append({'path':p.relative_to(src).as_posix(),'sha256':sha(p),'kind':p.suffix,'animationSignals':sorted(set(re.findall(r'Animator:|Animation:|m_AnimationClips:|m_Controller:|PlayableDirector:|m_Curve:',text)))})
    families=data['families'];remaining=[f for f in families if f['usageStatus']=='requires-route-review']
    added=[f for f in families if f.get('galleryEntry') in NEW]
    runtime=[f for f in families if f['classification']=='runtime-candidate']
    report={'schemaVersion':1,'date':'2026-11-20','sourceInventorySha256':sha(KIT/'references/source-fingerprints.json'),
        'sourceFiles':len(scanned),'sourceSignals':dict(Counter(s for f in scanned for s in f['signals'])),
        'serializedKinds':dict(Counter(f['kind'] for f in serialized)),'serializedFiles':serialized,
        'candidateGroups':len(families),'runtimeCandidateGroups':len(runtime),'usageStates':dict(Counter(f['usageStatus'] for f in families)),
        'addedGalleryIds':sorted(NEW),'addedSourceRoutes':[{'id':f['id'],'galleryEntry':f['galleryEntry'],'source':f['source'],'sha256':f['fileSha256'],'route':f['route']} for f in added],
        'remainingCandidates':[{'id':f['id'],'source':f['source'],'class':f['class'],'methods':[m['name'] for m in f['methods']],'constructionSites':f['constructionSites'],'status':f['usageStatus']} for f in remaining],
        'scannedFiles':scanned,
        'limits':['Static coverage is not all-gameplay-branches runtime validation.','Runtime candidates include geometry, clocks, physics and inactive branches; unreviewed entries are not claimed active.','Seven newly delivered designs cover the listed presentation slices, not complete original host systems.']}
    write(KIT/'references/coverage-audit.json',report)
    lines=['# 补间遗漏复查 · 2026-11-20','',
        f"重跑Roslyn扫描及宽模式检索，核对{len(scanned)}个C#文件的当前字节，得到{len(families)}个候选源分组（其中运行时候选{len(runtime)}个）。同时检查Assets内场景、预制体和动画/控制器/Timeline文件：{report['serializedKinds']}。",'',
        '这些数量表示扫描覆盖。当前页面是否真正启用仍需按入口确认；测试、物理、旧逻辑和未启用实验分别保留，不直接计为遗漏动画。','',
        '## 这轮确认遗漏并补入的7项','',
        '| 设计 | 已核对调用链 | 本轮交付 |','|---|---|---|']
    for f in added:lines.append(f"| {f['trigger']} | `{f['route']}` | `{f['deliveredComponent']}` / `{f['galleryEntry']}` |")
    lines+=['','分数相关现在分为：单段数字滚动、到达时变金放大、加分合并上浮。这三项用途与触发时机不同，分别体验和复制Prompt。','',
        '## 仍需继续确认或拆分的范围','',
        '还存在开章光门、地图节点/路径/拖拽回弹、主页欢迎语分层入场、联赛匹配和排行位移、连胜奖励、教学引导、弹幕、目标庆祝等候选；场景侧另有藤蔓摆动、镜头、碎片、消除和特殊机制编排。已扫描并记录，尚未把它们统称为已交付组件。',
        '',f'下面列出全部{len(remaining)}个待确认分组；它们可能含业务、物理或不可达分支。已交付的一部分也不能代表同文件其他动画全部交付。','',
        '| 源分组 | 本次匹配方法 |','|---|---|']
    for f in remaining:lines.append(f"| `{f['source']}` / {f['class']} | {', '.join(m['name'] for m in f['methods'])} |")
    lines+=['','维护证据：[逐文件扫描与剩余候选](coverage-audit.json)。展馆只展示可操作效果与迁移Prompt，不展示源码档案。','']
    (KIT/'references/coverage-audit.md').write_text('\n'.join(lines),encoding='utf-8')
    print(json.dumps({'sourceFiles':len(scanned),'newDesigns':len(NEW),'remainingCandidates':len(remaining),'serializedKinds':report['serializedKinds']},ensure_ascii=False))

if __name__=='__main__':main()
