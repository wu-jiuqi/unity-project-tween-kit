using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace ProjectTweenKit {
// Fit the caller's local presentation rectangle, independent of full-screen UI.
public sealed class ResultSurface048:MonoBehaviour {
 public RectTransform Content{get;private set;}public RectTransform Panel{get;private set;}
 public Vector2 AvailableSize=new Vector2(1080,1920);public bool LayoutFits{get;private set;}
 public static ResultSurface048 Create(Transform parent,string name){var s=parent.gameObject.AddComponent<ResultSurface048>();s.Content=UICraft.Root(parent,name,0,0,1080,1920);s.Panel=LevelLoadingArt.Image(s.Content,"panel",0,5,1000);s.Layout();return s;}
 public static TMP_Text Text(Transform p,string text,float x,float y,float w,float h,int size,bool muted=false){var t=UICraft.Label(p,text,x,y,w,h,size,muted?CSettingsArt.Muted:CSettingsArt.Ink);t.fontStyle=FontStyles.Normal;return t;}
 public static Image Image(Transform p,string name,Sprite sprite,float x,float y,float w,float h){var i=UICraft.Box(p,name,x,y,w,h,Color.white);i.sprite=sprite;i.type=UnityEngine.UI.Image.Type.Simple;i.preserveAspect=true;i.raycastTarget=false;return i;}
 void Layout(){if(Content==null)return;float q=Mathf.Min(AvailableSize.x/1080,AvailableSize.y/1920);Content.localScale=Vector3.one*Mathf.Max(.001f,q);LayoutFits=q>0&&1080*q<=AvailableSize.x+.1f&&1920*q<=AvailableSize.y+.1f;}
 void LateUpdate(){Layout();}
}
}
