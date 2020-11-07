using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    public bool enableOnStart;
    public bool enableOnSpace;

    void Start()
    {
        if (enableOnStart)
            PlaySoundTest();
    }

    void Update()
    {
        if (enableOnSpace && Input.GetKeyDown(KeyCode.Space))
        {
            PlaySoundTest();
        }
    }

    private void PlaySoundTest()
    {
        AkSoundEngine.PostEvent("Example", gameObject); // This code is used when triggering a sound event. 
                                                        // The names of sound events can be found by opening "Window / Wwise Picker" and then locating "Events"
        Debug.Log("sound test", gameObject);
    }
}