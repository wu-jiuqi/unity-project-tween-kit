using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectTweenKit {
[Serializable]public sealed class MotionPreset {
 public int schemaVersion=1;
 public string component;
 public List<Setting> settings=new List<Setting>();
 [Serializable]public sealed class Setting {public string field,type;public float value,x,y,z;}
 static bool IsSupported(Type t)=>t==typeof(float)||t==typeof(int)||t==typeof(bool)||t.IsEnum||t==typeof(Vector2)||t==typeof(Vector3);
 static IEnumerable<FieldInfo> Fields(MonoBehaviour target)=>target.GetType().GetFields(BindingFlags.Public|BindingFlags.Instance).Where(f=>!f.IsInitOnly&&IsSupported(f.FieldType));
 public static MotionPreset Capture(MonoBehaviour target){
  var p=new MotionPreset{component=target.GetType().FullName};
  foreach(var f in Fields(target)){
   var s=new Setting{field=f.Name,type=f.FieldType.FullName};object value=f.GetValue(target);
   if(value is Vector2 v){s.x=v.x;s.y=v.y;}else if(value is Vector3 w){s.x=w.x;s.y=w.y;s.z=w.z;}else s.value=Convert.ToSingle(value);
   p.settings.Add(s);
  }
  return p;
 }
 static bool Finite(float f)=>!float.IsNaN(f)&&!float.IsInfinity(f);
 public void Apply(MonoBehaviour target){
  if(schemaVersion!=1||target==null||target.GetType().FullName!=component)throw new InvalidDataException("Preset version or component does not match");
  var fs=Fields(target).ToDictionary(f=>f.Name);var pending=new List<Action>();
  foreach(var s in settings){
   if(!fs.TryGetValue(s.field,out var f)||!Finite(s.value)||!Finite(s.x)||!Finite(s.y)||!Finite(s.z)||(!string.IsNullOrEmpty(s.type)&&s.type!=f.FieldType.FullName))throw new InvalidDataException("Invalid preset field: "+s.field);
   if((f.Name.Contains("Duration")||f.Name.Contains("Delay")||f.Name.Contains("Interval"))&&s.value<0)throw new InvalidDataException("Negative time parameter: "+s.field);
   if(f.FieldType.IsEnum&&!Enum.IsDefined(f.FieldType,Mathf.RoundToInt(s.value)))throw new InvalidDataException("Invalid enum parameter: "+s.field);
   object value;
   if(f.FieldType==typeof(Vector2))value=new Vector2(s.x,s.y);
   else if(f.FieldType==typeof(Vector3))value=new Vector3(s.x,s.y,s.z);
   else if(f.FieldType.IsEnum)value=Enum.ToObject(f.FieldType,Mathf.RoundToInt(s.value));
   else if(f.FieldType==typeof(float))value=s.value;
   else if(f.FieldType==typeof(int))value=Mathf.RoundToInt(s.value);
   else value=s.value!=0;
   pending.Add(()=>f.SetValue(target,value));
  }
  foreach(var set in pending)set();
 }
 public void Save(string path){Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path)));File.WriteAllText(path,JsonUtility.ToJson(this,true));}
 public static MotionPreset Load(string path)=>JsonUtility.FromJson<MotionPreset>(File.ReadAllText(path));
}
}
