# HomeANavigation · HomeANavigation

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/HomeANavigation.cs`
文件 SHA-256：`42c498110af7ba98e573c6c925e9ffb8292f43c5d0e1ff45ddd3dec357c244f3`
[原始源码快照](../source/Assets/Scripts/UI/HomeANavigation.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Update · L20

方法 SHA-256：`5cb20110c05bab764dc848353022d87826b67b3f299dedc37ffe8f06fea6f5ac`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Cos, Mathf.Exp, Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.HomeANavigation.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(plate==null)return;if(!focused||!FeedbackProfile.Active.enabledFeedback){Snap();return;}float dt=Mathf.Min(Time.unscaledDeltaTime,.05f);plateTime=Mathf.Min(1,plateTime+dt/.32f);plate.anchoredPosition=new Vector2(Mathf.Lerp(plateFrom,plateTo,1-Mathf.Pow(1-plateTime,3)),0);
  for(int i=0;i<5;i++){var p=poses[i];if(!p.moving)continue;p.time+=dt;float t=Mathf.Clamp01(p.time/(p.active?.85f:.22f));if(t>=1){Finish(i);continue;}
   if(!p.active){float q=1-Mathf.Pow(1-t,3);p.y=Mathf.Lerp(p.fromY,0,q);p.xs=Mathf.Lerp(p.fromX,1,q);p.ys=Mathf.Lerp(p.fromScaleY,1,q);p.r=Mathf.Lerp(p.fromR,0,q);}
   else if(t<.12f){float q=t/.12f;p.y=p.fromY-5*q;p.xs=Mathf.Lerp(p.fromX,1.16f,q);p.ys=Mathf.Lerp(p.fromScaleY,.78f,q);p.r=p.fromR*(1-q);}
   else if(t<.38f){float q=(t-.12f)/.26f;p.y=Mathf.Lerp(p.fromY-5,80,1-Mathf.Pow(1-q,3));p.xs=Mathf.Lerp(1.16f,.92f,q);p.ys=Mathf.Lerp(.78f,1.36f,q);p.r=0;}
   else{float q=(t-.38f)/.62f,envelope=Mathf.Exp(-5*q)*(1-q),wave=Mathf.Sin(q*Mathf.PI*4)*.16f*envelope;p.y=12+68*envelope*Mathf.Cos(q*Mathf.PI*3.5f);float v=1-Mathf.Pow(1-q,2);p.xs=Mathf.Lerp(.92f,1.06f,v)+wave;p.ys=Mathf.Lerp(1.36f,1.06f,v)-wave;p.r=-Mathf.Sin(q*11)*4*envelope;}
   poses[i]=p;Render(i);
  }
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){Snap();}
```

```csharp
void OnApplicationFocus(bool value){focused=value;if(!value)Snap();}
```

```csharp
void OnApplicationPause(bool paused){focused=!paused;if(paused)Snap();}
```

## 源字段与依赖

```csharp
readonly Button[] buttons=new Button[5];
```
```csharp
readonly RectTransform[] icons=new RectTransform[5];
```
```csharp
readonly TMP_Text[] labels=new TMP_Text[5];
```
```csharp
readonly UnityEngine.UI.Image[] paws=new UnityEngine.UI.Image[5];
```
```csharp
readonly Pose[] poses=new Pose[5];
```
```csharp
RectTransform plate;
```
```csharp
int selected=-1;
```
```csharp
float layoutScale=1,plateTime=1,plateFrom,plateTo;
```
```csharp
bool focused=true;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
