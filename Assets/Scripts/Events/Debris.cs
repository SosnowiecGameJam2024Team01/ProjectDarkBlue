using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Debris;


public class Debris : MonoBehaviour
{
	public enum PickupType
	{
		Ability,
		Wobble,
		Knockback,
		Bird
	}
	public PickupType pickupType = PickupType.Ability;
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
		
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (pickupType)
        {
            case PickupType.Ability:
				collision.gameObject.GetComponent<MovementController>().abilityBar++;
				break;
            case PickupType.Wobble:
				collision.gameObject.GetComponent<MovementController>().EnableWobble(10);
				break;
			case PickupType.Knockback:
				collision.gameObject.GetComponent<MovementController>().Knockback(transform.position, 1000);
				break;
			case PickupType.Bird:
				CanvasEffects.Instance.ShowBird(collision.gameObject.GetComponent<Player>().type);
				break;
        }
        Destroy(gameObject);
    }
}
