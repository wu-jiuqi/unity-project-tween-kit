# LoadingBoxAnimation · LoadingBoxAnimation

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/LoadingBoxAnimation.cs`
文件 SHA-256：`31f9215f9f6b55fcbadf43a7dfe02a794a896b2fcbf5460b7adb7a9cf72153b3`
[原始源码快照](../source/Assets/Scripts/UI/LoadingBoxAnimation.cs.txt)

本分组已交付以下表现片段；其余宿主方法仍需继续核对，不代表完整系统已提取。

- `LoadingBoxAnimation` / `loadingbox`：0.5.1表现切片；KitClock替换Unity时钟；宿主业务与屏幕布局适配见 current-051.md

接线与边界见[接入](../integration.md)和[补充交付记录](../supplemental-deliveries.json)。

## 源方法、参数与曲线

### Render · L30

方法 SHA-256：`0581ec87a7d31c9a42ff6fa3069155b653985be88ea8a458f5426a3a75cfc724`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.LoadingBoxAnimation.Render(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/ErrorPopup0411.cs:34` · `ErrorPopup0411.Update` → `.Render(Age)`
- `Assets/Scripts/UI/LoadingBoxAnimation.cs:27` · `LoadingBoxAnimation.Build` → `Render(0)`
- `Assets/Scripts/UI/LoadingPresentation.cs:59` · `LoadingPresentation.Render` → `box.Render(Elapsed)`

```csharp
public void Render(float elapsed){Elapsed=elapsed;
  if(hero==null)return;
  float drop=Mathf.Clamp01(Elapsed/.38f);float bounce=Mathf.Clamp01((Elapsed-.38f)/.24f);
  hero.anchoredPosition=new Vector2(0,430*(1-drop*drop));
  hero.localScale=Vector3.one*(1+.16f*Mathf.Sin(bounce*Mathf.PI));
  shadow.localScale=Vector3.one*Mathf.Lerp(.55f,1,drop);
  float t=Mathf.Repeat(Mathf.Max(0,Elapsed-.62f),CatCycleDuration);
  CurrentPose=Elapsed<.38f?"BoxDrop":Elapsed<.62f?"BoxBounce":t<1.2f?"BricksPop":t<1.65f?"CatPop":t<2.9f?"Hold":t<3.85f?"Disappear":"Empty";
  for(int i=0;i<5;i++)AnimatePart(bricks[i],brickAlpha[i],destinations[i],t,i*.18f,3.15f+i*.075f,1.18f);
  AnimatePart(cat,catAlpha,catDestination,t,1.12f,2.94f,1.15f);
  catPaws.localScale=cat.localScale;catPaws.anchoredPosition=cat.anchoredPosition+(pawsDestination-catDestination)*cat.localScale.x;pawsAlpha.alpha=catAlpha.alpha;
  float grow=Mathf.Clamp01((Elapsed-.42f)/.65f);sprout.localScale=new Vector3(Ease(grow),Mathf.Max(.001f,UICraft.OutBack(grow)),1);
  sprout.localRotation=Quaternion.Euler(0,0,Mathf.Sin(Mathf.Max(0,Elapsed-1.07f)*2.2f)*9*grow);
 }
```

### AnimatePart · L44

方法 SHA-256：`39c4ec5ccf99b6469867c6b8d67d0f1c20325836cc3c787c82e252000c49a209`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.SmoothStep
解析符号：`BounceGarden.LoadingBoxAnimation.AnimatePart(UnityEngine.RectTransform, UnityEngine.CanvasGroup, UnityEngine.Vector2, float, float, float, float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/LoadingBoxAnimation.cs:38` · `LoadingBoxAnimation.Render` → `AnimatePart(bricks[i],brickAlpha[i],destinations[i],t,i*.18f,3.15f+i*.075f,1.18f)`
- `Assets/Scripts/UI/LoadingBoxAnimation.cs:39` · `LoadingBoxAnimation.Render` → `AnimatePart(cat,catAlpha,catDestination,t,1.12f,2.94f,1.15f)`

```csharp
void AnimatePart(RectTransform part,CanvasGroup alpha,Vector2 destination,float t,float delay,float hide,float overshoot){
  if(Elapsed<.62f||t<delay){part.localScale=Vector3.one*.001f;alpha.alpha=0;return;}
  float age=t-delay,scale;float y=0,a=1;
  if(age<.19f){float p=Ease(age/.19f);scale=Mathf.Lerp(.08f,overshoot,p);y=Mathf.Lerp(-130,18,p);}
  else if(age<.43f){float p=Mathf.SmoothStep(0,1,(age-.19f)/.24f);scale=Mathf.Lerp(overshoot,1,p);y=Mathf.Lerp(18,0,p);}
  else scale=1;
  if(t>=hide){float p=Mathf.Clamp01((t-hide)/.42f);scale=p<.18f?Mathf.Lerp(1,1.07f,p/.18f):Mathf.Lerp(1.07f,.001f,Ease((p-.18f)/.82f));y=-150*Ease(p);a=1-Mathf.SmoothStep(0,1,p);}
  part.anchoredPosition=destination+Vector2.up*y;part.localScale=Vector3.one*scale;alpha.alpha=a;
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
public const float CatCycleDuration=4.2f;
```
```csharp
RectTransform hero,cat,catPaws,sprout,shadow;
```
```csharp
RectTransform[] bricks=new RectTransform[5];
```
```csharp
CanvasGroup[] brickAlpha=new CanvasGroup[5];
```
```csharp
CanvasGroup catAlpha,pawsAlpha;
```
```csharp
Vector2[] destinations;
```
```csharp
Vector2 catDestination,pawsDestination;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
