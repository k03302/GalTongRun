using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WormButton : MonoBehaviour
{
    public AudioClip ButtonAudio;
    public Transform MealWorm;
    public Transform CuteWorm;
    public Transform Broccoli;
    private Text text;

    private string ItemType;
    private AudioSource audioSource;

    private void Start()
    {
        ItemType = "CuteWorm";
        UpdateActive();
        MealWormControl.SetItemType(ItemType);

        audioSource = GetComponent<AudioSource>();
        text = gameObject.GetComponentInChildren<Text>();
        UpdateText();
    }

    private string GetNextItemType(string itemType)
    {
        if(itemType == "MealWorm")
        {
            return "CuteWorm";
        }
        else if(itemType == "CuteWorm")
        {
            return "Broccoli";
        }
        else if(itemType == "Broccoli")
        {
            return "MealWorm";
        }
        return null;
    }

    private void UpdateActive()
    {
        switch (ItemType)
        {
            case "MealWorm":
                MealWorm.gameObject.SetActive(true);
                CuteWorm.gameObject.SetActive(false);
                Broccoli.gameObject.SetActive(false);
                break;
            case "CuteWorm":
                MealWorm.gameObject.SetActive(false);
                CuteWorm.gameObject.SetActive(true);
                Broccoli.gameObject.SetActive(false);
                break;
            case "Broccoli":
                MealWorm.gameObject.SetActive(false);
                CuteWorm.gameObject.SetActive(false);
                Broccoli.gameObject.SetActive(true);
                break;
        }
    }

    public void OnClick()
    {
        ItemType = GetNextItemType(ItemType);
        UpdateActive();
        MealWormControl.SetItemType(ItemType);
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
        switch (ItemType)
        {
            case "CuteWorm":
                text.text = "귀여운 벌레";
                break;
            case "MealWorm":
                text.text = "밀웜";
                break;
            case "Broccoli":
                text.text = "브로콜리";
                break;
        }
    }
}
