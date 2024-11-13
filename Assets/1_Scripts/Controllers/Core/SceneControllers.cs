using Core.Main;
using Core.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Controllers.Core
{
    public class SceneControllers : Singleton<SceneControllers>
    {
        [HideInInspector] public UnityEvent OnInitEvent;

        [SerializeField] private Controller[] sceneControllers;

        public void InitControllers()
        {
            BoxControllers.OnInit.AddListener(AfterInit);
            BoxControllers.InitControllers(sceneControllers);
        }

        private void AfterInit()
        {
            BoxControllers.OnInit.RemoveListener(AfterInit);
            UIManager.Instance.OnInitialize();
            BoxControllers.OnInit.AddListener(AfterStartControllers);
            BoxControllers.StartControllers();
        }

        private void AfterStartControllers()
        {
            BoxControllers.OnInit.RemoveListener(AfterStartControllers);
            UIManager.Instance.OnStart();

            OtherActions();

            BoxControllers.GetController<GameController>().StartGameScene();

            OnInitEvent?.Invoke();
        }

        protected virtual void OtherActions()
        {
        }
    }
}