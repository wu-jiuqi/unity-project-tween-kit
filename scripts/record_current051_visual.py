"""Record the actual partial native review, without upgrading it to a full pass."""
import json
from inventory import KIT,write,sha

def main():
    previous=KIT/'evidence/visual-checks.json'
    historical=KIT/'evidence/visual-checks-040.json'
    if previous.exists() and not historical.exists():historical.write_bytes(previous.read_bytes())
    result={
        'date':'2026-11-27','result':'partial-user-stopped','fullVisualAcceptancePassed':False,
        'testedPlayerRunSha256':sha(KIT/'evidence/player-run.json'),
        'method':'Computer Use bounded-window observation; user stopped Computer Use with physical Escape. No further desktop control was performed.',
        'observedBeforeFinalGalleryTextUpdate':['The native gallery listed 44 entries and the eight current additions.','The error popup displayed the complete cream panel, shared box/cat/brick animation and both styled buttons.'],
        'fixesFollowingObservation':['Added specific operation guidance for all eight new entries.','Adjusted local stage sizing for new examples.','Added automatic UI raycast reachability checks for gallery navigation in all eight examples.'],
        'notCompleted':['Final-build native visual review of all new examples, transitions and resolutions.','Manual native click-through after the operation-guidance update.'],
        'images':[],'historicalImages':'visual/ and visual-checks-040.json belong to previous releases; they are not current-build acceptance evidence.',
        'automatedEvidence':'runtime-verification.json and integrity-verification.json bind the tested final code and binary; they do not replace native visual acceptance.'}
    write(previous,result);write(KIT/'evidence/current-051-visual-review.json',result)
    print('Recorded partial native review and explicit user stop')
if __name__=='__main__':main()
