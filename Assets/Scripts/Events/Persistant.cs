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

	private void Start()
	{
		//transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
	}
	private void Update()
	{
	}


}
