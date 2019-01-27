using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using QuasarGames.Utils;
using DG.Tweening;

namespace QuasarGames
{

    public enum GameState
    {
        MENU,
        GAMEPLAY,
        GAMEOVER
    }

    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;

        public Camera mainCamera;

        [Space(20)]
        public GameState gameState;


        void Awake()
        {
            Instance = this;

            if (mainCamera == null)
                mainCamera = Camera.main;

        }

        void Start()
        {
            Initialize();
        }


        private void Initialize()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            // DOTween
            DOTween.Init();

            AudioManager.Instance.SetMasterVolume();
        }

        public void ChangeState(GameState newState)
        {
            gameState = newState;

            ScoreManager.Instance.UpdateAllStats();

            if (GetCurrentGameState() == GameState.GAMEPLAY)
            {
                BirthController.Instance.DeployBaby(true);

                ScoreManager.Instance.ClearBabies();
                ScoreManager.Instance.StartTimer();
            }
            if (GetCurrentGameState() == GameState.GAMEOVER)
            {
                BirthController.Instance.StopTimer();

                GameStateParametersManager.Instance.SetTrigger("ShowGameOver");
            }

        }

        public GameState GetCurrentGameState()
        {
            return gameState;
        }

        public void StartANewGame()
        {
            ChangeState(GameState.GAMEPLAY);

            //SceneChanger.Instance.LoadScene("Gameplay");

        }

        public void PauseGame()
        {
            //GameplayManager.Instance.PauseGame();
        }

        public void UnPauseGame()
        {
            //GameplayManager.Instance.UnPauseGame();

        }

        public void RemovePlayerPrefs()
        {
            // TODO
            PrefsCenter.ClearPrefs();
        }


    }
}
