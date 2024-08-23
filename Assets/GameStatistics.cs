using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MovesValueText;
    [SerializeField] private TextMeshProUGUI TimeValueText;

    private int moveCounter;
    private float timeCounter;

    private void Start()
    {
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;
        TimeValueText.text = $"{timeCounter.ToString("F1")}";
    }
}
