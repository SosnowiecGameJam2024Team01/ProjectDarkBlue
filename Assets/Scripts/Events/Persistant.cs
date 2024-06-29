using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistant : MonoBehaviour
{
	public enum PersistantType
    {
        Slow,
        Speed,
        Wobble,
        Ice
    }

    public PersistantType type;
	[SerializeField] float lifeTime = 3;
	private void Start()
	{
		transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
	}
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
			EventHandler.eventCount--;
		}


	}
}
