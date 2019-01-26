using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace QuasarGames
{
#if UNITY_EDITOR

    [CreateAssetMenu(fileName = "SceneSetup", menuName = "Data/SceneSetup")]
    public class SceneSetupData : ScriptableObject
    {

        [System.Serializable]
        public struct SetupData
        {
            public string name;
            public int index;
            public SceneSetup[] setupScenes;
        }

        public SetupData[] setupPresets;
    }
#endif
}