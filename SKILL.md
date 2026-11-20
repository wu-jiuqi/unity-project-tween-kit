---
name: unity-project-tween-kit
description: 为新的Unity项目选择、复用和调整已归档的补间动画设计，覆盖按压、开关、弹窗、收集飞行、文字与入场编排；提供可运行组件、调参展馆及来源证据。
---

# Unity 项目动画工具包

主要用于后续项目复用动画设计：根据新宿主的交互目的，选择触发方式、节奏、曲线、对象分层和中断规则，再接入同一套组件并替换宿主素材。原项目与展馆提供可运行的设计基准。

做新项目时先读 [设计选型与迁移](references/design-reuse.md)，只读取选中方案的接入行、生命周期与来源页。维护归档时读 [当前目录与覆盖状态](references/catalog.md)。不要把候选数量当成当前动画总数，也不要把档案页当成已交付组件。

- **查实现或实际调用**：按目录进入单页；查 `source-map.json` 的方法、SHA-256、`resolvedCallSites`、构造位置、时间源、曲线、资源入口和生命周期。`nameMatchedCallSites` 仅为检索线索；无法解析的委托、动态资源及条件分支需要继续核对。
- **选择复用形式并接入**：读 [接入说明](references/integration.md)。独立组件挂载绑定；组合示例保留原资源；业务编排按明确的宿主合同对接。先确认 [生命周期](references/lifecycle.md)，再绑定目标与事件。
- **应用到新项目**：先检查宿主已有组件、视觉规范、坐标系与输入方式；按设计选型表匹配需求。保留一份原参数基准，再在宿主对象上绑定和调参，记录有意改变的节奏或交互语义。交付实际接线及暂停、连续操作、关闭后的验证结果。
- **体验与调参**：运行 Windows 展馆；交互条目等待真实操作。保存的 JSON 对应同一个交付组件，可通过 `MotionPreset.Apply`、`PresetLoader` 或 Unity 的 `Tween Kit/Component parameters` 载入。重播与采样只改变表现。
- **更新来源**：运行 `scripts/inventory.py --source <主Unity工程>`，再运行 `scripts/catalog.py`；读取 [核对与发布流程](references/validation.md)。扫描会重置审阅状态，需重新核对变动的方法与实际入口，不能直接沿用旧结论。
- **验证与打包**：以 `assets/unity-kit` 为唯一组件源。新克隆用`materialize.py`物化组件，无需原游戏；维护者更新来源时用`prepare_project.py --source ...`。Unity `KitBuild.Build` 导出同一批组件。运行来源/GUID/同源核对、玩家运行验证及独立导入，再用 `package.py` 生成四类交付和逐文件哈希。

保持原曲线、节奏及交互语义；算法或生命周期变化必须进入差异记录。不要把原项目业务代码或实验代码放进默认动画运行时。素材遵守 [来源与授权记录](references/licenses.md)，保留原图、导入meta、运行时切片数据与帧时序。
