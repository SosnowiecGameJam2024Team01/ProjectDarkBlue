using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] PlayerType type;
	public float sightRange;


	private void Start()
	{
		PlayerController.Instance.AddPlayer(type, this);
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, sightRange);
	}
}
