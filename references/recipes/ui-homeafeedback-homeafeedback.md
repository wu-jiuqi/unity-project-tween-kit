# HomeAFeedback · HomeAFeedback

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/HomeAFeedback.cs`
文件 SHA-256：`a586099a31ad687ae84fc58a877525df9aad987f9a3b407d3eeff987263c67a0`
[原始源码快照](../source/Assets/Scripts/UI/HomeAFeedback.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Create · L11

方法 SHA-256：`10dae324e8de5c67a04439efe006cbf2f685c0bd56b40afc1dc0fd9d23251c37`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.HomeAFeedback.Create(UnityEngine.Transform)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public static HomeAFeedback Create(Transform parent){var root=UICraft.Root(parent,"Home achievement feedback",0,0,950,240);var view=root.gameObject.AddComponent<HomeAFeedback>();view.app=root.GetComponentInParent<AppUI>();var hit=root.gameObject.AddComponent<UnityEngine.UI.Image>();hit.color=Color.clear;hit.raycastTarget=true;
  var visual=UICraft.Root(root,"Encouragement drag visual",0,0,950,240);view.titleMotion=UICraft.Root(visual,"Headline motion",(InnerLeft+InnerRight)*.5f,45,700,120);view.detailMotion=UICraft.Root(visual,"Detail motion",0,-52.5f,710,65);
  view.title=HomeAArt.Text(view.titleMotion,"Home A encouragement title","",0,0,700,120,90);view.detail=HomeAArt.Text(view.detailMotion,"Home A encouragement detail","",0,0,710,65,45);view.mode=HomeAArt.Text(visual,"Feedback preview indicator","",0,-108,800,24,18);
  view.material=new Material(view.title.fontSharedMaterial);view.material.EnableKeyword("OUTLINE_ON");view.material.SetFloat("_FaceDilate",.12f);view.material.SetColor("_OutlineColor",HomeAArt.Cream);view.material.SetFloat("_OutlineWidth",.18f);view.material.SetFloat("_OutlineSoftness",0);view.material.DisableKeyword("UNDERLAY_ON");view.title.fontSharedMaterial=view.material;view.title.UpdateMeshPadding();
  HomeAArt.Image(visual,"encourage-left",-357.5f,42,95,136);HomeAArt.Image(visual,"encourage-right",357.5f,42,95,136);
  root.gameObject.AddComponent<EncouragementDrag>().Initialize(visual,view.title,view.detail,()=>view.focused&&(view.app==null||view.app.HomeMotionAllowed));return view;
 }
```

### Update · L21

方法 SHA-256：`04911c6d87437d4abf08d3481811fa704803908fa446d77489d2b29fe66c7d96`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.HomeAFeedback.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(title==null)return;if(!focused||!FeedbackProfile.Active.enabledFeedback||app!=null&&!app.HomeMotionAllowed){ResetPose();return;}float dt=Mathf.Min(Time.unscaledDeltaTime,.05f);idle+=dt;if(entrance>=0){entrance+=dt;if(entrance>1.35f)entrance=-1;}float breath=Mathf.Sin(idle*1.8f),wave=HomeFeedbackView.Pop(entrance,0)*strength,follow=HomeFeedbackView.Pop(entrance,.13f)*strength;
  titleMotion.localScale=new Vector3(1+breath*.018f-wave*.065f,1+breath*.018f+wave*.12f,1);titleMotion.anchoredPosition=new Vector2((InnerLeft+InnerRight)*.5f,45+breath*3+wave*13);detailMotion.localScale=Vector3.one*(1+Mathf.Sin(idle*1.8f-.6f)*.009f+follow*.045f);detailMotion.anchoredPosition=new Vector2(0,-52.5f+Mathf.Sin(idle*1.8f-.6f)*2+follow*9);
 }
```

## 对象层级和素材入口

- L11：`UICraft.Root(parent,"Home achievement feedback",0,0,950,240)`
- L12：`UICraft.Root(root,"Encouragement drag visual",0,0,950,240)`
- L15：`HomeAArt.Image(visual,"encourage-left",-357.5f,42,95,136)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void ResetPose(){entrance=-1;if(titleMotion!=null){titleMotion.localScale=Vector3.one;titleMotion.anchoredPosition=new Vector2((InnerLeft+InnerRight)*.5f,45);}if(detailMotion!=null){detailMotion.localScale=Vector3.one;detailMotion.anchoredPosition=new Vector2(0,-52.5f);}}
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

```csharp
void OnDestroy(){if(material!=null)Destroy(material);}
```

## 源字段与依赖

```csharp
TMP_Text title,detail,mode;
```
```csharp
RectTransform titleMotion,detailMotion;
```
```csharp
AppUI app;
```
```csharp
Material material;
```
```csharp
float entrance=-1,idle,strength=1;
```
```csharp
bool focused=true,pending;
```
```csharp
const float InnerLeft=-311.3318f,InnerRight=315.21795f;
```
```csharp
string rendered;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
