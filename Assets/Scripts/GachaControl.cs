using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SwapPositionAnimation
{
    private Transform obj1, obj2;
    private Vector3 initPos1, initPos2;
    private float distance;
    private Vector3 move1to2Dir;
    private float time;

    private float period;
    private float animationStartTime;
    private bool isFinish = false;
    
    public SwapPositionAnimation(Transform obj1, Transform obj2, float period)
    {
        this.obj1 = obj1;
        this.obj2 = obj2;
        this.period = period;



        initPos1 = obj1.position;
        initPos2 = obj2.position;
        distance = (initPos1 - initPos2).magnitude;
        move1to2Dir = (initPos1 - initPos2).normalized;
    }

    public bool PlayOneCut()
    {
        if(IsFinish())
        {
            return false;
        }

        if(Time.time - time < period)
        {
            obj1.Translate(move1to2Dir * distance * Time.deltaTime / period);
            obj2.Translate(-move1to2Dir * distance * Time.deltaTime / period);
            
        }
        else
        {
            Finish();
        }

        return true;
    }

    public void Finish()
    {
        obj1.position = initPos2;
        obj2.position = initPos1;
        isFinish = true;
    }

    public void Initalize()
    {
        isFinish = false;
        animationStartTime = Time.time;
        time = Time.time;
    }

    public bool IsFinish()
    {
        return isFinish;
    }
}


public class GachaControl : MonoBehaviour
{
    public Transform RedArrow;
    public Transform YellowArrow;
    public Transform BlueArrow;
    public float period;


    private SwapPositionAnimation swapAni;
    private bool isStop = false;




    void Update()
    {

        if (swapAni == null || swapAni.IsFinish())
        {
            if(!isStop)
            {
                int excludeIndex = Random.Range(0, 3);
                switch (excludeIndex)
                {
                    case 0:
                        swapAni = new SwapPositionAnimation(YellowArrow, BlueArrow, period);
                        swapAni.Initalize();
                        break;
                    case 1:
                        swapAni = new SwapPositionAnimation(RedArrow, BlueArrow, period);
                        swapAni.Initalize();
                        break;
                    case 2:
                        swapAni = new SwapPositionAnimation(RedArrow, BlueArrow, period);
                        swapAni.Initalize();
                        break;
                    default:
                        swapAni = null;
                        break;
                }
            }
        }


        if (swapAni != null)
        {
            swapAni.PlayOneCut();
        }
        
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isStop = true;
        }
    }
}
