using System.IO;
using Data.Core;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace EditorTools
{
    public class MenuTools
    {
        [MenuItem("Tools/Delete Save")]
        private static void DeleteSave()
        {
            if (File.Exists(Application.persistentDataPath + MainData.PATH_LOGS))
            {
                File.Delete(Application.persistentDataPath + MainData.PATH_LOGS);
            }

            if (File.Exists(Application.persistentDataPath + MainData.PATH_SAVE))
            {
                File.Delete(Application.persistentDataPath + MainData.PATH_SAVE);
            }
            
            PlayerPrefs.DeleteAll();
        }
    }
}

#endif