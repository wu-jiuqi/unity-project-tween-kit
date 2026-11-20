"""Build the four reviewable preview artifacts with deterministic file ordering."""
from pathlib import Path
import json,zipfile,shutil,subprocess
from inventory import KIT,write,sha
IGNORED={'Library','Logs','Temp','Obj','obj','UserSettings','.git','work','dist','__pycache__'}
def zipfiles(path,items):
    with zipfile.ZipFile(path,'w',zipfile.ZIP_DEFLATED,compresslevel=6) as z:
        for p,name in sorted(items,key=lambda x:x[1]):z.write(p,name)
def files(root):return [p for p in root.rglob('*') if p.is_file() and not any(x in IGNORED for x in p.relative_to(root).parts)]
def main():
    subprocess.run(['python',str(KIT/'scripts/validate.py')],check=True)
    dist=KIT/'dist';version='0.3.0-preview';skill=dist/f'unity-project-tween-kit-{version}.zip';sample=dist/f'TweenKit-SampleProject-{version}.zip';windows=dist/f'TweenKit-Windows-{version}.zip'
    selected=[KIT/'SKILL.md',KIT/'README.md']
    for folder in ['references','scripts','assets/unity-kit','assets/sample-project','evidence']:selected+=files(KIT/folder)
    # The default skill includes the independent sample without Library caches.
    zipfiles(skill,[(p,'unity-project-tween-kit/'+p.relative_to(KIT).as_posix()) for p in set(selected)])
    zipfiles(sample,[(p,'TweenKitSample/'+p.relative_to(KIT/'assets/sample-project').as_posix()) for p in files(KIT/'assets/sample-project')]+[(KIT/'references/integration.md','TweenKitSample/INTEGRATION.md')])
    zipfiles(windows,[(p,'TweenGallery/'+p.relative_to(dist/'Windows').as_posix()) for p in files(dist/'Windows')]+[(p,'TweenGallery/evidence/'+p.relative_to(KIT/'evidence').as_posix()) for p in files(KIT/'evidence')]+[(KIT/'README.md','TweenGallery/README.md'),(KIT/'references/acceptance.json','TweenGallery/acceptance.json')])
    artifacts=[skill,sample,windows,dist/'ProjectTweenKit.unitypackage']
    write(dist/'release-manifest.json',{'version':version,'releaseState':'preview-partial-coverage','acceptanceSha256':sha(KIT/'references/acceptance.json'),'artifacts':[{'name':p.name,'bytes':p.stat().st_size,'sha256':sha(p)} for p in artifacts],'testedPlayerFiles':json.loads((KIT/'evidence/player-run.json').read_text(encoding='utf-8'))['binaryFilesSha256'],'runtimeReportSha256':sha(KIT/'evidence/runtime-verification.json'),'integrityReportSha256':sha(KIT/'evidence/integrity-verification.json')})
    print('\n'.join(str(p)+' '+str(round(p.stat().st_size/1048576,2))+' MiB' for p in artifacts))
if __name__=='__main__':main()
