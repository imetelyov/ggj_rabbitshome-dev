using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Linq;

namespace QuasarGames
{

    public class SceneSetupLoader : MonoBehaviour
    {
        static SceneSetupData data;

        [InitializeOnLoadMethod]
        public static void Load()
        {
            data = (SceneSetupData)AssetDatabase.LoadAssetAtPath("Assets/_TenSecondRace/_Scenes/SceneSetups/SceneSetup.asset", typeof(SceneSetupData)); //+"Assets/Data/SceneSetup.asset"
        }

        [MenuItem("Scenes/Scene Setup 1 &1")]
        static void SceneSetup1()
        {
            Load(1);
        }

        [MenuItem("Scenes/Scene Setup 2 &2")]
        static void SceneSetup2()
        {
            Load(2);
        }

        [MenuItem("Scenes/Scene Setup 3 &3")]
        static void SceneSetup3()
        {
            Load(3);
        }

        [MenuItem("Scenes/Scene Setup 4 &4")]
        static void SceneSetup4()
        {
            Load(4);
        }

        [MenuItem("Scenes/Scene Setup 5 &5")]
        static void SceneSetup5()
        {
            Load(5);
        }

        [MenuItem("Scenes/Scene Setup 6 &6")]
        static void SceneSetup6()
        {
            Load(6);
        }

        static void Load(int index)
        {
            if (data == null)
            {
                Debug.LogError("Scene setup data not configured. Plase add it to 'Assets/_TenSecondRace/_Scenes/SceneSetups' folder");
            }
            else
            {
                var config = data.setupPresets.FirstOrDefault(d => d.index == index - 1);
                if (config.setupScenes == null)
                {
                    Debug.LogError(string.Format("Scene setup with index {0} not configured. Plase add it to 'Assets/_TenSecondRace/_Scenes/SceneSetups' folder", index));
                }
                else
                {
                    EditorUtility.DisplayProgressBar("Loading scene setup", "Please wait a bit, work in progress", 0);
                    EditorSceneManager.RestoreSceneManagerSetup(config.setupScenes);
                    //var hierarchy = EditorApplication.ExecuteMenuItem("Window/Hierarchy");
                    
                    EditorUtility.DisplayProgressBar("Loading scene setup", "Please wait a bit, work in progress", 1);
                    EditorUtility.ClearProgressBar();
                }
            }
            
        }
    }
}
