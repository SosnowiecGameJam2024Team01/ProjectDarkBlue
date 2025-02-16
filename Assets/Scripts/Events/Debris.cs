using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static Debris;


public class Debris : MonoBehaviour
{
	public enum PickupType
	{
		Cabbages,
		Wobble,
		Knockback,
		Bird,
		Moving,
		None,
        AbilityDionysus,
		AbilityVenus,
	}
	public PickupType pickupType = PickupType.None;
    [SerializeField]float lifeTime = 3;
	float lifetimeRemain;
	public MovementController thrownByController = null;

	[SerializeField] float speed;
	[SerializeField] bool reset;
	[SerializeField] float respawn;
	[SerializeField] bool on;
	[SerializeField] GameObject destroyed;
	Vector2 starting;

	private void Start()
	{
		starting = transform.position;
		lifetimeRemain = lifeTime;
		on = true;
		//transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
	}

	private void Update()
	{
		if(reset) CheckIfToDestroyed();
        
        transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.up, speed * Time.deltaTime);
        
    }

	void CheckIfToDestroyed()
    {
		lifetimeRemain -= Time.deltaTime;
		if (lifetimeRemain > 0 || PlayerController.Instance.InVision(transform.position)) return;
        else
        {
            if (destroyed != null) destroyed.SetActive(false);
            transform.position = starting;
            if (destroyed != null) destroyed.SetActive(true);
            lifetimeRemain = lifeTime;
			//EventHandler.eventCount--;
		}
		
	}

	IEnumerator Return()
	{
		yield return new WaitForSeconds(respawn);

		while(PlayerController.Instance.InVision(transform.position))
		{
			yield return new WaitForSeconds(0.5f);
		}
		transform.GetComponent<SpriteRenderer>().enabled = true; 
		on = true;
		if (destroyed != null) destroyed.SetActive(false);
	}


    void OnTriggerEnter2D(Collider2D collision)
    {
		if (!on) return;
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
            case PickupType.Cabbages:
				controller.EnableWobble(3);
				//SoundController.Instance.PlaySound(SoundType.Cabbages);
				break;
            case PickupType.Wobble:
                controller.EnableWobble(3);
				break;
			case PickupType.Knockback:
                controller.Knockback(transform.position, 1000);
				break;
			case PickupType.Bird:
				CanvasEffects.Instance.ShowBird(controller.gameObject.GetComponent<Player>().type);
				break;
            case PickupType.AbilityVenus:
                if (controller.GetComponent<Player>().type == PlayerType.Venus)
				{
					//SoundController.Instance.PlaySound(SoundType.ItemPickup);
					controller.abilityBar = Mathf.Min(controller.abilityBar + 1, controller.maxAbilityBar);
				}
				else return;
				break;
            case PickupType.AbilityDionysus:
				if (controller.GetComponent<Player>().type == PlayerType.Dionysus)
				{
					//SoundController.Instance.PlaySound(SoundType.ItemPickup);
					controller.abilityBar = Mathf.Min(controller.abilityBar + 1, controller.maxAbilityBar);
				}
				else return;
				break;
        }
		if(destroyed!=null)destroyed.SetActive(true);
		on = false;
		transform.GetComponent<SpriteRenderer>().enabled = false;
		StartCoroutine(Return());

	}
}
