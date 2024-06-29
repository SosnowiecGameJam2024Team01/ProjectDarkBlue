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
    [Header("Flying")]
    public ParticleSystem[] particlesToPause;
    public ParticleSystem[] particlesToPlay;
    [Header("Throwing")]
    public GameObject throwPrefab;
    public Transform throwPos;

    private Rigidbody2D rb;
    private float currentSpeed = 0f;
    private float inputVertical;
    private float inputHorizontal;
    private float boostTimer = 0f;
    private float wobbleTimer = 0f;
	private float iceTimer = 0f;
    private float flyTimer = 0f;

    public bool isPaused = false;
    public bool isFlying = false;

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
        if (Input.GetKeyDown(KeyCode.E)) { UseAbility(); }
        
    }

    void FixedUpdate()
    {
        if (isPaused) return;

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
        if (flyTimer > 0f)
        {
            flyTimer -= Time.fixedDeltaTime;
            if (flyTimer < 0)
            {
                isFlying = false;
                foreach(var pSys in particlesToPause)
                {
                    pSys.Play();
                }
                foreach(var pSys in particlesToPlay)
                {
                    pSys.Stop();
                }
            }
        }

        rb.AddForce(forward);

        // Apply steering based on player input
        
        rb.angularVelocity -= inputHorizontal * ((iceTimer > 0)&&!isFlying ? steering * 0.5f : steering);
        
    }
	void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("PersistantEffect") && !isFlying)
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
        int abilityLevel = (abilityBar * 3) / maxAbilityBar;
        abilityBar -= abilityLevel * maxAbilityBar / 3;
        switch (abilityLevel)
        {
            case 0:
                break;
            case 1:
                Throw();
                break;
            case 2:
                EnableBoost(3);
                break;
            case 3:
                MegaBoost(10);
                break;

        }
    }
    public void Throw()
    {
        GameObject thrownObj = Instantiate(throwPrefab, throwPos.position, throwPos.rotation);
        thrownObj.GetComponent<Debris>().thrownByController = this;
    }

    public void MegaBoost(float length)
    {
        EnableBoost(length);
        EnableFly(length);
    } 

    public void EnableFly(float length)
    {
        flyTimer = length;
        isFlying = true;
        wobbleTimer = 0;
        iceTimer = 0;
        foreach (var pSys in particlesToPause)
        {
            pSys.Stop();
        }
        foreach (var pSys in particlesToPlay)
        {
            pSys.Play();
        }
    }
}
