using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateSpeed = 200f;
    public float speed = 0.6f;
    public float frequency = 5.0f;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);

        transform.Translate(Vector3.up * speed * Mathf.Sin(Mathf.PI * 2 * time / frequency) * Time.deltaTime, Space.World);
        time += Time.deltaTime;
        time %= frequency;

    }
}
