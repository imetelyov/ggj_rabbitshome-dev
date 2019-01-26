using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuasarGames
{

    public class SceneChanger : MonoBehaviour
    {

        public static SceneChanger Instance;

        [SerializeField]
        private string mainSceneName;

        [SerializeField]
        private Scene mainScene;

        [SerializeField]
        private string currentSceneName;

        [SerializeField]
        private Scene currentScene;

        [SerializeField]
        private bool sceneIsLoaded;

        [SerializeField]
        private bool sceneIsUnloaded;

        public GameObject mainMenuCamera;
        public GameObject mainMenuEventSystem;

        void Awake()
        {
            Instance = this;

            mainScene = SceneManager.GetActiveScene();
            mainSceneName = mainScene.name;

        }


        // SCENES

        public void LoadScene(string newSceneName = "Gameplay")
        {
            currentSceneName = newSceneName;

            SceneManager.sceneLoaded += OnSceneLoaded;

            StartCoroutine(LoadSceneCoroutine());
        }

        private IEnumerator LoadSceneCoroutine()
        {
            sceneIsLoaded = false;

            SceneManager.LoadScene(currentSceneName, LoadSceneMode.Additive);

            while (!sceneIsLoaded)
            {
                yield return null;
            }

            SceneManager.sceneLoaded -= OnSceneLoaded;

            if (currentScene.IsValid())
            {
                SceneManager.SetActiveScene(currentScene);
            }

            //// Smooth gameplay load
            //GameplayCanvasFullscreenEffects.Instance.FadeInOut(true);

            //GameplayStateParametersManager.Instance.SetTrigger("Start");


            mainMenuCamera.SetActive(false);
            mainMenuEventSystem.SetActive(false);

            yield return null;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            sceneIsLoaded = true;

            currentScene = scene;
        }

        public void UnloadScene()
        {
            if (sceneIsLoaded)
            {
                SceneManager.sceneUnloaded += OnSceneUnloaded;

                StartCoroutine(UnloadSceneCoroutine());
            }
        }

        private IEnumerator UnloadSceneCoroutine()
        {
            SceneManager.SetActiveScene(mainScene);

            mainMenuCamera.SetActive(true);
            mainMenuEventSystem.SetActive(true);

            sceneIsUnloaded = false;

            // Smooth gameplay load
            CanvasFullscreenEffects.Instance.FadeOut();

            SceneManager.UnloadSceneAsync(currentScene);

            while (!sceneIsUnloaded)
            {
                yield return null;
            }

            SceneManager.sceneUnloaded -= OnSceneUnloaded;


            GameStateParametersManager.Instance.SetTrigger("Start");

            yield return null;
        }

        private void OnSceneUnloaded(Scene scene)
        {
            sceneIsUnloaded = true;
            sceneIsLoaded = false;
        }


    }
}