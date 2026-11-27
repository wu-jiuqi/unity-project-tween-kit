# UIBounce · UIBounce

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/UIBounce.cs`
文件 SHA-256：`1b77e68db3f5fe0713dd8c7a69cc2cd591fb3efc528f41a6fad970b27c2cadc3`
[原始源码快照](../source/Assets/Scripts/UI/UIBounce.cs.txt)

用在哪里：Bootstrap.Boot → AppUI.Awake → ShowPage → CSettingsArt.Button / UICraft.Button
触发操作：悬停、按下、拖拽、松开；命中根保持不动
导入组件：`ProjectTweenKit.UIBounce`；展馆 ID：`press`。
复用形式：独立组件。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Update · L26

方法 SHA-256：`8a23843ba61c4edf4927527fbf123d754283271910257ff4e13bf3b985986a5d`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.UIBounce.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){Initialize();if(!initialized)return;
  if(!focused||!Ready||!MotionAllowed){if(held||hovered||pose!=0||pull!=Vector2.zero)ResetState();idleTime=0;return;}
  if(IdleInterval<=0)return;
  if(held||hovered||(app!=null&&!app.HomeMotionAllowed)){idleTime=0;return;}
  idleTime+=Time.unscaledDeltaTime;
  if(idleTime>=IdleInterval){idleTime=0;IdlePulseCount++;IdleWiggle();}
 }
```

### Animate · L33

方法 SHA-256：`238645a707abf882170787c246a7806a0fe0d03df15b430f7d02c181a71ef18d`
时间来源：SetUpdate(true)
曲线证据：Mathf.LerpUnclamped
解析符号：`BounceGarden.UIBounce.Animate(float, float, DG.Tweening.Ease)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/UIBounce.cs:48` · `UIBounce.OnPointerEnter` → `Animate(1,.16f,Ease.OutBack)`
- `Assets/Scripts/UI/UIBounce.cs:49` · `UIBounce.OnPointerExit` → `Animate(0,.32f,Ease.OutElastic)`
- `Assets/Scripts/UI/UIBounce.cs:53` · `UIBounce.OnPointerDown` → `Animate(-1,.10f,Ease.OutQuad)`
- `Assets/Scripts/UI/UIBounce.cs:63` · `UIBounce.OnPointerUp` → `Animate(hovered?1:0,.48f,Ease.OutElastic)`

```csharp
void Animate(float target,float duration,Ease ease){motion?.Kill();if(!MotionAllowed){pose=0;pull=Vector2.zero;Render();return;}
  float from=pose;Vector2 fromPull=pull;float t=0;
  motion=DOTween.To(()=>t,v=>{t=v;pose=Mathf.LerpUnclamped(from,target,v);pull=Vector2.LerpUnclamped(fromPull,Vector2.zero,v);Render();},1,duration).SetEase(ease).SetUpdate(true);
 }
```

### IdleWiggle · L66

方法 SHA-256：`7470259c41dc9b28751c4ff5ae005ecd8f53efcbc58fd491842f60054b946f1d`
时间来源：SetUpdate(true)
曲线证据：Ease.Linear, Mathf.Sin
解析符号：`BounceGarden.UIBounce.IdleWiggle()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/UIBounce.cs:31` · `UIBounce.Update` → `IdleWiggle()`

```csharp
void IdleWiggle(){motion?.Kill();float t=0;motion=DOTween.To(()=>t,v=>{t=v;float wave=Mathf.Sin(v*Mathf.PI*8)*Mathf.Sin(v*Mathf.PI)*(1-v);pose=wave*.7f;pull=new Vector2(wave*.32f,0);Render();},1,.7f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(()=>{pose=0;pull=Vector2.zero;Render();});}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void ResetState(){motion?.Kill();motion=null;held=hovered=dragged=false;pointer=-999;pose=0;pull=Vector2.zero;idleTime=0;if(initialized)Render();}
```

```csharp
void OnApplicationFocus(bool focus){focused=focus;if(!focus)ResetState();}
```

```csharp
void OnApplicationPause(bool pause){if(pause)ResetState();}
```

```csharp
void OnDisable(){ResetState();}
```

## 源字段与依赖

```csharp
public RectTransform Target;
```
```csharp
public float IdleInterval;
```
```csharp
Button button;
```
```csharp
AppUI app;
```
```csharp
Tween motion;
```
```csharp
Vector3 restScale;
```
```csharp
Vector2 restPosition;
```
```csharp
Quaternion restRotation;
```
```csharp
JellyButtonMesh[] meshes;
```
```csharp
Vector2 start,pull;
```
```csharp
float pose,shear,idleTime;
```
```csharp
bool initialized,hovered,held,dragged,focused=true;
```
```csharp
int pointer=-999;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
