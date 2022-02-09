using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSound : MonoBehaviour
{
    AudioSource WSound;
    public static bool WinCheck = false;
    public int LoopCount;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        WSound = GetComponent<AudioSource>();
        LoopCount = 0;
    }

    public void Update()
    {
        if (WinCheck == true && LoopCount == 0)
        {
            WSound.Play();
            LoopCount += 1;
        }
    }
}