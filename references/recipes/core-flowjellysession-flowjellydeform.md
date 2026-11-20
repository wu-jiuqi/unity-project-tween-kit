# FlowJellyDeform · FlowJellySession

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/FlowJellySession.cs`
文件 SHA-256：`6f748e9e143d84ea4bd39b96ebe960aa53b504cd41b6157b87ac1a6f282c292d`
[原始源码快照](../source/Assets/Scripts/Core/FlowJellySession.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### LateUpdate · L24

方法 SHA-256：`119eb4374a367a19bc45b7912ea92b0af1e31f8ffd6be7c0f22144c3819207e7`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Exp, Mathf.Sin
解析符号：`BounceGarden.FlowJellyDeform.LateUpdate()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void LateUpdate(){
  if(owner==null||owner.IsPaused||!owner.IsPlaying||!FeedbackProfile.Active.enabledFeedback)return;
  float squeeze=landed?Mathf.Sin(age/.27f*Mathf.PI*2)*Mathf.Exp(-age*11)*.12f*weight:-Mathf.Clamp01(velocity.magnitude/8.8f)*.055f*weight;
  float tilt=landed?0:Mathf.Clamp(velocity.x/8.8f,-1,1)*-3f*weight;
  for(int i=0;i<parts.Length;i++){var t=parts[i];if(t==null||t.GetComponent<Collider2D>()!=null||t.GetComponent<Rigidbody2D>()!=null)continue;scales[i]=t.localScale;rotations[i]=t.localRotation;t.localScale=Vector3.Scale(scales[i],new Vector3(1+squeeze,1-squeeze,1));t.localRotation=rotations[i]*Quaternion.Euler(0,0,tilt);}
  applied=true;
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Restore(){if(!applied)return;for(int i=0;i<parts.Length;i++)if(parts[i]!=null){parts[i].localScale=scales[i];parts[i].localRotation=rotations[i];}applied=false;}
```

```csharp
public void ResetPose(){Restore();age=1;enabled=false;}
```

```csharp
void OnDisable(){Restore();}
```

## 源字段与依赖

```csharp
GameSession owner;
```
```csharp
Transform[] parts;
```
```csharp
Vector3[] scales;
```
```csharp
Quaternion[] rotations;
```
```csharp
bool applied,landed;
```
```csharp
float age,weight;
```
```csharp
Vector2 velocity;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
