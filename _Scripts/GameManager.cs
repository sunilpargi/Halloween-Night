using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public int tapCount, targetCount;
    public static int highScore;
    public float timer, countDownTimer;
    public float defaultTimer;
    public GameplayUI gameplayUI;
    public bool timerHasEnded, hasWon, countDownTimerHasEnded;
    public int levelNum;
    public int baseLevelMultipier = 10;
    // Update is called once per frame
    public BallonsSpawner ballonSpawner;

    public bool startSpawningBallons;
    public ClickCounter clickCounter;
    public AudioSource bgSound;
    public AudioClip gameFinsihedClip, laodingScreenClip;
    private void Start()
    {
        countDownTimer = 3;
      
        LoadHIghScore();


        //if (PlayerPrefs.HasKey("LevelNum"))
        //{
        //    targetCount = GetTapTargetCount(levelNum);
        //}
          
        if(PlayerPrefs.GetInt("LevelNum") == 0)
        {
            PlayerPrefs.SetInt("LevelNum",1);
          
        }
      
        levelNum =  PlayerPrefs.GetInt("LevelNum");

      targetCount =   GetTapTargetCount(levelNum);
       timer = GetTimerCount(levelNum);

    }
    void Update()
    {
       
        if (!countDownTimerHasEnded)
        {
            countDownTimer -= Time.deltaTime;
            if (countDownTimer <= 0)
            {
                countDownTimerHasEnded = true;
                gameplayUI.DisableCountDOwnTImer();
            }   
        }
        

        if (timerHasEnded == false && countDownTimerHasEnded)
        {
          
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                timer = 0;
                timerHasEnded = true;
                ballonSpawner.StopSpawnBallon();

                if(clickCounter.tapCount > highScore)
                {
                    highScore = clickCounter.tapCount;
                }
                SaveHIghScore();

                if (clickCounter.tapCount > targetCount)
                {
                    hasWon = true;
                    bgSound.Stop();
                    bgSound.clip = gameFinsihedClip;
                    bgSound.Play();

                    gameplayUI.ShowGameoverAndWin();
                    gameplayUI.ShowHighScore(highScore);
                    gameplayUI.ShowPlayerScore(clickCounter.tapCount);
                }
                else
                {
                    hasWon = false;
                    bgSound.Stop();
                    bgSound.clip = gameFinsihedClip;
                    bgSound.Play();

                    gameplayUI.ShowGameoverAndWin();
                    gameplayUI.ShowGameoverHighScore(highScore);
                    gameplayUI.ShowGameoverPlayerScore(clickCounter.tapCount);
                }          


              
               
            }

            gameplayUI.UpdateTimerText(timer);
            gameplayUI.UpdateTargetText(targetCount);
        }
      

        if (!gameplayUI.isPaused  && !timerHasEnded && countDownTimerHasEnded)
        {
            //tapCount++;
            //gameplayUI.UpdateTapCounterText(tapCount);
            //gameplayUI.PlayBtnSound();
            if (!startSpawningBallons)
            {
                startSpawningBallons = true;
                ballonSpawner.startSpawning = true;
                ballonSpawner.SpawnBallon();
            }
            
        }   
    }

    public void SaveHIghScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void LoadHIghScore()
    {
      highScore = PlayerPrefs.GetInt("HighScore");
    }

    public int GetTapTargetCount(int _levelNum)
    {
        int temp = 0;
        float tempTimer = 0;
        temp = baseLevelMultipier * _levelNum;
        tempTimer = defaultTimer * _levelNum;
        return temp;
    }

    public float GetTimerCount(int _levelNum)
    {
      
        float tempTimer = 0;
       
        tempTimer = defaultTimer * _levelNum;
        return tempTimer;
    }

    public void IncreaseLevel()
    {
        levelNum++;
        PlayerPrefs.SetInt("LevelNum", levelNum);
    }

    public int  GetLevelNum()
    {
        return levelNum;
    }
}
