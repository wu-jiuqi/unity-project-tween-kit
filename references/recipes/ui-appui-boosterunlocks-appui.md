# AppUI · AppUI.BoosterUnlocks

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/AppUI.BoosterUnlocks.cs`
文件 SHA-256：`5d878fe433aec6a5f70f71192a4201c0a80d0e23af1b7097644b8e7b03b44695`
[原始源码快照](../source/Assets/Scripts/UI/AppUI.BoosterUnlocks.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### ShowBoosterUnlocks · L8

方法 SHA-256：`172cd6495b32ab0af541cf062227659a1fa34ed216265c55be027bf83acb41b6`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.AppUI.ShowBoosterUnlocks(int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:177` · `AppUI.StartPreparedLevel` → `ShowBoosterUnlocks(generation)`

```csharp
IEnumerator ShowBoosterUnlocks(int generation){
  if(Session.IsTutorial||Session.IsStreakTrial)yield break;
  for(int i=1;i<=3;i++){var k=(StreakBooster)i;if(!Profile.BoosterUnlocked(k,currentLevel))continue;if(!Profile.GrantBoosterUnlock(k,currentLevel)){Toast("道具赠送保存失败，请稍后重试。");continue;}
   if(!BattleToolsVisible||Profile.BoosterIntroSeen(k))continue;
   bool complete=false;var root=UICraft.Root(canvasRoot,"道具解锁介绍 "+k);alphabetIntroduction=root.gameObject.AddComponent<AlphabetIntroductionView>();alphabetIntroduction.Begin(this,BoosterIntroduction.Copy(k),()=>{Profile.MarkBoosterIntro(k);complete=true;});while(!complete&&generation==viewGeneration&&alphabetIntroduction!=null)yield return null;if(generation!=viewGeneration)yield break;alphabetIntroduction=null;
  }
  for(int i=1;i<=3;i++){var k=(StreakBooster)i;if(Profile.BoosterUnlocked(k,currentLevel)&&!Profile.BoosterGuideSeen(k)&&Session.StreakStock(k)>0){pendingBoosterGuide=k;break;}}
 }
```

## 对象层级和素材入口

- L12：`UICraft.Root(canvasRoot,"道具解锁介绍 "+k)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
StreakBooster pendingBoosterGuide;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
