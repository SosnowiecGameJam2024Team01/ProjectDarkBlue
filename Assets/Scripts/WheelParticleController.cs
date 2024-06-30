using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleController : MonoBehaviour
{

    public ParticleSystem[] particleSystems;
    private Rigidbody2D rb;
    [SerializeField] AudioSource drift;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        foreach (var particle in particleSystems)
        {
            SetRateOverDistanceMultiplier(Mathf.Pow(Vector2.Dot(rb.transform.right, rb.velocity),2), particle);
        }
    }

    void SetRateOverDistanceMultiplier(float multiplier, ParticleSystem particleSystem)
    {
        var emissionModule = particleSystem.emission;
		drift.volume = multiplier*rb.velocity.magnitude/10;
		// Set the rateOverDistanceMultiplier directly
		emissionModule.rateOverDistanceMultiplier = multiplier;
    }
}
