using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    [Header("Field Objects")]
    [SerializeField] private GameObject drivers;
    [SerializeField] private int numOfLaps;
    private (GameObject driverObj, int numOfLap, int numOfPonts, float time)[] driver;
    private int places;

    [Header("Timer")]
    [SerializeField] private float accuracyOfTimer;
    private float timer = 0f;
    private IEnumerator timerCoroutine;

    void Start()
    {
        places = 0;
        timer = 0f;
        StartCoroutine(timerCoroutine = TimerCoroutine());
        driver = new (GameObject driverObj, int numOfLap, int numOfPonts, float time)[drivers.transform.childCount];
        for(int i = 0; i < driver.Length; ++i) 
            driver[i] = (drivers.transform.GetChild(i).gameObject, 0, 0, timer);
    }

    public void SetStats(GameObject currentDriver, bool isStartPoint)
    {
        for (int i = 0; i < driver.Length; ++i)
        {
            if (driver[i].driverObj == currentDriver)
            {
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
    }
    void Update()
    {

    }
    private void ResetPoints(int driverNum) => driver[driverNum].numOfPonts = 0;

    private IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(accuracyOfTimer);
        timer += accuracyOfTimer;
        StartCoroutine(timerCoroutine = TimerCoroutine());
    }
}
