using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingItemControl : MonoBehaviour
{

    public Vector3 MoveDirection;
    public Vector3 Axis;
    public float speed;
    public float angleSpeed;



    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < GameSys.DeathHeight)
        {
            gameObject.SetActive(false);
        }

        transform.Translate(MoveDirection * speed * Time.deltaTime, Space.World);
        transform.Rotate(2f * Mathf.PI * Axis * angleSpeed * Time.deltaTime);
    }
}
