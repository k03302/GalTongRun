using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleInfo : MonoBehaviour
{
    public float minHeadScale = 4.0f;
    private Transform[] Poles;
    private Vector3[] PathPos;
    private int PoleCount = 4;

    // Start is called before the first frame update
    void Awake()
    {
        Poles = new Transform[PoleCount];
        for(int i = 0; i < Poles.Length; i++)
        {
            Poles[i] = transform.GetChild(i);
        }

        PathPos = new Vector3[PoleCount - 1];
        for(int i = 0; i < PoleCount-1; i++)
        {
            PathPos[i] = (Poles[i].position + Poles[i + 1].position) / 2.0f;
        }
    }



    public int GetMinXDistanceIndex(float Xposition)
    {
        float min = float.PositiveInfinity;
        int minIndex = 0;
        for(int i=0;i< PathPos.Length; i++)
        {
            if (Mathf.Abs(PathPos[i].x - Xposition) < min)
            {
                min = Mathf.Abs(PathPos[i].x - Xposition);
                minIndex = i;
            }
        }
        return minIndex;
    }

    

    public bool isOnPoleX(float Xposition, int index)
    {
        if (Mathf.Abs(PathPos[index].x - Xposition) < 0.1f) {
            return true;
        }
        return false;
    }

    public int GetXAdjustSign(float Xposition, int index)
    {
        if(PathPos[index].x - Xposition > 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public bool isScaleOver(float scale)
    {
        return scale > minHeadScale;
    }



}
