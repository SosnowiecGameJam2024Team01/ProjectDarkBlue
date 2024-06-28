using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DriverController : MonoBehaviour
{
    [SerializeField] private GameObject points;
    private (GameObject pointObject, bool isPassed)[] point;
    [SerializeField] private GameObject startPoint;
    private FieldController field;
    void Start()
    {
        field = FindAnyObjectByType<FieldController>();
        point = new (GameObject pointObject, bool isPassed)[points.transform.childCount];
        for (int i = 0; i < point.Length; ++i)
            point[i] = (points.transform.GetChild(i).gameObject, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool IsAllPoints()
    {
        Debug.Log("Checking");
        for (int i = 0; i < point.Length; ++i)
            if (!point[i].isPassed) return false;
        return true;
    }
    private void SetPoint(GameObject currentPoint)
    {
        for (int i = 0; i < point.Length; ++i)
            if (point[i].pointObject == currentPoint && !point[i].isPassed) 
            { 
                point[i].isPassed = true;
                Debug.Log($"Point {point[i].isPassed}");
                field.SetStats(gameObject, false);
            }
    }
    private void ResetPoints()
    {
        for (int i = 0; i < point.Length; ++i)
            point[i].isPassed = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Point"))
            SetPoint(other.gameObject);
        if (other.CompareTag("StartZone") && IsAllPoints())
        {
            ResetPoints();
            field.SetStats(other.gameObject, true);
            Debug.Log("Lap passed");
        }
    }

}
