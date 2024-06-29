using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistant : MonoBehaviour
{
	public enum PersistantType
    {
        Slow,
        Speed,
        Wobble
    }

    public PersistantType type; 
}
