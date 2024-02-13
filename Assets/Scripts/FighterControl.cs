using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterControl : MonoBehaviour
{
    public Animator animator;

    public AudioSource audioSource;
    public AudioClip audioDie;
    public AudioClip audioWin;
    public AudioClip audioHit;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("HeadButt"))
        {
            FixAnimator();
        }
    }

    private void FixAnimator()
    {
        animator.transform.position = transform.position;
        animator.transform.rotation = transform.rotation;
    }

    public void PlaySound(string action)
    {
        switch (action)
        {
            case "DIE":
                audioSource.clip = audioDie;
                audioSource.volume = 1.0f;
                break;
            case "WIN":
                audioSource.clip = audioWin;
                audioSource.volume = 0.3f;
                break;
            case "HIT":
                audioSource.clip = audioHit;
                audioSource.volume = 0.3f;
                break;
        }
        audioSource.Play();
    }
}
