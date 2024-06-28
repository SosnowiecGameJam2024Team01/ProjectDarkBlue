using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Car Settings")]
    public float acceleration = 15f;       // Acceleration force applied to the car
    public float maxSpeed = 20f;           // Maximum speed of the car
    public float steering = 2f;            // How much the car can steer

    [Header("Drag Settings")]
    public float dragForward = 0.5f;     // Drag in the forward direction (local y-axis)
    public float dragSideways = 2.0f;    // Drag in the sideways direction (local x-axis)


    private Rigidbody2D rb;
    private float currentSpeed = 0f;
    private float inputVertical;
    private float inputHorizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        // Get player input
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");
        
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

        // Apply forward force based on player input
        Vector2 forward = transform.up * inputVertical * acceleration;
        rb.AddForce(forward);

        // Apply steering based on player input
        
        rb.angularVelocity -= inputHorizontal * steering ;
        
    }

    private void OnDrawGizmos()
    {
        // Visualize car's current speed in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * currentSpeed);
    } 
}
