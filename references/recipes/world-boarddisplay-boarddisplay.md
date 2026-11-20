# BoardDisplay · BoardDisplay

状态：`non-animation`；交付：`source-evidence-only`。

源文件：`Assets/Scripts/World/BoardDisplay.cs`
文件 SHA-256：`f8fe9a800ae6fbfa87c2f6fb3bdabeaf24ef5aa5cb747f52b68e9d839eba395a`
[原始源码快照](../source/Assets/Scripts/World/BoardDisplay.cs.txt)

此页是来源证据，尚未构成可独立导入的组件或完整适配配方。不能据此宣称当前页面已启用或已覆盖交付。

## 源方法、参数与曲线

### Y · L8

方法 SHA-256：`cd9a85476fd1f6b92a46c3b948015b01d77e0d417068c930060964d17ddac10e`
时间来源：本方法未直接取时；查调用者/字段
曲线证据：Mathf.LerpUnclamped
解析符号：`BounceGarden.BoardDisplay.Y(float, bool)`

未解析到直接调用；仍须检查委托、反射、动态类型和条件入口。

```csharp
static float Y(float y,bool inverse){float a=map.x,s=map.y,l=map.z,h=map.w;if(Mathf.Abs(s-1)<.00001f)return y;float lo=a+(l-a)*s,hi=a+(h-a)*s;if(y<=-5.9f||y>=7.5f)return y;
  if(!inverse){if(y<l)return Mathf.LerpUnclamped(-5.9f,lo,(y+5.9f)/(l+5.9f));if(y>h)return Mathf.LerpUnclamped(hi,7.5f,(y-h)/(7.5f-h));return a+(y-a)*s;}
  if(y<lo)return Mathf.LerpUnclamped(-5.9f,l,(y+5.9f)/(lo+5.9f));if(y>hi)return Mathf.LerpUnclamped(h,7.5f,(y-hi)/(7.5f-hi));return a+(y-a)/s;
 }
```

## 对象层级和素材入口


素材完整路径、原始 GUID 与包内映射见 [resource-map.json](../resource-map.json)。未解析的动态路径仍留在 source-map.json 的 resourceEvidence；不能猜测为已打包。

## 生命周期证据

本源类没有显式的停止／暂停／清理方法。检查拥有者的生命周期；尚未提取条目需要先定义宿主合同。

## 源字段与依赖

```csharp
static Vector4 map=new Vector4(0,1,0,0);
```

业务/组件依赖保留在上述字段、泛型 Get/AddComponent 和源方法参数中。语义解析不覆盖运行时委托结果；`nameMatchedCallSites` 仅供补充检索。
