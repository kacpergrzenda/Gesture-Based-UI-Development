using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public void LevelOne ()
    {
        SceneManager.LoadScene(1);
    }
}
