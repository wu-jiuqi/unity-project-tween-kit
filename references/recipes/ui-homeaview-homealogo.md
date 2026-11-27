# HomeALogo · HomeAView

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/HomeAView.cs`
文件 SHA-256：`e2be761f13387c77a667bd70cf1905ed9c0cefa99433be1ca29f5766ec9db166`
[原始源码快照](../source/Assets/Scripts/UI/HomeAView.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Render · L37

方法 SHA-256：`b6154c023436fab1c6a8904a205845197868b8bbdbe815046a4b6dcf32631453`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.HomeALogo.Render(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/HomeAView.cs:35` · `HomeALogo.Build` → `Render(-1)`
- `Assets/Scripts/UI/HomeAView.cs:43` · `HomeALogo.Update` → `Render(-1)`
- `Assets/Scripts/UI/HomeAView.cs:43` · `HomeALogo.Update` → `Render(elapsed)`
- `Assets/Scripts/UI/HomeAView.cs:44` · `HomeALogo.OnDisable` → `Render(-1)`
- `Assets/Scripts/UI/HomeAView.cs:44` · `HomeALogo.OnApplicationFocus` → `Render(-1)`
- `Assets/Scripts/UI/HomeAView.cs:44` · `HomeALogo.OnApplicationPause` → `Render(-1)`

```csharp
public void Render(float time){float age=time<0?-1:time%Period,s=1;Phase="Rest";if(age>=0){if(age<.24f){s=1.5f*(1-Mathf.Pow(1-age/.24f,3));Phase="Appear";}else if(age<.58f){s=1.5f-.5f*Mathf.Sin((age-.24f)/.34f*Mathf.PI*.5f);Phase="Settle";}else if(age<2.98f)Phase="Hold";else if(age<3.32f){s=1.5f-.5f*Mathf.Cos((age-2.98f)/.34f*Mathf.PI*.5f);Phase="ExitGrow";}else if(age<3.56f){s=1.5f*(1-Mathf.Pow((age-3.32f)/.24f,3));Phase="ExitShrink";}else{s=0;Phase="Hidden";}}
  for(int i=0;i<parts.Count;i++){var p=parts[i];float scale=p.key.StartsWith("letter-",StringComparison.Ordinal)?1:s,wave=0,lift=0;
   if(Phase=="Hold"&&p.key.StartsWith("jelly-",StringComparison.Ordinal))foreach(float start in PulseStarts){float q=(age-.58f-start-i*.055f)/.62f;if(q>0&&q<1){float envelope=Mathf.Pow(Mathf.Sin(q*Mathf.PI),2);wave+=Mathf.Sin(q*Mathf.PI*4)*envelope;lift+=envelope*9;}}
   p.rect.localScale=Vector3.Scale(p.scale,new Vector3(scale*(1+wave*.14f),scale*(1-wave*.12f),1));p.rect.localRotation=Quaternion.Euler(0,0,wave*2.4f);p.rect.anchoredPosition=p.rest+Vector2.up*lift;
  }
 }
```

### Update · L43

方法 SHA-256：`5c46abd3674c8075a45fee1ed80f2935ab0fe8a52d6775f6e017c6bc0d66eb69`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.HomeALogo.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(parts.Count==0)return;if(!focused||!FeedbackProfile.Active.enabledFeedback||app!=null&&!app.HomeMotionAllowed){elapsed=0;Render(-1);return;}elapsed+=Mathf.Min(Time.unscaledDeltaTime,.05f);Render(elapsed);}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnDisable(){elapsed=0;Render(-1);}
```

```csharp
void OnApplicationFocus(bool value){focused=value;if(!value){elapsed=0;Render(-1);}}
```

```csharp
void OnApplicationPause(bool paused){focused=!paused;if(paused){elapsed=0;Render(-1);}}
```

## 源字段与依赖

```csharp
static readonly float[] PulseStarts={.24f,1.22f};
```
```csharp
readonly System.Collections.Generic.List<Part> parts=new System.Collections.Generic.List<Part>();
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
```csharp
public const float Period=4.11f;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
