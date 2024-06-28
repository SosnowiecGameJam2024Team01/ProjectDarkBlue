using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DirectionalDrag : MonoBehaviour
{
    [Header("Drag Settings")]
    public float dragForward = 0.5f;     // Drag in the forward direction (local y-axis)
    public float dragSideways = 2.0f;    // Drag in the sideways direction (local x-axis)

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Get the local velocity
        Vector2 localVelocity = transform.InverseTransformDirection(rb.velocity);

        // Apply different drag forces in local directions
        localVelocity.x *= 1.0f - dragSideways * Time.fixedDeltaTime;
        localVelocity.y *= 1.0f - dragForward * Time.fixedDeltaTime;

        // Convert the local velocity back to world space
        rb.velocity = transform.TransformDirection(localVelocity);
    }
}
