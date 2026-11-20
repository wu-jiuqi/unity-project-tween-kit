"""Capture generated prefabs/presets and the exact successful build source set."""
import json,shutil
from pathlib import Path
from inventory import KIT,write,sha
from prepare_project import stable_meta
def main():
    result=json.loads((KIT/'dist/build-result.json').read_text(encoding='utf-8-sig'))
    if result['result']!='Succeeded' or result['errors']:raise RuntimeError('Build did not succeed')
    canonical=KIT/'assets/unity-kit';generated=KIT/'assets/sample-project/Assets/ProjectTweenKit'
    for folder in ['Examples','Presets']:
        shutil.copytree(generated/folder,canonical/folder,dirs_exist_ok=True)
        m=generated/(folder+'.meta')
        if m.exists():shutil.copy2(m,canonical/m.name)
    for p in canonical.rglob('*'):
        if p.suffix!='.meta':stable_meta(p,canonical)
    # Metadata newly materialized for default preset JSONs is propagated before comparing.
    shutil.copytree(canonical,generated,dirs_exist_ok=True)
    write(KIT/'references/materialization.json',{'canonical':'assets/unity-kit','generated':'assets/sample-project/Assets/ProjectTweenKit','files':{p.relative_to(canonical).as_posix():sha(p) for p in sorted(canonical.rglob('*')) if p.is_file()}})
    evidence=KIT/'evidence';evidence.mkdir(exist_ok=True);shutil.copy2(KIT/'dist/build-result.json',evidence/'build-result.json')
    write(evidence/'build-inputs.json',{'sourceFingerprintsSha256':sha(KIT/'references/source-fingerprints.json'),'kit':{p.relative_to(canonical).as_posix():sha(p) for p in sorted(canonical.rglob('*.cs'))},'gallery':{p.relative_to(KIT).as_posix():sha(p) for p in sorted((KIT/'assets/sample-project/Assets/Gallery').glob('*.cs'))}})
    print('Captured successful build, generated examples and source identity')
if __name__=='__main__':main()
