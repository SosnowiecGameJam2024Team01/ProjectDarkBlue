using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR;

public class FieldController : MonoBehaviour
{
    public static FieldController Instance;
    [Header("Field Objects")]
    [SerializeField] private GameObject[] drivers;
    [SerializeField] private int numOfLaps;
    private (GameObject driverObj, int numOfLap, int numOfPonts, float time)[] driver;
    private int places;
    private string placesStats;

    [Header("Timer")]
    [SerializeField] private float accuracyOfTimer;
    private float timer = 0f;
    private IEnumerator timerCoroutine;

    [Header("MapBounds")]
	[SerializeField] Vector2 middlePoint;
	[SerializeField] Vector2 topRight;
	[SerializeField] Vector2 bottomLeft;


	private void Awake()
	{
        if (Instance == null) Instance = this;
        else Debug.LogError("Two field controllers");
	}

	void Start()
    {
        places = 0;
        timer = 0f;
        StartCoroutine(timerCoroutine = TimerCoroutine());
        driver = new (GameObject driverObj, int numOfLap, int numOfPonts, float time)[drivers.Length];
        for(int i = 0; i < driver.Length; ++i) 
            driver[i] = (drivers[i], 0, 0, timer);
    }

    public void SetStats(GameObject currentDriver, bool isStartPoint)
    {
        for (int i = 0; i < driver.Length; ++i)
        {
            if (driver[i].driverObj == currentDriver)
            {
                Debug.Log(isStartPoint);
                if (!isStartPoint) Debug.Log($"{++driver[i].numOfPonts}");
                else if (driver[i].numOfLap == numOfLaps)
                    Debug.Log($"{currentDriver.name} is {++places} place! \nTime {timer}");
                else
                {
                    ResetPoints(i);
                    Debug.Log($"Lap {++driver[i].numOfLap}");
                }
            }
        }
        SetPlaces();
    }
    void Update()
    {

    }
    private void ResetPoints(int driverNum) => driver[driverNum].numOfPonts = 0;
    private void SetPlaces()
    {
        placesStats = "";
        (GameObject driverObj, int numOfLap, int numOfPoints, float time)[] statsOfDrivers = driver;
        Array.Sort(statsOfDrivers, (x, y) => y.numOfPoints.CompareTo(x.numOfPoints));
        Array.Sort(statsOfDrivers, (x, y) => y.numOfLap.CompareTo(x.numOfLap));
        for (int i = 0; i < statsOfDrivers.Length; ++i)
            placesStats += $"{i + 1} Place: {statsOfDrivers[i].driverObj.name}\n";
        Debug.Log(placesStats);
    }

    private IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(accuracyOfTimer);
        timer += accuracyOfTimer;
        StartCoroutine(timerCoroutine = TimerCoroutine());
    }

    public Vector2 GetTopRight() { return topRight; }
    public Vector2 GetBottomLeft() { return bottomLeft;  }

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
        Gizmos.DrawLine(middlePoint + new Vector2(topRight.x, topRight.y), middlePoint + new Vector2(topRight.x, bottomLeft.y));
        Gizmos.DrawLine(middlePoint + new Vector2(topRight.x, bottomLeft.y), middlePoint + new Vector2(bottomLeft.x, bottomLeft.y));
        Gizmos.DrawLine(middlePoint + new Vector2(bottomLeft.x, bottomLeft.y), middlePoint + new Vector2(bottomLeft.x, topRight.y));
        Gizmos.DrawLine(middlePoint + new Vector2(bottomLeft.x, topRight.y), middlePoint + new Vector2(topRight.x, topRight.y));
	}
}
