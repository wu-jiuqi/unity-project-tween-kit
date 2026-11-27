# 0.5.0 工具包：同步游戏 0.5.1

2026-11-27。工具包版本与游戏版本分别记录：本次工具包为 0.5.0-preview，来源为 BounceBusters 当前 0.5.1 工作树。来源包含未提交修改，因此同时保存完整源码指纹，不仅依赖游戏 Git HEAD。旧版 36 个展馆 ID 保持稳定，新增 8 个，共 44 个。

## 新增与更新

| 展馆 ID | 组件及绑定 | 触发、停止与接入边界 |
|---|---|---|
| toast | ToastMotion.Visual、Group、Label | Show(message)；同文案续时，退出期间只保留最新消息；StopAndRestore/禁用/失焦清空。默认停留3.2秒，宿主可按成功2.2秒、金币不足2.8秒配置，不通过文案推断业务结果。布局和安全区由宿主提供。 |
| loadingbox | LoadingBoxAnimation.Create(parent) | Render(absoluteSeconds)；原落箱.38秒、回弹.24秒、猫与砖块循环4.2秒。AutoPlay可选，默认为false，外部驱动时关闭，避免双重推进。StopAndRestore归零。 |
| errorpopup | ErrorPopupMotion.Build/Open | 使用同一LoadingBoxAnimation；CloseWithIntent接受一次真实按钮意图，关闭完成后转交宿主。禁用、StopAndRestore或再次Open丢弃待交付意图。组件不下载、不自动重试。 |
| pageflow | PageTransition.Pages按真实标签顺序绑定 | Request(index)返回票据；资源就绪后Resolve(ticket,true)，失败传false；Cancel使旧票据失效。只显示出发和目的页、方向按标签顺序、跨标签只滑一屏，动画中禁用页面输入。连续切换先对齐上次逻辑目的页。旧PageSlide保留为简单轨道组件。 |
| nativecharge | StarChargeView.Initialize(host,initial) | SetRotation角度，Charge/Advance/SetImmediate保留。使用原生RectMask2D与星形Mask，136单位方形包含完整旋转对角线。满星隐藏空星底色。旧charge条目也同步此实现，不再依赖StarCharge Shader。 |
| celebration | GoalCelebration.Begin | 保留标题关键帧、12帧角色、光环、弧线下移和2.85秒落位后循环。Sample/StopAndRestore可采样归零；ReducedMotion减少形变。无完成回调、奖励或游戏会话依赖；宿主按Elapsed/Docked控制流程。 |
| lowmoves | MovesFeedback.Target、Label | 普通Play沿用旧步数反馈，Warn启动.48秒×3次整体缩放，红色保持至宿主更新或停止。StopAndRestore/禁用恢复原比例和颜色。宿主判断从大于5变为5并调用400ms震动，不在组件中推断阈值或重复震动。 |
| teaching | ThreeStepLoop.RenderStep(index,phase) | 6.72秒三步循环，RestPhases控制不活动步；示例切换501/505/506/518/511/521/531七页，使用OpeningStory049实际素材、轴心和原各页静态相位，不计分、不写教学进度。 |

胜利/失败整页同步原 C 版结算布局、共享02完整底板、星星原飞入与待机、八帧橘白猫、实际逐帧时长（胜利1.92秒，失败2.32秒）。保留VictoryCatAnimation/FailureCatAnimation及FrameSequence接口。ResultSurface048.AvailableSize由宿主传入安全区域的本地尺寸；示例默认1080×1920。固定布局根，视觉子层承载形变；非等比父级和相机投影补偿仍应由宿主的安全区适配层提供。

## 参数和时间

新条目使用同一个MotionPreset与展馆JSON导出流程，引用和委托不序列化。Toast、切页、教学、庆祝使用KitClock.Delta，步数反馈使用KitClock.ScaledDelta。宿主默认无需手工驱动KitClock；手动模式每帧一次Frame。LoadingBox使用绝对时间采样，ErrorPopup自己推进它的时间。加载进度继续使用LoadingProgress真实输入和99%完成门控，不从装饰时长推导任务完成。

## 素材与来源

[当前切片映射](current-051-deliveries.json)逐条记录当前源码与目标组件哈希及适配差异；[资源映射](resource-map.json)记录原资源字节、GUID与导入设置。FiveBakedV80/sequence-layout.json保留不等距行列、轴心和名义宽度，不把五色相位图简单等分。毛线和角色分别读取原animation-layout/layout，帧时长不重采样。

旧版LoadingPresentation、启动页和其他仍可复用的参考效果保留。来源已移除或字节已变的旧资源明确标记sourceStatus=frozen-0.4.0-reference，按包内哈希核验；它们不再声称与当前主工程一致。历史源码快照只供追溯，当前覆盖数量仅取source-fingerprints/source-map。新素材沿用原项目许可边界；ThirdPartyLicenses和licenses.md仍适用。

## 构建与复核

1. 重新扫描当前源码：`python scripts/inventory.py --source <主Unity工程>`。
2. 旧版适配记录：`python scripts/register_supplemental.py --source <主Unity工程>`；缺失旧来源保留历史记录。
3. 当前表现同步：`python scripts/sync_current051.py --source <主Unity工程>`。手工维护的ToastMotion、PageTransition、ThreeStepLoop、ErrorPopupMotion等保留；精确来源适配在脚本中维护，修改时同时更新脚本。
4. `python scripts/catalog.py`、`python scripts/audit_coverage.py --source <主Unity工程>`、`python scripts/document_parameters.py`、`python scripts/materialize.py`。
5. Unity KitBuild.Build → finish_build.py → run_player.py → clean_import.py → finalize_records.py → validate.py --source <主Unity工程> → package.py。

upgrade_gallery051.py是本次条目注册迁移工具，不是普通使用或日常构建步骤。避免重新运行旧extract.py覆盖新版适配；如需完整基础提取，随后必须运行当前同步并重新核对差异。

自动验收覆盖所有44项参数导出与时钟、Toast续时替换/后台清理、Loading循环、异常按钮取消/去重、五页20种方向组合/等待/取消/失败重试、旋转充星、庆祝重入与落位、三次警告、七页教学全部相位和不等距图集边界，以及新版结算。独立空工程验证导入包与示例预制体；可见窗口另做视觉复查。实际通过数以evidence报告为准，旧报告不能代替本轮结果。

本次范围完成不等于全部原游戏动画已提取。fullAcceptancePassed继续保留false；其余待核对来源分组和跨平台、真人首次接入试验仍单列。
