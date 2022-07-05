using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    Camera camera;
    float zoomCamera = 60f;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        camera.fieldOfView = zoomCamera;

        zoomCamera = Mathf.Clamp(zoomCamera, 40f, 70f);

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            zoomCamera--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            zoomCamera++;
        }
    }
}
