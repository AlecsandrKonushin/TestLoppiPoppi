using System.Text.RegularExpressions;
using Controllers.Core;
using Core.Main;
using UnityEngine;

namespace Controllers
{
    [CreateAssetMenu(fileName = "CalculatorController", menuName = "Controllers/CalculatorController")]
    public class CalculatorController : Controller
    {
        private static string REGEX = @"^\d+\+\d+$",
            SEPARATOR = "+",
            EQUALS_SIGN = "=";

        public void TryCountResult(string inputString)
        {
            string input = inputString.Replace("\n", "").Replace("\r", "");
            string output;

            if (IsValidInput(input))
            {
                int sum = GetSumInput(input);
                output = $"{inputString}{EQUALS_SIGN}{sum}";
                BoxControllers.GetController<GameController>().SuccessOperation(output);
            }
            else
            {
                output = $"{inputString}{EQUALS_SIGN}{Localizator.GetTextUI("Error")}";
                BoxControllers.GetController<GameController>().FailedOperation(output);
            }

            Debug.Log($"output = {output}");
        }

        private bool IsValidInput(string input)
        {
            return Regex.IsMatch(input, REGEX);
        }

        private int GetSumInput(string input)
        {
            string[] parts = input.Split(SEPARATOR);
            int.TryParse(parts[0], out int firstNumber);
            int.TryParse(parts[1], out int secondNumber);
            return firstNumber + secondNumber;
        }
    }
}