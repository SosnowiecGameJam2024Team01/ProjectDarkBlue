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
		Bird,
		Moving,
		None,
	}
	public PickupType pickupType = PickupType.Ability;
    [SerializeField]float lifeTime = 3;
	public MovementController thrownByController = null;

	[SerializeField] float speed;
	[SerializeField] float respawn;

	private void Start()
	{
		transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
	}

	private void Update()
	{
		CheckIfToDestroyed();
		transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.up, speed * Time.deltaTime);
	}

	void CheckIfToDestroyed()
    {
		lifeTime -= Time.deltaTime;
		if (lifeTime > 0 || PlayerController.Instance.InVision(transform.position)) return;
        else
        {
            Destroy(gameObject);
			EventHandler.eventCount--;
		}
		
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
		MovementController controller;

        if (collision.GetComponent<MovementController>() == null) {
			if (collision.GetComponent<FakeMovementController>() == null) return;
			else controller = collision.GetComponent<FakeMovementController>().realController;
		}
		else
		{
            controller = collision.GetComponent<MovementController>();
        }




		if (controller.isFlying || controller == thrownByController) return;
		
		

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
