using System;
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
        NumberTile.OnTileMoved += UpdateMoveCounterAndItsText;
        MovesValueText.text = "0";
    }

    private void UpdateMoveCounterAndItsText(object sender, EventArgs e)
    {
        moveCounter++;
        MovesValueText.text = moveCounter.ToString();
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;
        TimeValueText.text = $"{(int)timeCounter / 60}:{timeCounter.ToString("F1")}";
    }
}
