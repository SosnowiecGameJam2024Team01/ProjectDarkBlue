using System.Collections;
using System.Collections.Generic;
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

	[SerializeField] Vector2 eventTriggerTime;

	[SerializeField] List<EventBase> dionysusEvents;
	[SerializeField] List<EventBase> venusEvents;

	[SerializeField] List<EventZone> dropZones;

	float nextDionysusEvent;
	float nextVenusEvent;

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
		if(nextDionysusEvent < Time.time)
		{
			EventBase next = dionysusEvents[Random.Range(0, dionysusEvents.Count)];
			next.Trigger(PlayerType.Dionysus, FindPlaceToSpawn());
			nextDionysusEvent = Time.time + Random.Range(eventTriggerTime.x, eventTriggerTime.y); 
		}

		if (nextVenusEvent < Time.time)
		{
			EventBase next = venusEvents[Random.Range(0, venusEvents.Count)];
			next.Trigger(PlayerType.Venus, FindPlaceToSpawn());
			nextVenusEvent = Time.time + Random.Range(eventTriggerTime.x, eventTriggerTime.y);
		}
	}

	Vector2 FindPlaceToSpawn()
	{
		Vector2 position;

		do
		{
			EventZone zone = dropZones[Random.Range(0, dropZones.Count)];
			position = zone.position + new Vector2(Random.Range(-zone.size.x, zone.size.x), Random.Range(-zone.size.y, zone.size.y));
		}
		while (PlayerController.Instance.InVision(position));
		return position;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.black;
		dropZones.ForEach(zone =>
		{
			Gizmos.DrawLine(zone.position + new Vector2(zone.size.x, zone.size.y), zone.position + new Vector2(zone.size.x, -zone.size.y));
			Gizmos.DrawLine(zone.position + new Vector2(zone.size.x, -zone.size.y), zone.position + new Vector2(-zone.size.x, -zone.size.y));
			Gizmos.DrawLine(zone.position + new Vector2(-zone.size.x, -zone.size.y), zone.position + new Vector2(-zone.size.x, zone.size.y));
			Gizmos.DrawLine(zone.position + new Vector2(-zone.size.x, zone.size.y), zone.position + new Vector2(zone.size.x, zone.size.y));
		});
	}

}
