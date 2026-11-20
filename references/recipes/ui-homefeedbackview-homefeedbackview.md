# HomeFeedbackView · HomeFeedbackView

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/HomeFeedbackView.cs`
文件 SHA-256：`aef2773f232632afb25debb34374b5036bed8d539969d3cc5c3fcd029ea9b6b5`
[原始源码快照](../source/Assets/Scripts/UI/HomeFeedbackView.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L30

方法 SHA-256：`2d7f9c13be0489a086dcc5101e579cfc740c05b5526131f50021ed2c829e6c85`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.HomeFeedbackView.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(title==null)return;if(!focused||!FeedbackProfile.Active.enabledFeedback||(app!=null&&!app.HomeMotionAllowed)){ResetPose();return;}
  float dt=Mathf.Min(Time.unscaledDeltaTime,.05f);idle+=dt;if(entrance>=0){entrance+=dt;if(entrance>1.35f)entrance=-1;}
  float breath=Mathf.Sin(idle*1.8f),wave=Pop(entrance,0)*strength,follow=Pop(entrance,.13f)*strength;
  title.rectTransform.localScale=new Vector3(1+breath*.018f-wave*.065f,1+breath*.018f+wave*.12f,1);
  title.rectTransform.anchoredPosition=new Vector2(0,15+breath*3+wave*13);
  detail.rectTransform.localScale=Vector3.one*(1+Mathf.Sin(idle*1.8f-.6f)*.009f+follow*.045f);
  detail.rectTransform.anchoredPosition=new Vector2(0,-82+Mathf.Sin(idle*1.8f-.6f)*2+follow*9);
  decoration.localScale=new Vector3(1-wave*.09f,1+wave*.17f,1);decoration.anchoredPosition=new Vector2(0,113+Mathf.Sin(idle*1.8f+.4f)*4+wave*9);
  decoration.localRotation=Quaternion.Euler(0,0,Mathf.Sin(idle*1.5f)*3+wave*5);
 }
```

### Pop · L40

方法 SHA-256：`49e9431bdf620c840234f09179f57bf4056451f05be006ee18dbece524b0d235`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Exp
解析符号：`BounceGarden.HomeFeedbackView.Pop(float, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/HomeFeedbackView.cs:32` · `HomeFeedbackView.Update` → `Pop(entrance,0)`
- `Assets/Scripts/UI/HomeFeedbackView.cs:32` · `HomeFeedbackView.Update` → `Pop(entrance,.13f)`

```csharp
static float Pop(float time,float delay){if(time<0)return 0;float t=time-delay;if(t<0)return -.75f;return -Mathf.Cos(t*17)*Mathf.Exp(-t*4.7f)*.85f;}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void ResetPose(){entrance=-1;if(group!=null)group.alpha=1;if(title!=null){title.rectTransform.localScale=Vector3.one;title.rectTransform.anchoredPosition=new Vector2(0,15);}if(detail!=null){detail.rectTransform.localScale=Vector3.one;detail.rectTransform.anchoredPosition=new Vector2(0,-82);}if(decoration!=null){decoration.localScale=Vector3.one;decoration.anchoredPosition=new Vector2(0,113);decoration.localRotation=Quaternion.identity;}}
```

```csharp
void OnApplicationFocus(bool value){focused=value;if(!value)ResetPose();}
```

```csharp
void OnApplicationPause(bool paused){focused=!paused;if(paused)ResetPose();}
```

```csharp
void OnDisable(){ResetPose();}
```

## 源字段与依赖

```csharp
TMP_Text title,detail,mode;
```
```csharp
RectTransform decoration;
```
```csharp
CanvasGroup group;
```
```csharp
string rendered;
```
```csharp
AppUI app;
```
```csharp
float entrance=-1,idle,strength;
```
```csharp
bool focused=true,pendingEntrance;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
