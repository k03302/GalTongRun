using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleControll : MonoBehaviour
{
    public Transform LeftPole, RightPole;
    private float distance;

    void  Awake()
    {
        distance = RightPole.localPosition.x - LeftPole.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
