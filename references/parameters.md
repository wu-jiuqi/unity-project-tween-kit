# 参数基准与导出

展馆“参数显示”以当前组件字段值为准。下面是各项设计基准；固定常量只是说明，不会通过JSON覆盖当前值。导出JSON可用MotionPreset载入，目标对象引用需另行绑定。

## 按钮按压与拖拽 · press

HoverDuration=.16秒，PressDuration=.10秒，ReleaseDuration=.48秒；Magnitude=1为原幅度。IdleInterval=0可关闲置反馈。

时间来源：`KitClock.Delta`。

## 设置开关 · switch

SlideDuration=.16秒，OutCubic；原示例左右位置为±39设计单位，适配轨道宽度。

时间来源：`KitClock.Delta`。

## C 版弹窗开合 · modal

OpenDuration=.18秒，OpenScale=.96；CloseDuration=.14秒，CloseScale=.94。开OutCubic、关InCubic，比例相对绑定对象初始值。

时间来源：`KitClock.Delta`。

## 收集物贝塞尔吸入 · reward

Duration=.6秒，SmoothStep；控制点在位移22%处并向上偏移；EndScale=.28。UnitsPerWorldUnit按宿主坐标转换。

时间来源：`KitClock.Delta`。

## 结算分段数字滚动 · number

From/To为整数；Duration=.2秒；曲线1-(1-t)^3。Delay以秒计；不在组件中计算奖励。

时间来源：`KitClock.Delta`。

## Logo 入场与逐字文案 · logo

逐帧形变使用1/30秒步长；文案每字.035秒。独立字层节奏保留，字数和字图按宿主布局适配。

时间来源：`KitClock.Delta`。

## Logo 逐字波浪 · wordmark

LoopInterval=5.5秒；每字错开.09秒，每次震颤.48秒。保留各字原位置。

时间来源：`KitClock.Delta`。

## 加载页与文字波浪 · loading

猫咪周期3.6秒；保留姿势帧序与文字相位差。此组件不负责真实加载。

时间来源：`KitClock.Delta`。

## 装饰猫漂浮 · float

amplitude=3本地设计单位，speed=1.4 rad/s，phase为弧度。按宿主尺寸调整距离。

时间来源：`KitClock.Delta`。

## 欢迎语弹簧拖拽 · drag

tanh限位；弹性105、阻尼6.5，每帧四子步；这组为固定算法，修改时记录差异。

时间来源：`KitClock.Delta`。

## 活动卡果冻弹簧 · activity

弹性155、阻尼10、四子步；保留卡面与图标的相位关系。固定常量需代码适配。

时间来源：`KitClock.Delta`。

## 方向受击挤压 · hit

Duration=.45秒，Amplitude=.28；sin(4πt)×exp(-5t)。只变形视觉层。

时间来源：`KitClock.Delta`。

## 目标居中后归位 · goal

总长2.35秒；1.45秒开始飞回，移动.75秒SmoothStep；按真实父级做坐标换算。

时间来源：`KitClock.Delta`。

## 星星飞光与材质充能 · charge

飞光.24秒，填充.36秒；保留透明处理、填充遮罩与随时间变化的材质参数。

时间来源：`KitClock.Delta`。

## 胜利猫非均匀序列帧 · victorycat

Victory=true，16帧非均匀时长，总长1.92秒；PlaybackSpeed为倍率，Loop控制循环。不要平均化每帧时长。

时间来源：`KitClock.Delta`。

## 失败猫非均匀序列帧 · failurecat

Victory=false，16帧非均匀时长，总长2.32秒；PlaybackSpeed为倍率。不要复用胜利帧时长。

时间来源：`KitClock.Delta`。

## 胜利结算页面编排 · victory

光环96帧、爆发16帧；保留子演出相对顺序。整体固定编排，改动子时序需明确列出。

时间来源：`KitClock.Delta`。

## 失败结算页面编排 · failure

背景96帧，猫16帧非均匀；按钮随演出入场。固定编排，可按需适配布局。

时间来源：`KitClock.Delta`。

## 联赛奖章与升降名次 · medal

入场.55秒；做父级非等比缩放补偿。数值为输入，不在表现中计算排名。

时间来源：`KitClock.Delta`。

## 分数变色放大计分 · scoreimpact

Duration=.25秒，Amplitude=.2表示峰值1.2倍；p=sin(πt)，颜色Lerp(原色,Highlight,p)；Highlight默认RGB(.85,.45,.06)。

时间来源：`KitClock.Delta`。

## 加分合并与上浮淡出 · scoregain

MergeDelay=.07秒，每次到达重置合并等待；Duration=.65秒，FadeIn=.05秒，Rise=32单位，Amplitude=.06。alpha=(1-t)×clamp(age/FadeIn)。

时间来源：`KitClock.ScaledDelta`。

## 目标到达与完成印章 · taskarrival

IntroDuration=.48秒OutBack；PulseDuration=.38秒，RootAmplitude=.025，IconAmplitude=.1；底垫sin(27age)×exp(-8age)×.11；StampDuration=.4秒OutBack。

时间来源：`KitClock.ScaledDelta`。

## 剩余步数挤压反馈 · moves

IntroDuration=.5秒OutBack，从.9到1且上限1.025；PulseDuration=.3秒，Amplitude=.025，sin(πt)控制挤压。

时间来源：`KitClock.ScaledDelta`。

## 连击评价与星光爆发 · rating

Duration=1.8秒；前12%从.45到1.12，随后17%回到1；4+Tier×3颗星，角度步进2.399弧度。字图可替换为宿主评价。

时间来源：`KitClock.ScaledDelta`。

## 排行卡片果冻抖动 · leaguejelly

duration=.65秒；wave=sin(5πt)×exp(-4t)×(1-t)；横向+wave×.09，纵向-wave×.12，旋转wave×2.2度。

时间来源：`KitClock.Delta`。

## 主页背景平滑换景 · themedissolve

Duration=.8秒；旧图alpha=1-t²(3-2t)。保持两张图相同覆盖布局和遮罩范围。

时间来源：`KitClock.Delta`。

## Loading 果冻进度条 · loadingprogress

FillSpeed=.32比例/秒，MaxFrameDelta=.05秒。SetProgress单调增加且最多.99；Complete后才到1。完成脉冲.42秒、.045幅度；完成星光.48秒，6颗。

时间来源：`KitClock.Delta`。

## 微信启动 Loading 整页 · startuploading

进度使用同一个LoadingProgress；猫周期9秒，浮岛4.8秒/9单位；文字每2.8秒切换，4字错开.17秒。进度不按装饰时钟虚增。

时间来源：`KitClock.Delta`。

## 同级页面横向切换 · pagenav

Duration=.27秒，OutCubic：1-(1-t)^3。目标为父级本地坐标；连续触发从当前位置接续。

时间来源：`KitClock.Delta`。

## 商店分组错峰入场 · stagger

Duration=.20秒，Interval=.018秒，StartScale=.98；逐项OutCubic回到各自原比例。

时间来源：`KitClock.Delta`。

## 排行榜名次平滑重排 · rankmove

Duration=.48秒，OutCubic；Stride为宿主行距。每行新名次由宿主传入，重入从当前姿态接续。

时间来源：`KitClock.Delta`。

## 教学手势与目标强调 · guide

Period=4.4秒；StartDelay=.6秒，MoveDuration=1.3秒，SmoothStep位移。触点缩放=.85+.12sin(8t)，目标框=1+.05sin(4t)。

时间来源：`KitClock.ScaledDelta`。

## 操作后提示平滑淡出 · hintfade

Speed=2：alpha每秒改变2。提示不拦截射线；Visible只控制表现。

时间来源：`KitClock.ScaledDelta`。

## 欢迎语分层入场与呼吸 · homefeedback

Duration=1.35秒，副文案延迟.13秒；Pop=-cos(17t)exp(-4.7t)×.85。闲置角速度1.8，标题位置幅度3、比例幅度.018。

时间来源：`KitClock.Delta`。

## 星星挂件受力摆动 · pendulum

Spring=30，Damping=3.6，四子步；MaxAngle=36度，碰边速度×-.25；ImpulseStrength=120（默认示例输入）。

时间来源：`KitClock.ScaledDelta`。

## 联赛徽章轻微呼吸 · breath

Amplitude=.035，AngularSpeed=3 rad/s；相对原比例乘1+Amplitude×sin(age×AngularSpeed)。

时间来源：`KitClock.Delta`。
