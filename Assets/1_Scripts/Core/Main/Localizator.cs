using System.Collections.Generic;
using Core.Managers;
using Data.Core;
using UnityEngine;

namespace Core.Main
{
    public class Localizator : Singleton<Localizator>
    {
        [SerializeField] private TypeLanguage language;

        private const string FILE_UI_TEXTS = "UI";

        private bool isLoad;

        private static Dictionary<string, string[]> uiTexts = new Dictionary<string, string[]>();

        public void Init()
        {
            
#if !UNITY_EDITOR

        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            language = TypeLanguage.RU;
        }
        else
        {
            language = TypeLanguage.EN;
        }
            
#endif

            LoadData();
        }

        public void Init(TypeLanguage language)
        {
            this.language = language;

            LoadData();
        }

        public void ChangeLanguage(TypeLanguage language)
        {
            this.language = language;

            LoadSaveManager.Language = language;

            // BoxControllers.GetController<EventsController>().ChangeLocalization();
        }

        #region GET_TEXT

        public static string GetTextUI(string idText)
        {
            return GetText(uiTexts, idText);
        }

        private static string GetText(Dictionary<string, string[]> dictionary, string idText)
        {
            string[] textsById = null;
            string needText = "";

            dictionary.TryGetValue(idText, out textsById);

            if (textsById == null)
            {
                Debug.LogError($"Not have text in {dictionary} with id '{idText}' !");
            }
            else
            {
                needText = textsById[(int)Instance.language]; // Берём текст текущего языка
            }

            if (needText == "")
            {
                Debug.LogError($"Text {dictionary} with id '{idText}', language {0} empty!");
            }

            return needText;
        }

        #endregion GET_TEXT

        #region LOAD_DATA_TEXTS

        private void LoadData()
        {
            if (isLoad)
            {
                return;
            }

            LoadData(FILE_UI_TEXTS, uiTexts);

            isLoad = true;
        }

        private void LoadData(string fileName, Dictionary<string, string[]> dataDictionary)
        {
            TextAsset textAsset = Resources.Load(MainData.PATH_LOCALIZATION_FILES + fileName.ToString()) as TextAsset;

            if (textAsset == null)
            {
                Debug.LogError($"Not have txt file {fileName} in resources!");
            }


            List<string> splitText = new List<string>(textAsset.text.Split('\n'));

            for (int i = 0; i < splitText.Count; i++)
            {
                List<string> addTexts = new List<string>(splitText[i].Split(';'));
                string[] textLanguages = new string[2] { addTexts[1], addTexts[2] };

                dataDictionary.Add(addTexts[0], textLanguages);
            }
        }

        #endregion LOAD_DATA_TEXTS
    }

    public enum TypeLanguage
    {
        RU,
        EN
    }
}