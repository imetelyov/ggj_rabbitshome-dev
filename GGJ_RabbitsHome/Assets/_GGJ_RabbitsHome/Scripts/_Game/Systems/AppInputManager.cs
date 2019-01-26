using UnityEngine;
using System.Collections;
using System;

namespace QuasarGames
{
    public class AppInputManager : MonoBehaviour
    {
        void Update()
        {
            if (GameManager.Instance.GetCurrentGameState() == GameState.MENU)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    UI_Manager.Instance.ReactOnButton("ESCAPE");
                }
            }
        }
    }
}

