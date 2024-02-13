using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeButton : MonoBehaviour
{
    public AudioClip ButtonAudio;
    public Transform BigRani;
    public Transform SmallRani;
    private Text text;

    private AudioSource audioSource;
    private bool isSmall;

    private void Start()
    {
        isSmall = true;

        SmallRani.gameObject.SetActive(isSmall);
        BigRani.gameObject.SetActive(!isSmall);

        HambergerControl.SetItemType("Hamberger");

        audioSource = GetComponent<AudioSource>();
        text = gameObject.GetComponentInChildren<Text>();
        UpdateText();
    }


    public void OnClick()
    {
        isSmall = !isSmall;
        SmallRani.gameObject.SetActive(isSmall);
        BigRani.gameObject.SetActive(!isSmall);
        HambergerControl.SetItemType(isSmall?"Hamberger" : "Maratang");
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
        if (isSmall)
        {
            text.text = "햄버거";
        }
        else
        {
            text.text = "마라탕";
        }
    }
}
