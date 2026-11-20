# BallBody · PhysicsBodies

状态：`mixed-physics-presentation-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/PhysicsBodies.cs`
文件 SHA-256：`4a7c2a6cede5908904f677f27deef0120740c077dad28921b906f4eedea61d21`
[原始源码快照](../source/Assets/Scripts/World/PhysicsBodies.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### FixedUpdate · L77

方法 SHA-256：`68ebb4fc7a5c8d62a29de09fbde2bfee7f1a515c09e16410dfe8cf5207340e72`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BallBody.FixedUpdate()`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/World/PhysicsBodies.cs:129` · `BallBody.QATick` → `FixedUpdate()`

```csharp
void FixedUpdate()
        {
            if(!Active||owner==null||!owner.IsPlaying||owner.IsPaused||owner.JuiceFrozen||owner.RefillFreezeBalls)return;
            for(int i=ignored.Count-1;i>=0;i--){var c=ignored[i];if(c==null){ignored.RemoveAt(i);continue;}var bounds=c.bounds;bounds.Expand(FlightGeometry.Radius*2+.03f);if(!c.enabled||!bounds.Contains(new Vector3(Body.position.x,Body.position.y,c.bounds.center.z))){Physics2D.IgnoreCollision(circle,c,false);ignored.RemoveAt(i);}}
            if(restoreStreakPierce){Body.linearVelocity=flightDirection*flightSpeed;restoreStreakPierce=false;}
            RestoreSpeed();
            SweepStreakPierce();
            Vector2 p=Body.position;
            float returnY=owner.ReturnLineY;
            if(p.y>(owner.IsChainCollection?returnY+.1f:-5.0f))enteredBoard=true;
            if((enteredBoard&&previousY>returnY&&p.y<=returnY) || p.y < -10.65f || Mathf.Abs(p.x)>6.2f || p.y>10.5f)
            { var exit=enteredBoard&&previousY>returnY&&p.y<=returnY?Vector2.Lerp(previousPosition,p,(previousY-returnY)/(previousY-p.y)):p;trail.Sample(exit);if(owner.TryPawReturn(this,exit))return;owner.LoseBall(this,exit);return; }
            owner.CheckChainTools(this,previousPosition,p);
            trail.Sample(p);previousPosition=p;previousY=p.y;
        }
```

### Update · L132

方法 SHA-256：`a1cf11adb56da4cb2802667cdc8a5b4b3e33055874f64a4cc62c31a1dbf01a34`
时间来源：Time.deltaTime
曲线证据：Mathf.Sin
解析符号：`BounceGarden.BallBody.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){if(!Active||owner==null||owner.IsPaused||owner.JuiceFrozen)return;impact=Mathf.Max(0,impact-Time.deltaTime);float k=FeedbackProfile.Active.enabledFeedback?Mathf.Sin(impact/.10f*Mathf.PI)*.07f:0;visual.transform.localScale=Vector3.Scale(StreakPower==StreakBooster.None?visualBaseScale:Vector3.one*StreakBallArt.BallScale(StreakPower),new Vector3(1+k,1-k,1)*(strand?.72f:1));if(StreakPower!=StreakBooster.None){streakArtAge+=Time.deltaTime;visual.sprite=StreakBallArt.Frame(StreakPower,4+(int)(streakArtAge*12)%4);}if(owner.IsJuiceStage){int power=owner.Mechanic==JuiceMechanic.Heat?(HeatHits>=8?2:HeatHits>=4?1:0):BankCharge;visual.color=power>0?Color.Lerp(Color.white,owner.Mechanic==JuiceMechanic.Bank?JuiceGardenView.Mint:JuiceGardenView.Gold,.25f*power):Color.white;}if(owner.IsAbilityRoute){abilityAge+=Time.deltaTime;visual.sprite=AbilityArt.Ball(Ability,Mathf.FloorToInt(abilityAge*(charged||piercing?16:8))%6);}}
```

### OnCollisionEnter2D · L146

方法 SHA-256：`2febe3633caade43e400550a14cb8352054dec9246bd2af50b894a783adfcad8`
时间来源：Time.time
曲线证据：查原式或下级调用
解析符号：`BounceGarden.BallBody.OnCollisionEnter2D(UnityEngine.Collision2D)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
void OnCollisionEnter2D(Collision2D collision)
        {
            if(!Active||owner==null||!owner.IsPlaying||owner.IsPaused||SessionGeneration!=owner.Generation)return;
            PlayerHaptics71.Pulse();
            var abilityBrick=collision.collider.GetComponentInParent<BrickBody>();
            if((owner.IsStreakTrial||owner.UsesShopBoosters)&&StreakPower==StreakBooster.Pierce&&abilityBrick!=null&&abilityBrick.Definition.kind!=BrickKind.Stone&&!owner.FusionPierceBlocked(abilityBrick)&&owner.FusionPierceAvailable(this)){
                IncomingDirection=flightDirection;Physics2D.IgnoreCollision(circle,collision.collider,true);if(!ignored.Contains(collision.collider))ignored.Add(collision.collider);restoreStreakPierce=true;Body.linearVelocity=flightDirection*flightSpeed;owner.HitBrick(abilityBrick,this,Body.position);owner.PlayStreakImpact(StreakBooster.Pierce,Body.position);owner.FusionConsumePierce(this);return;
            }
            if(owner.IsAbilityRoute&&piercing&&abilityBrick!=null&&abilityBrick.Definition.kind!=BrickKind.Stone){
                if(pierced.Add(abilityBrick.Id)){
                    // Preserve the incoming direction; bypass the solver's reflection.
                    Physics2D.IgnoreCollision(circle,collision.collider,true);ignored.Add(collision.collider);
                    Body.linearVelocity=flightDirection*flightSpeed;
                    owner.HitBrick(abilityBrick,this,Body.position);
                    if(pierced.Count>=3)piercing=false;
                }return;
            }
            if(owner.IsAbilityRoute&&piercing)piercing=false;
            IncomingDirection=flightDirection;RestoreSpeed(collision);
            impact=.10f;if(collision.contactCount>0){var c=collision.GetContact(0);trail.Sample(c.point+c.normal*FlightGeometry.Radius);}
            var brick=collision.collider.GetComponentInParent<BrickBody>();
            Vector2 point=collision.contactCount>0?collision.GetContact(0).point:Body.position;
            var boss=collision.collider.GetComponent<BossBody>();
            if(boss!=null){owner.HitBoss(boss,this,point);return;}
            var jellyWall=collision.collider.GetComponent<JellyWallContact>();
            if(jellyWall!=null){float normalSpeed=Mathf.Abs(jellyWall.Side==1?IncomingDirection.y:IncomingDirection.x)*collision.relativeVelocity.magnitude;jellyWall.Hit(point,normalSpeed);owner.OnJuiceWall(this,jellyWall.Side,point);owner.OnChainWall(this,jellyWall.Side);}
            if(brick!=null){if(owner.FusionActive)owner.HitFusionBallAndPower(brick,this,point);else{owner.HitBrick(brick,this,point);owner.TriggerStreakImpact(this,point);}return;}
            if(jellyWall!=null)owner.TriggerStreakImpact(this,point);
            var pillar=collision.collider.GetComponentInParent<ElectricPillarContact>();if(pillar!=null)pillar.Circuit.PreviewKnownConnections();
            var mechanism=collision.collider.GetComponentInParent<MovingObstacle>();
            if(mechanism!=null&&mechanism.IsRotor)owner.RecordRotorContact();
            var ball=collision.collider.GetComponent<BallBody>();
            if(ball!=null&&Id>ball.Id)return;
            if(Time.time-lastContactTime>.06f && collision.relativeVelocity.sqrMagnitude>1)
            {lastContactTime=Time.time;owner.EmitFeedback(point,"wall",new Color(.92f,1,.94f));}
        }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

```csharp
void RestoreSpeed(Collision2D collision=null)
        {
            if(collision==null&&(Body.linearVelocity-flightDirection*flightSpeed).sqrMagnitude<.00000001f)return;
            Vector2 direction=flightDirection;
            // The solver supplies the bounce. Only recover a stopped/inward result at a contact.
            if(collision!=null)for(int i=0;i<collision.contactCount;i++){
                Vector2 normal=collision.GetContact(i).normal;
                if(Vector2.Dot(direction,normal)<-.0001f)direction=Vector2.Reflect(direction,normal);
            }
            flightDirection=direction.normalized;Body.linearVelocity=flightDirection*flightSpeed;
        }
```

```csharp
public void PauseTrail(bool pause){Body.gravityScale=0;}
```

## 源字段与依赖

```csharp
public int ChainBurstId;
```
```csharp
public float ChainBornAt;
```
```csharp
public int StreakProcStep=-1;
```
```csharp
float streakArtAge;
```
```csharp
GameSession owner;
```
```csharp
bool enteredBoard;
```
```csharp
Vector2 previousPosition;
```
```csharp
float previousY, lastContactTime, impact;
```
```csharp
Vector2 flightDirection=Vector2.up;
```
```csharp
float flightSpeed;
```
```csharp
SpriteRenderer visual,boosterHalo;
```
```csharp
FlightTrail trail;
```
```csharp
public int ChainBreaks,ChainBankBreaks;
```
```csharp
public bool ChainBank,ChainGenerated;
```
```csharp
public int HeatHits, BankCharge;
```
```csharp
bool charged,piercing,strand;
```
```csharp
int strandHits;
```
```csharp
readonly System.Collections.Generic.HashSet<int> pierced=new System.Collections.Generic.HashSet<int>();
```
```csharp
readonly System.Collections.Generic.List<Collider2D> ignored=new System.Collections.Generic.List<Collider2D>();
```
```csharp
readonly RaycastHit2D[] streakPierceHits=new RaycastHit2D[24];
```
```csharp
bool restoreStreakPierce;
```
```csharp
CircleCollider2D circle;
```
```csharp
float abilityAge;
```
```csharp
Vector3 visualBaseScale;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
