"""Prepare a Git clone for Unity without needing the original game project."""
import shutil,json
from inventory import KIT,sha,write
from prepare_project import stable_meta
def main():
    canonical=KIT/'assets/unity-kit';target=KIT/'assets/sample-project/Assets/ProjectTweenKit'
    if not (canonical/'Runtime/UIFloat.cs').exists():raise RuntimeError('Canonical kit is missing')
    shutil.copytree(canonical,target,dirs_exist_ok=True)
    # Delete only stale files in this explicitly generated tree.
    root=target.resolve()
    for p in target.rglob('*'):
        if p.is_file() and not (canonical/p.relative_to(target)).exists():
            if not p.resolve().is_relative_to(root):raise RuntimeError('Unsafe generated path')
            p.unlink()
    stable_meta(target,target.parent)
    write(KIT/'assets/sample-project/Assets/Resources/ProjectTweenKit/parameters.json',json.loads((KIT/'references/parameter-catalog.json').read_text(encoding='utf-8')))
    write(KIT/'references/materialization.json',{'canonical':'assets/unity-kit','generated':'assets/sample-project/Assets/ProjectTweenKit','files':{p.relative_to(canonical).as_posix():sha(p) for p in sorted(canonical.rglob('*')) if p.is_file()}})
    print('Ready: open assets/sample-project and Assets/Scenes/Gallery.unity')
if __name__=='__main__':main()
