using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamera : MonoBehaviour
{

    public Transform cameraPosition;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
