using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainDirection : MonoBehaviour
{
    private Vector3 initEularAngle;
    // Start is called before the first frame update
    void Start()
    {
        initEularAngle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = initEularAngle;
    }
}
