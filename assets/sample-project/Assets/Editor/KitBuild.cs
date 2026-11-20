using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Build.Reporting;
using ProjectTweenKit;
public static class KitBuild {
 static string Repo{get{string candidate=Path.GetFullPath(Path.Combine(Application.dataPath,"../../.."));return Directory.Exists(Path.Combine(candidate,"assets/unity-kit"))?candidate:Path.GetFullPath(Path.Combine(Application.dataPath,".."));}}
 [MenuItem("Tween Kit/Generate examples and build Windows")]
 public static void Build(){
  PlayerSettings.companyName="ProjectTweenKit";PlayerSettings.productName="BounceBusters Tween Gallery";PlayerSettings.bundleVersion="0.2.0";PlayerSettings.defaultScreenWidth=1600;PlayerSettings.defaultScreenHeight=1000;PlayerSettings.fullScreenMode=FullScreenMode.Windowed;PlayerSettings.resizableWindow=true;PlayerSettings.runInBackground=true;PlayerSettings.SetScriptingBackend(UnityEditor.Build.NamedBuildTarget.Standalone,ScriptingImplementation.Mono2x);
  var scene=EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,NewSceneMode.Single);new GameObject("Tween Gallery").AddComponent<TweenGallery.Gallery>();Directory.CreateDirectory("Assets/Scenes");EditorSceneManager.SaveScene(scene,"Assets/Scenes/Gallery.unity");
  GeneratePrefabs();GeneratePresets();AssetDatabase.SaveAssets();AssetDatabase.Refresh();
  string dist=Path.Combine(Repo,"dist");Directory.CreateDirectory(dist);AssetDatabase.ExportPackage("Assets/ProjectTweenKit",Path.Combine(dist,"ProjectTweenKit.unitypackage"),ExportPackageOptions.Recurse);
  var report=BuildPipeline.BuildPlayer(new BuildPlayerOptions{scenes=new[]{"Assets/Scenes/Gallery.unity"},locationPathName=Path.Combine(dist,"Windows/TweenGallery.exe"),target=BuildTarget.StandaloneWindows64,options=BuildOptions.None});
  File.WriteAllText(Path.Combine(dist,"build-result.json"),JsonUtility.ToJson(new Result{result=report.summary.result.ToString(),errors=report.summary.totalErrors,warnings=report.summary.totalWarnings,size=report.summary.totalSize.ToString()},true));
  if(report.summary.result!=BuildResult.Succeeded)throw new Exception("Windows build failed");
 }
 [Serializable]class Result {public string result,size;public int errors,warnings;}
 public static void ImportOnly(){AssetDatabase.Refresh();Debug.Log("KIT_CLEAN_IMPORT_OK");}
 static void GeneratePrefabs(){
  const string dir="Assets/ProjectTweenKit/Examples";Directory.CreateDirectory(dir);
  foreach(ExamplePrefab.Kind kind in Enum.GetValues(typeof(ExamplePrefab.Kind))){var go=new GameObject(kind.ToString(),typeof(RectTransform));go.AddComponent<ExamplePrefab>().Example=kind;Save(go,dir+"/"+kind+".prefab");}
 }
 static void GeneratePresets(){string dir="Assets/ProjectTweenKit/Presets";Directory.CreateDirectory(dir);foreach(var type in new[]{typeof(UIBounce),typeof(UIFloat),typeof(ModalMotion),typeof(NumberRoll),typeof(RewardFlight),typeof(JellyHit),typeof(FrameSequence),typeof(HomeWordmarkWiggle)}){var go=new GameObject("Preset capture",typeof(RectTransform));var c=(MonoBehaviour)go.AddComponent(type);MotionPreset.Capture(c).Save(Path.Combine(dir,type.Name+".json"));UnityEngine.Object.DestroyImmediate(go);}}
 static void Save(GameObject go,string path){if(!File.Exists(path+".meta")){using(var md5=System.Security.Cryptography.MD5.Create()){string guid=BitConverter.ToString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes("unity-project-tween-kit/v1/Examples/"+Path.GetFileName(path)))).Replace("-","").ToLowerInvariant();File.WriteAllText(path+".meta","fileFormatVersion: 2\nguid: "+guid+"\nPrefabImporter:\n  externalObjects: {}\n  userData: \n  assetBundleName: \n  assetBundleVariant: \n");}}PrefabUtility.SaveAsPrefabAsset(go,path);UnityEngine.Object.DestroyImmediate(go);}
}
