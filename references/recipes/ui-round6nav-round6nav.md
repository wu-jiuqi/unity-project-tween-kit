# Round6Nav · Round6Nav

状态：`legacy-route-disabled`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/Round6Nav.cs`
文件 SHA-256：`77a6c3ee86b861b455367eb5e71628fcb0c55cdc98703fe7e176105090d03bd9`
[原始源码快照](../source/Assets/Scripts/UI/Round6Nav.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Select · L17

方法 SHA-256：`599f4ec471456fdec2323194d1a72420756a62d174d94e0192b2cf54a50bfce2`
时间来源：SetUpdate(true)
曲线证据：Ease.OutCubic
解析符号：`BounceGarden.Round6Nav.Select(string, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/Round6Nav.cs:35` · `Round6Nav.OnEnable` → `Select(key,selectedLevel)`

```csharp
public void Select(string key,int level=0){selectedLevel=level;ApplyDifficulty(level);if(selected==key)return;selected=key;
 for(int i=0;i<3;i++){int index=i;bool active=Keys[i]==key;buttons[i].interactable=!active;panels[i].enabled=active;moves[i]?.Kill();int version=++popVersions[i];strips[i].gameObject.SetActive(active);captions[i].alpha=0;
 if(!FeedbackProfile.Active.enabledFeedback){SetFinal(i,active);continue;}
 var icon=icons[i];Vector2 from=icon.anchoredPosition;Vector3 scale=icon.localScale;float angle=icon.localEulerAngles.z;float progress=0;
 if(active){icon.localScale=new Vector3(1.16f,.78f,1);StartCoroutine(StartPopAfterVisibleFrame(index,version,from));}
 else moves[i]=DOTween.To(()=>progress,v=>{progress=v;icon.anchoredPosition=Vector2.Lerp(from,Vector2.zero,v);icon.localScale=Vector3.Lerp(scale,Vector3.one,v);icon.localRotation=Quaternion.Euler(0,0,Mathf.LerpAngle(angle,0,v));},1,.22f).SetEase(Ease.OutCubic).SetUpdate(true).OnComplete(()=>SetFinal(index,false));
 }}
```

### StartPopAfterVisibleFrame · L24

方法 SHA-256：`c8e4a9f79f455d8954bfd3f7ecea4bbc45b799c34fe15a936474194c8762940a`
时间来源：SetUpdate(true)
曲线证据：Ease.Linear
解析符号：`BounceGarden.Round6Nav.StartPopAfterVisibleFrame(int, int, UnityEngine.Vector2)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/Round6Nav.cs:21` · `Round6Nav.Select` → `StartPopAfterVisibleFrame(index,version,from)`

```csharp
IEnumerator StartPopAfterVisibleFrame(int i,int version,Vector2 from){yield return new WaitForEndOfFrame();yield return null;if(version!=popVersions[i]||!isActiveAndEnabled)yield break;float progress=0;var scale=new Vector3(1.16f,.78f,1);moves[i]=DOTween.To(()=>progress,v=>{progress=v;RenderPop(i,v,from,scale);},1,PopDuration).SetEase(Ease.Linear).SetUpdate(true).OnComplete(()=>SetFinal(i,true));}
```

### RenderPop · L25

方法 SHA-256：`0a069b7cc244f3ea0eb4810662e9a4d2b970161ad5fec9add236fc80a012f7f6`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Exp, Mathf.Pow, Mathf.Sin
解析符号：`BounceGarden.Round6Nav.RenderPop(int, float, UnityEngine.Vector2, UnityEngine.Vector3)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/Round6Nav.cs:24` · `Round6Nav.StartPopAfterVisibleFrame` → `RenderPop(i,v,from,scale)`

```csharp
void RenderPop(int i,float t,Vector2 from,Vector3 startScale){var icon=icons[i];float y;Vector3 scale;float rotation=0;
 if(t<.12f){float q=t/.12f;y=Mathf.Lerp(from.y,from.y-5,q);scale=Vector3.Lerp(startScale,new Vector3(1.16f,.78f,1),q);}
 else if(t<.38f){float q=(t-.12f)/.26f;y=Mathf.Lerp(from.y-5,RaisedY+22,1-Mathf.Pow(1-q,3));scale=Vector3.Lerp(new Vector3(1.16f,.78f,1),new Vector3(.92f,1.36f,1),q);}
 else{float q=(t-.38f)/.62f;float envelope=Mathf.Exp(-5*q)*(1-q);y=RaisedY+22*envelope*Mathf.Cos(q*Mathf.PI*3.5f);float wobble=Mathf.Sin(q*Mathf.PI*4)*.16f*envelope;scale=Vector3.Lerp(new Vector3(.92f,1.36f,1),new Vector3(1.24f,1.24f,1),1-Mathf.Pow(1-q,2))+new Vector3(wobble,-wobble,0);rotation=Mathf.Sin(q*11)*4*envelope;}
 icon.anchoredPosition=new Vector2(0,y);icon.localScale=scale;icon.localRotation=Quaternion.Euler(0,0,rotation);captions[i].alpha=Mathf.Clamp01((t*PopDuration-.24f)/.18f);
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void OnApplicationFocus(bool focused){if(!focused&&icons[0]!=null)SnapSelection();}
```

```csharp
void OnDisable(){SnapSelection();}
```

## 源字段与依赖

```csharp
public const float RaisedY=84,PopDuration=.85f;
```
```csharp
readonly Button[] buttons=new Button[3];
```
```csharp
readonly Image[] panels=new Image[3],strips=new Image[3];
```
```csharp
readonly RectTransform[] icons=new RectTransform[3];
```
```csharp
readonly TMP_Text[] labels=new TMP_Text[3];
```
```csharp
readonly CanvasGroup[] captions=new CanvasGroup[3];
```
```csharp
readonly Tween[] moves=new Tween[3];
```
```csharp
readonly int[] popVersions=new int[3];
```
```csharp
string selected;
```
```csharp
int selectedLevel;
```
```csharp
static readonly string[] Keys={"shop","home","ranking"};
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
