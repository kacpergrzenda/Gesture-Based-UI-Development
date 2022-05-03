using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    /* Public Variables */
    public  GameObject[] lives;
    /* Private Variabls */
    private int temporaryLife = 3;
    public void handleLife () {
        
        if(temporaryLife != 0) {
            Debug.Log("here: " + lives.Length + " : " + temporaryLife);
            
            GameObject temp = lives.GetValue(temporaryLife-1) as GameObject;
            temp.GetComponent<Image>().enabled = false;
            temporaryLife--;
        }

        // GameOver
        if(temporaryLife == 0)
        {

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
           handleLife(); //
           Destroy(col.gameObject);
        }
    }
}
