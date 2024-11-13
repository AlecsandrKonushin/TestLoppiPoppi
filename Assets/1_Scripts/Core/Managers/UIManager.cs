using System;
using System.Collections.Generic;
using Core.Main;
using UI.Windows;
using UnityEngine;

namespace Core.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [HideInInspector] public Action<bool> WindowIsOpenEvent;

        private Dictionary<Type, Window> windows;

        #region INITIALIZE

        public void OnInitialize()
        {
            Window[] getWindows = GetComponentsInChildren<Window>();
            windows = new Dictionary<Type, Window>();

            foreach (var window in getWindows)
            {
                windows.Add(window.GetType(), window);
            }

            foreach (var window in windows)
            {
                window.Value.OnInitialize();
            }
        }

        public void OnStart()
        {
            foreach (var window in windows)
            {
                window.Value.OnStart();
            }
        }

        #endregion INITIALIZE

        #region GET/SHOW/HIDE

        public static T GetWindow<T>() where T : Window
        {
            if (Instance.windows.TryGetValue(typeof(T), out var window))
            {
                return window as T;
            }
            else
            {
                Debug.LogError($"Not have window {typeof(T)} !");

                return null;
            }
        }

        public static void ShowWindow<T>(bool isNeedBlockInput = true) where T : Window
        {
            if (Instance.windows.TryGetValue(typeof(T), out var window))
            {
                if (isNeedBlockInput)
                {
                    Instance.WindowIsOpenEvent?.Invoke(true);
                }
                
                window.Show();
            }
            else
            {
                Debug.LogError($"Not have window {typeof(T)} for show!");
            }
        }

        public static void HideWindow<T>() where T : Window
        {
            if (Instance.windows.TryGetValue(typeof(T), out var window))
            {
                Instance.WindowIsOpenEvent?.Invoke(false);

                window.Hide();
            }
            else
            {
                Debug.LogError($"Not have window {typeof(T)} for close");
            }
        }

        public static void ChangeLanguage()
        {
            foreach (var window in Instance.windows)
            {
                window.Value.ChangeLanguage();
            }
        }

        #endregion

        private void ChangeLocalization()
        {
            foreach (var window in Instance.windows)
            {
                window.Value.ChangeLanguage();
            }
        }
    }
}