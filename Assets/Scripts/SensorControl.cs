using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorControl : MonoBehaviour
{
    private bool PlayerEnter = false;
    private bool MovingEnemyEnter = false;
    private bool first = false;
    private bool first1 = false;

    public bool PlayerEntered()
    {
        if(PlayerEnter && first)
        {
            first = false;
            return true;
        }
        return false;
    }

    public bool MovingEnemyEntered()
    {
        if (MovingEnemyEnter && first1)
        {
            first1 = false;
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            first = true;
            PlayerEnter = true;
        }
        if(other.gameObject.CompareTag("MovingEnemy"))
        {
            Debug.Log("ok");
            first1 = true;
            MovingEnemyEnter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.name);
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerEnter = false;
        }
        if (other.gameObject.CompareTag("MovingEnemy"))
        {
            MovingEnemyEnter = false;
        }
    }
}
