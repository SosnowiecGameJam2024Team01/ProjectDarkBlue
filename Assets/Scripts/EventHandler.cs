using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
	public static EventHandler Instance;

	[SerializeField] Vector2 eventTriggerTime;

	[SerializeField] List<EventBase> dionysusEvents;
	[SerializeField] List<EventBase> venusEvents;

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
			next.Trigger(PlayerType.Dionysus);
			nextDionysusEvent = Time.time + Random.Range(eventTriggerTime.x, eventTriggerTime.y); 
		}

		if (nextVenusEvent < Time.time)
		{
			EventBase next = venusEvents[Random.Range(0, venusEvents.Count)];
			next.Trigger(PlayerType.Venus);
			nextVenusEvent = Time.time + Random.Range(eventTriggerTime.x, eventTriggerTime.y);
		}
	}
}
