using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public Button instructionButton;
    public Button backLevelButton;
    public Button backInstructionButton;

    public Button playerOne;
    public Button playerTwo;
    public GameObject instructionPanel;
    public GameObject mainMenuPanel;
    public GameObject playerSelectPanel;


void Awake()
{
    playButton.onClick.AddListener(OnPlayGame);
    quitButton.onClick.AddListener(OnQuitGame);
    instructionButton.onClick.AddListener(OnInstructionButton);
    backLevelButton.onClick.AddListener(OnBackLevelButton);
    playerOne.onClick.AddListener(OnPlayerOne);
    playerTwo.onClick.AddListener(OnPlayerTwo);
    backInstructionButton.onClick.AddListener(OnBackInstructionButtonn);
}

    private void OnBackInstructionButtonn()
    {
        instructionPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(true);
    }

    private void OnPlayerTwo()
    {
       //SceneManager.LoadScene("");
    }

    private void OnPlayerOne()
    {
         SceneManager.LoadScene("SinglePlayerGameplay");
        // load scne
    }

    private void OnBackLevelButton()
    {
        playerSelectPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(true);
        
    }

    private void OnInstructionButton()
    {
        instructionPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
    }

    private void OnQuitGame()
    {
         Application.Quit();
    }

    private void OnPlayGame()
    {
        playerSelectPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
        //play button click
    }

}
