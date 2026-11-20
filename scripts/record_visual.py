"""Index already observed native-window captures; do not manufacture visual passes."""
import json
from inventory import KIT,write,sha
def main():
    folder=KIT/'evidence/visual';files=sorted(folder.glob('*.png'))
    write(KIT/'evidence/visual-checks.json',{'method':'computer-use sky native bounded-window capture, after source and runtime validation','testedPlayerRunSha256':sha(KIT/'evidence/player-run.json'),'images':[{'path':p.relative_to(KIT).as_posix(),'sha256':sha(p)} for p in files],'observed':['Original C-style button and Chinese font render in visible Windows gallery.','Actual pointer drag returned without click callback; normal click displayed the local-only callback.','Source/type filters and parameter panel labels are readable.','Modal open and done-close work through real UI; open button and footer do not overlap.','Original four-glyph logo and typewriter quote are visible.'],'limits':['Native interaction checks sample representative entries, not all 19 workflows or all original game call sites.','Hidden-window black screenshots are excluded.']})
    print('Indexed',len(files),'native-window captures')
if __name__=='__main__':main()
