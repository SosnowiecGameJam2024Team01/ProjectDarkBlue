using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBananana : EventBase
{
	[SerializeField] GameObject prefab;

	public override void Trigger(PlayerType type)
	{
		Instantiate(prefab, PlayerController.Instance.GetPointInFrontInView(type), Quaternion.identity);
	}

}
