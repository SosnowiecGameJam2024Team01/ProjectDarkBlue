using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartingRaceController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private MovementController[] players;
    [SerializeField] AudioSource gong;
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
        while(timer >= 1)
        {
            yield return new WaitForSeconds(1);
            timerTxt.text = timer.ToString();
            timer--;
        }
		yield return new WaitForSeconds(0.5f);
		gong.Play();
		yield return new WaitForSeconds(0.5f);
		timerTxt.text = "GO!";
        ChangePause(false);
        
		yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
