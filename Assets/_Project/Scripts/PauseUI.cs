using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{

    private void Start()
    {
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.width);
    }

    public void ToggleFade()
    {
        Debug.Log("fade toggled");
        if (gameObject.activeSelf)
        {
            FadeOut();
        }
        else
        {
            gameObject.SetActive(true);
            FadeIn();
        }
    }

    private void FadeIn()
    {
        var image = GetComponent<Image>();
        image.DOFade(1, 0.5f);

        var text = GetComponentInChildren<TextMeshProUGUI>();
        text.DOFade(1, 0.5f);
    }

    private void FadeOut()
    {
        var image = GetComponent<Image>();
        image.DOFade(0, 0.5f).onComplete += () => gameObject.SetActive(false);

        var text = GetComponentInChildren<TextMeshProUGUI>();
        text.DOFade(1, 0.5f);
    }
}
