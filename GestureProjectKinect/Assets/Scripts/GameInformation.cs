using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameInformation : MonoBehaviour
{
    /* Public Variables */
    public static int score = 0;
    public TextMeshProUGUI scoreCountText;

    public GameObject winMenu;


    void Update() {
        scoreCountText.text = "SCORE: " + score.ToString();
        if(score == 5 && SceneManager.GetActiveScene().name == "MainScene")
        {
            Win();
        }
        else if(score == 10 && SceneManager.GetActiveScene().name == "LevelTwo")
        {
            Win();
        }
        else if(score == 15 && SceneManager.GetActiveScene().name == "LevelThree")
        {
            Win();
        }
    }

    public void Win()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
