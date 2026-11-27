# GameSession · GameSession

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/Core/GameSession.cs`
文件 SHA-256：`dd1eded2be4039a1aff04a9659e1fe3245e80b5bcc916b329c6417bd5b252b74`
[原始源码快照](../source/Assets/Scripts/Core/GameSession.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### StartSession · L118

方法 SHA-256：`34c5a1e41a2363978d5c8b8b50677c0124c2b78bfc57715a5e8de8698259cf0f`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameSession.StartSession(int, bool, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/GameSession.cs:115` · `GameSession.StartLevel` → `StartSession(index,false)`

```csharp
void StartSession(int index,bool tutorialMode,int trial=-1)
        {
            var stack=new Stack<IEnumerator>();stack.Push(BuildSession(index,tutorialMode,trial,false));
            try{while(stack.Count>0){var step=stack.Peek();if(!step.MoveNext()){(stack.Pop() as IDisposable)?.Dispose();continue;}if(step.Current is IEnumerator nested)stack.Push(nested);}}
            finally{while(stack.Count>0)(stack.Pop() as IDisposable)?.Dispose();}
        }
```

### PrepareSession · L125

方法 SHA-256：`e44ac37a713228b2debfe137f0c91afbb6a624d73688cb55f4b734572e261de1`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameSession.PrepareSession(int, bool, int)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/AppUI.cs:170` · `AppUI.StartPreparedLevel` → `Session.PrepareSession(currentLevel,false,streakTrial)`

```csharp
// UI entry yields between construction batches; fixture callers retain synchronous entry.
        public IEnumerator PrepareSession(int index,bool tutorialMode,int trial=-1)=>BuildSession(trial>=0?20:index,tutorialMode,trial,true);
```

### BuildSession · L126

方法 SHA-256：`1f69fd765c8673e99399ea2dee73c8c2afd320c70eab54604a60d9b63eaca55e`
时间来源：Time.realtimeSinceStartupAsDouble
曲线证据：查原式或下级调用
解析符号：`BounceGarden.GameSession.BuildSession(int, bool, int, bool)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/GameSession.cs:120` · `GameSession.StartSession` → `BuildSession(index,tutorialMode,trial,false)`
- `Assets/Scripts/Core/GameSession.cs:125` · `GameSession.PrepareSession` → `BuildSession(trial>=0?20:index,tutorialMode,trial,true)`

```csharp
IEnumerator BuildSession(int index,bool tutorialMode,int trial,bool incremental)
        {
            double phaseStarted=Time.realtimeSinceStartupAsDouble;
            Action<string> phase=name=>{double now=Time.realtimeSinceStartupAsDouble;Debug.Log("BOUNCE_BOARD_PHASE level="+index+" phase="+name+" ms="+((now-phaseStarted)*1000).ToString("F1",System.Globalization.CultureInfo.InvariantCulture));phaseStarted=now;};
            StopLevel();ResetScore();nextVolleyYarn=UnityEngine.Random.Range(0,YarnArt.ColorCount);StreakTrialIndex=trial;ResetStreakTrialTools();LevelIndex=Mathf.Clamp(index,0,LevelCatalog.Count-1);
            var source=LevelCatalog.Get(LevelIndex);
            OverrideEditorLevel(ref source);CurrentLevel=FitToArt(source);
            Board=BoardSpace.ForLevel(LevelIndex,CurrentLevel);
            BoardDisplay.Set(Board);
            Generation++;OnEditorSessionStarted();nextTargetId=1;nextBallId=1;SpawnCount=LostCount=PhysicsStep=HitCount=BreakCount=RotorContacts=0;simulationClock=0;
            rules.Reset(CurrentLevel.initialShots);rules.UnlimitedShots=IsCollection&&!IsChainCollection;IsPlaying=true;IsPaused=incremental;
            world=new GameObject("WorldRoot").transform;world.SetParent(transform,false);
            fx=new GameObject("WorldFeedback").AddComponent<WorldFx>();fx.transform.SetParent(world,false);fx.Owner=this;
            if(IsCollection||JellyBrickArt.Enabled(LevelIndex))fx.PrepareJelly(CurrentLevel.bricks.Length+6);
            
            phase("pools");if(incremental)yield return null;
            BuildGarden();
            phase("garden");if(incremental)yield return null;
            ballRoot=new GameObject("Balls").transform;ballRoot.SetParent(world,false);
            for(int i=0;i<20;i++){pool.Push(CreateBall());if(incremental&&i%5==4)yield return null;}
            for(int i=0;i<CurrentLevel.bricks.Length;i++){CreateBrick(CurrentLevel.bricks[i],Mathf.Min(i*.012f,.23f));if(incremental&&i%8==7)yield return null;}
            phase("balls");
            if(incremental&&IsOpening)yield return InitializeOpeningGridSteps(true);else StartCollection();
            phase("grid");if(incremental)yield return null;            BuildCannon();BuildAimDots();            phase("launcher");if(incremental)yield return null;
            var feel=new GameObject("Combat feedback v79");feel.transform.SetParent(world,false);CombatFeel=feel.AddComponent<CombatFeedback79>();CombatFeel.Initialize(this);
            Physics2D.SyncTransforms();changed=true;StateChanged?.Invoke();
            Debug.Log("SESSION_START generation="+Generation+" level="+CurrentLevel.label+" shots="+Shots+" targets="+RemainingTargets+" volley="+CurrentLevel.volleyCount);
        }
```

### NormalizeAimDirection · L206

方法 SHA-256：`4e4703f8cdd1f0f42f6f5bb4a3b8ef90407ae7e5aa6b9ca0e8829e4518eae09f`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.GameSession.NormalizeAimDirection(UnityEngine.Vector2)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/Core/ChainLiveSession.cs:14` · `GameSession.BufferChainShot` → `NormalizeAimDirection(direction)`
- `Assets/Scripts/Core/GameSession.cs:35` · `GameSession.SpawnPosition` → `NormalizeAimDirection(direction)`
- `Assets/Scripts/Core/GameSession.cs:203` · `GameSession.TryShootCore` → `NormalizeAimDirection(direction)`
- `Assets/Scripts/Core/GameSession.cs:343` · `GameSession.SetAim` → `NormalizeAimDirection(direction.Value)`
- `Assets/Scripts/Core/GameSession.cs:356` · `GameSession.PredictAim` → `NormalizeAimDirection(direction)`

```csharp
public static Vector2 NormalizeAimDirection(Vector2 direction)
        {
            float angle=Mathf.Clamp(Mathf.Atan2(direction.y,direction.x),12*Mathf.Deg2Rad,168*Mathf.Deg2Rad);
            return new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));
        }
```

## 对象层级和素材入口

- L132：`FitToArt(source)`
- L137：`world.SetParent(transform,false)`
- L138：`fx.transform.SetParent(world,false)`
- L144：`ballRoot.SetParent(world,false)`
- L150：`feel.transform.SetParent(world,false)`

素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
public bool IsPaused { get; private set; }
```

```csharp
partial void OnEditorSessionStopped();
```

```csharp
public void StopLevel()
        {
            VictoryBoardSwept=false;VictorySettlementActive=VictorySpecialsReleased=false;VictoryGroupsClosed=VictorySpecialsTriggered=0;
            ClearVictoryDeferred();
            if(CombatFeel!=null)CombatFeel.CancelStop();CombatFeel=null;
            var shake=Camera.main!=null?Camera.main.GetComponent<CameraJuice>():null;if(shake!=null&&shake.Owner==this)shake.ResetImpact();
            ClearCollection();
            StreakTrialIndex=-1;ResetStreakTrialTools();
            
            
            IsPlaying=false;IsPaused=false;queuedShot=null;noDamageTime=0;Generation++;
            BoardDisplay.Clear();
            foreach(var ball in allBalls)if(ball!=null)ball.Deactivate();
            active.Clear();reserved.Clear();pool.Clear();allBalls.Clear();aimDots.Clear();contactsThisStep.Clear();rules.Reset(0);
            if(world!=null){world.gameObject.SetActive(false);Destroy(world.gameObject);world=null;}
            cannonPivot=null;cannonVisual=null;catLauncher=null;loadedBall=null;streakArtFx=null;jellyPowerFxPlayed=0;fx=null;GardenDecor=null;changed=true;
            OnEditorSessionStopped();
        }
```

```csharp
public void SetPaused(bool paused)
        {
            if(!IsPlaying)return;
            if(CombatFeel!=null)CombatFeel.CancelStop();
            IsPaused=paused;queuedShot=null;ClearBufferedChainShot();SetAim(null);
            if(paused){var shake=Camera.main!=null?Camera.main.GetComponent<CameraJuice>():null;if(shake!=null&&shake.Owner==this)shake.ResetImpact();}
            if(IsChainCollection)foreach(var brick in CollectionBricks){var rb=brick.GetComponent<Rigidbody2D>();if(rb!=null)rb.simulated=!paused;}
            
            foreach(var ball in active.Values)if(ball!=null){ball.Body.gravityScale=0;ball.Body.linearDamping=0;ball.Body.simulated=!paused&&!RefillFreezeBalls;ball.PauseTrail(paused||RefillFreezeBalls);}
            
            changed=true;StateChanged?.Invoke();
        }
```

```csharp
void OnApplicationFocus(bool focus){if(!focus&&IsPlaying&&!QAIgnoreFocusLoss)SetPaused(true);}
```

```csharp
void OnDestroy(){StopLevel();if(bounce!=null)Destroy(bounce);}
```

## 源字段与依赖

```csharp
readonly RuleState rules = new RuleState();
```
```csharp
readonly Stack<BallBody> pool = new Stack<BallBody>();
```
```csharp
readonly Queue<BallBody> reserved = new Queue<BallBody>();
```
```csharp
readonly Dictionary<int,BallBody> active = new Dictionary<int,BallBody>();
```
```csharp
readonly List<BallBody> allBalls = new List<BallBody>();
```
```csharp
readonly HashSet<long> contactsThisStep = new HashSet<long>();
```
```csharp
readonly List<SpriteRenderer> aimDots = new List<SpriteRenderer>();
```
```csharp
Transform world,ballRoot,cannonPivot,cannonVisual;
```
```csharp
WorldFx fx;
```
```csharp
Vector2 feedbackIncoming=Vector2.up;
```
```csharp
RearCatLauncher catLauncher;
```
```csharp
int nextVolleyYarn;
```
```csharp
SpriteRenderer aimEnd;
```
```csharp
Transform loadedBall;
```
```csharp
readonly RaycastHit2D[] aimHits=new RaycastHit2D[32];
```
```csharp
float noDamageTime;
```
```csharp
PhysicsMaterial2D bounce;
```
```csharp
Vector2? queuedShot;
```
```csharp
Vector2 burstDirection=Vector2.up;
```
```csharp
float simulationClock,nextSpawnTime;
```
```csharp
int nextBallId,nextTargetId;
```
```csharp
bool changed;
```
```csharp
public const float MuzzleOffset = 105*10.8f/592f;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
