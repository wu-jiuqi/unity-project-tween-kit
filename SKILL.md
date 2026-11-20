---
name: unity-project-tween-kit
description: 从喵弹拆箱真实工程定位、体验和复用已归档的Unity补间与动画，使用组件、项目素材、Windows展馆及宿主适配说明；也用于更新来源指纹和交付覆盖记录。
---

# Unity 项目动画工具包

先读 [当前目录与覆盖状态](references/catalog.md)。本包提供可运行的提取组件及原素材示例，同时保留尚未提取或未确认入口的来源证据；不要把候选数量当成当前动画总数，也不要把档案页当成已交付组件。

- **查实现或实际调用**：按目录进入单页；查 `source-map.json` 的方法、SHA-256、`resolvedCallSites`、构造位置、时间源、曲线、资源入口和生命周期。`nameMatchedCallSites` 仅为检索线索；无法解析的委托、动态资源及条件分支需要继续核对。
- **选择复用形式并接入**：读 [接入说明](references/integration.md)。独立组件挂载绑定；组合示例保留原资源；业务编排按明确的宿主合同对接。先确认 [生命周期](references/lifecycle.md)，再绑定目标与事件。
- **体验与调参**：运行 Windows 展馆；交互条目等待真实操作。保存的 JSON 对应同一个交付组件，可通过 `MotionPreset.Apply`、`PresetLoader` 或 Unity 的 `Tween Kit/Component parameters` 载入。重播与采样只改变表现。
- **更新来源**：运行 `scripts/inventory.py --source <主Unity工程>`，再运行 `scripts/catalog.py`；读取 [核对与发布流程](references/validation.md)。扫描会重置审阅状态，需重新核对变动的方法与实际入口，不能直接沿用旧结论。
- **验证与打包**：以 `assets/unity-kit` 为唯一组件源。新克隆用`materialize.py`物化组件，无需原游戏；维护者更新来源时用`prepare_project.py --source ...`。Unity `KitBuild.Build` 导出同一批组件。运行来源/GUID/同源核对、玩家运行验证及独立导入，再用 `package.py` 生成四类交付和逐文件哈希。

保持原曲线、节奏及交互语义；算法或生命周期变化必须进入差异记录。不要把原项目业务代码或实验代码放进默认动画运行时。素材遵守 [来源与授权记录](references/licenses.md)，保留原图、导入meta、运行时切片数据与帧时序。
