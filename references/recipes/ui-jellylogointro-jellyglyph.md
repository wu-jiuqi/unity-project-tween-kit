# JellyGlyph · JellyLogoIntro

状态：`source-route-reviewed`；交付：`extracted-component`。

源文件：`Assets/Scripts/UI/JellyLogoIntro.cs`
文件 SHA-256：`b555af8ce8910e5c3c35bfb6b6a3abedf91be7e191cb3ab482036f9aeaeac469`
[原始源码快照](../source/Assets/Scripts/UI/JellyLogoIntro.cs.txt)

用在哪里：JellyLogoIntro.Begin → 每个独立字层 JellyGlyph.Begin → Impact
触发操作：错峰下落、手工帧挤压、齐落后衰减
导入组件：`ProjectTweenKit.JellyGlyph`；展馆 ID：`logo`。
复用形式：组合预制体。绑定、调用与停止见 [接入](../integration.md) / [生命周期](../lifecycle.md)。

## 源方法、参数与曲线

### Begin · L21

方法 SHA-256：`e5781e4ae8730c2f621fbe9db9483a45ea22e88a35246098d40ac8e12a6dfbed`
时间来源：SetUpdate(true), Time.unscaledTime
曲线证据：Ease.InCubic
解析符号：`BounceGarden.JellyGlyph.Begin(int)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
public void Begin(int index){landingPitch=.94f+(index%4)*.035f;started=Time.unscaledTime;rect=(RectTransform)transform;rest=rect.anchoredPosition;rect.anchoredPosition=new Vector2(rest.x+(index%2==0?-22:22),1170f-((RectTransform)rect.parent).anchoredPosition.y+rect.rect.height+80);visibility=gameObject.AddComponent<CanvasGroup>();visibility.alpha=0;rect.localRotation=Quaternion.Euler(0,0,index%2==0?-13:12);rect.localScale=new Vector3(.87f,1.14f,1);
 motion=DOTween.Sequence().SetUpdate(true);motion.AppendInterval(.12f+index*.105f);motion.AppendCallback(()=>{DropStarted=true;visibility.alpha=1;});motion.Append(DOTween.To(()=>rect.anchoredPosition,v=>rect.anchoredPosition=v,rest,.5f).SetEase(Ease.InCubic));motion.Join(rect.DOLocalRotate(Vector3.zero,.5f));motion.AppendCallback(()=>StartCoroutine(Impact()));
 }
```

### Impact · L24

方法 SHA-256：`779a65216bbc72683dac93921c7799e05613b3fa4a303e7ebc9481489c7ec3bd`
时间来源：Time.unscaledTime, WaitForSecondsRealtime
曲线证据：Mathf.Exp, Mathf.Sin
解析符号：`BounceGarden.JellyGlyph.Impact()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JellyLogoIntro.cs:22` · `JellyGlyph.Begin` → `Impact()`

```csharp
IEnumerator Impact(){SoundService.Play("jelly_land",landingPitch);for(int frame=0;frame<Poses.Length;frame++){var pose=Poses[frame];rect.localScale=new Vector3(pose.x,pose.y,1);rect.localRotation=Quaternion.Euler(0,0,frame==Poses.Length-1?0:pose.z);deformation=frame==Poses.Length-1?0:Mathf.Sin(frame*1.2f)*(1-frame/(float)Poses.Length);graphic.SetVerticesDirty();yield return new WaitForSecondsRealtime(1f/30f);}DropComplete=true;while(jellyStarted<0)yield return null;InSustainedPhase=true;while(Time.unscaledTime-started<JellyLogoIntro.HoldDuration){float t=Time.unscaledTime-jellyStarted;float remaining=JellyLogoIntro.HoldDuration-(Time.unscaledTime-started);float envelope=Mathf.Exp(-.42f*t)*Mathf.Clamp01(remaining/.65f);float wave=Mathf.Sin(t*11.5f);rect.localScale=new Vector3(1+wave*.17f*envelope,1-wave*.14f*envelope,1);rect.anchoredPosition=rest+new Vector2(0,Mathf.Abs(Mathf.Sin(t*5.75f))*21*envelope);rect.localRotation=Quaternion.Euler(0,0,Mathf.Sin(t*8)*3.8f*envelope);deformation=Mathf.Sin(Mathf.Floor(t*30)/30*11.5f)*.42f*envelope;graphic.SetVerticesDirty();yield return null;}rect.anchoredPosition=rest;rect.localScale=Vector3.one;rect.localRotation=Quaternion.identity;deformation=0;graphic.SetVerticesDirty();}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
protected override void OnDisable(){motion?.Kill();StopAllCoroutines();base.OnDisable();}
```

## 源字段与依赖

```csharp
RectTransform rect;
```
```csharp
Vector2 rest;
```
```csharp
Sequence motion;
```
```csharp
float deformation,started,jellyStarted=-1,landingPitch=1;
```
```csharp
CanvasGroup visibility;
```
```csharp
// Hand-keyed impact poses at 30 fps, layered over the position tween.
 static readonly Vector3[] Poses={new Vector3(1.32f,.65f,-7),new Vector3(1.39f,.61f,-4),new Vector3(1.26f,.73f,4),new Vector3(.87f,1.22f,6),new Vector3(.82f,1.27f,3),new Vector3(.94f,1.12f,-3),new Vector3(1.12f,.87f,-4),new Vector3(1.15f,.85f,-1),new Vector3(.95f,1.07f,2),new Vector3(.97f,1.04f,1),new Vector3(1.04f,.96f,-1),new Vector3(1.02f,.98f,0),Vector3.one};
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
