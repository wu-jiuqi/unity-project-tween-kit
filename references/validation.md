# 来源核对、构建和验证

以下命令在仓库根运行；Python3无需额外依赖。Unity路径可改为本机编辑器。

```powershell
python scripts/inventory.py --source <主Unity工程> --unity <Unity的Editor目录>
python scripts/catalog.py
python scripts/audit_coverage.py --source <主Unity工程>
python scripts/extract.py --source <主Unity工程> --dotween-standard <原版DOTween1.3.045目录>
python scripts/prepare_project.py --source <主Unity工程>
<Unity.exe> -batchmode -nographics -quit -projectPath <仓库>/assets/sample-project -executeMethod KitBuild.Build -logFile <仓库>/work/build.log
python scripts/finish_build.py
python scripts/run_player.py
python scripts/clean_import.py --unity <Unity.exe>
python scripts/validate.py --source <主Unity工程>
python scripts/package.py
```

`extract.py`后续可省略dotween-standard，使用包内已保留的原版模块/readme。不会把源项目改过的模块重新覆盖进去。修改任何运行代码后，重新prepare/build/finish并验证相应行为。

## 可核对的证据

- `source-fingerprints.json`与源码快照：包括未命中动画模式的源码，检测工作树漂移。
- `source-map.json`：类/文件分组、精确方法指纹、Roslyn解析调用、未解析调用、Unity回调、构造位置、层级、字段、曲线、资源入口及禁用预处理分支。
- `extraction-diffs.json`、`adaptations.md`：原实现到交付实现的差异；未完成的提取不伪装成已交付配方。
- `resource-map.json`：逐项资源来源、目标与哈希；原meta保持GUID、平台导入配置、切片与九宫格。
- `materialization.json`：展馆/独立工程中的组件与唯一源逐文件一致。
- `evidence/build-result.json`：Unity真实构建结果；`player-run.json`绑定被测程序文件集、运行记录与退出码。
- `evidence/runtime-verification.json`：自动派发指针、暂停、关闭重入、归位、参数来回载入和即时/动画结果断言。它不替代所有真实操作与原游戏场景实测。
- `evidence/clean-import-result.json`：另一个空工程只导入unitypackage；编译、实例化示例预制体和缺脚本核对。验证工程不从展馆Library读取程序集。
- `evidence/visual/`：只收录实际可见窗口的截图。隐藏窗口黑图排除，不能宣称视觉验收通过。
- `dist/release-manifest.json`：Skill、Unity包、Windows包、独立工程ZIP与内容文件指纹；分发前可重新计算对照。

## 全量验收与阶段交付

`validate.py`默认检验已构建交付是否自洽，不将尚未核对的候选强行判成通过。`--require-full`还要求`acceptance.json`的全量验收通过；未完成时退出失败。`package.py`当前只产出标记为preview的阶段包，不会将它登记为完成全部动画的正式版。

还需要逐个处理`requires-route-review`、混合物理/视觉与未解析的动态调用，核对当前45关、页面与条件入口，再扩大交付与展馆。物理模拟、业务计时、编辑器/QA/预览与已停用旧导航分别登记。完成范围最终取自经复核的实际清单。
