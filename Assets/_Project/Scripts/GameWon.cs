using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWon : MonoBehaviour
{
    [SerializeField] private Image gameWonImage;
    void Start()
    {
        TileManager.OnGameWon += ShowGameWonImage;
    }

    private void ShowGameWonImage(object sender, EventArgs e)
    {
        gameWonImage.gameObject.SetActive(true);
    }
}
