# BrandGardenScene · BrandGardenScene

状态：`requires-route-review`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/UI/BrandGardenScene.cs`
文件 SHA-256：`65d9bcaad7daa794a95a1d04f78d14fb7862b484c52995bfa36a76d6f7b64f85`
[原始源码快照](../source/Assets/Scripts/UI/BrandGardenScene.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Plaza · L24

方法 SHA-256：`a4d995f5907caf3781208ad26b641afb597ce2d007c50703acf9766896b8afff`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.Cos, Mathf.Sin
解析符号：`BounceGarden.BrandGardenScene.Plaza(float)`

Roslyn 已解析的实际源码调用位置：

- `Assets/Scripts/UI/BrandGardenScene.cs:21` · `BrandGardenScene.Build` → `Plaza(angle)`
- `Assets/Scripts/UI/BrandGardenScene.cs:33` · `BrandGardenScene.Update` → `Plaza(angle)`
- `Assets/Scripts/UI/BrandGardenScene.cs:42` · `BrandGardenScene.Update` → `Plaza(angle)`
- `Assets/Scripts/UI/BrandGardenScene.cs:54` · `BrandGardenScene.Update` → `Plaza(angle+.32f)`

```csharp
Vector2 Plaza(float a){float h=root.rect.height;return new Vector2(Mathf.Cos(a)*318,-h*.14f+Mathf.Sin(a)*h*.018f);}
```

### Update · L28

方法 SHA-256：`472f5be98333e8c34f1933ef69cd926c9882a4e0fb6fcbe53cbc635ffd7d30dd`
时间来源：Time.unscaledDeltaTime
曲线证据：Mathf.Sin, Mathf.SmoothStep
解析符号：`BounceGarden.BrandGardenScene.Update()`

Unity 引擎回调；是否创建并启用组件，见 constructionSites 与场景入口。

```csharp
void Update(){
  if(cat==null)return;float dt=Mathf.Min(Time.unscaledDeltaTime,.05f);age+=dt;
  float size=Mathf.Clamp(root.rect.height*.12f,210,280);cat.sizeDelta=new Vector2(size,size);
  float t=age-stateAt;Vector2 p=last;int frame=Mathf.FloorToInt(age*10)%4;
  if(action==Action.Run){
   angle+=dt*.9f;p=Plaza(angle);
   if(age>=nextVisit&&hedge.GrownCount>0)StartVisit(random.Next(hedge.GrownCount));
   else if(age>=nextWatch){action=Action.Watch;stateAt=age;from=p;nextWatch=age+Between(8,13);}
  }else if(action==Action.Approach){
   float f=Mathf.Clamp01(t/.9f);p=Vector2.Lerp(from,GardenTarget(),Mathf.SmoothStep(0,1,f));
   if(f>=1){action=Action.Paw;stateAt=age;}
  }else if(action==Action.Paw){
   p=GardenTarget();frame=6+(Mathf.FloorToInt(t*5)%2);
   if(!poked&&t>=.24f){hedge.Poke(selected);PawCount++;poked=true;}
   if(t>=1.0f){action=Action.Return;stateAt=age;from=p;returnTo=Plaza(angle);}
  }else if(action==Action.Return){
   float f=Mathf.Clamp01(t/1.0f);p=Vector2.Lerp(from,returnTo,Mathf.SmoothStep(0,1,f));
   if(f>=1){action=Action.Run;nextVisit=age+Between(5,9);}
  }else{
   p=from;frame=t>.22f&&t<.36f?5:4;if(t>=.7f)action=Action.Run;
  }
  float dx=p.x-last.x;float direction=action==Action.Paw?(selected<BrandGardenHedge.ClumpCount/2?-1:1):Mathf.Abs(dx)>.03f?Mathf.Sign(dx):Mathf.Sign(cat.localScale.x);
  bool moving=action==Action.Run||action==Action.Approach||action==Action.Return;
  cat.localScale=new Vector3(direction,1,1);cat.anchoredPosition=p+Vector2.up*(moving?Mathf.Abs(Mathf.Sin(age*10))*4:Mathf.Sin(age*2)*.8f);
  cat.localRotation=Quaternion.Euler(0,0,action==Action.Paw?Mathf.Sin(t*8)*1.2f:0);catImage.sprite=poses[frame];
  shadow.anchoredPosition=p+Vector2.down*4;shadow.sizeDelta=new Vector2(size*.55f,20);
  ball.anchoredPosition=Plaza(angle+.32f)+Vector2.up*20;ball.localRotation=Quaternion.Euler(0,0,-angle*Mathf.Rad2Deg*3);
  last=p;
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
RectTransform root,cat,shadow,ball;
```
```csharp
Image catImage;
```
```csharp
Sprite[] poses;
```
```csharp
BrandGardenHedge hedge;
```
```csharp
System.Random random;
```
```csharp
Action action;
```
```csharp
float age,angle,stateAt,nextVisit,nextWatch;
```
```csharp
Vector2 from,last,returnTo;
```
```csharp
int selected;
```
```csharp
bool poked;
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
