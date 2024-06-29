using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Debris;


public class Debris : MonoBehaviour
{
	public enum PickupType
	{
		Boost,
		Wobble,
		Knockback,
		Bird
	}
	public PickupType pickupType = PickupType.Boost;
    [SerializeField]float lifeTime = 3;
	

    private void Update()
	{
		CheckIfToDestroyed();
	}

	void CheckIfToDestroyed()
    {
		lifeTime -= Time.deltaTime;
		if (lifeTime > 0 || PlayerController.Instance.InVision(transform.position)) return;
        else
        {
            Destroy(gameObject);
        }
		ParticleSystem sa;
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (pickupType)
        {
            case PickupType.Boost:
				collision.gameObject.GetComponent<MovementController>().EnableBoost(10);
				break;
            case PickupType.Wobble:
				collision.gameObject.GetComponent<MovementController>().EnableWobble(10);
				break;
			case PickupType.Knockback:
				collision.gameObject.GetComponent<MovementController>().Knockback(transform.position, 1000);
				break;
			case PickupType.Bird:
				//birb spawn
				//collision.gameObject.GetComponent<Player>();
				break;
        }
        Destroy(gameObject);
    }
}
