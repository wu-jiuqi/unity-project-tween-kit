# Loading进度接线

`LoadingProgress`来自项目微信启动层`Tools/Platform/WeChat/StartupLoadingView86.cs`的进度表现。旧主工程LoadingPresentation只提供猫咪与文字波浪，因此新增两个条目分别展示进度驱动与完整启动构图。完整页和独立条共用同一个进度实现。

在Canvas下创建根对象：

```csharp
var progress = ProjectTweenKit.LoadingProgress.Create(parent);
progress.SetProgress(0.25f);  // 宿主已完成一批必需任务
progress.SetProgress(0.65f);  // 保持单调；表现平滑追赶
// 所有必需任务和首个可用画面真正完成后：
progress.Complete();
// 等待progress.IsComplete再交接页面，可保留原0.5秒完成观察。
```

也可导入`Examples/LoadingProgress.prefab`或`StartupLoading.prefab`。示例按钮明确标为模拟任务，不会请求网络或推进原游戏。完整启动页通过`StartupLoadingPresentation.Build()`创建，`Progress`返回同一套可调组件。

- `FillSpeed=.32`：每秒最多推进32个百分点；`MaxFrameDelta=.05`：卡顿单帧最多推进1.6个百分点。
- `SetProgress`不回退且输入上限.99，必须`Complete()`才允许到1；百分比、填充和弹珠共用显示进度。
- 完成时保留.42秒/.045脉冲、弹珠抬升14单位和.48秒六星爆发。
- `Failure()`暂停进度推进，保留当前显示与目标；`Resume()`继续。它们不重新执行网络任务。
- `ResetProgress()`/`StopAndRestore()`开始新的0%表现；禁用清空旧轮状态。重入和页面切换前按宿主生命周期管理。
- `TravelWidth=636`为原设计行程；适配宽度时同时调整轨道/填充尺寸与布局，不能只改变图片比例。

完整启动页保留已有猫五姿态、字标、浮岛、箱子和轮播文案。安全区适配交给宿主，示例按自身RectTransform尺寸等比适配。原始图来自同项目已导出的StartupLoading资源目录，PNG和meta逐项映射；没有使用参考仓库美术。

维护证据见[supplemental-deliveries.json](supplemental-deliveries.json)，任务宿主HomeContentStartup的源码快照仅用于确认SetProgress/Complete的真实调用顺序，不随运行组件引入微信SDK或业务系统。
