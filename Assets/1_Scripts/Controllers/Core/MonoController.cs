using Core;
using Core.Interfaces;
using UnityEngine;

namespace Controllers.Core
{
    public abstract class MonoController : MonoBehaviour, IController
    {
        protected bool isPause = false;

        public virtual void OnInitialize() { }

        public virtual void OnStart() { }

        private void Start()
        {
            BoxControllers.AddMonoController(GetType(), this);
        }
        
        public void SetPause(bool value)
        {
            isPause = value;
            
            OnPause(value);
        }

        protected void Reset()
        {
            name = GetType().Name;
        }
        
        protected virtual void OnPause(bool value) { }

        public virtual void Save() { }
    }
}