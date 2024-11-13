using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HistoryScroll : MonoBehaviour
{
    [SerializeField] private float heightPadding, maxHeight;
    [SerializeField] private TextMeshProUGUI operationTextPrefab;
    [SerializeField] private GameObject content;

    private List<TextMeshProUGUI> poolTexts = new List<TextMeshProUGUI>();

    private RectTransform rectTransform;
    private float heightText;

    public void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        heightText = operationTextPrefab.GetComponent<RectTransform>().sizeDelta.y;
    }

    public void ShowOperationTexts(List<string> texts)
    {
        ClearContent();

        foreach (var text in texts)
        {
            GetTextFromPoolAreCreate().text = text;
        }

        ChangeHeightRectTransform(texts.Count);
    }

    private TextMeshProUGUI GetTextFromPoolAreCreate()
    {
        if (poolTexts.Count > 0)
        {
            TextMeshProUGUI text = poolTexts[0];
            poolTexts.Remove(text);
            text.gameObject.SetActive(true);
            return text;
        }
        else
        {
            return Instantiate(operationTextPrefab, content.transform);
        }
    }

    private void ChangeHeightRectTransform(int countTexts)
    {
        Vector2 sizeDelta = rectTransform.sizeDelta;
        float height = (heightText + heightPadding) * countTexts;

        if (height > maxHeight)
        {
            height = maxHeight;
        }

        sizeDelta.y = height;
        rectTransform.sizeDelta = sizeDelta;
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