using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private int greenSnakeScore = 0;
    [SerializeField] private int yellowSnakeScore = 0;
    [SerializeField] private int scoreMultiplier = 1;
    [SerializeField] private TextMeshProUGUI _GreenscoreText;
    [SerializeField] private TextMeshProUGUI _YellowscoreText;
    public TextMeshProUGUI gameOverUIScoreText;
    public bool isScoreBoost;
    public Button pauseButton;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private bool is2Player;

    void Awake()
    {
        is2Player = GameManager.Instance.isGame2Player;
        pauseButton.onClick.AddListener(OnPauseClick);
        isScoreBoost = false;
    }

    void Start()
    {
        ResetText();
    }

    private void ResetText()
    {
        _GreenscoreText.text = "Score: " + greenSnakeScore;
        if (is2Player)
        {
            _YellowscoreText.text = "Score: " + yellowSnakeScore;
        }
    }

    public void UpdateGreenSnakeScore()
    {
        if (isScoreBoost)
        {
            greenSnakeScore += scoreMultiplier * 2;
            isScoreBoost = false;
        }
        else
        {
            greenSnakeScore += scoreMultiplier;
        }
        _GreenscoreText.text = "Score: " + greenSnakeScore;
    }

    public void UpdateYellowSnakeScore()
    {
        if (is2Player)
        {
            if (isScoreBoost)
            {
                yellowSnakeScore += scoreMultiplier * 2;
                isScoreBoost = false;
            }
            else
            {
                yellowSnakeScore += scoreMultiplier;
            }
            _YellowscoreText.text = "Score: " + yellowSnakeScore;
        }
    }
 public void OnGameOver()
    {
        GameManager.Instance.PlaySfx(Sounds.GameOver);
        gameOverPanel.SetActive(true);
      
        if (is2Player)
        {
            if (greenSnakeScore > yellowSnakeScore)
            {
                gameOverUIScoreText.text = "Green Snake Wins !!!\n\nGreen Snake Score: " + greenSnakeScore + "\nYellow Snake Score: " + yellowSnakeScore;
            }
            else if (greenSnakeScore < yellowSnakeScore)
            {
                 gameOverUIScoreText.text = "Yellow Snake Wins!\n\nGreen Snake Score: " + greenSnakeScore + "\nYellow Snake Score: " + yellowSnakeScore;
            }
            else if( greenSnakeScore == yellowSnakeScore)
            {
                 gameOverUIScoreText.text = "Draw !!!\n\nGreen Snake Score: " + greenSnakeScore + "\nYellow Snake Score: " + yellowSnakeScore;
            }
        }
        else
        {
            gameOverUIScoreText.text = "Green Snake Score: " + greenSnakeScore;
        }
    }
    public void OnPauseClick()
    {
        Time.timeScale = 0;
         GameManager.Instance.PlaySfx(Sounds.BUTTON_CLICK);
        pauseMenuPanel.SetActive(true);
        // play button click sound
    }

    public void OnResumeClick()
    {
        Time.timeScale = 1;
         GameManager.Instance.PlaySfx(Sounds.BUTTON_CLICK);
        pauseMenuPanel.SetActive(false);
        // play button click sound
    }

    public void OnRestartClick()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
         GameManager.Instance.PlaySfx(Sounds.BUTTON_CLICK);
        SceneManager.LoadScene(currentSceneBuildIndex);
    }

    public void OnMainMenuClick()
    {
        Time.timeScale = 1;
         GameManager.Instance.PlaySfx(Sounds.BUTTON_CLICK);
        SceneManager.LoadScene("MainMenu");
    }

   
}
