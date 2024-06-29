using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
<<<<<<< Updated upstream
    float lifeTime = 3;
=======
	public string pickupType = "None";
    [SerializeField]float lifeTime = 3;

>>>>>>> Stashed changes
	
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
        if(pickupType == "Boost")
        {
            collision.gameObject.GetComponent<MovementController>().EnableBoost(10);
          }

        if (pickupType == "Wobble")
        {
            collision.gameObject.GetComponent<MovementController>().EnableWobble(10);
        }

        Destroy(gameObject);
    }
}
