using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    // public GameObject winMenu;

    // public static bool win = false;

    // void Update() {
    //     if(win == true)
    //     {      
    //         LoadWinMenu();
    //     }
    // }
    // public void LoadWinMenu()
    // {
    //     winMenu.SetActive(true);
    //     Time.timeScale = 0f;
    // }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
        GameInformation.score = 0;
    }

    public void LoadLevelThree()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
        GameInformation.score = 0;
    }

    public void QuitGame ()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        GameInformation.score = 0;
    }
}
