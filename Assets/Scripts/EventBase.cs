using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EventBase : MonoBehaviour
{
	public bool triggerInView;
	public PlayerType playerTriggerType;

	public abstract void Trigger(PlayerType type);

}