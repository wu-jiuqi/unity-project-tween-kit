# ActivityJellyMotion · ActivityJellyMotion

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/ActivityJellyMotion.cs`
文件 SHA-256：`66ff3a51f3027947649d5ba47a4dc7c1d1161f5a473fcf6395724474ec64e344`
[原始源码快照](../source/Assets/Scripts/UI/ActivityJellyMotion.cs.txt)

用在哪里：ChapterHomeView.Build → ActivityCarousel.Create / Build → ActivityJellyMotion.Initialize
触发操作：活动卡悬停、按下、松开与初次入场
导入组件：`ProjectTweenKit.ActivityJellyMotion`；展馆 ID：`activity`。
复用形式：宿主适配配方。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L13

方法 SHA-256：`f6e573f1371071b6aabd00c44694922bc1f83e042b8fa8d792803d7848eead58`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.ActivityJellyMotion.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(visual==null)return;if(!FeedbackProfile.Active.enabledFeedback||allowed==null||!allowed()){ResetPose();return;}
  float dt=Mathf.Min(Time.unscaledDeltaTime,.04f);clock+=dt;float previous=age;age+=dt;if(previous<0&&age>=0)Celebrate();
  // Damped spring, substepped for consistent motion at low frame rates.
  float target=held?-1f:hovered?.45f:0;
  for(int n=0;n<4;n++){float step=dt*.25f;velocity+=((target-spring)*155-velocity*10)*step;spring+=velocity*step;}
  spring=Mathf.Clamp(spring,-1.4f,1.4f);
  float phase=clock*2.4f+index*1.8f;
  float idle=Mathf.Sin(phase)*.018f;
  visual.localScale=new Vector3(1+spring*.018f,1-spring*.025f,1);
  float grow=hovered?.16f:0;
  icon.localScale=new Vector3(1+grow-spring*.15f+idle,Mathf.Clamp(1+grow+spring*.21f-idle,.72f,1.21f),1);
  icon.anchoredPosition=new Vector2(iconRest.x,Mathf.Clamp(iconRest.y+Mathf.Sin(phase)*7+(hovered?18:0)+spring*19,-176+icon.rect.height*icon.localScale.y*.5f,126-icon.rect.height*icon.localScale.y*.5f));
  icon.localRotation=Quaternion.Euler(0,0,Mathf.Sin(phase*.8f)*(hovered?7:3)+velocity*1.1f);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public void ResetPose(){spring=velocity=0;hovered=held=false;if(visual!=null)visual.localScale=Vector3.one;if(icon!=null){icon.localScale=Vector3.one;icon.anchoredPosition=iconRest;icon.localRotation=Quaternion.identity;}}
```

```csharp
void OnDisable(){ResetPose();}
```

## 源字段与依赖

```csharp
RectTransform visual,icon;
```
```csharp
Vector2 iconRest;
```
```csharp
Func<bool> allowed;
```
```csharp
bool hovered,held;
```
```csharp
float spring,velocity,clock,age;
```
```csharp
int index;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
