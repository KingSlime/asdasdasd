using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using AcquisLib;
using AcquisLib.Editor;

namespace AcquisLib.AcquisEditor
{
    public static class AcMenuTap
    {
        [MenuItem("AcquisLib/Create/Singleton %&s")]
        public static void Acquis_Create_Singleton()
        {
            AcSingletonCreater.ShowWindow();
        }

        [MenuItem("AcquisLib/Create/Scene %&c")]
        public static void Acquis_Create_Scene()
        {
            AcSceneCreater.ShowWindow();
        }
    }
}