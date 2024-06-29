using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
	[SerializeField] RectTransform dionysus;
	[SerializeField] RectTransform venus;
	[SerializeField] RectTransform map;

	private void Update()
	{
		Vector2 mapSize = map.sizeDelta;
		
		dionysus.anchoredPosition = PlayerController.Instance.GetRelativePosition(PlayerType.Dionysus) * mapSize - mapSize/2;
		venus.anchoredPosition = PlayerController.Instance.GetRelativePosition(PlayerType.Venus) * mapSize - mapSize / 2;
	}
}
