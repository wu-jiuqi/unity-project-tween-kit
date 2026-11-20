# 接入与依赖

为新项目选择动画时，先读[设计选型与迁移](design-reuse.md)。下表负责具体接线；原素材是运行基准，宿主可以保留交互设计并替换自己的视觉资源。

适用 Unity 6（源项目声明6000.0.26f1，验证以记录中的实际编辑器版本为准）。默认需要 UGUI 2.0.0（含TMP）、Audio、JSON、ImageConversion、UI；附带的原版 DOTween 模块还需要 Unity Physics / Physics2D。没有接入 GameSession、AppUI、LocalProfile、支付、广告或网络。

## 首次使用，目标约5分钟

1. 干净 Unity 工程安装 UGUI。导入 `ProjectTweenKit.unitypackage`；新工程保留默认内置模块。
2. 建立 Canvas，添加 CanvasScaler（参考尺寸1080×2340）与 GraphicRaycaster；场景需要 EventSystem / StandaloneInputModule，并启用旧 Input Manager 或 Both。
3. 将 `Assets/ProjectTweenKit/Examples/Press.prefab` 拖入 Canvas，运行。实际点击、拖拽与回弹直接调用交付 `UIBounce`。
4. `ExamplePrefab` 在运行时重建原项目的 Sprite.Create 图集精灵和字体，避免把临时Sprite/动态字体错误地序列化成丢失引用。`ExamplePrefab.Component` 返回实际组件；点击事件与开关值可从预制体的 UnityEvent 绑定宿主。
5. 换图时保留固定命中根与视觉子层；参数在实际组件上修改。用 `Tween Kit/Component parameters` 保存/载入，或给 `PresetLoader.Target` 和 JSON `TextAsset` 赋值。

这是首次接入目标，未把自动导入耗时等同于开发者手工5分钟体验测试。独立验证报告明确列出实际执行内容。

如果从Git仓库体验完整展馆，运行`python scripts/materialize.py`后，用Unity打开`assets/sample-project`并打开`Assets/Scenes/Gallery.unity`。这个过程无需访问原游戏工程。独立示例ZIP已物化组件，直接打开即可；它在独立目录构建时把输出放在自己的dist中。

## 每个可体验条目的绑定与调用

| 条目 | 导入/绑定 | 触发 | 可调参数与单位 | 停止/清理 |
|---|---|---|---|---|
| 按压 | `UIBounce` 挂固定Button根；`Target`指向ButtonVisual | Unity指针事件 | Hover/Exit/Press/Release/Idle Duration（秒）、Magnitude（倍率）、IdleInterval（秒） | 禁用或失焦自动归位；拖拽取消点击 |
| 开关 | `SettingsSwitch77.Create(parent,key,x,y,read,write)`，或Switch预制体 | 原Button点击 | SlideDuration（秒） | 结束旧滑动；禁用对齐read值；不要用Refresh提交宿主数据 |
| 弹窗 | `ModalMotion` + CanvasGroup；Target为已拟合的面板 | `Open()` / `Close()`；传true即时完成 | Open/CloseDuration（秒），Open/CloseScale（向量倍率） | `StopAndRestore()`；导航、暂停恢复由宿主监听流程决定 |
| 收集飞行 | `RewardFlight`；Target与Destination为RectTransform | `Play()`，或`Play(true)` | Duration=.6秒、Delay、Kind、UnitsPerWorldUnit、EndScale=.28 | `StopAndRestore()`；宿主独立决定收集计数 |
| 分段滚分 | `NumberRoll.Target`绑定TMP；From/To为宿主算好的单段起止值 | 星光到达时`Play()` | Duration=.2秒、Delay；From/To（分） | `StopAndRestore()`；传true直达同一个To |
| Logo入场 | 新空UI根添加`JellyLogoIntro`；素材取原四字布局 | `Begin()` | 保留原下落/逐字节奏，展馆支持整体播放速度 | `StopAndRestore()`；重入先清理旧字层/引语 |
| 字标波浪 | 父级下面放独立字图，添加`HomeWordmarkWiggle` | 进入启用自动更新 | LoopInterval（秒），CanAnimate宿主门控 | 禁用、失焦还原所有字层原变换 |
| 加载表现 | 新UI根添加`LoadingPresentation` | `Build()`一次；`Play()`可重播；可给ContentStatus委托 | 原五姿势与3.6秒周期；展馆整体速度 | `StopAndRestore()`；此组件不加载关卡 |
| 装饰漂浮 | `UIFloat`挂插画视觉层 | 启用自动运行 | amplitude（本地设计单位）、speed（rad/s）、phase（rad） | `StopAndRestore()`或禁用还原 |
| 欢迎语拖拽 | 固定根上`EncouragementDrag.Initialize(visual,title,detail,allowed)` | 原指针事件 | 原105弹性/6.5阻尼，保留在源码中 | `ResetPose()`；禁用、失焦自动归位 |
| 活动卡弹簧 | `ActivityJellyMotion.Initialize(face,icon,phase,allowed)` | 宿主转发`SetHover/Press/Release` | 原155弹性、10阻尼、四子步 | `ResetPose()`；不附带活动导航业务 |
| 方向受击 | `JellyHit.Target`绑定视觉；不绑碰撞根 | `Play()`，Incoming由宿主给方向 | Duration=.45秒，Amplitude倍率 | `StopAndRestore()`；宿主保留碰撞与受伤逻辑 |
| 目标开场 | 空Overlay根`CollectionGoalEntrance.Begin(realGoalsRect)` | 开局自动；`Finished`供宿主观察 | 原2.35秒编排及目标父级实时坐标换算 | **关闭/销毁Overlay之前先调用Restore()**，归还借入容器 |
| 星星充能 | 挂Image；`Initialize(layer,initial)` | `Charge(value,worldSource)`；宿主每帧`Advance(dt)` | value为0—1；保留0.24秒飞光和0.36秒充能 | `SetImmediate()`归位；销毁清理彗星与独立材质 |
| 猫序列帧 | `FrameSequence.Target`绑定Image，Victory选胜/败 | `Play()` / `Sample(seconds)` | 原非均匀逐帧时长；PlaybackSpeed，Loop | `StopAndRestore()`；不重切原帧 |
| 胜利页面 | 新UI根`VictoryPresentation.Begin(actionsFactory,level,first,coins,medals)` | 宿主已算完业务结果后创建 | 原编排常量；整体观察速度 | 同一个实例只Begin一次；`Finish()`直达末态；重播销毁旧根并新建 |
| 失败页面 | 新UI根`FailurePresentation.Begin(retry,home,watch,canContinue)` | 宿主失败后创建 | 原编排常量；整体观察速度 | 同实例Begin一次；重播销毁旧根；按钮回调由宿主去重 |
| 联赛奖章 | `LeagueMedalPresentation.Show(parent,before,after,medals)` | 宿主给出真实前后名次 | 原入场与父级比例补偿 | 展示根随宿主关闭销毁；不计算排名 |

`RewardFlight` 将原世界单位曲线放在目标父级坐标中：`UnitsPerWorldUnit` 是源世界单位到宿主设计单位的换算，默认1；展馆100。目标位置由世界坐标转入同一个父级。Source中的`.6秒 / SmoothStep / 控制点.22 / 入场脉冲 / 缩小 / 摇摆`均保留。分数组件只提取`VictoryFinale`的单个到达段，完整多弹投掷、计分分配与交接仍属于待提取的宿主编排。

## 本轮新增效果

| 设计 | 绑定与触发 | 关键参数与清理 |
|---|---|---|
| 分数变色放大 | `ScoreImpact.Target`绑定TMP；更新显示分数后`Play()` | Duration=.25秒，Amplitude=.2；Highlight为RGBA色。`StopAndRestore()`恢复原色、原比例。可直接拖入`ScoreImpact.prefab` |
| 加分合并上浮 | `ScoreGain.Text/Target/Group`绑定数字、视觉容器、CanvasGroup；`Add(amount)` | MergeDelay=.07秒，Duration=.65秒，Rise=32；`StopAndRestore()`隐藏并清空待合并量。可拖入`ScoreGain.prefab` |
| 目标到达/印章 | `TaskArrival.Root/Icon/Pad/Stamp/Count`；宿主更新数值后`Arrive(completed)` | .38秒到达、.4秒盖章；停止归位并保留宿主完成状态 |
| 步数挤压 | `MovesFeedback.Target`绑定独立视觉层；数值改变后`Play()` | .5秒入场、.3秒脉冲、.025幅度；停止归位 |
| 评价爆发 | `CheerBurst.Art/Group/Sparks/SparkImages`，最多13个星；设置Tier后`Play()` | Duration=1.8秒，Tier=0—3；禁用/停止隐藏。原字图随包 |
| 排行卡抖动 | `LeagueJelly`挂视觉层；`Play(wait)` | duration=.65秒，wait以秒计；重入、禁用恢复原比例和旋转 |
| 背景换景 | 新背景在下层，旧背景CanvasGroup绑定`ThemeDissolve.Previous`；`Play()` | Duration=.8秒；`StopAndRestore()`显示旧图；回收和导航归宿主 |

这7项均由同一份随包组件驱动展馆。加分、任务进度与剩余步数只是输入；组件不修改业务状态。[对应参数基准](parameters.md)。

## 0.4.0补入的适用效果

| 组件 | 绑定/入口 | 关键值与边界 |
|---|---|---|
| LoadingProgress | `Create(parent)`或绑定填充/弹珠/扫光/星光/数字；`SetProgress / Complete / Failure / Resume` | .32比例/秒，.05秒帧上限；完成前最多99%。[完整接线](loading-progress.md) |
| StartupLoadingPresentation | `Build()`，通过`Progress`操作同一个LoadingProgress | 原竖屏构图，宿主负责安全区和真实任务 |
| PageSlide | Target绑定页面轨道，`Go(destination)` | .27秒OutCubic，从当前位置接续；路由由宿主负责 |
| GroupStagger | Targets绑定视觉层数组，`Play()` | .20秒、.018错峰、.98起始比例；不更改实际布局 |
| RankReorder | Rows绑定稳定身份的行，`Reorder(ranks)`传0起始排列 | .48秒OutCubic，Stride为行距；不计算名次 |
| TutorialGesture | Hand/Ring/Outline/Group，From/To为父级坐标；`SetInputActive(bool)` | 4.4秒周期，.6秒等待、1.3秒位移；不自动瞄准 |
| HintFade | Group绑定提示组，Visible控制显隐 | 透明度每秒变化2；不拦截输入 |
| HomeLayeredFeedback | Title/Detail/Decoration；`Play()` | 副文案延迟.13秒，17/4.7衰减；仅修改视觉层 |
| PendantSwing | Target绑定挂点，`Impulse(direction)` | 30弹性/3.6阻尼/四子步/36度；无物理或伤害 |
| DecorativeBreath | Target绑定徽章，启用自动运行 | .035幅度，3 rad/s；停止归位 |

上述默认值保留项目设计。所有对象引用都需由接收宿主提供；停止/禁用行为见生命周期文档。

## 参数文件与一键导出

展馆文件在 `Application.persistentDataPath/presets/<条目>.json`。参数JSON包含schemaVersion、组件完整类型名及公开数值/布尔/向量/RGBA颜色字段；对象引用由接收工程绑定。载入会拒绝其他类型与未知字段，先验证再整体应用。`PresetLoader`应绑定实际生成后的组件；组合预制体可在`ExamplePrefab.Build()`后调用`MotionPreset.Load(path).Apply(example.Component)`。

## 依赖与差异

`assets/unity-kit`是唯一维护源；示例工程中的`Assets/ProjectTweenKit`由脚本生成，禁止分叉维护。提取了原运行类时，其完整命名空间/门控/时间源适配见`extraction-diffs.json`。新宿主适配器`ModalMotion`、`RewardFlight`、`NumberRoll`、`JellyHit`及`FrameSequence`分别链接上表中的原方法；差异见`adaptations.md`。

复杂页面适配还涉及容器排序、输入门控、generation失效、真实安全区、业务事件去重。不能统一承诺一键接入；待核对项按来源页继续拆分后才能估算工作量。


“一键复制参数导出”会复制并写入当前条目的JSON，文件路径与“保存本项”相同。导出保留schemaVersion=1、component和settings，附带entryId、title、timeSource、referenceParameterNotes；原MotionPreset读取器会忽略额外说明字段，只应用settings。固定参数说明不会覆盖用户调好的字段值。

公开数组中的Unity对象引用、运行状态与展馆观察速度不会导出。固定编排没有公开字段时，settings为空，参考参数只供核对；不能把修改说明文本当成已经修改运行算法。
