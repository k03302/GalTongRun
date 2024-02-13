using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInfo : MonoBehaviour
{
    public float maxHeadScale = 6f;
    public Transform HoleCenter;

    private float adjustSpeed = 5f;
    


    // Start is called before the first frame update
    void Awake()
    {
        float MinRadius = Mathf.Min(transform.localScale.x, transform.localScale.y);
        maxHeadScale *= MinRadius;
    }



    public bool isScaleOk(float scale)
    {
        return scale < maxHeadScale;
    }

    public bool isOnHoleX(Transform Player)
    {
        if (Mathf.Abs(HoleCenter.position.x - Player.position.x) < 0.1f)
        {
            return true;
        }
        return false;
    }

    public float GetJumpHeight(Transform Player)
    {
        return HoleCenter.position.y - Player.position.y;
    }

    public Vector3 GetAdustVector(Transform Player)
    {
        if(isOnHoleX(Player))
        {
            return Vector3.zero;
        }
        if (HoleCenter.position.x - Player.position.x > 0)
        {
            return Vector3.right * adjustSpeed;
        }
        else
        {
            return Vector3.left * adjustSpeed;
        }
    }

}
