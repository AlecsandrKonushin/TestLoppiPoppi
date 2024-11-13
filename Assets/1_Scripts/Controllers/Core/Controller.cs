using Core.Interfaces;
using UnityEngine;

namespace Controllers.Core
{
    public class Controller : ScriptableObject, IController
    {
        protected bool pause = true;

        public virtual void OnInitialize() { }

        public virtual void OnStart() { }

        public void SetPause(bool value)
        {
            pause = value;
            
            OnPause(value);
        }
        
        protected virtual void OnPause(bool value) { }

        public virtual void Save() { }
    }
}