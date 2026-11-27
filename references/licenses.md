# 来源与授权记录

资源逐项路径、GUID、SHA、打包原因见`resource-map.json`；全工程资源候选另见`resource-inventory.json`。没有新增美术。PNG、音频、字体、Shader与原meta直接复制，运行时图集/九宫格/尺寸数据也保留原件。

| 范围 | 来源与随包证据 | 许可记录 |
|---|---|---|
| C设置图集、四字Logo、猫帧、结算光环、奖章、铜钱等 | 当前BounceBusters主工程Assets/Resources/Art；映射记录原路径/指纹 | 项目现有素材；源项目未对全部美术附单独公共许可，保留为“项目素材，公共再分发许可未单独确认”，不把它们标成MIT或CC0 |
| 猫啃珠圆体 | 原MaokenZhuyuanTi.ttf；同目录字体授权说明、OFL/OFL_ZHS原文 | 按随附OFL及字体说明；名称与文件保持 |
| MiaoRoundedUI-Bold缺字回退 | 原Fonts文件；StreamingAssets/FontLicenses/ResourceHanRounded-OFL.txt | 保留源项目关联的字体许可证据；衍生命名链尚需作者逐项确认 |
| 旧气泡/界面音源授权证据 | StreamingAssets/AudioLicenses里的来源表、manifest、CC0原文和作者页面快照 | CC0声明只覆盖其明确列出的原录音；不能将它套用于整个音频目录 |
| 当前选定AI/DeliveryV68/DeliveryV87音频 | 保留原WAV/OGG与meta；sound-map.json记录SoundService最终解析到的路径 | 属项目已有交付；各交付录音的独立再分发许可并未由旧CC0清单统一证明，保持“项目交付，许可链待逐项确认” |
| DOTween 1.3.045 | 原核心DLL；原版readme；未修改的Modules三文件 | [供应方DOTween Artistic License](https://dotween.demigiant.com/license.php)，核对日期2026-11-20；不是MIT，也没有打包DOTween Pro |
| UGUI/TMP essentials | 源项目TextMesh Pro资源和Shader；ThirdPartyLicenses/Unity-UGUI.md | 按随包Unity包许可；本工具包用于Unity项目，不扩大到其他引擎 |

DOTween官网列出对原版与修改版的不同分发条件。因此不分发源项目加过defines的ModuleUtils，保留同版本核心DLL，并使用本地原始发行包的模块和原readme。具体文件哈希在资源映射中，不以Github当前HEAD的不同DLL替换源项目运行时。

本工具包尚未为项目原有代码、美术或音频新增一份覆盖全部内容的公共许可证。最终公开发布验收中，应逐项确认项目素材与交付录音的再分发范围。此处是实际证据状态，不能将“运行示例可加载”写成“所有素材已取得统一许可”。


0.4.0新增示例继续使用项目已有StartupLoading、OnboardingV1、TutorialV41、BattleUIV42和藤蔓挂件素材。启动页PNG及meta来自同一BounceBusters项目已导出的微信工程资源，沿用原GUID和导入设置，映射中的source以../../Builds开头；不将其冒充主Assets文件。没有导入unity-ui-tween仓库美术，也没有新生成图。许可判断继续沿用上面的项目素材记录。


## 游戏0.5.1同步

新版接口、时间驱动、共享Loading、五页资源等待、八帧结果页、原生遮罩充星及新增清理合同见[当前补充说明](current-051.md)。旧章节的3.6秒加载猫与旧材质充星说明属于0.4.0历史实现；当前charge条目和结果页以本轮代码为准，独立旧LoadingPresentation仍保留。
