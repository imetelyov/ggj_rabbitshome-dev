using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuasarGames
{

    public class UI_CurveNumberChanger : MonoBehaviour
    {

        public Text numberText;
        public TextMeshPro numberText_TMPro;
        public TextMeshProUGUI numberText_TMPro_UI;

        public bool tmproMode = false;
        public bool tmproModeUI = false;

        [Range(0.01f, 3.0f)]
        public float animationTime = 0.3f;

        // one second normalized curve
        public AnimationCurve valueCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private float startNumber;
        private float currentNumber;
        private float goalNumber;

        private int currentNumberCashedInt;

        private float elapsedTime = 0f;

        private bool isChanging = false;

        private bool isReady = false;

        void Awake()
        {
            if (!isReady)
            {
                GetReady();
            }
        }

        private void GetReady()
        {
            if (numberText == null)
                numberText = GetComponent<Text>();

            if (numberText_TMPro == null)
                numberText_TMPro = GetComponent<TextMeshPro>();

            if (numberText_TMPro_UI == null)
                numberText_TMPro_UI = GetComponent<TextMeshProUGUI>();

            if (numberText_TMPro != null || numberText_TMPro_UI != null)
            {
                tmproMode = true;

                if (numberText_TMPro_UI != null)
                    tmproModeUI = true;
            }

            isReady = true;

        }



        public void Reset()
        {
            if (gameObject.activeInHierarchy)
            {
                startNumber = 0f;
                currentNumber = 0f;
                goalNumber = 0f;

                currentNumberCashedInt = 0;

                isChanging = false;
                elapsedTime = 0f;

                currentNumberCashedInt = (int)currentNumber;

                UpdateText();

            }
        }

        public void SetNumberInstantly(float value)
        {
            startNumber = value;
            currentNumber = value;
            goalNumber = value;

            isChanging = false;
            elapsedTime = 0f;

            currentNumberCashedInt = (int)currentNumber;

            UpdateText();
        }

        public void SetNumber(float value)
        {
            if (isChanging)
            {
                currentNumber = goalNumber;
                startNumber = currentNumber;
                goalNumber = value;

                isChanging = true;
                elapsedTime = 0f;
            }
            else
            {
                startNumber = currentNumber;
                goalNumber = value;

                isChanging = true;
                elapsedTime = 0f;
            }

            currentNumberCashedInt = (int)currentNumber;

            UpdateText();
        }

        public void AddToNumber(float value)
        {
            if (isChanging)
            {
                currentNumber = goalNumber;
                startNumber = currentNumber;
                goalNumber = startNumber + value;

                isChanging = true;
                elapsedTime = 0f;
            }
            else
            {
                startNumber = currentNumber;
                goalNumber = startNumber + value;

                isChanging = true;
                elapsedTime = 0f;
            }

            currentNumberCashedInt = (int)currentNumber;

            UpdateText();
        }

        void Update()
        {
            if (isChanging)
            {
                if (currentNumber != goalNumber && (animationTime >= 0.01f) && (elapsedTime <= animationTime))
                {
                    currentNumber = (goalNumber - startNumber) * valueCurve.Evaluate(elapsedTime / animationTime) + startNumber;

                    elapsedTime += Time.deltaTime;

                    if (Mathf.Abs(goalNumber - currentNumber) <= 0.01f)
                    {
                        currentNumber = goalNumber;
                    }
                }
                else
                {
                    currentNumber = goalNumber;
                    isChanging = false;
                }

                int currentNumberInt = (int)currentNumber;

                if (currentNumberInt != currentNumberCashedInt)
                {
                    currentNumberCashedInt = currentNumberInt;

                    UpdateText();
                }
            }
        }

        private void UpdateText()
        {
            if (!isReady)
            {
                GetReady();
            }


            if (tmproMode)
            {
                if (tmproModeUI)
                    numberText_TMPro_UI.text = currentNumberCashedInt.ToString();
                else
                    numberText_TMPro.text = currentNumberCashedInt.ToString();
            }
            else
            {
                numberText.text = currentNumberCashedInt.ToString();
            }
        }

    }
}