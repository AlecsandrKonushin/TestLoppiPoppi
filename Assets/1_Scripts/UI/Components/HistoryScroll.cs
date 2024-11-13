using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HistoryScroll : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI operationTextPrefab;
    [SerializeField] private GameObject content;

    private List<TextMeshProUGUI> poolTexts = new List<TextMeshProUGUI>();

    public void ShowOperationTexts(List<string> texts)
    {
        ClearContent();

        foreach (var text in texts)
        {
            GetTextFromPoolAreCreate().text = text;
        }
    }

    private TextMeshProUGUI GetTextFromPoolAreCreate()
    {
        if (poolTexts.Count > 0)
        {
            TextMeshProUGUI text = poolTexts[0];
            poolTexts.Remove(text);
            return text;
        }
        else
        {
            return Instantiate(operationTextPrefab, content.transform);
        }
    }

    private void ClearContent()
    {
        foreach (var text in content.GetComponentsInChildren<TextMeshProUGUI>())
        {
            poolTexts.Add(text);
            text.gameObject.SetActive(false);
        }
    }
}
