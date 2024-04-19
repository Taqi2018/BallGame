using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarRotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate direction from health bar to camera
        Vector3 directionToCamera = transform.position - Camera.main .transform.position ;

        // Ignore the y-component (keeps the health bar upright)
        directionToCamera.y = 0;

        // Calculate rotation to face the camera
        Quaternion rotationToCamera = Quaternion.LookRotation(directionToCamera);

        // Apply rotation to health bar
        transform.rotation = rotationToCamera;
    }
}
