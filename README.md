# unity-project-tween-kit

从《喵弹拆箱 / BounceBusters》当前主工程提取的Unity动画档案、复用组件和Windows交互展馆。

**当前：0.5.0-preview，来源游戏0.5.1，共44项展馆示例。** 新增轻提示、共享Loading与异常弹窗、五页切换、原生旋转充星、目标庆祝、低步数警告和七页教学；更新八帧结算猫与C版结果页。[本轮接线与范围](references/current-051.md) · [本轮验收](evidence/VERIFICATION.md)。下文0.4.0条目为兼容功能历史。

主要用途是在后续Unity项目中复用这些补间动画的设计：从交互需求选方案，保留节奏、曲线与反馈规则，换用新项目的视觉对象并继续调参。[新项目设计选型与迁移](references/design-reuse.md)。

0.4.0将右侧改为“参数显示”，支持“一键复制参数导出”：复制当前JSON并保存文件，可用同一套MotionPreset载入。对照参考仓库96项后新增10个适用条目，累计36项；包括独立Loading进度条、完整微信启动页、页面横移、分组入场、排行重排、教学手势、提示淡出、欢迎语分层、挂件摆动与徽章呼吸。全部使用项目现有素材。

[96项逐条对照与取舍](references/reference96-comparison.md) · [Loading接线](references/loading-progress.md) · [参数基准](references/parameters.md)

**当前为阶段预览，尚未完成全量覆盖验收。** 已提供44个可体验条目，以及内部来源扫描、差异和资源/GUID映射；未提取、未确认入口和物理/业务混合项均显式列出。

- [Skill入口](SKILL.md)
- [参数显示与JSON导出](references/parameters.md)
- [来源目录与覆盖进度](references/catalog.md)
- [首次导入与逐项接线](references/integration.md)
- [Windows展馆操作流程](references/gallery-ui.md)
- [暂停、中断、重入与清理](references/lifecycle.md)
- [提取差异](references/adaptations.md) · [授权证据](references/licenses.md)
- [验证与重建](references/validation.md) · [未完成验收项](references/acceptance.json)

展馆覆盖按压拖拽、设置开关、C版弹窗、当前收集物贝塞尔吸入、结算分段滚分、Logo入场与逐字/波浪文字、加载、漂浮、两种弹簧交互、方向受击、目标开场、材质充星、胜败猫序列帧、胜败结算页和联赛奖章。交互型条目等待真实操作；重播、即时完成和采样不连接游戏存档、奖励、广告或导航。

维护组件的唯一位置是`assets/unity-kit`。展馆工程在`assets/sample-project`；Git克隆后运行`python scripts/materialize.py`，无需原游戏工程，即可打开`Assets/Scenes/Gallery.unity`。独立示例ZIP已包含所需组件，可直接打开。Unity构建入口`KitBuild.Build`同时导出Unity导入包与Windows程序。生成的`Assets/ProjectTweenKit`不单独维护。

本机构建产物位于仓库`dist`：完整Skill ZIP、`.unitypackage`、Windows ZIP和独立示例工程ZIP。`release-manifest.json`记录包与被测程序哈希；验证记录在[evidence](evidence)。源项目使用的Unity版本和本次实际测试版本以报告为准。

## 0.5.0-preview · 游戏0.5.1同步

新增轻提示、共享箱体Loading、异常弹窗、五页资源门控切换、旋转遮罩充星、目标庆祝、低步数警告及七页三步教学；同步新版八帧结算猫与C版结果页。共44个展馆条目，参数导出、独立预制体与生命周期验证同步更新。旧版示例保留为兼容参考。

[新版接线与提取边界](references/current-051.md) · [当前来源映射](references/current-051-deliveries.json) · [验收记录](evidence/VERIFICATION.md)
