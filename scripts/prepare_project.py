"""Materialize the independent sample from the single canonical kit implementation.

The generated Assets/ProjectTweenKit folder must not be edited. Hash equivalence is
checked by validate.py. Unity import packages are exported from this exact folder.
"""
from pathlib import Path
import argparse,hashlib,json,shutil
from inventory import KIT,sha,write

def stable_meta(p,root):
    meta=p.with_name(p.name+'.meta')
    if meta.exists():return
    guid=hashlib.md5(('unity-project-tween-kit/v1/'+p.relative_to(root).as_posix()).encode()).hexdigest()
    if p.is_dir():body='folderAsset: yes\nDefaultImporter:\n  externalObjects: {}\n  userData: \n  assetBundleName: \n  assetBundleVariant: \n'
    elif p.suffix=='.cs':body='MonoImporter:\n  externalObjects: {}\n  serializedVersion: 2\n  defaultReferences: []\n  executionOrder: 0\n  icon: {instanceID: 0}\n  userData: \n  assetBundleName: \n  assetBundleVariant: \n'
    else:body='DefaultImporter:\n  externalObjects: {}\n  userData: \n  assetBundleName: \n  assetBundleVariant: \n'
    meta.write_text('fileFormatVersion: 2\nguid: '+guid+'\n'+body,encoding='utf-8')

def main():
    ap=argparse.ArgumentParser();ap.add_argument('--source',type=Path,required=True);a=ap.parse_args();src=a.source.resolve();kit=KIT/'assets/unity-kit';project=KIT/'assets/sample-project'
    # Essential TMP shaders, settings and linked defaults, copied with original GUIDs.
    tmp=src/'Assets/TextMesh Pro'
    shutil.copytree(tmp,kit/'TextMesh Pro',dirs_exist_ok=True)
    resourceMap=json.loads((KIT/'references/resource-map.json').read_text(encoding='utf-8'))
    for p in sorted(tmp.rglob('*')):
        if p.is_file() and p.suffix!='.meta':
            q=kit/'TextMesh Pro'/p.relative_to(tmp);resourceMap.append({'source':p.relative_to(src).as_posix(),'target':q.relative_to(KIT).as_posix(),'sha256':sha(q),'reason':'Original TMP essentials: shaders, includes, defaults and serialized GUID closure','license':'Unity package license','licenseEvidence':'assets/unity-kit/ThirdPartyLicenses/Unity-UGUI.md'})
    licenseSource=src/'Library/PackageCache/com.unity.ugui/LICENSE.md'
    if licenseSource.exists():
        target=kit/'ThirdPartyLicenses/Unity-UGUI.md';target.parent.mkdir(parents=True,exist_ok=True);shutil.copy2(licenseSource,target)
    unique={}
    for item in resourceMap:
        key=item['target']
        if key in unique:
            if unique[key]['sha256']!=item['sha256']:raise RuntimeError('Conflicting resource identity: '+key)
            continue
        unique[key]=item
    write(KIT/'references/resource-map.json',list(unique.values()))
    for root in [kit,project/'Assets/Gallery',project/'Assets/Editor']:
        for p in sorted(root.rglob('*')):
            if p.suffix!='.meta':stable_meta(p,root)
    generated=project/'Assets/ProjectTweenKit';shutil.copytree(kit,generated,dirs_exist_ok=True)
    # Synchronize stale generated files too; all mutation is confined to this known derived folder.
    generatedRoot=generated.resolve()
    for p in generated.rglob('*'):
        if p.is_file() and not (kit/p.relative_to(generated)).exists() and not p.relative_to(generated).parts[0] in ['Examples','Presets']:
            if not p.resolve().is_relative_to(generatedRoot):raise RuntimeError('Unsafe materialization path')
            p.unlink()
    for name in ['manifest.json','packages-lock.json']:
        p=src/'Packages'/name
        if p.exists():q=project/'Packages'/name;q.parent.mkdir(parents=True,exist_ok=True);shutil.copy2(p,q)
    manifest=json.loads((project/'Packages/manifest.json').read_text());manifest['dependencies']['com.unity.modules.physics']='1.0.0';write(project/'Packages/manifest.json',manifest)
    settings=project/'ProjectSettings';settings.mkdir(parents=True,exist_ok=True)
    for name in ['ProjectVersion.txt','ProjectSettings.asset','AudioManager.asset','GraphicsSettings.asset','QualitySettings.asset','TimeManager.asset','InputManager.asset','TagManager.asset','Physics2DSettings.asset']:
        p=src/'ProjectSettings'/name
        if p.exists():shutil.copy2(p,settings/name)
    write(project/'Assets/Resources/ProjectTweenKit/prompts.json',json.loads((KIT/'references/prompt-library.json').read_text(encoding='utf-8')))
    write(KIT/'references/materialization.json',{'canonical':'assets/unity-kit','generated':'assets/sample-project/Assets/ProjectTweenKit','files':{p.relative_to(kit).as_posix():sha(p) for p in sorted(kit.rglob('*')) if p.is_file()}})
    print('Prepared clean project:',project)
if __name__=='__main__':main()
