using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControll : MonoBehaviour
{
    private float initHeight;
    private bool disappear = false;
    public float length = 20.0f;
    public float speed = 4.0f;

    private void Start()
    {
        initHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(disappear)
        {
            transform.Translate(speed * Vector3.down * Time.deltaTime);
        }
        if(transform.position.y < initHeight - length)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            disappear = true;
        }
    }

}
