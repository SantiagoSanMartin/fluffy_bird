using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Button restartGame;

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void ShowRestartButton(bool value)
    {
        restartGame.gameObject.SetActive(value);
    }

    public void Restart()
    {
        ShowRestartButton(false);
        UpdateScore(0);
    }
}
