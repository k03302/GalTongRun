using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAni : MonoBehaviour
{
    public Transform Neck;

    public float rotateSpeed = 240f;
    public float speed = 2.0f;
    public float rollingSpeed = 30f;

    private float time;
    private float frequency = 0.5f;
    private float scale;

    private float Increase;
    private float Decrease;
    private float targetScale;

    private Vector3 initLocalPosition;
    private Vector3 initLocalEular;
    private float initScale;
    private bool isRolling = false;

    private float MaxScale = 18f;
    private float MinScale = 0.5f;


    private void Awake()
    {
        initLocalPosition = transform.localPosition;
        initLocalEular = transform.localEulerAngles;
        initScale = transform.localScale[0];
        scale = initScale;
        targetScale = scale;

        Increase = 0.1f;
        Decrease = 1.0f - 1.0f / (1.0f + Increase);

        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(targetScale);
        if (/*targetScale != scale && */!isRolling)
        {
            if (time < frequency)
            {
                transform.Translate(Vector3.up * Time.deltaTime * GetSpeed());
                transform.Rotate(2 * Mathf.PI * Vector3.up * Time.deltaTime * Mathf.Sign(targetScale - scale) * GetRotateSpeed());
                transform.localScale = Vector3.one * targetScale;
                scale += Time.deltaTime * (targetScale - scale) / frequency;

                time += Time.deltaTime;
            }

            else
            {
                scale = targetScale;
                FinishAnimation();
            }
        }
        if(/*targetScale != scale && */isRolling)
        {
            if (time < frequency)
            {
                transform.localScale = Vector3.one * scale;
                scale += Time.deltaTime * (targetScale - scale) / frequency;
                time += Time.deltaTime;
            }
            else
            {
                scale = targetScale;
                Restart(0);
                ResizeNeck(scale);
            }
        }
        if(isRolling)
        {
            transform.Rotate(2 * Mathf.PI * Vector3.right * Time.deltaTime * rollingSpeed);
        }
        
    }

    public void Restart(int arg)
    {
        // 애니메이션  시간을 0으로 초기화
        time = 0.0f;
        // 위치 초기화. 머리 크기에 따라서 초기화 위치 재조정
        transform.localPosition = initLocalPosition + (scale - initScale) * 0.1f * Vector3.up;
        // 각도 초기화
        if(!isRolling)
        {
            transform.localEulerAngles = initLocalEular;
        }
        

        // 본체 머리크기를 타겟스케일로 바꾼 후 타겟스케일 변경

        transform.localScale = Vector3.one * targetScale;
        float temp;
        if (arg == 1)
        {
            temp = targetScale * (1.0f + Increase);
            if(temp < MaxScale)
            {
                targetScale = temp;
            }
        }
        else if (arg == -1)
        {
            temp = targetScale * (1.0f - Decrease);
            if(temp > MinScale)
            {
                targetScale = temp;
            }
        }
        else
        {
            targetScale = scale;
        }
    }


    public void FinishAnimation()
    {
        scale = targetScale;
        /*
        gameObject.SetActive(true);
        transform.localScale = Vector3.one * scale;
        */
        ResizeNeck(targetScale);
        Restart(0);
        gameObject.SetActive(false);
    }


    public void SetRolling(bool isRolling)
    {
        this.isRolling = isRolling;
    }

    public void ResizeWithoutAnimation(bool isIncrease)
    {
        float temp;
        if (isIncrease)
        {
            temp = scale * (1.0f + Increase);
            if (temp < MaxScale)
            {
                scale = temp;
                targetScale = temp;
            }
        }
        else
        {
            temp = scale * (1.0f - Decrease);
            if (temp > MinScale)
            {
                scale = temp;
                targetScale = temp;
            }
        }
        ResizeNeck(targetScale);
    }


    float GetSpeed()
    {
        return speed * (Mathf.Sin(2 * Mathf.PI * time / frequency));
    }
    float GetRotateSpeed()
    {
        return rotateSpeed; // * Mathf.Sin(Mathf.PI * time / frequency);
    }

    void ResizeNeck(float myScale)
    {
        Neck.localScale = Vector3.one * (myScale / 2);
    }
}
