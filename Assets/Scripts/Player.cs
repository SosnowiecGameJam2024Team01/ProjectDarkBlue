using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public PlayerType type;
	public float sightRange;


	private void Start()
	{
		Debug.Log(transform.name);
		PlayerController.Instance.AddPlayer(type, this);
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, sightRange);
	}
}
