"""Index final-player native captures separately from automated event tests."""
from inventory import KIT,write,sha

def main():
 files=sorted((KIT/'evidence/visual').glob('*.png'))
 write(KIT/'evidence/visual-checks.json',{
  'method':'computer-use sky native bounded-window capture after exact-player runtime checks',
  'testedPlayerRunSha256':sha(KIT/'evidence/player-run.json'),
  'images':[{'path':p.relative_to(KIT).as_posix(),'sha256':sha(p)} for p in files],
  'observed':[
   'The native gallery exposes Parameter display and One-click parameter copy/export; the former Animation Prompt/source archive navigation is absent.',
   'The original project loading track, fill, paw, marble and numeric label render together; native task input advances the shown percentage.',
   'The full startup view uses the original title, cat poses, island, box, background and progress driver while preserving the original portrait proportions.',
   'Parameter summary shows current values with units, fixed curve notes and component type; the export button acknowledges copying and writing JSON.',
   'The complete startup view visibly holds at 99 percent pending its completion signal; native task input and the required-task completion action are distinct.',
  ],
  'automatedEvidence':'runtime-verification.json verifies all 36 parameter exports, clipboard/file equality, legacy MotionPreset loading, current RGBA/time values, loading monotonicity/99% completion gate/failure/pause, host-origin-preserving rank changes and all delivered component lifecycle regressions.',
  'limits':['Native checks sample workflows and do not reproduce all 96 reference recipes.','The reference comparison is by project relevance and effect family, not frame-for-frame reproduction.','Older version captures remain historical evidence; hidden-window black captures are excluded.']})
 print('Indexed',len(files),'final native captures')
if __name__=='__main__':main()
