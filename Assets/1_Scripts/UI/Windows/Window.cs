using System;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public abstract class Window : MonoBehaviour, IInitialize
    {
        [HideInInspector]
        public Action EndShow, EndChange, EndHideEvent;
        
        [SerializeField] protected Button closeButton, backgroundCloseButton;
        
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

        public virtual void Show() { }
        public virtual void Hide() { }
        public virtual void Change() { }

        public virtual void ChangeLanguage() { }        
    }
}