using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlane : MonoBehaviour
{
    public Transform Player;
    public Transform Accelerate;


    private float length;
    public float speed;
    public Vector3 direction;

    private int headIndex;
    private int tailIndex;
    private bool PlayerMoveMode = false;

    private float period;
    private float time;

    private Transform[] Planes;
    void Start()
    {
        Planes = new Transform[Accelerate.childCount];
        for(int i=0;i< Accelerate.childCount;i++)
        {
            Planes[i] = Accelerate.GetChild(i);
        }
        length = (Planes[0].position - Planes[1].position).magnitude;

        time = Time.time;
        headIndex = 0;
        tailIndex = Accelerate.childCount - 1;

        period = length / speed;
    }

    // Update is called once per frame
    void Update()
    {


        if (PlayerMoveMode)
        {
            Player.Translate(direction * speed * Time.deltaTime, Space.World);
        }
        else
        {
            for (int i = 0; i < Planes.Length; i++)
            {
                Planes[i].Translate(-direction * speed * Time.deltaTime, Space.World);
            }

            if (Time.time - time > period)
            {
                Planes[headIndex].Translate(direction * Planes.Length * length, Space.World);
                tailIndex = (tailIndex + 1) % Planes.Length;
                headIndex = (headIndex + 1) % Planes.Length;


                time = Time.time;
            }
        }
  

    }


    private int mod(int num, int arg)
    {
        if (0 <= num || num < arg)
        {
            return num;
        }
        if (-arg <= num || num < 0)
        {
            return num + arg;
        }
        return mod(num % arg, arg);
        
    }



    public void StartPlayerMoveMode()
    {
        time = 0f;
        PlayerMoveMode = true;
    }


}
