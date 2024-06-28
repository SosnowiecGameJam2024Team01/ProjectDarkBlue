using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Car Settings")]
    public bool arrows = false;
    public float acceleration = 15f;       // Acceleration force applied to the car
    public float steering = 2f;            // How much the car can steer


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
        if (arrows)
        {
            inputVertical = Input.GetAxis("Vertical");
            inputHorizontal = Input.GetAxis("Horizontal");
        }
        else
        {
            inputVertical = Input.GetAxis("Vertical2");
            inputHorizontal = Input.GetAxis("Horizontal2");
        }
        
    }

    void FixedUpdate()
    {
       

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
