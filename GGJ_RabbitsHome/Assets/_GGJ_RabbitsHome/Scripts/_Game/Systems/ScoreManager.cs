using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QuasarGames.Utils;

namespace QuasarGames
{

    public class ScoreManager : MonoBehaviour
    {

        public static ScoreManager Instance;

        public int coins
        {
            get
            {
                return _coins;
            }
            set
            {
                if (_coins != value)
                {
                    _coins = Mathf.Max(0, value);

                    PrefsCenter.Coins = _coins;

                    UpdateAllStats();
                }
            }
        }
        [SerializeField]
        private int _coins;

        [Space(20)]
        [Header("LEVEL SELECT MENU")]
        public TextMeshProUGUI coinsTextLevelSelectMenu;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            ResetAllStats();
        }

        public void UpdateValues()
        {
            //
        }

        public void ResetAllStats()
        {
            // COINS
            coins = PrefsCenter.Coins;

            UpdateAllStats();
        }

        public void UpdateAllStats()
        {
            UpdateValues();

            //COINS
            //coinsTextLevelSelectMenu.GetComponent<UI_CurveNumberChanger>().SetNumber(_coins);

        }


        // HIGH SCORE

        public void TrySetBestTime(string levelID, float value)
        {
            if (
                (value > 0f)
                && (value < PrefsCenter.GetLevelBestTime(levelID))
               )
            {
                PrefsCenter.SetLevelBestTime(levelID, value);
            }

            UpdateAllStats();
        }


        // COINS

        public void ClearCoins()
        {
            PrefsCenter.Coins = 0;

            ResetAllStats();
        }

        public void AddCoins(int value = 1)
        {
            coins += value;
        }


        // SPEND COINS

        public bool SpendCoins(int value)
        {
            bool success = false;
            if (CanSpend(value))
            {
                coins -= value;
                success = true;
            }
            else
                Quasarlog.Log("Cannot spend so much!");

            return success;
        }

        public bool CanSpend(int value)
        {
            return (value <= _coins);
        }


    }
}