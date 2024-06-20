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
    public Button soundButton;

    public Button playerOne;
    public Button playerTwo;
    public GameObject instructionPanel;
    public GameObject mainMenuPanel;
    public GameObject playerSelectPanel;
    [SerializeField] bool isSoundOff = false;
    public Sprite soundOn;
    public Sprite soundOff;




    void Awake()
    {
        playButton.onClick.AddListener(OnPlayGame);
        quitButton.onClick.AddListener(OnQuitGame);
        soundButton.onClick.AddListener(OnMuteGameButton);
        instructionButton.onClick.AddListener(OnInstructionButton);
        backLevelButton.onClick.AddListener(OnBackLevelButton);
        playerOne.onClick.AddListener(OnPlayerOne);
        playerTwo.onClick.AddListener(OnPlayerTwo);
        backInstructionButton.onClick.AddListener(OnBackInstructionButtonn);
    }
    void Start()
    {
        GameManager.Instance.isGame2Player = false;
    }
    private void OnBackInstructionButtonn()
    {
        GameManager.Instance.PlaySfx(Sounds.BUTTON_CLICK);
        instructionPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(true);
    }
    private void OnPlayerOne()
    {
        GameManager.Instance.PlaySfx(Sounds.BUTTON_CLICK);
        GameManager.Instance.isGame2Player = false;
        SceneManager.LoadScene("SinglePlayerGameplay");
        // load scne
    }
    private void OnPlayerTwo()
    {
        GameManager.Instance.PlaySfx(Sounds.BUTTON_CLICK);
        GameManager.Instance.isGame2Player = true;
        SceneManager.LoadScene("TwoPlayerGameplay");
    }



    private void OnBackLevelButton()
    {
        GameManager.Instance.PlaySfx(Sounds.BUTTON_CLICK);
        playerSelectPanel.gameObject.SetActive(false);
        mainMenuPanel.gameObject.SetActive(true);

    }

    private void OnInstructionButton()
    {
        GameManager.Instance.PlaySfx(Sounds.BUTTON_CLICK);
        instructionPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
    }

    private void OnQuitGame()
    {
        GameManager.Instance.PlaySfx(Sounds.BUTTON_CLICK);
        Application.Quit();
    }

    private void OnPlayGame()
    {
        GameManager.Instance.PlaySfx(Sounds.BUTTON_CLICK);
        playerSelectPanel.gameObject.SetActive(true);
        mainMenuPanel.gameObject.SetActive(false);
        //play button click
    }

    private void OnMuteGameButton()
    {
            GameManager.Instance.PlaySfx(Sounds.BUTTON_CLICK);    
            GameManager.Instance.SetVolume(1);
    }

}
