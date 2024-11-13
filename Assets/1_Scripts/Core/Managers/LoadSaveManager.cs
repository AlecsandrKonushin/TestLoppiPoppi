using System;
using System.Collections.Generic;
using System.IO;
using Controllers.Core;
using Core.Main;
using Data.Core;
using SaveSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Managers
{
    public class LoadSaveManager : Singleton<LoadSaveManager>
    {
        [HideInInspector] public static UnityEvent OnLoad;

        public static bool IsHaveSave;

        private SaveData saveData;
        
        public static TypeLanguage Language
        {
            get => Instance.saveData.Language;
            set => Instance.saveData.Language = value;
        }
        
        public static List<string> Operations
        {
            get => Instance.saveData.Operations;
            set => Instance.saveData.Operations = value;
        }
        
        protected override void AfterAwake()
        {
            OnLoad = new UnityEvent();
        }

        public static void LoadData()
        {
            Instance.saveData = null;
            IsHaveSave = false;

            try
            {
                if (File.Exists(Application.persistentDataPath + MainData.PATH_SAVE))
                {
                    string strLoadJson = File.ReadAllText(Application.persistentDataPath + MainData.PATH_SAVE);
                    Instance.saveData = JsonUtility.FromJson<SaveData>(strLoadJson);
                    IsHaveSave = true;
                }
                else
                {
                    Debug.Log($"Not have save!");
                    Instance.saveData = new SaveData();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error load game - {ex}");
            }

            OnLoad?.Invoke();
        }

        public static void Save()
        {
            IsHaveSave = true;

            BoxControllers.SaveGame();

            string jsonString = JsonUtility.ToJson(Instance.saveData);

            try
            {
                File.WriteAllText(Application.persistentDataPath + MainData.PATH_SAVE, jsonString);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error save game - {ex}");
            }
        }

        public static void DeleteAllSave(bool closeGame = true)
        {
            if (File.Exists(Application.persistentDataPath + MainData.PATH_SAVE))
            {
                File.Delete(Application.persistentDataPath + MainData.PATH_SAVE);
            }

            if (File.Exists(Application.persistentDataPath + MainData.PATH_LOGS))
            {
                File.Delete(Application.persistentDataPath + MainData.PATH_LOGS);
            }

            // PlayerPrefs.DeleteAll();

            if (closeGame)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }
            else
            {
                IsHaveSave = false;
                Instance.saveData = new SaveData();
            }
        }
    }
}