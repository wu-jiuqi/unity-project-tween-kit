using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace ProjectTweenKit {
public static class UICraft {
 public static readonly Color Ink = new Color32(108,76,74,255);
 public static readonly Color Green = new Color32(160,226,195,255);
 public static readonly Color Blue = new Color32(193,169,235,255);
 public static readonly Color Gold = new Color32(255,199,42,255);
 static Sprite rounded, circle; static Font font;
 public static Font Font { get { if(font==null)font=Resources.Load<Font>("Fonts/MaokenZhuyuanTi");return font;} }
 public static RectTransform Root(Transform parent,string name,float x=0,float y=0,float w=1080,float h=2340){var go=new GameObject(name,typeof(RectTransform));var rt=go.GetComponent<RectTransform>();rt.SetParent(parent,false);rt.sizeDelta=new Vector2(w,h);rt.anchoredPosition=new Vector2(x,y);return rt;}
 public static Sprite Shape(bool disk=false){if(disk&&circle!=null)return circle;if(!disk&&rounded!=null)return rounded;const int n=64;var t=new Texture2D(n,n,TextureFormat.RGBA32,false);t.filterMode=FilterMode.Bilinear;t.wrapMode=TextureWrapMode.Clamp;var p=new Color[n*n];for(int y=0;y<n;y++)for(int x=0;x<n;x++){float dx=Mathf.Abs(x-31.5f),dy=Mathf.Abs(y-31.5f);float d=disk?Mathf.Sqrt(dx*dx+dy*dy)-30f:Mathf.Sqrt(Mathf.Pow(Mathf.Max(dx-18,0),2)+Mathf.Pow(Mathf.Max(dy-18,0),2))-13f;p[y*n+x]=new Color(1,1,1,Mathf.Clamp01(1-d));}t.SetPixels(p);t.Apply();var s=Sprite.Create(t,new Rect(0,0,n,n),new Vector2(.5f,.5f),100,0,SpriteMeshType.FullRect,disk?Vector4.zero:new Vector4(18,18,18,18));if(disk)circle=s;else rounded=s;return s;}
 public static Image Box(Transform p,string n,float x,float y,float w,float h,Color color,bool disk=false,bool raycast=false){var rt=Root(p,n,x,y,w,h);var i=rt.gameObject.AddComponent<Image>();i.sprite=Shape(disk);i.type=disk?Image.Type.Simple:Image.Type.Sliced;i.color=color;i.raycastTarget=raycast;return i;}
 public static Image Art(Transform p,string resource,float x,float y,float w,float h){var i=Box(p,resource,x,y,w,h,Color.white);i.sprite=PastelArt.Resolve(resource)??PastelArt.Get(resource)??Resources.Load<Sprite>("Art/"+resource);if(i.sprite==null)throw new InvalidOperationException("Missing example art "+resource);i.type=Image.Type.Simple;i.preserveAspect=true;return i;}
 public static TMP_Text Label(Transform p,string value,float x,float y,float w,float h,int size,Color? color=null,TextAnchor align=TextAnchor.MiddleCenter,bool outline=false){
 var rt=Root(p,"Text "+value,x,y,w,h);var t=rt.gameObject.AddComponent<TextMeshProUGUI>();t.text=System.Text.RegularExpressions.Regex.Replace(value??"",@"\bLevel\s*(\d+)","第$1关");t.font=size>=48?GameTypography.HeadingFont:GameTypography.Font;t.fontSize=size;t.color=color.HasValue&&color.Value.maxColorComponent<.5f?color.Value:Ink;t.raycastTarget=false;t.overflowMode=TextOverflowModes.Overflow;
 t.alignment=align==TextAnchor.MiddleLeft?TextAlignmentOptions.MidlineLeft:align==TextAnchor.MiddleRight?TextAlignmentOptions.MidlineRight:TextAlignmentOptions.Midline;
 t.enableAutoSizing=true;t.fontSizeMin=Mathf.Min(size,22);t.fontSizeMax=size;return t;
 }
 public static Button Button(Transform p,string value,float x,float y,float w,float h,Color color,Action action,int size=44)=>CSettingsArt.Button(p,"Button "+value,value,x,y,w,h,action,"button_peach_normal",size);
 public static RectTransform Character(Transform p,float x,float y,float scale,Color color,bool ears=true){var r=Root(p,"Garden friend",x,y,300,300);r.localScale=Vector3.one*scale;Art(r,"cat_idle",0,0,300,300);return r;}
 public static float OutBack(float t){t-=1;return 1+2.70158f*t*t*t+1.70158f*t*t;}
}
public sealed class UILightSurface:MonoBehaviour {}
}
