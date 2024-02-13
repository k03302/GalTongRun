using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyTrigger : MonoBehaviour
{
    public Transform MovingEnemy;
    public Transform Sensor;
    public SensorControl SensorCon;


    // Start is called before the first frame update
    void Start()
    {
        if(MovingEnemy == null)
        {
            Debug.Log(gameObject.name);
        }
        MovingEnemy.gameObject.SetActive(false);
        SensorCon = Sensor.GetComponent<SensorControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SensorCon.PlayerEntered())
        {
            MovingEnemy.gameObject.gameObject.SetActive(true);
        }
        if(SensorCon.MovingEnemyEntered())
        {
            MovingEnemy.gameObject.gameObject.SetActive(false);
        }
    }


}
