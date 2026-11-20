"""Verify provenance, GUIDs, source identity, exported files and real test records."""
from pathlib import Path
import argparse,json,hashlib,re,tarfile
from inventory import KIT,sha,write
def main():
    ap=argparse.ArgumentParser();ap.add_argument('--source',type=Path);ap.add_argument('--require-full',action='store_true');a=ap.parse_args();checks=[]
    def check(name,ok,detail=''):checks.append({'name':name,'passed':bool(ok),'detail':detail})
    parameters=json.loads((KIT/'references/parameter-catalog.json').read_text(encoding='utf-8'))
    galleryParameters=json.loads((KIT/'assets/sample-project/Assets/Resources/ProjectTweenKit/parameters.json').read_text(encoding='utf-8'))
    check('gallery uses maintained parameter notes',parameters==galleryParameters)
    ids=re.findall(r'\bE\("([a-z]+)"',(KIT/'assets/sample-project/Assets/Gallery/Gallery.cs').read_text(encoding='utf-8-sig'))
    check('every delivered entry has parameter notes',sorted(ids)==sorted(p['id'] for p in parameters['entries']))
    check('gallery no longer ships AI prompt data',not (KIT/'assets/sample-project/Assets/Resources/ProjectTweenKit/prompts.json').exists())
    comparison=json.loads((KIT/'references/reference96-comparison.json').read_text(encoding='utf-8'))
    check('all 96 reference recipes reviewed exactly once',len(comparison['entries'])==96 and len({e['id'] for e in comparison['entries']})==96)
    check('reference matrix links only delivered examples',all(i in ids for e in comparison['entries'] for i in e['galleryIds']))
    extra=json.loads((KIT/'references/supplemental-deliveries.json').read_text(encoding='utf-8'))
    for entry in extra['entries']:
        check('supplemental snapshot '+entry['galleryEntry'],sha(KIT/entry['snapshot'])==entry['sourceSha256'])
        check('supplemental component '+entry['galleryEntry'],sha(KIT/entry['target'])==entry['targetSha256'])
        if a.source:check('supplemental source drift '+entry['galleryEntry'],sha(a.source/entry['source'])==entry['sourceSha256'])
    host=extra['platformHostEvidence'];check('platform startup host snapshot',sha(KIT/host['snapshot'])==host['sha256'])
    if a.source:check('platform startup host drift',sha(a.source/host['source'])==host['sha256'])
    check('gallery no longer packages source archive',not (KIT/'assets/sample-project/Assets/Resources/ProjectTweenKit/archive.json').exists())
    data=json.loads((KIT/'references/source-map.json').read_text(encoding='utf-8'))
    fingerprints=json.loads((KIT/'references/source-fingerprints.json').read_text(encoding='utf-8'))
    for path,h in fingerprints.items():
        p=KIT/'references/source'/(path+'.txt');check('snapshot '+path,p.exists() and sha(p)==h)
        if a.source:check('source drift '+path,(a.source/path).exists() and sha(a.source/path)==h)
    for f in data['families']:
        check('catalogue page '+f['id'],(KIT/'references/recipes'/(f['id']+'.md')).exists())
        for m in f['methods']:check('method fingerprint '+f['id']+'.'+m['name'],hashlib.sha256(m['source'].encode('utf-8')).hexdigest()==m['sha256'])
    for item in json.loads((KIT/'references/extraction-diffs.json').read_text(encoding='utf-8')):
        p=KIT/item['target'];check('extraction '+item['target'],p.exists() and sha(p)==item['targetSha256'])
    for item in json.loads((KIT/'references/resource-map.json').read_text(encoding='utf-8')):
        p=KIT/item['target'];check('resource '+item['target'],p.exists() and sha(p)==item['sha256'])
        if item.get('sourceGuid'):
            meta=p.with_name(p.name+'.meta');check('original resource GUID '+item['target'],meta.exists() and ('guid: '+item['sourceGuid']) in meta.read_text(encoding='utf-8-sig'))
        if item.get('packagedMetaSha256'):check('resource importer fingerprint '+item['target'],sha(p.with_name(p.name+'.meta'))==item['packagedMetaSha256'])
        if a.source and (item['source'].startswith('Assets/') or item.get('scope')=='project-platform'):
            original=a.source/item['source'];meta=original.with_name(original.name+'.meta');targetMeta=p.with_name(p.name+'.meta')
            check('resource matches live source '+item['target'],original.exists() and sha(original)==sha(p))
            if meta.exists():check('resource import settings unchanged '+item['target'],targetMeta.exists() and sha(meta)==sha(targetMeta))
    canonical=KIT/'assets/unity-kit';generated=KIT/'assets/sample-project/Assets/ProjectTweenKit'
    guids={}
    for p in canonical.rglob('*.meta'):
        found=re.search(r'^guid: (\w+)',p.read_text(encoding='utf-8-sig'),re.M)
        check('valid GUID '+p.relative_to(canonical).as_posix(),bool(found))
        if found:guids.setdefault(found[1],[]).append(p.relative_to(canonical).as_posix())
    check('no duplicate package GUIDs',all(len(v)==1 for v in guids.values()),str({k:v for k,v in guids.items() if len(v)>1}))
    for p in canonical.rglob('*'):
        if p.is_file():q=generated/p.relative_to(canonical);check('shared implementation '+p.relative_to(canonical).as_posix(),q.exists() and sha(p)==sha(q))
    code='\n'.join(p.read_text(encoding='utf-8-sig') for p in (canonical/'Runtime').rglob('*.cs'))
    stripped=re.sub(r'//[^\n]*|/\*.*?\*/','',code,flags=re.S)
    check('no original business assembly dependencies',not re.search(r'\b(?:GameSession|AppUI|LocalProfile|LocalMockServices|PlayerPrefs)\b',stripped))
    package=KIT/'dist/ProjectTweenKit.unitypackage'
    if package.exists():
        with tarfile.open(package,'r:gz') as archive:
            members={m.name:m for m in archive.getmembers()}
            for name,m in members.items():
                if name.endswith('/pathname'):
                    path=archive.extractfile(m).read().decode('utf-8');prefix='Assets/ProjectTweenKit/'
                    if path.startswith(prefix) and path.endswith('.cs'):
                        asset=archive.extractfile(members[name.rsplit('/',1)[0]+'/asset']).read();p=canonical/path[len(prefix):]
                        check('exported exact component '+path,p.exists() and hashlib.sha256(asset).hexdigest()==sha(p))
    else:check('Unity package exists',False)
    runPath=KIT/'evidence/player-run.json'
    if runPath.exists():
        run=json.loads(runPath.read_text(encoding='utf-8'));report=KIT/'evidence/runtime-verification.json'
        check('runtime record matches report',sha(report)==run['runtimeReportSha256'])
        check('runtime succeeded',run['exitCode']==0 and run['filesUnchanged'])
        for path,h in run['binaryFilesSha256'].items():p=KIT/'dist/Windows'/path;check('tested player '+path,p.exists() and sha(p)==h)
        inputs=json.loads((KIT/'evidence/build-inputs.json').read_text(encoding='utf-8'))
        for path,h in inputs['kit'].items():check('tested kit source '+path,sha(canonical/path)==h)
        for path,h in inputs['gallery'].items():check('tested gallery source '+path,sha(KIT/path)==h)
    else:check('player run record exists',False)
    cleanPath=KIT/'evidence/clean-import-run.json'
    if cleanPath.exists():
        clean=json.loads(cleanPath.read_text(encoding='utf-8'));report=json.loads((KIT/'evidence/clean-import-result.json').read_text(encoding='utf-8-sig'))
        check('independent import passed',clean['exitCode']==0 and report['result']=='passed' and report['missingScripts']==0)
        check('independent import matches current package',sha(package)==clean['packageSha256'])
    else:check('independent import record exists',False)
    acceptance=json.loads((KIT/'references/acceptance.json').read_text(encoding='utf-8'))
    if a.require_full:check('full requested scope accepted',acceptance['fullAcceptancePassed'])
    result={'passed':sum(c['passed'] for c in checks),'failed':sum(not c['passed'] for c in checks),'fullAcceptancePassed':acceptance['fullAcceptancePassed'],'checks':checks}
    write(KIT/'evidence/integrity-verification.json',result);print(json.dumps({k:v for k,v in result.items() if k!='checks'}))
    if a.source:write(KIT/'evidence/source-integrity-verification.json',result)
    for c in checks:
        if not c['passed']:print('FAIL',c['name'],c['detail'])
    if result['failed']:raise SystemExit(1)
if __name__=='__main__':main()
