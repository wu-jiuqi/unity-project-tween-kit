using UnityEditor;
using UnityEngine;
namespace ProjectTweenKit.Editor {
public sealed class MotionPresetInspector:EditorWindow {
 MonoBehaviour component;
 [MenuItem("Tween Kit/Component parameters")]
 static void Open(){GetWindow<MotionPresetInspector>("动画参数");}
 void OnGUI(){component=(MonoBehaviour)EditorGUILayout.ObjectField("组件",component,typeof(MonoBehaviour),true);if(component==null||component.GetType().Namespace!="ProjectTweenKit")return;
  if(GUILayout.Button("保存当前组件动画参数…")){string p=EditorUtility.SaveFilePanel("保存参数","",component.GetType().Name+".json","json");if(!string.IsNullOrEmpty(p))MotionPreset.Capture(component).Save(p);}
  if(GUILayout.Button("载入同一组件动画参数…")){string p=EditorUtility.OpenFilePanel("载入参数","","json");if(!string.IsNullOrEmpty(p)){try{Undo.RecordObject(component,"Load motion preset");MotionPreset.Load(p).Apply(component);EditorUtility.SetDirty(component);}catch(System.Exception e){EditorUtility.DisplayDialog("无法载入",e.Message,"确定");}}}
 }
}
}
