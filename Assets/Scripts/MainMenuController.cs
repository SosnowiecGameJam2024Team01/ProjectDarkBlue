using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
