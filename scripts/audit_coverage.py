"""Audit main and platform source surfaces, keeping unreviewed groups explicit."""
import argparse,json,re
from collections import Counter
from pathlib import Path
from inventory import KIT,write,sha
PATTERNS={
 'DOTween':r'\bDOTween\b|\bDO(?:Scale|Fade|Move|LocalMove|Rotate|LocalRotate|AnchorPos|Color|Punch|Shake|Jump|Path)\w*\s*\(',
 'coroutine':r'\bIEnumerator\b|\byield\s+return\b',
 'frame-clock':r'\bTime\.(?:deltaTime|unscaledDeltaTime|time|unscaledTime)\b|PresentationDelta\s*\(',
 'curve-spring':r'AnimationCurve|SmoothDamp|Mathf\.(?:Sin|Cos|Exp|Pow|Lerp|SmoothStep|MoveTowards)|Vector[234]\.Lerp',
 'text-material-frame':r'maxVisibleCharacters|\.SetFloat\s*\(|\.SetColor\s*\(|fillAmount\s*=|ForceMeshUpdate|FrameAt\s*\(',
 'other-animation-api':r'\b(?:LeanTween|iTween|Animator|Animation|PlayableDirector)\b',
}
def signals(p):
 code=p.read_text(encoding='utf-8-sig');return [k for k,v in PATTERNS.items() if re.search(v,code)]
def main():
 ap=argparse.ArgumentParser();ap.add_argument('--source',type=Path,required=True);a=ap.parse_args();src=a.source.resolve()
 data=json.loads((KIT/'references/source-map.json').read_text(encoding='utf-8'));fingerprints=json.loads((KIT/'references/source-fingerprints.json').read_text(encoding='utf-8'));scanned=[]
 for rel,h in fingerprints.items():
  p=src/rel
  if not p.exists() or sha(p)!=h:raise RuntimeError('Source inventory is stale: '+rel)
  scanned.append({'path':rel,'sha256':h,'signals':signals(p)})
 platform=src.parents[1]/'Tools/Platform/WeChat'
 platformFiles=[{'path':p.relative_to(src.parents[1]).as_posix(),'sha256':sha(p),'signals':signals(p)} for p in sorted(platform.glob('*.cs'))]
 serialized=[]
 for p in sorted((src/'Assets').rglob('*')):
  if p.is_file() and p.suffix in {'.unity','.prefab','.anim','.controller','.overrideController','.playable'}:
   text=p.read_text(encoding='utf-8-sig',errors='replace');serialized.append({'path':p.relative_to(src).as_posix(),'sha256':sha(p),'kind':p.suffix,'animationSignals':sorted(set(re.findall(r'Animator:|Animation:|m_AnimationClips:|m_Controller:|PlayableDirector:|m_Curve:',text)))})
 families=data['families'];remaining=[f for f in families if f['usageStatus']=='requires-route-review'];extra=json.loads((KIT/'references/supplemental-deliveries.json').read_text(encoding='utf-8'))
 extra["entries"]+=json.loads((KIT/"references/current-051-deliveries.json").read_text(encoding="utf-8"))["entries"]
 report={'schemaVersion':2,'date':'2026-11-27','sourceInventorySha256':sha(KIT/'references/source-fingerprints.json'),'sourceFiles':len(scanned),'sourceSignals':dict(Counter(s for f in scanned for s in f['signals'])),'platformSourceFiles':platformFiles,'serializedKinds':dict(Counter(f['kind'] for f in serialized)),'serializedFiles':serialized,'candidateGroups':len(families),'runtimeCandidateGroups':sum(f['classification']=='runtime-candidate' for f in families),'usageStates':dict(Counter(f['usageStatus'] for f in families)),'addedGalleryIds':[r['galleryEntry'] for r in extra['entries']],'addedSourceRoutes':extra['entries'],'remainingCandidates':[{'id':f['id'],'source':f['source'],'class':f['class'],'methods':[m['name'] for m in f['methods']],'status':f['usageStatus'],'additionalDeliveries':f.get('additionalDeliveries',[])} for f in remaining],'scannedFiles':scanned,'limits':['Static scans do not prove every original gameplay branch.','Partially delivered host classes remain pending group review; their confirmed slices are listed separately.','Platform startup source and resources are included explicitly rather than silently treated as main Assets source.']}
 write(KIT/'references/coverage-audit.json',report)
 lines=['# 补间覆盖复查 · 2026-11-27','',f"核对主工程{len(scanned)}个C#文件、{len(families)}个候选分组；另扫描Tools/Platform/WeChat中的{len(platformFiles)}个C#文件。主工程序列化资源：{report['serializedKinds']}。",'','微信启动Loading原先位于主Assets扫描范围之外，本轮已明确补入。HomeContentStartup真实阶段输入进度，首页首帧就绪后才发送Complete；运行示例只模拟这些事件，不加载网络资源。','','## 0.4.0兼容片段及0.5.1新增/更新','','| 设计条目 | 对应组件 | 提取边界 |','|---|---|---|']
 for r in extra['entries']:lines.append(f"| `{r['galleryEntry']}` | `{r['component']}` | {r['adaptation']} |")
 lines+=['','前一轮已补入分数变色放大、加分合并上浮、任务印章、步数挤压、评级星光、排行卡抖动与背景换景；累计44项可体验条目。参数显示与导出不包含AI Prompt。','','参考仓库96项逐条取舍见[对照表](reference96-comparison.md)。未把不相关的翻牌、背包、表单和经验系统加入工具包。','','## 待继续确认的源分组','',f'仍有{len(remaining)}个源分组需要整体核对，部分已有明确交付片段。它们包含大型宿主、业务/物理混合和可能不可达分支，不等于还有这么多个已确认在用动画遗漏。','','| 源分组 | 匹配方法 | 已交付的补充片段 |','|---|---|---|']
 for f in remaining:lines.append(f"| `{f['source']}` / {f['class']} | {', '.join(m['name'] for m in f['methods'])} | {', '.join(d['component'] for d in f.get('additionalDeliveries',[])) or '—'} |")
 lines+=['','文件指纹与平台来源见[机器记录](coverage-audit.json)及[supplemental-deliveries.json](supplemental-deliveries.json)。','']
 (KIT/'references/coverage-audit.md').write_text('\n'.join(lines),encoding='utf-8');print(json.dumps({'sourceFiles':len(scanned),'platformFiles':len(platformFiles),'newDesigns':len(extra['entries']),'remainingSourceGroups':len(remaining)},ensure_ascii=False))
if __name__=='__main__':main()
