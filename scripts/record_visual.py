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
            'Search typed through native input filters to the newly delivered score color/scale effect.',
            'The score timeline at its midpoint shows the enlarged gold number; a real main-action click increases the demonstration from 12,000 to 12,120 and restores its original color and scale.',
            'The Animation Prompt tab shows the matching design, trigger, baseline parameters, current parameters with explicit precedence, and cleanup rules, without source navigation.',
            'The copy button gives visible success feedback even while the animation is paused.',
            'Visible scrollbar handles remain inside their tracks.',
        ],
        'automatedEvidence':'runtime-verification.json covers all 26 prompts, current-parameter clipboard contents, RGBA round trip, new effect curves/cleanup/reentry, parameter IO, focus-view lifecycle and input-focus rules; these are not all claimed as native-input checks.',
        'limits':[
            'Native checks sample representative entries, not all workflows or original game call sites.',
            'Final-build native checks exercise search text, timeline, main-action input, prompt navigation and copy acknowledgment; they do not cover every numeric field.',
            'Earlier v0.1.0 and v0.2.0 screenshots are historical evidence and are not final-build captures.',
            'Hidden-window black screenshots are excluded.',
        ]})
    print('Indexed',len(files),'native-window captures')

if __name__=='__main__':main()
