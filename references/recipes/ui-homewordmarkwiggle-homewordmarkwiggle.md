# HomeWordmarkWiggle · HomeWordmarkWiggle

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/HomeWordmarkWiggle.cs`
文件 SHA-256：`40a12fc07fa703fe29a86c524d1e9923e075f120f6c37163407cca58fd192ed1`
[原始源码快照](../source/Assets/Scripts/UI/HomeWordmarkWiggle.cs.txt)

用在哪里：AppUI.BuildHome → BuildChapterHome → ChapterHomeView.Build → HomeWordmarkWiggle
触发操作：主页字标定期震颤；HomeMotionAllowed 门控
导入组件：`ProjectTweenKit.HomeWordmarkWiggle`；展馆 ID：`wordmark`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L14

方法 SHA-256：`57f6c493879900e9af9753d9370ff9b866f68ce77609ce2646fd5f2f84240b98`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.HomeWordmarkWiggle.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(positions==null)return;if(!focused||!FeedbackProfile.Active.enabledFeedback||(app!=null&&!app.HomeMotionAllowed)){Restore();elapsed=0;return;}
  elapsed+=Time.unscaledDeltaTime;float cycle=elapsed%LoopInterval;
  for(int i=0;i<letters.Count;i++){float t=(cycle-.7f-i*.09f)/.48f;float wave=t>0&&t<1?Mathf.Sin(t*Mathf.PI*8)*Mathf.Sin(t*Mathf.PI):0;
   letters[i].anchoredPosition=positions[i]+new Vector2(wave*1.2f,wave*3.2f);
   letters[i].localRotation=rotations[i]*Quaternion.Euler(0,0,wave*2.2f);
   letters[i].localScale=Vector3.Scale(scales[i],new Vector3(1+wave*.018f,1-wave*.025f,1));
  }
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void Restore(){if(positions==null)return;for(int i=0;i<letters.Count;i++){letters[i].anchoredPosition=positions[i];letters[i].localScale=scales[i];letters[i].localRotation=rotations[i];}}
```

```csharp
void OnDisable(){Restore();elapsed=0;}
```

```csharp
void OnApplicationFocus(bool value){focused=value;if(!value){Restore();elapsed=0;}}
```

## 源字段与依赖

```csharp
public float LoopInterval=5.5f;
```
```csharp
readonly List<RectTransform> letters=new List<RectTransform>();
```
```csharp
Vector2[] positions;
```
```csharp
Vector3[] scales;
```
```csharp
Quaternion[] rotations;
```
```csharp
AppUI app;
```
```csharp
float elapsed;
```
```csharp
bool focused=true;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
