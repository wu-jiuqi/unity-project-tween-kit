using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using ProjectTweenKit;
namespace TweenGallery {
public sealed partial class Gallery {
 [Serializable]class ParameterCatalog {public ParameterNotes[] entries;}
 [Serializable]class ParameterNotes {public string id,notes,timeSource;}
 [Serializable]public sealed class ParameterExport {
  public int schemaVersion=1;public string component;public List<MotionPreset.Setting> settings;
  public string entryId,title,timeSource,referenceParameterNotes;
 }
 ParameterCatalog parameterCatalog;Text parameterSummaryLabel;float parametersCopiedUntil;string parametersCopyFeedback;
 public Button CopyParametersButton=>copyParametersButton;
 public string ParameterExportPath=>PresetPath;
 public void ShowParameters(bool show){ShowParameterSummary(show);if(show)UpdateParameterSummary();}
 ParameterNotes CurrentParameterNotes(){if(parameterCatalog==null){var resource=Resources.Load<TextAsset>("ProjectTweenKit/parameters");if(resource==null)throw new InvalidOperationException("Parameter catalog is missing");parameterCatalog=JsonUtility.FromJson<ParameterCatalog>(resource.text);}return Array.Find(parameterCatalog.entries,p=>p.id==Current.id)??throw new InvalidOperationException("Missing parameter notes: "+Current.id);}
 public string BuildParameterExport(){var preset=MotionPreset.Capture(component);var notes=CurrentParameterNotes();return JsonUtility.ToJson(new ParameterExport{component=preset.component,settings=preset.settings,entryId=Current.id,title=Current.title,timeSource=notes.timeSource,referenceParameterNotes=notes.notes},true);}
 public string BuildParametersText(){
  var notes=CurrentParameterNotes();var b=new StringBuilder("当前生效参数\n\n");int count=0;
  foreach(var field in component.GetType().GetFields(BindingFlags.Public|BindingFlags.Instance)){
   if(field.IsInitOnly)continue;object value=field.GetValue(component);string formatted=null;
   if(value is float f)formatted=f.ToString("0.#####",CultureInfo.InvariantCulture)+(string.IsNullOrEmpty(Spec(field.Name).unit)?"":" "+Spec(field.Name).unit);
   else if(value is int n)formatted=n.ToString(CultureInfo.InvariantCulture);
   else if(value is bool flag)formatted=flag?"true":"false";
   else if(value is Enum)formatted=value.ToString();
   else if(value is Vector2 v)formatted="("+v.x.ToString("0.###",CultureInfo.InvariantCulture)+", "+v.y.ToString("0.###",CultureInfo.InvariantCulture)+")";
   else if(value is Vector3 w)formatted="("+w.x.ToString("0.###",CultureInfo.InvariantCulture)+", "+w.y.ToString("0.###",CultureInfo.InvariantCulture)+", "+w.z.ToString("0.###",CultureInfo.InvariantCulture)+")";
   else if(value is Color color)formatted="#"+ColorUtility.ToHtmlStringRGBA(color);
   if(formatted!=null){b.Append(field.Name).Append(" = ").Append(formatted).Append('\n');count++;}
  }
  if(count==0)b.Append("此项没有公开调参字段。\n");
  b.Append("\n固定参数与曲线基准\n").Append(notes.notes).Append("\n\n时间来源\n").Append(notes.timeSource).Append("\n\n组件\n").Append(component.GetType().FullName).Append("\n\n导出格式\n兼容 MotionPreset 的 JSON。可调字段放在 settings；固定参数说明只供核对，不会覆盖当前调参值。对象引用另行绑定。");return b.ToString();
 }
 void UpdateParameterSummary(){if(parameterSummaryLabel==null)return;string value=BuildParametersText();if(parameterSummaryLabel.text!=value)parameterSummaryLabel.text=value;parameterSummaryLabel.rectTransform.sizeDelta=new Vector2(322,Mathf.Max(600,parameterSummaryLabel.preferredHeight+15));detailsContent.sizeDelta=new Vector2(329,parameterSummaryLabel.rectTransform.sizeDelta.y+20);}
 public void CopyParameters(){string json=BuildParameterExport();GUIUtility.systemCopyBuffer=json;try{Directory.CreateDirectory(Path.GetDirectoryName(PresetPath));File.WriteAllText(PresetPath,json);parametersCopyFeedback="已复制并导出 JSON";Report("参数已复制并导出。切到“调参数”，点“参数目录”查看 JSON。");}catch(Exception e){parametersCopyFeedback="已复制，文件导出失败";Report("参数已复制；文件导出失败："+e.Message);}parametersCopiedUntil=Time.unscaledTime+3;}
}
}
