using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RotateAroundIItem : MonoBehaviour
{

    
    public float angleSpeed;
    public Transform Center;
    public Vector3 Axis;

    private Transform[] ItemArray;
    

    // Start is called before the first frame update
    void Start()
    {
        ItemArray = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            ItemArray[i] = transform.GetChild(i);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Center;
        for(int i=0;i<transform.childCount;i++)
        {
            ItemArray[i].RotateAround(Center.position, Axis, 2 * Mathf.PI * Time.deltaTime * angleSpeed);
        }
        
    }

    

}
