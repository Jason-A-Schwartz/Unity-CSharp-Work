using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource BGMusic;
    public static bool WinCheck = false;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        BGMusic = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (WinCheck == true)
        {
            BGMusic.Stop();
        }
    }
}