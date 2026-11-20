"""Render parameter baselines from the same notes used by the gallery."""
import json,re
from inventory import KIT

def main():
 data=json.loads((KIT/'references/parameter-catalog.json').read_text(encoding='utf-8'))
 titles=dict(re.findall(r'\bE\("([a-z]+)","([^"]+)"',(KIT/'assets/sample-project/Assets/Gallery/Gallery.cs').read_text(encoding='utf-8-sig')))
 lines=['# 参数基准与导出','','展馆“参数显示”以当前组件字段值为准。下面是各项设计基准；固定常量只是说明，不会通过JSON覆盖当前值。导出JSON可用MotionPreset载入，目标对象引用需另行绑定。','']
 for e in data['entries']:lines += ['## '+titles[e['id']]+' · '+e['id'],'',e['notes'],'','时间来源：`'+e['timeSource']+'`。','']
 (KIT/'references/parameters.md').write_text('\n'.join(lines),encoding='utf-8')
 print('Documented',len(data['entries']),'parameter baselines')
if __name__=='__main__':main()
