using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform Player;
    private Vector3 distanceVector;
    private float camHeight;


    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        float[] distances = new float[32];
        distances[8] = 60f;
        distances[9] = 60f;
        camera.layerCullDistances = distances;

        camHeight = transform.position.y;
        distanceVector = new Vector3(0.0f, 4.0f, -4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPosition = Player.position + distanceVector;
        transform.position = new Vector3(camPosition.x, camHeight, camPosition.z);
    }
}
