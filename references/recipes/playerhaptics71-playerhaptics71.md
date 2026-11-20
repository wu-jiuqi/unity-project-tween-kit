# PlayerHaptics71 · PlayerHaptics71

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/PlayerHaptics71.cs`
文件 SHA-256：`1605e8f0f36d6b9e39c7f7526323e7a055a2841ae4fcef70d74380ae5aaa9bec`
[原始源码快照](../source/Assets/Scripts/PlayerHaptics71.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### TryPulse · L9

方法 SHA-256：`f3c059818acddb7a57ab694641d1c19ad72bd629ace86af1cf8a05d758d41356`
时间来源：Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.PlayerHaptics71.TryPulse(BounceGarden.PlayerHaptics71.Strength, System.Action<bool>)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/CollisionHapticsQA.cs:25` · `CollisionHapticsQA.Start` → `PlayerHaptics71.TryPulse(PlayerHaptics71.Strength.Heavy,ok=>result=ok)`
- `Assets/Scripts/Mobile73QA.cs:49` · `Mobile73QA.Start` → `PlayerHaptics71.TryPulse(PlayerHaptics71.Strength.Medium,_=>{})`
- `Assets/Scripts/Mobile73QA.cs:51` · `Mobile73QA.Start` → `PlayerHaptics71.TryPulse(PlayerHaptics71.Strength.Medium,ok=>result=ok)`
- `Assets/Scripts/PlayerHaptics71.cs:8` · `PlayerHaptics71.Pulse` → `TryPulse(strength,null)`

```csharp
public static void TryPulse(Strength strength,Action<bool> completed){
  // One long impact owns the motor for 400ms. Dense contacts must not shorten
  // it or keep restarting it; ordinary contacts outside that window stay immediate.
  if(!Enabled||Time.unscaledTime<longUntil){completed?.Invoke(false);return;}
  int request=++longRequest;
  if(strength==Strength.Heavy)longUntil=Time.unscaledTime+LongDuration;
  Action<bool> finish=ok=>{if(!ok&&request==longRequest)longUntil=-1;completed?.Invoke(ok);};
  try{
   if(MobilePulse!=null){MobilePulse(strength,finish);return;}
   if(RuntimeQA.IsIsolated){finish(false);return;}
   #if UNITY_ANDROID && !UNITY_EDITOR
   bool ok=AndroidPulse(strength);finish(ok);
   #elif UNITY_IOS && !UNITY_EDITOR
   bool ok=BounceImpactHaptic(strength==Strength.Heavy?1:0);finish(ok);
   #else
   finish(false);
   #endif
  }catch(Exception){finish(false);}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public const float LongDuration=.4f;
```
```csharp
static bool isolatedEnabled=true;
```
```csharp
static float longUntil=-1;
```
```csharp
static int longRequest;
```
```csharp
public static Action<Strength,Action<bool>> MobilePulse;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
