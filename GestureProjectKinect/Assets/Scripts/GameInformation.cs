using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInformation : MonoBehaviour
{
    /* Public Variables */
    public static int score = 0;
    public TextMeshProUGUI scoreCountText;

    public GameObject winMenu;


    void Update() {
        scoreCountText.text = "SCORE: " + score.ToString();
        if(score == 5)
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
