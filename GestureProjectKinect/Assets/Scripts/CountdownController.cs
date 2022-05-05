using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CountdownController : MonoBehaviour
{

    /* Public Variables */
    public GameObject fruitOneSpawner;
    public GameObject fruitTwoSpawner;
    public GameObject fruitThreeSpawner;
    public GameObject fruitFourSpawner;

    /* Countdown Text Variables */
    public int countdownTime;
    public TMP_Text countdownDisplay;

    void Start() 
    {
        StartCoroutine(CountdownToStart());
    }
    
    IEnumerator CountdownToStart() {
        while(countdownTime > 0) 
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        countdownDisplay.text = "GO!".ToString();

        
        yield return new WaitForSeconds(1f);

        SpawnFruit();

        countdownDisplay.gameObject.SetActive(false);
    }


    private void SpawnFruit() {
        fruitOneSpawner.gameObject.SetActive(true);
        fruitTwoSpawner.gameObject.SetActive(true);
        fruitThreeSpawner.gameObject.SetActive(true);
        fruitFourSpawner.gameObject.SetActive(true);
    }
    
}
