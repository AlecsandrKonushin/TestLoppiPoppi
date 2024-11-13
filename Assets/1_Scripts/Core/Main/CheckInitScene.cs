using Core.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Main
{
    public class CheckInitScene : MonoBehaviour
    {
        private void Awake()
        {
            if (AppManager.Instance == null)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}