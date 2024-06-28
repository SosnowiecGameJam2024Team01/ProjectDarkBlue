using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
	Dionysus,
	Venus,
	All
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

	public static Player dionysus { get; private set; }
	public static Player venus { get; private set; }

	private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	}

	public void AddPlayer(PlayerType type, Player controller)
	{
		switch (type)
		{
			case PlayerType.Dionysus:
				if (dionysus != null) Debug.LogError("Dionysus already exists");
				dionysus = controller;
				break;
			case PlayerType.Venus:
				if (venus != null) Debug.LogError("Venus already exists");
				venus = controller;
				break;
		}
	}

	public Vector2 GetPointInFrontInView(PlayerType type)
	{
		//tofill
		return Vector2.zero;
	}

	public Vector2 GetPointInFrontNotInView(PlayerType type)
	{
		//to fill
		return Vector2.zero;
	}

	public PlayerType GetFirstPlace()
	{
		//To fill
		return PlayerType.Dionysus;
	}

	public float GetDistance()
	{
		//To fill
		return 0;
	}

	public bool InVision(Vector2 position)
	{
		return Vector2.Distance(dionysus.transform.position, position) < dionysus.sightRange || Vector2.Distance(venus.transform.position, position) < venus.sightRange;
	}
}
