using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MovesValueText;
    [SerializeField] private TextMeshProUGUI TimeValueText;

    private int moveCounter;
    private float timeCounter;

    private bool isGamePaused;
    private bool isGameStarted;


    private void Start()
    {
        NumberTile.OnTileMoved += UpdateMoveCounterAndItsText;
        NumberTile.OnTileMoved += StartGameTimer;
        MovesValueText.text = "0";
        TimeValueText.text = "0:0.0";
    }

    public void ToggleGameState()
    {
        isGamePaused = !isGamePaused;
    }

    private void StartGameTimer(object sender, EventArgs e)
    {
        isGameStarted = true;
        isGamePaused = false;
    }

    private void UpdateMoveCounterAndItsText(object sender, EventArgs e)
    {
        moveCounter++;
        MovesValueText.text = moveCounter.ToString();
    }

    private void Update()
    {
        if (isGamePaused || !isGameStarted)
        {
            return;
        }

        timeCounter += Time.deltaTime;
        TimeValueText.text = $"{(int)timeCounter / 60}:{timeCounter.ToString("F1")}";
    }

    public void ResetGame()
    {
        moveCounter = 0;
        timeCounter = 0;
        MovesValueText.text = "0";
        TimeValueText.text = "0:0.0";
        isGameStarted = false;
        isGamePaused = true;
    }
}
