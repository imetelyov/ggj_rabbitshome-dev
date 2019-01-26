using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace QuasarGames.Editor
{
    public class LocalizedTextEditor : EditorWindow
    {
        public LocalizationTextData localizationTextData;

        [MenuItem("QuasarGames/Localized Text Editor")]
        static void Init()
        {
            EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();
        }

        private void OnGUI()
        {
            if (localizationTextData != null)
            {
                SerializedObject serializedObject = new SerializedObject(this);
                SerializedProperty serializedProperty = serializedObject.FindProperty("localizationTextData");
                EditorGUILayout.PropertyField(serializedProperty, true);
                serializedObject.ApplyModifiedProperties();

                if (GUILayout.Button("Save data"))
                {
                    SaveGameData();
                }
            }

            if (GUILayout.Button("Load data"))
            {
                LoadGameData();
            }

            if (GUILayout.Button("Create new data"))
            {
                CreateNewData();
            }
        }

        private void LoadGameData()
        {
            string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");

            if (!string.IsNullOrEmpty(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);

                localizationTextData = JsonUtility.FromJson<LocalizationTextData>(dataAsJson);
            }
        }

        private void SaveGameData()
        {
            string filePath = EditorUtility.SaveFilePanel("Save localization data file", Application.streamingAssetsPath, "", "json");

            if (!string.IsNullOrEmpty(filePath))
            {
                string dataAsJson = JsonUtility.ToJson(localizationTextData);
                File.WriteAllText(filePath, dataAsJson);
            }
        }

        private void CreateNewData()
        {
            localizationTextData = new LocalizationTextData();
        }

    }
}