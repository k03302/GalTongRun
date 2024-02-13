using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.transform.position = transform.position;
        animator.transform.rotation = transform.rotation;
    }
}
