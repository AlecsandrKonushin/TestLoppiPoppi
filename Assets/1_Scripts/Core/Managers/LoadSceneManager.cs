using System.Collections;
using Controllers.Core;
using Core.Main;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Managers
{
    public class LoadSceneManager : Singleton<LoadSceneManager>
    {
        [SerializeField] private GameObject loaderCanvas;

        private AsyncOperation operation;
        private TypeScene currentScene = TypeScene.Init;

        public void LoadGameScene()
        {
            StartCoroutine(CoLoadScene(1));
            currentScene = TypeScene.Game;
        }

        private IEnumerator CoLoadScene(int number)
        {
            loaderCanvas.SetActive(true);

            operation = SceneManager.LoadSceneAsync(number);

            while (!operation.isDone)
            {
                yield return null;
            }

            operation = null;
            loaderCanvas.SetActive(false);

            SceneControllers.Instance.InitControllers();
        }
    }
}

public enum TypeScene
{
    Init,
    Game
}