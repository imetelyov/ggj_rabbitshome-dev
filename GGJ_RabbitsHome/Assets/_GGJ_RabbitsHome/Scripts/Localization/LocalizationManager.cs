using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace QuasarGames
{

    public enum Language
    {
        DEFAULT,
        ENGLISH,
        GERMAN,
        RUSSIAN
    }

    public enum LocalizationType
    {
        TEXT,
        IMAGE,
        AUDIO
    }


    // TODO

    //[System.Serializable]
    //public class LibraryAudio
    //{
    //    public string key;

    //    public AudioClip lAudio;
    //}

    [System.Serializable]
    public class LocalizationTextData
    {
        public LocalizationTextItem[] items;
    }

    [System.Serializable]
    public class LocalizationTextItem
    {
        public string key;
        [TextArea]
        public string value;
    }

    [System.Serializable]
    public class LocalizationImageData
    {
        public LocalizationImageItem[] items;
    }

    [System.Serializable]
    public class LocalizationImageItem
    {
        public string key;
        public Sprite value;
    }


    public class LocalizationManager : MonoBehaviour
    {

        public static LocalizationManager Instance;

        public Language currentLanguage;

        public Language[] languageSequence;

        [SerializeField]
        private List<LocalizationAgent> localizationAgentsPool = new List<LocalizationAgent>();


        // TEXT
        [Space(20)]
        [Header("TEXT")]
        public string missingTextString = "Text not found";

        private Dictionary<string, string> localizedText;

        // IMAGE
        [Space(20)]
        [Header("IMAGE")]
        public Sprite missingSprite;

        private Dictionary<string, Sprite> localizedImages;


        private bool isReady = false;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            isReady = false;

        }

        // Use this for initialization
        void Start()
        {

            currentLanguage = (Language)PrefsCenter.Language;

            if (currentLanguage == Language.DEFAULT)
            {
                SystemLanguage sysLang = Application.systemLanguage;

                switch (sysLang)
                {
                    case SystemLanguage.Russian:
                        currentLanguage = Language.RUSSIAN;
                        break;
                    case SystemLanguage.Belarusian:
                        currentLanguage = Language.RUSSIAN;
                        break;
                    case SystemLanguage.Ukrainian:
                        currentLanguage = Language.RUSSIAN;
                        break;
                    case SystemLanguage.English:
                        currentLanguage = Language.ENGLISH;
                        break;
                    //case SystemLanguage.German:
                    //    currentLanguage = Language.GERMAN;
                    //    break;
                    case SystemLanguage.French:
                        currentLanguage = Language.ENGLISH;
                        break;
                    default:
                        currentLanguage = Language.ENGLISH;
                        break;
                }

                PrefsCenter.Language = (int)currentLanguage;
            }

            ChangeLanguage(currentLanguage);
        }

        public bool GetIsReady()
        {
            return isReady;
        }

        void ChangeLanguage(Language newLanguage)
        {
            isReady = false;

            currentLanguage = newLanguage;
            PrefsCenter.Language = (int)currentLanguage;

            LoadLocalizedText("localizedText_" + currentLanguage.ToString() + ".json");

            ApplyLocalization();
        }

        public void NextLanguage()
        {
            int langID = 0;

            for (int i = 0; i < languageSequence.Length; i++)
            {
                if (currentLanguage == languageSequence[i])
                    langID = i;
            }

            langID += 1;

            if (languageSequence.Length > langID)
                ChangeLanguage(languageSequence[langID]);
            else
                ChangeLanguage(languageSequence[0]);

        }

        void LoadLocalizedText(string fileName)
        {
            localizedText = new Dictionary<string, string>();

            string filePath = "";

            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WebGLPlayer)
            {
                StartCoroutine(GetLocalizedTextWWWCoroutine(fileName));

                return;
            }
            else
            {
                // iOS or Standalone
                filePath = Path.Combine(Application.streamingAssetsPath, fileName);
                if (File.Exists(filePath))

                {
                    string dataAsJson = File.ReadAllText(filePath);

                    LoadLocalizationData(dataAsJson);

                }
                else
                {
                    Debug.LogError("Cannot find file!");
                }
            }


            isReady = true;
        }

        private IEnumerator GetLocalizedTextWWWCoroutine(string fileName)
        {
            // WebGL or Android
            // TODO fix
            string oriPath = Path.Combine(Application.streamingAssetsPath, fileName);

            // WebGL only use WWW to read file
            WWW reader = new WWW(oriPath);
            yield return reader;

            string dataAsJson = reader.text;

            LoadLocalizationData(dataAsJson);

            yield return null;
        }

        private void LoadLocalizationData(string dataAsJson)
        {
            LocalizationTextData loadedData = JsonUtility.FromJson<LocalizationTextData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");

            isReady = true;
        }

        void ApplyLocalization()
        {

            localizationAgentsPool.RemoveAll(a => a == null);

            for (int i = 0; i < localizationAgentsPool.Count; i++)
            {
                LocalizationAgent locAgent = localizationAgentsPool[i];

                if (locAgent.currentLanguage != currentLanguage)
                {
                    locAgent.Localize();
                }
            }

        }

        public void AddToAgentsPool(LocalizationAgent newAgent)
        {
            if (!localizationAgentsPool.Contains(newAgent))
                localizationAgentsPool.Add(newAgent);
        }

        public string GetLocalizedTextValue(string key)
        {
            string result = missingTextString;
            if (localizedText.ContainsKey(key))
            {
                result = localizedText[key];
            }
            else
            {
                result = key;
            }

            return result;
        }

        public Sprite GetLocalizedImageValue(string key)
        {
            Sprite result = missingSprite;
            if (localizedImages.ContainsKey(key))
            {
                result = localizedImages[key];
            }

            return result;
        }

    }
}