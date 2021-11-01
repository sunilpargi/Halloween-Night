using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameplayUI : MonoBehaviour
{
    public Text tapCounterText, countDownTimerText, targetText;
    public Text timerText, highScoretext, playerScore, gameoverhighScoretext, gameoverplayerScore;
    public GameManager gameManager;
    public GameObject gameoverPanel;
    public GameObject winPanel;
    public GameObject resumePanel;

    public bool isPaused;

    public AudioClip[] audioClip;
    public AudioClip loadingScreenClip;
    public AudioSource audioSource, bgSound;

    public GameObject loadingScreen;

    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        // timerText.text = "Timer:" + gameManager.timer.ToString();
        if(!gameManager.countDownTimerHasEnded)
        countDownTimerText.text =  gameManager.countDownTimer.ToString("F0");
    }

    public void DisableCountDOwnTImer()
    {
        countDownTimerText.gameObject.SetActive(false); 
    }

    public void BackBtnClicked()
    {
        SceneManager.LoadScene(0);
        PlayBtnSound();
    }

    public void MainMenuBtnClicked()
    {

        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        PlayBtnSound();
    }

    public void PauseBtnClicked()
    {
        resumePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
        PlayBtnSound();
    }

    public void ResumeBtnClicked()
    {
        Time.timeScale = 1;
        resumePanel.SetActive(false);
        isPaused = false;
        PlayBtnSound();

    }

    public void UpdateTapCounterText(int _tapCounter)
    {
        tapCounterText.text = _tapCounter.ToString();
    }

    public void UpdateTargetText(int _targetCount)
    {
        targetText.text = "Collect :"+ _targetCount.ToString();
    }

    public void UpdateTimerText(float _timerCounter)
    {
       int timer =  (int)_timerCounter;
        timerText.text = "Timer :" + timer.ToString();
        PlayBtnSound();
    }



    public void ShowGameoverAndWin()
    {
        if (gameManager.hasWon)
        {
            winPanel.SetActive(true);

        }
        else
        {
            gameoverPanel.SetActive(true);
        }
    }

    public void RestartBtnClicked()
    {
        PlayBtnSound();
        SceneManager.LoadScene(1);
    }


    public void ShowHighScore(int _highScore)
    {
        highScoretext.text = "HighScore:" + _highScore.ToString();
    }

    public void ShowPlayerScore(int _Score)
    {
        playerScore.text = "YourScore:" + _Score.ToString();
    }

    public void ShowGameoverHighScore(int _highScore)
    {
        gameoverhighScoretext.text = "HighScore:" + _highScore.ToString();
    }

    public void ShowGameoverPlayerScore(int _Score)
    {
        gameoverplayerScore.text = "YourScore:" + _Score.ToString();
    }

    public void PlayBtnSound()
    {
        int random = UnityEngine.Random.Range(0, audioClip.Length);
        audioSource.PlayOneShot(audioClip[random]);
    }

    public void NextLevel()
    {
        bgSound.Stop();
        bgSound.clip = loadingScreenClip;
        bgSound.Play();
        loadingScreen.SetActive(true);
        gameManager.IncreaseLevel();
        StartCoroutine(SceneChange());
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
