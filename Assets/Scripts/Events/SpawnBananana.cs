using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBananana : EventBase
{
	[SerializeField] GameObject prefab;

	public override void Trigger(PlayerType type, Vector2 position)
	{
		Instantiate(prefab, position, Quaternion.identity);
	}

}
