using Controllers;
using Controllers.Core;
using Core.Main;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ErrorWindow : Window
    {
        [SerializeField] private TextMeshProUGUI errorText;
        [SerializeField] private Button gotItButton;

        public override void OnStart()
        {
            gotItButton.GetComponentInChildren<TextMeshProUGUI>().text = Localizator.GetTextUI("GotIt");
            gotItButton.onClick.AddListener(() => BoxControllers.GetController<GameController>().CloseErrorWindow());
        }

        public void SetErrorMessage(string message)
        {
            errorText.text = message;
        }
    }
}