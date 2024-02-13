using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanControl : MonoBehaviour
{
    public Transform Propeller;
    public Transform Wind;
    public ParticleSystem ParticleSystem;
    public float AngleSpeed;
    public float AngleToWind = 0.02f;
    public float AngleToRate = 0.02f;


    // Start is called before the first frame update
    void Start()
    {
        Wind.GetComponent<WindInfo>().SetWindSpeed(AngleSpeed * AngleToWind);
        var emission = ParticleSystem.emission;
        emission.rateOverTime = AngleSpeed * AngleToRate;
    }

    // Update is called once per frame
    void Update()
    {
        Propeller.Rotate(Vector3.left * AngleSpeed * Time.deltaTime);

    }
}
