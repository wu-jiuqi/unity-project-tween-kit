# unity-project-tween-kit

从《喵弹拆箱 / BounceBusters》当前主工程提取的Unity动画档案、复用组件和Windows交互展馆。

主要用途是在后续Unity项目中复用这些补间动画的设计：从交互需求选方案，保留节奏、曲线与反馈规则，换用新项目的视觉对象并继续调参。[新项目设计选型与迁移](references/design-reuse.md)。

0.2.0更新了展馆界面与操作流程：[操作说明](references/gallery-ui.md)。采用原生UGUI布局，区分直接操作、主动作触发和自动播放，提供中文精确调参、独立来源档案及放大舞台。

**当前为阶段预览，尚未完成用户要求的全量覆盖验收。** 已提供19个可体验条目，以及全工程来源扫描、原代码快照、逐文件差异和资源/GUID映射；未提取、未确认入口和物理/业务混合项均显式列出，不把扫描候选包装成已完成组件。

- [Skill入口](SKILL.md)
- [来源目录与覆盖进度](references/catalog.md)
- [首次导入与逐项接线](references/integration.md)
- [Windows展馆操作流程](references/gallery-ui.md)
- [暂停、中断、重入与清理](references/lifecycle.md)
- [提取差异](references/adaptations.md) · [授权证据](references/licenses.md)
- [验证与重建](references/validation.md) · [未完成验收项](references/acceptance.json)

展馆覆盖按压拖拽、设置开关、C版弹窗、当前收集物贝塞尔吸入、结算分段滚分、Logo入场与逐字/波浪文字、加载、漂浮、两种弹簧交互、方向受击、目标开场、材质充星、胜败猫序列帧、胜败结算页和联赛奖章。交互型条目等待真实操作；重播、即时完成和采样不连接游戏存档、奖励、广告或导航。

维护组件的唯一位置是`assets/unity-kit`。展馆工程在`assets/sample-project`；Git克隆后运行`python scripts/materialize.py`，无需原游戏工程，即可打开`Assets/Scenes/Gallery.unity`。独立示例ZIP已包含所需组件，可直接打开。Unity构建入口`KitBuild.Build`同时导出Unity导入包与Windows程序。生成的`Assets/ProjectTweenKit`不单独维护。

本机构建产物位于仓库`dist`：完整Skill ZIP、`.unitypackage`、Windows ZIP和独立示例工程ZIP。`release-manifest.json`记录包与被测程序哈希；验证记录在[evidence](evidence)。源项目使用的Unity版本和本次实际测试版本以报告为准。
