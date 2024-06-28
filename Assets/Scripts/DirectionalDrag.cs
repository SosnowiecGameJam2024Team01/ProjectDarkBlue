using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DirectionalDrag : MonoBehaviour
{
    [Header("Drag Settings")]
    public float dragForward = 0.5f;     // Drag in the forward direction (local y-axis)
    public float dragSideways = 2.0f;    // Drag in the sideways direction (local x-axis)
    private float dragGrass = 5f;

    private Rigidbody2D rb;
    private bool onGrass = false;
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

        if (onGrass )
        localVelocity *= 1f - dragGrass * Time.fixedDeltaTime;

        // Convert the local velocity back to world space
        rb.velocity = transform.TransformDirection(localVelocity);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grass"))
        {
            onGrass = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grass"))
        {
            onGrass = false;
        }
    }
}
