using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationBar : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private int buttonWidth;

    private void Start()
    {
        SetupButtonRectTransforms();
    }

    private void SetupButtonRectTransforms()
    {
        buttonWidth = (int)Screen.width / buttons.Length;
        var xPosition = buttonWidth / 2;
        for (int i = 0; i < buttons.Length; i++)
        {
            var buttonRectTransform = buttons[i].GetComponent<RectTransform>();
            buttonRectTransform.sizeDelta = new Vector2(buttonWidth, GetComponent<RectTransform>().sizeDelta.y);
            buttonRectTransform.anchoredPosition = new Vector2(xPosition, buttonRectTransform.anchoredPosition.y);
            xPosition += buttonWidth;
        }
    }
}
