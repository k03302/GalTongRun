using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceControl : MonoBehaviour
{
    public Animator animator;
    public float range = 2.25f;
    private float myRandom;
    private float startTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("AnimationPlay", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        myRandom = Random.Range(0f, range);
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time - startTime > myRandom)
        {
            animator.SetBool("AnimationPlay", true);
        }
    }
}
