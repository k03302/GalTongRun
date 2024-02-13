using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    static bool first = true;
    public static bool isPause = false;
    private bool myFirst;
    private AudioSource myAudio;

    void Awake()
    {
        myFirst = first;
        first = false;
        myAudio = GetComponent<AudioSource>();
        myAudio.volume = 0.3f;
        if (myFirst)
        {
            myAudio.volume = 0.3f;
            PauseSound();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            myAudio.Stop();
            gameObject.GetComponent<AudioListener>().enabled = false;
        }
    }

    private void Update()
    {
        if(isPause || GameSys.isStartMode())
        {
            myAudio.Pause();
        }
    }

    public void PlaySound()
    {
        if (isPause)
        {
            myAudio.Play();
            isPause = false;
        }
    }

    public void PauseSound()
    {
        isPause = true;
    }
}

