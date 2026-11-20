"""Snapshot the actual working tree, preserving syntax evidence and explicit uncertainty.

Requires a Unity 6 editor (bundled Roslyn and .NET). No pip dependencies.
"""
import argparse, hashlib, json, pathlib, re, shutil, subprocess

KIT = pathlib.Path(__file__).resolve().parents[1]
def sha(p): return hashlib.sha256(p.read_bytes()).hexdigest()
def write(p, data):
    p.parent.mkdir(parents=True, exist_ok=True)
    p.write_text(json.dumps(data, ensure_ascii=False, indent=2)+'\n', encoding='utf-8')

def main():
    ap=argparse.ArgumentParser();ap.add_argument('--source',type=pathlib.Path,required=True);ap.add_argument('--unity',type=pathlib.Path,default=pathlib.Path('D:/Unity/Editor'));a=ap.parse_args()
    project=a.source.resolve();work=KIT/'work';work.mkdir(exist_ok=True)
    runtime=next((a.unity/'Data/NetCoreRuntime/shared/Microsoft.NETCore.App').iterdir())
    roslyn=a.unity/'Data/DotNetSdkRoslyn';dotnet=a.unity/'Data/NetCoreRuntime/dotnet.exe'
    refs=[f'-r:"{p}"' for p in runtime.glob('*.dll') if (p.name.startswith('System.') and '.Native' not in p.name) or p.name in ['netstandard.dll','mscorlib.dll','Microsoft.CSharp.dll']]+[f'-r:"{roslyn/n}"' for n in ['Microsoft.CodeAnalysis.dll','Microsoft.CodeAnalysis.CSharp.dll']]
    rsp=work/'scanner.rsp';rsp.write_text('\n'.join(['-nologo','-target:exe',f'-out:"{work}/scanner.dll"',*refs,f'"{KIT}/scripts/SourceInventory.cs"']),encoding='utf-8')
    subprocess.run([str(dotnet),str(roslyn/'csc.dll'),'@'+str(rsp)],check=True)
    for n in ['Microsoft.CodeAnalysis.dll','Microsoft.CodeAnalysis.CSharp.dll']:shutil.copy2(roslyn/n,work/n)
    write(work/'scanner.runtimeconfig.json',{'runtimeOptions':{'tfm':'net6.0','framework':{'name':'Microsoft.NETCore.App','version':runtime.name}}})
    metadata=[p for p in runtime.glob('*.dll') if (p.name.startswith('System.') and '.Native' not in p.name) or p.name in ['netstandard.dll','mscorlib.dll','Microsoft.CSharp.dll']]
    metadata+=list((a.unity/'Data/Managed/UnityEngine').glob('*.dll'))
    metadata+=list((a.unity/'Data/Managed').glob('Unity*.dll'))
    metadata+=list((project/'Library/ScriptAssemblies').glob('Unity*.dll'))
    metadata+=list((project/'Assets/Plugins').rglob('*.dll'))
    (work/'metadata.txt').write_text('\n'.join(str(p) for p in metadata),encoding='utf-8')
    subprocess.run([str(dotnet),str(work/'scanner.dll'),str(project),str(work/'syntax.json'),str(work/'metadata.txt')],check=True)
    files=json.loads((work/'syntax.json').read_text(encoding='utf-8-sig'))
    # Conservative candidate discovery across tween, coroutine, frame, curve, mesh and material implementations.
    motion=re.compile(r'DOTween|\bDO(?:Scale|Fade|Move|LocalMove|Rotate|LocalRotate|AnchorPos|Color|Punch|Shake)|\b(?:IEnumerator|AnimationCurve|WaitForSeconds|SmoothDamp)\b|Mathf\.(?:Sin|Cos|Exp|Lerp|SmoothStep|MoveTowards|PerlinNoise)|Vector[234]\.Lerp|Time\.(?:unscaledTime|time|deltaTime|unscaledDeltaTime)|SetFloat\(|SetVerticesDirty\(|fillAmount\s*=|maxVisibleCharacters\s*=')
    calls={};types={};semantic={}
    for f in files:
        for m in f['members']:
            caller={'path':f['path'],'class':m['owner'],'method':m['name'],'line':m['line']}
            for c in m['calls']:
                key=re.sub(r'<.*>','',c['expression']).split('.')[-1].lstrip('?')
                calls.setdefault(key,[]).append(dict(caller,callLine=c['line'],expression=c['expression'],arguments=c['arguments']))
                if c.get('sourcePath'):semantic.setdefault((c['sourcePath'],c['sourceLine']),[]).append(dict(caller,callLine=c['line'],expression=c['expression'],arguments=c['arguments'],callee=c['callee']))
            for c in m['types']:types.setdefault(c['type'].strip('<>'),[]).append(dict(caller,callLine=c['line'],operation=c['name']))
    families=[];excluded=[]
    for f in files:
        path=f['path'];snapshot=KIT/'references/source'/path
        snapshot=snapshot.with_suffix('.cs.txt');snapshot.parent.mkdir(parents=True,exist_ok=True);shutil.copy2(project/path,snapshot)
        category='editor-tool' if '/Editor/' in path else 'test-fixture' if re.search(r'(?:QA|Test|Recorder|Research|Survey|BalanceRunner|LevelEditor|Preview|Demo|Audit|FrameStats)',path) else 'runtime-candidate'
        found=[m for m in f['members'] if motion.search(m['source']) or ('Animation' in path and m['name'] in ['FrameAt','Get'])]
        if not found:
            excluded.append({'path':path,'sha256':f['sha256'],'classification':category if category!='runtime-candidate' else 'no-motion-pattern'});continue
        # Family = source class, sites are separate. Partial classes keep source-file grouping to preserve context.
        for owner in sorted(set(m['owner'] for m in found)):
            ms=[m for m in found if m['owner']==owner]
            ident=re.sub(r'[^a-z0-9]+','-',path.replace('Assets/Scripts/','').replace('.cs','').lower()+'-'+owner.lower()).strip('-')
            lifecycle=[{'method':m['name'],'line':m['line'],'source':m['source']} for m in f['members'] if m['owner']==owner and re.search(r'OnDisable|OnDestroy|OnApplication|Stop|Kill|Reset|Restore|Cancel|Pause',m['name'])]
            for m in ms:
                m['timeSources']=sorted(set(re.findall(r'Time\.\w+|WaitForSecondsRealtime|WaitForSeconds|SetUpdate\([^)]*\)|PresentationDelta',m['source'])))
                m['curveEvidence']=sorted(set(re.findall(r'Ease\.\w+|Mathf\.(?:Sin|Cos|Exp|Pow|SmoothStep|LerpUnclamped)|AnimationCurve|SmoothDamp',m['source'])))
                m['hierarchyCalls']=[c for c in m['calls'] if re.search(r'(?:Root|Box|Art|Image|Label|SetParent|SetSiblingIndex|Find|GetChild)$',c['expression'])]
                m['engineCallback']=m['name'] in ['Awake','Start','Update','LateUpdate','FixedUpdate','OnEnable','OnDisable','OnDestroy','OnPopulateMesh','ModifyMesh']
                m['resolvedCallSites']=semantic.get((path,m['line']),[])
            family={'id':ident,'class':owner,'source':path,'fileSha256':f['sha256'],'snapshot':snapshot.relative_to(KIT).as_posix(),'classification':category,
                'usageStatus':'requires-route-review' if category=='runtime-candidate' else category,'deliveryStatus':'source-evidence-only',
                'methods':ms,'constructionSites':types.get(owner.split('.')[-1],[]),'nameMatchedCallSites':{m['name']:calls.get(m['name'],[]) for m in ms},'lifecycleEvidence':lifecycle,
                'parametersEvidence':[{'name':m['name'],'line':m['line'],'source':m['source']} for m in f['members'] if m['owner']==owner and m['kind']=='FieldDeclaration'],
                'resourceEvidence':[{'method':m['name'],'line':m['line'],'calls':[c for c in m['calls'] if re.search(r'Resources\.Load|\.Get$|\.Play$|\.Image$|\.Art$|Sprite\.Create|new Material',c['expression'])]} for m in f['members'] if m['owner']==owner],
                'limitations':['Call sites matched by method name are candidates, not semantic resolution.','Static construction does not prove that a current page enables the branch.','Dynamic resource strings and serialized references require route review.']}
            families.append(family)
    assets=[]
    for p in sorted((project/'Assets').rglob('*')):
        if not p.is_file() or p.suffix in ['.meta','.cs'] or any(s in p.parts for s in ['Editor','Scripts']):continue
        meta=p.with_name(p.name+'.meta');guid=re.search(r'^guid: (\w+)',meta.read_text(encoding='utf-8-sig'),re.M) if meta.exists() else None
        assets.append({'source':p.relative_to(project).as_posix(),'sha256':sha(p),'bytes':p.stat().st_size,'guid':guid[1] if guid else None,'usage':'unresolved-until-resource-closure','redistribution':'consult-license-evidence'})
    baseline=project.parents[1]/'DEVELOPMENT_BASELINE.json'
    write(KIT/'references/source-map.json',{'schemaVersion':1,'sourceProject':'BounceGarden/BounceGarden','sourceVersion':(project/'ProjectSettings/ProjectVersion.txt').read_text(),'baseline':json.loads(baseline.read_text(encoding='utf-8')) if baseline.exists() else None,'evidencePolicy':'Conservative syntax candidates plus partial semantic call resolution. catalog.py applies explicit source-route reviews; unknown is never promoted to active.','families':families,'nonAnimationFiles':excluded,'disabledBranches':[{'path':f['path'],'disabled':f['disabled']} for f in files if f['disabled']]})
    write(KIT/'references/resource-inventory.json',assets)
    # Entire file manifest detects source drift even in non-animation dependencies.
    write(KIT/'references/source-fingerprints.json',{f['path']:f['sha256'] for f in files})
    print(json.dumps({'files':len(files),'families':len(families),'runtimeCandidates':sum(x['classification']=='runtime-candidate' for x in families),'assets':len(assets)},ensure_ascii=False))
if __name__=='__main__':main()
