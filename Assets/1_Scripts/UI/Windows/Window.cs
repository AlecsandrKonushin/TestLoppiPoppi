using Core.Interfaces;
using UnityEngine;

namespace UI.Windows
{
    public abstract class Window : MonoBehaviour, IInitialize
    {
        protected GameObject background;
        protected CanvasGroup substrate;

        protected bool IsActive => background.activeSelf;

        public void OnInitialize()
        {
            background = transform.GetChild(0).gameObject;
            substrate = background.transform.GetChild(0).GetComponent<CanvasGroup>();
        }

        public virtual void OnStart()
        {
            
        }

        public void Show()
        {
            background.SetActive(true);
        }

        public void Hide()
        {
            background.SetActive(false);
        }

        public virtual void ChangeLanguage() { }        
    }
}