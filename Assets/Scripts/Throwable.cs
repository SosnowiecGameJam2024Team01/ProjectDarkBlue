using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Debris
{
	[SerializeField] float force;
	private void Awake()
	{
		gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
	}
}
