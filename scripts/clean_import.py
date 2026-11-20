"""Run a genuinely separate Unity project, importing only the exported unitypackage."""
from pathlib import Path
import argparse,subprocess,json,shutil,uuid,time
from inventory import KIT,write,sha

CS=r'''
using System;using System.IO;using System.Linq;using UnityEngine;using UnityEngine.UI;using UnityEditor;using UnityEditor.SceneManagement;using UnityEditor.Build.Reporting;
public static class CleanImport {
 [Serializable]class Report {public string result;public int scripts,prefabs,missingScripts;public string[] errors;}
 [InitializeOnLoadMethod]static void Resume(){if(SessionState.GetBool("KitImportRequested",false))EditorApplication.update+=WaitForImport;}
 public static void Import(){var args=Environment.GetCommandLineArgs();int at=Array.IndexOf(args,"--package");SessionState.SetBool("KitImportRequested",true);EditorApplication.update+=WaitForImport;AssetDatabase.ImportPackage(args[at+1],false);}
 static void WaitForImport(){if(EditorApplication.timeSinceStartup>180){Debug.LogError("Import timeout");EditorApplication.Exit(1);return;}if(EditorApplication.isCompiling||EditorApplication.isUpdating)return;if(AssetDatabase.LoadAssetAtPath<MonoScript>("Assets/ProjectTweenKit/Runtime/NumberRoll.cs")==null)return;SessionState.SetBool("KitImportRequested",false);EditorApplication.update-=WaitForImport;EditorApplication.Exit(0);}
 public static void Verify(){
  var r=new Report();var errors=new System.Collections.Generic.List<string>();
  if(Type.GetType("ProjectTweenKit.UIBounce, Assembly-CSharp")==null)errors.Add("UIBounce did not compile");
  if(Type.GetType("ProjectTweenKit.NumberRoll, Assembly-CSharp")==null)errors.Add("NumberRoll did not compile");
  if(Type.GetType("TweenGallery.Gallery, Assembly-CSharp")!=null)errors.Add("Gallery leaked into independent import");
  var canvas=new GameObject("Canvas",typeof(RectTransform),typeof(Canvas));
  foreach(string path in AssetDatabase.GetAllAssetPaths().Where(p=>p.StartsWith("Assets/ProjectTweenKit/")&&p.EndsWith(".cs")))r.scripts++;
  foreach(string path in AssetDatabase.GetAllAssetPaths().Where(p=>p.StartsWith("Assets/ProjectTweenKit/Examples/")&&p.EndsWith(".prefab"))){r.prefabs++;var prefab=AssetDatabase.LoadAssetAtPath<GameObject>(path);var go=(GameObject)PrefabUtility.InstantiatePrefab(prefab);go.transform.SetParent(canvas.transform,false);r.missingScripts+=GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);var c=go.GetComponents<MonoBehaviour>().FirstOrDefault(m=>m!=null&&m.GetType().FullName=="ProjectTweenKit.ExamplePrefab");if(c==null)errors.Add("Example bootstrap missing: "+path);else try{c.GetType().GetMethod("Build").Invoke(c,null);}catch(Exception e){errors.Add(path+": "+e.GetBaseException().Message);}foreach(var t in go.GetComponentsInChildren<Transform>(true))r.missingScripts+=GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject);UnityEngine.Object.DestroyImmediate(go);}
  if(r.prefabs<11)errors.Add("Missing example prefabs");foreach(string required in new[]{"LoadingProgress","StartupLoading"})if(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/ProjectTweenKit/Examples/"+required+".prefab")==null)errors.Add("Missing loading example: "+required);if(r.missingScripts>0)errors.Add("Prefab contains missing scripts");
  UnityEngine.Object.DestroyImmediate(canvas);r.errors=errors.ToArray();r.result=errors.Count==0?"passed":"failed";
  var args=Environment.GetCommandLineArgs();int at=Array.IndexOf(args,"--evidence");File.WriteAllText(args[at+1],JsonUtility.ToJson(r,true));if(errors.Count>0)throw new Exception(string.Join("\n",errors));
 }
}
'''
def main():
    ap=argparse.ArgumentParser();ap.add_argument('--unity',type=Path,default=Path('D:/Unity/Editor/Unity.exe'));a=ap.parse_args()
    project=KIT/'work/clean-import-runs'/uuid.uuid4().hex[:10];editor=project/'Assets/Editor';editor.mkdir(parents=True,exist_ok=True)
    (editor/'CleanImport.cs').write_text(CS,encoding='utf-8')
    write(project/'Packages/manifest.json',{'dependencies':{'com.unity.ugui':'2.0.0',**{'com.unity.modules.'+s:'1.0.0' for s in ['audio','ui','imgui','imageconversion','jsonserialize','physics','physics2d']}}})
    (project/'ProjectSettings').mkdir(exist_ok=True);shutil.copy2(KIT/'assets/sample-project/ProjectSettings/ProjectVersion.txt',project/'ProjectSettings/ProjectVersion.txt')
    first=[str(a.unity),'-batchmode','-nographics','-projectPath',str(project),'-executeMethod','CleanImport.Import','--package',str(KIT/'dist/ProjectTweenKit.unitypackage'),'-logFile',str(KIT/'work/clean-import-stage.log')]
    subprocess.run(first,check=True)
    cmd=[str(a.unity),'-batchmode','-nographics','-quit','-projectPath',str(project),'-executeMethod','CleanImport.Verify','--evidence',str(KIT/'work/clean-import-result.json'),'-logFile',str(KIT/'work/clean-import.log')]
    start=time.time();p=subprocess.run(cmd);report=KIT/'work/clean-import-result.json'
    if not report.exists() or report.stat().st_mtime<start:raise RuntimeError('Independent import produced no fresh verification report')
    shutil.copy2(report,KIT/'evidence/clean-import-result.json')
    write(KIT/'evidence/clean-import-run.json',{'project':str(project),'packageSha256':sha(KIT/'dist/ProjectTweenKit.unitypackage'),'resultSha256':sha(report),'exitCode':p.returncode,'verification':'New empty project; compile and instantiate all eleven composed prefabs including both loading examples in the Editor; no gallery/source Library references.'})
    print('Clean import exit:',p.returncode);raise SystemExit(p.returncode)
if __name__=='__main__':main()
