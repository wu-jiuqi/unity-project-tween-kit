---
name: unity-project-tween-kit
description: 为新的Unity项目复用补间动画设计，按交互需求选择效果、导出当前参数、替换宿主素材并接入同源组件；覆盖按压、弹窗、收集、计分、Loading进度、文字与入场编排。
---

# Unity 项目动画工具包

用于后续项目复用动画设计与组件：先匹配交互目的，再保留合适的节奏、曲线、对象分层和中断规则，绑定宿主对象并调参。当前44个展馆条目使用同一份交付代码。

- **选型**：先读[设计选型与迁移](references/design-reuse.md)，按需读[参数基准](references/parameters.md)、[接入](references/integration.md)与[生命周期](references/lifecycle.md)。沿用接收项目的视觉规范；原素材提供可运行基准。
- **接入新项目**：检查Unity/UGUI、输入系统与已有DOTween，避免重复导入同名库。按需导入包内组件、依赖和示例，绑定宿主视觉子层；奖励、导航、任务与存档仍由宿主管理。
- **调参与导出**：展馆“参数显示”只列当前值、单位和固定曲线基准。“一键复制参数导出”同时复制JSON并保存到参数目录；JSON兼容`MotionPreset.Load(path).Apply(component)`、`PresetLoader`和Editor参数工具。可调值在`settings`，固定参数说明不覆盖它们；对象引用由宿主绑定。观察速度不写成组件参数。
- **Loading**：读[Loading接线](references/loading-progress.md)。`SetProgress`输入单调任务进度，完成信号前最多显示99%；所有必需任务与可用首帧完成后才调用`Complete`。中断保持进度，恢复不重复提交任务。不用计时器伪造加载进度。
- **验证**：实际运行宿主接线，覆盖连续操作、暂停恢复、关闭和销毁；说明已验证行为。展馆重播不执行宿主业务。改算法或时序时记录与基准的差异。
- **维护**：`assets/unity-kit`是唯一组件源；克隆后用`materialize.py`准备示例。来源复查读[覆盖记录](references/coverage-audit.md)；96项参考的适用/排除/待整理见[取舍表](references/reference96-comparison.md)。维护指纹与构建流程见[验证文档](references/validation.md)。来源证据供内部核对，不是展馆界面。

保留图片透明度、切片、九宫格、尺寸和帧时长。项目素材优先；确需补图时沿用宿主已确认的视觉规则。素材来源与许可见[记录](references/licenses.md)。未提取的候选不能称为可交付组件。

## 0.5.0-preview · 游戏0.5.1同步

新增轻提示、共享箱体Loading、异常弹窗、五页资源门控切换、旋转遮罩充星、目标庆祝、低步数警告及七页三步教学；同步新版八帧结算猫与C版结果页。共44个展馆条目，参数导出、独立预制体与生命周期验证同步更新。旧版示例保留为兼容参考。

[新版接线与提取边界](references/current-051.md) · [当前来源映射](references/current-051-deliveries.json) · [验收记录](evidence/VERIFICATION.md)
