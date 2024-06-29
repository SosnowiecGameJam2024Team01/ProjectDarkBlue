using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canGr;
    [SerializeField] private float fadeSpeed = 0.1f; // Скорость плавного исчезновения
    [SerializeField] private Button[] buttons;
    [SerializeField] private MovementController[] players;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePause();
            StartCoroutine(Fading(isPaused));
        }
    }

    private IEnumerator Fading(bool mode)
    {
        InteractableInButtons(false);

        float targetAlpha = mode ? 1 : 0;
        while (!Mathf.Approximately(canGr.alpha, targetAlpha))
        {
            canGr.alpha = Mathf.MoveTowards(canGr.alpha, targetAlpha, fadeSpeed * Time.unscaledDeltaTime);
            if (!canGr.gameObject.activeInHierarchy) canGr.gameObject.SetActive(true);
            yield return null;
        }

        canGr.gameObject.SetActive(mode);
        InteractableInButtons(true);
        Time.timeScale = mode ? 0 : 1;
    }

    private void ChangePause()
    {
        isPaused = !isPaused;
        foreach (MovementController player in players)
            player.isPaused = isPaused;
    }

    private void InteractableInButtons(bool mode)
    {
        for (int i = 0; i < buttons.Length; ++i)
            buttons[i].interactable = mode;
    }
    //Buttons
    public void Resume()
    {
        ChangePause();
        StartCoroutine(Fading(false));
    }
    public void StartNewRace()
    {
        ChangePause();
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("ChariotScene");
    }
    public void MainMenu()
    {
        ChangePause();
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("MainMenuScene");
    }
    public void Exit()
    {
        ChangePause();
        Time.timeScale = 1;
        Application.Quit();
    }
}
