using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingControl : MonoBehaviour
{
    public Transform FromObject;
    public Transform ToObject;
    public float length;
    public float angleSpeed;
    public LineRenderer[] renderers;


    private Vector3 Axis;
    // Start is called before the first frame update
    void Start()
    {
        Axis = (ToObject.position - FromObject.position).normalized;



        float angle = 360f / renderers.Length;
        Vector3 direction = GetNormalVector(Axis).normalized;
        for (int i = 0; i < renderers.Length; i++)
        {
            
            renderers[i].SetPosition(0, transform.position);
            renderers[i].SetPosition(1, transform.position + direction * length);
            renderers[i].startWidth = 2f;
            renderers[i].endWidth = 1f;

            direction = Quaternion.AngleAxis(angle, Axis) * direction;
        }

        
    }



    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<renderers.Length;i++)
        {
            Vector3 direction = renderers[i].GetPosition(1) - renderers[i].GetPosition(0);
            Vector3 rotatedDirection = (Quaternion.AngleAxis(360f * angleSpeed * Time.deltaTime, Axis) * direction).normalized;
            renderers[i].SetPosition(1, transform.position + rotatedDirection * length);
        }
    }

    private Vector3 GetNormalVector(Vector3 vector)
    {
        return new Vector3(vector.y - vector.z, vector.z - vector.x, vector.x - vector.y);
        
    }
}
