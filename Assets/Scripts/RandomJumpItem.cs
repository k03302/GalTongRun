using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomJumpItem : MonoBehaviour
{
    public Vector3 Direction;
    public float speed;
    public float period;

    public float delayStart;
    public float delayEnd;
    public float delayBias;

    private float delay;
    

    public float randomDelayCoef;

    private bool isFirst;

    private Transform Item;
    private float time;
    private bool isWait;

    // Start is called before the first frame update
    void Start()
    {
        Item = transform.GetChild(0);

        Direction = Direction.normalized;

        time = 0;

        isWait = true;
        randomDelayCoef = Mathf.Clamp(randomDelayCoef, 0f ,1f);
        delay = Random.Range(0f, period * randomDelayCoef) + delayBias;

    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(isFirst)
        {
            if(time > delay)
            {
                isFirst = false;
                delay = Random.Range(delayStart, delayEnd);
            }
        }
        else
        {
            if (isWait)
            {
                if (time > delay)
                {
                    time = 0f;
                    isWait = false;
                }
            }

            else
            {
                if (time > period / 2f)
                {
                    time = 0f;
                    delay = Random.Range(delayStart, delayEnd);
                    isWait = true;
                }
                else
                {
                    Vector3 velocity = Direction * speed * Mathf.Cos(2 * Mathf.PI * time / period);


                    Item.transform.Translate(velocity * Time.deltaTime, Space.World);

                }
            }
        }
    }
}
