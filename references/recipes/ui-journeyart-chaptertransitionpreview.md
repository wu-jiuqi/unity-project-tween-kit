# ChapterTransitionPreview · JourneyArt

状态：`inactive-preview`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/JourneyArt.cs`
文件 SHA-256：`268f21ca9b4064170115073a3fe0abc28296ef2cac45824f8a3ba1eb5e6d17fa`
[原始源码快照](../source/Assets/Scripts/UI/JourneyArt.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Sample · L16

方法 SHA-256：`75f5ed2be4d04b71794f0144ab2521296ccf3e56b6d83a3b7568e47a25d9a378`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Sin
解析符号：`BounceGarden.ChapterTransitionPreview.Sample(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/JourneyArt.cs:15` · `ChapterTransitionPreview.Build` → `Sample(0)`
- `Assets/Scripts/UI/JourneyArt.cs:17` · `ChapterTransitionPreview.Update` → `Sample(age)`

```csharp
public void Sample(float seconds){float t=Mathf.Clamp01((seconds-.65f)/1.35f);float q=t*t*(3-2*t);oldCard.anchoredPosition=new Vector2(-1050*q,Mathf.Sin(q*Mathf.PI)*50);oldCard.localRotation=Quaternion.Euler(0,0,-q*7);oldGroup.alpha=1-q;newCard.anchoredPosition=new Vector2(1050*(1-q),Mathf.Sin(q*Mathf.PI)*50);newCard.localRotation=Quaternion.Euler(0,0,(1-q)*7);newGroup.alpha=q;newCard.localScale=Vector3.one*Mathf.Lerp(.88f,1,q);title.text=t<.45f?"这一座花园，完成啦！":"第"+(next+1)+"章 · "+ChapterSceneArt.Title(next);note.text=t<.45f?"收好回忆，继续出发":"新的花园，等你来探索";}
```

### Update · L17

方法 SHA-256：`cf3bf6380626fa8632c8e26b9ee3001af0be29545882f77852f9d9e493e066e7`
时间来源：Time.unscaledDeltaTime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.ChapterTransitionPreview.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(playing){age+=Time.unscaledDeltaTime;Sample(age);if(age>=3.2f)playing=false;}}
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
RectTransform oldCard,newCard;
```
```csharp
CanvasGroup oldGroup,newGroup;
```
```csharp
TMPro.TMP_Text title,note;
```
```csharp
float age;
```
```csharp
int next;
```
```csharp
bool playing=true;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
