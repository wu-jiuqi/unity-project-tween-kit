# CollectionGoalEntrance · CollectionGoalEntrance

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/CollectionGoalEntrance.cs`
文件 SHA-256：`e830eb8ef97a4c7d3c66b949778e9dba82e22790c0a865dd2a9ee6ab208fa4f8`
[原始源码快照](../source/Assets/Scripts/UI/CollectionGoalEntrance.cs.txt)

用在哪里：AppUI.StartPreparedLevel → ShowCollectionGoalEntrance → CollectionGoalEntrance.Begin(hud.GoalsRect)
触发操作：本关目标先居中展示，再返回真实 HUD 容器
导入组件：`ProjectTweenKit.CollectionGoalEntrance`；展馆 ID：`goal`。
复用形式：宿主适配配方。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Render · L32

方法 SHA-256：`11b667282d0c864a6f121ff06e128a809d5daed2e86e2f87ca1afce756d82f92`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.LerpUnclamped, Mathf.Sin, Mathf.SmoothStep
解析符号：`BounceGarden.CollectionGoalEntrance.Render()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/CollectionGoalEntrance.cs:28` · `CollectionGoalEntrance.Begin` → `Render()`
- `Assets/Scripts/UI/CollectionGoalEntrance.cs:43` · `CollectionGoalEntrance.Update` → `Render()`

```csharp
void Render(){if(goals==null||home==null)return;
  float fit=Mathf.Min(root.rect.width/1080f,root.rect.height/2340f);
  float enter=UICraft.OutBack(Mathf.Clamp01(age/.32f)),fly=Mathf.SmoothStep(0,1,Mathf.Clamp01((age-1.45f)/.75f));
  float enlarged=Mathf.Min(1.85f*fit,root.rect.width*.86f/Mathf.Max(1,goals.rect.width));
  goals.anchoredPosition=Vector2.Lerp(Vector2.zero,Destination(),fly);goals.localScale=Vector3.Lerp(Vector3.one*enlarged*Mathf.LerpUnclamped(.65f,1,enter),DestinationScale(),fly);goals.localRotation=Quaternion.identity;
  float fade=1-Mathf.SmoothStep(0,1,Mathf.Clamp01((age-1.35f)/.8f));
  dim.color=new Color(.035f,.025f,.02f,.57f*Mathf.Clamp01(age/.18f)*fade);copy.alpha=Mathf.Clamp01(age/.18f)*fade;
  title.rectTransform.anchoredPosition=new Vector2(0,270*fit);title.rectTransform.localScale=Vector3.one*fit;
  halo.localScale=Vector3.one*fit*(1+.055f*Mathf.Sin(age*4));halo.localRotation=Quaternion.Euler(0,0,age*8);
  halo.GetComponent<Image>().color=new Color(1,.94f,.63f,.85f*fade);
 }
```

### Update · L43

方法 SHA-256：`1808371fa91a0d1d816beb242363066beb6714ec48c81945d77779683e40dd1b`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.CollectionGoalEntrance.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(suspended||(!focused&&!RuntimeQA.IsIsolated))return;if(home==null||goals==null){age=Duration;return;}age+=Mathf.Min(Time.unscaledDeltaTime,.05f);Render();}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public void Restore(){if(restored)return;restored=true;if(goals==null||home==null)return;goals.SetParent(home,false);goals.SetSiblingIndex(sibling);goals.anchoredPosition=homePosition;goals.localScale=homeScale;goals.localRotation=homeRotation;}
```

```csharp
void OnApplicationPause(bool value){suspended=value;}
```

```csharp
void OnApplicationFocus(bool value){focused=value;}
```

```csharp
void OnDisable(){Restore();}
```

```csharp
void OnDestroy(){Restore();}
```

## 源字段与依赖

```csharp
public const float Duration=2.35f;
```
```csharp
RectTransform root,goals,home,halo;
```
```csharp
int sibling;
```
```csharp
Vector2 homePosition;
```
```csharp
Vector3 homeScale;
```
```csharp
Quaternion homeRotation;
```
```csharp
Image dim;
```
```csharp
CanvasGroup copy;
```
```csharp
TMPro.TMP_Text title;
```
```csharp
float age;
```
```csharp
bool restored,suspended,focused=true;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
