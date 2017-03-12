using UnityEngine;
using UnityEditor;

namespace AcquisLib.AcquisEditor
{
    public class AcSceneCreater : EditorWindow
    {
        private string sceneName = "";
        private string sceneFolder = "";

        static public void ShowWindow()
        {
            AcSceneCreater window = GetWindow<AcSceneCreater>();
            window.titleContent = new GUIContent("Scene Creater");
            window.minSize = new Vector2(350, 100);
            window.maxSize = new Vector2(350, 100);
        }

        void OnGUI()
        {
            sceneName   = EditorGUI.TextField(new Rect(10, 10, 300, 17), "Scene name", sceneName);
            sceneFolder = EditorGUI.TextField(new Rect(10, 30, 300, 17), "Folder name", sceneFolder);

            if (GUI.Button(new Rect(10, 50, 300, 17), "Create"))
            {
                if (sceneName.Equals("")) {
                    Debug.Log("씬 이름이 없습니다.");
                    return;
                }

                if (sceneFolder.Equals(""))
                {
                    System.IO.File.Create(Application.dataPath + "/Scenes/" + sceneName + ".unity");
                }
                else
                {
                    string TempAssets = Application.dataPath + "/" + sceneFolder + "/";
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(TempAssets);

                    if (!di.Exists) di.Create();

                    if (System.IO.File.Exists(Application.dataPath + "/" + sceneFolder + "/" + sceneName + ".unity"))
                        Debug.Log("Same Scene");
                    else
                        System.IO.File.Create(Application.dataPath + "/" + sceneFolder + "/" + sceneName + ".unity");
                }

                EditorApplication.update();
                AssetDatabase.Refresh();
            }
        }
    }
}