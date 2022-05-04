using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;

    public void Retry ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        deathMenu.SetActive(true);
    }
    public void QuitGame ()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
