using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

namespace AcquisLib.Editor
{
    public class AcSingletonCreater : EditorWindow
    {
        private string SingletonName = "";
        private string ForderName = "";
        private bool IsDontDestroyObject = false;

        static public void ShowWindow()
        {
            AcSingletonCreater window = GetWindow<AcSingletonCreater>();
            window.titleContent = new GUIContent("Singleton Creater");
            window.minSize = new Vector2(320, 100);
            window.maxSize = new Vector2(320, 100);
        }

        void OnGUI()
        {
            SingletonName       = EditorGUI.TextField(new Rect(10, 10, 300, 17), "Singleton Name", SingletonName);
            ForderName          = EditorGUI.TextField(new Rect(10, 30, 300, 17), "Forder Name", ForderName);
            IsDontDestroyObject = EditorGUI.Toggle(new Rect(10, 50, 300, 17), "DontDestroyObject", IsDontDestroyObject);

            if (GUI.Button(new Rect(10, 70, 300, 17), "Create"))
            {
                if (SingletonName.Equals(""))
                {
                    Debug.Log("ERROR : NO SINGLETON NAME");
                    return;
                }

                FileStream saveStream;
                if (ForderName.Equals(""))
                {
                    saveStream = new FileStream(Application.dataPath + "/" + SingletonName + ".cs", FileMode.Create);
                }
                else
                {
                    string TempAssets = Application.dataPath + "/" + ForderName + "/";
                    DirectoryInfo di = new DirectoryInfo(TempAssets);
                    if (!di.Exists) di.Create();
                    saveStream = new FileStream(Application.dataPath + "/" + ForderName + "/" + SingletonName + ".cs", FileMode.Create);
                }

                StreamWriter sw = new StreamWriter(saveStream, Encoding.UTF8);
                #region SingletonScript
                string[] SingletonScripts =
                {
                    "using UnityEngine;",
                    "using System.Collections.Generic;",
                    "using AcquisLib;",
                    "",
                    "public class " + SingletonName,
                    "    : AcSingleton<" + SingletonName + "> {",
                    "}",
                };

                string[] SingletonScriptsDontDistroy =
                {
                    "using UnityEngine;",
                    "using System.Collections.Generic;",
                    "using AcquisLib;",
                    "",
                    "public class " + SingletonName,
                    "    : AcSingleton<" + SingletonName + "> {",
                    "{",
                    "    protected override void init()",
                    "    {",
                    "        DontDestroyOnLoad(this.gameObject);",
                    "    }",
                    "}",
                };
                #endregion

                if (IsDontDestroyObject)
                {
                    for (int i = 0; i < SingletonScriptsDontDistroy.Length; i++)
                    {
                        sw.WriteLine(SingletonScriptsDontDistroy[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < SingletonScripts.Length; i++)
                    {
                        sw.WriteLine(SingletonScripts[i]);
                    }
                }
                sw.Close();
                saveStream.Close();
            }

            EditorApplication.update();
            AssetDatabase.Refresh();
        }
    }
}