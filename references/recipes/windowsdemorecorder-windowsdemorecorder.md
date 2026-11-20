# WindowsDemoRecorder · WindowsDemoRecorder

状态：`test-fixture`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/WindowsDemoRecorder.cs`
文件 SHA-256：`d98261922a0bc6053ebb3076eadead8d72c031190fb3ce0e823e874cd9daa44f`
[原始源码快照](../source/Assets/Scripts/WindowsDemoRecorder.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### CaptureDisplayedFrames · L34

方法 SHA-256：`c8950794e92e0de2a52077a42ce78502ecbcbaf05069d6db37de23516e442432`
时间来源：Time.realtimeSinceStartup
曲线证据：查原式或下级调用
解析符号：`BounceGarden.WindowsDemoRecorder.CaptureDisplayedFrames()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/WindowsDemoRecorder.cs:18` · `WindowsDemoRecorder.Start` → `CaptureDisplayedFrames()`

```csharp
IEnumerator CaptureDisplayedFrames(){while(true){yield return new WaitForEndOfFrame();if(!recording||Time.realtimeSinceStartup<next)continue;float stamp=Time.realtimeSinceStartup-started;next=Time.realtimeSinceStartup+1f/30;
  var texture=ScreenCapture.CaptureScreenshotAsTexture();File.WriteAllBytes(Path.Combine(directory,frame.ToString("D6")+".jpg"),texture.EncodeToJPG(92));Destroy(texture);frames.WriteLine(frame+","+stamp.ToString("F6",CultureInfo.InvariantCulture));frame++;
 }}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public void StopEvidence(){Finish();}
```

```csharp
void OnApplicationQuit(){Finish();}
```

## 源字段与依赖

```csharp
string directory;
```
```csharp
StreamWriter frames,events;
```
```csharp
BinaryWriter audioWriter;
```
```csharp
readonly object audioLock=new object();
```
```csharp
volatile bool recording;
```
```csharp
bool finished;
```
```csharp
int frame,channels,rate;
```
```csharp
float started,next;
```
```csharp
double dspStart;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
