using System;
using UnityEngine;
namespace ProjectTweenKit {
// Sound nodes remain observable and use source project clips; no game mixer or storage writes.
public sealed class SoundService:MonoBehaviour {
 [Serializable]class Map {public Clip[] clips;}[Serializable]class Clip {public string cue,path;}
 static SoundService instance;Map map;AudioSource voice;
 public static event Action<string> Cue;
 public static bool Muted;
 static SoundService Ensure(){if(instance==null){var o=new GameObject("Tween kit sound nodes");instance=o.AddComponent<SoundService>();DontDestroyOnLoad(o);instance.voice=o.AddComponent<AudioSource>();instance.voice.playOnAwake=false;var json=Resources.Load<TextAsset>("ProjectTweenKit/sound-map");if(json!=null)instance.map=JsonUtility.FromJson<Map>(json.text);}return instance;}
 public static void Play(string cue,float pitch=1){if(!Application.isPlaying||KitClock.Sampling||Muted)return;Cue?.Invoke(cue);var s=Ensure();if(s.map==null)return;foreach(var c in s.map.clips)if(c.cue==cue){var clip=Resources.Load<AudioClip>(c.path);if(clip!=null){s.voice.pitch=pitch;s.voice.PlayOneShot(clip,.55f);}return;}}
 public static void CancelTransient(){if(instance!=null)instance.voice.Stop();}
 public static void Music(string cue){} // Gallery omits background music; see recorded extraction differences.
 public static bool Speak(string cue,int priority=40){Play(cue);return true;}
}
}
