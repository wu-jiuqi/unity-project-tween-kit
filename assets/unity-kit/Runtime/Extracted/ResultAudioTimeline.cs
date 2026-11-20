using UnityEngine;
namespace ProjectTweenKit {
public sealed class ResultAudioTimeline:MonoBehaviour {
 bool win,active;float age;int fired;
 public void Initialize(bool success){win=success;active=true;age=0;fired=0;SoundService.CancelTransient();SoundService.Music("bgm_result");SoundService.Play(win?"result_win_stinger":"result_lose_stinger");}
 void Cue(int bit,float at,string cue){if((fired&(1<<bit))!=0||age<at)return;fired|=1<<bit;SoundService.Play(cue);}
 void Update(){if(!active)return;age+=KitClock.Delta;if(win){Cue(0,.38f,"cat_jump_soft");Cue(1,.55f,"cat_happy");Cue(2,.76f,"cat_land_soft");Cue(3,1,"result_win_burst");Cue(4,1.45f,"result_reward_reveal");}else{Cue(0,.55f,"cat_retry");Cue(1,.72f,"cat_droop_soft");}if(age>=2.1f&&(fired&128)==0){fired|=128;SoundService.Speak(win?"vo_win_zh":"vo_retry_zh",90);}}
 public void Skip(){active=false;SoundService.CancelTransient();}
 void OnDisable(){active=false;SoundService.CancelTransient();}
}
}
