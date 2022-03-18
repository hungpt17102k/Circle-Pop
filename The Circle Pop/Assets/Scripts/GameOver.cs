using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gamePlayScreen;
    public Text finalPoint;
    public Text scoreText;
    public Text highScoreText;
    int score = 0;

    Mover mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = FindObjectOfType<Mover>();
        mover.OnGetPointEvent += GetPoint;
        mover.OnLoseEvent += OnGameOver;
    }

    void GetPoint()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    void GetFinalResult() {
        finalPoint.text = score.ToString();
    }

    void SetHighSorce() {
        if(PlayerPrefs.GetInt("high score", 0) < score) {
            PlayerPrefs.SetInt("high score", score);
        }

        highScoreText.text = "High score: " + PlayerPrefs.GetInt("high score").ToString();
    }

    void OnGameOver()
    {
        gamePlayScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        mover.OnLoseEvent -= OnGameOver;
        mover.OnGetPointEvent -= GetPoint;

        GetFinalResult();
        SetHighSorce();
    }

    public void RestartButton() {
        SceneManager.LoadScene("Play Scene");
    }

    public void ResetHighScore() {
        PlayerPrefs.DeleteAll();
    }
}
