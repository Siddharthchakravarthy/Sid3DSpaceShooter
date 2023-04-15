using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text ScoreText;
    [SerializeField]
    private TMP_Text GameOverText;
    public static int Score = 0;
    public static int Level = 0;
    void Start()
    {
        ScoreText.text = "Score: " + Score;
        GameOverText.gameObject.SetActive(false);
    }
    public void GameOverScreen() {
        GameOverText.gameObject.SetActive(true);
    } 
    void Update()
    {
        ScoreText.text = "Score: " + Score;
    }
}
