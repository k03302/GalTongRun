using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    public float rotateSpeed;
    public float axisRotateSpeed;

    private Vector3 Axis;
    private Vector3 AxisRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        Axis = GetRandomAxis();
        AxisRotation = GetRandomAxis();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Axis * rotateSpeed * Time.deltaTime, Space.World);
        Axis = (Axis + AxisRotation * axisRotateSpeed * Time.deltaTime).normalized;
    }

    Vector3 GetRandomAxis()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
