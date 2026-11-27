# PlaytestMetrics76 · PlaytestMetrics76

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/PlaytestMetrics76.cs`
文件 SHA-256：`103919f9a06b61d18a9b60d872dd7e882a7232e3d3790d4bdb8ffa124fc83383`
[原始源码快照](../source/Assets/Scripts/PlaytestMetrics76.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L37

方法 SHA-256：`5240ff4bdf9717dffa80dbdaac37582df6de96424fcce1a10f07326e6011b7c3`
时间来源：Time.unscaledDeltaTime, Time.unscaledTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.PlaytestMetrics76.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){
  if(app==null||round==null)return;var game=app.Session;
  if(game.IsPaused)releases.Clear();
  if(!game.IsPlaying||game.IsPaused)return;
  float ms=Time.unscaledDeltaTime*1000;frames[cursor]=ms;cursor=(cursor+1)%Capacity;frameCount=Mathf.Min(Capacity,frameCount+1);round.frames++;round.activeSeconds+=Time.unscaledDeltaTime;round.worstFrameMs=Mathf.Max(round.worstFrameMs,ms);
  if(Time.unscaledTime>=nextMemorySample){nextMemorySample=Time.unscaledTime+1;round.peakAllocatedBytes=Math.Max(round.peakAllocatedBytes,Profiler.GetTotalAllocatedMemoryLong());round.peakReservedBytes=Math.Max(round.peakReservedBytes,Profiler.GetTotalReservedMemoryLong());}
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
internal void RestoreJson(string json){var report=JsonUtility.FromJson<Report>(json);if(report==null||report.schema<1||report.schema>2)throw new InvalidOperationException("Invalid metrics schema");if(report.attempts!=null&&report.attempts.Length==78)for(int i=0;i<78;i++)attempts[i]=Mathf.Clamp(report.attempts[i],0,1000000);rounds.Clear();if(report.rounds!=null)foreach(var item in report.rounds)if(item!=null&&item.level>=1&&item.level<=78)rounds.Add(item);if(report.active!=null&&report.active.level>=1&&report.active.level<=78){report.active.outcome="interrupted";rounds.Add(report.active);RecoveredCount++;}while(rounds.Count>HistoryLimit)rounds.RemoveAt(0);}
```

```csharp
void OnApplicationPause(bool paused){if(paused)Save();}
```

```csharp
void OnApplicationQuit(){Finish("interrupted");}
```

```csharp
void OnDestroy(){if(app!=null&&app.Session!=null)app.Session.VolleyStarted-=VolleyStarted;}
```

## 源字段与依赖

```csharp
const int Capacity=2048,HistoryLimit=64;
```
```csharp
readonly float[] frames=new float[Capacity],sorted=new float[Capacity];
```
```csharp
readonly List<Round> rounds=new List<Round>();
```
```csharp
int[] attempts=new int[78];
```
```csharp
int frameCount,cursor;
```
```csharp
float nextMemorySample;
```
```csharp
readonly Queue<float> releases=new Queue<float>();
```
```csharp
AppUI app;
```
```csharp
Round round;
```
```csharp
string lastSavedJson;
```
```csharp
const string StorageKey="BounceGarden.PlaytestMetrics.v76";
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
