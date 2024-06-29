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
		if (collision.GetComponent<MovementController>() == null || collision.GetComponent<MovementController>().isFlying) return;
		
		MovementController controller = collision.GetComponent<MovementController>();

        switch (pickupType)
        {
            case PickupType.Ability:
				controller.abilityBar=Mathf.Min(controller.abilityBar+1, controller.maxAbilityBar);
				break;
            case PickupType.Wobble:
                controller.EnableWobble(10);
				break;
			case PickupType.Knockback:
                controller.Knockback(transform.position, 1000);
				break;
			case PickupType.Bird:
				CanvasEffects.Instance.ShowBird(collision.gameObject.GetComponent<Player>().type);
				break;
        }
        Destroy(gameObject);
    }
}
