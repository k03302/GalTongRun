using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingItem : MonoBehaviour
{

    public Vector3 Direction;
    public float speed;
    public float period;
    public float delay;


    private Transform[] ItemArray;
    private float time;
    private bool first = true;


    // Start is called before the first frame update
    void Start()
    {
        ItemArray = new Transform[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            ItemArray[i] = transform.GetChild(i);
        }

        Direction = Direction.normalized;

        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > delay && first)
        {

            time = 0f;
            first = false;
        }
        
        else
        {
            Vector3 velocity = Direction * speed * Mathf.Cos(2 * Mathf.PI * time / period);

            for (int i = 0; i < transform.childCount; i++)
            {
                ItemArray[i].transform.Translate(velocity * Time.deltaTime, Space.World);
            }

            time %= period;
        }


    }
}
