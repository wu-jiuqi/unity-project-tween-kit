# EncouragementDrag · EncouragementDrag

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/EncouragementDrag.cs`
文件 SHA-256：`d2c55d0c4d8f6ca52f2623ff8c3b1452bf5e24d68f8c3b53ee1e16cd95a378fa`
[原始源码快照](../source/Assets/Scripts/UI/EncouragementDrag.cs.txt)

用在哪里：ChapterHomeView.Build → HomeFeedbackView.Create → EncouragementDrag.Initialize
触发操作：拖动主页欢迎语；释放后四子步弹簧归位
导入组件：`ProjectTweenKit.EncouragementDrag`；展馆 ID：`drag`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### LateUpdate · L17

方法 SHA-256：`07d973766d5d5a1c03e33006c3c8771ce6c0adc1c513a86442035592cb5aac4f`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.EncouragementDrag.LateUpdate()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void LateUpdate(){if(visual==null)return;if(!Allowed){ResetPose();return;}if(!held){float dt=Mathf.Min(Time.unscaledDeltaTime,.04f)/4;for(int i=0;i<4;i++){velocity+=(-pull*105-velocity*6.5f)*dt;pull+=velocity*dt;}if(pull.sqrMagnitude<.000001f&&velocity.sqrMagnitude<.00001f)pull=velocity=Vector2.zero;}Render();}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public void ResetPose(){held=false;pointer=int.MinValue;pull=velocity=Vector2.zero;if(visual!=null){visual.anchoredPosition=Vector2.zero;visual.localScale=Vector3.one;visual.localRotation=Quaternion.identity;}}
```

```csharp
void OnApplicationFocus(bool value){focused=value;if(!value)ResetPose();}
```

```csharp
void OnApplicationPause(bool pause){focused=!pause;if(pause)ResetPose();}
```

```csharp
void OnDisable(){ResetPose();}
```

## 源字段与依赖

```csharp
RectTransform visual,hit;
```
```csharp
TMP_Text title,detail;
```
```csharp
Func<bool> permitted;
```
```csharp
Vector2 start,pull,velocity;
```
```csharp
int pointer=int.MinValue;
```
```csharp
bool held,focused=true;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
