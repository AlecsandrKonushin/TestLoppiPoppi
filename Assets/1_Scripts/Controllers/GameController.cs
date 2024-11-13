using System.Collections.Generic;
using Controllers.Core;
using Core.Main;
using Core.Managers;
using UI.Windows;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "GameController", menuName = "Controllers/GameController")]
    public class GameController : Controller
    {
        private List<string> operations;

        public override void OnStart()
        {
            if (LoadSaveManager.IsHaveSave)
            {
                operations = LoadSaveManager.Operations;
            }
            else
            {
                operations = new List<string>();
            }
        }

        public void StartGameScene()
        {
            UIManager.GetWindow<CalculatorWindow>().SetLastInput(LoadSaveManager.LastInput);
            ShowCalculatorWindow();
        }

        public void ChangeInput(string input)
        {
            LoadSaveManager.LastInput = input;
            LoadSaveManager.Save();
        }

        public void SuccessOperation(string operation)
        {
            SaveOperation(operation);
            ShowCalculatorWindow();
        }

        public void FailedOperation(string operation)
        {
            SaveOperation(operation);

            UIManager.HideWindow<CalculatorWindow>();
            UIManager.GetWindow<ErrorWindow>().SetErrorMessage(Localizator.GetTextUI("ErrorMessage"));
            UIManager.ShowWindow<ErrorWindow>();
        }

        public void CloseErrorWindow()
        {
            UIManager.HideWindow<ErrorWindow>();

            ShowCalculatorWindow();
        }

        private void ShowCalculatorWindow()
        {
            List<string> reverseOperations = new List<string>(operations);
            reverseOperations.Reverse();
            UIManager.GetWindow<CalculatorWindow>().SetOperations(reverseOperations);
            UIManager.ShowWindow<CalculatorWindow>();
        }

        private void SaveOperation(string operation)
        {
            operations.Add(operation);
            LoadSaveManager.Operations = operations;
            LoadSaveManager.LastInput = "";
            LoadSaveManager.Save();
        }
    }
}