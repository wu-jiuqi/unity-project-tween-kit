# 扫描边界与排除项

主工程：`BounceGarden/BounceGarden`；启动方式：Main场景加载后`Bootstrap.Boot`动态建立`AppUI`及绝大部分UI对象。仅查`.unity`或prefab引用不能找到全部调用与资源。

| 范围 | 处理 |
|---|---|
| 主工程Assets/Scripts | 全文件指纹和源码快照；Roslyn语法/部分语义解析，记录调用、创建、常量、对象、曲线和资源入口 |
| 主工程Assets/Editor | 编辑器工具或禁用预处理分支单列，不当作玩家动画 |
| QA、Preview、Demo、Audit、Research、Survey、Recorder、LevelEditor | 保留证据，归入测试/预览，不进入“当前在用”结论 |
| AndroidBuildProject、Builds下微信/Windows副本 | 构建/平台副本，未与主工程混计家族；交付组件不从这些副本提取 |
| ValidationProject、PastelValidationProject、GameplayValidationProject | 验证夹具工程，不作为当前运行入口 |
| Backups、Archives、ArtSource旧交付 | 历史来源；默认不取未证实当前使用的美术 |
| Rigidbody、碰撞、移动障碍、业务计数/计时 | 业务与物理单列；同类内可能夹带视觉，需逐方法拆分，不能整体当补间 |
| 原GameSession与AppUI混合方法 | 保存全原式和调用上下文；只按已声明子集提取，未交付部分保持待处理 |

当前普通页已停用的旧Round6Nav，以及没有确认到当前入口的钱包滚动、金币池飞行、烟花等，保留在档案中。没有删除或改写主工程及其已有工作树修改。

扫描覆盖DOTween、协程、自定义Update/LateUpdate、数学插值/正弦/衰减、弹簧、网格与材质更新，以及FrameAt序列帧时间表。它仍是静态候选发现与局部语义核对，不能代替所有运行时动态分支的证明。
