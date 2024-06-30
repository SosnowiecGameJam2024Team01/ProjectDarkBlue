using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DirectionalDrag : MonoBehaviour
{
    [Header("Drag Settings")]
    public float dragForward = 0.5f;     // Drag in the forward direction (local y-axis)
    public float dragSideways = 2.0f;    // Drag in the sideways direction (local x-axis)
    private float dragGrass = 1.25f;



    private Rigidbody2D rb;
    private MovementController controller;
    private bool slowed = false;
	private bool iced = false;
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<MovementController>();
        if(controller == null)
        {
            controller= GetComponent<FakeMovementController>().realController;
        }
    }

    void FixedUpdate()
    {
        // Get the local velocity
        Vector2 localVelocity = transform.InverseTransformDirection(rb.velocity);

        // Apply different drag forces in local directions
        localVelocity.x *= 1.0f - (iced&&!controller.isFlying ? dragSideways *0.5f : dragSideways) * Time.fixedDeltaTime;
        localVelocity.y *= 1.0f - (iced && !controller.isFlying ? dragForward * 0.5f : dragForward) * Time.fixedDeltaTime;

        if (slowed && !controller.isFlying)
        localVelocity *= 1f - dragGrass * Time.fixedDeltaTime;

        // Convert the local velocity back to world space
        rb.velocity = transform.TransformDirection(localVelocity);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PersistantEffect"))
        {
            switch(collision.GetComponent<Persistant>().type)
            {
                case Persistant.PersistantType.Slow:
                    slowed = true;
                    break;
                case Persistant.PersistantType.Ice:
                    iced = true;
					break;
			}
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PersistantEffect"))
        {
			switch (collision.GetComponent<Persistant>().type)
			{
				case Persistant.PersistantType.Slow:
					slowed = false;
					break;
				case Persistant.PersistantType.Ice:
					iced = false;
					break;
			}
		}
    }
}
