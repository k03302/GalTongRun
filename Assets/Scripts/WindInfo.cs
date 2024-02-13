using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindInfo : MonoBehaviour
{
    public float WindSpeed;
    public Vector3 direction;



    public float GetWindSpeed()
    {
        return WindSpeed;
    }
    public void SetWindSpeed(float speed)
    {
        WindSpeed = speed;
    }
    public Vector3 GetWindDirection()
    {
        return direction;
    }
    public Vector3 GetWindVector()
    {
        return WindSpeed * direction;
    }
}
