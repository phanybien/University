using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour{
    public Text scoreText;
    public Text highscoreText;
    void Start()
    {
        GlobalManager.highscore = PlayerPrefs.GetInt("highScore");
    }
    public void Update() {
        scoreText.text = "Score: " + GlobalManager.score;
        highscoreText.text = "HighScore: " + GlobalManager.highscore;

    }
}