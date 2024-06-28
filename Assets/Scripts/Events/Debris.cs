using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    float lifeTime = 3;
	
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
}
