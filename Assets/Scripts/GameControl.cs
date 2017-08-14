using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public static GameControl instance;
    public GameObject gameOverText;
    public GameObject startGameText;
    public Text scoreText;
    public Text highScoreText;
    public bool gameOver = false;
    public bool startGame = true;
    public bool ClearScore = false;
    public float scrollSpeed = -1f;

    private int score = 0;

    // Use this for initialization
    void Awake ()
    {
		if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(WaitForRestart());
        }
	}

    public IEnumerator WaitForRestart()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        RestartGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        if (startGame == true)
        {
            Time.timeScale = 0;
            startGameText.SetActive(true);
            DeletePlayerPrefs();
            if (Input.GetMouseButtonDown(0))
            {
                startGameText.SetActive(false);
                Time.timeScale = 1;
                startGame = false;
            }
        }
    }

    public void HeroDied()
    {
        HighScore();
        gameOverText.SetActive(true);
        gameOver = true;
    }

    public void HeroScored()
    {
        if (gameOver)
        {
            return;
        }
        score++;
        scoreText.text = score.ToString ();
        Hero.instance.PlayGoalSound();
        int temp = PlayerPrefs.GetInt("highScore");
        if (score > temp)
        {
            PlayerPrefs.SetInt("highScore", score);
        }
    }

    public void HighScore()
    {
        highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
    }

    public void DeletePlayerPrefs()
    {
        if (ClearScore == false)
        {
            return;
        }
        else if (ClearScore == true)
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
