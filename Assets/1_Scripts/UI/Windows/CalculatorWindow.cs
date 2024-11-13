using System.Collections.Generic;
using Controllers;
using Controllers.Core;
using Core.Main;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class CalculatorWindow : Window
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private InputField equationInputField;
        [SerializeField] private HistoryScroll historyScroll;
        [SerializeField] private Button resultButton;

        public override void OnStart()
        {
            nameText.text = Localizator.GetTextUI("CalculatorPro");
            resultButton.GetComponentInChildren<TextMeshProUGUI>().text = Localizator.GetTextUI("Result");
            resultButton.onClick.AddListener(ClickResultButton);

            ResetInputField();
        }

        public void SetOperations(List<string> operations)
        {
            historyScroll.gameObject.SetActive(operations.Count > 0);

            if (operations.Count > 0)
            {
                historyScroll.ShowOperationTexts(operations);
            }
        }

        private void ClickResultButton()
        {
            BoxControllers.GetController<CalculatorController>().TryCountResult(equationInputField.textComponent.text);
            ResetInputField();
        }

        private void ResetInputField()
        {
            if (equationInputField.placeholder is Text placeholderText)
            {
                placeholderText.text = Localizator.GetTextUI("DefaultEquation");
            }
            else
            {
                Debug.LogError("Placeholder text not UnityEngine.UI.Text!");
            }

            equationInputField.text = "";
            equationInputField.DeactivateInputField();
        }
    }
}