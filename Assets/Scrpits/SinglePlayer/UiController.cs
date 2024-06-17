using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public int score = 0;
    [SerializeField] private int scoreMultiplier;
    [SerializeField] private TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverUIScoreText;
    public bool isScoreBoost;
    public Button pauseButton;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject gameOverPanel;


    void Awake()
    {
        pauseButton.onClick.AddListener(OnPauseClick);
        isScoreBoost = false;
    }

    void Start()
    {
        ResetText();
    }

    void ResetText()
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateScore()
    {
        if (isScoreBoost == true)
        {
           
            score = score + (scoreMultiplier * 2);
            isScoreBoost = false;
        }
        else if (isScoreBoost == false)
        {
            
            score += scoreMultiplier;
        }
        ResetText();
    }
    public void OnPauseClick()
    {
        Time.timeScale = 0;
        pauseMenuPanel.SetActive(true);
        // play button click sound
    }

    public void OnResumeClick()
    {
        Time.timeScale = 1;
        pauseMenuPanel.SetActive(false);
        // play button click sound
    }

    public void OnRestartClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SinglePlayerGameplay");
        //pauseMenuPanel.SetActive(false);
        // gameOverPanel.SetActive(false);

    }

    public void OnMainMenuClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        // Load MainMenu scene

    }

    public void OnGameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        gameOverUIScoreText.text = "Score: " + score;
    }



}//class
