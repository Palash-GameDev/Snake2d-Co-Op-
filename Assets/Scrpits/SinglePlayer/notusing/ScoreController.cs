using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int score = 0;
    [SerializeField] private int scoreMultiplier;
    [SerializeField] private TextMeshProUGUI scoreText;
    public bool isScoreBoost;
    //private TextMeshProUGUI highScoreText;


    void Awake()
    {
        //  scoreText = GetComponent<TextMeshProUGUI>();
        isScoreBoost = false;
    }
    // Start is called before the first frame update
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
            Debug.Log(isScoreBoost);
            score = score + (scoreMultiplier * 2);
            isScoreBoost = false;
        }
        else if (isScoreBoost == false)
        {
            Debug.Log(isScoreBoost);
            score += scoreMultiplier;
        }
        ResetText();
    }


}
