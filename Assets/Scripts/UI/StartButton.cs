using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartButton : MonoBehaviour
{
    public AudioClip Going;
    public GameObject RunAnimationSys;

    private AudioSource audioSource;
    private MovePlane movePlane;
    private bool isStart = false;
    private float time;
    private bool first = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movePlane = RunAnimationSys.GetComponent<MovePlane>();
    }
    private void Update()
    {
        if(isStart)
        {
            if(Time.time - time > Going.length)
            {
                StartMainGame();
            }
        }
    }

    public void OnClick()
    {
        if(first)
        {
            isStart = true;
            time = Time.time;
            PlaySound(Going);
            movePlane.StartPlayerMoveMode();
            first = false;
            GameSys.InitalizeScores();
        }
    }

    private void StartMainGame()
    {
        SceneManager.LoadScene(GameSys.SceneNumber);
    }

    private void PlaySound(AudioClip audioClip)
    {
        float volume = 1.0f;

        audioSource.PlayOneShot(audioClip, volume);
        

    }
}
