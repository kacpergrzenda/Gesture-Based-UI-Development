using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameInformation : MonoBehaviour
{
    /* Public Variables */
    public static int score = 0;
    public TextMeshProUGUI scoreCountText;


    void Update() {
        scoreCountText.text = "Score: " + score.ToString();
    }
}
