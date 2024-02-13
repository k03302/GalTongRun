using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public AudioClip ButtonAudio;
    public Transform BigRani;
    public Transform SmallRani;
    private Text text;

    private AudioSource audioSource;
    private bool isEasy;

    private void Start()
    {
        isEasy = true;

        SmallRani.gameObject.SetActive(isEasy);
        BigRani.gameObject.SetActive(!isEasy);

        GameSys.SetDifficulty(isEasy);

        audioSource = GetComponent<AudioSource>();
        text = gameObject.GetComponentInChildren<Text>();
        UpdateText();
    }


    public void OnClick()
    {
        isEasy = !isEasy;
        SmallRani.gameObject.SetActive(isEasy);
        BigRani.gameObject.SetActive(!isEasy);

        GameSys.SetDifficulty(isEasy);

        PlaySound(ButtonAudio);
        UpdateText();
    }

    private void PlaySound(AudioClip audioClip)
    {
        float volume = 1.0f;

        audioSource.PlayOneShot(audioClip, volume);


    }

    private void UpdateText()
    {
        if (isEasy)
        {
            text.text = "알잘딱 난이도";
        }
        else
        {
            text.text = "지옥의 난이도";
        }
    }
}
