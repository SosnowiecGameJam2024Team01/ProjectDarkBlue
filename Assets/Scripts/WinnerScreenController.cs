using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class WinnerScreenController : MonoBehaviour
{
    [SerializeField] private Sprite[] winners;
    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] AudioSource dio;
	[SerializeField] AudioSource wenus;

	private string theWinner;
    private string winnersList;
    private void Start()
    {
        Time.timeScale = 1;
        theWinner = PlayerPrefs.GetString("TheWinner");
        text.text = PlayerPrefs.GetString("WinnersInfo");
        Debug.Log(theWinner);
        switch(theWinner)
        {
            case "Wenus":
                bg.sprite = winners[0];
                wenus.Play();
                break;
            case "Dionysus":
                bg.sprite = winners[1];
                dio.Play();
				break;
            default: 
                bg.sprite = null;
                break;
        }
    }
    //Buttons
    public void StartNewRace()
    {
        SceneManager.LoadSceneAsync("ChariotScene");
    }
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
