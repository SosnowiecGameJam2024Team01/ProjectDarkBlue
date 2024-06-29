using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartingRaceController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private MovementController[] players;
    private int timer = 3;
    void Start()
    {
        ChangePause(true);
        StartCoroutine(ReverseCounting());
    }
    private void ChangePause(bool mode)
    {
        foreach (MovementController player in players)
            player.isPaused = mode;
    }

    private IEnumerator ReverseCounting()
    {
        while(timer >= 0)
        {
            yield return new WaitForSeconds(2);
            timerTxt.text = timer.ToString();
            timer--;
        }
        timerTxt.text = "GO!";
        ChangePause(false);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
