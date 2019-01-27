using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using QuasarGames.Utils;

namespace QuasarGames
{

    public enum UI_State
    {
        MenuMain,
        MenuSettings,
        MenuAbout,
        MenuGameOver,
        GameplayMain
    }


    public class UI_Manager : MonoBehaviour
    {

        // SINGLETON
        public static UI_Manager Instance;

        public UI_State currentState;

        public GameObject menuMain;
        public GameObject menuSettings;
        public GameObject menuGameOver;
        public GameObject menuAbout;

        void Awake()
        {
            Instance = this;
        }

        public void ChangeUIByGameMode()
        {

        }

        public void ChangeActiveUI(string stateName)
        {
            List<GameObject> ui_all = new List<GameObject>();

            ui_all.Add(menuMain);
            ui_all.Add(menuSettings);
            ui_all.Add(menuGameOver);
            ui_all.Add(menuAbout);

            List<GameObject> ui_active = new List<GameObject>();

            // choose active ui elements
            switch (stateName)
            {
                case "MenuMain":
                    currentState = UI_State.MenuMain;

                    ui_active.Add(menuMain);

                    TurnUIOnOff(ui_all, ui_active);

                    GameManager.Instance.ChangeState(GameState.MENU);

                    break;

                case "MenuSettings":
                    currentState = UI_State.MenuSettings;

                    ui_active.Add(menuSettings);

                    TurnUIOnOff(ui_all, ui_active);

                    GameManager.Instance.ChangeState(GameState.MENU);

                    break;

                case "MenuGameOver":
                    currentState = UI_State.MenuGameOver;

                    ui_active.Add(menuGameOver);

                    TurnUIOnOff(ui_all, ui_active);

                    GameManager.Instance.ChangeState(GameState.MENU);

                    break;

                case "MenuAbout":
                    currentState = UI_State.MenuAbout;

                    ui_active.Add(menuAbout);

                    TurnUIOnOff(ui_all, ui_active);

                    GameManager.Instance.ChangeState(GameState.MENU);

                    break;

                case "GameplayMain":

                    currentState = UI_State.GameplayMain;

                    TurnUIOnOff(ui_all, ui_active);

                    GameManager.Instance.StartANewGame();

                    break;

                default:
                    break;
            }

            ButtonToggleAudio[] buttonsAudio = FindObjectsOfType<ButtonToggleAudio>();

            foreach (ButtonToggleAudio bttn in buttonsAudio)
            {
                if (bttn.gameObject.activeInHierarchy)
                    bttn.Refresh();
            }

        }

        void TurnUIOnOff(List<GameObject> allUI, List<GameObject> activeUI)
        {
            foreach (GameObject ui in allUI)
            {
                bool shouldBeActive = activeUI.Contains(ui);

                bool reactivate = shouldBeActive && !ui.activeInHierarchy;

                ui.SetActive(shouldBeActive);

                if (reactivate)
                {
                    Animator anim = ui.GetComponent<Animator>();
                    if (anim != null)
                    {
                        anim.SetTrigger("Reset");
                    }

                }

            }
        }

        void BackButtonAction()
        {
            switch (currentState)
            {
                case UI_State.MenuMain:
                    GameStateParametersManager.Instance.SetTrigger("ShowQuit");
                    break;

                case UI_State.MenuSettings:
                    GameStateParametersManager.Instance.SetTrigger("HideSettings");
                    break;

                case UI_State.MenuAbout:
                    GameStateParametersManager.Instance.SetTrigger("HideAbout");
                    break;


                case UI_State.GameplayMain:
                    GameStateParametersManager.Instance.SetTrigger("ShowPause");
                    break;

                case UI_State.MenuGameOver:
                    //GameStateParametersManager.Instance.SetBoolTrue("Bool_ToMainMenu");
                    break;

                default:
                    break;
            }

        }

        public void ReactOnButton(string buttonName)
        {
            switch (buttonName)
            {
                case "ESCAPE":
                    BackButtonAction();
                    break;
                default:
                    Quasarlog.Log("Button action not found!");
                    break;
            }

        }


    }
}