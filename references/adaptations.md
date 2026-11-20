# 提取差异

完整逐文件diff与前后SHA见`extraction-diffs.json`。原始`.cs.txt`保留在`references/source`，不参与Unity编译。

| 交付部分 | 来源 | 必要改动和差异 |
|---|---|---|
| 原类提取 | UIBounce等27个源文件/片段 | 命名空间改ProjectTweenKit；AppUI门控改宿主委托；Time转KitClock；DOTween可选Manual更新；图集、曲线和默认常量保留 |
| UIBounce | UI/UIBounce.cs | 把已使用的五段时长和视觉倍率开放为字段；暂停展馆时不把当前姿态重置 |
| SettingsSwitch77 | UI/SettingsSwitch77.cs | 滑动时长开放；禁用时对齐宿主当前值，消除滑动中断半格；Refresh不写业务 |
| ModalMotion | LayoutSettingsSheetC + CloseModal | 外部先完成安全区和父级比例拟合；组件只做拟合后scale×.96→原向量/alpha入场，关闭scale×.94/alpha；不销毁页面、不恢复GameSession |
| RewardFlight | CollectionFlightMotion.Apply | 0.6秒SmoothStep贝塞尔及按物种缩放/摇摆保持；用Target/Destination替代ChainLoot和会话；世界到UI的单位变换通过UnitsPerWorldUnit给定；无收集回调 |
| NumberRoll | VictoryFinale.Advance/Ease | 提取每个星光到达后的0.2秒分数段，保留double累加和Math.Round规则；宿主给From/To与触发时机；没有提取原投弹权重分配或全结算调度 |
| JellyHit | JellyMotion.HitStretch | 原方向受击方程；保留Target初始scale；把时长与倍率显式公开；不接伤害或物理 |
| FrameSequence | Victory/FailureCatAnimation | 原16帧及非均匀FrameAt；加可控Play/Sample/Seek/Stop接口；不重排、不均匀化帧时长 |
| UIFloat | UICraft.UIFloat | 正弦表达式不变；增加禁用归位及StopAndRestore；原类没有显式还原 |
| JellyLogoIntro | JellyLogoIntro/JellyGlyph | 重入先移除自己创建的字层/文案；取消旧序列/协程；原实时逐帧等待改成可暂停KitWait，保留1/30秒每姿态 |
| LoadingPresentation | LoadingPresentation | GameContent.Status改可选ContentStatus；不接加载业务；加入显式停止和归位 |
| 胜/败页面 | Victory/FailurePresentation | 原时序、帧、布局与子组件；按钮由宿主传入；展馆仅用本地提示回调 |
| 音频宿主 | SoundService调用点 / ResultAudioTimeline | 原节点与选定音频原件；使用窄AudioSource播放适配器；不带混音存档、限流分组随机、语音优先级或BGM；采样静音 |
| UICraft | 原Root/Shape/Box/Label/OutBack | 保留源构造方程；Button改复用当前CSettingsArt；移除无关游戏工厂；只带演示实际引用的Pastel资源；完整原catalog作尺寸依据 |
| 铜钱图集 | OpeningCopperArt.Clip/Frame/State | 保留原帧矩形、缩放、PPU和状态索引；剥离GameSession与世界SpriteRenderer工厂；展馆UI宿主显示原透明帧，原世界材质由接收者保留 |
| DOTween | 源项目1.3.045核心DLL | 核心DLL与本机原始1.3.045发行包SHA一致；项目改过的ModuleUtils不再分发，换成供应方未修改的Utils/Physics/Physics2D源码并附原readme。这些模块只提供依赖；展馆不调用物理补间 |

钱包`CountWallet`、金币`FlyCoins/FlyCoin`、烟花`Fireworks`等在当前源码中没有确认到正常页面入口，留在旧逻辑证据中。默认交付的奖励/数值示例来自当前收集与结算，不使用这些旧方法。

展馆的开关、奖励、结算按钮只更改本地演示状态；真实存档、计分、排名与导航继续由接收宿主管理。展馆不是完整游戏的替代实现。


## 0.3.0 补入设计

| 组件 | 原实现 | 提取边界与差异 |
|---|---|---|
| ScoreImpact | BattleHUDSummary.Update / ShowVictoryScore | 保留.25秒正弦、.2放大及金色插值；目标和时间适配，原色/比例从绑定对象捕获；增加精确归位 |
| ScoreGain | ScoreGainPresentation.Changed / Update | 保留.07秒重新计时合并、.65秒上移32及alpha/scale原式；StateChanged改显式Add；移除GameSession，禁用清空待播值 |
| TaskArrival | ChainTaskCard.Update | 保留卡片入场、到达脉冲、底垫衰减和盖章；拆除目标读取，宿主传完成状态；距离/比例相对宿主初始值 |
| MovesFeedback | ChainCollectionHUD.Update | 只提取步数视觉入场/挤压，保留.5/.3秒与.025；不带目标卡工厂、低步数声音或会话 |
| CheerBurst | ChainRatingView.Update | 保留评级字图、13星分布和1.8秒节奏；评级序号检测改显式Play；不带评级计算和声音 |
| LeagueJelly | LeagueRankingView.LeagueJelly | 原类提取，Time换KitClock；原duration/延迟/归位规则不变。增加首次Play前等待，避免脱离原工厂立即Play的假设时把未捕获的初始比例写成零 |
| ThemeDissolve | ChapterHomeView.Transition | 只提取.8秒SmoothStep透明度；背景创建/排序/回收及换章由宿主完成，增加采样和停止 |

新设计默认值和原方程不以展馆放大显示比例代替。PresetIO额外支持Color RGBA（同schema扩展字段w，旧数值预设仍可读取）；对象引用不写进参数文件。所有新增原素材与导入设置纳入resource-map。
