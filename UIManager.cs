using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI startText;

    public void Start()
    {
        if (restartText == null)
            Debug.LogError("restart text is null");
        if (scoreText == null)
        {
            Debug.LogError("scoreText is null");
            return;
        }
        if (startText == null)
            Debug.LogError("startText is null");

        startText.gameObject.SetActive(true);     // 시작 텍스트 켜기
        restartText.gameObject.SetActive(false);  // 재시작 텍스트 끄기
    }

    public void SetStart()
    {
        startText.gameObject.SetActive(true);
    }


    public void SetRestart()
    {
        restartText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}