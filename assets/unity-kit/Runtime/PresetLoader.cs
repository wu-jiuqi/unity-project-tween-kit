using UnityEngine;
namespace ProjectTweenKit {
public sealed class PresetLoader:MonoBehaviour {
 public MonoBehaviour Target;public TextAsset Preset;
 void Awake(){if(Target!=null&&Preset!=null)JsonUtility.FromJson<MotionPreset>(Preset.text).Apply(Target);}
}
}
