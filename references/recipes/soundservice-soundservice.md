# SoundService · SoundService

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/SoundService.cs`
文件 SHA-256：`617910b930e89df479417a087292b8d66bc09a285434e6b735a1dbfd51b215af`
[原始源码快照](../source/Assets/Scripts/SoundService.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### WarmEffects · L34

方法 SHA-256：`a3e6198f02d37527093850e1f1c3beaa51bca21975daf15098f0993d01df8173`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SoundService.WarmEffects()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SoundService.cs:29` · `SoundService.Awake` → `WarmEffects()`

```csharp
IEnumerator WarmEffects(){yield return null;foreach(var key in clipPaths.Keys){if(!key.StartsWith("bgm_")&&!key.StartsWith("candidate_"))LoadClip(key);yield return null;}}
```

### Trigger · L49

方法 SHA-256：`b871d689ecc9f277cfb24b5753e7d3bd8500a7bafa4e71ee0b094a097b7b7627`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SoundService.Trigger(string, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SoundService.cs:37` · `SoundService.Play` → `s.Trigger(cue,pitch)`
- `Assets/Scripts/SoundService.cs:53` · `SoundService.Trigger` → `Trigger("cat_collect_trill",1)`

```csharp
void Trigger(string cue,float pitch){if(muted||suspended||sfxLevel<=0)return;if(cue=="win")cue="result_win_stinger";else if(cue=="lose")cue="result_lose_stinger";float now=Time.unscaledTime;if(last.TryGetValue(cue,out float prior)&&now-prior<Limit(cue))return;string key=Variant(cue);var clip=LoadClip(key);if(clip==null)return;bool isCat=buses.TryGetValue(key,out var bus)&&bus=="cat";
 if(isCat){if(cat.isPlaying||speech.isPlaying)return;last[cue]=now;cat.clip=clip;catGain=.68f;cat.volume=catGain*sfxLevel;cat.pitch=1;catUntil=now+clip.length;cat.Play();Played?.Invoke(cue);return;}
 int same=0;for(int i=0;i<12;i++)if(voices[i].isPlaying&&playingCue[i]==cue)same++;if(same>=2)return;int chosen=cue=="result_win_stinger"||cue=="result_lose_stinger"||cue=="goal_complete"||cue=="victory_finale_blast"||cue=="victory_stars_transfer"||cue=="score_star_full"?12:cue=="coin"||cue=="reward_claim"?13:-1;if(chosen<0)for(int i=0;i<12;i++)if(!voices[i].isPlaying){chosen=i;break;}if(chosen<0)chosen=cursor++%12;
 var v=voices[chosen];v.Stop();v.clip=clip;gains[chosen]=cue=="score_count_tick"?.35f:cue=="lucky_cat_charge_ready"?.45f:cue=="victory_finale_blast"||cue=="victory_stars_transfer"?.85f:cue.StartsWith("snow_")?.45f:cue=="bomb"||cue=="cross_laser"?.42f:cue=="basket_hit"||cue=="pierce_pass"||cue=="cookie_break"?.48f:cue.StartsWith("result_")?.85f:.7f;v.volume=gains[chosen]*sfxLevel;v.pitch=Mathf.Clamp(pitch+(cue=="shoot"||cue=="wall"||cue=="hit"?UnityEngine.Random.Range(-.05f,.05f):0),.85f,1.15f);playingCue[chosen]=cue;ends[chosen]=now+clip.length/v.pitch;last[cue]=now;v.Play();Played?.Invoke(cue);
 if(cue=="goal_complete")Trigger("cat_collect_trill",1);
 }
```

### Speak · L55

方法 SHA-256：`ef0f050b544ddc007abb4ede26928028dbd590300e8fea195205d829c8b7383c`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SoundService.Speak(string, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SoundService.cs:37` · `SoundService.Play` → `Speak(cue,40)`
- `Assets/Scripts/SoundService.cs:56` · `SoundService.Cheer` → `Speak("vo_cheer_"+names[tier]+"_en",30+tier)`
- `Assets/Scripts/UI/AppUI.cs:177` · `AppUI.StartPreparedLevel` → `SoundService.Speak("vo_tutorial_launch_zh",70)`
- `Assets/Scripts/UI/FailureFlow.cs:23` · `AppUI.WatchContinueAd` → `SoundService.Speak("vo_continue_zh",80)`
- `Assets/Scripts/UI/ResultAudioTimeline.cs:7` · `ResultAudioTimeline.Update` → `SoundService.Speak(win?"vo_win_zh":"vo_retry_zh",90)`

```csharp
public static bool Speak(string cue,int priority=40){var s=Ensure();if(s.muted||s.suspended||s.voiceLevel<=0)return false;float now=Time.unscaledTime;if(s.speech.isPlaying&&priority<=s.voicePriority||priority<70&&now<s.voiceGate)return false;var c=s.LoadClip(s.Variant(cue));if(c==null)return false;s.speech.Stop();s.speech.clip=c;s.speech.pitch=1;s.speech.volume=s.voiceLevel;s.voicePriority=priority;s.voiceUntil=now+c.length;s.voiceGate=now+Mathf.Max(2.5f,c.length+.25f);s.speech.Play();Played?.Invoke(cue);return true;}
```

### Crossfade · L59

方法 SHA-256：`491239f6819f696deb3729e1f1e88974e74489518efe62747b2ec3c0d3957d89`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SoundService.Crossfade(int, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/SoundService.cs:58` · `SoundService.Music` → `s.Crossfade(from,to)`

```csharp
IEnumerator Crossfade(int from,int to){float initial=music[from].volume,t=0;while(t<.65f){t+=Time.unscaledDeltaTime;float a=Mathf.Clamp01(t/.65f);music[from].volume=initial*(1-a);musicBlend=a;yield return null;}music[from].Stop();musicBlend=1;fade=null;}
```

### Update · L60

方法 SHA-256：`bb28f69dde034dd5bc26b0ebb001358e65ab886b173bf7eb44bfd18c73dcb4eb`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SoundService.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(mixDirty&&Time.unscaledTime-mixChangedAt>=.35f)SaveMix();bool talking=speech.isPlaying;float duck=talking?.38f:1;music[musicIndex].volume=.31f*musicLevel*musicBlend*duck;for(int i=0;i<voices.Length;i++)if(voices[i].isPlaying)voices[i].volume=gains[i]*sfxLevel*(talking&&i<12?.42f:1);cat.volume=catGain*sfxLevel*(talking?.25f:1);if(suspended||muted){speech.Stop();cat.Stop();}}
```

### SetBusVolume · L64

方法 SHA-256：`f0b9769025ce38ac85aacf4561e00d4aef03f29bd80bff1ccb20975fb48e5fa4`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SoundService.SetBusVolume(int, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OptimizationV76QA.cs:28` · `OptimizationV76QA.Start` → `SoundService.SetBusVolume(0,n/100f)`
- `Assets/Scripts/OptimizationV76QA.cs:28` · `OptimizationV76QA.Start` → `SoundService.SetBusVolume(0,.42f)`
- `Assets/Scripts/SfxV87QA.cs:27` · `SfxV87QA.Start` → `SoundService.SetBusVolume(1,0)`
- `Assets/Scripts/SfxV87QA.cs:27` · `SfxV87QA.Start` → `SoundService.SetBusVolume(1,.8f)`

```csharp
public static void SetBusVolume(int bus,float value){var s=Ensure();value=Mathf.Clamp01(value);if(Mathf.Approximately(BusVolume(bus),value))return;s.mixDirty=true;s.mixChangedAt=Time.unscaledTime;if(bus==0)s.musicLevel=value;else if(bus==1)s.sfxLevel=value;else s.voiceLevel=value;if(bus==2){s.speech.volume=value;if(value==0)s.speech.Stop();}if(bus==1&&value==0){foreach(var v in s.voices)v.Stop();s.cat.Stop();}}
```

### SaveMix · L65

方法 SHA-256：`9920447f435c18c83a2fa5492f2a49a69b20b1cecbd2475c2e8efc1a178f2e77`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.SoundService.SaveMix()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/OptimizationV76QA.cs:28` · `OptimizationV76QA.Start` → `SoundService.SaveMix()`
- `Assets/Scripts/OptimizationV76QA.cs:28` · `OptimizationV76QA.Start` → `SoundService.SaveMix()`
- `Assets/Scripts/SoundService.cs:60` · `SoundService.Update` → `SaveMix()`
- `Assets/Scripts/SoundService.cs:67` · `SoundService.OnApplicationPause` → `SaveMix()`
- `Assets/Scripts/SoundService.cs:68` · `SoundService.OnApplicationQuit` → `SaveMix()`
- `Assets/Scripts/UI/AppUI.AudioSettings.cs:20` · `AppUI.OpenAudioSettings` → `SoundService.SaveMix()`
- `Assets/Scripts/UI/AppUI.cs:91` · `AppUI.OnApplicationPause` → `SoundService.SaveMix()`
- `Assets/Scripts/UI/AppUI.PlayerSettings71.cs:81` · `SaveAudioMix71.OnDisable` → `SoundService.SaveMix()`
- `Assets/Scripts/UI/AudioSliderCommit76.cs:5` · `AudioSliderCommit76.OnPointerUp` → `SoundService.SaveMix()`
- `Assets/Scripts/UI/AudioSliderCommit76.cs:6` · `AudioSliderCommit76.OnEndDrag` → `SoundService.SaveMix()`
- `Assets/Scripts/UI/AudioSliderCommit76.cs:7` · `AudioSliderCommit76.OnDisable` → `SoundService.SaveMix()`

```csharp
public static void SaveMix(){if(instance==null||!instance.mixDirty)return;var s=instance;if(!IsQA){PlayerPrefs.SetFloat("audio.music.v65",s.musicLevel);PlayerPrefs.SetFloat("audio.sfx.v65",s.sfxLevel);PlayerPrefs.SetFloat("audio.voice.v65",s.voiceLevel);PlayerPrefs.Save();}s.mixDirty=false;MixSaveCount++;}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public static void CancelTransient(){if(instance==null)return;var s=instance;s.speech.Stop();s.cat.Stop();s.voiceUntil=s.catUntil=s.voiceGate=0;s.voicePriority=0;foreach(var v in s.voices)v.Stop();}
```

```csharp
void OnApplicationPause(bool pause){suspended=pause;if(pause){CancelTransient();SaveMix();}ApplyPlayback();}
```

```csharp
void OnApplicationQuit(){SaveMix();}
```

## 源字段与依赖

```csharp
bool mixDirty;
```
```csharp
float mixChangedAt;
```
```csharp
static SoundService instance;
```
```csharp
AudioSource[] voices,music;
```
```csharp
AudioSource speech,cat;
```
```csharp
int musicIndex,cursor,voicePriority;
```
```csharp
string musicCue;
```
```csharp
bool muted,suspended;
```
```csharp
float voiceUntil,catUntil,voiceGate;
```
```csharp
Coroutine fade;
```
```csharp
readonly Dictionary<string,string> clipPaths=new Dictionary<string,string>();
```
```csharp
readonly Dictionary<string,AudioClip> clips=new Dictionary<string,AudioClip>();
```
```csharp
readonly Dictionary<string,float> last=new Dictionary<string,float>();
```
```csharp
readonly Dictionary<string,int> variants=new Dictionary<string,int>();
```
```csharp
readonly Dictionary<string,string> buses=new Dictionary<string,string>();
```
```csharp
readonly Dictionary<string,string[]> groups=new Dictionary<string,string[]>();
```
```csharp
readonly string[] playingCue=new string[14];
```
```csharp
readonly float[] gains=new float[14];
```
```csharp
readonly float[] ends=new float[14];
```
```csharp
float musicLevel=.7f,sfxLevel=.8f,voiceLevel=1;
```
```csharp
float musicBlend;
```
```csharp
float catGain;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
