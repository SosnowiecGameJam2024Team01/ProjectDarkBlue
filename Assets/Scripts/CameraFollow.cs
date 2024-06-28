using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    // The player object that the camera will follow
    public Vector3 offset;      // Offset from the target object
    public float smoothSpeed = 0.125f;  // Speed of the camera following the player

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.fixedDeltaTime);
            transform.position = smoothedPosition;
        }
    }
}
