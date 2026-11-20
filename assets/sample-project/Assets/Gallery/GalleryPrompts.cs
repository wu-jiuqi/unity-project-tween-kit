using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
namespace TweenGallery {
public sealed partial class Gallery {
 [Serializable]class PromptLibrary {public PromptDesign[] entries;}
 [Serializable]class PromptDesign {public string id,body,trigger,tuning,cleanup;}
 PromptLibrary promptLibrary;Text promptLabel;float promptCopiedUntil;
 public Button CopyPromptButton=>copyPromptButton;
 public void ShowPrompt(bool show){ShowIntegration(show);if(show)UpdatePrompt();}
 public string BuildPrompt(){
  if(promptLibrary==null){var resource=Resources.Load<TextAsset>("ProjectTweenKit/prompts");if(resource==null)throw new InvalidOperationException("Prompt library is missing");promptLibrary=JsonUtility.FromJson<PromptLibrary>(resource.text);}
  var design=Array.Find(promptLibrary.entries,p=>p.id==Current.id);if(design==null)throw new InvalidOperationException("Missing animation prompt: "+Current.id);
  var b=new StringBuilder("请在当前 Unity 项目中实现以下补间动画，沿用当前项目的视觉风格、素材和输入方式。优先复用随 Skill 提供的组件。\n\n效果目标\n");
  b.Append(design.body).Append("\n\n触发方式\n").Append(design.trigger).Append("\n\n关键参数怎么写（设计基准）\n").Append(design.tuning);
  b.Append("\n\n当前参数（优先于上方基准）\n");int count=0;
  if(component!=null)foreach(var field in component.GetType().GetFields(BindingFlags.Public|BindingFlags.Instance)){
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
  if(count==0)b.Append("此项是固定编排，关键时间与曲线按上文实现；需要改变时显式记录差异。\n");
  b.Append("\n复用组件\nProjectTweenKit.").Append(component.GetType().Name).Append("\n\n对象绑定\n").Append(Current.binding);
  b.Append("\n\n中断与重入\n").Append(design.cleanup).Append("\n\n由宿主确定业务结果；重播只重播表现。验证快速连续触发、暂停恢复、关闭和销毁后的姿态与清理。交付真实接线与可调参数。");return b.ToString();
 }
 void UpdatePrompt(){if(promptLabel==null)return;string value=BuildPrompt();if(promptLabel.text!=value)promptLabel.text=value;promptLabel.rectTransform.sizeDelta=new Vector2(322,Mathf.Max(600,promptLabel.preferredHeight+15));detailsContent.sizeDelta=new Vector2(329,promptLabel.rectTransform.sizeDelta.y+20);}
 public void CopyPrompt(){GUIUtility.systemCopyBuffer=BuildPrompt();promptCopiedUntil=Time.unscaledTime+3;Report("已复制本项动画 Prompt 和当前参数，可粘贴到新项目的对话中。");}
}
}
