using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QuasarGames.Utils;

namespace QuasarGames
{

    public class ScoreManager : MonoBehaviour
    {

        public static ScoreManager Instance;

        public int babiesCount = 0;

        [Space(20)]
        [Header("UI")]
        public TextMeshProUGUI babiesTextUI;
        public TextMeshProUGUI babiesTextUI_GameOver;


        [Header("TIMER")]
        public float gameTime = 180f;
        public float gameTimer = 0f;
        public TextMeshProUGUI gameTimerUI;
        private bool isCounting = false;
        
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            ResetAllStats();
        }

        public void ResetAllStats()
        {
            // COINS
            babiesCount = 0;

            gameTimer = 0f;
            isCounting = false;

            UpdateAllStats();
        }

        public void UpdateAllStats()
        {
            babiesTextUI.GetComponent<TMPro.TextMeshProUGUI>().text = babiesCount.ToString();
            babiesTextUI_GameOver.GetComponent<TMPro.TextMeshProUGUI>().text = babiesCount.ToString();
            gameTimerUI.GetComponent<TMPro.TextMeshProUGUI>().text = ((int)(gameTime - gameTimer)).ToString() + " s";

        }




        public void ClearBabies()
        {
            ResetAllStats();
        }

        public void AddBabies(int value = 1)
        {
            babiesCount += value;
            UpdateAllStats();
        }

        public void StartTimer()
        {
            gameTimer = 0f;
            isCounting = true;
        }



        private void Update()
        {
            if (isCounting)
            {
                gameTimer += Time.deltaTime;

                if (gameTimer > gameTime)
                {
                    isCounting = false;
                    GameManager.Instance.ChangeState(GameState.GAMEOVER);
                }
            }

            UpdateAllStats();

        }


    }
}