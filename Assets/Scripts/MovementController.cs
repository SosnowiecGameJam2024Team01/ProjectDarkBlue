using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Car Settings")]
    public bool arrows = false;
    public float acceleration = 15f;       // Acceleration force applied to the car
    public float boostMultiplier = 1.3f;
    public float steering = 2f;            // How much the car can steer
    public int abilityBar = 0;
    public int maxAbilityBar = 9;

    private Rigidbody2D rb;
    private float currentSpeed = 0f;
    private float inputVertical;
    private float inputHorizontal;
    private float boostTimer = 0f;
    private float wobbleTimer = 0f;
	private float iceTimer = 0f;

    public bool isPaused = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        if (isPaused) return;
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

        //wobble and boost testing REMOVE
        if (Input.GetKeyDown(KeyCode.E)) { EnableWobble( 10); }
        if (Input.GetKeyDown(KeyCode.Q)) { EnableBoost( 10); }
    }

    void FixedUpdate()
    {

        Vector2 driveDir = transform.up;
        if (wobbleTimer > 0f)
        {
            driveDir = transform.up + transform.right*Mathf.Sin(Time.time*8)/2;
            wobbleTimer -= Time.fixedDeltaTime;
        }
        // Apply forward force based on player input
        Vector2 forward = driveDir.normalized * inputVertical * acceleration;



        if (boostTimer > 0f) { 
            forward *= boostMultiplier;
            boostTimer -= Time.fixedDeltaTime;
        }

        rb.AddForce(forward);

        // Apply steering based on player input
        
        rb.angularVelocity -= inputHorizontal * (iceTimer > 0 ? steering * 0.5f : steering);
        
    }
	void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("PersistantEffect"))
		{
			switch (collision.GetComponent<Persistant>().type)
			{
				case Persistant.PersistantType.Speed:
                    EnableBoost(0.1f);
					break;
				case Persistant.PersistantType.Wobble:
					EnableWobble(0.1f);
					break;
                case Persistant.PersistantType.Ice:
                    EnableIce(0.1f);
                    break;
			}
		}
	}

	private void OnDrawGizmos()
    {
        // Visualize car's current speed in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * currentSpeed);
    } 

    public void EnableBoost(float boostLength)
    {
        boostTimer = Mathf.Max(boostLength, boostTimer);
    }
    public void EnableWobble(float wobbleLength)
    {
        wobbleTimer = Mathf.Max(wobbleLength, wobbleTimer);
    }

    public void EnableIce(float iceLength)
	{
		iceTimer = Mathf.Max(iceLength, iceTimer);
	}
    
    public void Knockback(Vector2 from, float strength)
    {
        rb.AddForce(((Vector2)transform.position - from).normalized * strength);
    }
        
    private void UseAbility()
    {
        int abilityLevel = (abilityBar * 3)  / maxAbilityBar;
        abilityBar -= abilityLevel * maxAbilityBar / 3;
        switch (abilityLevel)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;

        }
        

}
