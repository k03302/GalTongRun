using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoserAnimationControl : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.SetInteger("AnimationIndex", Random.Range(0, 3)); 
    }


}
