---
name: unity-project-tween-kit
description: 为新的Unity项目复用补间动画设计，按交互需求选择效果、复制动画Prompt与关键参数、替换宿主素材并接入可运行组件；覆盖按压、弹窗、收集、计分、文字与入场编排。
---

# Unity 项目动画工具包

主要用于后续项目复用动画设计：根据新宿主的交互目的，选择触发方式、节奏、曲线、对象分层和中断规则，再接入同一套组件并替换宿主素材。原项目与展馆提供可运行的设计基准。

做新项目时先读 [设计选型与迁移](references/design-reuse.md)，再读取选中方案的 [动画Prompt与参数写法](references/animation-prompts.md)、接入行和生命周期。展馆只展示效果、参数与迁移Prompt。

- **给新项目生成实施Prompt**：选择对应效果，说明视觉目标、真实触发、曲线与关键参数、中断重入行为。展馆的“复制Prompt与当前参数”可带出实调值；按宿主上下文具体化对象绑定，沿用宿主视觉风格。不要求接收者定位原项目源码。
- **选择复用形式并接入**：读 [接入说明](references/integration.md)。独立组件挂载绑定；组合示例保留原资源；业务编排按明确的宿主合同对接。先确认 [生命周期](references/lifecycle.md)，再绑定目标与事件。
- **应用到新项目**：先检查宿主已有组件、视觉规范、坐标系与输入方式；按设计选型表匹配需求。保留一份原参数基准，再在宿主对象上绑定和调参，记录有意改变的节奏或交互语义。交付实际接线及暂停、连续操作、关闭后的验证结果。
- **体验与调参**：运行 Windows 展馆；交互条目等待真实操作。保存的 JSON 对应同一个交付组件，可通过 `MotionPreset.Apply`、`PresetLoader` 或 Unity 的 `Tween Kit/Component parameters` 载入。重播与采样只改变表现。
- **维护时复查遗漏**：运行 `scripts/inventory.py --source <主Unity工程>`、`scripts/catalog.py` 和 `scripts/audit_coverage.py --source <主Unity工程>`；读 [复查记录](references/coverage-audit.md) 与 [核对流程](references/validation.md)。内部指纹和调用链用于维护核对。未提取候选不能称为可交付组件。
- **验证与打包**：以 `assets/unity-kit` 为唯一组件源。新克隆用`materialize.py`物化组件，无需原游戏；维护者更新来源时用`prepare_project.py --source ...`。Unity `KitBuild.Build` 导出同一批组件。运行来源/GUID/同源核对、玩家运行验证及独立导入，再用 `package.py` 生成四类交付和逐文件哈希。

保持原曲线、节奏及交互语义；算法或生命周期变化必须进入差异记录。不要把原项目业务代码或实验代码放进默认动画运行时。素材遵守 [来源与授权记录](references/licenses.md)，保留原图、导入meta、运行时切片数据与帧时序。
