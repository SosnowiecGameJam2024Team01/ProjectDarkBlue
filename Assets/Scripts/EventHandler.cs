using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
class EventZone
{
	public Vector2 position;
	public Vector2 size;
}

public class EventHandler : MonoBehaviour
{
	public static EventHandler Instance;

	public static int eventCount = 0;

	[SerializeField] Vector2 eventTriggerTime;

	[SerializeField] List<EventBase> dionysusEvents;

	[SerializeField] EventZone zone;
	[SerializeField] Collider2D trueBound;
	[SerializeField] Collider2D coliderBound;

	[SerializeField] int minEventCount = 40;

	void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);

	}

	private void Update()
	{
		TriggerEvent();
	}

	void TriggerEvent()
	{
		if (eventCount < minEventCount)
		{
			EventBase next = dionysusEvents[Random.Range(0, dionysusEvents.Count)];
			next.Trigger(PlayerType.Dionysus, FindPlaceToSpawn());
			eventCount++;
		}
	}

	Vector2 FindPlaceToSpawn()
	{
		Vector2 position;

		do
		{
			position = zone.position + new Vector2(Random.Range(-zone.size.x, zone.size.x), Random.Range(-zone.size.y, zone.size.y));
		}
		while (PlayerController.Instance.InVision(position) || !trueBound.bounds.Contains(position) || coliderBound.bounds.Contains(position));
		return position;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.black;

			Gizmos.DrawLine(zone.position + new Vector2(zone.size.x, zone.size.y), zone.position + new Vector2(zone.size.x, -zone.size.y));
			Gizmos.DrawLine(zone.position + new Vector2(zone.size.x, -zone.size.y), zone.position + new Vector2(-zone.size.x, -zone.size.y));
			Gizmos.DrawLine(zone.position + new Vector2(-zone.size.x, -zone.size.y), zone.position + new Vector2(-zone.size.x, zone.size.y));
			Gizmos.DrawLine(zone.position + new Vector2(-zone.size.x, zone.size.y), zone.position + new Vector2(zone.size.x, zone.size.y));
	}

}
