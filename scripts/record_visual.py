"""Index observed final-player captures; distinguish native and automated evidence."""
from inventory import KIT,write,sha

def main():
    files=sorted((KIT/'evidence/visual').glob('*.png'))
    write(KIT/'evidence/visual-checks.json',{
        'method':'computer-use sky native bounded-window capture, after source and runtime validation',
        'testedPlayerRunSha256':sha(KIT/'evidence/player-run.json'),
        'images':[{'path':p.relative_to(KIT).as_posix(),'sha256':sha(p)} for p in files],
        'observed':[
            'Final native UGUI three-column layout, original C artwork and Chinese labels render in the visible Windows gallery.',
            'Operation guide, stage, main action, playback controls and parameter tab have distinct fixed locations.',
            'The final modal opens from the main action and closes with its real Done button.',
            'Visible scrollbar handles remain inside their tracks.',
        ],
        'automatedEvidence':'runtime-verification.json covers search, archive return, parameter IO, focus-view lifecycle and input-focus rules; these are not all claimed as native-input checks.',
        'limits':[
            'Native checks sample representative entries, not all workflows or original game call sites.',
            'Final precise numeric keyboard-entry verification was interrupted: the window became minimized and recovery repeatedly reported user input detected. No native numeric-entry pass is claimed.',
            'Earlier v0.1.0 screenshots are preserved separately as historical evidence and are not final-build captures.',
            'Hidden-window black screenshots are excluded.',
        ]})
    print('Indexed',len(files),'native-window captures')

if __name__=='__main__':main()
