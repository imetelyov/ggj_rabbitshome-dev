using UnityEngine;
using System.Collections;

namespace QuasarGames
{

    public class GameStateParametersManager : MonoBehaviour
    {

        public static GameStateParametersManager Instance;

        public Animator gameStateAnimator;

        void Awake()
        {
            Instance = this;
        }

        public void SetBool(string name, bool value)
        {
            gameStateAnimator.SetBool(name, value);
        }

        public void SetBoolTrue(string name)
        {
            SetBool(name, true);
        }

        public void SetIntegerDefault(string name)
        {
            gameStateAnimator.SetInteger(name, 0);
        }

        public void SetInteger1(string name)
        {
            gameStateAnimator.SetInteger(name, 1);
        }

        public void SetInteger2(string name)
        {
            gameStateAnimator.SetInteger(name, 2);
        }

        public void SetInteger3(string name)
        {
            gameStateAnimator.SetInteger(name, 3);
        }

        public void SetTrigger(string name)
        {
            gameStateAnimator.SetTrigger(name);
        }
    }
}