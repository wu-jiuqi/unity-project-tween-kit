"""Bind real player-side checks to the exact tested Windows file set."""
from pathlib import Path
import subprocess,json,shutil,time
from inventory import KIT,sha,write
def main():
    binary=KIT/'dist/Windows/TweenGallery.exe';folder=binary.parent
    before={p.relative_to(folder).as_posix():sha(p) for p in sorted(folder.rglob('*')) if p.is_file()}
    out=KIT/'work/evidence';out.mkdir(parents=True,exist_ok=True);start=time.time()
    startup=subprocess.STARTUPINFO();startup.dwFlags|=subprocess.STARTF_USESHOWWINDOW;startup.wShowWindow=0
    result=subprocess.run([str(binary),'-screen-fullscreen','0','-screen-width','1600','-screen-height','1000','--verify-kit','--evidence',str(out),'-logFile',str(KIT/'work/player.log')],startupinfo=startup)
    path=out/'runtime-verification.json'
    if not path.exists() or path.stat().st_mtime<start:raise RuntimeError('Runtime verification did not produce a fresh report')
    report=json.loads(path.read_text(encoding='utf-8-sig'));after={p.relative_to(folder).as_posix():sha(p) for p in sorted(folder.rglob('*')) if p.is_file()}
    record={'exitCode':result.returncode,'elapsedSeconds':round(time.time()-start,3),'binaryFilesSha256':before,'filesUnchanged':before==after,'runtimeReportSha256':sha(path),'buildInputsSha256':sha(KIT/'evidence/build-inputs.json'),'visualEvidence':'Hidden-window screen captures are excluded; use separately captured visible-window evidence.'}
    shutil.copy2(path,KIT/'evidence/runtime-verification.json');shutil.copy2(out/'number-preset.json',KIT/'evidence/number-preset.json');write(KIT/'evidence/player-run.json',record)
    print(json.dumps({'exitCode':result.returncode,'passed':report['passed'],'failed':report['failed'],'errors':len(report['errors']),'unchanged':before==after}))
    if result.returncode or report['failed'] or report['errors'] or before!=after:raise SystemExit(1)
if __name__=='__main__':main()
