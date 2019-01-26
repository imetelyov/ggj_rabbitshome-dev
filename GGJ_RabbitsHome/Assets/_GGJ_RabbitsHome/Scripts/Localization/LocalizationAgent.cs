using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuasarGames
{

    public class LocalizationAgent : MonoBehaviour
    {
        public LocalizationType localizationType = LocalizationType.TEXT;

        [SerializeField]
        private string _key;
        public string key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
                needToLocalize = true;
                Localize();
            }
        }

        [SerializeField]
        private Language _currentLanguage = Language.DEFAULT;
        public Language currentLanguage
        {
            get
            {
                return _currentLanguage;
            }
        }

        public bool needToLocalize = true;

        public string localizedText;

        // TEXT
        private Text textComponent;
        private TextMeshProUGUI textMPComponent;

        // IMAGE
        private Image imageComponent;

        [SerializeField]
        private Dictionary<string, string> stringTemplate = new Dictionary<string, string>(); // for params key-value

        void Awake()
        {
            switch (localizationType)
            {
                case LocalizationType.TEXT:
                    if (textMPComponent == null)
                        textMPComponent = GetComponent<TextMeshProUGUI>();
                    if (textMPComponent == null)
                        textComponent = GetComponent<Text>();
                    break;

                case LocalizationType.IMAGE:
                    if (imageComponent == null)
                        imageComponent = GetComponent<Image>();
                    break;

                default:
                    break;
            }

        }

        void Start()
        {
            LocalizationManager.Instance.AddToAgentsPool(this);

            Localize();
        }

        private void Update()
        {
            if (needToLocalize)
            {
                Localize();
            }
        }

        public void SetTemplate(string newKey, string newValue)
        {
            if (stringTemplate.ContainsKey(newKey))
            {
                stringTemplate[newKey] = newValue;
            }
            else
            {
                stringTemplate.Add(newKey, newValue);
            }

            needToLocalize = true;

            Localize();
        }

        private string ApplyTemplate(string inString)
        {
            string outString = inString;

            foreach (KeyValuePair<string, string> keyPair in stringTemplate)
            {
                outString = outString.Replace(keyPair.Key, keyPair.Value);
            }


            return outString;
        }

        public void Localize()
        {
            if (this.gameObject.activeInHierarchy)
            {
                StartCoroutine(LocalizeCoroutine());
                needToLocalize = false;
            }
            else
            {
                needToLocalize = true;
            }

        }

        public IEnumerator LocalizeCoroutine()
        {
            while (!LocalizationManager.Instance.GetIsReady())
            {
                yield return null;
            }

            if (_currentLanguage != LocalizationManager.Instance.currentLanguage || needToLocalize)

            {
                _currentLanguage = LocalizationManager.Instance.currentLanguage;

                switch (localizationType)
                {
                    case LocalizationType.TEXT:

                        string locString = LocalizationManager.Instance.GetLocalizedTextValue(_key);

                        if (locString != "")
                        {
                            // APPLY TEMPLATE IF EXIST
                            locString = ApplyTemplate(locString);

                            if (textMPComponent != null)
                                textMPComponent.text = locString;
                            else if (textComponent != null)
                                textComponent.text = locString;
                        }

                        localizedText = locString;

                        break;

                    case LocalizationType.IMAGE:
                        if (imageComponent != null)
                            imageComponent.sprite = LocalizationManager.Instance.GetLocalizedImageValue(_key);
                        break;

                    default:
                        break;
                }
            }

        }

    }
}