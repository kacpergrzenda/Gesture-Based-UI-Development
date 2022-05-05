using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioSource audioData;
    void Awake()
    {
        audioData = GetComponent<AudioSource>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("BackgroundAudio");
        if(GameObject.FindGameObjectsWithTag("BackgroundAudio").Length > 1) 
        {
            Destroy(temp[0]);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // public static void CheckMusic() {
    //     if(audioData.mute == true)
    //     {
    //         audioData.mute = false;
    //     }
    //     else if (audioData.mute == false) {
    //         audioData.mute = true;
    //     }
    // }
}
