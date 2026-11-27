# AppUI · AppUI.ContentLoading

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.ContentLoading.cs`
文件 SHA-256：`a925e7dc0fd4de317d5c16880c627b46ffaf118f619327bf2f72af7c22f1ab11`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.ContentLoading.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### PrefetchFollowingLevel · L12

方法 SHA-256：`eb11829f4af12f64daf2562dcdc0fa9ef453f00991e41588bc79e42846841ed0`
时间来源：WaitForSecondsRealtime
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.PrefetchFollowingLevel(int, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:183` · `AppUI.StartPreparedLevel` → `PrefetchFollowingLevel(currentLevel,generation)`

```csharp
IEnumerator PrefetchFollowingLevel(int index,int generation){
  if(GameContent.Prefetch==null)yield break;
  yield return new WaitForSecondsRealtime(1);
  if(generation!=viewGeneration||!Session.IsPlaying)yield break;
  var operation=new ContentLoadOperation();contentPrefetch=operation;
  try{
   // Victory is already prepared. Spend idle bandwidth on the next level,
   // then navigation; optional loss art cannot hold these requests behind it.
   prefetchMenuPage=null;prefetchLevelIndex=index+1;
   if(index+1<OpeningCampaign.Count)yield return operation.Run(GameContent.PrefetchLevel(index+1));
   if(!operation.Succeeded||contentLoad!=null||!Session.IsPlaying)yield break;
   prefetchLevelIndex=-1;prefetchMenuPage="chapters";
   yield return operation.Run(GameContent.PreparePage("chapters"));
   if(!operation.Succeeded||contentLoad!=null||!Session.IsPlaying)yield break;
   prefetchMenuPage="result-lose";yield return operation.Run(GameContent.PreparePage("result-lose"));
  }finally{FinishPrefetch(operation);}
  if(!operation.Cancelled)Debug.Log("BOUNCE_NEXT_PREFETCH "+(operation.Succeeded?"ready":"retry-on-entry")+" level="+(index+2));
 }
```

### PrepareOutcomeRoutine · L41

方法 SHA-256：`036224879c2e3f938871b3e768de862dfd7931c7196e11b8da1462100fae6682`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.PrepareOutcomeRoutine(bool, string, BounceGarden.ContentLoadOperation, UnityEngine.RectTransform, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.ContentLoading.cs:39` · `AppUI.PrepareOutcomeContent` → `PrepareOutcomeRoutine(win,page,operation,root,viewGeneration)`

```csharp
IEnumerator PrepareOutcomeRoutine(bool win,string page,ContentLoadOperation operation,RectTransform root,int generation){
  yield return operation.Run(GameContent.PreparePage(page));
  if(operation.Cancelled||contentLoad!=operation||generation!=viewGeneration)yield break;
  contentLoad=null;loadingMenuPage=null;
  if(operation.Succeeded){Destroy(root.gameObject);OnOutcome(win);yield break;}
  Debug.LogWarning("BOUNCE_RESULT_LOAD_FAILED "+(operation.TimedOut?"timeout":operation.Failure?.Message));
  Destroy(root.gameObject);OpenErrorPopup0411("网络连接中断","本局仍可继续结算，请重试连接。",()=>{RemoveModal(false);PrepareOutcomeContent(win);},()=>{RemoveModal(false);CancelLoadingToHome();});
 }
```

### RunLevelLoad · L50

方法 SHA-256：`67d116986af58e8d549cc9c76ed5850df65d78a4a24c0f7573d920b284f37d9a`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.RunLevelLoad(UnityEngine.RectTransform, int, int, bool, bool, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:155` · `AppUI.BeginLevel` → `RunLevelLoad(loading,viewGeneration,index,tutorialMode,skipOnboarding,trial)`

```csharp
IEnumerator RunLevelLoad(RectTransform loading,int generation,int index,bool tutorialMode,bool skipOnboarding,int trial){
  var operation=new ContentLoadOperation();contentLoad=operation;
  var presentation=loading.GetComponent<LoadingPresentation>();
  yield return operation.Run(StartPreparedLevel(loading,generation));
  if(operation.Cancelled||generation!=viewGeneration)yield break;
  if(contentLoad==operation)contentLoad=null;
  if(!operation.Succeeded){
   Session.StopLevel();busy=false;playing=false;
   if(presentation!=null)presentation.SuspendForError();
   OpenErrorPopup0411("暂时未能进入关卡",operation.TimedOut?"等待时间较长，请检查网络。\n已准备的资源会保留，可重试或返回花园。":"本关准备未完成。\n请重试，或返回花园。",()=>BeginLevel(index,tutorialMode,skipOnboarding,trial),CancelLoadingToHome);
   Debug.LogWarning("BOUNCE_LEVEL_LOAD_FAILED "+(operation.TimedOut?"timeout":operation.Failure?.Message));
  }
 }
```

### PrepareMenuContentRoutine · L64

方法 SHA-256：`c65a0c335493b1bdef3194d9bd6345e37e0d3ee28a1785a2b6bbc34602ae0103`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.PrepareMenuContentRoutine(string, bool, BounceGarden.ContentLoadOperation)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.ContentLoading.cs:63` · `AppUI.PrepareMenuContent` → `PrepareMenuContentRoutine(page,animate,operation)`

```csharp
IEnumerator PrepareMenuContentRoutine(string page,bool animate,ContentLoadOperation operation){
  Toast(page=="ranking"||page=="vip"||page=="league-detail"||page=="league-prizes"?"正在准备花园联赛…":page=="shop"||page=="shop-detail"?"正在准备小铺…":page=="dressing"?"正在准备装扮…":"正在准备关卡地图…");
  yield return operation.Run(GameContent.PreparePage(page));
  if(operation.Cancelled||contentLoad!=operation)yield break;
  contentLoad=null;loadingMenuPage=null;
  if(operation.Succeeded)ShowPage(page,animate);else Toast(operation.TimedOut?"等待时间较长，请检查网络后重试。":"资源准备未完成，请重试。");
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void CancelContentLoad(bool keepPrefetch=false){contentLoad?.Cancel();contentLoad=null;if(!keepPrefetch){contentPrefetch?.Cancel();contentPrefetch=null;prefetchLevelIndex=-1;prefetchMenuPage=null;}loadingMenuPage=null;}
```

```csharp
void CancelContentForLevel(int index){CancelContentLoad(contentPrefetch!=null&&prefetchLevelIndex==index);}
```

```csharp
void CancelLoadingToHome(){CancelContentLoad();viewGeneration++;busy=false;Session.StopLevel();ShowPage("home",false);}
```

## 源字段与依赖

```csharp
ContentLoadOperation contentLoad,contentPrefetch;
```
```csharp
string loadingMenuPage;
```
```csharp
int prefetchLevelIndex=-1;
```
```csharp
string prefetchMenuPage;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
